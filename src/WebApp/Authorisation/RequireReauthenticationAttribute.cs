using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Authorisation;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequireReauthenticationAttribute : Attribute, IAsyncResourceFilter
{
    private readonly int _timeElapsedSinceLast;
    public RequireReauthenticationAttribute(int timeElapsedSinceLast)
    {
        _timeElapsedSinceLast = timeElapsedSinceLast;
    }
    public async Task OnResourceExecutionAsync(
        ResourceExecutingContext context,
        ResourceExecutionDelegate next)
    {
        var loggerService = context.HttpContext.RequestServices.GetRequiredService<ILogger<RequireReauthenticationAttribute>>();


        var foundAuthTime = int.TryParse(context.HttpContext.User.FindFirst("auth_time")?.Value, out int authTime);

        DateTime currentTime = DateTime.UtcNow;
        long unixTime = ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
        var timeElapsed = unixTime - authTime;
        Debug.WriteLine($"Time elapsed: {timeElapsed} Required: {_timeElapsedSinceLast}");
        loggerService.LogInformation("Time elapsed: {timeElapsed} Required: {timeElapsedSinceLast}", timeElapsed, _timeElapsedSinceLast);

        if (foundAuthTime && timeElapsed < _timeElapsedSinceLast)
        {
            loggerService.LogInformation("Authentication has already occurred withing required interval");
            await next();
        }
        else
        {
            loggerService.LogInformation("Authentication token older than required, triggering re-auth");
            var state = new Dictionary<string, string> { { "reauthenticate", "true" } };
            await context.HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties(state)
            {
                RedirectUri = context.HttpContext.Request.Path
            });
        }
    }
}