﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuyTravellersCheque.ascx.cs" Inherits="BankProject.Views.TellerApplication.ForeignExchange.BuyTravellersCheque" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit"  />
<style>
    .labelDisabled {
        color: darkgray !important;
    }
</style>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
         OnClientButtonClicking="OnClientButtonClicking" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
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
            ToolTip="Search" Value="btSearch" CommandName="search" PostBack="false">
        </telerik:RadToolBarButton>
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print" Enabled="false">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
</div>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtId" runat="server" Width="200" /><asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="txtId"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="TTNo is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Buy Travellers Cheques</a></li>
        <!--<li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>-->
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer Name <span class="Required">(*)</span><asp:HiddenField ID="tbCustomerNameOld" runat="server" /></td>
                <td class="MyContent"><telerik:RadTextBox ID="tbCustomerName" runat="server" Width="410"></telerik:RadTextBox></td>
                <td>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldtbCustomerName"
                        ControlToValidate="tbCustomerName"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Customer Name is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Address <span class="Required">(*)</span></td>
                <td class="MyContent"><telerik:RadTextBox ID="tbAddress" runat="server" Width="410"></telerik:RadTextBox></td>
                <td>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldtbAddress"
                        ControlToValidate="tbAddress"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Address is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Passport No. <span class="Required">(*)</span></td>
                <td class="MyContent"><telerik:RadTextBox ID="tbPassportNo" runat="server" Width="160"></telerik:RadTextBox></td>
                <td>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldtbPassportNo"
                        ControlToValidate="tbPassportNo"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Passport No. is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Date of isssue</td>
                <td class="MyContent" style="width:160px;"><telerik:RadDatePicker ID="tbDateOfIsssue" runat="server" Width="140"></telerik:RadDatePicker></td>
                <td><table><tr>
                    <td class="MyLable" style="width:80px;">Place of Iss</td>
                    <td class="MyContent"><telerik:RadTextBox ID="tbPlaceOfIss" runat="server" Width="160"></telerik:RadTextBox></td>
                </tr></table></td>
            </tr>
            <tr>
                <td class="MyLable">Phone No.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtPhoneNo" runat="server"></telerik:RadTextBox>
                   <%-- <telerik:RadMaskedTextBox ID="txtPhoneNo" runat="server" Mask="(###)###-####" 
                    EmptyMessage="-- Enter Phone Number --" HideOnBlur="true" ZeroPadNumericRanges="true" 
                    DisplayMask="(###)###-####"></telerik:RadMaskedTextBox>--%>

                </td>
                <td></td>
            </tr>
        </table>
        <hr />
         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Teller ID <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldtxtTellerId"
                        ControlToValidate="txtTellerId"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Teller ID is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtTellerId"
                        runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>
        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">TC Currency
                <span class="Required">(*)</span>
                    </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbTCCurrency"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        AutoPostBack="false"
                        runat="server"
                        OnClientSelectedIndexChanged="cmbTCCurrency_OnClientSelectedIndexChanged">
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
                <td><asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldcmbTCCurrency"
                        ControlToValidate="cmbTCCurrency"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="TC Currency is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Dr Account</td>
                <td class="MyContent" >
                    <telerik:RadComboBox ID="rcbDrAccount"                        
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" >                       
                    </telerik:RadComboBox> <asp:Label ID="lbDrAccount" runat="server"></asp:Label></td>             
            </tr>
        </table>
         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">TC Amount <span class="Required">(*)</span></td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="tbTCAmount" runat="server" ClientEvents-OnValueChanged ="tbTCAmount_OnValueChanged" />
                </td>
                <td>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldtbTCAmount"
                        ControlToValidate="tbTCAmount"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="TC Amount is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
         <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
               <td class="MyLable">Currency Paid <span class="Required">(*)</span>
                    </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbCurrencyPaid"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" Enabled="true" >
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
                <td>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldrcbCurrencyPaid"
                        ControlToValidate="rcbCurrencyPaid"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Currency Paid is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>

         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Teller ID</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbTellerIDCR"
                        runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>
         <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
                <td class="MyLable">Cr Account</td>
                <td class="MyContent" style="width:160px">
                    <telerik:RadComboBox ID="rcbCrAccount"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        Width="160"
                        runat="server" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="10001" Text="USD-10001-0184-184" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            <td><asp:Label ID="lbCrAccount" runat="server"></asp:Label></td>
            </tr>
        </table>

              <table width="100%" cellpadding="0" cellspacing="0">
            
            <tr>
                <td class="MyLable">Exchange Rate</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="tbExchangeRate" runat="server" />
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Charge Amt LCY</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="tbChargeAmtLCY" runat="server" />
                </td>
            </tr>
        </table>

         <table width="100%" cellpadding="0" cellspacing="0">
            <tr class="labelDisabled">
                <td class="MyLable">Charge Amt FCY</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="tbChargeAmtFCY" ReadOnly="true" runat="server" />
                </td>
            </tr>
        </table>
              <table width="100%" cellpadding="0" cellspacing="0">
            <tr class="labelDisabled">
                <td class="MyLable">Amount Paid</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="tbAmountPaid" ReadOnly="true" runat="server" />
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Narrative</td>
                <td class="MyContent" style="width:350px; ">
                    <telerik:RadTextBox ID="txtNarrative" Width="350"
                        runat="server"  />
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
        </table>

        <fieldset id="fDenominations" runat="server">
         <legend>
              <div style="font-weight:bold; text-transform:uppercase;"><asp:Label ID="titleAmend_Confirm" runat="server" Text="Denominations/Serial Numbers"></asp:Label></div>
                                </legend>

        <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
                <td class="MyLable" style="width:135px;">TC Issuer <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldrcbTCIssuer"
                        ControlToValidate="rcbTCIssuer"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="TC Issuer is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbTCIssuer"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" Width="160" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="0" Text="Amex" />
                            <telerik:RadComboBoxItem Value="1" Text="CITI CORP" />
                            <telerik:RadComboBoxItem Value="2" Text="MASTER CARD" />
                            <telerik:RadComboBoxItem Value="3" Text="THOMAS COOK" />
                            <telerik:RadComboBoxItem Value="4" Text="VISA" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

 <table cellpadding="0" cellspacing="0">
     <tr>
         <td class="MyLable" style="width:135px;">Denomination <span class="Required">(*)</span></td>
         <td class="MyContent">
             <telerik:RadComboBox ID="rcbDenomination"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" Width="160" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="0" Text="USDTC100" />
                        </Items>
                    </telerik:RadComboBox>

             <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldrcbDenomination"
                        ControlToValidate="rcbDenomination"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Denomination is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
         </td>
         <td class="MyLable">Unit <span class="Required">(*)</span></td>
         <td class="MyContent">
             <telerik:RadNumericTextBox ID="rcbUnit" runat="server"> </telerik:RadNumericTextBox>
             <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldrcbUnit"
                        ControlToValidate="rcbUnit"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Unit is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
         </td>
         <td class="MyLable">Serial No. <span class="Required">(*)</span></td>
         <td class="MyContent">
             <telerik:RadTextBox ID="rcbSerialNo" runat="server" > </telerik:RadTextBox>
             <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldrcbSerialNo"
                        ControlToValidate="rcbSerialNo"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Serial No. is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
         </td>
</table>    
            </fieldset>
    </div>


    <div id="dvAudit" runat="server">
         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Override</td>
                <td class="MyContent">CREDIT TILL CLOSING BALANCE</td>
            </tr>
                 <tr>
                <td class="MyLable">Override</td>
                <td class="MyContent">Unauthorised overdraft of USD 5028 on account 050001688331</td>
            </tr>
                <tr>
                <td class="MyLable">Record Status</td>
                <td class="MyContent">INAU INPUT Unauthorised</td>
            </tr>
                <tr>
                <td class="MyLable">Current Number</td>
                <td class="MyContent">1</td>
            </tr>
                <tr>
                <td class="MyLable">Inputter</td>
                <td class="MyContent">23_ID0296_I_INAU</td>
            </tr>
              <tr>
                <td class="MyLable">Date Time</td>
                <td class="MyContent">05 JUL 14 16:34</td>
            </tr>

            <tr>
                <td class="MyLable">Authoriser</td>
                <td class="MyContent"></td>
            </tr>
            <tr>
                <td class="MyLable">Company Code</td>
                <td class="MyContent">VN-001-2691</td>
            </tr>
             <tr>
                <td class="MyLable">Department Code</td>
                <td class="MyContent">1</td>
            </tr>
             <tr>
                <td class="MyLable">Auditor Code</td>
                <td class="MyContent"></td>
            </tr>
             <tr>
                <td class="MyLable">Audit Date Time</td>
                <td class="MyContent"></td>
            </tr>
        </table>
    </div>
</div>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">
    $(document).ready(
    function () {
        $('a.add').live('click',
            function () {
                $(this)
                    .html('<img src="Icons/Sigma/Delete_16X16_Standard.png" />')
                    .removeClass('add')
                    .addClass('remove')
                ;
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
                $(this).attr('name', 'row' + thisRow + thisName);
                $(this).attr('id', 'row' + thisRow + thisName);
            });

    });

    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        var ttNo = $('#<%= txtId.ClientID%>').val();
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Preview%>") {
            window.location = '<%=EditUrl("list")%>&lst=4appr';
        }
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Search%>") {
            window.location = '<%=EditUrl("list")%>';
        }

    }

    function cmbTCCurrency_OnClientSelectedIndexChanged(sender, eventArgs) {
        var objCurPaid = $find("<%= rcbCurrencyPaid.ClientID%>");
        var curValue = $find("<%= cmbTCCurrency.ClientID%>").get_text();
        if (objCurPaid.get_text() == '') {
            objCurPaid.set_text(curValue);
        }
        if (curValue != '') {
            tbTCAmount_OnValueChanged();
        }
    }

    function tbTCAmount_OnValueChanged() {
        var TCAmount = Number($find("<%= tbTCAmount.ClientID%>").get_value());
        var ChargeAmtFCY = TCAmount * 0.01;
        $find("<%= tbChargeAmtFCY.ClientID%>").set_value(ChargeAmtFCY);
        var exchangeRate = Number($find("<%= cmbTCCurrency.ClientID%>").get_value().split('#')[1]);
        $find("<%= tbChargeAmtLCY.ClientID%>").set_value(ChargeAmtFCY * exchangeRate);
        $find("<%= tbAmountPaid.ClientID%>").set_value(TCAmount - ChargeAmtFCY);
    }
  </script>
</telerik:RadCodeBlock>