using Microsoft.Extensions.Configuration;

namespace MinecraftServer;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _accessKey;

    public AuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _accessKey = $"{configuration["AccessKey"]}";
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        var header = authHeader.ToString();

        if (!header.StartsWith("Bearer "))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        var token = header.Substring("Bearer ".Length).Trim();

        if (token != _accessKey)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await _next(context);
    }
}

public static class AuthMiddlewareExtensions
{
    public static IApplicationBuilder UseAccessKeyAuth(this IApplicationBuilder app)
    {
        return app.UseMiddleware<AuthMiddleware>();
    }
}