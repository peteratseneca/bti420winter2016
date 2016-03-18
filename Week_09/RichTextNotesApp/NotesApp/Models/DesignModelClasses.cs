using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models
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

    public class Note
    {
        public Note()
        {
            DateCreated = DateTime.Now;
            DateOfLastEdit = DateTime.Now;
        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, StringLength(10000)]
        public string Content { get; set; }

        [Required, StringLength(128)]
        public string Owner { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateOfLastEdit { get; set; }
    }
}
