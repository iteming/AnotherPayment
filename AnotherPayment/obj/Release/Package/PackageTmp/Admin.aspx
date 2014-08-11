<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="AnotherPayment.Admin" %>

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
                <td>管理员帐号：</td>
                <td>
                    <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>管理员密码：</td>
                <td>
                    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnManage" runat="server" Text="管理员登陆" OnClick="btnManage_Click" />
                    
                    <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_Click" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        </center>
    
    </div>
    </form>
</body>
</html>
