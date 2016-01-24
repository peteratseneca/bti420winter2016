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

            Mapper.CreateMap<Models.Customer, Controllers.CustomerBase>();
            Mapper.CreateMap<Controllers.CustomerBase, Controllers.CustomerEditContactInfoForm>();
            
            //hmmm...
            //Mapper.CreateMap<Controllers.CustomerEditContactInfo, Controllers.CustomerEditContactInfoForm>();

            Mapper.CreateMap<Controllers.CustomerAdd, Models.Customer>();
        }
    }
}