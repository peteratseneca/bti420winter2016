using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace DataAnnotations.Controllers
{
    // A class that describes a vehicle (car, truck)

    // Coding style recommendation:
    // Before (above) a property that has data annotations, add a blank line
    // This makes the code more readable

    public class VehicleAdd
    {
        public VehicleAdd()
        {
            DateAvailable = DateTime.Now;
            ModelYear = DateAvailable.Year;
        }

        [Required]
        [StringLength(100)]
        [Display(Name = "Manufacturer name")]
        public string Manufacturer { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Vehicle model name")]
        public string ModelName { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Trim level")]
        public string TrimLevel { get; set; }

        [Range(1885,2285)]
        [Display(Name ="Model year")]
        public int ModelYear { get; set; }

        [Required]
        [Display(Name = "Public availability date")]
        [DataType(DataType.Date)]
        public DateTime DateAvailable { get; set; }

        [Range(1,50)]
        [Display(Name = "Number of seats")]
        public int Seats { get; set; }

        [Range(1.0, 50.0)]
        [Display(Name = "Fuel consumption, L/100km, city")]
        public double ConsumptionCity { get; set; }

        [Range(1.0, 50.0)]
        [Display(Name = "Fuel consumption, L/100km, highway")]
        public double ConsumptionHighway { get; set; }
    }

    public class VehicleBase : VehicleAdd
    {
        public VehicleBase()
        {

        }

        public int Id { get; set; }
    }

    public class VehicleAddPlain
    {
        public VehicleAddPlain()
        {
            DateAvailable = DateTime.Now;
            ModelYear = DateAvailable.Year;
        }

        public string Manufacturer { get; set; }
        public string ModelName { get; set; }
        public string TrimLevel { get; set; }
        public int ModelYear { get; set; }
        public DateTime DateAvailable { get; set; }
        public int Seats { get; set; }
        public double ConsumptionCity { get; set; }
        public double ConsumptionHighway { get; set; }

    }
}
