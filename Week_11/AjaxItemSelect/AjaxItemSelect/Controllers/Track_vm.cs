using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AjaxItemSelect.Controllers
{
    // Attention - 02 - View model classes for Artist, Album, Track

    public class ArtistForm
    {
        public SelectList ArtistList { get; set; }
    }

    public class ArtistBase
    {
        public int ArtistId { get; set; }

        [StringLength(120)]
        public string Name { get; set; }
    }

    public class AlbumForm
    {
        public SelectList AlbumList { get; set; }
    }

    public class AlbumBase
    {
        public int AlbumId { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }
    }

    public class TrackForm
    {
        public SelectList TrackList { get; set; }
    }

    public class TrackBase
    {
        public int TrackId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public int? AlbumId { get; set; }

        public int MediaTypeId { get; set; }

        public int? GenreId { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        public int Milliseconds { get; set; }

        public int? Bytes { get; set; }

        public decimal UnitPrice { get; set; }
    }

    // Attention - 03 - Packages the user-selected data from the three HTML item-selection elements
    public class UserSelectedData
    {
        public UserSelectedData()
        {
            TrackIds = new List<int>();
        }

        public int ArtistId { get; set; }
        public int AlbumId { get; set; }
        public IEnumerable<int> TrackIds { get; set; }
    }

    // Attention - 04 - Text names for the user-selected data, delivered to the view
    public class SelectedDataText
    {
        public SelectedDataText()
        {
            TrackNames = new List<string>();
        }

        [Display(Name = "Artist name")]
        public string ArtistName { get; set; }

        [Display(Name = "Album name")]
        public string AlbumTitle { get; set; }

        [Display(Name = "Track name(s)")]
        public List<string> TrackNames { get; set; }
    }

}
