///FILE : LoadConfig.cs
///PROJECT : ASQL - Group Project
///PROGRAMMER(S) : Nick Whitney, Constantine Grigoriadis, Jim Raithby
///FIRST VERSION : 12/6/2014
///DESCRIPTION : Calls Config file to load data
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
        static string filename = @"config.ini";


        /// <summary>
        /// Loads from config.ini
        /// </summary>
        /// <returns>True if data loaded</returns>
        public static int Load (string serverPath)
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
                    ConfigFile config = new ConfigFile(serverPath + filename);
                    Database.ConnectionString = config.getValue("connectionString");
                    Logging.FileName = serverPath + config.getValue("LogFile");
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