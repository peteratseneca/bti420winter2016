using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SearchMusic.Controllers
{
    public class TrackBase
    {
        public int TrackId { get; set; }

        [Display(Name = "Track name")]
        public string Name { get; set; }

        [Display(Name = "Album name")]
        public string AlbumTitle { get; set; }

        [Display(Name = "Artist name")]
        public string AlbumArtistName { get; set; }

        [Display(Name = "Media type")]
        public string MediaTypeName { get; set; }

        [Display(Name = "Genre")]
        public string GenreName { get; set; }

        [Display(Name = "Composer name(s)")]
        public string Composer { get; set; }
    }

    public class TrackSearchForm
    {
        [Required, StringLength(100)]
        [Display(Name = "All or part of a track name")]
        public string SearchText { get; set; }
    }
}
