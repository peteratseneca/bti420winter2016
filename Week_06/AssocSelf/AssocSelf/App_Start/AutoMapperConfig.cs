using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace AssocSelf
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            Mapper.CreateMap<Models.Employee, Controllers.EmployeeBase>();
            Mapper.CreateMap<Models.Employee, Controllers.EmployeeWithOrgInfo>();
            Mapper.CreateMap<Controllers.EmployeeBase, Controllers.EmployeeEditSupervisorForm>();
            Mapper.CreateMap<Controllers.EmployeeBase, Controllers.EmployeeEditDirectReportsForm>();
        }
    }
}