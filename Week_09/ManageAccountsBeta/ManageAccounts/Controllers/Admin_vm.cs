using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageAccounts.Controllers
{
    public class ApplicationUserBase
    {
        public ApplicationUserBase()
        {

            Roles = new List<string>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }

    }

    public class ApplicationUserAdd
    {

    }

    public class ApplicationUserDetail : ApplicationUserBase
    {

        public string GivenName { get; set; }

        public string Surname { get; set; }

    }

    public class ApplicationUserDelete
    {
        [Required]
        public string Id { get; set; }
    }

    public class ApplicationUserEditForm
    {

        [Key]
        public string Id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Given Name")]
        public string GivenName { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        // For select list
        [Display(Name = "All Roles")]
        public MultiSelectList RoleList { get; set; }

    }

    public class ApplicationUserEdit 
    {
        public ApplicationUserEdit()
        {
            Roles = new List<string>();
        }

        [Key]
        public string Id{ get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string UserName { get; set; }

        [Display(Name = "Given (first) name(s)")]
        [Required, StringLength(128, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string GivenName { get; set; }

        [Display(Name = "Surname (family name)")]
        [Required, StringLength(128, ErrorMessage = "The {0} must be {2} or fewer characters.")]
        public string Surname { get; set; }

        public IEnumerable<string> Roles{ get; set; }
    }
}

