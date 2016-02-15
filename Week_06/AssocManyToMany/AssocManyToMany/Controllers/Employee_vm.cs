using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocManyToMany.Controllers
{
    public class EmployeeBase
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Employee name")]
        public string Name { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Office number")]
        public string Office { get; set; }
    }

    public class EmployeeWithJobDuties : EmployeeBase
    {
        public EmployeeWithJobDuties()
        {
            JobDuties = new List<JobDutyBase>();
        }

        [Display(Name = "List of job duties")]
        public IEnumerable<JobDutyBase> JobDuties { get; set; }
    }

    // ############################################################

    // Attention - Edit job duties for an employee
    // Send TO the HTML Form
    public class EmployeeEditJobDutiesForm
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Employee name")]
        public string Name { get; set; }

        // Attention - Multiple select requires a MultiSelectList object
        [Display(Name = "Job duties")]
        public MultiSelectList JobDutyList { get; set; }
    }

    // Data submitted by the browser user
    public class EmployeeEditJobDuties
    {
        public EmployeeEditJobDuties()
        {
            JobDutyIds = new List<int>();
        }

        public int Id { get; set; }

        // Incoming collection of selected job duty identifiers
        public IEnumerable<int> JobDutyIds { get; set; }
    }
}
