using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LoadDataFromWebService.Controllers
{
    public class EmployeesController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            // Attempt to fetch the collection
            var c = await m.EmployeeGetAll();

            if (c == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(c);
            }
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            // Attempt to fetch the object
            var o = await m.EmployeeGetByIdWithDetails(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
        }
    }
}
