<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Amend.ascx.cs" Inherits="BankProject.TellerApplication.SignatureManagement.Amend" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"/>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"  />
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
         OnClientButtonClicking="OnClientButtonClicking" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit" ValidationGroup="Commit">
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
        <td class="MyLable">Customer Id <span class="Required">(*)</span></td>
        <td class="MyContent"><asp:HiddenField ID="txtCustomerIdOld" runat="server" /><asp:TextBox ID="txtCustomerId" runat="server" Width="200" /></td>
        <td class="MyLable" style="color: darkgrey;"><asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td class="MyLable">Signature new <span class="Required">(*)</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="CustomerSignature require !" Display="None" ControlToValidate="txtSignature" ValidationGroup="Commit"></asp:RequiredFieldValidator></td>
        <td class="MyContent"><asp:FileUpload ID="txtSignature" runat="server" /></td>
    </tr>
    <tr>
        <td class="MyLable" style="vertical-align:top;">
            <asp:Label ID="lblSignaturePreview" runat="server" Text="Signature old"></asp:Label> </td>
        <td class="MyContent">
            <asp:HyperLink ID="lnkSignature" runat="server" Target="_blank"><asp:Image ID="imgSignature" runat="server" Width="100" Height="100" /></asp:HyperLink></td>
    </tr>
</table>
<script type="text/javascript">
    function loadInfo() {
        var objCust = $("#<%=txtCustomerId.ClientID%>");
        var objCustOld = $("#<%=txtCustomerIdOld.ClientID%>");
        if (objCust.val() == null || objCust.val() == '') {
            alert('CustomerId require !');
            return;
        }
        if (objCust.val() != objCustOld.val()) {
            objCustOld.val(objCust.val());
            window.location = "Default.aspx?tabid=288&cid=" + objCust.val();
        }
    }
    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Commit%>") {
            //do nothing
        }
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Preview%>") {
            loadInfo();            
        }
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Search%>") {
            window.location = "Default.aspx?tabid=286";
        }
    }
    $("#<%=txtCustomerId.ClientID%>")
        .focusout(function () {
            loadInfo();
        })
        .keypress(function (event) {
            if (event.which == 13) {
                loadInfo();
            }
        });
</script>