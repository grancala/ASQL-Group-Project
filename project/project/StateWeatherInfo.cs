using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    /// <summary>
    /// Class to represent a single line in the database and in the file
    /// </summary>
    class StateWeatherInfo
    {
        enum HeaderOptions { StateCode, Division, YearMonth, PCP, TAVG, PDSI, PHDI, ZNDX, PMDI, CDD, HDD, SP01, SP02, SP03, SP06, SP09, SP12, SP24, TMIN, TMAX, END };

        #region database data members

        private int stateCode = 0;
        private Int16 year = 0;
        private byte month = 0;
        private decimal tavg = 0;
        private decimal tmin = 0;
        private decimal tmax = 0;
        private decimal pcp = 0;
        private int cdd = 0;
        private int hdd = 0;

        public int StateCode
        {
            get { return stateCode; }
            set { stateCode = value; }
        }

        public Int16 Year
        {
            get { return year; }
            set { year = value; }
        }

        public byte Month
        {
            get { return month; }
            set { month = value; }
        }
        
        public decimal Tavg
        {
            get { return tavg; }
            set { tavg = value; }
        }
        
        public decimal Tmin
        {
            get { return tmin; }
            set { tmin = value; }
        }
        
        public decimal Tmax
        {
            get { return tmax; }
            set { tmax = value; }
        }
        
        public decimal PCP
        {
            get { return pcp; }
            set { pcp = value; }
        }
        
        public int CDD
        {
            get { return cdd; }
            set { cdd = value; }
        }
        
        public int HDD
        {
            get { return hdd; }
            set { hdd = value; }
        }
        
        #endregion


        #region validation data members

        private List<String> FailedParses = new List<string>();
        private bool isValid = true;

        public bool IsValid 
        { 
            get { return isValid; } 
        }

        #endregion


        /// <summary>
        /// Fills in the structure of data from the file
        /// </summary>
        /// <param name="data">Data split by "," from a file</param>
        public StateWeatherInfo(string[] data)
        {
            // ensures data has enough lines
            if(data.Count() != (int)HeaderOptions.END)
            {
                isValid = false;
            }
            else
            {
                // attempts to parse state code
                if (!(int.TryParse(data[(int)HeaderOptions.StateCode].Trim(), out stateCode)))
                {
                    FailedParses.Add("StateCode");
                    isValid = false;
                }

                // attempts to parse the year and month
                Int16 tempYear = 0;
                byte tempMonth = 0;
                if (Helper.ParseYearMonth(data[(int)HeaderOptions.YearMonth].Trim(), out tempYear, out tempMonth))
                {
                    year = tempYear;
                    month = tempMonth;
                }
                else
                {
                    FailedParses.Add("YearMonth");
                }

                // attempts to parse the pcp, and transform it into mm
                if(Decimal.TryParse(data[(int)HeaderOptions.PCP].Trim(),out pcp))
                {
                    pcp = Helper.InchesToMM(pcp);
                }
                else
                {
                    FailedParses.Add("PCP");
                    isValid = false;
                }

                // attempts to parse the tavg, and transform it into celcius
                if (Decimal.TryParse(data[(int)HeaderOptions.TAVG].Trim(), out tavg))
                {
                    tavg = Helper.FahrenheitToCelcius(tavg);
                }
                else
                {
                    FailedParses.Add("TAVG");
                    isValid = false;
                }

                // attempts to parse the tmin, and transform it into celcius
                if (Decimal.TryParse(data[(int)HeaderOptions.TMIN].Trim(), out tmin))
                {
                    tmin = Helper.FahrenheitToCelcius(tmin);
                }
                else
                {
                    FailedParses.Add("TMIN");
                    isValid = false;
                }

                // attempts to parse the tmax, and transform it into celcius
                if (Decimal.TryParse(data[(int)HeaderOptions.TMAX].Trim(), out tmax))
                {
                    tmax = Helper.FahrenheitToCelcius(tmax);
                }
                else
                {
                    FailedParses.Add("TMAX");
                    isValid = false;
                }

                // attempts to parse the cdd
                if (!(int.TryParse(data[(int)HeaderOptions.CDD].Trim(), out cdd)))
                {
                    FailedParses.Add("CDD");
                    isValid = false;
                }

                // attempts to parse the hdd
                if (!(int.TryParse(data[(int)HeaderOptions.HDD].Trim(), out hdd)))
                {
                    FailedParses.Add("HDD");
                    isValid = false;
                }
            }

            // if something is invalid, set to known data
            if (!IsValid)
            {
                SetDefaults();
            }
        }
        

        /// <summary>
        /// Sets all database data members to known values
        /// </summary>
        private void SetDefaults()
        {
            stateCode = -1;
            Year = 0;
            Month = 0;
            Tavg = 0;
            Tmin = 0;
            Tmax = 0;
            PCP = 0;
            CDD = 0;
            HDD = 0;
        }


        /// <summary>
        /// Creates a string enumerating the errors while parsing
        /// </summary>
        /// <returns>Error list</returns>
        public string GetErrors()
        {
            StringBuilder errors = new StringBuilder();
            errors.Append("Failed to parse the following fields:");
            foreach (string err in FailedParses)
            {
                errors.AppendFormat(" {0}", err);
            }
            return errors.ToString();
        }

    }
}
