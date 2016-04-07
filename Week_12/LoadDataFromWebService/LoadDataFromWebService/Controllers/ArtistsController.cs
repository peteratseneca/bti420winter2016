using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LoadDataFromWebService.Controllers
{
    // Attention - 6 - Artist controller, which is very similar to any other web app controller

    public class ArtistsController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();
        
        // Attention - 7 - Notice the calling pattern to async methods...
        // We convert THESE methods to async too, and add the "await" prefix to the method call

        // GET: Artists
        public async Task<ActionResult> Index()
        {
            // Attempt to fetch the collection
            var c = await m.ArtistGetAll();

            if (c == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(c);
            }
        }

        // GET: Artists/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            // Attempt to fetch the object
            var o = await m.ArtistGetByIdWithAlbums(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
        }

        // Attention - 8 - Also... study the "ArtistsHTML5Page.html" in the project root...
        // It uses JavaScript to call out to the web service
        // Attention - 9 - Also... study the "FetchFromBrowser.js" JavaScript code

    }
}
