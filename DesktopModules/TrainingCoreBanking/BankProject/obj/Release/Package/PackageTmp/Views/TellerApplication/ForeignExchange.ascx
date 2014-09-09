<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForeignExchange.ascx.cs" Inherits="BankProject.Views.TellerApplication.ForeignExchange" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>
<%@ Register Src="~/Controls/VVComboBox.ascx" TagPrefix="uc1" TagName="VVComboBox" %>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit"  />
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });

</script>
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
        OnClientButtonClicking="OnClientButtonClicking"  OnButtonClick="RadToolBar1_ButtonClick">
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
        <li><a href="#ChristopherColumbus">Sell Travellers Cheques</a></li>
        <li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer Name <span class="Required">(*)</span></td>
                <td class="MyContent"><telerik:RadTextBox ID="tbCustomerName" runat="server" Width="350"></telerik:RadTextBox></td>
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
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Address <span class="Required">(*)</span></td>
                <td class="MyContent"><telerik:RadTextBox ID="tbAddress" runat="server" Width="350"></telerik:RadTextBox></td>
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
        </table>

                <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Phone No.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtPhoneNo" runat="server"></telerik:RadTextBox>
                 <%--   <telerik:RadMaskedTextBox ID="txtPhoneNo" runat="server" Mask="(###)###-####" 
                    EmptyMessage="-- Enter Phone Number --" HideOnBlur="true" ZeroPadNumericRanges="true" 
                    DisplayMask="(###)###-####"> </telerik:RadMaskedTextBox> --%>

                </td>
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Debit Currency
                <span class="Required">(*)</span>
                    </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbDebitCurrency"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        OnSelectedIndexChanged="cmbDebitCurrency_SelectedIndexChanged"
                        AutoPostBack="true"
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
                        ID="RequiredFieldcmbTCCurrency"
                        ControlToValidate="cmbDebitCurrency"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Debit Currency is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
            </tr>
        </table>
           <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
                <td class="MyLable">Debit Account</td>
                <td class="MyContent" style="width:350px;" >
                    <telerik:RadComboBox ID="rcbDebitAccount"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" Width="350" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="USD" Text="USD-10111-0296-296" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
        </table>
            </ContentTemplate>
            </asp:UpdatePanel>
            <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Debit Amt LCY</span></td>
                <td class="MyContent">
                    <asp:Label ID="lblDebitAmtLCY" runat="server"></asp:Label>
                </td>
            </tr>
        </table>

         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Debit Amt FCY</span></td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="tbDebitAmtFCY" runat="server" NumberFormat-DecimalSeparator="," NumberFormat-DecimalDigits="2" ClientEvents-OnValueChanged="OnAmountValueChanged"/>
                </td>
            </tr>
        </table>
         <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
               <td class="MyLable">Currency Paid
                    </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbCurrencyPaid"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
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
                    <asp:Label ID="lblCurrencyPaid" runat="server"></asp:Label>
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
                <td class="MyLable">Credit Account</td>
                <td class="MyContent" style="width:160px">
                    <telerik:RadComboBox ID="rcbCrAccount"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        Width="160"
                        OnClientSelectedIndexChanged="CrAccount_OnClientSelectedIndexChanged"
                        runat="server" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND-10001-0296-296" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            <td><asp:Label ID="lbCreditAccount" runat="server"></asp:Label></td>
            </tr>
        </table>

              <table width="100%" cellpadding="0" cellspacing="0">
            
            <tr>
                <td class="MyLable">Deal Rate</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="tbDealRate" runat="server"  NumberFormat-DecimalDigits="0" ClientEvents-OnValueChanged="OnAmountValueChanged" />
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Amount Paid to Cust</td>
                <td class="MyContent">
                    <asp:Label ID="lblAmountPaidToCust" runat="server"></asp:Label>
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
        var lbCrAccountElement = $('#<%= lbCreditAccount.ClientID%>');
        lbCrAccountElement.html("RECORD AUT");
    }
    function ValidatorUpdateIsValid() {
        //var i;
        //for (i = 0; i < Page_Validators.length; i++) {
        //    if (!Page_Validators[i].isvalid) {
        //        Page_IsValid = false;
        //        return;
        //    }
        //}
        var teller = $('#<%= txtTellerId.ClientID%>').val();
        var CustomerName = $('#<%= tbCustomerName.ClientID%>').val();
        var Address = $('#<%= tbAddress.ClientID%>').val();
        var DebitCurrency = $('#<%= cmbDebitCurrency.ClientID%>').val();

        Page_IsValid = teller != "" && CustomerName != "" && Address != "" && DebitCurrency != "";
    }

    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        args.set_cancel(false);
        ValidatorUpdateIsValid();
        if (Page_IsValid) {
            if (button.get_commandName() == "commit" && !clickCalledAfterRadconfirm) {
                args.set_cancel(true);
                lastClickedItem = args.get_item();
                radconfirm("Credit Till Closing Balance", confirmCallbackFunction2);
            }
            if (button.get_commandName() == "authorize" && !clickCalledAfterRadconfirm) {
                radconfirm("Authorised Completed", confirmCallbackFunction2);
                args.set_cancel(true);
            }
        }

        if (button.get_commandName() == "Preview") {
            window.location = "Default.aspx?tabid=153&ctl=chitiet&mid=798";
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
            var TCAmountElement = $find("<%= tbDebitAmtFCY.ClientID%>");
            var TCAmount = TCAmountElement.get_value();

            var AmountLCYElement = $('#<%= lblDebitAmtLCY.ClientID%>');
            var AmountPaidElement = $('#<%= lblAmountPaidToCust.ClientID%>');

            var DealRateElement = $find("<%= tbDealRate.ClientID%>");
            if (DealRateElement.get_value() <= 0) {
                DealRateElement.set_value(17791);
            }

            if (TCAmount) {
                var DealRate = DealRateElement.get_value();
                var lcy = TCAmount * DealRate;
                var amountpaid = lcy;
                AmountLCYElement.html(lcy.toLocaleString("en-US"));
                AmountPaidElement.html(amountpaid.toLocaleString("en-US"));
            }

            var DebitAccElement = $('<%= rcbDebitAccount.ClientID%>');
            DebitAccElement.get_items().getItem(1).select();

            var CrAccElement = $find("<%= rcbCrAccount.ClientID%>");
            CrAccElement.get_items().getItem(1).select();

        }
  </script>