using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocSelf.Controllers
{
    // Base class with many of the properties from the design model
    public class EmployeeBase
    {
        public EmployeeBase()
        {
            BirthDate = DateTime.Now.AddYears(-25);
            HireDate = DateTime.Now;
        }

        [Key]
        public int EmployeeId { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        // Attention - FullName composed property
        [Display(Name = "Employee name")]
        public string FullName
        {
            get
            {
                return string.Format("{0}, {1}", LastName, FirstName);
            }
        }

        [StringLength(30)]
        [Display(Name = "Job title")]
        public string Title { get; set; }

        [Display(Name = "Birth date")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Hire date")]
        public DateTime? HireDate { get; set; }

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

        [StringLength(60)]
        public string Email { get; set; }
    }

    public class EmployeeWithOrgInfo : EmployeeBase
    {
        public EmployeeWithOrgInfo()
        {
            Employee1 = new List<EmployeeBase>();
        }

        // Self-referencing to-one properties
        // If YOU were writing this class yourself, you should use better names
        // The nullable int should be named "ReportsToId"
        // The EmployeeBase should be named "ReportsTo"
        [Display(Name = "Reports to")]
        public int? ReportsTo { get; set; }

        [Display(Name = "Reports to")]
        public virtual EmployeeBase Employee2 { get; set; }

        // Self-referencing to-many property
        // If YOU were writing this class yourself, you should use a better name
        // The collection should be named "DirectReports" (plural)
        [Display(Name = "Direct reports")]
        public IEnumerable<EmployeeBase> Employee1 { get; set; }
    }

    // ############################################################
    // Edit the supervisor

    // Attention - Edit the supervisor
    // Send this TO the HTML Form
    public class EmployeeEditSupervisorForm
    {
        public EmployeeEditSupervisorForm()
        {
            CurrentSupervisor = "(none)";
        }

        // Identification properties

        [Key]
        public int EmployeeId { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        // Current supervisor
        [Display(Name = "Current supervisor")]
        public string CurrentSupervisor { get; set; }

        // Will allow the job title to be edited

        [StringLength(30)]
        [Display(Name = "Job title")]
        public string Title { get; set; }

        // Attention - SelectList for the supervisor
        [Display(Name = "New supervisor")]
        public SelectList EmployeeList { get; set; }
    }

    // For the data submitted by the browser user
    public class EmployeeEditSupervisor
    {
        [Key]
        public int EmployeeId { get; set; }

        [StringLength(30)]
        [Display(Name = "Job title")]
        public string Title { get; set; }

        // Attention - Identifier for the supervisor
        public int ReportsToId { get; set; }
    }

    // ############################################################
    // Edit the direct reports

    // Attention - Edit the direct reports
    // Send this TO the HTML Form
    public class EmployeeEditDirectReportsForm
    {
        // Identification properties

        [Key]
        public int EmployeeId { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        // Attention - FullName composed property
        [Display(Name = "Employee name")]
        public string FullName
        {
            get
            {
                return string.Format("{0}, {1}", LastName, FirstName);
            }
        }

        // Attention - Current direct reports
        [Display(Name = "Current direct reports")]
        public IEnumerable<EmployeeBase> DirectReports { get; set; }

        // Attention - MultiSelectList for the direct reports
        [Display(Name = "New direct reports")]
        public MultiSelectList EmployeeList { get; set; }
    }

    public class EmployeeEditDirectReports
    {
        public EmployeeEditDirectReports()
        {
            EmployeeIds = new List<int>();
        }

        [Key]
        public int EmployeeId { get; set; }

        // Collection of selected identifiers
        public IEnumerable<int> EmployeeIds { get; set; }
    }

}
