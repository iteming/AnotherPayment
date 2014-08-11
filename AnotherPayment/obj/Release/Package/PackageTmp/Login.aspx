<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AnotherPayment.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
        <table id="Table1" >
            <tr>
                <td>用户名：</td>
                <td>
                    <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnLogin" runat="server" Text="登陆" OnClick="btnLogin_Click" />
                    &nbsp;
                    <asp:Button ID="btnAdmin" runat="server" Text="管理员入口" OnClick="btnAdmin_Click" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </center>
    </div>
    </form>
</body>
</html>
