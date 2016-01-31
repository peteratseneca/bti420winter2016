using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace AssocOneToMany.Controllers
{
    public class TeamBase
    {
        public TeamBase()
        {

        }

        public int Id { get; set; }

        [Display(Name = "Team Code")]
        public string CodeName { get; set; }

        [Display(Name = "Team Code")]
        public string Name { get; set; }

        public string City { get; set; }

        [Display(Name = "Year Founded")]
        public int YearFounded { get; set; }

        public string Conference { get; set; }
        public string Division { get; set; }
        public string Stadium { get; set; }
    }

    public class TeamWithPlayers : TeamBase
    {
        public TeamWithPlayers()
        {
            Players = new List<PlayerBase>();
        }

        public IEnumerable<PlayerBase> Players { get; set; }
    }

}