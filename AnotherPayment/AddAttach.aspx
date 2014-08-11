<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAttach.aspx.cs" Inherits="AnotherPayment.AddAttach" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1
        {
            width: 71px;
        }
    </style>
    
    <script type="text/javascript">
        function back() {
            window.location.href = "Login.aspx";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
        <table style="width: 548px">
            <tr>
                <td class="auto-style1">
                    优惠券：
                </td>
                <td>
                    <asp:Label ID="lblCoupon" runat="server" Text="等待管理员审核，审核完毕，可以在此提取优惠码！"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnUpload" runat="server" Text="上传图片" OnClick="btnUpload_Click" />
                    <asp:Button ID="btnNext" runat="server" Text="下一步" OnClick="btnNext_Click"></asp:Button>
                    <input id="Back" name="Back" type="button" value="返回首页" onclick="back()" />
                </td>
            </tr>
        </table>
    </center>
    </div>
    </form>
</body>
</html>
