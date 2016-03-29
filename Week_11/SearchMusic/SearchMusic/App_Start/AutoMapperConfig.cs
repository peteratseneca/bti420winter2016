using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace SearchMusic
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

#pragma warning disable CS0618

            Mapper.CreateMap<Models.Track, Controllers.TrackBase>();




#pragma warning restore CS0618
        }
    }
}