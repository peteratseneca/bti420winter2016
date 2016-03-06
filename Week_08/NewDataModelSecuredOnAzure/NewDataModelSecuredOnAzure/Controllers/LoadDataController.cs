using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewDataModelSecuredOnAzure.Controllers
{
    // Protect all actions
    [Authorize(Roles = "Admin")]
    public class LoadDataController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: LoadData
        public ActionResult Index()
        {
            return View();
        }

        // GET: LoadData/Country
        public ActionResult Country()
        {
            if (m.LoadDataCountry())
            {
                ViewBag.Result = "Country data was loaded";
            }
            else
            {
                ViewBag.Result = "(done)";
            }
            return View("result");
        }

        // GET: LoadData/Manufacturer
        public ActionResult Manufacturer()
        {
            ViewBag.Result = m.LoadDataManufacturer()
                ? "Manufacturer data was loaded"
                : "(done)";

            return View("result");
        }

        // GET: LoadData/Vehicle
        public ActionResult Vehicle()
        {
            ViewBag.Result = m.LoadDataVehicle()
                ? "Vehicle data was loaded"
                : "(done)";

            return View("result");
        }

    }
}
