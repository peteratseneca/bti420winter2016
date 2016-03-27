using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace PhotoEntity
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

            Mapper.CreateMap<Models.Property, Controllers.PropertyBase>();
            Mapper.CreateMap<Models.Property, Controllers.PropertyWithPhotoStringIds>();
            Mapper.CreateMap<Controllers.PropertyAdd, Models.Property>();
            Mapper.CreateMap<Controllers.PropertyBase, Controllers.PropertyAddForm>();

            Mapper.CreateMap<Models.Photo, Controllers.PhotoBase>();
            Mapper.CreateMap<Models.Photo, Controllers.PhotoContent>();



#pragma warning restore CS0618
        }
    }
}