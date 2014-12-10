///FILE : Logging.cs
///PROJECT : ASQL - Group Project
///PROGRAMMER(S) : Nick Whitney, Constantine Grigoriadis, Jim Raithby
///FIRST VERSION : 12/6/2014
///DESCRIPTION : Contains a logging class, it will log errors or success into
///a given filename
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace FinalProject
{
    /// <summary>
    /// Logs to a file specified in the config file
    /// </summary>
    public static class Logging
    {
        public static string FileName = string.Empty;


        /// <summary>
        /// Logs an attempt to insert into the database
        /// </summary>
        /// <param name="UserName">User requesting insert</param>
        /// <param name="iYear">First Year in the batch</param>
        /// <param name="iMonth">First Month in the batch</param>
        /// <param name="fYear">Last Year in the batch</param>
        /// <param name="fMonth">Last Month in the batch</param>
        /// <param name="iTime">Initial time of job</param>
        /// <param name="DataBefore">Data existing before</param>
        /// <param name="Overwrite">Overwrite existing data</param>
        public static void Log(string UserName, Int16 iYear, byte iMonth, Int16 fYear, byte fMonth, DateTime iTime, bool DataBefore = false, bool Overwrite = false)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Database Insertion");
            builder.AppendLine("User: " + UserName);
            builder.AppendLine("Start time: " + GetTime(iTime));
            builder.AppendLine("End time: " + GetNow());
            builder.AppendLine("Start Range: " + iYear.ToString() + " " + iMonth.ToString());
            builder.AppendLine("End Range: " + fYear.ToString() + " " + fMonth.ToString());

            if (DataBefore)
            {
                builder.AppendLine("Data present before loading attempt");
                if (Overwrite)
                {
                    builder.AppendLine("User allowed data to be over written");
                }
                else
                {
                    builder.AppendLine("User did not allow data to be overwritten");
                }
            }
            else
            {
                builder.AppendLine("Data was not present before loading attempt");
            }
            builder.AppendLine();
            builder.AppendLine();

            WriteFile(builder.ToString());
        }


        /// <summary>
        /// Logs an error 
        /// </summary>
        /// <param name="UserName">Username generating error</param>
        /// <param name="error">String of error to be logged</param>
        public static void LogError(string UserName, string error)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("ERROR");
            builder.AppendLine("User: " + UserName);
            builder.AppendLine("Time: " + GetNow());
            builder.AppendLine(error);
            builder.AppendLine();
            builder.AppendLine();

            WriteFile(builder.ToString());
        }


        /// <summary>
        /// Formats a time into a MM/dd/yy HH:mm:ss format
        /// </summary>
        /// <param name="time">Time in UTC</param>
        /// <returns>Formatted date</returns>
        private static string GetTime(DateTime time)
        {
            return string.Format("{0:MM/dd/yy HH:mm:ss} UTC", time);
        }


        /// <summary>
        /// Formats the current time into a MM/dd/yy HH:mm:ss format
        /// </summary>
        /// <returns>Formatted date</returns>
        private static string GetNow()
        {
            return GetTime(DateTime.UtcNow);
        }


        /// <summary>
        /// Writes a string to the log file
        /// </summary>
        /// <param name="toLog">Data to log</param>
        private static void WriteFile(string toLog)
        {
            using (StreamWriter writer = new StreamWriter(FileName, true))
            {
                writer.Write(toLog);
                writer.Flush();
            }
        }
    }
}