using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace AssocOneToMany.Models
{
    public class Team
    {
        public Team()
        {
            Players = new List<Player>();
        }

        public int Id { get; set; }
        public string CodeName { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int YearFounded { get; set; }
        public string Conference { get; set; }
        public string Division { get; set; }
        public string Stadium { get; set; }

        public ICollection<Player> Players { get; set; }
    }

    public class Player
    {
        public Player()
        {
            BirthDate = DateTime.Now.AddYears(-25);
        }

        public int Id { get; set; }
        public int UniformNumber { get; set; }
        public string PlayerName { get; set; }
        public string Position { get; set; }
        public string Height { get; set; }
        public int Weight { get; set; }
        public DateTime BirthDate { get; set; }
        public int YearsExperience { get; set; }
        public string College { get; set; }

        [Required]
        public Team Team { get; set; }
    }
}
