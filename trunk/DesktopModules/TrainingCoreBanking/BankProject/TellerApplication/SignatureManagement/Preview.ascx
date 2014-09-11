<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Preview.ascx.cs" Inherits="BankProject.TellerApplication.SignatureManagement.Preview" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"  />
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btPreview" CommandName="preview" PostBack="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="search" Enabled="false">
        </telerik:RadToolBarButton>
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print" Enabled="false">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
</div>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td class="MyLable">Customer Id <span class="Required">(*)</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Customer require !" Display="None" ControlToValidate="txtCustomerId" ValidationGroup="Commit"></asp:RequiredFieldValidator></td>
        <td class="MyContent"><asp:HiddenField ID="txtCustomerIdOld" runat="server" /><asp:TextBox ID="txtCustomerId" runat="server" Width="200" /> <span style="color: darkgrey;"><asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label></span></td>
    </tr>
    <tr>
        <td class="MyLable" style="vertical-align:top;">Signature </td>
        <td class="MyContent">
            <asp:HyperLink ID="lnkSignature" runat="server" Target="_blank"><asp:Image ID="imgSignature" runat="server" CssClass="imgSignaturePreview" /></asp:HyperLink></td>
    </tr>
</table>
<script type="text/javascript">
    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Preview%>") {
            var custID = $("#<%=txtCustomerId.ClientID%>").val();
            if (custID == null || custID == '') {
                alert('Customer require !');
                return;
            }
            window.location = "Default.aspx?tabid=287&cid=" + custID;
        }
    }
    function loadInfo() {
        var objCust = $("#<%=txtCustomerId.ClientID%>");
        var objCustOld = $("#<%=txtCustomerIdOld.ClientID%>");
        if (objCust.val() != objCustOld.val()) {
            objCustOld.val(objCust.val());
                $find('<%=RadToolBar1.ClientID%>').findItemByValue("btPreview").click();
            }
    }
    $("#<%=txtCustomerId.ClientID%>")
            .keypress(function (event) {
                if (event.which == 13) {
                    loadInfo();
                }
            });
</script>