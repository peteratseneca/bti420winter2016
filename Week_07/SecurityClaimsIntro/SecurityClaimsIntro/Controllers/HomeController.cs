using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecurityClaimsIntro.Controllers
{
    // Attention - 10 - Authorize tests
    // Configure methods with various Authorize attributes

    // Protect all methods in this controller
    [Authorize]
    public class HomeController : Controller
    {
        // Override class-level protection
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // No attribute needed - inherits from the class-level declaration
        public ActionResult AnyUser()
        {
            ViewBag.Result = "Success - any user";
            return View("result");
        }

        // Specific user "mary@example.com"
        [Authorize(Users = "mary@example.com")]
        public ActionResult MaryOnly()
        {
            ViewBag.Result = "Success - user mary@example.com";
            return View("result");
        }

        // Admin role only
        [Authorize(Roles = "Admin")]
        public ActionResult AdminsOnly()
        {
            ViewBag.Result = "Success - users in the Admin role";
            return View("result");
        }
    }
}