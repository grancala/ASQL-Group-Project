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
                        "FROM [ASQLGroup].[dbo].[Demo_2_12_2014] " +
                        "WHERE (stateCode='101') " +
                        "AND ( data_year BETWEEN 2001 AND 2013) ";
                        //"Go";        //populate using Data Provided by Jim Change this later
        
        protected void Page_Load(object sender, EventArgs e)
        {
            var googleDataTable = new Bortosky.Google.Visualization.GoogleDataTable(this.ProgrammingTable); //construct a datatable from a google table
            Page.ClientScript.RegisterStartupScript(
                this.GetType(), "vis", string.Format("var Chartdata = {0};", googleDataTable.GetJson()), true);
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

        protected void RegionLookup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Period_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ChartType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void JimsButton_Click(object sender, EventArgs e)
        {

        }


    }
}