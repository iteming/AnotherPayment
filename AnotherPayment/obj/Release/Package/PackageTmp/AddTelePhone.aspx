<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTelePhone.aspx.cs" Inherits="AnotherPayment.AddTelePhone" %>

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
                <td>请输入手机号：</td>
                <td>
                    <asp:TextBox ID="txtTelePhone" runat="server" MaxLength="11" 
                        onKeyPress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;" ></asp:TextBox>
                    <asp:Button ID="btnSubmit" runat="server" Text="提交" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnNext" runat="server" Text="下一步" OnClick="btnNext_Click"></asp:Button>
                </td>
            </tr>
        </table>
        
        <asp:Label ID="lblMsg" runat="server" ></asp:Label>
            <br /><br /><br />
        <span style="font-size:26px; font-weight:bold;color:red" >
            请注意！
            提交的手机号码必须没有注册过异度支付，
            否则审核不会通过！！！
            <br />
            （审核之前不可以注册异度支付）
        </span>
        </center>
    </div>
    </form>
</body>
</html>
