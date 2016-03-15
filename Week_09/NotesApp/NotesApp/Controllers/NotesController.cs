using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesApp.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // Attention - 1 - Make sure that you study the views for each of these methods
        // They will show how to display user information in the UI

        // GET: Notes
        public ActionResult Index()
        {
            return View(m.NoteGetAll());
        }

        // GET: Notes/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.NoteGetById(id.GetValueOrDefault());

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

        // GET: Notes/Create
        public ActionResult Create()
        {
            // For this code example, we don't need to create and send a form
            return View();
        }

        // POST: Notes/Create
        [HttpPost]
        public ActionResult Create(NoteAdd newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.NoteAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.Id });
            }
        }

        // GET: Notes/Edit/5
        public ActionResult Edit(int? id)
        {
            // Attempt to fetch the matching object
            var o = m.NoteGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create and configure an "edit form"
                var form = AutoMapper.Mapper.Map<NoteEditForm>(o);

                return View(form);
            }
        }
        
        // POST: Notes/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, NoteEdit newItem)
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
            var editedItem = m.NoteEdit(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.Id });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("details", new { id = newItem.Id});
            }
        }

        // GET: Notes/Delete/5
        public ActionResult Delete(int? id)
        {
            var itemToDelete = m.NoteGetById(id.GetValueOrDefault());

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

        // POST: Notes/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var result = m.NoteDelete(id.GetValueOrDefault());

            // "result" will be true or false
            // We probably won't do much with the result, because 
            // we don't want to leak info about the delete attempt

            // In the end, we should just redirect to the list view
            return RedirectToAction("index");
        }

    }
}
