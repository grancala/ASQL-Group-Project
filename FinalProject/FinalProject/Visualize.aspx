﻿<!--FILE : Visualize.aspx
    PROJECT : ASQL - Group Project
    PROGRAMMER(S) : Nick Whitney, Constantine Grigoriadis, Jim Raithby
    FIRST VERSION : 12/6/2014
    DESCRIPTION : Allows the user to interact with the charted data -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Visualize.aspx.cs" Inherits="FinalProject.Visualize" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://www.google.com/jsapi"></script>

    <script type="text/javascript">
        google.load("visualization", "1", { "packages": ["corechart", "controls"] }); 
        google.setOnLoadCallback(function () {
            var data = new google.visualization.DataTable(Chartdata, 0.5);
            var chart = new google.visualization.LineChart(document.getElementById("chart_div"));
            //var Options = { title: "Data Visualization", hAxis: { title: "Time" }, vAxis: { title: "Units" } };
            //chart.draw(data, Options);
            

            // Create a dashboard.
            var dash_container = document.getElementById('dashboard_div'),
              myDashboard = new google.visualization.Dashboard(dash_container);

            // Create a date range slider
            var myDateSlider = new google.visualization.ControlWrapper({
                'controlType': 'DateRangeFilter',
                'containerId': 'control_div',
                'options': {
                    'filterColumnLabel': 'Time'
                }
            });

            // Line chart visualization
            var myLine = new google.visualization.ChartWrapper({
                'chartType': 'LineChart',
                'containerId': 'chart_div',
            });

            // Bind myLine to the dashboard, and to the controls
            // this will make sure our line chart is update when our date changes
            myDashboard.bind(myDateSlider, myLine);

            myDashboard.draw(data);


            //TODO link to "help" with chart range filter
            // https://blog.smalldo.gs/2013/04/google-chart-tools-walkthrough-part-3/
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
        <input id="btnprint" type="button" onclick="PrintDiv()" value="Print" />
        <asp:Button ID="BackToMain" runat="server" OnClick="BackToMain_Click" text="Back To Main" />
        <br /><br />
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

        <div id ="dashboard_div" >
            <div id ="chart_div" style="width: 700px; height: 300px;"" ></div>
            <div id="control_div" ></div>
        </div>
    </form>
</body>
</html>
