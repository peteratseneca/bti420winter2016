using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocManyToMany.Controllers
{
    public class JobDutyBase
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Job duty name")]
        public string Name { get; set; }

        [Required, StringLength(1000)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        // Attention - New property, composed name + description
        // Getter only, no setter (i.e. it's read only)
        // Notice the use of C# 6.0 string interpolation feature
        [Display(Name = "Job duty and description")]
        public string FullName
        {
            get
            {
                return $"{Name} - {Description}";
            }
        }
    }

    public class JobDutyWithEmployees : JobDutyBase
    {
        public JobDutyWithEmployees()
        {
            Employees = new List<EmployeeBase>();
        }

        [Display(Name = "Employees with this job duty")]
        public IEnumerable<EmployeeBase> Employees { get; set; }
    }

    // Send TO the HTML Form
    public class JobDutyEditEmployeesForm
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Job duty name")]
        public string Name { get; set; }

        // Attention - Multiple select requires a MultiSelectList object
        public MultiSelectList EmployeeList { get; set; }
    }

    // Data submitted by the browser user
    public class JobDutyEditEmployees
    {
        public JobDutyEditEmployees()
        {
            EmployeeIds = new List<int>();
        }

        public int Id { get; set; }

        // Incoming collection of selected employee identifiers
        public IEnumerable<int> EmployeeIds { get; set; }
    }
}
