using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace LoadDataFromXLSX.Models
{
    // Add your design model classes below

    // Follow these rules or conventions:

    // To ease other coding tasks, the name of the 
    //   integer identifier property should be "Id"
    // Collection properties (including navigation properties) 
    //   must be of type ICollection<T>
    // Valid data annotations are pretty much limited to [Required] and [StringLength(n)]
    // Required to-one navigation properties must include the [Required] attribute
    // Do NOT configure scalar properties (e.g. int, double) with the [Required] attribute
    // Initialize DateTime and collection properties in a default constructor

    // Attention - 1 - Look in the App_Data folder to see the XLSX file(s)

    // Attention - 2 - Design model class is based on the XLSX file contents

    public class Player
    {
        public Player()
        {
            BirthDate = DateTime.Now.AddYears(-25);
        }

        public int Id { get; set; }

        [Required, StringLength(10)]
        public string Team { get; set; }

        public int UniformNumber { get; set; }

        [Required, StringLength(100)]
        public string PlayerName { get; set; }

        [Required, StringLength(10)]
        public string Position { get; set; }

        [Required, StringLength(10)]
        public string Status { get; set; }

        [Required, StringLength(10)]
        public string Height { get; set; }

        public int Weight { get; set; }
        public DateTime BirthDate { get; set; }
        public int YearsExperience { get; set; }

        [Required, StringLength(100)]
        public string College { get; set; }
    }

    public class RoleClaim
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }

}
