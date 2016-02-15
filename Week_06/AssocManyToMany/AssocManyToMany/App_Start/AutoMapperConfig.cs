using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace AssocManyToMany
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            Mapper.CreateMap<Models.Employee, Controllers.EmployeeBase>();
            Mapper.CreateMap<Models.Employee, Controllers.EmployeeWithJobDuties>();
            Mapper.CreateMap<Controllers.EmployeeBase, Controllers.EmployeeEditJobDutiesForm>();

            Mapper.CreateMap<Models.JobDuty, Controllers.JobDutyBase>();
            Mapper.CreateMap<Models.JobDuty, Controllers.JobDutyWithEmployees>();
            Mapper.CreateMap<Controllers.JobDutyBase, Controllers.JobDutyEditEmployeesForm>();
        }
    }
}