using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchMusic.Controllers
{
    public class MusicController : Controller
    {
        // Reference to the data manager
        private Manager m = new Manager();

        // GET: Music
        public ActionResult Index()
        {
            return RedirectToAction("search");
        }

        // GET: Music/Search
        // Attention - 2 - Controller method to display the "search" form
        public ActionResult Search()
        {
            return View(new TrackSearchForm());
        }

        // GET: Music/Tracks/{searchText}
        // Attention - 3 - Controller method that listens for the Ajax call
        // Attention - 4 - Look at the JavaScript code in Scripts > App > core.js
        // Attention - 5 - Look in the view Search.cshtml for edited markup
        // Attention - 6 - Look in the partial view _TrackList.cshtml for rendering markup
        [Route("music/tracks/{searchText}")]
        public ActionResult Tracks(string searchText = "")
        {
            // Fetch matching tracks
            var c = m.TrackGetAllByText(searchText);

            if (c == null)
            {
                // Empty list
                return PartialView("_TrackList", new List<TrackBase>());
            }
            else
            {
                return PartialView("_TrackList", c);
            }
        }
    }
}