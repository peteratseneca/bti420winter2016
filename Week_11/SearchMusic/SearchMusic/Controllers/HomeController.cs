using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchMusic.Controllers
{
    public class HomeController : Controller
    {
        // Reference to the data manager
        private Manager m = new Manager();

        public ActionResult Index()
        {
            return View();
        }
    }
}