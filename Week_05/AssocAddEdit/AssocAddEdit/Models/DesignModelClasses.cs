using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssocAddEdit.Models
{
    public class Country
    {
        public Country()
        {
            Manufacturers = new List<Manufacturer>();
        }

        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        public ICollection<Manufacturer> Manufacturers { get; set; }
    }

    public class Manufacturer
    {
        public Manufacturer()
        {
            YearStarted = 1950;
            Vehicles = new List<Vehicle>();
        }

        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }
        public int YearStarted { get; set; }

        [Required]
        public Country Country { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }

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

        [Required]
        public Manufacturer Manufacturer { get; set; }
    }
}
