using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace PhotoProperty.Models
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

    // ############################################################
    // Vehicle

    public class Vehicle
    {
        public Vehicle()
        {
            ModelYear = DateTime.Now.Year;
            MSRP = 20000;
        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Model { get; set; }

        [Required, StringLength(100)]
        public string Trim { get; set; }

        public int ModelYear { get; set; }
        public int MSRP { get; set; }

        [Required, StringLength(100)]
        public string Manufacturer { get; set; }

        // Attention - 1 - Design model class needs two properties for the media item
        // Here, both can be null, but the view model classes will require them
        [StringLength(200)]
        public string PhotoContentType { get; set; }
        public byte[] Photo { get; set; }
    }



    // ############################################################
    // RoleClaim

    public class RoleClaim
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }

}
