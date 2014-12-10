<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Test.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script type="text/javascript">
        google.load("visualization", "1", { "packages": ["corechart"] });
        google.setOnLoadCallback(function () {
            var data = new google.visualization.DataTable(Chartdata, 0.5);
            var chart = new google.visualization.ColumnChart(document.getElementById("chart_div"));
            chart.draw(data, { title: "Visualization Satisfaction", hAxis: { title: "Programming method" }, vAxis: { title: "Units" } });

            var dash_container = document.getElementById('dashBoard_div'),
            myDashboard = new google.visualization.Dashboard(dash_container);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <div id ="chart_div" style="width: 700px; height: 300px;"" >
    
        </div>
        <div id ="dashBoard_div" style="width: 700px; height: 300px;"" >
    
        </div>
    </form>
</body>
</html>
