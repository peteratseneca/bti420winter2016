using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoadDataFromXLSX.Controllers
{
    public class PlayersController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: Players
        public ActionResult Index()
        {
            return View(m.PlayerGetAll());
        }
    }
}
