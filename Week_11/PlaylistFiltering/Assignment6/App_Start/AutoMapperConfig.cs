using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment6
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // AutoMapper maps
#pragma warning disable CS0618

            Mapper.CreateMap<Models.Playlist, Controllers.PlaylistBase>();
            Mapper.CreateMap<Models.Playlist, Controllers.PlaylistWithDetails>();

            Mapper.CreateMap<Models.Track, Controllers.TrackBase>();
            Mapper.CreateMap<Models.Track, Controllers.TrackWithGenre>();

            Mapper.CreateMap<Models.Genre, Controllers.GenreBase>();


#pragma warning restore CS0618
        }
    }
}