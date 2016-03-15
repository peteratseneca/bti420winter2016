using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageAccounts.Controllers
{
    public class LoadController : Controller
    {

        private Manager m = new Manager();
        // GET: Load
        public ActionResult Index()
        {
            m.LoadData();
            return RedirectToAction("Index", "Home");
        }
    }
}
