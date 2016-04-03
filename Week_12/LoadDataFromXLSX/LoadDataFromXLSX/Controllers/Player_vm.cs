using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoadDataFromXLSX.Controllers
{
    // Attention - 3 - "...Add" view model class EXACTLY matches the XLSX file contents

    public class PlayerAdd
    {
        public PlayerAdd()
        {
            BirthDate = DateTime.Now.AddYears(-25);
        }

        [Required, StringLength(10)]
        public string Team { get; set; }

        [Range(0, 99)]
        [Display(Name = "Uniform number")]
        public int UniformNumber { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Player name")]
        public string PlayerName { get; set; }

        [Required, StringLength(10)]
        public string Position { get; set; }

        [Required, StringLength(10)]
        public string Status { get; set; }

        [Required, StringLength(10)]
        [Display(Name = "Height in feet and inches (e.g. 6'1\")")]
        public string Height { get; set; }

        [Range(100, 500)]
        [Display(Name = "Weight in pounds")]
        public int Weight { get; set; }

        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Range(0, 30)]
        [Display(Name = "Years of experience")]
        public int YearsExperience { get; set; }

        [Required, StringLength(100)]
        public string College { get; set; }
    }

    public class PlayerBase : PlayerAdd
    {
        public int Id { get; set; }
    }
}
