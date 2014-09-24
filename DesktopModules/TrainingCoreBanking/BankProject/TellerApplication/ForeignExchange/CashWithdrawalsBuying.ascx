<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CashWithdrawalsBuying.ascx.cs" Inherits="BankProject.Views.TellerApplication.ForeignExchange.CashWithdrawalsBuying" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"/>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit"  />
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
         OnClientButtonClicking="OnClientButtonClicking" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btPreview" CommandName="preview" PostBack="false" Enabled="true">
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
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print" Enabled="true">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
</div>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtId" runat="server" Width="200" ValidationGroup="Commit" /><asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator2"
                        ControlToValidate="txtId"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="TTNo is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Cash Withdrawals</a></li>
        <!--<li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>-->
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer</td>
                <td class="MyContent" style="width:100px;"><asp:Label ID="lbCustomer"  Width="100" runat="server"></asp:Label></td>
                <td class="MyContent" ><asp:Label ID="lbCustomerName" runat="server"></asp:Label></td>
            </tr>
        </table>
        <hr />         
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Currency</td>
                <td class="MyContent"><asp:Label ID="lbCurrency" runat="server"></asp:Label></td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Account Customer
                <span class="Required">(*)</span>
                    </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbCustomerAccount"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        AutoPostBack="true" 
                        OnSelectedIndexChanged="cmbCustomerAccount_SelectedIndexChanged"
                        runat="server"           width="300"             >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="0" Text="03.000045632.1 - BANK OF SHANGHAI" />
                            <telerik:RadComboBoxItem Value="1" Text="03.000068528.1 - CITI BANK NEWYORK" />
                            <telerik:RadComboBoxItem Value="2" Text="03.000078945.1 - HSBC" />
                        </Items>
                    </telerik:RadComboBox> <asp:Label ID="lblCustomerAccount" runat="server" Text=""></asp:Label>                    
                </td>
                <td><asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator8"
                        ControlToValidate="cmbCustomerAccount"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Customer Account is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
            </tr>
        </table>
           <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Amt LCY<asp:HiddenField ID="txtAmtLCYOldValue" Value="0" runat="server" /></td>
                <td class="MyContent">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td><telerik:RadNumericTextBox ID="txtAmtLCY" runat="server" Type="Number"/></td>
                            <td style="padding-left: 10px; padding-right: 5px; color: darkgrey;">Exchange Rate: </td>
                            <td style="color: darkgrey;"><telerik:RadNumericTextBox ID="txtExchangeRate" ReadOnly="true" TabIndex="0" runat="server" Type="Number" ClientEvents-OnValueChanged ="OnExchangeRateChanged" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Amt FCY</td>
                <td class="MyContent"><asp:HiddenField ID="txtAmtFCYOldValue" Value="0" runat="server" />
                    <telerik:RadNumericTextBox ID="txtAmtFCY" runat="server" ClientEvents-OnValueChanged ="OnAmtFCYChanged"/>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Next Trans Com</td>
                <td class="MyContent">
                    <asp:Label ID="lbNextTransCom" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
       
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Currency Paid
                <span class="Required">(*)</span>
                    </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbCurrencyPaid"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        OnClientSelectedIndexChanged="cmbCurrencyPaid_OnClientSelectedIndexChanged"
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
                <td><asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="cmbCurrencyPaid"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Currency Paid is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="MyLable">Deal Rate</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtDealRate" runat="server" Type="Number" ClientEvents-OnValueChanged ="OnDealRateChanged" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Amt Paid to Cust</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="lblAmtPaidToCust" ReadOnly="true" TabIndex="0" runat="server" Type="Number" ClientEvents-OnValueChanged ="OnAmtPaidToCustChanged" />
                </td>
            </tr>

            <tr>
                <td class="MyLable">New Cust Bal</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="lblNewCustBal" ReadOnly="true" TabIndex="0" runat="server" Type="Number" />
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
                        runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>
        <hr />
       <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">
                Waive Charges
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbWaiveCharges"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="YES" Text="YES" />
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Narrative</td>
                <td class="MyContent"><telerik:RadTextBox ID="txtNarrative" Width="300" runat="server"  /><%if (RadToolBar1.FindItemByValue("btCommitData").Enabled){ %><span><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a></span><%} %></td>
            </tr>
        </table>
        </telerik:RadCodeBlock>
    </div>
    <div id="dvAudit" runat="server">
        <hr />

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
    </script>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"><img src="icons/bank/ajax-loader-16x16.gif" />
</telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
    <AjaxSettings>        
        <telerik:AjaxSetting AjaxControlID="cmbCustomerAccount">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lbCustomer" />
                <telerik:AjaxUpdatedControl ControlID="lbCustomerName" />
                <telerik:AjaxUpdatedControl ControlID="lbCurrency" />
                <telerik:AjaxUpdatedControl ControlID="txtExchangeRate" />
                <telerik:AjaxUpdatedControl ControlID="lblCustomerAccount" />
                <telerik:AjaxUpdatedControl ControlID="txtAmtLCY" />
                <telerik:AjaxUpdatedControl ControlID="lblAmtPaidToCust" />
                <telerik:AjaxUpdatedControl ControlID="lblNewCustBal" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">
    function cmbCurrencyPaid_OnClientSelectedIndexChanged(sender, eventArgs) {
        var currencyValue = $find("<%= cmbCurrencyPaid.ClientID%>").get_value();
        var txtDealRate = $find("<%= txtDealRate.ClientID%>");
        txtDealRate.set_value(0);
        if (currencyValue != '') {
            txtDealRate.set_value(currencyValue.split('#')[1]);
        }
    }

    function ValidatorUpdateIsValid() {
        var CustomerAccount = $('#<%= cmbCustomerAccount.ClientID%>').val();
        var TellerId = $('#<%= txtTellerId.ClientID%>').val();
        var CurrencyPaid = $('#<%= cmbCurrencyPaid.ClientID%>').val();

        Page_IsValid = CustomerAccount != "" && TellerId != "" && CurrencyPaid != "";
    }

    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Commit%>" && !clickCalledAfterRadconfirm) {
            ValidatorUpdateIsValid();
            if (Page_IsValid) {
                args.set_cancel(true);
                lastClickedItem = args.get_item();
                radconfirm("Credit Till Closing Balance", confirmCallbackFunction1);
            }
            return;
        }

        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Preview%>") {
            window.location = '<%=EditUrl("list")%>&lst=4appr';
        }
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Search%>") {
            window.location = '<%=EditUrl("list")%>';
        }
    }

    var lastClickedItem = null;
    var clickCalledAfterRadprompt = false;
    var clickCalledAfterRadconfirm = false;

    function confirmCallbackFunction1(args) {
        clickCalledAfterRadconfirm = false;
        var amtFCYDepositedElement = $find("<%= txtAmtFCY.ClientID%>");
        var amtFCYDeposited = amtFCYDepositedElement.get_value();
        radconfirm("Unauthorised overdraft of " + $('#<%=lbCurrency.ClientID%>').val() + " " + amtFCYDeposited + " on account " + $('#<%= cmbCustomerAccount.ClientID%>').val(), confirmCallbackFunction2); //" + amtFCYDeposited + "
    }
   
    function confirmCallbackFunction2(args) {
        if (args) {
            clickCalledAfterRadconfirm = true;
            lastClickedItem.click();
            lastClickedItem = null;
        }
    }

    function OnExchangeRateChanged() {
        var objAmtLCY = $find("<%= txtAmtLCY.ClientID %>");
        var ExchangeRate = Number($find("<%= txtExchangeRate.ClientID%>").get_value());        
        var amtFCY = Number($find("<%= txtAmtFCY.ClientID%>").get_value());
        if (ExchangeRate <= 0 || amtFCY <= 0) {
            objAmtLCY.set_value(0);
            return;
        }
        //Tinh lai LCY
        objAmtLCY.set_value(amtFCY * ExchangeRate);
        //Tinh lai Amt Paid To Cust
        OnDealRateChanged();
    }

    function OnAmtFCYChanged() {
        var amtFCY = Number($find("<%= txtAmtFCY.ClientID%>").get_value());
        var amtFCYOldValue = Number($('#<%=txtAmtFCYOldValue.ClientID%>').val());
        if (amtFCY == amtFCYOldValue) return;//check loop :)
        $('#<%=txtAmtFCYOldValue.ClientID%>').val(amtFCY);
        //
        OnExchangeRateChanged();
    }

    function OnDealRateChanged() {
        //Tinh lai Amt Paid To Cust
        var ExchangeRate = Number($find("<%= txtExchangeRate.ClientID%>").get_value());
        var amtFCY = Number($find("<%= txtAmtFCY.ClientID%>").get_value());
        var Currency = $find("<%= cmbCurrencyPaid.ClientID%>").get_value();
        var dealRate = Number($find("<%= txtDealRate.ClientID%>").get_value());
        var objAmtPaidToCust = $find("<%= lblAmtPaidToCust.ClientID%>");
        if (Currency == '' || dealRate <= 0) {
            objAmtPaidToCust.set_value(0);
            return;
        }
        //
        objAmtPaidToCust.set_value(amtFCY * ExchangeRate / dealRate);
    }

    function OnAmtPaidToCustChanged() {
        var amount = Number($find("<%= lblAmtPaidToCust.ClientID%>").get_value());
        $find("<%= lblNewCustBal.ClientID%>").set_value(amount * -1);
    }
  </script>
</telerik:RadCodeBlock>