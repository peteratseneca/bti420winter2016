using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace PhotoProperty
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

            // Attention - 4 - Notice all the create map statements

            Mapper.CreateMap<Models.Vehicle, Controllers.VehicleBase>();
            Mapper.CreateMap<Models.Vehicle, Controllers.VehiclePhoto>();
            Mapper.CreateMap<Controllers.VehicleAdd, Models.Vehicle>();




#pragma warning restore CS0618
        }
    }
}