using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Authorisation
{
    public class IsCertifiedForCraftRequirement : IAuthorizationRequirement { }

    public class IsCertifiedForCraftAuthorizationResourceHandler(
        ILogger<IsCertifiedForCraftAuthorizationResourceHandler> logger) :
        AuthorizationHandler<IsCertifiedForCraftRequirement, Craft>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       IsCertifiedForCraftRequirement requirement,
                                                       Craft resource)
        {
            if (context.User.HasClaim("craft", resource.Name))
            {
                logger.LogInformation("User is certified for {craft}", resource.Name);
                context.Succeed(requirement);
            }
            else
            {
                logger.LogInformation("User is not certified for {craft}", resource.Name);
            }

            return Task.CompletedTask;
        }
    }
}
