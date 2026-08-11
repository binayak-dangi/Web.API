using System.Linq.Expressions;

namespace Web.API.Repositories.Common
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplySorting<T>(
            this IQueryable<T> query,
            string? sort,
            string? sortDir)
        {
            if (string.IsNullOrWhiteSpace(sort))
                return query;

            var property = typeof(T).GetProperty(
                sort,
                System.Reflection.BindingFlags.IgnoreCase |
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance
            );

            if (property == null)
                return query;

            var parameter = Expression.Parameter(
                typeof(T),
                "x"
            );

            var propertyAccess = Expression.Property(
                parameter,
                property
            );

            var orderByExpression = Expression.Lambda(
                propertyAccess,
                parameter
            );

            var methodName =
                string.Equals(
                    sortDir,
                    "DESC",
                    StringComparison.OrdinalIgnoreCase
                )
                    ? "OrderByDescending"
                    : "OrderBy";

            var expression = Expression.Call(
                typeof(Queryable),
                methodName,
                new[]
                {
                    typeof(T),
                    property.PropertyType
                },
                query.Expression,
                Expression.Quote(orderByExpression)
            );

            return query.Provider
                .CreateQuery<T>(expression);
        }
    }
}