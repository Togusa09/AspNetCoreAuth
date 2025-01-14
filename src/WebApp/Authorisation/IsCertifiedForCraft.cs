using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Authorisation
{
    public class IsCertifiedForCraftRequirement : IAuthorizationRequirement { }

    public class IsCertifiedForCraftAuthorizationResourceHandler :
        AuthorizationHandler<IsCertifiedForCraftRequirement, Craft>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       IsCertifiedForCraftRequirement requirement,
                                                       Craft resource)
        {
            if (context.User.HasClaim("craft", resource.Name))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
