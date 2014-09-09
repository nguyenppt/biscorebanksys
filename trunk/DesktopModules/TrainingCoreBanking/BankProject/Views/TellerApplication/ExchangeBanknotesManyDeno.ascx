<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExchangeBanknotesManyDeno.ascx.cs" Inherits="BankProject.Views.TellerApplication.ExchangeBanknotesManyDeno" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Controls/VVComboBox.ascx" TagPrefix="uc1" TagName="VVComboBox" %>
<%@ Register Src="../../Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>


<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit"  />
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });

</script>
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
          OnButtonClick="RadToolBar1_ButtonClick">
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
</div>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtId" runat="server" Width="200" />
        </td>
    </tr>
</table>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Foreign Exchange</a></li>
        <li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyContent">
                <uc1:VVTextBox runat="server" id="txtCustomerName" VVTLabel="Customer Name" VVTDataKey='txtId'  />
                </td>
               
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyContent">
                <uc1:VVTextBox runat="server" id="txtAddress" VVTLabel="Address" VVTDataKey='txtId'  />

                </td>
            </tr>
        </table>
          <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Phone No.</td>
                <td class="MyContent"><telerik:RadMaskedTextBox ID="txtPhoneNo" runat="server" Mask="(###)###-####" 
    EmptyMessage="-- Enter Phone Number --" HideOnBlur="true" ZeroPadNumericRanges="true" DisplayMask="(###)###-####">
</telerik:RadMaskedTextBox></td>
            </tr>
        </table>
       <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Currency bought
                    </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbCurrencyBought"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        OnClientSelectedIndexChanged="OnCurrencyMatch"
                        runat="server" >
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
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Teller 1</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtTeller1"
                        runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
                <td class="MyLable">Debit Account</td>
                <td class="MyContent" style="width:160px;" >
                    <telerik:RadComboBox ID="rcbDebitAccount"
                        OnClientSelectedIndexChanged="OnCurrencyMatch"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" Width="160" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="USD" Text="USD-10001-2054-2861" />
                            <telerik:RadComboBoxItem Value="EUR" Text="EUR-10001-2054-2861" />
                            <telerik:RadComboBoxItem Value="GBP" Text="GBP-10001-2054-2861" />
                            <telerik:RadComboBoxItem Value="JPY" Text="JPY-10001-2054-2861" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND-10001-2054-2861" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            <td><asp:Label ID="lbDrAccount" runat="server"></asp:Label></td>
            </tr>
        </table>

           <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Amount</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtAmount" runat="server" ClientEvents-OnValueChanged="OnAmountValueChanged" NumberFormat-DecimalDigits="2" ></telerik:RadNumericTextBox>
                </td>
            </tr>
        </table>

 <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Narrative</td>
                <td class="MyContent" style="width:200px; ">
                    <telerik:RadTextBox ID="txtNarrative" Width="200"
                        runat="server"  />
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
        </table>
          <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Value Date</td>
                <td class="MyContent"><telerik:RadDatePicker ID="txtValueDate" runat="server" Width="160"></telerik:RadDatePicker></td>
            </tr>
        </table>
        <hr />

                <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Teller 2</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtTeller2" runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>

         <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
                <td class="MyLable">Credit Account</td>
                <td class="MyContent" style="width:160px;" >
                    <telerik:RadComboBox ID="rcbCreditAccount"
                        OnClientSelectedIndexChanged="OnAmountValueChanged"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" Width="160" >
                           <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="USD" Text="USD-10001-2054-2861" />
                            <telerik:RadComboBoxItem Value="EUR" Text="EUR-10001-2054-2861" />
                            <telerik:RadComboBoxItem Value="GBP" Text="GBP-10001-2054-2861" />
                            <telerik:RadComboBoxItem Value="JPY" Text="JPY-10001-2054-2861" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND-10001-2054-2861" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            <td><asp:Label ID="lbCreditAccount" runat="server"></asp:Label></td>
            </tr>
        </table>

           <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Exchange Rate</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtExchangeRate" runat="server" ClientEvents-OnValueChanged="OnAmountValueChanged" NumberFormat-DecimalDigits="0" ></telerik:RadNumericTextBox>
                </td>
            </tr>
        </table>

                   <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Amt Pay Cust</td>
                <td class="MyContent">
                    <asp:Label ID="txtAmtPayCust" runat="server" ></asp:Label>
                </td>
            </tr>
        </table>

    </div>

     <fieldset id="fDenominations" runat="server">
         <legend>
              <div style="font-weight:bold; text-transform:uppercase;"><asp:Label ID="titleAmend_Confirm" runat="server" Text="USD Denomination"></asp:Label></div>
                                </legend>

 <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
                <td class="MyLable">Denomination (Y/N)
                </td>
                <td class="MyContent">
                   <telerik:RadTextBox ID="txtDenomination" runat="server" Width="50px" Text="Y"></telerik:RadTextBox>
                </td>
            </tr>
        </table>

 <table width="100%" cellpadding="0" cellspacing="0">
     <tr>
         <td class="MyLable" style="width:10%;">Denomination</td>
         <td class="MyLable" style="width:15%;">Rate</td>
         <td class="MyLable" style="width:10%;">Unit</td>
         <td class="MyLable" style="width:10%;">Amt Lcy</td>
         <td class="MyLable" style="width:55%;"></td>
     </tr>
     <tr>
         <td class="MyContent">
             USD100
         </td>
         <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtRate100" Width="80%" runat="server" ClientEvents-OnValueChanged="OnUSD100Changed" NumberFormat-DecimalDigits="0" ></telerik:RadNumericTextBox>
         </td>
         <td class="MyContent">
             <telerik:RadNumericTextBox ID="txtUnit100" runat="server" Width="50%" ClientEvents-OnValueChanged="OnUSD100Changed" NumberFormat-DecimalDigits="0" > </telerik:RadNumericTextBox>
             
         </td>
         <td class="MyContent">
             <asp:Label ID="lbAmtLcy100" runat="server"></asp:Label>
         </td>
     </tr>
     <tr>
         <td class="MyContent">
             USD50
         </td>
         <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtRate50" Width="80%" runat="server" ClientEvents-OnValueChanged="OnUSD50Changed" NumberFormat-DecimalDigits="0" ></telerik:RadNumericTextBox>
         </td>
         <td class="MyContent">
             <telerik:RadNumericTextBox ID="txtUnit50" runat="server" Width="50%" ClientEvents-OnValueChanged="OnUSD50Changed" NumberFormat-DecimalDigits="0" > </telerik:RadNumericTextBox>
             
         </td>
         <td class="MyContent">
             <asp:Label ID="lbAmtLcy50" runat="server"></asp:Label>
         </td>
     </tr>
    <tr>
         <td class="MyContent">
            USD20
         </td>
         <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtRate20" Width="80%" runat="server" ClientEvents-OnValueChanged="OnUSD20Changed" NumberFormat-DecimalDigits="0" ></telerik:RadNumericTextBox>
         </td>
         <td class="MyContent">
             <telerik:RadNumericTextBox ID="txtUnit20" runat="server" Width="50%" ClientEvents-OnValueChanged="OnUSD20Changed" NumberFormat-DecimalDigits="0" > </telerik:RadNumericTextBox>
             
         </td>
         <td class="MyContent">
             <asp:Label ID="lbAmtLcy20" runat="server"></asp:Label>
         </td>
     </tr>
     <tr>
         <td class="MyContent">
             USD10
         </td>
         <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtRate10" Width="80%" runat="server" ClientEvents-OnValueChanged="OnUSD10Changed" NumberFormat-DecimalDigits="0" ></telerik:RadNumericTextBox>
         </td>
         <td class="MyContent">
             <telerik:RadNumericTextBox ID="txtUnit10" runat="server" Width="50%" ClientEvents-OnValueChanged="OnUSD10Changed" NumberFormat-DecimalDigits="0" > </telerik:RadNumericTextBox>
             
         </td>
         <td class="MyContent">
             <asp:Label ID="lbAmtLcy10" runat="server"></asp:Label>
         </td>
     </tr>
     <tr>
         <td class="MyContent">
             USD5
         </td>
         <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtRate5" Width="80%" runat="server" ClientEvents-OnValueChanged="OnUSD5Changed" NumberFormat-DecimalDigits="0" ></telerik:RadNumericTextBox>
         </td>
         <td class="MyContent">
             <telerik:RadNumericTextBox ID="txtUnit5" runat="server" Width="50%" ClientEvents-OnValueChanged="OnUSD5Changed" NumberFormat-DecimalDigits="0" > </telerik:RadNumericTextBox>
             
         </td>
         <td class="MyContent">
             <asp:Label ID="lbAmtLcy5" runat="server"></asp:Label>
         </td>
     </tr>
</table>    
            </fieldset>
</div>
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

    function CrAccount_OnClientSelectedIndexChanged() {
        var CreditAccountElement = $find("<%= rcbCreditAccount.ClientID %>");
        var lbCrAccountElement = $('#<%= lbCreditAccount.ClientID%>');
        lbCrAccountElement.html("");
        if (CreditAccountElement.get_value() != "") {
            lbCrAccountElement.html("RECORD AUTOMATICALLY");
        }
    }

    function ValidatorUpdateIsValid() {
        //var i;
        //for (i = 0; i < Page_Validators.length; i++) {
        //    if (!Page_Validators[i].isvalid) {
        //        Page_IsValid = false;
        //        return;
        //    }
        //}

        //Page_IsValid = teller != "" && drCurrency != "" && drAccount != "" && crCurrency != "";
    }

    function OnClientButtonClicking(sender, args) {
        ValidatorUpdateIsValid();
        if (Page_IsValid) {

            var button = args.get_item();

            if (button.get_commandName() == "commit" && !clickCalledAfterRadconfirm) {
                args.set_cancel(true);
                lastClickedItem = args.get_item();
                radconfirm("Credit Till Closing Balance", confirmCallbackFunction2);
            }
            if (button.get_commandName() == "authorize" && !clickCalledAfterRadconfirm) {
                radconfirm("Authorised Completed", confirmCallbackFunction2);
            }
        }
    }

    var lastClickedItem = null;
    var clickCalledAfterRadprompt = false;
    var clickCalledAfterRadconfirm = false;

    function confirmCallbackFunction1(args) {
        radconfirm("Unauthorised overdraft of USD on account 050001688331", confirmCallbackFunction2); //" + amtFCYDeposited + "
    }
   
    function confirmCallbackFunction2(args) {
        clickCalledAfterRadconfirm = true;
        lastClickedItem.click();
        lastClickedItem = null;
    }
    
    function OnAmountValueChanged() {
        var AmountElement = $find("<%= txtAmount.ClientID%>");
        var Amount = AmountElement.get_value();

        var AmountPaidElement = $('#<%= txtAmtPayCust.ClientID%>');

        var ExchangeRateElement = $find("<%= txtExchangeRate.ClientID%>");
        var ExchangeRateVal = 1;
        var DebitAccountElement = $find("<%= rcbDebitAccount.ClientID%>");
        var CreditAccountElement = $find("<%= rcbCreditAccount.ClientID%>");

        if (ExchangeRateElement.get_value() > 0) {
            if (DebitAccountElement.get_value() != CreditAccountElement.get_value()) {
                ExchangeRateVal = ExchangeRateElement.get_value();
            }
        }

        if (Amount) {
            var lcy = Amount * ExchangeRateVal;
            var amountpaid = lcy;
            AmountPaidElement.html(amountpaid.toLocaleString("en-IN"));
        }

    }

    function OnUSD100Changed() {
        var UnitElement = $find("<%= txtUnit100.ClientID%>");
        var Unit = UnitElement.get_value();

        var ExchangeRateElement = $find("<%= txtRate100.ClientID%>");
        var AmtLcyElement = $('#<%= lbAmtLcy100.ClientID%>');

        AmtLcyElement.html((Unit * ExchangeRateElement.get_value()).toLocaleString("en-IN"));
    }

    function OnUSD50Changed() {
        var UnitElement = $find("<%= txtUnit50.ClientID%>");
        var Unit = UnitElement.get_value();

        var ExchangeRateElement = $find("<%= txtRate50.ClientID%>");
        var AmtLcyElement = $('#<%= lbAmtLcy50.ClientID%>');

        AmtLcyElement.html((0.5 * Unit * ExchangeRateElement.get_value()).toLocaleString("en-IN"));
    }

    function OnUSD20Changed() {
        var UnitElement = $find("<%= txtUnit20.ClientID%>");
        var Unit = UnitElement.get_value();

        var ExchangeRateElement = $find("<%= txtRate20.ClientID%>");
        var AmtLcyElement = $('#<%= lbAmtLcy20.ClientID%>');

        AmtLcyElement.html((0.2 * Unit * ExchangeRateElement.get_value()).toLocaleString("en-IN"));
    }

    function OnUSD10Changed() {
        var UnitElement = $find("<%= txtUnit10.ClientID%>");
        var Unit = UnitElement.get_value();

        var ExchangeRateElement = $find("<%= txtRate10.ClientID%>");
        var AmtLcyElement = $('#<%= lbAmtLcy10.ClientID%>');

        AmtLcyElement.html((0.1 * Unit * ExchangeRateElement.get_value()).toLocaleString("en-IN"));
    }

    function OnUSD5Changed() {
        var UnitElement = $find("<%= txtUnit5.ClientID%>");
        var Unit = UnitElement.get_value();

        var ExchangeRateElement = $find("<%= txtRate5.ClientID%>");
        var AmtLcyElement = $('#<%= lbAmtLcy5.ClientID%>');

        AmtLcyElement.html((0.05 * Unit * ExchangeRateElement.get_value()).toLocaleString("en-IN"));
    }

    function ShowMessageCurrencyNotMath() {
        radconfirm("Currency bought and Debit Account is not matched", confirmCallbackFunction2);
    }

    function OnCurrencyMatch(e) {
        var currencyDepositedElement = $find("<%= rcbCurrencyBought.ClientID %>");
        var currencyDepositedValue = currencyDepositedElement.get_value();
        var cashAccountElement = $find("<%= rcbDebitAccount.ClientID %>");
        var cashAccountValue = cashAccountElement.get_value();
        OnAmountValueChanged();
        if (currencyDepositedValue && cashAccountValue && (currencyDepositedValue != cashAccountValue)) {
            ShowMessageCurrencyNotMath();
            currencyDepositedElement.trackChanges();
            currencyDepositedElement.get_items().getItem(0).select();
            currencyDepositedElement.updateClientState();
            currencyDepositedElement.commitChanges();

            cashAccountElement.trackChanges();
            cashAccountElement.get_items().getItem(0).select();
            cashAccountElement.updateClientState();
            cashAccountElement.commitChanges();
            return false;
        }

        return true;
    }
  </script>