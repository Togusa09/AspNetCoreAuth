using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApp.Models;

namespace WebApp.Authorisation
{
    public class IsAstronautRequirement : IAuthorizationRequirement { }

    public class IsAstronautAuthorizationHandler(Craft[] craft) :
        AuthorizationHandler<IsAstronautRequirement>
    {
        private readonly Craft[] _spaceCraft = craft.Where(c => c.SpaceWorthy).ToArray();

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IsAstronautRequirement requirement)
        {
            if (!context.User.HasClaim(ClaimTypes.Role, "Pilot")) return Task.CompletedTask;

            var intersection = context.User.FindAll("craft").IntersectBy(_spaceCraft.Select(c => c.Name), c => c.Value);
            if (!intersection.Any())
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
