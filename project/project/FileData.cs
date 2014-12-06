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
    public class FileData
    {
        
        List<StateWeatherInfo> info;


        public FileData()
        {
        }

        public bool Load(string filename)
        {
            //LOG attempting to load
            bool successfulLoad = true;
            info = new List<StateWeatherInfo>();

            string[] lines = File.ReadAllLines(filename);

            // line number = 0 is file headers
            int lineNumber = 1;
            while (lineNumber < lines.Count() && successfulLoad == true)
            {
                string[] data = lines[lineNumber].Split(',');
                try
                {
                    StateWeatherInfo temp = new StateWeatherInfo(data);
                    if(temp.IsValid)
                    {
                        info.Add(new StateWeatherInfo(data));
                    }
                    else
                    {
                        //LOG temp.GetErrors(); add line number before it and that you are ignoring it.
                    }
                }
                catch (Exception ex)
                {
                    //LOG failed loading exception ex.Message
                    successfulLoad = false;
                }
                ++lineNumber;
            }
            return successfulLoad;
        }

        public bool Insert()
        {
            bool successfulInsert = true;
            //LOG attempting to insert
            try
            {
                #region first pass

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

            }
            catch (Exception ex)
            {
                //LOG failed inserting, rolling back
                successfulInsert = false;
            }
            return successfulInsert;
        }
    }
}
