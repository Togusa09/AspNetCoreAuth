using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApp.Models;

namespace WebApp.Authorisation
{
    public class IsAstronautRequirement : IAuthorizationRequirement { }

    public class IsAstronautAuthorizationHandler(
        Craft[] craft,
        ILogger<IsAstronautAuthorizationHandler> logger) :
        AuthorizationHandler<IsAstronautRequirement>
    {
        private readonly Craft[] _spaceCraft = craft.Where(c => c.SpaceWorthy).ToArray();

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IsAstronautRequirement requirement)
        {
            if (!context.User.HasClaim(ClaimTypes.Role, "Pilot"))
            {
                logger.LogInformation("User does not have {role}", "Pilot");
                return Task.CompletedTask;
            }

            logger.LogInformation("User has {role}", "Pilot");

            var intersection = context.User.FindAll("craft").IntersectBy(_spaceCraft.Select(c => c.Name), c => c.Value);
            if (!intersection.Any())
            {
                logger.LogInformation("User is not certified for any space worthy craft");
                return Task.CompletedTask;
            }

            logger.LogInformation("User is certified for space worthy craft");
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
