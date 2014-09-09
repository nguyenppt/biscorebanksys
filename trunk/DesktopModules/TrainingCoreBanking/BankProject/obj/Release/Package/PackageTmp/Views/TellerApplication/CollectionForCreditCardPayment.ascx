<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollectionForCreditCardPayment.ascx.cs" Inherits="BankProject.Views.TellerApplication.CollectionForCreditCardPayment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });

    function CalculateDealRate() {
        var currencyDepositedElement = $find("<%= cmbDebitCurrency.ClientID %>");
        var currencyDepositedValue = currencyDepositedElement.get_value();

        var cmbCurrencyElement = $find("<%= cmbCreditCurrency.ClientID %>");
        var cmbCurrencyValue = cmbCurrencyElement.get_value();

        var dealRateElement = $find("<%= txtDealRate.ClientID %>");
        var dealRateValue = dealRateElement.get_value();
        if (dealRateValue <= 0 || currencyDepositedValue == cmbCurrencyValue) {
            dealRateValue = 1;
        }

        return dealRateValue;
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

    function showmessageTrungCurrency(text) {
        radconfirm(text + " Currency and " + text + " Account is not matched", confirmCallbackFunction2);
    }

    function OnCurrencyMatch() {
        //credit
        var CreditcurrencyDepositedElement = $find("<%= cmbCreditCurrency.ClientID %>");
        var CreditcurrencyDepositedValue = CreditcurrencyDepositedElement.get_value();
        var CreditAccountElement = $find("<%= cmbCreditAccount.ClientID %>");
        var CreditAccountValue = CreditAccountElement.get_value();

        //Debit
        var DebitcurrencyDepositedElement = $find("<%= cmbDebitCurrency.ClientID %>");
        var DebitcurrencyDepositedValue = DebitcurrencyDepositedElement.get_value();
        var DebitAccountElement = $find("<%= cmbDebitAccount.ClientID %>");
        var DebitAccountValue = DebitAccountElement.get_value();

        CreditAccountElement.enabled = true;
        CreditAccountElement.enable();
        
        var debititem = CreditAccountElement.findItemByValue(DebitcurrencyDepositedElement.get_value());
        if (debititem != null) {
            CreditAccountElement.trackChanges();
            CreditAccountElement.get_items().getItem(debititem.get_index()).select();
            CreditAccountElement.updateClientState();
            CreditAccountElement.disable();
            CreditAccountElement.enabled = false;
            CreditAccountElement.commitChanges();
        }
        //if (CreditcurrencyDepositedValue && CreditAccountValue && (CreditcurrencyDepositedValue != CreditAccountValue)) {
        //    showmessageTrungCurrency("Credit");
        //    //alert("Currency and Cash Account is not matched");
        //    CreditcurrencyDepositedElement.trackChanges();
        //    CreditcurrencyDepositedElement.get_items().getItem(0).select();
        //    CreditcurrencyDepositedElement.updateClientState();
        //    CreditcurrencyDepositedElement.commitChanges();

        //    CreditAccountElement.trackChanges();
        //    CreditAccountElement.get_items().getItem(0).select();
        //    CreditAccountElement.updateClientState();
        //    CreditAccountElement.commitChanges();
        //    return false;
        //}

        if (DebitcurrencyDepositedValue && DebitAccountValue && (DebitcurrencyDepositedValue != DebitAccountValue)) {
            showmessageTrungCurrency("Debit");
            //alert("Currency and Cash Account is not matched");
            DebitcurrencyDepositedElement.trackChanges();
            DebitcurrencyDepositedElement.get_items().getItem(0).select();
            DebitcurrencyDepositedElement.updateClientState();
            DebitcurrencyDepositedElement.commitChanges();

            DebitAccountElement.trackChanges();
            DebitAccountElement.get_items().getItem(0).select();
            DebitAccountElement.updateClientState();
            DebitAccountElement.commitChanges();
            return false;
        }

        return true;
    }

    function OnChangeDealRate() {
        if (OnCurrencyMatch()) {
            var amtLCYDepositedElement = $find("<%= txtDebitAmtLCY.ClientID %>");
            var amtLCYDeposited = amtLCYDepositedElement.get_value();

            var currencyDeposited = $find("<%= cmbCreditCurrency.ClientID %>");
            var currencyDepositedValue = currencyDeposited.get_value();

            var newCustBalElement = $('#<%=lblCreditAmount.ClientID%>');
            var dealRate = CalculateDealRate();
            switch (currencyDepositedValue) {
                case "":
                    newCustBalElement.html("");
                    break;
                default:
                    var parCurrency1 = amtLCYDeposited * dealRate;
                    if (parCurrency1) {
                        newCustBalElement.html(parCurrency1.toLocaleString("en-US"));
                    }
                    break;
            }
        }
    }

    function Customer_OnClientSelectedIndexChanged() {
        var customerElement = $find("<%= cmbCustomerId.ClientID %>");

        var FullNameElement = $find("<%= txtFullName.ClientID %>");
        FullNameElement.set_value(customerElement.get_selectedItem().get_attributes().getAttribute("CustomerName"));

        var AddressElement = $find("<%= txtAddress.ClientID %>");
        AddressElement.set_value(customerElement.get_selectedItem().get_attributes().getAttribute("Address"));

        var legalIdElement = $find("<%= txtLegalID.ClientID %>");
        legalIdElement.set_value(customerElement.get_selectedItem().get_attributes().getAttribute("IdentityNo"));

        var PlaceOfIssElement = $find("<%= txtPlaceOfIs.ClientID %>");
        PlaceOfIssElement.set_value(customerElement.get_selectedItem().get_attributes().getAttribute("IssuePlace"));

        var txtTelElement = $find("<%= txtTel.ClientID %>");
        txtTelElement.set_value(customerElement.get_selectedItem().get_attributes().getAttribute("Telephone"));

        var datesplit = customerElement.get_selectedItem().get_attributes().getAttribute("IssueDate").split('/');
        var IsssuedDateElement = $find("<%= rdpIssueDate.ClientID %>");
        IsssuedDateElement.set_selectedDate(new Date(datesplit[2].substring(0, 4), datesplit[0], datesplit[1]));
    }

</script>
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%"
        OnButtonClick="OnRadToolBarClick">
        <Items>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
                ToolTip="Commit Data" Value="btdoclines" CommandName="doclines">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
                ToolTip="Preview" Value="btdocnew" CommandName="docnew">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
                ToolTip="Authorize" Value="btdraghand" CommandName="draghand">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
                ToolTip="Reverse" Value="btreverse" CommandName="reverse">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
                ToolTip="Search" Value="searchNew" CommandName="searchNew">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
                ToolTip="Print Deal Slip" Value="print" CommandName="print">
            </telerik:RadToolBarButton>
        </Items>
    </telerik:RadToolBar>
</div>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
    ShowSummary="False" ValidationGroup="Commit" />

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtId" runat="server" Width="200" Text="TT/09161/07929" />
            <i>
                <asp:Label ID="lblDepositCode" runat="server" /></i></td>
    </tr>
</table>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Collection For Credit Card Payment</a></li>
        <li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer ID</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbCustomerId" Width="300"
                        MarkFirstMatch="True"
                        AllowCustomText="false" 
                        OnItemDataBound="cmbCustomerId_ItemDataBound"
                        OnClientSelectedIndexChanged="Customer_OnClientSelectedIndexChanged"
                        runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Full Name</td>
                <td class="MyContent" width="300">
                    <telerik:RadTextBox ID="txtFullName" Width="300"
                        runat="server" ValidationGroup="Group1" />
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Address</td>
                <td class="MyContent" width="300">
                    <telerik:RadTextBox ID="txtAddress" Width="300" runat="server" ValidationGroup="Group1" />
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
        </table>


        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Legal ID</td>
                <td class="MyContent" width="300">
                    <telerik:RadTextBox ID="txtLegalID" runat="server" ValidationGroup="Group1" />
                </td>
                <td class="MyLable" style="width: 80px;">Issue Date</td>
                <td class="MyContent">
                    <telerik:RadDatePicker ID="rdpIssueDate" runat="server" ValidationGroup="Group1" />
                </td>
            </tr>
        </table>


        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Tel</td>
                <td class="MyContent" width="300">
                    <telerik:RadTextBox ID="txtTel" runat="server" ValidationGroup="Group1" />
                </td>
                <td class="MyLable" style="width: 80px;">Place of Is</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtPlaceOfIs" runat="server" ValidationGroup="Group1" />
                </td>
            </tr>
        </table>
        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Teller ID<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator6"
                        ControlToValidate="txtTellerId1"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Teller ID is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtTellerId1"
                        runat="server" ValidationGroup="Group1">
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>

        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Debit Currency<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="cmbDebitCurrency"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Debit Currency is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbDebitCurrency"
                        MarkFirstMatch="True" 
                        AllowCustomText="false"
                        OnClientSelectedIndexChanged="OnCurrencyMatch"
                        runat="server" ValidationGroup="Group1">
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
                <td class="MyLable">Debit Account</td>
                <td class="MyContent" width="250">
                    <telerik:RadComboBox ID="cmbDebitAccount"
                        MarkFirstMatch="True"
                        OnClientSelectedIndexChanged="OnCurrencyMatch"
                        AllowCustomText="false"
                        Width="250" runat="server" ValidationGroup="Group1">
                        <Items>
                             <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="USD" Text="USD-11477-0002-0001" />
                            <telerik:RadComboBoxItem Value="EUR" Text="EUR-26951-0002-0001" />
                            <telerik:RadComboBoxItem Value="GBP" Text="GBP-27002-0002-0001" />
                            <telerik:RadComboBoxItem Value="JPY" Text="JPY-27051-0002-0001" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND-10001-0002-0001" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
        </table>


        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Debit Amt</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtDebitAmtLCY"
                        runat="server"
                        
                        ClientEvents-OnValueChanged="OnChangeDealRate"
                        ValidationGroup="Group1">
                        <NumberFormat DecimalDigits="2" />
                    </telerik:RadNumericTextBox>
                </td>
            </tr>
          
        </table>

        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Teller ID:<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator4"
                        ControlToValidate="txtTellerId2"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Teller ID is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtTellerId2"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" Text="0695" ValidationGroup="Group1">                        
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>
        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Credit Account<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator2"
                        ControlToValidate="cmbCreditAccount"
                        ValidationGroup="Commit"
                        InitialValue=""
                        Enabled="false"
                        ErrorMessage="Credit Account is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbCreditAccount"
                        MarkFirstMatch="True"
                        Width="320"
                        AllowCustomText="false"
                        runat="server" ValidationGroup="Group1">
                        <Items>
                             <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="USD" Text="USD-11477-0024-2690" />
                            <telerik:RadComboBoxItem Value="EUR" Text="EUR-11477-0024-2695" />
                            <telerik:RadComboBoxItem Value="GBP" Text="GBP-11477-0024-2700" />
                            <telerik:RadComboBoxItem Value="JPY" Text="JPY-11477-0024-2705" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND-11477-0024-2710" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Credit Currency</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbCreditCurrency"
                        MarkFirstMatch="True" OnClientSelectedIndexChanged="OnChangeDealRate"
                        AllowCustomText="false"
                        runat="server" ValidationGroup="Group1">
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
            <tr>
                <td class="MyLable">Deal Rate:</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtDealRate" ClientEvents-OnValueChanged="OnChangeDealRate"
                        runat="server" ValidationGroup="Group1">
                    </telerik:RadTextBox>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Credit Amount:</td>
                <td class="MyContent">
                    <asp:Label ID="lblCreditAmount" runat="server" /></td>
            </tr>
        </table>
        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Credit Card Number<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator3"
                        ControlToValidate="txtCreditCardNumber"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Credit Card Number is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtCreditCardNumber"
                        runat="server" ValidationGroup="Group1">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Waive Charges?:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbWaiveCharges"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server">
                        <Items>                            
                            <telerik:RadComboBoxItem Value="YES" Text="YES" />
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Narrative:</td>
                <td class="MyContent" width="300">
                    <telerik:RadTextBox ID="txtNarrative" Width="300"
                        runat="server" ValidationGroup="Group1" />
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
        </table>
    </div>
</div>

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
               $(this).attr('name', 'row' + thisRow + thisName);
               $(this).attr('id', 'row' + thisRow + thisName);
           });

   });
</script>
