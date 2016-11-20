using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Linq;
using System.Threading.Tasks;

namespace WebApIAuthorization.requirement
{
    public class DocumentEditHandler : AuthorizationHandler<EditRequirement, Document>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditRequirement requirement, Document resource)
        {
            if (resource.Author == context.User.FindFirst(ClaimTypes.Name).Value)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
