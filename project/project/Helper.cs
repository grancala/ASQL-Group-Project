using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    public static class Helper
    {
        public static decimal FahrenheitToCelcius(decimal fah)
        {
            return ((fah - 32) * 5.0M / 9.0M);
        }

        public static decimal InchesToMM(decimal inches)
        {
            return inches / 0.039370M;
        }

        public static bool ParseYearMonth(string input, out int year, out int month)
        {
            bool returnVal = true;
            if(!Int32.TryParse(input.Substring(0, 4), out year))
            {
                returnVal = false;
            }
            if(!Int32.TryParse(input.Substring(4, 2), out month))
            {
                returnVal = false;
            }
            return returnVal;
        }
    }
}
