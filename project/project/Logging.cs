using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    public static class Logging
    {
        private static string fileName = "log.txt";

        public static void Log()
        {

        }

        public static void LogError(string error)
        {
            StringBuilder logData = new StringBuilder();
            logData.Append(GetNow());
        }

        private static string GetNow()
        {
            return string.Format("{0:MM/dd/yy HH:mm:ss} UTC", DateTime.UtcNow);
        }

        private static void WriteFile(string toLog)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(toLog);
                writer.Flush();
            }
        }
    }
}
