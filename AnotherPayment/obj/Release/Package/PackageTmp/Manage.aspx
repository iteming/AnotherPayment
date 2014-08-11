<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="AnotherPayment.Manage" %>

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
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="Login.aspx">登陆</asp:LinkButton>
        &nbsp;&nbsp;
        <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="AddCoupon.aspx">生成优惠券</asp:LinkButton>
        &nbsp;&nbsp;
        <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="SearchAllPhone.aspx">审批电话号码</asp:LinkButton>
        &nbsp;&nbsp;
        <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="SearchAttach.aspx">发放优惠券</asp:LinkButton>
        
        <%--<asp:LinkButton ID="LinkButton5" runat="server" PostBackUrl="AddTelePhone.aspx">新增电话号码</asp:LinkButton>
        <asp:LinkButton ID="LinkButton6" runat="server" PostBackUrl="AttachUpload.aspx">上传图片</asp:LinkButton>--%>
        </center>
    </div>
    </form>
</body>
</html>
