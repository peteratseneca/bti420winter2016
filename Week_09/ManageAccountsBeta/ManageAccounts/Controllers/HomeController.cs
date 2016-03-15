using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageAccounts.Controllers
{
    public class HomeController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        public ActionResult Index()
        {
            return View();
        }
    }
}