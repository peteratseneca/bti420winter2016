using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocAddEdit.Controllers
{
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
        [Display(Name = "Model Year")]
        public int ModelYear { get; set; }

        [Range(1, Int32.MaxValue)]
        [Display(Name = "Sell Price")]
        public int MSRP { get; set; }

        // Attention - Identifier for the associated item
        [Range(1, Int32.MaxValue)]
        public int ManufacturerId { get; set; }
    }

    public class VehicleAddForm : VehicleAdd
    {
        // Attention - SelectList for the associated item
        [Display(Name = "Manufacturer Name")]
        public SelectList ManufacturerList { get; set; }

        // Attention - Display the name of the associated item
        public string ManufacturerName { get; set; }
    }

    public class VehicleBase : VehicleAdd
    {
        public int Id { get; set; }
    }

    public class VehicleWithDetail : VehicleBase
    {
        [Display(Name = "Manufacturer Name")]
        public string ManufacturerName { get; set; }

        [Display(Name = "Country")]
        public string ManufacturerCountryName { get; set; }
    }

    public class VehicleEditForm
    {
        public int Id { get; set; }

        // To be displayed only
        public string Model { get; set; }

        // To be displayed only
        public string Trim { get; set; }

        // To be displayed only
        public int ModelYear { get; set; }

        [Range(1, Int32.MaxValue)]
        [Display(Name = "Sell Price")]
        public int MSRP { get; set; }

        // To be displayed only
        public string ManufacturerName { get; set; }

        // We usually NEVER edit/change the to-one association
        // in an "edit existing" use case
    }

    public class VehicleEdit
    {
        public int Id { get; set; }

        // We allow ONLY the MSRP to be edited/changed
        [Range(1, Int32.MaxValue)]
        [Display(Name = "Sell Price")]
        public int MSRP { get; set; }
    }

}
