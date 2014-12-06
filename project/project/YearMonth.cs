using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    public class YearMonth
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public YearMonth (int newYear, int newMonth)
        {
            Year = newYear;
            Month = newMonth;
        }

        // SELECT id WHERE year and month
        public string InsertLine()
        {
            string sql = string.Empty;

            return sql;
        }
    }
}
