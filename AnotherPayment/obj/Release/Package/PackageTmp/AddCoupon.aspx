<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCoupon.aspx.cs" Inherits="AnotherPayment.AddCoupon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <div>
        请输入生成优惠券数量：
        <asp:TextBox ID="txtNun" runat="server" Text="10"></asp:TextBox>
        <asp:Button ID="btnGenerate" runat="server" Text="生成" OnClick="btnGenerate_Click" />
        <asp:Button ID="back" runat="server" OnClick="back_Click" Text="返回" />

    </div>
    <div>
        <asp:ListBox ID="ListBox1" runat="server" Width="416px" Height="300px"></asp:ListBox>
    </div>
    </form>
</body>
</html>
