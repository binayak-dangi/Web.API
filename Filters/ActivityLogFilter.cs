using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.API.Helpers;
using Web.API.Models.Entities.Setup;
using Web.API.Repositories.Setup.Interfaces;

namespace Web.API.Filters;

public class ActivityLogFilter : IAsyncActionFilter
{
    private readonly IActivityLogRepository _activityLogRepository;

    public ActivityLogFilter(IActivityLogRepository activityLogRepository)
    {
        _activityLogRepository = activityLogRepository;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        // Execute the actual API
        var executedContext = await next();

        var httpContext = context.HttpContext;

        var user = httpContext.User;

        // Employee ID from JWT
        long.TryParse(
            user.FindFirstValue("IDHREmployee"),
            out var employeeId);

        // Company ID from JWT
        long.TryParse(
            user.FindFirstValue("IdHRCompany"),
            out var companyId);

        // Username
        var username =
            user.FindFirstValue(ClaimTypes.Name)
            ?? user.FindFirstValue("Username")
            ?? "Anonymous";

        // Controller
        var controller =
            context.Controller.GetType().Name
                .Replace("Controller", "");

        // HTTP method
        var method =
            httpContext.Request.Method;

        // Sanitize parameters
        var parameters =
            ActivityLogHelper.SanitizeParameters(
                context.ActionArguments);

        // IP Address
        var ipAddress =httpContext.Connection.RemoteIpAddress?
         .MapToIPv4()
         .ToString();

        // User Agent
        var userAgent =
            httpContext.Request.Headers["User-Agent"]
                .ToString();

        // Action
        var action =
            context.ActionDescriptor.DisplayName
            ?? "Unknown";

        var activityLog = new ActivityLog
        {
            IdEmployee = employeeId,

            IdHRCompany = companyId,

            Username = username,

            Action = action,

            Controller = controller,

            Method = method,

            Parameters = parameters,

            IPAddress = ipAddress,

            UserAgent = userAgent,

            Created_On = DateTime.Now,

            IdGUID = Guid.NewGuid()
        };

        await _activityLogRepository.LogAsync(activityLog);
    }
}