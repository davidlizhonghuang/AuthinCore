using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApIAuthorization.requirement
{
    public class HasTemporaryPassHandler : AuthorizationHandler<Officeentryrequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Officeentryrequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "TemporaryBadgeExpiry" &&
                                             c.Issuer == "https://www.y9se.com"))
            {
                return Task.FromResult(0);
            }

            var temporaryBadgeExpiry =
                Convert.ToDateTime(context.User.FindFirst(
                                       c => c.Type == "TemporaryBadgeExpiry" &&
                                       c.Issuer == "https://www.y9se.com").Value);

            if (temporaryBadgeExpiry > DateTime.Now)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;

        }
    }
}
