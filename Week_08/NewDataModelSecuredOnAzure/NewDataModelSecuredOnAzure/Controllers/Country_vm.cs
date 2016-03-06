using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewDataModelSecuredOnAzure.Controllers
{
    public class CountryBase
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Country Name")]
        public string Name { get; set; }
    }

}