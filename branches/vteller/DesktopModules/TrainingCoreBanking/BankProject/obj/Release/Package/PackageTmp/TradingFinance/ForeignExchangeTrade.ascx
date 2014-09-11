<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForeignExchangeTrade.ascx.cs" Inherits="BankProject.Views.TellerApplication.ForeignExchangeTrade" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit"  />

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });

</script>
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
       OnClientButtonClicking="OnClientButtonClicking"   OnButtonClick="RadToolBar1_ButtonClick">
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
        <li><a href="#ChristopherColumbus">Input</a></li>
        <li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Transaction Type</td>
                <td class="MyContent">
                     <telerik:RadComboBox 
                        AutoPostBack="True" OnTextChanged="rcbTransactionType_OnTextChanged"
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
                <td class="MyLable">Counterparty</td>
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
                <td class="MyLable">Exchange Type <span class="Required">(*)</span></td>
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
                        ErrorMessage="Exchange Type is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
                <td><asp:Label ID="lblExchangeType" runat="server" Text="Trading market"></asp:Label></td>
            </tr>
        </table>

       
        <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Buy Currency
                    </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbBuyCurrency"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="rcbBuyCurrency_OnSelectedIndexChanged"
                        OnClientSelectedIndexChanged="OnAmountValueChanged"
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
                <td class="MyLable">Buy Amount </td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtBuyAmount" runat="server" NumberFormat-DecimalSeparator="," NumberFormat-DecimalDigits="0" ClientEvents-OnValueChanged="OnAmountValueChanged"/>
                </td>
            </tr>
        </table>
         
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
               <td class="MyLable">Sell Currency</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbSellCurrency"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="rcbSellCurrency_OnSelectedIndexChanged"
                        OnClientSelectedIndexChanged="OnAmountValueChanged"
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
                <td class="MyLable">Sell Amount</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtSellAmount" runat="server" NumberFormat-DecimalSeparator="," NumberFormat-DecimalDigits="2" />
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
                    <telerik:RadTextBox ID="txtCustomerCode" 
                        runat="server"  />
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
                <td class="MyContent" style="width:350px; ">
                    <telerik:RadTextBox ID="txtComment" Width="350"
                        runat="server"  />
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
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
                        <telerik:RadComboBoxItem Value="" Text="" />
                        <telerik:RadComboBoxItem Value="312" Text="312 - Le Thi Hoa" />
                            <telerik:RadComboBoxItem Value="313" Text="313 - Tran Thi Lan" />
                            <telerik:RadComboBoxItem Value="314" Text="314 - Nguyen Thi Thu" />
                            <telerik:RadComboBoxItem Value="315" Text="315 - Nguyen Khoa Minh Tri" />
                            <telerik:RadComboBoxItem Value="316" Text="316 - Le Tran Hong Phuc" />
                            <telerik:RadComboBoxItem Value="317" Text="317 - Phan Minh Hoang" />
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
    function rcbExchangeType_OnClientSelectedIndexChanged() {
        var ExchangeType = $('#<%= rcbExchangeType.ClientID%>').val();
        var lblExchangeTypeElement = $('#<%= lblExchangeType.ClientID%>');
        lblExchangeTypeElement.html("");
        if (ExchangeType != "") {
            lblExchangeTypeElement.html("Trading market");
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
        var ExchangeType = $('#<%= rcbExchangeType.ClientID%>').val();

        Page_IsValid = ExchangeType != "";
    }

    function OnClientButtonClicking(sender, args) {
        ValidatorUpdateIsValid();
        var button = args.get_item();
        var showmessage = false;//muon show thi bật lên
        if (Page_IsValid) {
            
            if (showmessage && button.get_commandName() == "commit" && !clickCalledAfterRadconfirm) {
                args.set_cancel(true);
                lastClickedItem = args.get_item();
                radconfirm("Credit Till Closing Balance", confirmCallbackFunction2);
            }
            if (showmessage && button.get_commandName() == "authorize" && !clickCalledAfterRadconfirm) {
                radconfirm("Authorised Completed", confirmCallbackFunction2);
            }
        }

        if (button.get_commandName() == "Preview") {
            window.location = "Default.aspx?tabid=267&ctl=chitiet&mid=811";
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
            var BuyAmountElement = $find("<%= txtBuyAmount.ClientID%>");
            var BuyAmount = BuyAmountElement.get_value();

            var SellAmountFCYElement = $find("<%= txtSellAmount.ClientID%>");

            if (BuyAmount) {
                var ExchangeRate = CalculateDealRate();
                var fcy = BuyAmount * ExchangeRate;
                SellAmountFCYElement.set_value(fcy);
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