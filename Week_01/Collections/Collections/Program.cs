using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example 1
            // Collection of strings
            // ########################################

            // Create a collection
            var colours = new List<string>();

            // Add items to the collection
            colours.Add("Red");
            colours.Add("Green");
            colours.Add("Blue");

            Console.WriteLine("Strings:");

            // Send the results to standard output
            for (int i = 0; i < colours.Count; i++)
            {
                Console.WriteLine("   " + colours[i]);
            }

            // Blank line
            Console.WriteLine();

            // Example 2
            // Collection of ints
            // ########################################

            // Create a collection
            var grades = new List<int>();

            // Add items to the collection
            grades.Add(78);
            grades.Add(68);
            grades.Add(72);
            grades.Add(83);
            grades.Add(79);

            // Can do a "for" loop, like above, or
            // can do a "foreach" loop

            Console.WriteLine("Numbers:");

            var total = 0;

            foreach (var item in grades)
            {
                Console.WriteLine(string.Format("   Processing grade {0}", item));
                total += item;
            }

            // Some results
            Console.WriteLine(string.Format("   Average: {0}", (total / grades.Count)));
            Console.WriteLine(string.Format("   Highest grade: {0}", grades.Max()));

            // Blank line
            Console.WriteLine();

            // Example 3
            // Collection of Person objects
            // ########################################

            // Create a collection
            var teachers = new List<Person>();

            // Add items to the collection

            var peter = new Person();
            peter.Name = "Peter McIntyre";
            peter.Age = 39;
            teachers.Add(peter);

            var wei = new Person { Name = "Wei Song", Age = 35 };
            teachers.Add(wei);

            teachers.Add(new Person { Name = "Ian Tipson", Age = 41 });

            // Output
            Console.WriteLine("Objects:");

            foreach (var item in teachers)
            {
                // Using the new C# 6.0 interpolated strings feature
                // https://msdn.microsoft.com/en-us/library/dn961160.aspx

                Console.WriteLine($"   Teacher {item.Name} is {item.Age} years old.");
            }

            // Blank line
            Console.WriteLine();
        }
    }

    class Person
    {
        public Person() { }

        // Properties
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
