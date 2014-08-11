<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchAllPhone.aspx.cs" Inherits="AnotherPayment.SearchAllPhone" %>

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
                <td>手机号：</td>
                <td>
                    <asp:TextBox ID="txtTelePhone" runat="server" MaxLength="11"></asp:TextBox>
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
                    <asp:Button ID="btnAuto" runat="server" OnClick="btnAuto_Click" Text="切换自动审核" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <asp:GridView ID="gvPhone" runat="server" AutoGenerateColumns="False"
            OnRowUpdating="gvPhone_RowUpdating" Width="500px" AllowPaging="true" PageSize="20" 
            Mode="Numeric" PageSetting="Numeric" OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="电话号码">
                    <ItemTemplate>
                        <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="审批状态" ItemStyle-Width="100px"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# VerifyPhone(Eval("state")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建时间" ItemStyle-Width="100px"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Convert.ToString(Eval("createtime")) == "" ? "--" : Convert.ToDateTime(Eval("createtime")).ToString("yyyy-MM-dd") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-Width="100px"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnUpdate" runat="server" Text="审批通过" CommandName="Update" 
                            Enabled='<%# VerifyEnable(Eval("state")) %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </center>
    </div>
    </form>
</body>
</html>
