<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VVComboBox.ascx.cs" Inherits="BankProject.Controls.VVComboBox" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:UpdatePanel ID="rcbupdatepanel" runat="server" >
    <ContentTemplate>
         <asp:Table ID="tblMain" runat="server">
            <asp:TableRow>
                <asp:TableCell CssClass="MyLable">
                    <asp:Label ID="lblVVT" runat="server" />
                </asp:TableCell>
                <asp:TableCell CssClass="MyContent" ID="tcTop" runat="server">
                    <telerik:RadComboBox AppendDataBoundItems="True"  
                    ID="rcbVVC" Runat="server"
                    MarkFirstMatch="True"  Height="150px" 
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:ImageButton ID="ibtVVT" runat="server" OnClick="ibtVVT_Click" ImageUrl="~/Icons/Sigma/Add_16X16_Standard.png" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
</ContentTemplate>
</asp:UpdatePanel>