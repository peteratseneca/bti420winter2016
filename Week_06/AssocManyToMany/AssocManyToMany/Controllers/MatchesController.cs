using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocManyToMany.Controllers
{
    public class MatchesController : Controller
    {
        // Reference to the data manager
        private Manager m = new Manager();

        // GET: Matches
        public ActionResult Index()
        {
            return View();
        }

        // ############################################################
        // Employee

        // GET: Matches/ByEmployee
        public ActionResult ByEmployee()
        {
            return View(m.EmployeeGetAll());
        }

        // GET: Matches/ByEmployeeMore
        public ActionResult ByEmployeeMore()
        {
            return View(m.EmployeeGetAllWithJobDuties());
        }

        // GET: Matches/ByEmployeeWithDetails/5
        public ActionResult ByEmployeeWithJobDuties(int? id)
        {
            // Attempt to get the matching object
            var o = m.EmployeeGetByIdWithDetail(id.GetValueOrDefault());

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

        // Attention - Edit an employee's job duties

        // GET: Matches/EmployeeAndJobDuties/5
        // Prepare and display the HTML Form to enable editing an employee's job duties
        public ActionResult EmployeeAndJobDuties(int? id)
        {
            // Attempt to fetch the matching object
            var o = m.EmployeeGetByIdWithDetail(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create a form, based on the fetched matching object
                var form = AutoMapper.Mapper.Map<EmployeeEditJobDutiesForm>(o);

                // For the multi select list, configure the "selected" items
                // Notice the use of the Select() method, 
                // which allows us to select/return/use only some properties from the source
                var selectedValues = o.JobDuties.Select(jd => jd.Id);

                // For clarity, use the named parameter feature of C#
                form.JobDutyList = new MultiSelectList
                    (items: m.JobDutyGetAll(),
                    dataValueField: "Id",
                    dataTextField: "FullName",
                    selectedValues: selectedValues);

                return View(form);
            }
        }

        // POST: Matches/EmployeeAndJobDuties/5
        // Handle the data submitted by the browser user
        [HttpPost]
        public ActionResult EmployeeAndJobDuties(int? id, EmployeeEditJobDuties newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.Id });
            }

            if (id.GetValueOrDefault() != newItem.Id)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            // Attempt to do the update
            var editedItem = m.EmployeeEditJobDuties(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("ByEmployee", new { id = newItem.Id });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("ByEmployeeWithJobDuties", new { id = newItem.Id });
            }
        }

        // ############################################################
        // JobDuty

        // GET: Matches/ByJobDuty
        public ActionResult ByJobDuty()
        {
            return View(m.JobDutyGetAll());
        }

        // GET: Matches/ByJobDutyMore
        public ActionResult ByJobDutyMore()
        {
            return View(m.JobDutyGetAllWithEmployees());
        }

        // GET: Matches/ByJobDutyWithDetails/5
        public ActionResult ByJobDutyWithEmployees(int? id)
        {
            // Attempt to get the matching object
            var o = m.JobDutyGetByIdWithDetail(id.GetValueOrDefault());

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

        // Attention - Edit a job duty's list of employees

        // GET: Matches/JobDutyAndEmployees/5
        // Prepare and display the HTML Form to enable editing an employee's job duties
        public ActionResult JobDutyAndEmployees(int? id)
        {
            // Attempt to fetch the matching object
            var o = m.JobDutyGetByIdWithDetail(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create a form, based on the fetched matching object
                var form = AutoMapper.Mapper.Map<JobDutyEditEmployeesForm>(o);

                // For the multi select list, configure the "selected" items
                // Notice the use of the Select() method, 
                // which allows us to select/return/use only some properties from the source
                var selectedValues = o.Employees.Select(e => e.Id);

                // For clarity, use the named parameter feature of C#
                form.EmployeeList = new MultiSelectList
                    (items: m.EmployeeGetAll(),
                    dataValueField: "Id",
                    dataTextField: "Name",
                    selectedValues: selectedValues);

                return View(form);
            }
        }

        // POST: Matches/JobDutyAndEmployees/5
        // Handle the data submitted by the browser user
        [HttpPost]
        public ActionResult JobDutyAndEmployees(int? id, JobDutyEditEmployees newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("JobDutyAndEmployees", new { id = newItem.Id });
            }

            if (id.GetValueOrDefault() != newItem.Id)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("ByJobDuty");
            }

            // Attempt to do the update
            var editedItem = m.JobDutyEditEmployees(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("ByJobDuty", new { id = newItem.Id });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("ByJobDutyWithEmployees", new { id = newItem.Id });
            }
        }

    }
}
