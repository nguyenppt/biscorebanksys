<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CashDeposits.ascx.cs" Inherits="BankProject.Views.TellerApplication.CashDeposits" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>

<div>
   <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%"
       OnClientButtonClicking="OnClientButtonClicking" OnButtonClick="OnRadToolBarClick">
        <Items>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
                ToolTip="Commit Data" Value="btCommit" CommandName="Commit">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
                ToolTip="Preview" Value="btPreview" CommandName="Preview">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
                ToolTip="Authorize" Value="btAuthorize" CommandName="Authorize">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
                ToolTip="Reverse" Value="btReverse" CommandName="Reverse">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
                ToolTip="Search" Value="btSearch" CommandName="Search">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
                ToolTip="Print Deal Slip" Value="btPrint" CommandName="Print">
            </telerik:RadToolBarButton>
        </Items>
    </telerik:RadToolBar>
</div>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
    ShowSummary="False" ValidationGroup="Commit" />
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtId" runat="server" Width="200" />
        </td>
    </tr>
</table>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Cash Deposits</a></li>
        <%--<li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>--%>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer</td>
                <td width="120">
                    <asp:Label runat="server" ID="lblCustomerId" /></td>
                <td>
                    <asp:Label runat="server" ID="lblCustomerName" /></td>
            </tr>
        </table>

        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
         <td class="MyLable">Account Type</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbAccountType" runat="server" 
                        OnSelectedIndexChanged="rcbAccountType_OnSelectedIndexChanged" autopostback="true"
                    AllowcustomText="false" MarkFirstMatch="true" AppendDataBoundItems="True" width="200" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" /> 
                    <Items>
                        <telerik:RadComboBoxItem Value="1" Text="Non Term Saving Account" />
                        <telerik:RadComboBoxItem Value="2" Text="Saving Account - Arrear" />
                        <telerik:RadComboBoxItem Value="3" Text="Saving Account - Periodic" />
                        <telerik:RadComboBoxItem Value="4" Text="Saving Account - Discounted" />
                        <telerik:RadComboBoxItem Value="5" Text="Loan Working Account" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer Account
                <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator8"
                        ControlToValidate="cmbCustomerAccount"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Customer Account is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent" width="120">
                    <telerik:RadTextBox ID="cmbCustomerAccount"
                        autopostback="true" width="120"
                        OnTextChanged="cmbCustomerAccount_TextChanged"
                        runat="server" >
                    </telerik:RadTextBox>
                </td>
                 <td>
                    <asp:Label ID="lbErrorAccount"  ForeColor="Red" Visible="false" runat="server" text="Customer Account does not exist" ></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbAccountTitle" runat="server" ></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbAccountId" runat="server" visible="false" ></asp:Label>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Currency</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="cmbCurrency" borderwidth="0" readonly="true"
                        runat="server" >
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Amt Paid to Cust
                </td>
                <td>
                    <telerik:radNumericTextBox runat="server" ID="lblAmtPaidToCust" readonly="true" BorderWidth="0" >
                        <NumberFormat DecimalDigits="2" />
                    </telerik:radNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Cust Bal
                </td>
                <td>
                    <telerik:radNumericTextBox runat="server" ID="lblCustBal" readonly="true" BorderWidth="0">
                        <NumberFormat DecimalDigits="2" />
                    </telerik:radNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">New Cust Bal
                </td>
                <td>
                    <telerik:radNumericTextBox runat="server" ID="lblNewCustBal" readonly="true" BorderWidth="0"/>
                        <NumberFormat DecimalDigits="2" />
                    </telerik:radNumericTextBox>
                    </td>
            </tr>
        </table>

        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Teller ID <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator6"
                        ControlToValidate="txtTellerId"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Teller ID is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtTellerId"
                        runat="server" ValidationGroup="Group1">
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>

        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Currency Deposited
                <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="cmbCurrencyDeposited"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Currency Deposited is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbCurrencyDeposited"
                        MarkFirstMatch="True" 
                        autopostback="true"
                        OnSelectedIndexChanged="cmbCurrencyDeposited_OnSelectedIndexChanged"
                        OnClientSelectedIndexChanged="OnCurrencyMatch"
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
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Cash Account</td>
                <td class="MyContent" >
                    <telerik:RadComboBox ID="cmbCashAccount"
                        MarkFirstMatch="True"
                        AllowCustomText="false" 
                        Width="350" runat="server" ValidationGroup="Group1">
                       
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Amount Deposited</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtAmtDeposited" runat="server" ClientEvents-OnValueChanged="OnChangeDealRate"
                        ValidationGroup="Group1">
                        <NumberFormat DecimalDigits="2" />
                    </telerik:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Deal Rate</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtDealRate" runat="server" 
                        ClientEvents-OnValueChanged="OnChangeDealRate"
                        ValidationGroup="Group1">
                        <NumberFormat DecimalDigits="5" />
                    </telerik:RadNumericTextBox>
                </td>
            </tr>
        </table>

        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Waive Charges?</td>
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
                <td class="MyLable">Narrative</td>
                <td class="MyContent" >
                    <telerik:RadTextBox ID="txtNarrative" Width="300"
                        runat="server" ValidationGroup="Group1" />
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Print LnNo of PS</td>
                <td class="MyContent" >
                    <telerik:RadTextBox ID="txtPrintLnNoOfPS" Width="300" Text="1"
                        runat="server" ValidationGroup="Group1" />
                </td>
            </tr>
        </table>
    </div>
    <div id="blank"></div>
</div>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="cmbCustomerAccount">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lbAccountId" />
                 <telerik:AjaxUpdatedControl ControlID="lblCustomerId" />
                 <telerik:AjaxUpdatedControl ControlID="lblCustomerName" />
                 <telerik:AjaxUpdatedControl ControlID="lblCustBal" />
                 <telerik:AjaxUpdatedControl ControlID="cmbCurrency" />
                 <telerik:AjaxUpdatedControl ControlID="lbErrorAccount" />
                 <telerik:AjaxUpdatedControl ControlID="lbAccountTitle" />
                 <telerik:AjaxUpdatedControl ControlID="hdfCheckCustomer" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="rcbTransactionType">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lbAccountId" />
                 <telerik:AjaxUpdatedControl ControlID="lblCustomerId" />
                 <telerik:AjaxUpdatedControl ControlID="lblCustomerName" />
                 <telerik:AjaxUpdatedControl ControlID="lblCustBal" />
                 <telerik:AjaxUpdatedControl ControlID="cmbCurrency" />
                 <telerik:AjaxUpdatedControl ControlID="lbErrorAccount" />
                 <telerik:AjaxUpdatedControl ControlID="lbAccountTitle" />
                 <telerik:AjaxUpdatedControl ControlID="hdfCheckCustomer" />
            </UpdatedControls>
        </telerik:AjaxSetting>

         <telerik:AjaxSetting AjaxControlID="cmbCurrencyDeposited">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="cmbCashAccount" />
                
            </UpdatedControls>
        </telerik:AjaxSetting>
        
    </AjaxSettings>
</telerik:RadAjaxManager>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });

    function CalculateDealRate() {
        var currencyDepositedElement = $find("<%= cmbCurrencyDeposited.ClientID %>");
        var currencyDepositedValue = currencyDepositedElement.get_value();

        var cmbCurrencyElement = $find("<%= cmbCurrency.ClientID %>");
        var cmbCurrencyValue = cmbCurrencyElement.get_value();

        var dealRateElement = $find("<%= txtDealRate.ClientID %>");
        var dealRateValue = dealRateElement.get_value();
        if (dealRateValue <= 0 || currencyDepositedValue == cmbCurrencyValue) {
            dealRateValue = 1;
        }

        return dealRateValue;
    }


    function OnCurrencyMatch(e) {
        var currencyDepositedElement = $find("<%= cmbCurrencyDeposited.ClientID %>");
        var currencyDepositedValue = currencyDepositedElement.get_value();
        var cashAccountElement = $find("<%= cmbCashAccount.ClientID %>");
        var cashAccountValue = cashAccountElement.get_value();
        //if (currencyDepositedValue && cashAccountValue && (currencyDepositedValue != cashAccountValue)) {
        //    //alert("Currency Deposited and Cash Account is not matched");
        //    showmessageTrungCurrency();
        //    currencyDepositedElement.trackChanges();
        //    currencyDepositedElement.get_items().getItem(0).select();
        //    currencyDepositedElement.updateClientState();
        //    currencyDepositedElement.commitChanges();

        //    cashAccountElement.trackChanges();
        //    cashAccountElement.get_items().getItem(0).select();
        //    cashAccountElement.updateClientState();
        //    cashAccountElement.commitChanges();
        //    return false;
        //}

        OnChangeDealRate();
        return true;
    }

    function OnChangeDealRate() {
        var lblCustBalElement = $find("<%= lblCustBal.ClientID %>");
        var CustBal = lblCustBalElement.get_value();
        var amtDepositedElement = $find("<%= txtAmtDeposited.ClientID %>");
        var amtDeposited = amtDepositedElement.get_value();

        var currencyDeposited = $find("<%= cmbCurrency.ClientID %>");
        var currencyDepositedValue = currencyDeposited.get_value();

        var nAmtPaidToCustElement = $find("<%=lblAmtPaidToCust.ClientID%>");
        var dealRate = CalculateDealRate();
        var parCurrency1 = amtDeposited * dealRate;
        nAmtPaidToCustElement.set_value(parCurrency1);

        var newCustBalElement = $find("<%=lblNewCustBal.ClientID%>");
        if (amtDeposited) {
            newCustBalElement.set_value(CustBal + parCurrency1);
        }
    }

    $('#<%=txtId.ClientID%>').keyup(function (event) {
        if (event.keyCode == 13) { $("#<%=btSearch.ClientID%>").click(); }
    });

    function ValidatorUpdateIsValid() {
        //var i;
        //for (i = 0; i < Page_Validators.length; i++) {
        //    if (!Page_Validators[i].isvalid) {
        //        Page_IsValid = false;
        //        return;
        //    }
        //}
        var CustomerAccount = $('#<%= cmbCustomerAccount.ClientID%>').val();
        var TellerId = $('#<%= txtTellerId.ClientID%>').val();
        var CurrencyDeposited = $('#<%= cmbCurrencyDeposited.ClientID%>').val();
        Page_IsValid = CustomerAccount != "" && TellerId != "" && CurrencyDeposited != "";
    }

    var lastClickedItem = null;
    var clickCalledAfterRadconfirm = false;

    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        if (button.get_commandName() == "Commit") {
            ValidatorUpdateIsValid();
            if (Page_IsValid) {
                if ($('#<%= hdfCheckCustomer.ClientID%>').val() == "0") {
                    Page_IsValid = false;
                    alert("Customer Account does not exists");
                    return false;
                }
            }
        }

        if (button.get_commandName() == "Preview") {
            window.location = "Default.aspx?tabid=124&ctl=CashDepositePreviewList&mid=776";
        }

        if (button.get_commandName() == "Print" && !clickCalledAfterRadconfirm) {
            args.set_cancel(true);
            radconfirm("Do you want to download Cash Deposit file?", confirmPrintDeal, 340, 150, null, 'Download');
        }
    }

    function confirmPrintDeal(result) {
        clickCalledAfterRadconfirm = false;
        if (result) {
            $("#<%=btPrintDeal.ClientID %>").click();
            }
        radconfirm("Do you want to download Non Term Saving Account file?", confirmPrintOpenAccount, 340, 150, null, 'Download');
    }

    function confirmPrintOpenAccount(result) {
        clickCalledAfterRadconfirm = false;
        if (result) {
            $("#<%=btPrintOpen.ClientID %>").click();
        }
    }

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

<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" Text="Search"  OnClick="btSearch_Click" />
    <asp:hiddenfield ID="hdfCheckCustomer" runat="server" value="1"></asp:hiddenfield>
    <asp:Button ID="btPrintDeal" runat="server" Text="Search"  OnClick="btPrintDeal_Click" />
    <asp:Button ID="btPrintOpen" runat="server" Text="Search"  OnClick="btPrintOpen_Click" />
</div>