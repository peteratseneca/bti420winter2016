using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocOneToMany.Controllers
{
    public class HomeController : Controller
    {
        private Manager m = new Manager();

        public ActionResult Index()
        {
            //m.LoadData();

            return View();
        }
    }
}