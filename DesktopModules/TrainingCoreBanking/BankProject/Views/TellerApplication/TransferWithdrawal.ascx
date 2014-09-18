<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransferWithdrawal.ascx.cs" Inherits="BankProject.Views.TellerApplication.TransferWithdrawal" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register Src="../../Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />
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

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtId" runat="server" Width="200" />
        </td>
    </tr>
</table>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Transfer Withdrawal</a></li>
        <%--<li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>--%>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>_______________________________Debit Inforation_______________________________</td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer</td>
                <td width="80px">
                    <asp:Label ID="lblCustomerId" runat="server" /></td>
                <td>
                    <asp:Label ID="lblCustomerName" runat="server" /></td>
            </tr>
        </table>

       <table width="100%" cellpadding="0" cellspacing="0">
         <td class="MyLable">Account Type</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbAccountType" runat="server" AllowcustomText="false" 
                        OnSelectedIndexChanged="rcbAccountType_OnSelectedIndexChanged" autopostback="true"
                    MarkFirstMatch="true" AppendDataBoundItems="True" width="250" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" /> 
                    <Items>
                        <telerik:RadComboBoxItem Value="1" Text="Current & Non-Term Saving Account" />
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
                <td class="MyLable">Debit Account
                <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator8"
                        ControlToValidate="cmbDebitAccount"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Debit Account is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent" width="120" >
                    <telerik:RadTextBox ID="cmbDebitAccount"
                        AutoPostBack="true" width="120"
                        OnTextChanged="cmbDebitAccount_TextChanged"
                        runat="server" >
                    </telerik:RadTextBox>
                </td>
                 <td>
                    <asp:Label ID="lbErrorDebitAccount"  ForeColor="Red" Visible="false" runat="server" text="Debit Account does not exist" ></asp:Label>
                </td>
             
                <td>
                    <asp:Label ID="lbDebitAccountTitle" runat="server" ></asp:Label>
                </td>

                <td><asp:Label ID="lbDebitAccountId" runat="server" visible="false" ></asp:Label></td>
               
            </tr>
            </table>
        <table width="100%" cellpadding="0" cellspacing="0">

            <tr>
                <td class="MyLable">Currency</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="cmbDebitCurrency"
                        readonly="true" BorderWidth="0"
                        OnClientTextChanged="OnChangeDealRate"
                        runat="server" >
                       
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>
         

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Debit Amt</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtDebitAmt" runat="server"
                         ClientEvents-OnValueChanged="OnChangeDealRate"
                        ValidationGroup="Group1">
                        <NumberFormat DecimalDigits="2" />
                    </telerik:RadNumericTextBox>
                </td>
            </tr>
         
             <tr>
                <td class="MyLable">Cust Bal</td>
                <td>
                    <telerik:radNumericTextBox runat="server" readonly="true" BorderWidth="0" ID="lblCustBal" >
                        <NumberFormat DecimalDigits="2" />
                    </telerik:radNumericTextBox>

                </td>
            </tr>
            <tr>
                <td class="MyLable">New Cust Bal</td>
                <td>
                    <telerik:radNumericTextBox runat="server" readonly="true" BorderWidth="0" ID="lblNewCustBal" >
                        <NumberFormat DecimalDigits="2" />
                    </telerik:radNumericTextBox>

                </td>
            </tr>
            <tr>
                <td class="MyLable">Value Date</td>
                <td class="MyContent">
                    <telerik:RadDatePicker ID="rdpValueDate" runat="server" ValidationGroup="Group1" />
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>_______________________________Credit Inforation_______________________________</td>
            </tr>
        </table>

       <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer</td>
                <td style="width:80px; ">
                    <asp:Label ID="lbCustomerID_CR" runat="server" /></td>
                <td>
                    <asp:Label ID="lbCustomerName_CR" runat="server" /></td>
            </tr>
            </table>

        <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Credit Account
                <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator1"
                            ControlToValidate="cmbCreditAccount"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Credit Account is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator></td>
                    <td class="MyContent" width="120">
                        <telerik:RadTextBox ID="cmbCreditAccount"
                            AutoPostBack="true" width="120"
                            OnTextChanged="cmbCreditAccount_TextChanged"
                            runat="server">
                            
                        </telerik:RadTextBox>
                    </td>
                  
                   <td>
                    <asp:Label ID="lbErrorCreditAccount" ForeColor="Red" Visible="false" runat="server" text="Credit Account does not exist" ></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbCreditAccountTitle" runat="server" ></asp:Label>
                </td>
                 <td>
                        <asp:Label runat="server" ID="lbCreditAccountId" Visible="false"></asp:Label>
                 </td>
                </tr>
            </table>

       <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Currency</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="cmbCreditCurrency" 
                        OnClientSelectedIndexChanged="OnChangeDealRate"
                        readonly="true" BorderWidth="0"
                        runat="server" >
                       
                    </telerik:RadTextBox>
                </td>
            </tr>
            
      </table>
              
            <table width="100%" cellpadding="0" cellspacing="0">
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

                <tr>
                    <td class="MyLable">Amt Credit for Cust</td>
                    <td>
                        <telerik:RadNumericTextBox ID="lblAmtCreditForCust" readonly="true" BorderWidth="0" runat="server" >
                            <NumberFormat DecimalDigits="2" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Value Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="rdpCreditValueDate" runat="server" ValidationGroup="Group1" />
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
                            width="60"
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
    </div>
</div>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
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

    function showmessageTrungCurrency() {
        radconfirm("Currency and Credit Account is not matched", confirmCallbackFunction2);
    }

    var lastClickedItem = null;
    var clickCalledAfterRadprompt = false;
    var clickCalledAfterRadconfirm = false;

    function confirmCallbackFunction1(args) {
        clickCalledAfterRadconfirm = false;
        radconfirm("Unauthorised overdraft of USD " + amtFCYDeposited + " on account 050001688331", confirmCallbackFunction2); //" + amtFCYDeposited + "
    }

    function confirmCallbackFunction2(args) {
        clickCalledAfterRadconfirm = true;
        lastClickedItem.click();
        lastClickedItem = null;
    }

    function OnChangeDealRate() {
        var amtDepositedElement = $find("<%= txtDebitAmt.ClientID %>");
        var amtDeposited = amtDepositedElement.get_value();

        var newCustBalElement = $find("<%=lblNewCustBal.ClientID%>");
        var nAmtPaidToCustElement = $find("<%=lblAmtCreditForCust.ClientID%>");
        var dealRate = CalculateDealRate();

        var newCustBalElement = $find("<%=lblNewCustBal.ClientID%>");
        var lblCustBal = $find("<%=lblCustBal.ClientID%>");
        var custBal = lblCustBal.get_value();

        if (amtDeposited > custBal) {
            showmessage();
        }

        var parCurrency1 = amtDeposited * dealRate;
        if (parCurrency1) {
            newCustBalElement.set_value(custBal - amtDeposited);
            nAmtPaidToCustElement.set_value(parCurrency1);
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

        var DebitAccount = $('#<%= cmbDebitAccount.ClientID%>').val();
        var CreditAccount = $('#<%= cmbCreditAccount.ClientID%>').val();
        Page_IsValid = DebitAccount != "" && CreditAccount != "";
    }

    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        if (button.get_commandName() == "Commit") {
            ValidatorUpdateIsValid();
            if (Page_IsValid) {
                $('#<%= hdfCheckOverdraft.ClientID%>').val(1);

                if ($('#<%= hdfCheckDebit.ClientID%>').val() == "0") {
                    Page_IsValid = false;
                    alert("Debit account does not exist");
                    return false;
                }

                if ($('#<%= hdfCheckCredit.ClientID%>').val() == "0") {
                    Page_IsValid = false;
                    alert("Credit account does not exist");
                    return false;
                }

                var CustBalElement = $find("<%= lblCustBal.ClientID %>");
                var CustBal = CustBalElement.get_value();
                var amtDepositedElement = $find("<%= txtDebitAmt.ClientID %>");
                var amtDeposited = amtDepositedElement.get_value();
                if (amtDeposited > CustBal) {
                    showmessage();
                    $('#<%= hdfCheckOverdraft.ClientID%>').val(0);
                    Page_IsValid = false;
                    return false;
                }
            }
        }

        if (button.get_commandName() == "Preview") {
            window.location = "Default.aspx?tabid=126&ctl=TransferWithdrawalPreviewList&mid=802";
        }
    }

    function showmessage() {
        var CustBalElement = $find("<%= lblCustBal.ClientID %>");
        var CustBal = CustBalElement.get_value();
        var CurElement = $find("<%=cmbDebitCurrency.ClientID%>");
        var CurValue = CurElement.get_value();
        radconfirm("Can't overdraft. Maximum is " + CustBal.toLocaleString("en-IN") + " " + CurValue, confirmCallbackFunction2);
    }

    $('#<%=txtId.ClientID%>').keyup(function (event) {
        if (event.keyCode == 13) { $("#<%=btSearch.ClientID%>").click(); }
    });

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
 <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="cmbDebitAccount">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lbDebitAccountId" />
                 <telerik:AjaxUpdatedControl ControlID="lblCustomerId" />
                 <telerik:AjaxUpdatedControl ControlID="lblCustomerName" />
                 <telerik:AjaxUpdatedControl ControlID="lblCustBal" />
                 <telerik:AjaxUpdatedControl ControlID="cmbDebitCurrency" />
                 <telerik:AjaxUpdatedControl ControlID="lbErrorDebitAccount" />
                 <telerik:AjaxUpdatedControl ControlID="lbDebitAccountTitle" />
                 <telerik:AjaxUpdatedControl ControlID="hdfCheckDebit" />
                
                
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="cmbCreditAccount">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lbCreditAccountId" />
                 <telerik:AjaxUpdatedControl ControlID="lbCustomerID_CR" />
                 <telerik:AjaxUpdatedControl ControlID="lbCustomerName_CR" />
                 <telerik:AjaxUpdatedControl ControlID="cmbCreditCurrency" />
                 <telerik:AjaxUpdatedControl ControlID="lbErrorCreditAccount" />
                 <telerik:AjaxUpdatedControl ControlID="lbCreditAccountTitle" />
                 <telerik:AjaxUpdatedControl ControlID="hdfCheckCredit" />
                
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="rcbAccountType">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lbDebitAccountId" />
                 <telerik:AjaxUpdatedControl ControlID="lblCustomerId" />
                 <telerik:AjaxUpdatedControl ControlID="lblCustomerName" />
                 <telerik:AjaxUpdatedControl ControlID="lblCustBal" />
                 <telerik:AjaxUpdatedControl ControlID="cmbDebitCurrency" />
                 <telerik:AjaxUpdatedControl ControlID="lbErrorDebitAccount" />
                 <telerik:AjaxUpdatedControl ControlID="lbDebitAccountTitle" />
                 <telerik:AjaxUpdatedControl ControlID="hdfCheckDebit" />
                 <telerik:AjaxUpdatedControl ControlID="lbCreditAccountId" />
                 <telerik:AjaxUpdatedControl ControlID="lbCustomerID_CR" />
                 <telerik:AjaxUpdatedControl ControlID="lbCustomerName_CR" />
                 <telerik:AjaxUpdatedControl ControlID="cmbCreditCurrency" />
                 <telerik:AjaxUpdatedControl ControlID="lbErrorCreditAccount" />
                 <telerik:AjaxUpdatedControl ControlID="lbCreditAccountTitle" />
                 <telerik:AjaxUpdatedControl ControlID="hdfCheckCredit" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
    </AjaxSettings>
</telerik:RadAjaxManager>

<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" Text="Search"  OnClick="btSearch_Click" />
    <asp:hiddenfield ID="hdfCheckDebit" runat="server" value="1"></asp:hiddenfield>
    <asp:hiddenfield ID="hdfCheckCredit" runat="server" value="1"></asp:hiddenfield>
    <asp:hiddenfield ID="hdfCheckOverdraft" runat="server" value="0"></asp:hiddenfield>
</div>