using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetAllGetOne.Controllers
{
    public class CustomerBase
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name ="First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [StringLength(80)]
        public string Company { get; set; }

        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(40)]
        public string State { get; set; }

        [StringLength(40)]
        public string Country { get; set; }

        [StringLength(10)]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Email address")]
        public string Email { get; set; }
    }

    public class SearchByName
    {
        [Required, StringLength(80)]
        [Display(Name = "All or part of name")]
        public string Name { get; set; }
    }
}
