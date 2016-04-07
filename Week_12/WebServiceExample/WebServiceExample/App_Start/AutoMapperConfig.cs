using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace WebServiceExample
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            // AutoMapper create map statements

            Mapper.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

            // Add more below...

            Mapper.CreateMap<Models.Artist, Controllers.ArtistBase>();
            Mapper.CreateMap<Models.Artist, Controllers.ArtistWithAlbums>();

            Mapper.CreateMap<Models.Album, Controllers.AlbumBase>();

            Mapper.CreateMap<Models.Employee, Controllers.EmployeeBase>();

            // Change property names when mapping
            Mapper.CreateMap<Models.Employee, Controllers.EmployeeWithDetails>()
                .ForMember(dest => dest.ReportsTo, opt => opt.MapFrom(src => src.Employee2))
                .ForMember(dest => dest.DirectReports, opt => opt.MapFrom(src => src.Employee1));




#pragma warning restore CS0618
        }
    }
}