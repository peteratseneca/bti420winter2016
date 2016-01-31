using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocOneToMany.Controllers
{
    public class PlayersController : Controller
    {
        private Manager m = new Manager();

        // GET: Players
        public ActionResult Index()
        {
            return View(m.PlayerGetAll());
        }

        public ActionResult WithTeamInfo()
        {
            return View(m.PlayerGetAllWithTeamInfo());
        }

        public ActionResult WithTeamName()
        {
            return View(m.PlayerGetAllWithTeamName());
        }

        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.PlayerGetById(id.GetValueOrDefault());

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
        // GET: Players/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
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

        // GET: Players/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Players/Edit/5
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

        // GET: Players/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Players/Delete/5
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
