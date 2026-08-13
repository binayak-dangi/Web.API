using System.Text.Json;

namespace Web.API.Helpers;

public static class ActivityLogHelper
{
    private static readonly HashSet<string> SensitiveFields =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "password",
            "oldPassword",
            "newPassword",
            "confirmPassword",
            "token",
            "accessToken",
            "refreshToken",
            "authorization",
            "secret",
            "clientSecret"
        };

    public static string SanitizeParameters(
        IDictionary<string, object?> parameters)
    {
        var sanitized = new Dictionary<string, object?>();

        foreach (var parameter in parameters)
        {
            sanitized[parameter.Key] =
                SanitizeValue(parameter.Key, parameter.Value);
        }

        return JsonSerializer.Serialize(sanitized);
    }

    private static object? SanitizeValue(
        string propertyName,
        object? value)
    {
        if (SensitiveFields.Contains(propertyName))
        {
            return "***";
        }

        if (value == null)
            return null;

        return SanitizeObject(value);
    }

    private static object? SanitizeObject(object value)
    {
        var type = value.GetType();

        // Simple values don't need processing
        if (type.IsPrimitive ||
            value is string ||
            value is decimal ||
            value is DateTime ||
            value is Guid)
        {
            return value;
        }

        var properties = type.GetProperties();

        var result = new Dictionary<string, object?>();

        foreach (var property in properties)
        {
            if (!property.CanRead)
                continue;

            var propertyName = property.Name;
            var propertyValue = property.GetValue(value);

            if (SensitiveFields.Contains(propertyName))
            {
                result[propertyName] = "***";
            }
            else
            {
                result[propertyName] =
                    propertyValue == null
                        ? null
                        : SanitizeObject(propertyValue);
            }
        }

        return result;
    }
}