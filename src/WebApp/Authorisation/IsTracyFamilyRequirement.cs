using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApp.Authorisation
{
    public class IsTracyFamilyAuthorizationHandler :
        AuthorizationHandler<IsTracyFamilyRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IsTracyFamilyRequirement requirement)
        {
            // If the users last name is "Tracy", then they're a member of the family
            if (context.User.HasClaim(ClaimTypes.Surname, "Tracy"))
            {
                context.Succeed(requirement);
            }

            // Brains (Actual name Hiram Hackenbacker) is like one of the family, so also gets access
            if (context.User.HasClaim(ClaimTypes.Name, "Hiram") &&
                context.User.HasClaim(ClaimTypes.Surname, "Hackenbacker"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class IsTracyFamilyRequirement : IAuthorizationRequirement { }
}
