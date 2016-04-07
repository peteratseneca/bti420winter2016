using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebServiceExample.Controllers
{
    // Attention - 2 - Employee view model classes, plain, and with reports-to and direct-reports info

    public class EmployeeBase
    {
        [Key]
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
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

        // Mapped from the Employee2 property
        public EmployeeBase ReportsTo { get; set; }

        // Mapped from the Employee1 property
        public IEnumerable<EmployeeBase> DirectReports { get; set; }
    }
}
