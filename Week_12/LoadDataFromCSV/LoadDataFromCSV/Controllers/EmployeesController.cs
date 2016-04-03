using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoadDataFromCSV.Controllers
{
    public class EmployeesController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: Employees
        public ActionResult Index()
        {
            return View(m.EmployeeGetAll());
        }
    }
}
