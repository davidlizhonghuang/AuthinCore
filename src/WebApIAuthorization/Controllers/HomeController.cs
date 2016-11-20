using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApIAuthorization.Controllers
{

    //david 5
    //[Authorize]  //run index.cshtml get authen david huang comes
    //but we can not add authorize in all controllers, so this is removed, we set in startup.cs

    //david 8 ---now we use role here
    // [Authorize( Roles = "Administrator")]  //david huang is not administrator, so forbidden



    //david 10 
    //  [Authorize(Policy = "adminpolicy")] //now we chnge to policy in ahthorize

    //david 13
    // [Authorize(Policy = "EmployeeId")] //if id is define in startup.cs, this is forbiddn


    //david 15
    //[Authorize(Policy = "EmployeeId")] //in claim add id in accountontroller

    //david 21
    // [Authorize(Policy = "Over21Only")] //from startup.cs register, it is a class requirement,  so claim in 2 yeas old, it should forbidden


    //david 25
    [Authorize(Policy = "BuildingEntry")]  //build entry policy use badgenumber
    //forbidden

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }


       
    }
}
