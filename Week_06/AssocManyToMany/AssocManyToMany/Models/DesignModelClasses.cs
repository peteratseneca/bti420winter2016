using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssocManyToMany.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Office { get; set; }

        // Optional to-many job duties
        public ICollection<JobDuty> JobDuties { get; set; }
    }

    public class JobDuty
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(1000)]
        public string Description { get; set; }

        // Optional to-many employees
        public ICollection<Employee> Employees { get; set; }
    }
}
