using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetAllGetOne.Controllers
{
    public class CustomersController : Controller
    {
        // Reference to a manager object
        private Manager m = new Manager();

        // GET: Customers
        public ActionResult Index()
        {
            // Fetch the collection
            var c = m.CustomerGetAll();

            // Pass the collection to the view
            return View(c);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.CustomerGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(o);
            }
        }
    }
}
