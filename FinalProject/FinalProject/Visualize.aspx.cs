﻿///FILE : Visualize.cs
///PROJECT : ASQL - Group Project
///PROGRAMMER(S) : Nick Whitney, Constantine Grigoriadis, Jim Raithby
///FIRST VERSION : 12/6/2014
///DESCRIPTION : Allows the user to manipulate a chart of data
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    /// <summary>
    /// Code behind for Visualize.aspx
    /// </summary>
    public partial class Visualize : System.Web.UI.Page
    {
        string username = string.Empty;
        string password = string.Empty;

        string DatabaseConnectionString = string.Empty;
        
        /// <summary>
        /// Ensures the config is loaded and the user is logged in,
        /// sets up selectable regions and initial population of chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            bool logged = false;

            #region common
            // get config
            switch (LoadConfig.Load(Server.MapPath("~/")))
            {
                case -1:
                    // error
                    break;
                case 0:
                    // not needed
                    break;
                case 1:
                    // success
                    break;
            };

            // get login info
            if (Session["username"] != null)
            {
                username = (string)Session["username"];
            }
            if (Session["password"] != null)
            {
                password = (string)Session["password"];
            }
            if (username == "" || password == "")
            {
                // not logged in
                Server.Transfer("Login.aspx", true);
            }
            else
            {
                logged = true;
            }

            #endregion

            if(logged)
            {
                DatabaseConnectionString = Database.ConnectionString;
                if (!IsPostBack)
                {

                    Repopulate(101, 1, 1);
                    PopulateRegionDropDown();
                }
            }
        }

        /// <summary>
        /// Populates the selectable regions
        /// </summary>
        private void PopulateRegionDropDown()
        {
            //run sql script to get table
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("getRegions", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userName", username);
                cmd.Parameters.AddWithValue("@password", password);
                DataTable TempTable = new DataTable();
                TempTable.Load(cmd.ExecuteReader());
                foreach (DataColumn TempColumn in TempTable.Columns)
                {
                    foreach (DataRow row in TempTable.Rows)
                    {
                        string regionCode = row[TempColumn.ColumnName].ToString();
                        RegionLookup.Items.Add(regionCode);
                        //were done with the current entry move to the next row in the column
                    }
                }
                conn.Close();
            }
            //fill dropDown
        }


        /// <summary>
        /// When an index is changed, the chart is re-created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SelectedIndexChanged(object sender, EventArgs e)
        {
            int region = Convert.ToInt32(RegionLookup.SelectedValue);
            int period = Convert.ToInt32(Period.SelectedValue);
            int type = Convert.ToInt32(ChartType.SelectedValue);

            Repopulate(region, type, period);
        }

        /// <summary>
        /// Calls SelectedIndexChanged()
        /// Will change the timescale on the time slider (to be done later)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Period_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged(sender, e);
            int period = Period.SelectedIndex;
            switch (period)
            {
                case 1: // annual

                    break;
                case 2: // quarterly

                    break;
                case 3: // monthly

                    break;
            }
        }


        /// <summary>
        /// Repopulates the chart given the region, type and period
        /// </summary>
        /// <param name="region">Region code</param>
        /// <param name="type">Type of chart to make</param>
        /// <param name="period">Monthly, Quarterly, Annually</param>
        private void Repopulate(int region, int type, int period)
        {
            DataTable data = new DataTable();
            using (SqlConnection sqlConn = new SqlConnection(DatabaseConnectionString))
            {
                sqlConn.Open();
                //TODO replace with actual info
                SqlCommand command = new SqlCommand("GenericSearch", sqlConn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userName", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@stateCodeIn", region);
                command.Parameters.AddWithValue("@searchType", type);
                command.Parameters.AddWithValue("@timeIncrement", period);
                command.Parameters.Add("@return", SqlDbType.Int);
                command.Parameters["@return"].Direction = ParameterDirection.ReturnValue;
                data.Load(command.ExecuteReader());
                int ok = (int)command.Parameters["@return"].Value;
                if (ok == 0)
                {
                    data = SetUpTable(data, period, type);
                    var googleDataTable = new Bortosky.Google.Visualization.GoogleDataTable(data);
                    //construct a datatable from a google table
                    Page.ClientScript.RegisterStartupScript(
                        this.GetType(), "vis", string.Format("var Chartdata = {0};", googleDataTable.GetJson()), true);
                }
                else
                {
                    // error
                }
                sqlConn.Close();
            }
        }

        /// <summary>
        /// Formats a data table to have a date time field to sort on
        /// </summary>
        /// <param name="format">table to format</param>
        /// <param name="period">Monthly, quarterly, annually</param>
        /// <param name="type">Type of chart</param>
        /// <returns></returns>
        private DataTable SetUpTable(DataTable format, int period, int type)
        {
            DataTable formatted = new DataTable();

            formatted.Columns.Add("Time", typeof(DateTime));

            switch (type)
            {
                case 1:
                    formatted.Columns.Add("PCP", typeof(decimal));
                    break;
                case 2:
                    formatted.Columns.Add("CDD", typeof(int));
                    formatted.Columns.Add("HDD", typeof(int));
                    break;
                case 3:
                    formatted.Columns.Add("TMax", typeof(decimal));
                    formatted.Columns.Add("TMin", typeof(decimal));
                    formatted.Columns.Add("TAvg", typeof(decimal));
                    break;
            }

            switch (period)
            {
                case 1: // monthly
                    // 0 - Year (int16)
                    // 1 - Month (byte)
                    // 2 - PCP (decimal), CDD (int), TMax (decimal)
                    // 3 - HDD (int), TMin (decimal)
                    // 4 - TAvg (decimal)
                    foreach (DataRow row in format.Rows)
                    {
                        switch (type)
                        {
                            case 1:
                                formatted.Rows.Add(new DateTime(
                                    Convert.ToInt16(row["Year"]),
                                    Convert.ToByte(row["Month"]),
                                    1),
                                    Convert.ToInt32(row["PCP"]));
                                break;
                            case 2:
                                formatted.Rows.Add(new DateTime(
                                    Convert.ToInt16(row["Year"]),
                                    Convert.ToByte(row["Month"]),
                                    1),
                                    Convert.ToInt32(row["CDD"]),
                                    Convert.ToInt32(row["HDD"]));
                                break;
                            case 3:
                                formatted.Rows.Add(new DateTime(
                                    Convert.ToInt16(row["Year"]),
                                    Convert.ToByte(row["Month"]),
                                    1),
                                    Convert.ToDecimal(row["TMax"]),
                                    Convert.ToDecimal(row["TMin"]),
                                    Convert.ToDecimal(row["TAvg"]));
                                break;
                        }
                    }
                    break;
                case 2: // quarter
                    // 0 - Year
                    // 1 - Quarter (int)
                    // 2 - PCP, CDD, TMax
                    // 3 - HDD, TMin
                    // 4 - TAvg
                    foreach (DataRow row in format.Rows)
                    {
                        switch (type)
                        {
                            case 1:
                                formatted.Rows.Add(new DateTime(
                                    Convert.ToInt16(row["Year"]),
                                    (Convert.ToByte(row["Quarter"]) * 3 - 2),
                                    1),
                                    Convert.ToDecimal(row["PCP"]));
                                break;
                            case 2:
                                formatted.Rows.Add(new DateTime(
                                    Convert.ToInt16(row["Year"]),
                                    (Convert.ToByte(row["Quarter"]) * 3 - 2),
                                    1),
                                    Convert.ToInt32(row["CDD"]),
                                    Convert.ToInt32(row["HDD"]));
                                break;
                            case 3:
                                formatted.Rows.Add(new DateTime(
                                    Convert.ToInt16(row["Year"]),
                                    (Convert.ToByte(row["Quarter"]) * 3 - 2),
                                    1),
                                    Convert.ToDecimal(row["TMax"]),
                                    Convert.ToDecimal(row["TMin"]),
                                    Convert.ToDecimal(row["TAvg"]));
                                break;
                        }
                    }
                    break;
                case 3: // annual
                    // 0 - Year
                    // 1 - PCP, CDD, TMax
                    // 2 - HDD, TMin
                    // 3 - TAvg
                    foreach (DataRow row in format.Rows)
                    {
                        switch (type)
                        {
                            case 1:
                                formatted.Rows.Add(new DateTime(
                                    Convert.ToInt16(row["Year"]),
                                    1,
                                    1),
                                    Convert.ToDecimal(row["PCP"]));
                                break;
                            case 2:
                                formatted.Rows.Add(new DateTime(
                                    Convert.ToInt16(row["Year"]),
                                    1,
                                    1),
                                    Convert.ToInt32(row["CDD"]),
                                    Convert.ToInt32(row["HDD"]));
                                break;
                            case 3:
                                formatted.Rows.Add(new DateTime(
                                    Convert.ToInt16(row["Year"]),
                                    1,
                                    1),
                                    Convert.ToDecimal(row["TMax"]),
                                    Convert.ToDecimal(row["TMin"]),
                                    Convert.ToDecimal(row["TAvg"]));
                                break;
                        }
                    }
                    break;
            }

            return formatted;
        }


        /// <summary>
        /// Redirects to main
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BackToMain_Click(object sender, EventArgs e)
        {
            Server.Transfer("Main.aspx");
        }
    }
}