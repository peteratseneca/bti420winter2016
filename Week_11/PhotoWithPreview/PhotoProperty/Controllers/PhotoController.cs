using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoProperty.Controllers
{
    // Attention - 7 - Special-purpose media item delivery controller

    public class PhotoController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: Photo
        public ActionResult Index()
        {
            return View("index", "home");
        }

        // GET: Photo/5
        // Attention - 8 - Uses attribute routing
        [Route("photo/{id}")]
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.VehiclePhotoGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Attention - 9 - Return a file content result
                // Set the Content-Type header, and return the photo bytes
                return File(o.Photo, o.PhotoContentType);
            }
        }
    }
}
