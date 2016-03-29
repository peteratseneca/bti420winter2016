using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment6.Controllers
{
    // Playlist view models
    // Base (or Base + Add)
    // EditForm
    // Edit

    public class PlaylistAdd
    {
        // Should be required
        [Required]
        [StringLength(120)]
        [Display(Name = "Playlist name")]
        public string Name { get; set; }
    }

    public class PlaylistBase : PlaylistAdd
    {
        [Key]
        public int PlaylistId { get; set; }

        // Composed property
        [Display(Name = "Number of tracks on this playlist")]
        public int TracksCount { get; set; }
    }

    public class PlaylistWithDetails : PlaylistBase
    {
        public PlaylistWithDetails()
        {
            Tracks = new List<TrackBase>();
        }

        [Display(Name = "Tracks on this playlist")]
        public IEnumerable<TrackBase> Tracks { get; set; }
    }

    public class PlaylistEditTracksForm
    {
        public PlaylistEditTracksForm()
        {
            TracksNowOnPlaylist = new List<TrackBase>();
        }

        [Key]
        public int PlaylistId { get; set; }

        [Display(Name = "Playlist name")]
        public string Name { get; set; }

        [Display(Name = "All tracks")]
        public IEnumerable<TrackWithGenre> TrackList { get; set; }

        public IEnumerable<GenreBase> GenreList { get; set; }

        // Edited, replace with generic collection
        // For select list
        //[Display(Name = "All tracks")]
        //public MultiSelectList TrackList { get; set; }

        // For the extra list of tracks (before edits)
        [Display(Name = "Tracks now on this playlist")]
        public IEnumerable<TrackBase> TracksNowOnPlaylist { get; set; }

    }

    public class PlaylistEditTracks
    {
        public PlaylistEditTracks()
        {
            TrackIds = new List<int>();
        }

        [Key]
        public int PlaylistId { get; set; }

        public IEnumerable<int> TrackIds { get; set; }
    }
}
