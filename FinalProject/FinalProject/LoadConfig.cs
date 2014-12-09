using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject
{
    /// <summary>
    /// Class to check if load is required
    /// Executes config load
    /// </summary>
    public static class LoadConfig
    {
        static string filename = @"C:\Users\Nick\Desktop\ASQL\FinalProject\FinalProject\config.ini";


        /// <summary>
        /// Loads from config.ini
        /// </summary>
        /// <returns>True if data loaded</returns>
        public static int Load ()
        {
            //TODO remove
            Database.ConnectionString = string.Empty;
            Logging.FileName = string.Empty;

            int loaded = 0;
            if (Database.ConnectionString == string.Empty)
            {
                // need to load data
                try
                {
                    ConfigFile config = new ConfigFile(filename);
                    Database.ConnectionString = config.getValue("connectionString");
                    Logging.FileName = config.getValue("LogFile");
                    loaded = 1;
                }
                catch(Exception e)
                {
                    loaded = -1;
                }
            }
            return loaded;
        }
    }
}