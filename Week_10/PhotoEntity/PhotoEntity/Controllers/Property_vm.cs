using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoEntity.Controllers
{
    public class PropertyAddForm
    {
        public PropertyAddForm()
        {
            Price = 300000;
        }

        [Required, StringLength(200)]
        [Display(Name = "Street address")]
        public string Address { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "City / town")]
        public string City { get; set; }

        [Range(1, Int32.MaxValue)]
        [Display(Name = "Asking price")]
        public int Price { get; set; }
    }

    public class PropertyAdd
    {
        [Required, StringLength(200)]
        [Display(Name = "Street address")]
        public string Address { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "City / town")]
        public string City { get; set; }

        [Range(1, Int32.MaxValue)]
        [Display(Name = "Asking price")]
        public int Price { get; set; }
    }

    public class PropertyBase : PropertyAdd
    {
        public int Id { get; set; }
    }

    // Attention - 04 - View model class for an object with a photo info collection
    public class PropertyWithPhotoStringIds : PropertyBase
    {
        public PropertyWithPhotoStringIds()
        {
            Photos = new List<PhotoBase>();
        }

        public IEnumerable<PhotoBase> Photos { get; set; }
    }

}
