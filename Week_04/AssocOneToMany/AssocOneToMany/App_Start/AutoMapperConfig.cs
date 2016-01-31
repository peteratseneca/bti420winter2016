using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace AssocOneToMany
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            Mapper.CreateMap<Models.Team, Controllers.TeamBase>();
            Mapper.CreateMap<Models.Team, Controllers.TeamWithPlayers>();

            Mapper.CreateMap<Models.Player, Controllers.PlayerBase>();
            Mapper.CreateMap<Models.Player, Controllers.PlayerWithTeamInfo>();
            Mapper.CreateMap<Models.Player, Controllers.PlayerWithTeamName>();
        }
    }
}