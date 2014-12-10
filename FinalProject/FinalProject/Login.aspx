<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FinalProject.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
</head>
<body>
    <form id="LoginForm" runat="server">
        <div>
            <asp:Label ID="loginformlabel" runat="server" Text="LOGIN" />
            <br />
            <asp:Label ID="userloginlabel" runat="server">Username</asp:Label>
            <asp:TextBox ID="txtUserLogin" runat="server" />
            <br />
            <asp:Label ID="passloginlabel" runat="server">Password</asp:Label>
            <asp:TextBox ID="txtPassLogin" TextMode="Password" runat="server" />
            <br />
            <asp:Button ID="btnLogin" runat="server" OnClick="Login_Click" Text="LOGIN" />
        </div>
        <br />
        <div>
            <asp:Label ID="registerformlabel" runat="server" Text="REGISTER" />
            <br />
            <asp:Label ID="userregisterlabel" runat="server">Username</asp:Label>
            <asp:TextBox ID="txtUserReg" runat="server" />
            <br />
            <asp:Button ID="Register" runat="server" OnClick="Register_Click" Text="REGISTER" />
        </div>
        <br /><br />
        <asp:Label ID="Error" runat="server" />
    </form>

</body>
</html>
