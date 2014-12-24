<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForeignExchangeTrade.ascx.cs" Inherits="BankProject.Views.TellerApplication.ForeignExchangeTrade" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit"  />
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
</telerik:RadCodeBlock>
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
       OnClientButtonClicking="RadToolBar1_OnClientButtonClicking" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btPreview" CommandName="preview" PostBack="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="search" Enabled="false">
        </telerik:RadToolBarButton>
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print" Value="btPrint" CommandName="print" PostBack="false">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
</div>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtId" runat="server" Width="200" /> <span class="Required">*</span><asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator6"
            ControlToValidate="txtId"
            ValidationGroup="Commit"
            InitialValue=""
            ErrorMessage="FX Number is required" ForeColor="Red">
        </asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Input</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Transaction Type <span class="Required">*</span><asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator1"
            ControlToValidate="rcbTransactionType"
            ValidationGroup="Commit"
            InitialValue=""
            ErrorMessage="Transaction Type is required" ForeColor="Red">
        </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                     <telerik:RadComboBox 
                        AutoPostBack="True" 
                         OnTextChanged="rcbTransactionType_OnTextChanged"
                        ID="rcbTransactionType"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="TT" Text="TT" />
                            <telerik:RadComboBoxItem Value="LC" Text="LC" />
                            <telerik:RadComboBoxItem Value="DP/DA" Text="DP/DA" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>

            <tr>
                <td class="MyLable">TF No.</td>
                <td class="MyContent" style="width: 150px">
                    <telerik:RadTextBox ID="txtFTNo" runat="server"
                        MaxLength="100"
                        AutoPostBack="True" 
                        OnTextChanged="txtFTNo_OnTextChanged" />
                </td>
                <td>
                    <asp:Label ID="lblFTNoError" runat="server" ForeColor="red"/>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
            <tr>
                <td class="MyLable">Deal Type</td>
                <td class="MyContent">
                     <telerik:RadComboBox ID="rcbDealType"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="0" Text="SP" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Counterparty <span class="Required">*</span><asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator2"
            ControlToValidate="rcbCounterparty"
            ValidationGroup="Commit"
            InitialValue=""
            ErrorMessage="Counterparty is required" ForeColor="Red">
        </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                     <telerik:RadComboBox ID="rcbCounterparty"
                        Width="360"
                        AppendDataBoundItems="True"
                        AutoPostBack="True"
                         OnSelectedIndexChanged="rcbCounterparty_OnSelectedIndexChanged"
                        OnItemDataBound="rcbCounterparty_OnItemDataBound"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Deal Date</td>
                <td class="MyContent"><telerik:RadDatePicker ID="txtDealDate" runat="server" Width="160"></telerik:RadDatePicker></td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Value Date</td>
                <td class="MyContent"><telerik:RadDatePicker ID="txtValueDate" runat="server" Width="160"></telerik:RadDatePicker></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
            <tr>
                <td class="MyLable">Irregular Customer</td>
                <td class="MyContent"><telerik:RadTextBox ID="txtIrregularCustomers" runat="server" Width="350"></telerik:RadTextBox></td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
            <tr>
                <td class="MyLable">Doc ID</td>
                <td class="MyContent"><telerik:RadTextBox ID="txtIDCard" runat="server" Width="350"></telerik:RadTextBox></td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
            <tr>
                <td class="MyLable">Vat Serial No.</td>
                <td class="MyContent"><telerik:RadTextBox ID="txtVatSerialNo" runat="server" Width="200"></telerik:RadTextBox></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
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
                <td class="MyLable">Exchange Type <span class="Required"> </span></td>
                <td class="MyContent" style="width:160px">
                    <telerik:RadComboBox ID="rcbExchangeType"
                        MarkFirstMatch="True" Width="160"
                        AllowCustomText="false"
                        runat="server" >
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="1" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldrcbExchangeType"
                        ControlToValidate="rcbExchangeType"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Exchange Type is Required" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td><asp:Label ID="lblExchangeType" runat="server" Text="Trading market"></asp:Label></td>
            </tr>
        </table>

       
        <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Buy Currency <span class="Required">*</span><asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator3"
            ControlToValidate="rcbBuyCurrency"
            ValidationGroup="Commit"
            InitialValue=""
            ErrorMessage="Buy Currency is required" ForeColor="Red"></asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadComboBox AppendDataBoundItems="True"
                        ID="rcbBuyCurrency"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="rcbBuyCurrency_OnSelectedIndexChanged"
                        OnClientSelectedIndexChanged="OnAmountValueChanged"
                        runat="server" >
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
           
         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Buy Amount <span class="Required">*</span><asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator4"
            ControlToValidate="txtBuyAmount"
            ValidationGroup="Commit"
            InitialValue="0"
            ErrorMessage="Buy Amount is required" ForeColor="Red"></asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtBuyAmount" runat="server" NumberFormat-DecimalSeparator="," NumberFormat-DecimalDigits="2" Value="0" />
                </td>
            </tr>
        </table>
         
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
               <td class="MyLable">Sell Currency <span class="Required">*</span><asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator5"
            ControlToValidate="rcbSellCurrency"
            ValidationGroup="Commit"
            InitialValue=""
            ErrorMessage="Sell Currency is required" ForeColor="Red"></asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbSellCurrency"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="rcbSellCurrency_OnSelectedIndexChanged"
                        OnClientSelectedIndexChanged="OnAmountValueChanged"
                        runat="server" >
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Sell Amount <span class="Required">*</span><asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator7"
            ControlToValidate="txtSellAmount"
            ValidationGroup="Commit"
            InitialValue="0"
            ErrorMessage="Sell Amount is required" ForeColor="Red"></asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtSellAmount" runat="server" NumberFormat-DecimalSeparator="," NumberFormat-DecimalDigits="2"
                         ClientEvents-OnValueChanged="OnAmountValueChanged" Value="0" />
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">            
            <tr>
                <td class="MyLable">Rate</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtRate" runat="server"  NumberFormat-DecimalDigits="5" ClientEvents-OnValueChanged="OnAmountValueChanged" />
                </td>
            </tr>
        </table>
         <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
            <tr>
                <td class="MyLable">Customer Code</td>
                <td class="MyContent" >
                    <telerik:RadTextBox ID="txtCustomerCode" runat="server"  />
                </td>
            </tr>
        </table>    
        <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
            <tr>
                <td class="MyLable">H.O Code</td>
                <td class="MyContent" >
                    <asp:Label ID="lblHOCode" runat="server"></asp:Label>
                </td>
            </tr>
        </table> 
        <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
            <tr>
                <td class="MyLable">Swap Base Ccy </td>
                <td class="MyContent" >
                    <asp:Label ID="lbSwapBaseCcy" runat="server"></asp:Label>
                </td>
            </tr>
        </table> 

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Comment</td>
                <td class="MyContent" >
                    <telerik:RadTextBox ID="txtComment1" Width="350" runat="server" MaxLength="35" />
                </td>
            </tr>
            <tr>
                <td class="MyLable"></td>
                <td class="MyContent" >
                    <telerik:RadTextBox ID="txtComment2" Width="350" runat="server"  MaxLength="35"/>
                </td>
            </tr>
            <tr>
                <td class="MyLable"></td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtComment3" Width="350" runat="server"  MaxLength="35"/>
                </td>
            </tr>
        </table>

         <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
            <tr>
                <td class="MyLable">Desk</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbDesk"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="12" Text="12" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <hr />

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Credit Account</td>
                <td class="MyContent" style="width: 150px">
                    <telerik:RadTextBox ID="txtCustomerReceivingAC" runat="server"
                        AutoPostBack="True" OnTextChanged="txtCustomerReceivingAC_OnTextChanged" />
                </td>
                <td><asp:Label ID="lblCustomerReceivingACError" runat="server" ForeColor="red" /></td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Debit Account</td>
                <td class="MyContent" style="width: 150px">
                    <telerik:RadTextBox ID="txtCustomerPayingAC" runat="server" 
                        AutoPostBack="True" OnTextChanged="txtCustomerPayingAC_OnTextChanged"/>
                </td>
                <td><asp:Label ID="lblCustomerPayingACError" runat="server" ForeColor="red" /></td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Account Officer</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbAccountOfficer"
                        MarkFirstMatch="True"
                        AllowCustomText="false"  Width="250"
                        runat="server" >
                        <Items>
                        <%--<telerik:RadComboBoxItem Value="" Text="" />
                        <telerik:RadComboBoxItem Value="312" Text="312 - Le Thi Hoa" />
                            <telerik:RadComboBoxItem Value="313" Text="313 - Tran Thi Lan" />
                            <telerik:RadComboBoxItem Value="314" Text="314 - Nguyen Thi Thu" />
                            <telerik:RadComboBoxItem Value="315" Text="315 - Nguyen Khoa Minh Tri" />
                            <telerik:RadComboBoxItem Value="316" Text="316 - Le Tran Hong Phuc" />
                            <telerik:RadComboBoxItem Value="317" Text="317 - Phan Minh Hoang" />--%>
                            </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
            <tr>
                <td class="MyLable">HO Plus</td>
                <td class="MyContent" >
                    <telerik:RadTextBox ID="txtHOPlus" Width="160" runat="server"  />
                </td>
            </tr>
        </table> 
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

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1">
    <AjaxSettings>
        
        <telerik:AjaxSetting AjaxControlID="txtCustomerReceivingAC">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblCustomerReceivingACError" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="txtCustomerPayingAC">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblCustomerPayingACError" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="rcbTransactionType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtFTNo" />
                <telerik:AjaxUpdatedControl ControlID="rcbSellCurrency" />                
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="txtFTNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblFTNoError" />
                <telerik:AjaxUpdatedControl ControlID="rcbCounterparty" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="rcbSellCurrency">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtCustomerReceivingAC" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="rcbBuyCurrency">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtCustomerPayingAC" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="rcbCounterparty">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtCustomerReceivingAC" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
    </AjaxSettings>
</telerik:RadAjaxManager>

<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
    function rcbExchangeType_OnClientSelectedIndexChanged() {
        var ExchangeType = $('#<%= rcbExchangeType.ClientID%>').val();
        var lblExchangeTypeElement = $('#<%= lblExchangeType.ClientID%>');
        lblExchangeTypeElement.html("");
        if (ExchangeType != "") {
            lblExchangeTypeElement.html("Trading market");
        }
    }

    function RadToolBar1_OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        //
        if (button.get_commandName() == '<%=BankProject.Controls.Commands.Preview%>') {
            window.location = '<%=EditUrl("chitiet")%>&lst=4appr';
        }
        if (button.get_commandName() == '<%=BankProject.Controls.Commands.Search%>') {
            window.location = '<%=EditUrl("chitiet")%>';
        }
        //
        if (button.get_commandName() == '<%=BankProject.Controls.Commands.Print%>') {
            args.set_cancel(true);
            radconfirm("Do you want to download file?", confirmCallbackFunctionVAT, 340, 150, null, 'Download');
        }
    }

    function confirmCallbackFunctionVAT(result) {
        if (result) {
            $("#<%=btnReport.ClientID %>").click();
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
        var SellAmountElement = $find("<%= txtSellAmount.ClientID%>");
        var SellAmount = SellAmountElement.get_value();

        var BuyAmountFCYElement = $find("<%= txtBuyAmount.ClientID%>");
        var DealRate = $find("<%= txtRate.ClientID%>").get_value();
        if (SellAmount && DealRate) {
            var ExchangeRate = CalculateDealRate();
            var fcy = SellAmount * ExchangeRate;
            BuyAmountFCYElement.set_value(fcy);
        }

    }

    function CalculateDealRate() {
        var currencyBuyElement = $find("<%= rcbBuyCurrency.ClientID %>");
        var currencySellElement = $find("<%= rcbSellCurrency.ClientID %>");
        var dealRateElement = $find("<%= txtRate.ClientID %>");
          var dealRateValue = 1;
          if (currencyBuyElement.get_value() == currencySellElement.get_value()) {
              dealRateElement.set_value("");
          }

          if (dealRateElement.get_value() > 0) dealRateValue = dealRateElement.get_value();

          return dealRateValue;
    }
    
    $("#<%=txtId.ClientID %>").keyup(function (event) {

        if (event.keyCode == 13) {
            $("#<%=btSearch.ClientID %>").click();
          }
    });
  </script>
</telerik:RadCodeBlock>
<div style="visibility: hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Search" /></div>