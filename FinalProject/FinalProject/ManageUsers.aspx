<!--FILE : ManageUsers.aspx
    PROJECT : ASQL - Group Project
    PROGRAMMER(S) : Nick Whitney, Constantine Grigoriadis, Jim Raithby
    FIRST VERSION : 12/6/2014
    DESCRIPTION : Allows the user to manipulate user accounts -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="FinalProject.ManageUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Users</title>
</head>
<body>
    <form id="RegisterForm" runat="server">
        <div>
            <asp:Label ID="registerformlabel" runat="server" Text="REGISTER" />
            <br />
            <asp:Label ID="userregisterlabel" runat="server">Username</asp:Label>
            <asp:TextBox ID="txtUserReg" runat="server" />
            <br />
            <asp:Button ID="Register" runat="server" OnClick="Register_Click" Text="REGISTER" />
        </div>
        <br />
        <div>
            <asp:Label ID="updateformlabel" runat="server" Text="UPDATE" />
            <br />
            <asp:Label ID="userupdatelabel" runat="server">Username</asp:Label>
            <asp:TextBox ID="txtUserUpdate" runat="server" />
            <br />
            <asp:Label ID="oldupdatelabel" runat="server">Old Password</asp:Label>
            <asp:TextBox ID="txtOldUpdate" TextMode="Password" runat="server" />
            <br />
            <asp:Label ID="newupdatelabel" runat="server">New Password</asp:Label>
            <asp:TextBox ID="txtNewUpdate" TextMode="Password" runat="server" />
            <br />
            <asp:Button ID="Update" runat="server" OnClick="Update_Click" Text="UPDATE" />
        </div>
        <br />
        <div>
            <asp:Label ID="removeformlabel" runat="server" Text="REMOVE" />
            <br />
            <asp:Label ID="userremovelabel" runat="server">Username</asp:Label>
            <asp:TextBox ID="txtUserRem" runat="server" />
            <br />
            <asp:Label ID="passremovellabel" runat="server">Password</asp:Label>
            <asp:TextBox ID="txtPassRem" TextMode="Password" runat="server" />
            <br />
            <asp:Button ID="Remove" runat="server" OnClick="Remove_Click" Text="REMOVE" />

        </div>
        <br /><br />
        <asp:Label ID="Error" runat="server" />
    </form>
</body>
</html>
