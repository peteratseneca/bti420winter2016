using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AjaxWithWebService.Controllers
{
    // Attention - 5 - This is the web service controller...
    // Notice that each method is accessed with a specific route
    // Notice also the method return type, IHttpActionResult, which is a bit different

    public class DeliverDataController : ApiController
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: api/DeliverData/Artists
        [Route("api/deliverdata/artists")]
        public IHttpActionResult GetArtists()
        {
            var c = m.ArtistGetAll();

            // Attention - 6 - The return type is the collection (or object) type...
            // The request-handling framework automatically marshalls 
            // the collection type into plain-text JSON 
            return Ok(c);
        }

        // GET: api/DeliverData/Albums
        [Route("api/deliverdata/albums")]
        public IHttpActionResult GetAlbums()
        {
            var c = m.AlbumGetAll();

            return Ok(c);
        }

        // GET: api/DeliverData/Tracks
        [Route("api/deliverdata/tracks")]
        public IHttpActionResult GetTracks()
        {
            var c = m.TrackGetAll();

            return Ok(c);
        }
    }
}
