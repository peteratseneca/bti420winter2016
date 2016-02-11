using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace AssocAddEdit
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            Mapper.CreateMap<Models.Country, Controllers.CountryBase>();

            Mapper.CreateMap<Models.Manufacturer, Controllers.ManufacturerBase>();
            Mapper.CreateMap<Models.Manufacturer, Controllers.ManufacturerWithDetail>();

            Mapper.CreateMap<Models.Vehicle, Controllers.VehicleBase>();
            Mapper.CreateMap<Models.Vehicle, Controllers.VehicleWithDetail>();
            Mapper.CreateMap<Controllers.VehicleAdd, Models.Vehicle>();
            Mapper.CreateMap<Controllers.VehicleBase, Controllers.VehicleEditForm>();
            Mapper.CreateMap<Controllers.VehicleWithDetail, Controllers.VehicleEditForm>();

        }
    }
}