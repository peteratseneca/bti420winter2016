using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocSelf.Controllers
{
    public class EmployeesController : Controller
    {
        // Reference to the data manager
        private Manager m = new Manager();

        // GET: Employees
        public ActionResult Index()
        {
            return View(m.EmployeeGetAllWithOrgInfo());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.EmployeeGetByIdWIthOrgInfo(id.GetValueOrDefault());

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

        // ############################################################
        // Edit the supervisor (reports to)

        // GET: Employees/5/EditSupervisor
        // Attention - Attribute routing for nicer resource URLs
        [Route("employees/{id}/editsupervisor")]
        public ActionResult EditSupervisor(int? id)
        {
            // Attempt to fetch the matching object
            var o = m.EmployeeGetByIdWIthOrgInfo(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create a form, based on the fetched matching object
                var form = AutoMapper.Mapper.Map<EmployeeEditSupervisorForm>(o);

                // Fetch the employees into a temporary collection
                // Logically, an employee cannot select "self" as the supervisor
                // Therefore, we will remove the employee-being-edited from the collection
                // We must use a concrete collection type, to be able to use the Remove() method
                List<EmployeeBase> employees = m.EmployeeGetAll().ToList();
                var thisEmployee = employees.SingleOrDefault(e => e.EmployeeId == o.EmployeeId);
                employees.Remove(thisEmployee);

                // For the select list, configure the "selected" item
                // The easiest way is to read the value of ReportsTo
                // It is possible that its value is null, so use HasValue
                var selected = -1;
                if (o.ReportsTo.HasValue)
                {
                    selected = o.ReportsTo.GetValueOrDefault();
                    // Configure the "current supervisor" property, to display on the view
                    form.CurrentSupervisor = string.Format("{0}, {1}", o.Employee2.LastName, o.Employee2.FirstName);
                }

                // Create the new SelectList
                // Use C# named parameters to eliminate ambiguity
                form.EmployeeList = new SelectList
                    (items: employees, 
                    dataValueField: "EmployeeId", 
                    dataTextField: "FullName", 
                    selectedValue: selected);

                return View(form);
            }
        }

        // POST: Employees/5/EditSupervisor
        [Route("employees/{id}/editsupervisor")]
        [HttpPost]
        public ActionResult EditSupervisor(int? id, EmployeeEditSupervisor newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("editsupervisor", new { id = newItem.EmployeeId });
            }

            if (id.GetValueOrDefault() != newItem.EmployeeId)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            // Attempt to do the update
            var editedItem = m.EmployeeEditSupervisor(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("editsupervisor", new { id = newItem.EmployeeId });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("details", new { id = newItem.EmployeeId });
            }
        }


        // ############################################################
        // Edit the direct reports

        // GET: Employees/5/EditDirectReports
        [Route("employees/{id}/editdirectreports")]
        public ActionResult EditDirectReports(int? id)
        {
            // Attempt to fetch the matching object
            var o = m.EmployeeGetByIdWIthOrgInfo(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create a form, based on the fetched matching object
                var form = AutoMapper.Mapper.Map<EmployeeEditDirectReportsForm>(o);

                // Fetch the employees into a temporary collection
                // Logically, an employee cannot select "self" as a direct report
                // Therefore, we will remove the employee-being-edited from the collection
                // We must use a concrete collection type, to be able to use the Remove() method
                List<EmployeeBase> employees = m.EmployeeGetAll().ToList();
                var thisEmployee = employees.SingleOrDefault(e => e.EmployeeId == o.EmployeeId);
                employees.Remove(thisEmployee);

                // For the multi select list, configure the "selected" items
                // Notice the use of the Select() method, 
                // which allows us to select/return/use only some properties from the source
                var selectedValues = o.Employee1.Select(e => e.EmployeeId);

                // Send the current direct reports, to display them on the form
                form.DirectReports = o.Employee1.OrderBy(e => e.LastName).ThenBy(e => e.FirstName);

                // For clarity, use the named parameter feature of C#
                form.EmployeeList = new MultiSelectList
                    (items: employees,
                    dataValueField: "EmployeeId",
                    dataTextField: "FullName",
                    selectedValues: selectedValues);

                return View(form);
            }
        }

        // GET: Employees/5/EditDirectReports
        [Route("employees/{id}/editdirectreports")]
        [HttpPost]
        public ActionResult EditDirectReports(int? id, EmployeeEditDirectReports newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("editdirectreports", new { id = newItem.EmployeeId });
            }

            if (id.GetValueOrDefault() != newItem.EmployeeId)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            // Attempt to do the update
            var editedItem = m.EmployeeEditDirectReports(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("EditDirectReports", new { id = newItem.EmployeeId });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("Details", new { id = newItem.EmployeeId });
            }
        }

    }
}
