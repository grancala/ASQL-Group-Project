<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpLoad.aspx.cs" Inherits="FinalProject.UpLoad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Data</title>
</head>
<body>
    <form id="uploadForm" runat="server">
    <div>
        <div id="User Info">
            <asp:Label ID="LoggedInAs" runat="server" /><br />
            <asp:Label ID="Contents" runat="server" /><br />
            <asp:CheckBox ID="OverWrite" runat="server" Text="Remove old data" /><br />
        </div>
        <br /><br />
        <div id="file Upload">
            <asp:Label ID="filelabel" runat="server" Text="File to Upload" /><br />
            <asp:FileUpload ID="fileUploader" runat="server" AllowMultiple="false" /> 
            <br />
            <asp:Button ID="btnUpload" Text="Upload Now" runat="server" OnClick="Upload_Click" />
        </div>
        <asp:Label id="status" runat="server" />
        <br /><br />
        <br />
        <asp:Button ID="BackToMain" runat="server" OnClick="BackToMain_Click" Text="Back To Main Page" />
        <br />
        <asp:Button ID="visualization" runat="server" OnClick="visualization_Click" Text="Visualize Data" />
    </div>
    </form>
</body>
</html>
