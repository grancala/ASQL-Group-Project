
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="FinalProject.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main Page</title>
</head>
<body>
    <form id="mainForm" runat="server">
    <div>
        <asp:Label ID="mainformlabel" runat="server" Text="Main Page" /><br />
        <asp:Label ID="LoggedInAs" runat="server" /><br />
        <br /><br />
        <asp:Button ID="login" runat="server" OnClick="login_Click" Text="Login" />
        <br />
        <asp:Button ID="manage" runat="server" OnClick="manage_Click" Text="Manage Users" />
        <br />
        <asp:Button ID="upload" runat="server" OnClick="upload_Click" Text="Upload Data" />
        <br />
        <asp:Button ID="visualize" runat="server" OnClick="visualize_Click" Text="Chart Data" />
        <br />
    </div>
        <br /><br />
        <asp:Label ID="Error" runat="server" />
    </form>
</body>
</html>
