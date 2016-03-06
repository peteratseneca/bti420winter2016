using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace NewDataModelSecuredOnAzure
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            // Attention - AutoMapper create map statements

            Mapper.CreateMap<Models.Country, Controllers.CountryBase>();

            Mapper.CreateMap<Models.Manufacturer, Controllers.ManufacturerBase>();
            Mapper.CreateMap<Models.Manufacturer, Controllers.ManufacturerWithDetail>();

            Mapper.CreateMap<Models.Vehicle, Controllers.VehicleBase>();
            Mapper.CreateMap<Models.Vehicle, Controllers.VehicleWithDetail>();
            Mapper.CreateMap<Controllers.VehicleAdd, Models.Vehicle>();
            Mapper.CreateMap<Controllers.VehicleBase, Controllers.VehicleEditForm>();
            Mapper.CreateMap<Controllers.VehicleWithDetail, Controllers.VehicleEditForm>();

#pragma warning restore CS0618
        }
    }
}