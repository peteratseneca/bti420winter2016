using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment6.Controllers
{
    // Not really needed for Assignment 6

    public class GenreBase
    {
        [Key]
        public int GenreId { get; set; }

        [StringLength(120)]
        public string Name { get; set; }
    }

    public class GenreWithDetails : GenreBase
    {
        public GenreWithDetails()
        {
            Tracks = new List<TrackBase>();
        }

        public IEnumerable<TrackBase> Tracks { get; set; }
    }
}
