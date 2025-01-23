using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Authorisation
{
    public class IsFlightDirectorAuthorizationHandler :
        AuthorizationHandler<IsFlightDirectorRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       IsFlightDirectorRequirement requirement)
        {
            if (context.User.HasClaim(ClaimTypes.Role, "FlightDirector"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class IsFlightDirectorRequirement : IAuthorizationRequirement { }
}
