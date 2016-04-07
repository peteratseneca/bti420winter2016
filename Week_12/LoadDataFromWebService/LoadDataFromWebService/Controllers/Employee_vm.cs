using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoadDataFromWebService.Controllers
{
    // Attention - 2 - Employee view model classes, plain, and with reports-to and direct-reports info

    public class EmployeeBase
    {
        [Key]
        public int EmployeeId { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        public string Title { get; set; }

        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Hire date")]
        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }

    public class EmployeeWithDetails : EmployeeBase
    {
        public EmployeeWithDetails()
        {
            DirectReports = new List<EmployeeBase>();
        }

        public EmployeeBase ReportsTo { get; set; }
        public IEnumerable<EmployeeBase> DirectReports { get; set; }
    }
}
