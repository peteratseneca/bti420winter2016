using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace ManageAccounts
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

            Mapper.CreateMap<Models.ApplicationUser, Controllers.ApplicationUserBase>();
            Mapper.CreateMap<Controllers.UserAccount, Controllers.ApplicationUserDetail>();
            Mapper.CreateMap<Controllers.ApplicationUserDetail, Controllers.ApplicationUserEditForm>();
            Mapper.CreateMap<Controllers.ApplicationUserEdit, Controllers.ApplicationUserDetail>();


#pragma warning restore CS0618
        }
    }
}