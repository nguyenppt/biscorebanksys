<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhayHanhLC.ascx.cs" Inherits="BankProject.PhayHanhLC" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>
<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        if (button.get_commandName() == "print") {
            var tbLCNo = $("#<%= tbLCNo.ClientID %>").val();
            if (tbLCNo != "" && !clickCalledAfterRadconfirm) {
                args.set_cancel(true);
                lastClickedItem = args.get_item();
                radconfirm("Print Transfer Slip(VAT)?", confirmCallbackFunction1);
                }
            }
        }
    var lastClickedItem = null;
    var clickCalledAfterRadprompt = false;
    var clickCalledAfterRadconfirm = false;
  
    function confirmCallbackFunction1(args) {
        clickCalledAfterRadconfirm = false;
        radconfirm("Print Transfer Slip?", confirmCallbackFunction2);
    }
    function confirmCallbackFunction2(args) {
        clickCalledAfterRadconfirm = false;
        radconfirm("Print Transfer Slip For Buying FCY?", confirmCallbackFunction3);
    }
    function confirmCallbackFunction3(args) {
        clickCalledAfterRadconfirm = true;
        lastClickedItem.click();

        lastClickedItem = null;
    }
</script>
    </telerik:RadCodeBlock>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
          </telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />
<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_ButtonClick"
     OnClientButtonClicking="OnClientButtonClicking">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btPreview" CommandName="Preview">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="search">
        </telerik:RadToolBarButton>
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
<div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 200px; padding: 5px 0 5px 20px;">
                <asp:TextBox ID="tbDepositCode" runat="server" Width="200" AutoPostBack="true" OnTextChanged="tbDepositCode_TextChanged" />
                <i>
                    <asp:Label ID="lblDepositCode" runat="server" /></i></td>
        </tr>
    </table>
</div>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Provision Transfer</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
       <table width="100%" cellpadding="0" cellspacing="0">
           <tr>
               <td class="MyLable">Transactions Type</td>
               <td class="MyContent">ACPV Prov Transfer(TF)</td>
               <td></td>
           </tr>
           <tr>
               <td class="MyLable">LC No. <span class="Required">(*)</span><asp:RequiredFieldValidator 
                runat="server" Display="None"
                ID="RequiredFieldValidator1" 
                ControlToValidate="tbLCNo" 
                        ValidationGroup="Commit"
                InitialValue="" 
                ErrorMessage="LC No. is Required" ForeColor="Red">
            </asp:RequiredFieldValidator></td>
               <td class="MyContent"><asp:TextBox ID="tbLCNo" runat="server" /></td>
               <td></td>
           </tr>
       </table>
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Debit Infomation</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
               <tr>
                   <td class="MyLable">Ordered by</td>
                   <td class="MyContent" style="width:350px;"><telerik:RadComboBox AppendDataBoundItems="True"   
                      
                    ID="rcbOrderedby" Runat="server"
                    MarkFirstMatch="True" Width="350" Height="150px" 
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox></td>
                   <td><asp:Label ID="lblOrderedby" runat="server" /></td>
               </tr>
               <tr>
                   <td class="MyLable">Debit Ref.</td>
                   <td class="MyContent"><asp:TextBox ID="tbDebitRef" runat="server" /></td>
                   <td></td>
               </tr>
                </table>
                 <table width="100%" cellpadding="0" cellspacing="0">
               <tr>
                   <td class="MyLable">Debit Currency</td>
                   <td class="MyContent">
                       <telerik:RadComboBox width="150"
                            ID="rcbDebitCurrency" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="USD" Text="USD" />
                                <telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                                <telerik:RadComboBoxItem Value="GBP" Text="GBP" />
                                <telerik:RadComboBoxItem Value="JPY" Text="JPY" />
                                <telerik:RadComboBoxItem Value="VND" Text="VND" />
                            </Items>
                        </telerik:RadComboBox>
                       </td>
                   <td></td>
               </tr>
                <tr>
                   <td class="MyLable">Debit Account</td>
                   <td class="MyContent" Width="150">
                       <telerik:RadComboBox 
                    ID="rcbDebitAccount" Runat="server" AppendDataBoundItems="true"
                            onclientselectedindexchanged="rcbDebitAccount_OnClientSelectedIndexChanged"
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
                   </td>
                   <td style="text-transform:uppercase;"><asp:Label ID="lblDebitAccount" runat="server" /></td>
               </tr>
                
                <tr>
                   <td class="MyLable">Debit Amout</td>
                   <td class="MyContent"><asp:TextBox ID="tbDebitAmout" runat="server" /></td>
                   <td></td>
               </tr>
                <tr>
                   <td class="MyLable">Debit Date</td>
                   <td class="MyContent"><telerik:RadDatePicker ID="rdpDebitDDate" runat="server"></telerik:RadDatePicker></td>
                   <td></td>
               </tr>
                <tr>
                   <td class="MyLable">Amout Debited</td>
                   <td class="MyContent"></td>
                   <td></td>
               </tr>
           </table>
        </fieldset>
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Credit Infomation</div>
            </legend>
            <asp:UpdatePanel ID="update" runat="server">
                <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Credit Account</td>
                    <td class="MyContent" style="width:150px;">
                        <asp:TextBox ID="tbCreditAccount" runat="server" AutoPostBack="true" OnTextChanged="tbCreditAccount_TextChanged" /></td>
                    <td><asp:Label ID="lblCreditAccount" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Credit Currency</td>
                    <td class="MyContent"><asp:Label ID="lblCreditCurrency" runat="server" /></td>
                     <td></td>
                </tr>
                </table></ContentTemplate>
            </asp:UpdatePanel>
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Treasury Rate</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox incrementsettings-interceptarrowkeys="true" incrementsettings-interceptmousewheel="true"  
                            runat="server" id="tbTreasuryRate"/>
                    </td>
                     <td></td>
                </tr>
                <tr>
                    <td class="MyLable">Credit Amount</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox incrementsettings-interceptarrowkeys="true" incrementsettings-interceptmousewheel="true"  
                            runat="server" id="tbCreditAmount"/></td>
                     <td></td>
                </tr>
                <tr>
                    <td class="MyLable">Credit Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="rdpCreditDate" runat="server"></telerik:RadDatePicker></td>
                     <td></td>
                </tr>
                <tr>
                    <td class="MyLable">Amount Credited</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAuthoriser" runat="server" /></td>
                     <td></td>
                </tr>
                <tr>
                    <td class="MyLable">VAT Serial No</td>
                    <td class="MyContent">
                        <asp:TextBox ID="tbVATSerialNo" runat="server" /></td>
                     <td></td>
                </tr>
            </table>
            <uc1:VVTextBox runat="server" id="tbAddResmarks" VVTLabel="Add Resmarks"  VVTDataKey='tbEssurLCCode' />
        </fieldset>
    </div>

</div>
<asp:HiddenField ID="hdfDisable" runat="server" Value="0" />
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"
    DefaultLoadingPanelID="AjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbCustomerID">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblCustomer" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tbDepositCode">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbDepositCode" />
                <telerik:AjaxUpdatedControl ControlID="tbLCNo" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        $(document).ready(
    function () {
        $('a.add').live('click',
            function () {
                $(this)
                    .html('<img src="Icons/Sigma/Delete_16X16_Standard.png" />')
                    .removeClass('add')
                    .addClass('remove');
                $(this)
                    .closest('tr')
                    .clone()
                    .appendTo($(this).closest('table'));
                $(this)
                    .html('<img src="Icons/Sigma/Add_16X16_Standard.png" />')
                    .removeClass('remove')
                    .addClass('add');
            });
        $('a.remove').live('click',
            function () {
                $(this)
                    .closest('tr')
                    .remove();
            });
        $('input:text').each(
            function () {
                var thisName = $(this).attr('name'),
                    thisRrow = $(this)
                                .closest('tr')
                                .index();
                //$(this).attr('name', 'row' + thisRow + thisName);
                //$(this).attr('id', 'row' + thisRow + thisName);
            });

    });
        function rcbOrderedby_OnClientSelectedIndexChanged(sender, eventArgs) {
            var combo = $find('<%=rcbOrderedby.ClientID %>');
            //sender.set_text("You selected " + item.get_text());
            $('#<%=lblOrderedby.ClientID%>').html(combo.get_selectedItem().get_value());
        }
        function rcbDebitAccount_OnClientSelectedIndexChanged(sender, eventArgs) {
            var combo = $find('<%=rcbDebitAccount.ClientID %>');

            
            $('#<%=lblDebitAccount.ClientID%>').html("TKTT " + combo.get_selectedItem().get_value());
        }
        
        
    </script>
</telerik:RadCodeBlock>
<div style="visibility: hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" />
</div>
