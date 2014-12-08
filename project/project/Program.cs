using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    class Program
    {
        static void Main(string[] args)
        {
            string configFile = "config.ini";
            bool configSuccess = true;
            string dataFile = string.Empty;
            string LogFile = string.Empty;
            string connectionString = string.Empty;
            string UserName = "Demo";
            string TableName = "Demo_12_21_2012";

            #region config file

            // attempts to load and get data from the config file
            try
            {
                ConfigFile config = new ConfigFile(configFile);

                connectionString = config.getValue("connectionString");
                dataFile = config.getValue("DataFile");
                LogFile = config.getValue("LogFile");
            }
            catch
            {
                Console.WriteLine("error reading values from config file");
                configSuccess = false;
            }

            #endregion

            #region excute

            // if config file was successfully loaded and parsed
            if(configSuccess)
            {
                // set log file and load data
                Logging.FileName = LogFile;
                Database.ConnectionString = connectionString;
                //Database.ConnectionString = "Initial Catalog=ASQLGroup;User ID=dbAccessor;Password=wingding;Data Source=localhost";
                
                FileData file = new FileData();
                bool success = file.Load(UserName, dataFile);
                if (success)
                {
                    Console.WriteLine("Successful load");
                    success = file.Insert(UserName, TableName, false, false);
                    if (success)
                    {
                        Console.WriteLine("Successful insert");
                    }
                    else
                    {
                        Console.WriteLine("Failure");
                    }
                }
                else
                {
                    Console.WriteLine("Failure");
                }
            }
            Console.ReadKey(true);
            #endregion
        }
    }
}
