using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace WebApp.Authorisation
{
    public class IsFlightDirectorAuthorizationHandler(ILogger<IsFlightDirectorAuthorizationHandler> logger) :
        AuthorizationHandler<IsFlightDirectorRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       IsFlightDirectorRequirement requirement)
        {
            if (context.User.HasClaim(ClaimTypes.Role, "FlightDirector"))
            {
                logger.LogInformation("User has {role}", "FlightDirector");
                context.Succeed(requirement);
            }

            logger.LogInformation("User does not have {role}", "FlightDirector");
            return Task.CompletedTask;
        }
    }

    public class IsFlightDirectorRequirement : IAuthorizationRequirement { }
}
