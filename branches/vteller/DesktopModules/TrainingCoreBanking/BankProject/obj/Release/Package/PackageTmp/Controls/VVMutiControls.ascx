<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VVMutiControls.ascx.cs" Inherits="BankProject.Controls.VVMutiControls" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:UpdatePanel ID="rcbupdatepanel" runat="server" >
    <ContentTemplate>
         <asp:Table ID="tblMain" runat="server" CellPadding="0" CellSpacing="0">
        </asp:Table>
</ContentTemplate>
</asp:UpdatePanel>