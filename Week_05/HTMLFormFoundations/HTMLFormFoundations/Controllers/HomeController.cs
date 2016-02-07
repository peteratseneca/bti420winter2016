using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTMLFormFoundations.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SimpleInput()
        {
            return View();
        }

        public ActionResult DropdownListboxSimple()
        {
            // Create a collection of strings
            // They will be rendered in an item-selection control
            var colourNames = new List<string> { "Red", "Green", "Blue" };

            // Pass the collection to the view
            // At the top of the view, add a statement to declare the model/type
            // @model IEnumerable<string>
            return View(colourNames);
        }

        public ActionResult DropdownListboxObject()
        {
            // Create a collection of "Teacher" objects (the class is defined below)
            var teachers = new List<Teacher>
            {
                new Teacher { Id = 1, Name = "Peter McIntyre", Age = 39 },
                new Teacher { Id = 2, Name = "Ian Tipson", Age = 42 },
                new Teacher { Id = 3, Name = "Wei Song", Age = 35 }
            };

            // Pass the collection to the view
            // At the top of the view, add a statement to declare the model/type
            // @model IEnumerable<HTMLFormFoundations.Controllers.Teacher>
            return View(teachers);
        }

        public ActionResult RadioCheckSimple()
        {
            // Create a collection of strings
            // They will be rendered in an item-selection control
            var colourNames = new List<string> { "Red", "Green", "Blue" };

            // Pass the collection to the view
            // At the top of the view, add a statement to declare the model/type
            // @model IEnumerable<string>
            return View(colourNames);
        }

        public ActionResult RadioCheckObject()
        {
            // Create a collection of "Teacher" objects (the class is defined below)
            var teachers = new List<Teacher>
            {
                new Teacher { Id = 1, Name = "Peter McIntyre", Age = 39 },
                new Teacher { Id = 2, Name = "Ian Tipson", Age = 42 },
                new Teacher { Id = 3, Name = "Wei Song", Age = 35 }
            };

            // Pass the collection to the view
            // At the top of the view, add a statement to declare the model/type
            // @model IEnumerable<HTMLFormFoundations.Controllers.Teacher>
            return View(teachers);
        }
    }

    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

}