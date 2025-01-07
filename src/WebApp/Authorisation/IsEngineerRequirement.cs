using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Authorisation
{

    public class IsEngineerAuthorizationHandler :
        AuthorizationHandler<IsEngineerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       IsEngineerRequirement requirement)
        {
            if (context.User.HasClaim(ClaimTypes.Role, "Engineer"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class IsEngineerRequirement : IAuthorizationRequirement { }
}
