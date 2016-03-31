using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AjaxWithWebService.Controllers
{
    // Attention - 2 - View model classes for Artist, Album, Track

    public class ArtistBase
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
    }

    public class AlbumBase
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        // Composed property name, auto flattened by AutoMapper
        public string ArtistName { get; set; }
    }

    public class TrackBase
    {
        public int TrackId { get; set; }
        public string Name { get; set; }
        
        // Composed property names, next four...
        public string AlbumTitle { get; set; }
        public string AlbumArtistName { get; set; }
        public string MediaTypeName { get; set; }
        public string GenreName { get; set; }

        // "Composer" may be null, so guard against that
        // A simple way is to create another property
        public string Composer { get; set; }
        public string ComposerName
        {
            get
            {
                var composer = string.IsNullOrEmpty(Composer) ? "" : Composer;
                return composer;
            }
        }

        // "Milliseconds" is not useful for a display/view
        // So, we'll convert it into a minutes-based number
        public int Milliseconds { get; set; }
        public string TrackLength
        {
            get
            {
                return (Math.Round((((double)Milliseconds / 1000) / 60), 2)).ToString();
            }
        }

        public decimal UnitPrice { get; set; }
    }
}
