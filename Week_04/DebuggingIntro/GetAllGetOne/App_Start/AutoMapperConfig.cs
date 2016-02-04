using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace GetAllGetOne
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // Attention - Error - mapping error, missing AutoMapper map

            //Mapper.CreateMap<Models.Customer, Controllers.CustomerBase>();

            Mapper.CreateMap<Controllers.CustomerBase, Controllers.CustomerEditContactInfoForm>();
            
            Mapper.CreateMap<Controllers.CustomerAdd, Models.Customer>();
        }
    }
}