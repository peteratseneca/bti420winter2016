using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SelectListIntro.Controllers
{
    // This source code file will hold "...Form" view model classes

    // Attention - View model for an "add new" form
    public class VehicleAddForm
    {
        public string Model { get; set; }
        public string Trim { get; set; }
        public int ModelYear { get; set; }

        // Required on SelectList?

        // SelectList for Classification
        public SelectList ClassificationList { get; set; }

        // SelectList for Manufacturer
        public SelectList ManufacturerList { get; set; }
    }

    // Attention - View model for an "add new" form
    public class CoursePlanForm
    {
        [Display(Name ="Student name")]
        public string Name { get; set; }

        // SelectList for AcademicTerm
        [Display(Name ="Academic term")]
        public SelectList AcademicTermList { get; set; }

        // SelectList for Course collection
        [Display(Name = "Desired course(s)")]
        public MultiSelectList CourseList { get; set; }
    }

    // ############################################################
    // The essential view model classes for objects...

    public class ManufacturerBase
    {
        public ManufacturerBase()
        {
            Vehicles = new List<VehicleBase>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public IEnumerable<VehicleBase> Vehicles { get; set; }
    }

    public class VehicleBase
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public string Classification { get; set; }
        public int ModelYear { get; set; }

        [Required]
        public ManufacturerBase Manufacturer { get; set; }
    }

    public class ClassificationList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AcademicTerm
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Term { get; set; }

        // Assemble a nicer-looking value
        public string TermName
        {
            get { return string.Format("{0} - {1}", this.Year, this.Term); }
        }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        // Assemble a nicer-looking value
        public string CourseName
        {
            get { return string.Format("{0} - {1}", this.Code, this.Name); }
        }
    }

}
