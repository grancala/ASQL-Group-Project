///FILE : FileData.cs
///PROJECT : ASQL - Group Project
///PROGRAMMER(S) : Nick Whitney, Constantine Grigoriadis, Jim Raithby
///FIRST VERSION : 12/6/2014
///DESCRIPTION : Class to emulate a file full of data
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace FinalProject
{
    /// <summary>
    /// Class to emulate a file full of data
    /// </summary>
    public class FileData
    {
        List<StateWeatherInfo> info;


        /// <summary>
        /// Creates a new info list, and loads data from a file
        /// </summary>
        /// <param name="UserName">User requesting load</param>
        /// <param name="filename">Filename to load</param>
        /// <returns>True on success</returns>
        public bool Load(string UserName, string filename)
        {
            bool successfulLoad = true;
            info = new List<StateWeatherInfo>();
            string[] lines = null;

            // attempts to read lines to a file
            try
            {
                lines = File.ReadAllLines(filename);
            }
            catch (Exception e)
            {
                // logs an exception
                StringBuilder builder = new StringBuilder();
                Helper.BuildErrorMessage(e, ref builder);
                Logging.LogError(UserName, builder.ToString());
                successfulLoad = false;
            }

            // line number = 0 is file headers
            int lineNumber = 1;
            while (lineNumber < lines.Count() && successfulLoad == true)
            {
                try
                {
                    // attempts to split data and enter ino a new state weather info
                    string[] data = lines[lineNumber].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    StateWeatherInfo temp = new StateWeatherInfo(data);
                    if (temp.IsValid)
                    {
                        info.Add(new StateWeatherInfo(data));
                    }
                    else
                    {
                        // logs an error
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine("Error on line number " + lineNumber.ToString());
                        builder.AppendLine(temp.GetErrors());
                        Logging.LogError(UserName, builder.ToString());
                    }
                }
                catch (Exception ex)
                {
                    // logs an exception
                    StringBuilder builder = new StringBuilder();
                    Helper.BuildErrorMessage(ex, ref builder);
                    Logging.LogError(UserName, builder.ToString());
                    successfulLoad = false;
                }
                ++lineNumber;
            }
            return successfulLoad;
        }


        /// <summary>
        /// Creates an insert string and sends
        /// </summary>
        /// <param name="UserName">User requesting insert</param>
        /// <param name="TableName">Table to delete from and insert to</param>
        /// <param name="dataPresent">Is data present before</param>
        /// <param name="overwrite">Is data allowed to be overwritten</param>
        /// <returns>True if successful</returns>
        public bool Insert(string UserName, string TableName, bool dataPresent, bool overwrite)
        {
            bool successfulInsert = true;

            #region logging data

            Int16 iYear = info.First().Year;
            byte iMonth = info.First().Month;
            Int16 fYear = info.Last().Year;
            byte fMonth = info.Last().Month;
            DateTime initialTime = DateTime.UtcNow;

            #endregion

            try
            {
                DataTable table = new DataTable("weatherInfo");
                table.Columns.Add("stateCode", typeof(int));
                table.Columns.Add("data_year", typeof(Int16));
                table.Columns.Add("data_month", typeof(byte));
                table.Columns.Add("PCP", typeof(double));
                table.Columns.Add("CDD", typeof(int));
                table.Columns.Add("HDD", typeof(int));
                table.Columns.Add("TAVG", typeof(double));
                table.Columns.Add("TMIN", typeof(double));
                table.Columns.Add("TMAX", typeof(double));

                foreach (StateWeatherInfo current in info)
                {
                    table.Rows.Add(current.StateCode, current.Year, current.Month, current.PCP, current.CDD, current.HDD, current.Tavg, current.Tmin, current.Tmax);
                }


                if (Database.Insert(UserName, TableName, table))
                {
                    Logging.Log(UserName, iYear, iMonth, fYear, fMonth, initialTime, dataPresent, overwrite);
                }
                else
                {
                    successfulInsert = false;
                }
            }
            catch (Exception ex)
            {
                StringBuilder builder = new StringBuilder();
                Helper.BuildErrorMessage(ex, ref builder);
                Logging.LogError(UserName, builder.ToString());
                successfulInsert = false;
            }
            return successfulInsert;
        }
    }
}