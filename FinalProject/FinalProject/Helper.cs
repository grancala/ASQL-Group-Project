///FILE : Helper.cs
///PROJECT : ASQL - Group Project
///PROGRAMMER(S) : Nick Whitney, Constantine Grigoriadis, Jim Raithby
///FIRST VERSION : 12/6/2014
///DESCRIPTION : Contains a set of helper functions.
///Used to convert inches to mm, F to C, extract dates and build error messages
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FinalProject
{
    /// <summary>
    /// Helper functions
    /// Converting, Parsing and Building an error message
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Converts Fahrenheit to Celcius
        /// </summary>
        /// <param name="fah">Fahrenheit to convert</param>
        /// <returns>Converted value</returns>
        public static decimal FahrenheitToCelcius(decimal fah)
        {
            return ((fah - 32) * 5.0M / 9.0M);
        }


        /// <summary>
        /// Converts inches to mm
        /// </summary>
        /// <param name="inches">Inches to convert</param>
        /// <returns>Converted value</returns>
        public static decimal InchesToMM(decimal inches)
        {
            return inches / 0.039370M;
        }


        /// <summary>
        /// Attempts to parse a string into a year and a month
        /// </summary>
        /// <param name="input">year and month in format "195001" for 1950, 01</param>
        /// <param name="year">out parameter for year</param>
        /// <param name="month">put parameter for month</param>
        /// <returns>True if successful</returns>
        public static bool ParseYearMonth(string input, out Int16 year, out byte month)
        {
            bool returnVal = true;
            if (!Int16.TryParse(input.Substring(0, 4), out year))
            {
                returnVal = false;
            }
            if (!byte.TryParse(input.Substring(4, 2), out month))
            {
                returnVal = false;
            }
            return returnVal;
        }


        /// <summary>
        /// Recursively builds an error message with each inner error message
        /// </summary>
        /// <param name="e">Exception to log</param>
        /// <param name="message">reference to string builder to put into</param>
        public static void BuildErrorMessage(Exception e, ref StringBuilder message)
        {
            message.AppendLine(e.Message);
            if (e.InnerException != null)
            {
                BuildErrorMessage(e.InnerException, ref message);
            }
        }
    }
}