using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AjaxWithWebService.Controllers
{
    public class HomeController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Data()
        {
            // Attention - 3 - Study the "Data" view, and look for these features...
            // There is no HTML Form
            // Each button calls a JavaScript function
            // The "content" <div> will hold the data that's returned


            return View();

            // Attention - 4 - Study the "core.js" JavaScript source code file...
            // The fetchData() method handles Artist, Album, and Track
            // Notice how the user interface status is updated during the fetch task
            // Study how the HTML tables are built (yes, there are better ways to do this)
        }
    }
}