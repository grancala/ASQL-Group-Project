using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    class StateWeatherInfo
    {
        enum HeaderOptions { StateCode, Division, YearMonth, PCP, TAVG, PDSI, PHDI, ZNDX, PMDI, CDD, HDD, SP01, SP02, SP03, SP06, SP09, SP12, SP24, TMIN, TMAX, END };

        #region data members

        private int stateCode = 0;
        private YearMonth yearMonth;
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
        
        public int Year
        {
            get { return yearMonth.Year; }
            set { yearMonth.Year = value; }
        }
        
        public int Month
        {
            get { return yearMonth.Month; }
            set { yearMonth.Month = value; }
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

        private List<String> FailedParses = new List<string>();
        private Boolean isValid = true;

        public Boolean IsValid { get; }


        public StateWeatherInfo()
        {
        }

        
        public StateWeatherInfo(string[] data)
        {
            if(data.Count() != (int)HeaderOptions.END)
            {
                isValid = false;
            }
            else
            {
                if (!(int.TryParse(data[(int)HeaderOptions.StateCode].Trim(), out stateCode)))
                {
                    FailedParses.Add("StateCode");
                    isValid = false;
                }

                int tempYear = 0;
                int tempMonth = 0;
                if (Helper.ParseYearMonth(data[(int)HeaderOptions.YearMonth].Trim(), out tempYear, out tempMonth))
                {
                    this.yearMonth = new YearMonth(tempYear, tempMonth);
                }
                else
                {
                    FailedParses.Add("YearMonth");
                }

                if(!(Decimal.TryParse(data[(int)HeaderOptions.PCP].Trim(),out pcp)))
                {
                    FailedParses.Add("PCP");
                    isValid = false;
                }

                if (!(Decimal.TryParse(data[(int)HeaderOptions.TAVG].Trim(), out tavg)))
                {
                    FailedParses.Add("TAVG");
                    isValid = false;
                }

                if (!(Decimal.TryParse(data[(int)HeaderOptions.TMIN].Trim(), out tmin)))
                {
                    FailedParses.Add("TMIN");
                    isValid = false;
                }

                if (!(Decimal.TryParse(data[(int)HeaderOptions.TMAX].Trim(), out tmax)))
                {
                    FailedParses.Add("TMAX");
                    isValid = false;
                }

                if (!(int.TryParse(data[(int)HeaderOptions.CDD].Trim(), out cdd)))
                {
                    FailedParses.Add("CDD");
                    isValid = false;
                }

                if (!(int.TryParse(data[(int)HeaderOptions.HDD].Trim(), out hdd)))
                {
                    FailedParses.Add("HDD");
                    isValid = false;
                }
            }

            if (IsValid)
            {
                SetDefaults();
            }
        }

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

        // insert into values
        public static string InsertHeader()
        {
            string sql = string.Empty;

            return sql;
        }

        // ( data )
        public string InsertLine ()
        {
            string sql = string.Empty;

            return sql;
        }

    }
}
