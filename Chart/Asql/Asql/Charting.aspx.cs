using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Asql
{
    public partial class Charting : System.Web.UI.Page
    {
        string username = "Nick";
        string password = "Incorrect";

        string DatabaseConnectionString =  Properties.Resources.dbConnectionString;
        string SQLQUERY = "SELECT top 1000 stateCode,data_year,data_month,PCP " +
                        "FROM [ASQLGroup].[dbo].[Nick_20141209] " +
                        "WHERE (stateCode='101') " +
                        "AND ( data_year BETWEEN 2001 AND 2013) ";
                        //"Go";        //populate using Data Provided by Jim Change this later
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Repopulate("Northeast Region", 3, 1, new DateTime(2001, 1, 1), new DateTime(2013, 1, 1));
            if(!IsPostBack)
            {
                PopulateRegionDropDown();
            };
        }
        
        //A single Table that contains data displayed by the chart
        protected System.Data.DataTable ProgrammingTable
        {
            get // a DataTable filled in any way, for example:
            {
                using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(SQLQUERY, conn);
                    cmd.Connection.Open();
                    DataTable TempTable = new DataTable();
                    TempTable.Load(cmd.ExecuteReader());
                    return TempTable;
                }
            }
        }
        
        private void PopulateRegionDropDown()
        {
            //run sql script to get table
            using (SqlConnection conn = new SqlConnection(DatabaseConnectionString))
            {
                string Query = "EXEC getRegions";
                SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Connection.Open();
                DataTable TempTable = new DataTable();
                TempTable.Load(cmd.ExecuteReader());
                foreach(DataColumn TempColumn in TempTable.Columns)
                {
                    foreach (DataRow row in TempTable.Rows)
                    {
                        string regionCode = row[TempColumn.ColumnName].ToString();
                        RegionLookup.Items.Add(regionCode);
                         //were done with the current entry move to the next row in the column
                    }
                }
            }
            //fill dropDown
        }

        protected void SelectedIndexChanged(object sender, EventArgs e)
        {
            string region = RegionLookup.SelectedValue;
            int period = Period.SelectedIndex;
            int type = ChartType.SelectedIndex;

            //TODO get start and end from slider
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            
            Repopulate(region, type, period, start, end);
        }

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

        private void Repopulate(string region, int type, int period, DateTime start, DateTime end)
        {
            DataTable data = new DataTable();
            using (SqlConnection sqlConn = new SqlConnection(DatabaseConnectionString))
            {
                sqlConn.Open();
                //TODO replace with actual info
                SqlCommand command = new SqlCommand("GenericSearch", sqlConn); 
                command.CommandType = CommandType.StoredProcedure;
                /*command.Parameters.AddWithValue("@userName", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@stateCodeIn", region);
                */
                command.Parameters.AddWithValue("@searchType", type);
                /*command.Parameters.AddWithValue("@timeIncrement", period);
                command.Parameters.AddWithValue("@rangeStart", start.Date);
                command.Parameters.AddWithValue("@rangeEnd", end.Date);
                */
                command.Parameters.Add("@return", SqlDbType.Int);
                command.Parameters["@return"].Direction = ParameterDirection.ReturnValue;
                data.Load(command.ExecuteReader());
                int ok = (int)command.Parameters["@return"].Value;
                if (ok == 0)
                {
                    data = SetUpTable(data, 2, type);
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

        private DataTable SetUpTable (DataTable format, int period, int type)
        {
            DataTable formatted = new DataTable();

            formatted.Columns.Add("Time", typeof(DateTime));
  
            switch(type)
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
                                    Convert.ToByte(row["Quarter"]),
                                    1),
                                    Convert.ToDecimal(row["PCP"]));
                                break;
                            case 2:
                                formatted.Rows.Add(new DateTime(
                                    Convert.ToInt16(row["Year"]),
                                    Convert.ToByte(row["Quarter"]),
                                    1),
                                    Convert.ToInt32(row["CDD"]),
                                    Convert.ToInt32(row["HDD"]));
                                break;
                            case 3:
                                formatted.Rows.Add(new DateTime(
                                    Convert.ToInt16(row["Year"]),
                                    Convert.ToByte(row["Quarter"]),
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
                                    Convert.ToInt16(row["Year"]), 1, 1),
                                    Convert.ToDecimal(row["PCP"]));
                                break;
                            case 2:
                                formatted.Rows.Add(new DateTime(
                                    Convert.ToInt16(row["Year"]), 1, 1),
                                    Convert.ToInt32(row["CDD"]),
                                    Convert.ToInt32(row["HDD"]));
                                break;
                            case 3:
                                formatted.Rows.Add(new DateTime(
                                    Convert.ToInt16(row["Year"]), 1, 1),
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

        protected void JimsButton_Click(object sender, EventArgs e)
        {

        }
    }
}