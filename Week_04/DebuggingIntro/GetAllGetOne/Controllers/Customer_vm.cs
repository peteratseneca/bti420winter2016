using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetAllGetOne.Controllers
{
    public class CustomerAdd
    {
        public CustomerAdd()
        {

        }

        [Required]
        [StringLength(40)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
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
        public string PostalCode { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [Required]
        [StringLength(60)]
        public string Email { get; set; }
    }

    public class CustomerBase : CustomerAdd
    {
        public CustomerBase()
        {

        }

        [Key]
        public int CustomerId { get; set; }
    }

    public class CustomerEditContactInfoForm
    {
        public CustomerEditContactInfoForm() { }

        [Key]
        public int CustomerId { get; set; }

        // In the view, we will display this info
        [Required]
        [StringLength(40)]
        public string FirstName { get; set; }

        // In the view, we will display this info
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        // In the view, we will display this info
        [StringLength(80)]
        public string Company { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [Required]
        [StringLength(60)]
        public string Email { get; set; }
    }

    public class CustomerEditContactInfo
    {
        public CustomerEditContactInfo() { }

        [Key]
        public int CustomerId { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [Required]
        [StringLength(60)]
        public string Email { get; set; }
    }

}

