using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using SearchMusic.Models;

namespace SearchMusic.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        public Manager()
        {
            // If necessary, add constructor code here
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        public IEnumerable<TrackBase> TrackGetAll()
        {
            return Mapper.Map<IEnumerable<TrackBase>>(ds.Tracks.OrderBy(t => t.Name));
        }

        // Attention - 1 - Manager method to fetch tracks that match a search string
        public IEnumerable<TrackBase> TrackGetAllByText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            // Fetch the tracks that contain the search text
            var c = ds.Tracks
                .Include("Album.Artist")
                .Include("MediaType")
                .Include("Genre")
                .Where(t => t.Name.ToLower().Contains(text.Trim().ToLower()));

            return Mapper.Map<IEnumerable<TrackBase>>(c.OrderBy(t => t.Name));
        }

    }
}