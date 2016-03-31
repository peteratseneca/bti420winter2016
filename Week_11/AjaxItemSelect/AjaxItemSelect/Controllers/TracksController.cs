using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AjaxItemSelect.Controllers
{
    public class TracksController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();
        
        // GET: Tracks
        public ActionResult Index()
        {
            // Fetch the collection
            var c = m.ArtistGetAll();

            // Create a form
            var form = new ArtistForm();
            form.ArtistList = new SelectList(c, dataValueField: "ArtistId", dataTextField: "Name");

            return View(form);

            // Attention - 05 - Notice several things in the list-of-artists view...
            // The dropdown list has an "onchange" attribute that calls a JavaScript method
            // The view includes two <div> elements that will be filled in later
            // The "Send" (submit) button is disabled

            // Attention - 06 - Study the artistSelected() method in the JavaScript core.js source code file
        }

        // GET: Tracks/Albums/5
        // Attention - 07 - This get-albums-for-artist method is called from JavaScript+Ajax
        public ActionResult Albums(int? id)
        {
            // Fetch the collection
            var c = m.AlbumGetAllForArtist(id.GetValueOrDefault());

            // If null, create an empty collection
            if (c == null) { c = new List<AlbumBase>(); }

            // Create a form
            var form = new AlbumForm();
            form.AlbumList = new SelectList(c, dataValueField: "AlbumId", dataTextField: "Title");

            return PartialView("_AlbumList", form);

            // Attention - 08 - Notice several things in the list-of-albums view...
            // The dropdown list has an "onchange" attribute that calls a JavaScript method

            // Attention - 09 - Study the albumSelected() method in the JavaScript core.js source code file
        }

        // GET: Tracks/Tracks/5
        // Attention - 10 - This get-tracks-for-album method is called from JavaScript+Ajax
        public ActionResult Tracks(int? id)
        {
            // Fetch the collection
            var c = m.TrackGetAllForAlbum(id.GetValueOrDefault());

            // If null, create an empty collection
            if (c == null) { c = new List<TrackBase>(); }

            // Create a form
            var form = new TrackForm();
            form.TrackList = new SelectList(c, dataValueField: "TrackId", dataTextField: "Name");

            return PartialView("_TrackList", form);

            // Attention - 11 - Look at the list-of-tracks view
        }

        // POST: Tracks/DoSomething
        // Attention - 12 - This handles the user-selected data from the item-selection elements
        [HttpPost]
        public ActionResult DoSomething(UserSelectedData newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("index");
            }
            else
            {
                // Do something
                var o = m.DoSomething(newItem);

                return View(o);
            }
        }
    }
}
