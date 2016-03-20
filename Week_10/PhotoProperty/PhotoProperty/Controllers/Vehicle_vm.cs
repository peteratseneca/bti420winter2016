using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoProperty.Controllers
{
    public class VehicleAddForm
    {
        public VehicleAddForm()
        {
            ModelYear = DateTime.Now.Year;
            MSRP = 20000;
        }

        [Required, StringLength(100)]
        public string Model { get; set; }

        [Required, StringLength(100)]
        public string Trim { get; set; }

        [Range(1850, Int16.MaxValue)]
        [Display(Name = "Model Year")]
        public int ModelYear { get; set; }

        [Range(1, Int32.MaxValue)]
        [Display(Name = "Sell Price")]
        public int MSRP { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Manufacturer Name")]
        public string Manufacturer { get; set; }

        // Attention - 2 - In this "Form" class, the property type is string, and the data type is upload
        [Required]
        [Display(Name = "Vehicle Photo")]
        [DataType(DataType.Upload)]
        public string PhotoUpload { get; set; }
    }

    public class VehicleAdd
    {
        public VehicleAdd()
        {
            ModelYear = DateTime.Now.Year;
            MSRP = 20000;
        }

        [Required, StringLength(100)]
        public string Model { get; set; }

        [Required, StringLength(100)]
        public string Trim { get; set; }

        [Range(1850, Int16.MaxValue)]
        public int ModelYear { get; set; }

        [Range(1, Int32.MaxValue)]
        public int MSRP { get; set; }

        [Required, StringLength(100)]
        public string Manufacturer { get; set; }

        // Attention - 3 - In this "Add" class, notice the type is HttpPostedFileBase
        [Required]
        public HttpPostedFileBase PhotoUpload { get; set; }
    }

    public class VehicleBase
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Model { get; set; }

        [Required, StringLength(100)]
        public string Trim { get; set; }

        [Display(Name = "Model Year")]
        public int ModelYear { get; set; }

        [Display(Name = "Sell Price")]
        public int MSRP { get; set; }

        [Display(Name = "Manufacturer Name")]
        public string Manufacturer { get; set; }

        [Display(Name = "Vehicle Photo")]
        public string PhotoUrl
        {
            get
            {
                return $"/photo/{Id}";
            }
        }
    }

    public class VehiclePhoto
    {
        public int Id { get; set; }
        public string PhotoContentType { get; set; }
        public byte[] Photo { get; set; }
    }

}
