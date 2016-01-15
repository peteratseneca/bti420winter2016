using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strings
{
    class Program
    {
        static void Main(string[] args)
        {
            // This console app example will help you learn about the "string" class
            // You will use this class almost all the time, so learn it 
            // http://msdn.microsoft.com/en-us/library/system.string(v=vs.110).aspx

            // A string is a reference object, and is stored on the heap
            // For convenience and simpler syntax, the compiler allows us to 
            // initialize a string without using the "new" operator

            // A string is immutable (i.e. cannot change it after it's initialized) 
            // Again, for convenience and simpler syntax, the compiler allows us to
            // treat it as though it was mutable; it takes care of the object 
            // re-creation behind the scenes at runtime

            // Output a string literal
            Console.WriteLine("Hello, world!");

            // As a variable
            string hello = "Hello, world!";
            Console.WriteLine(hello);

            // Initialize a newline to simplify the syntax
            string br = Environment.NewLine;

            // Common string methods and properties
            String s = "   BTI420 - Web Programming on Windows [Servers]   ";

            // String concatenation, for simple strings
            Console.WriteLine(br + "Original string: " + s);

            // Trim whitespace and replace the original string
            s = s.Trim();
            Console.WriteLine(br + "Trimmed string:  " + s);

            // Case changes
            Console.WriteLine("Upper case:      " + s.ToUpper());
            Console.WriteLine("Lower case:      " + s.ToLower());

            // Number of characters in string
            Console.WriteLine(br + "String length:   " + s.Length.ToString() + " characters");
            // A better version of the above, using String.Format()
            Console.WriteLine(string.Format("{0}String length:   {1} characters", br, s.Length));

            // Find the position number of the left-square-bracket, or the last 'g'
            Console.WriteLine(br + "Left square bracket found at index: " + s.IndexOf("["));
            Console.WriteLine("Last letter 'g' found at index:     " + s.LastIndexOf("g"));

            // Extract the 10-character substring starting at index 17
            Console.WriteLine(br + "Substring, index 17, for 10 characters: " + s.Substring(17, 10));

            // Replace some characters
            string newS = s.Replace("Servers", "Web Platform");
            Console.WriteLine(br + "Replace 'Servers' with 'Web Platform': " + br + newS);

            Console.WriteLine(br);

            // ############################################################

            string saying = "The quick brown fox jumped over the lazy dog.";

            // Split string at whitespace; put results into a string array
            string[] sayingWords = saying.Split();

            // Enumerate using a standard 'for' loop
            // http://msdn.microsoft.com/en-us/library/ch45axte.aspx
            for (int i = 0; i < sayingWords.Length; i++)
            {
                Console.WriteLine("For loop:     " + sayingWords[i]);
            }

            Console.WriteLine(br);

            // Enumerate using a 'foreach' loop
            // http://msdn.microsoft.com/en-us/library/ttw7t8t6.aspx
            foreach (var word in sayingWords)
            {
                Console.WriteLine("Foreach loop: " + word);
            }

            Console.WriteLine(br);

            // ############################################################

            // Testing strings

            // Existing string, is empty?
            string empty = "";

            Console.WriteLine("Empty, using length:  " + (empty.Length == 0));
            Console.WriteLine("Empty, using compare: " + (empty == "") + br);

            // String object, not sure whether it's null

            Console.WriteLine("Known, using method:    " + string.IsNullOrEmpty("hello"));
            Console.WriteLine("Known, using method:    " + string.IsNullOrEmpty(empty));
            string nullString = null;
            Console.WriteLine("Unknown, using method:  " + string.IsNullOrEmpty(nullString));

            Console.WriteLine(br);
        }

    }

}
