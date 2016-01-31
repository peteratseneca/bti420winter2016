using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocOneToMany.Controllers
{
    public class TeamsController : Controller
    {
        private Manager m = new Manager();

        // GET: Teams
        public ActionResult Index()
        {
            return View(m.TeamGetAll());
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.TeamGetById(id.GetValueOrDefault());

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

        public ActionResult DetailsWithPlayers(int? id)
        {
            // Attempt to get the matching object
            var o = m.TeamGetByIdWithPlayers(id.GetValueOrDefault());

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

        /*
        // GET: Teams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Teams/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Teams/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        */
    }
}
