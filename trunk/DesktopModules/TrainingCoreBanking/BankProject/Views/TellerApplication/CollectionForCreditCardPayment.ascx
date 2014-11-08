<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollectionForCreditCardPayment.ascx.cs" Inherits="BankProject.Views.TellerApplication.CollectionForCreditCardPayment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>

<telerik:RadCodeBlock runat="server" >
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
        if (currencyDepositedValue == cmbCurrencyValue) {
            dealRateValue = 1;
        }

        return dealRateValue;
    }

    var lastClickedItem = null;
    var clickCalledAfterRadprompt = false;
    var clickCalledAfterRadconfirm = false;

    function confirmCallbackFunction1(args) {
        radconfirm("Unauthorised overdraft ", confirmCallbackFunction2); //" + amtFCYDeposited + "
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
        if (DebitAccountValue == "" || CreditcurrencyDepositedValue == "") {
            $find("<%=tbCreditAmt.ClientID%>").set_value("");
        }
        return true;
    }

    function OnChangeDealRate() {
        if (OnCurrencyMatch()) {
            var amtLCYDepositedElement = $find("<%= txtDebitAmtLCY.ClientID %>");
            var amtLCYDeposited = amtLCYDepositedElement.get_value();

            var currencyDeposited = $find("<%= cmbCreditCurrency.ClientID %>");
            var currencyDepositedValue = currencyDeposited.get_value();

            var CreditAmt = $find("<%=tbCreditAmt.ClientID%>");
            var dealRate = CalculateDealRate();
            switch (currencyDepositedValue) {
                case "":
                    CreditAmt.set_value("");
                    break;
                default:
                    var parCurrency1 = amtLCYDeposited * dealRate;
                    if (parCurrency1) {
                        CreditAmt.set_value(parCurrency1.toLocaleString("en-US"));
                    }
                    break;
            }
        }
    }

</script>
    </telerik:RadCodeBlock>
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
            <asp:TextBox ID="txtId" runat="server" Width="200" readOnly="true" />
            <i>
                <asp:Label ID="lblDepositCode" runat="server" /></i></td>
    </tr>
</table>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Collection For Credit Card Payment</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer ID<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  Display="None" ValidationGroup="Commit" 
                         ControlToValidate="tbCustomerId"
                         InitialValue="" ForeColor="Red" ErrorMessage="Debit Account is required" /></td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbCustomerId" Width="150"
                        MarkFirstMatch="True"
                        autoPostBack="true"
                        OntextChanged="tbCustomerId_OntextChanged"
                        runat="server" ValidationGroup="Group1">
                    </telerik:RadTextBox>
                    
                </td>
                <td class="MyLable"><asp:Label ID="lblNote" runat="server" Width="200"></asp:Label></td>
                <td class="MyContent" style="visibility:hidden;">
                    <telerik:RadTextbox id="sd" runat="server" />
                </td>
            </tr>
        </table>

        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Full Name</td>
                <td class="MyContent" width="350">
                    <telerik:RadTextBox ID="txtFullName" Width="350" readonly="true"
                        runat="server" ValidationGroup="Group1" />
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Address</td>
                <td class="MyContent" width="550">
                    <telerik:RadTextBox ID="txtAddress" Width="550" runat="server" ValidationGroup="Group1" readonly="true"/>
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
        </table>


        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Legal ID</td>
                <td class="MyContent" width="300">
                    <telerik:RadTextBox ID="txtLegalID" runat="server" ValidationGroup="Group1" readonly="true" />
                </td>
                <td class="MyLable" style="width: 80px;">Issue Date</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbIssueDate" runat="server" ValidationGroup="Group1" readonly="true" />
                </td>
            </tr>
        </table>


        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Telephone</td>
                <td class="MyContent" width="300">
                    <telerik:RadTextBox ID="txtTel" runat="server" ValidationGroup="Group1" readonly="true"/>
                </td>
                <td class="MyLable" style="width: 80px;">Issue Place</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtPlaceOfIs" runat="server" ValidationGroup="Group1" readonly="true"/>
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
                        MarkFirstMatch="True"  AutoPostBack="true"
                        AllowCustomText="false" OnSelectedIndexChanged="cmbDebitCurrency_OnSelectedIndexChanged"
                        OnClientSelectedIndexChanged="OnCurrencyMatch"
                        runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
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
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
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
                        MarkFirstMatch="True" enabled="false"
                        Width="250"
                        AllowCustomText="false"
                        runat="server" ValidationGroup="Group1">
                        <Items>
                             <telerik:RadComboBoxItem Value="" Text="" />
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
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Deal Rate:</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtDealRate" ClientEvents-OnValueChanged="OnChangeDealRate" NumberFormat-DecimalDigits="5"
                        runat="server" ValidationGroup="Group1">
                    </telerik:RadNumericTextBox>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Credit Amount:</td>
                <td class="MyContent">
                    <telerik:radnumerictextbox id="tbCreditAmt" runat="server" validationGroup="Group1" borderwidth="0" readonly="true" />
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
                    <telerik:RadTextBox ID="txtCreditCardNumber" MaxLength="20" width="250"
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
                <td class="MyContent" width="450">
                    <telerik:RadTextBox ID="txtNarrative" Width="450"
                        runat="server" ValidationGroup="Group1" />
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
            <tr>
                <td class="MyLable"></td>
                <td class="MyContent" width="450">
                    <telerik:RadTextBox ID="txtNarrative2" Width="450"
                        runat="server" ValidationGroup="Group1" />
                </td>
            </tr>
        </table>
    </div>
</div>
<telerik:RadCodeBlock runat="server">
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
    </telerik:RadCodeBlock>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="cmbDebitCurrency">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="cmbCreditAccount" />
                 <telerik:AjaxUpdatedControl ControlID="cmbDebitAccount" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="tbCustomerId">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblNote" />
                 <telerik:AjaxUpdatedControl ControlID="txtFullName" />
                 <telerik:AjaxUpdatedControl ControlID="txtAddress" />
                 <telerik:AjaxUpdatedControl ControlID="txtLegalID" />
                 <telerik:AjaxUpdatedControl ControlID="tbIssueDate" />
                 <telerik:AjaxUpdatedControl ControlID="txtTel" />
                 <telerik:AjaxUpdatedControl ControlID="txtPlaceOfIs" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
