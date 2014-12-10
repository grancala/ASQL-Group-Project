using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        static public DataTable ChartTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            var googleDataTable = new Bortosky.Google.Visualization.GoogleDataTable(ChartTable); //construct a datatable from a google table
            Page.ClientScript.RegisterStartupScript(
                this.GetType(), "vis", string.Format("var Chartdata = {0};", googleDataTable.GetJson()), true);
        }
        DataTable LoadChartData(string IP, string Port, string ChartType, 
            string regionCode, 
            int startYear, int StartMonth, int EndYear, int EndMonth)
        {
            DataTable Temp = new DataTable();
            return Temp;
        }
    }
}