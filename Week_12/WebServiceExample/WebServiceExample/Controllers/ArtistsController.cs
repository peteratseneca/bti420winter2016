using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebServiceExample.Controllers
{
    // Attention - 4 - Artist *web service* controller, which is very similar to the familiar web app controller

    // Attention - 6 - You can use a browser to test the get-all and get-one resource URLs
    // Depending upon your browser settings, the response data may be JSON or XML

    // Attention - 7 - Call this web service from JavaScript in a browser, or from another app
    // Had to add a custom header in the Web.config, look in the httpProtocol element

    public class ArtistsController : ApiController
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: api/Artists
        public IHttpActionResult Get()
        {
            return Ok(m.ArtistGetAll());
        }

        // GET: api/Artists/WithAlbums
        [Route("api/artists/withalbums")]
        public IHttpActionResult GetWithAlbums()
        {
            return Ok(m.ArtistGetAllWithAlbums());
        }

        // GET: api/Artists/5
        public IHttpActionResult Get(int? id)
        {
            // Fetch the object
            var o = m.ArtistGetById(id.GetValueOrDefault());

            // Continue?
            if (o == null) { return NotFound(); }

            return Ok(o);
        }

        // GET: api/Artists/5/WithAlbums
        [Route("api/artists/{id}/withalbums")]
        public IHttpActionResult GetWithAlbums(int? id)
        {
            // Fetch the object
            var o = m.ArtistGetByIdWithAlbums(id.GetValueOrDefault());

            // Continue?
            if (o == null) { return NotFound(); }

            return Ok(o);
        }
    }
}
