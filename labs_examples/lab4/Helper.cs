using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab4
{
   public static class Helper
    {
        public static string StringWithoutNumbers(string input) //why does not work via (this string input) - string extension
        {
            string output = Regex.Replace(input, @"[\d-]", string.Empty);
            //string output = new string(input.Where(c => c != '-' && (c < '0' || c > '9')).ToArray()); via lambda and Linq
            return output;
        }

        public static string NumbersWithoutString(string input) //why does not work via (this string input) - string extension
        {
            string output = Regex.Replace(input, "[A-Za-z&*%#$@!(){}. ]", string.Empty);
            output = Regex.Replace(output, @"\,+", ",");
            return output;
        }
    }
}
