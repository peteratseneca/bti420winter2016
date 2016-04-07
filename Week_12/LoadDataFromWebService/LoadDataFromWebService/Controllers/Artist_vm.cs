using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoadDataFromWebService.Controllers
{
    // Attention - 1 - Artist view model classes, plain, and with albums

    public class ArtistBase
    {
        [Key]
        public int ArtistId { get; set; }

        [Display(Name = "Artist name")]
        public string Name { get; set; }
    }

    public class ArtistWithAlbums : ArtistBase
    {
        public ArtistWithAlbums()
        {
            Albums = new List<AlbumBase>();
        }

        public IEnumerable<AlbumBase> Albums { get; set; }
    }

    public class AlbumBase
    {
        [Key]
        public int AlbumId { get; set; }
        public string Title { get; set; }
    }

}
