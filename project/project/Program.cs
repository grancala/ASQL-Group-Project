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

            //"Server=tcp:czsi7abult.database.windows.net,1433;
            //Database=a3ETL_db;
            //Uid=a3ETL@czsi7abult;
            //Pwd=Asql1234!;
            //Encrypt=yes;
            //Connection Timeout=30;

            // azure connection requirements
            string Server = string.Empty;
            string Database = string.Empty;
            string Uid = string.Empty;
            string Pwd = string.Empty;
            string Encrypted = string.Empty;
            string ConnectionTimeout = string.Empty;
            string LogFile = string.Empty;

            // attempts to load and get data from the config file
            try
            {
                ConfigFile config = new ConfigFile(configFile);

                Server = config.getValue("Server");
                Database = config.getValue("Database");
                Uid = config.getValue("Uid");
                Pwd = config.getValue("Pwd");
                Encrypted = config.getValue("Encrypted");
                ConnectionTimeout = config.getValue("ConnectionTimeout");
                dataFile = config.getValue("DataFile");
                LogFile = config.getValue("LogFile");
            }
            catch
            {
                Console.WriteLine("error reading values from config file");
                configSuccess = false;
            }

            // if config file was successfully loaded and parsed
            if(configSuccess)
            {
                // set log file and load data
                Logging.FileName = LogFile;

                FileData file = new FileData();
                bool success = file.Load("Current User", dataFile);
                if (success)
                {
                    Console.WriteLine("Successful load");
                }
                else
                {
                    Console.WriteLine("Failure");
                }
            }
        }
    }
}
