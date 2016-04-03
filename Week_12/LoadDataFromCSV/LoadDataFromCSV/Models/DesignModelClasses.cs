using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace LoadDataFromCSV.Models
{
    // Add your design model classes below

    // Follow these rules or conventions:

    // To ease other coding tasks, the name of the 
    //   integer identifier property should be "Id"
    // Collection properties (including navigation properties) 
    //   must be of type ICollection<T>
    // Valid data annotations are pretty much limited to [Required] and [StringLength(n)]
    // Required to-one navigation properties must include the [Required] attribute
    // Do NOT configure scalar properties (e.g. int, double) with the [Required] attribute
    // Initialize DateTime and collection properties in a default constructor

    // Attention - 1 - Look in the App_Data folder to see the CSV file(s)

    // Attention - 2 - Design model class is based on the CSV file contents

    public class Employee
    {
        public Employee()
        {
            BirthDate = DateTime.Now.AddYears(-25);
            HireDate = DateTime.Now.AddYears(-2);
        }

        public int Id { get; set; }
        public DateTime BirthDate { get; set; }

        [Required, StringLength(100)]
        public string FamilyName { get; set; }

        [Required, StringLength(100)]
        public string GivenNames { get; set; }

        public DateTime HireDate { get; set; }

        [Required, StringLength(100)]
        public string IdentityUserId { get; set; }
    }

    public class RoleClaim
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }

}
