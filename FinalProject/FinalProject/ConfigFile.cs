///FILE : Logging.cs
///PROJECT : ASQL - Group Project
///PROGRAMMER(S) : Nick Whitney, Constantine Grigoriadis, Jim Raithby
///FIRST VERSION : 12/6/2014
///DESCRIPTION : Loads a config file into memory and allows users to access given lines
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FinalProject
{
    /// <summary>
    /// Creates a copy of the log file in memory
    /// </summary>
    public class ConfigFile
    {
        List<string> contents;


        /// <summary>
        /// Loads file into memory
        /// </summary>
        /// <param name="fileName">File to load</param>
        public ConfigFile(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");
            if (fileName == "") throw new ArgumentException("parameter cannot be blank", "fileName");

            using (StreamReader reader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read)))
            {
                contents = reader.ReadToEnd().Split('\n').Select(line => line.Trim()).Where(line => line != "").ToList<string>();
            }

        }


        /// <summary>
        /// Gets a value from the config
        /// </summary>
        /// <param name="valueName">Name of line in the config file</param>
        /// <returns></returns>
        public string getValue(string valueName)
        {
            var matches = contents.Where(value => new Regex("^" + valueName + " ?= ?.+", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace).IsMatch(value));
            if (matches.Count() != 1) throw new FormatException("expected 1 match of \"" + valueName + "=<value>\", found " + matches.Count());

            return matches.First().Substring(valueName.Length + 1).Trim();
        }
    }
}