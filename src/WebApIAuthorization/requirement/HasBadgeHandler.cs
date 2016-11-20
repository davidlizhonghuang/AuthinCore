using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApIAuthorization.requirement
{
    public class HasBadgeHandler : AuthorizationHandler<Officeentryrequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Officeentryrequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "BadgeNumber" && c.Issuer == "https://www.y9se.com"))
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
