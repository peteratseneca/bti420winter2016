using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoadDataFromCSV.Controllers
{
    // Attention - 3 - "...Add" view model class EXACTLY matches the CSV file contents

    public class EmployeeAdd
    {
        public EmployeeAdd()
        {
            BirthDate = DateTime.Now.AddYears(-25);
            HireDate = DateTime.Now.AddYears(-2);
        }

        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Family name")]
        public string FamilyName { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Given name(s)")]
        public string GivenNames { get; set; }

        [Display(Name = "Hire date")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Identity user identifier")]
        [DataType(DataType.EmailAddress)]
        public string IdentityUserId { get; set; }
    }

    public class EmployeeBase : EmployeeAdd
    {
        public int Id { get; set; }
    }
}
