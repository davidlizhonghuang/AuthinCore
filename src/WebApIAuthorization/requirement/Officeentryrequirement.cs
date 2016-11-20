using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApIAuthorization.requirement
{
    public class Officeentryrequirement: IAuthorizationRequirement
    {
    }
}

//one requirement

//two handlers temp pass, open gate for on erequirement, entry
//Sometimes you may want multiple handlers for an Authorization Requirement, for example when there are multiple ways to fulfill a requirement.Microsoft's office doors open with your Microsoft badge, however on days you forget your badge you can go to reception and get a temporary pass and the receptionist will let you through the gates. Thus there are two ways to fufill the single entry requirement. In the ASP.NET Core authorization model this would be implemented as two handlers for a single requirement.