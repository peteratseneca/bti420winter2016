using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecurityIntro.Controllers
{
    // Attention - Protect all methods in this controller
    // Request must be authenticated
    [Authorize]
    public class HomeController : Controller
    {
        // Attention - Override the controller-level protection
        // Allow this action/method to run for all requests
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // This method is protected
        // Attention - Inherited from the controller-level protection
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // Override the controller-level protection
        // Attention - Allow only specific users to run the action/method
        [Authorize(Users = "john@example.com,mary@example.com")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}