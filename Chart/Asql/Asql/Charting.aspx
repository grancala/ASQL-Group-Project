<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Charting.aspx.cs" Inherits="Asql.Charting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://www.google.com/jsapi"></script>

    <script type="text/javascript">
        google.load("visualization", "1", { "packages": ["corechart"] });
        google.setOnLoadCallback(function () {
            var data = new google.visualization.DataTable(Chartdata, 0.5);
            var chart = new google.visualization.LineChart(document.getElementById("chart_div"));
            var Options = { title: "Visualization Satisfaction", hAxis: { title: "Programming method" }, vAxis: { title: "Units" } };
            chart.draw(data, Options);
            var dash_container = document.getElementById('dashBoard_div'),
            myDashboard = new google.visualization.Dashboard(dash_container);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
         Select ChartType:       
        <asp:DropDownList ID="ChartType" runat="server">
            <asp:ListItem Selected="True" Value="1">Precipitation</asp:ListItem>
            <asp:ListItem Value="1">Temperature</asp:ListItem>
            <asp:ListItem Value="2">Averages</asp:ListItem>
         </asp:DropDownList>
        </br>
         Select Region:       
        <asp:DropDownList ID="RegionLookup" runat="server"> </asp:DropDownList>
         </br>
            <asp:RadioButtonList id="RadioButtonList1" runat="server" Height="88px" Width="218px">
            <asp:ListItem>Annually</asp:ListItem>
            <asp:ListItem>Quarterly</asp:ListItem>
            <asp:ListItem>Monthly</asp:ListItem>
         </asp:RadioButtonList>
        <div id ="chart_div" style="width: 700px; height: 300px;"" >
    
        </div>
        <div id ="dashBoard_div" style="width: 700px; height: 300px;"" >
    
        </div>
    </form>
</body>
</html>
