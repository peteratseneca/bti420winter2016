using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelectListIntro.Controllers
{
    public class HomeController : Controller
    {
        // Attention - Properties will hold collections of data
        // Data is in memory only, and is not persisted

        public IEnumerable<string> Classifications { get; set; }
        public IEnumerable<ManufacturerBase> Manufacturers { get; set; }
        public List<AcademicTerm> AcademicTerms { get; set; }
        public List<Course> Courses { get; set; }

        // Constructor
        public HomeController()
        {
            // Create some classifications
            Classifications = new List<string>
            {
                "Passenger car: compact",
                "Passenger car: medium",
                "Sport utility vehicle",
                "Pickup truck",
                "Luxury sedan"
            };

            // Creaate some manufacturers
            Manufacturers = new List<ManufacturerBase>
            {
                new ManufacturerBase { Id = 1, Name = "Ford Motor Company", Country = "USA" },
                new ManufacturerBase { Id = 2, Name = "Toyota Motor Company", Country = "Japan" },
                new ManufacturerBase { Id = 3, Name = "Volkswagen AG", Country = "Germany" }
            };

            // Create some other data
            CreateData();
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: /Home/AddVehicle
        public ActionResult AddVehicle()
        {
            // Attention - Create and configure a view model object

            var form = new VehicleAddForm();

            form.ModelYear = DateTime.Now.Year;

            // this.Manufacturers is a
            // collection of Manufacturer objects

            form.ManufacturerList = new SelectList(this.Manufacturers, "Id", "Name");

            // this.Classifications is a
            // collection of string objects
            // Notice that we use the simple constructor
            // When the Razor view engine creates the HTML item-selection element,
            // it will not create a "value" attribute
            // The result will be that the visible text will be submitted
            // as the value of the name-value pair

            form.ClassificationList = new SelectList(this.Classifications);

            return View(form);
        }

        public ActionResult PlanCourses()
        {
            // Attention - Create and configure a view model object

            var form = new CoursePlanForm();

            // Attention - SelectList objects
            form.AcademicTermList = new SelectList(this.AcademicTerms, "Id", "TermName");
            form.CourseList = new MultiSelectList(this.Courses, "Id", "CourseName");

            // Attention - Carefully study the PlanCourses view
            return View(form);
        }

        // ############################################################

        // Create initial data (not saved, used only in memory)
        private void CreateData()
        {
            AcademicTerms = new List<AcademicTerm>();
            AcademicTerms.Add(new AcademicTerm { Id = 2164, Year = 2016, Term = "Summer" });
            AcademicTerms.Add(new AcademicTerm { Id = 2167, Year = 2016, Term = "Fall" });
            AcademicTerms.Add(new AcademicTerm { Id = 2171, Year = 2017, Term = "Winter" });

            Courses = new List<Course>();
            Courses.Add(new Course { Id = 47, Code = "BTR490", Name = "Research Internship" });
            Courses.Add(new Course { Id = 49, Code = "BTP500", Name = "Data Structures" });
            Courses.Add(new Course { Id = 55, Code = "BTB520", Name = "Canadian Business Environment" });
            Courses.Add(new Course { Id = 61, Code = "BTS530", Name = "Project Planning" });
            Courses.Add(new Course { Id = 63, Code = "BTH540", Name = "User Interface Design" });
            Courses.Add(new Course { Id = 77, Code = "BTP600", Name = "Design Patterns" });
            Courses.Add(new Course { Id = 79, Code = "BTE620", Name = "Ethics, Law, Professionalism" });
            Courses.Add(new Course { Id = 122, Code = "BTS630", Name = "Project Implementation" });
            Courses.Add(new Course { Id = 132, Code = "BTC640", Name = "Multimedia Design" });
        }
    }
}