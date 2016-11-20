using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApIAuthorization.Controllers
{
    //david 7
    [AllowAnonymous]
    public class AccountController : Controller
    {
        //david 3
        public async Task<IActionResult> Login(string returnUrl=null)
        {
            const string Issuer = "https://www.y9se.com";
            var claims = new List<Claim>(); //each handler to open the door
            claims.Add(new Claim(ClaimTypes.Name,"david huang", ClaimValueTypes.String, Issuer));
            claims.Add(new Claim(ClaimTypes.Role, "Administrator", ClaimValueTypes.String, Issuer));
            //david 12 --add a handler 
            //claims.Add(new Claim("EmployeeId", string.Empty, ClaimValueTypes.String, Issuer));


            //david 13--define id here to authorize, so employid 12 can see
            claims.Add(new Claim("EmployeeId", "12", ClaimValueTypes.String, Issuer));

            //david 17 -- policy with condition   //2015 2 years old pass to policy
            claims.Add(new Claim(ClaimTypes.DateOfBirth, "1970-06-08", ClaimValueTypes.Date));
            //client side will compare with server side. startup.cs
            //you pass 2 years old, handler check this find too young so firbidden


            //david 23 ---one requirement many handlers
            claims.Add(new Claim("BadgeNumber", "123456", ClaimValueTypes.String, Issuer));
            claims.Add(new Claim("TemporaryBadgeExpiry",
                     DateTime.Now.AddDays(1).ToString(),
                     ClaimValueTypes.String,
                     Issuer));



            var userIdentity = new ClaimsIdentity("SuperSecureLogin");
            userIdentity.AddClaims(claims); //one principle has many claims from fcebook twitter, linkedin etcc

            var userPrincipal = new ClaimsPrincipal(userIdentity); //add all clims to principle
            await HttpContext.Authentication.SignInAsync("Cookie", userPrincipal,
                new Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                        IsPersistent=false,
                        AllowRefresh=false
                }
                );


            return RedirectToLocal(returnUrl);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }

        public IActionResult Forbidden()
        {
            return View();
        }



    }
}
