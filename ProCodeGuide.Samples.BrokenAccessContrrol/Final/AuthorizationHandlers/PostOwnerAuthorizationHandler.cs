using Microsoft.AspNetCore.Authorization;
using ProCodeGuide.Samples.BrokenAccessControl.DbEntities;
using ProCodeGuide.Samples.BrokenAccessControl.Services;
using System.Security.Claims;

namespace ProCodeGuide.Samples.BrokenAccessControl.AuthorizationHandlers
{
    public class PostOwnerAuthorizationHandler : AuthorizationHandler<IsPostOwnerRequirement, PostEntity>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsPostOwnerRequirement requirement, PostEntity resource)
        {
            if (context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value == resource.CreatedBy)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
