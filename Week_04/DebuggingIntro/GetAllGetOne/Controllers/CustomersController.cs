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
            // Attention - Error - object reference not set to an instance of an object
            // Just as it says... an object is null, and you're trying to use it
            // Set a breakpoint on line 24 above, then "step over" to get to the error
            // Inspect the variable values as you step through the lines of code

            // Create a new CustomerEditContactInfo object
            // Intentionally leave out some property values
            var newCust = new CustomerEditContactInfo
            {
                //Fax = "(416) 661-4034",
                //Email = "customer@example.com",
                CustomerId = 2,
                Phone = "(416) 491-5050"
            };

            // Now, attempt to use some values that are not set
            var email = newCust.Email;
            var emailLength = email.Length;

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

        // GET: Customers/Create
        public ActionResult Create()
        {
            // At your option, create and send an object to the view
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        public ActionResult Create(CustomerAdd newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.CustomerAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.CustomerId });
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            // Attempt to fetch the matching object
            var o = m.CustomerGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create and configure an "edit form"

                // Notice that o is a CustomerBase object
                // We must map it to a CustomerEditContactInfoForm object
                // Notice that we can use AutoMapper anywhere, 
                // and not just in the Manager class!
                var editForm = AutoMapper.Mapper.Map<CustomerEditContactInfoForm>(o);

                return View(editForm);
            }
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, CustomerEditContactInfo newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.CustomerId });
            }

            if (id.GetValueOrDefault() != newItem.CustomerId)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            // Attention - Error - cause a validation error when saving to the store
            // At this point in the method, newItem has passed the initial validation test
            // Set a breakpoint at line 139 below
            // So, let's invalidate some of its values (null or string too long),
            // and pass it to the manager, which will attempt to save it to the data store
            // Then, BOOM!, a DbEntity validation error will appear

            // Set a required value to null
            newItem.Email = null;

            // Set another to a too-long value
            newItem.Phone = "This string is too long. It exceeds the 24-character limit.";

            // Attempt to do the update
            var editedItem = m.CustomerEditContactInfo(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.CustomerId });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("details", new { id = newItem.CustomerId });
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            var itemToDelete = m.CustomerGetById(id.GetValueOrDefault());

            if (itemToDelete == null)
            {
                // Don't leak info about the delete attempt
                // Simply redirect
                return RedirectToAction("index");
            }
            else
            {
                return View(itemToDelete);
            }
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var result = m.CustomerDelete(id.GetValueOrDefault());

            // "result" will be true or false
            // We probably won't do much with the result, because 
            // we don't want to leak info about the delete attempt

            // In the end, we should just redirect to the list view
            return RedirectToAction("index");
        }
    }
}
