using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssocAddEdit.Controllers
{
    public class ManufacturerBase
    {
        public ManufacturerBase()
        {
            YearStarted = 1950;
        }

        public int Id { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Manufacturer Name")]
        public string Name { get; set; }

        [Range(1850, Int16.MaxValue)]
        [Display(Name = "Year Started")]
        public int YearStarted { get; set; }
    }

    public class ManufacturerWithDetail : ManufacturerBase
    {
        public ManufacturerWithDetail()
        {
            Vehicles = new List<VehicleBase>();
        }

        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        public ICollection<VehicleBase> Vehicles { get; set; }
    }
}
