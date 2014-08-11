<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchAttach.aspx.cs" Inherits="AnotherPayment.SearchAttach" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%">
        
        <asp:Label ID="lblCouponCode" runat="server" style=" position:absolute; right:10px; top:10px; z-index:1;color:red;"></asp:Label>
        <center>
        <table id="Table1" >
            <tr>
                <td>上传人：</td>
                <td>
                    <asp:TextBox ID="txtUploadUser" runat="server" ></asp:TextBox>
                </td>
                <td>审批类别：</td>
                <td>
                    <asp:DropDownList ID="dllState" runat="server">
                        <asp:ListItem Text="全部" Value="" Selected></asp:ListItem>
                        <asp:ListItem Text="未审批" Value="0"></asp:ListItem>
                        <asp:ListItem Text="已审批" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
                </td>
                <td>
                    <asp:Button ID="back" runat="server" OnClick="back_Click" Text="返回" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblMsg" runat="server" ></asp:Label>
        <asp:GridView ID="gvPhone" runat="server" AutoGenerateColumns="False"
            OnRowUpdating="gvPhone_RowUpdating" Width="500px" AllowPaging="true" PageSize="20" 
            Mode="Numeric" OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="附件">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlFile1" runat="server" NavigateUrl='<%# GetPath(Eval("AttachUrl")) %>' 
                            Text='<%# Eval("AttachName") %>' Target="_blank"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="上传人" ItemStyle-Width="120px"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblUploadUser" runat="server" Text='<%# Eval("UploadUser") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="审批状态" ItemStyle-Width="100px"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# VerifyState(Eval("state")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-Width="110px" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnUpdate" runat="server" Text="发放优惠码" CommandName="Update"
                             Enabled='<%# VerifyEnable(Eval("state")) %>' ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </center>
    </div>
    </form>
</body>
</html>
