using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment6.Controllers
{
    public class PlaylistsController : Controller
    {
        // Playlist controller methods
        // Get all, get one, edit existing (pair of methods)

        // Reference to the data manager
        private Manager m = new Manager();

        // GET: Playlists
        public ActionResult Index()
        {
            // Study the Index view

            return View(m.PlaylistGetAll());
        }

        // GET: Playlists/Details/5
        public ActionResult Details(int? id)
        {
            // Study the Details view

            // Attempt to get the matching object
            var o = m.PlaylistGetByIdWithDetails(id.GetValueOrDefault());

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

        // GET: Playlists/Edit/5
        public ActionResult Edit(int? id)
        {
            // Study the Edit view

            // Attempt to fetch the matching object
            var o = m.PlaylistGetByIdWithDetails(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create a form
                var form = new PlaylistEditTracksForm();

                // Configure its properties
                // Alternatively, could create a mapper etc.
                form.PlaylistId = o.PlaylistId;
                form.Name = o.Name;
                form.TracksNowOnPlaylist = o.Tracks.OrderBy(t => t.Name);

                // Attention - 2 - Modified form-building strategy in the controller...
                // Need some data to enable a customized view
                // Look in the view...
                // Build a checkbox group, of genres, with the onchange event handled
                // Build a track list, with "display: none" to initially hide the rows
                // Turn them on as each genre is checked/selected

                // Call the method to get all tracks, with genre name
                form.TrackList = m.TrackWithGenreGetAll();

                // Now, go through the tracks-now-on-the-playlist,
                // and set the "selected" property
                foreach (var item in form.TracksNowOnPlaylist)
                {
                    form.TrackList.SingleOrDefault(t => t.TrackId == item.TrackId).Selected = true;
                }

                // Get the genre list, to display in the UI
                form.GenreList = m.GenreGetAll();

                return View(form);
            }
        }

        // POST: Playlists/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PlaylistEditTracks newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.PlaylistId });
            }

            if (id.GetValueOrDefault() != newItem.PlaylistId)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            // Attempt to do the upate
            var editedItem = m.PlaylistEditTracks(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.PlaylistId });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("details", new { id = newItem.PlaylistId });
            }
        }
    }
}
