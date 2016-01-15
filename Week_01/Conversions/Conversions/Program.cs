using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversions
{
    class Program
    {
        static void Main(string[] args)
        {
            // Popular string-to-number, and number-to-string conversions

            // Number-to-string
            // Oft-used method is ToString()
            // int ToString() - http://msdn.microsoft.com/en-us/library/system.int32.tostring(v=vs.110).aspx 
            // double ToString() - http://msdn.microsoft.com/en-us/library/system.double.tostring(v=vs.110).aspx 

            // String formatting... general info
            // http://msdn.microsoft.com/en-us/library/26etazsy(v=vs.110).aspx

            // Standard numeric format strings
            // http://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.110).aspx

            // Custom numeric format strings
            // http://msdn.microsoft.com/en-us/library/0c899ak8(v=vs.110).aspx

            string br = Environment.NewLine;

            // Simple integer
            int intNum = 12345;

            Console.WriteLine("int:            " + intNum.ToString());
            Console.WriteLine("int, formatted: " + intNum.ToString("#,##0") + br);

            // Simple double
            double doubleNum = 1234567.89;

            Console.WriteLine("double:            " + doubleNum.ToString());
            Console.WriteLine("double, formatted: " + doubleNum.ToString("N"));
            Console.WriteLine("double, currency:  " + doubleNum.ToString("C"));
            Console.WriteLine("double, custom:    " + doubleNum.ToString("#,##0.0000") + br);

            // ############################################################

            // String-to-number

            // Convert a string that for sure is or will be a number
            // You must be confident that this is the situation

            string intString = "12345";
            string doubleString = "1234567.89";

            // Re-use 'intNum' and 'doubleNum' from above

            intNum = Convert.ToInt32(intString);
            Console.WriteLine("int, formatted:    " + intNum.ToString("#,##0"));

            doubleNum = Convert.ToDouble(doubleString);
            Console.WriteLine("double, formatted: " + doubleNum.ToString("N") + br);

            // If you're not confident, you can attempt to convert it

            // int

            intNum = 0;
            bool intAttempt = Int32.TryParse(intString, out intNum);
            Console.WriteLine(string.Format
                ("Attempt to parse {0}; result={1}; number={2}",
                intString, intAttempt, intNum));

            intString = "123abc";
            intNum = 0;
            intAttempt = Int32.TryParse(intString, out intNum);
            Console.WriteLine(string.Format
                ("Attempt to parse {0}; result={1}; number={2}{3}",
                intString, intAttempt, intNum, br));

            // double

            doubleNum = 0;
            bool doubleAttempt = double.TryParse(doubleString, out doubleNum);
            Console.WriteLine(string.Format
                ("Attempt to parse {0}; result={1}; number={2}",
                doubleString, doubleAttempt, doubleNum));

            doubleString = "123abc.asdf123";
            doubleNum = 0;
            doubleAttempt = double.TryParse(doubleString, out doubleNum);
            Console.WriteLine(string.Format
                ("Attempt to parse {0}; result={1}; number={2}",
                doubleString, doubleAttempt, doubleNum));

            Console.WriteLine(br);
        }

    }

}
