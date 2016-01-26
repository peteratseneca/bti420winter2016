using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataAnnotations.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // ############################################################
        // Vehicle methods

        // Renders a "details" view
        public ActionResult DetailsVehicle(VehicleAdd item)
        {
            return View(item);
        }

        // Renders a "details" view
        public ActionResult DetailsVehiclePlain(VehicleAddPlain item)
        {
            return View(item);
        }

        // Display an HTML Form for vehicle "add new" use case
        // With useful data annotations

        public ActionResult CreateVehicle()
        {
            var form = new VehicleAdd();

            return View(form);
        }

        [HttpPost]
        public ActionResult CreateVehicle(VehicleAdd newItem)
        {
            return View("detailsvehicle", newItem);
        }

        // Display an HTML Form for vehicle "add new" use case
        // Without any data annotations

        public ActionResult CreateVehiclePlain()
        {
            return View(new VehicleAddPlain());
        }

        [HttpPost]
        public ActionResult CreateVehiclePlain(VehicleAddPlain newItem)
        {
            return View("detailsvehicleplain", newItem);
        }

        // ############################################################
        // Account methods

        // Renders a "details" view
        public ActionResult DetailsAccount(AccountAdd item)
        {
            return View(item);
        }

        // Renders a "details" view
        public ActionResult DetailsAccountPlain(AccountAddPlain item)
        {
            return View(item);
        }

        // Display an HTML Form for account "add new" use case
        // With useful data annotations

        public ActionResult CreateAccount()
        {
            return View(new AccountAdd());
        }

        [HttpPost]
        public ActionResult CreateAccount(AccountAdd newItem)
        {
            return View("detailsaccount", newItem);
        }

        // Display an HTML Form for account "add new" use case
        // Without any data annotations

        public ActionResult CreateAccountPlain()
        {
            return View(new AccountAddPlain());
        }

        [HttpPost]
        public ActionResult CreateAccountPlain(AccountAddPlain newItem)
        {
            return View("detailsaccountplain", newItem);
        }
    }
}