<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VVTextBox.ascx.cs" Inherits="BankProject.Controls.VVTextBox" %>
<asp:UpdatePanel ID="tbupdatepanel" runat="server" >
    <ContentTemplate>
         <asp:Table ID="tblMain" runat="server">
            <asp:TableRow>
                <asp:TableCell CssClass="MyLable">
                    <asp:Label ID="lblVVT" runat="server" />
                </asp:TableCell>
                <asp:TableCell CssClass="MyContent" Width="200">
                    <asp:TextBox width="200" ID="tbVVT" runat="server" />
                </asp:TableCell>
                <asp:TableCell>
                    <asp:ImageButton ID="ibtVVT" runat="server" OnClick="ibtVVT_Click" ImageUrl="~/Icons/Sigma/Add_16X16_Standard.png" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
</ContentTemplate>
</asp:UpdatePanel>