using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoEntity.Controllers
{
    public class PropertiesController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: Properties
        public ActionResult Index()
        {
            return View(m.PropertyGetAll());
        }

        // GET: Properties/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.PropertyGetById(id.GetValueOrDefault());

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

        // GET: Properties/DetailsWithPhotoInfo/5
        public ActionResult DetailsWithPhotoInfo(int? id)
        {
            // Attempt to get the matching object
            var o = m.PropertyGetByIdWithPhotoInfo(id.GetValueOrDefault());

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

        // GET: Properties/Create
        public ActionResult Create()
        {
            var form = new PropertyAddForm();

            return View(form);
        }

        // POST: Properties/Create
        [HttpPost]
        public ActionResult Create(PropertyAdd newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.PropertyAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", new { id = addedItem.Id });
            }
        }

        // GET: Properties/Edit/5
        public ActionResult Edit(int? id)
        {
            // Attempt to get the matching object
            var o = m.PropertyGetById(id.GetValueOrDefault());

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

        // Attention - 10 - Controller method pair to add photo for an *existing* for-sale property
        // Method uses attribute routing
        // GET: Properties/5/AddPhoto
        [Route("properties/{id}/addphoto")]
        public ActionResult AddPhoto(int? id)
        {
            // Attempt to get the matching object
            var o = m.PropertyGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create a form
                var form = new PhotoAddForm();
                // Configure its property values
                form.PropertyId = o.Id;
                form.PropertyInfo = $"{o.Address}, {o.City}<br>Asking price ${o.Price}";

                // Pass the object to the view
                return View(form);
            }
        }

        // POST: Properties/5/AddPhoto
        [HttpPost]
        [Route("properties/{id}/addphoto")]
        public ActionResult AddPhoto(int? id, PhotoAdd newItem)
        {
            // Validate the input
            // Two conditions must be checked
            if (!ModelState.IsValid && id.GetValueOrDefault() == newItem.PropertyId)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.PropertyPhotoAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", new { id = addedItem.Id });
            }
        }

        // POST: Properties/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PropertyBase newItem)
        {
            // Validate the input
            if (!ModelState.IsValid && id.GetValueOrDefault() == newItem.Id)
            {
                return View(newItem);
            }

            // Process the input
            var editedItem = m.PropertyEdit(newItem);

            if (editedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", new { id = editedItem.Id });
            }
        }

        // GET: Properties/Delete/5
        public ActionResult Delete(int? id)
        {
            var itemToDelete = m.PropertyGetById(id.GetValueOrDefault());

            if (itemToDelete == null)
            {
                // Don't leak info about the delete attempt
                // Simply redirect
                return Redirect("~/properties/index");
            }
            else
            {
                return View(itemToDelete);
            }
        }

        // POST: Properties/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var wasDeleted = m.PropertyDelete(id.GetValueOrDefault());

            // "wasDeleted" will be true or false
            // We probably won't do much with the result, because 
            // we don't want to leak info about the delete attempt

            // In the end, we should just redirect to the list view
            return Redirect("~/properties/index");
        }
    }
}
