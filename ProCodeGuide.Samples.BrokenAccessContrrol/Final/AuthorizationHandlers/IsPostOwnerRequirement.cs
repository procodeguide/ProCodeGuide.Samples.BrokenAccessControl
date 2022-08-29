using Microsoft.AspNetCore.Authorization;

namespace ProCodeGuide.Samples.BrokenAccessControl.AuthorizationHandlers
{
    public class IsPostOwnerRequirement : IAuthorizationRequirement
    {
    }
}
