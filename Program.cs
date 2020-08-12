using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RationalNumbersMultiple
{
    class Program
    {
        static void Main(string[] args)
        {
            //string str = "1/4";
            //Regex regex = new Regex(@"^\d*\.\d*[/]\d*");
            //Regex rationalPattern = new Regex(@"^\d*/\d*");

            //Console.WriteLine(rationalPattern.IsMatch(str));
            //Console.WriteLine(regex.IsMatch(str));




            Context context = new Context("2.1/4 + 5.6/1 - 3.1/4");

            context.Solve();

            Console.ReadLine();
        }
    }
}
