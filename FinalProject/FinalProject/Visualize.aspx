<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Visualize.aspx.cs" Inherits="FinalProject.Visualize" %>

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
            var Options = { title: "Data Visualization", hAxis: { title: "Time" }, vAxis: { title: "Units" } };
            chart.draw(data, Options);
        });

        function PrintDiv() {
            var divToPrint = document.getElementById('chart_div');
            var popupWin = window.open('', '_blank', 'width=700,height=300,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
    </script>
</head>
<body>
    <form id="chartingForm" runat="server">
         Select ChartType:       
        <asp:DropDownList ID="ChartType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectedIndexChanged" >
            <asp:ListItem Selected="True" Value="1">Precipitation</asp:ListItem>
            <asp:ListItem Value="2">Cooling and Heating Days</asp:ListItem>
            <asp:ListItem Value="3">Temperatures</asp:ListItem>
         </asp:DropDownList>
        </br>
         Select Region:       
        <asp:DropDownList ID="RegionLookup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectedIndexChanged" > </asp:DropDownList>
         </br>
            <asp:RadioButtonList id="Period" runat="server" Height="88px" Width="218px" AutoPostBack="true" OnSelectedIndexChanged="Period_SelectedIndexChanged" >
            <asp:ListItem Value="1">Monthly</asp:ListItem>
            <asp:ListItem Selected="True" Value="2">Quarterly</asp:ListItem>
            <asp:ListItem Value="3">Annually</asp:ListItem>
         </asp:RadioButtonList>
        <div id ="chart_div" style="width: 700px; height: 300px;"" >
    
        </div>
        <div id ="dashBoard_div" style="width: 700px; height: 300px;"" >
    
        </div>
        <input id="btnprint" type="button" onclick="PrintDiv()" value="Print" />
        <asp:Button ID="Print" Text="Print" OnClientClick="return PrintDiv();" />
    </form>
</body>
</html>
