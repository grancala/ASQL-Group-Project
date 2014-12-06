using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace project
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
                    if(temp.IsValid)
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



        //TODO
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
                #region first pass

                /*
                //FIRSTPASS
                // list of year months to be made
                List<YearMonth> ToBeMade = new List<YearMonth>();

                // find list of yearmonth's to be made
                foreach (StateWeatherInfo current in info)
                {
                    // if current.YearMonth is not in DB
                        // ToBeMade.add(current.YearMonth)
                }

                // insert yearmonths to be made
                foreach (YearMonth current in ToBeMade)
                {
                    // insert current
                }

                // create insert statement
                // string SQL = current.InsertHeader();
                foreach(StateWeatherInfo current in info)
                {
                    //SQL += current.InsertLine();
                }

                // send SQL
                */

                #endregion

                #region second pass

                //SECONDPASS
                // use DB generated state weather info?

                // info query
                    // var result = myList.GroupBy(test => test.id).Select(grp => grp.First()).ToList();
                // database query for yearmonth

                // insert previous join
                // join info and data, select where not exist
                    //infoQuery.RemoveAll(Item.YearMonth => databaseQuery.Contains(Item));
                    //insert infoQuery.YearMonth

                // insert info

                #endregion

                Logging.Log(UserName, iYear, iMonth, fYear, fMonth, initialTime, dataPresent, overwrite);
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
