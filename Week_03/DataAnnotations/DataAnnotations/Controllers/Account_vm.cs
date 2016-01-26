using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace DataAnnotations.Controllers
{
    // account create, two passwords
    // regex for that?
    // email address etc.
    // birth year (over 13 or 18)
    // data type...

    public class AccountAdd
    {
        public AccountAdd()
        {
            DateOfBirth = DateTime.Now.AddYears(-25);
        }

        [Required, StringLength(100, MinimumLength = 2)]
        [Display(Name = "First (given) name(s)")]
        public string FirstName { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        [Display(Name ="Last (family) name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Web site address")]
        [DataType(DataType.Url)]
        public string Website { get; set; }

        [Required, StringLength(1000)]
        [Display(Name = "Tell us a little about yourself")]
        [DataType(DataType.MultilineText)]
        public string AboutMe { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Password")]
        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&'])[^ ]{8,}", ErrorMessage ="Password must be 8+ characters, have 1+ digits, 1+ upper-case characters, 1+ lower-case characters, and 1+ special characters ( ! @ # $ % ^ &)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&'])[^ ]{8,}", ErrorMessage = "Password must be 8+ characters, have 1+ digits, 1+ upper-case characters, 1+ lower-case characters, and 1+ special characters ( ! @ # $ % ^ &)")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordCompare { get; set; }
    }

    public class AccountBase : AccountAdd
    {
        public AccountBase()
        {

        }

        public int Id { get; set; }
    }

    public class AccountAddPlain
    {
        public AccountAddPlain()
        {
            DateOfBirth = DateTime.Now.AddYears(-25);
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string AboutMe { get; set; }
        public string Password { get; set; }
        public string PasswordCompare { get; set; }
    }
}
