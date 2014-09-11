<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Capture.ascx.cs" Inherits="BankProject.TellerApplication.SignatureManagement.Capture" %>
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
            ToolTip="Preview" Value="btPreview" CommandName="preview" PostBack="false" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="search" PostBack="false" Enabled="false">
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
        <td class="MyContent">
            <asp:HiddenField ID="txtCustomerIdOld" runat="server" />
            <asp:TextBox ID="txtCustomerId" runat="server" Width="200" /> </td>
        <td class="MyLable" style="color: darkgrey;"><telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" OnAjaxRequest="RadAjaxPanel1_AjaxRequest"><asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label></telerik:RadAjaxPanel></td>
    </tr>
    <tr>
        <td class="MyLable">Signature <span class="Required">(*)</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Signature require !" Display="None" ControlToValidate="txtSignature" ValidationGroup="Commit"></asp:RequiredFieldValidator></td>
        <td class="MyContent">
            <asp:FileUpload ID="txtSignature" runat="server" /></td>
        <td class="MyLable"></td>
    </tr>
    <tr style="display:none;"><!-- Khong the lay duoc full-path nen khong preview duoc -->
        <td class="MyLable" style="vertical-align:top;">
            <asp:Label ID="lblSignaturePreview" runat="server" Text="Signature Preview"></asp:Label> </td>
        <td class="MyContent">
            <asp:HyperLink ID="lnkSignature" runat="server" Target="_blank"><asp:Image ID="imgSignature" runat="server" Width="100" Height="100" /></asp:HyperLink></td>
        <td class="MyLable"></td>
    </tr>
</table>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"><img src="icons/bank/ajax-loader-16x16.gif" />
</telerik:RadAjaxLoadingPanel>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function loadCustomerName() {
            var objCust = $("#<%=txtCustomerId.ClientID%>");
            var objCustOld = $("#<%=txtCustomerIdOld.ClientID%>");
            if (objCust.val() != objCustOld.val()) {
                objCustOld.val(objCust.val());
                //$("#<%=lblCustomerName.ClientID%>").html(objCust.val());
                $find("<%= RadAjaxPanel1.ClientID%>").ajaxRequestWithTarget("<%= RadAjaxPanel1.UniqueID %>", "loadCustomerName");
            }
        }
        function OnClientButtonClicking(sender, args) {
            var button = args.get_item();
            if (button.get_commandName() == "<%=BankProject.Controls.Commands.Commit%>") {
            //do nothing
        }
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Preview%>") {
                window.location = "Default.aspx?tabid=287";
            }
            if (button.get_commandName() == "<%=BankProject.Controls.Commands.Search%>") {
                window.location = "Default.aspx?tabid=286";
            }
        }
        $("#<%=txtCustomerId.ClientID%>")
            .focusout(function () {
                loadCustomerName();
            })
            .keypress(function (event) {
                if (event.which == 13) {
                    loadCustomerName();
                }
            });
        $('#<%=txtSignature.ClientID%>')
            .change(function () {
                var files = this.files; var mediafilename = "";
                //for (var i = 0; i < files.length; i++) { mediafilename = files[i].name; }
                //alert(files[0].name);
                $('#<%=imgSignature.ClientID%>').attr('src', files[0].name);
            });
    </script>
</telerik:RadCodeBlock>