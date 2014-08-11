<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachUpload.aspx.cs" Inherits="AnotherPayment.AttachUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script language="javascript">
    var submitcount = 0;
    function submitOnce() {
        if (submitcount == 0) {
            submitcount++;
            return true;
        } else {
            alert("正在操作，请不要重复提交，谢谢！");
            return false;
        }
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
        <table id="Table1" >
            <tr>
                <td>选择图片附件：</td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>
                    <asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click"/>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:HyperLink ID="hlFile1" runat="server" Text="" Target="_blank"></asp:HyperLink>
                </td>
            </tr>
        </table>
        </center>
    </div>
    </form>
</body>
</html>
