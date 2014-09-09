<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DCTransaction.ascx.cs" Inherits="BankProject.Views.TellerApplication.DCTransaction" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit"  />
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
        <li><a href="#ChristopherColumbus">Collect Charges</a></li>
        <li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
          <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer ID</td>
                <td class="MyContent" >
                    <telerik:RadComboBox AppendDataBoundItems="True" 
                    ID="rcbCustomerID" Runat="server"
                    MarkFirstMatch="True" Width="350" Height="150px" 
                        OnItemDataBound="rcbCustomerID_ItemDataBound"
                        OnClientSelectedIndexChanged="Customer_OnClientSelectedIndexChanged"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
                </td>
                <td></td>
                <td></td>
            </tr>

             <tr>
                <td class="MyLable">Customer Name</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbCustomerName" runat="server" Width="350">
                    </telerik:RadTextBox>
            </td>
                <td></td>
                   <td></td>
            </tr>
             <tr>
                <td class="MyLable">Address</td>
                <td class="MyContent" style="width:350px;">

                    <telerik:RadTextBox ID="tbAddress" runat="server" Width="350"></telerik:RadTextBox>

                    </td>
                  <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                   <td></td>
            </tr>
               <tr>
                <td class="MyLable">Legal ID</td>
                <td class="MyContent"><telerik:RadTextBox ID="tbLegalID" runat="server" Width="160"></telerik:RadTextBox></td>
                   <td></td>
                   <td></td>
            </tr>
                <tr>
                <td class="MyLable">Isssued Date</td>
                <td class="MyContent"><telerik:RadDatePicker ID="tbIsssuedDate" runat="server" Width="160"></telerik:RadDatePicker></td>

                <td class="MyLable">Place of Issue</td>
                <td class="MyContent"><telerik:RadTextBox ID="tbPlaceOfIss" runat="server" Width="160"></telerik:RadTextBox></td>
            </tr>
</table>

        <table width="100%" cellpadding="0" cellspacing="0">
          
        </table>
      <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
            <td class="MyLable">Reversal</td>
            <td class="MyContent">              
                  <telerik:RadcomboBox
                        ID="rcbReversal" runat="server" 
                        MarkFirstMatch="True" Width="150" Height="100"
                        AllowCustomText="false" >
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="YES" Text="YES" />
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                        </Items>
                    </telerik:RadcomboBox>   

            </td>
       </tr>
         </table>
         <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">Txn Code <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldTxnCode"
                        ControlToValidate="rcbTxnCode"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Txn Code is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                    <td class="MyContent" style="width:80px;">
                    
                       <telerik:RadcomboBox
                        ID="rcbTxnCode" runat="server" 
                        MarkFirstMatch="True" Width="80" Height="150"
                           OnClientSelectedIndexChanged="TxnCode_OnClientSelectedIndexChanged"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                             <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="902" Text="902" />
                        </Items>
                    </telerik:RadcomboBox> 
                    </td>
                  
                    <td><asp:Label ID="lbTxnCode" runat="server"></asp:Label></td>
                </tr>          
            </table>
              <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
            <td class="MyLable">Sign <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldSign"
                        ControlToValidate="rcbSign"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Sign is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
            <td class="MyContent">              
                  <telerik:RadcomboBox
                        ID="rcbSign" runat="server" 
                        MarkFirstMatch="True" Width="80" Height="150"
                        AllowCustomText="false" >
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="C" Text="C" />
                            <telerik:RadComboBoxItem Value="D" Text="D" />
                        </Items>
                    </telerik:RadcomboBox>   

            </td>
       </tr>
         </table>
        <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">Account</td>
                    <td class="MyContent" style="width:250px;">
                    
                       <telerik:RadcomboBox
                        ID="rcbAccount" runat="server" 
                        MarkFirstMatch="True" Width="250" Height="150"
                           OnClientSelectedIndexChanged="rcbAccount_OnClientSelectedIndexChanged"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                             <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="USD" Text="USD-19991-0024-2690" />
                            <telerik:RadComboBoxItem Value="EUR" Text="EUR-19991-0024-2695" />
                            <telerik:RadComboBoxItem Value="GBP" Text="GBP-19991-0024-2700" />
                            <telerik:RadComboBoxItem Value="JPY" Text="JPY-19991-0024-2705" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND-19991-0024-2710" />
                        </Items>
                    </telerik:RadcomboBox> 
                    </td>
                  
                    <td><asp:Label ID="lbAccount" runat="server"></asp:Label></td>
                </tr>          
            </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
            <td class="MyLable">Foreign Currency
            </td>
            <td class="MyContent">              
                  <telerik:RadcomboBox
                        ID="rcbForeignCurrency" runat="server" 
                        MarkFirstMatch="True" Width="160" Height="150"
                      OnClientSelectedIndexChanged="rcbForeignCurrency_OnClientSelectedIndexChanged"
                        AllowCustomText="false" >
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="USD" Text="USD" />
                            <telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                            <telerik:RadComboBoxItem Value="GBP" Text="GBP" />
                            <telerik:RadComboBoxItem Value="JPY" Text="JPY" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND" />
                        </Items>
                    </telerik:RadcomboBox>   

            </td>
       </tr>
         </table>
         <table width="100%" cellpadding="0" cellspacing="0">
         <tr>
            <td class="MyLable">Amount LCY</td>
            <td class="MyContent"> 
                <telerik:RadNumericTextBox ID="txtAmountLCY" runat="server" NumberFormat-DecimalSeparator="." NumberFormat-DecimalDigits="0" />
            </td>
        </tr>
          <tr>
            <td class="MyLable">Amount FCY</td>
            <td class="MyContent">
                <telerik:RadNumericTextBox ID="tbAmountFCY" runat="server" NumberFormat-DecimalSeparator="." NumberFormat-DecimalDigits="2" ClientEvents-OnValueChanged="ChargeAmountFCYOnValueChanged"/>
            </td>
        </tr>
              <tr>
                  <td class="MyLable">Deal Rate</td>
                 <td class="MyContent">
                     <telerik:RadNumericTextBox ID="tbDealRate" runat="server" NumberFormat-DecimalSeparator="." NumberFormat-DecimalDigits="2" ClientEvents-OnValueChanged="ChargeAmountFCYOnValueChanged"/>
                 </td>
             </tr>

             <tr>
                 <td class="MyLable">Value Date</td>
                 <td class="MyContent">
                     <telerik:RadDatePicker ID="tbValueDate" runat="server"></telerik:RadDatePicker>
                 </td>
               
             </tr>
    </table>

        <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">Narrative</td>
                    <td class="MyContent" style="width:350px;">
                        <telerik:RadTextBox width="350" ID="tbNarrative" runat="server" 
                            />
                    </td>
                    <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>  
			
		</div>
    </div>	
			
<script type="text/javascript">
    $(document).ready(function () {
        $('a.add').live('click', function () {
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
        $('a.remove').live('click', function () {
            $(this)
                .closest('tr')
                .remove();
        });
        $('input:text').each(function () {
            var thisName = $(this).attr('name'),
                thisRrow = $(this)
                            .closest('tr')
                            .index();
            $(this).attr('name', 'row' + thisRow + thisName);
            $(this).attr('id', 'row' + thisRow + thisName);
        });

    });

    function TxnCode_OnClientSelectedIndexChanged() {
        var lbTxnCodeElement = $('#<%= lbTxnCode.ClientID%>');
        lbTxnCodeElement.html("Contingent Credit");
    }

    function rcbForeignCurrency_OnClientSelectedIndexChanged() {
        var ForeignCurrencyElement = $find("<%= rcbForeignCurrency.ClientID%>");
        //ForeignCurrencyElement.val('USD');
        var DealRateElement = $find("<%= tbDealRate.ClientID%>");
        DealRateElement.set_value(17791.92);
        OnCurrencyMatch();
    }

    function rcbAccount_OnClientSelectedIndexChanged() {
        var AccountElement = $find("<%= rcbAccount.ClientID%>");

        var lbAccountElement = $('#<%= lbAccount.ClientID%>');
        lbAccountElement.html("");

        if (AccountElement.get_value() != "") {
            lbAccountElement.html("Other valuable papers");
        }
        OnCurrencyMatch();
    }

    function showmessageTrungCurrency() {
        radconfirm("Currency Deposited and Cash Account is not matched", confirmCallbackFunction2);
    }

    function OnCurrencyMatch() {
        var currencyDepositedElement = $find("<%= rcbForeignCurrency.ClientID %>");
        var currencyDepositedValue = currencyDepositedElement.get_value();
        var cashAccountElement = $find("<%= rcbAccount.ClientID %>");
        var cashAccountValue = cashAccountElement.get_value();
        if (currencyDepositedValue && cashAccountValue && (currencyDepositedValue != cashAccountValue)) {
            //alert("Currency Deposited and Account is not matched");
            showmessageTrungCurrency();
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

    function ChargeAmountFCYOnValueChanged() {
        var ChargeAmountFCYElement = $find("<%= tbAmountFCY.ClientID%>");
        var ChargeAmountFCY = ChargeAmountFCYElement.get_value();

        var ChargeAmountLCYElement = $find("<%= txtAmountLCY.ClientID%>");
        var DealRateElement = $find("<%= tbDealRate.ClientID%>");

        if (ChargeAmountFCY) {
            var DealRate = DealRateElement.get_value();
            var sotien = DealRate * ChargeAmountFCY;
            ChargeAmountLCYElement.set_value(sotien);
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
        var TxnCode = $('#<%= rcbTxnCode.ClientID%>').val();
        var Sign = $('#<%= rcbSign.ClientID%>').val();

        Page_IsValid = TxnCode != "" && Sign != "";
    }

    function OnClientButtonClicking(sender, args) {
        ValidatorUpdateIsValid();
        if (Page_IsValid) {
            var button = args.get_item();

            //if (button.get_commandName() == "commit" && !clickCalledAfterRadconfirm) {
            //    args.set_cancel(true);
            //    lastClickedItem = args.get_item();
            //    radconfirm("Credit Till Closing Balance", confirmCallbackFunction2);
            //}

            //if (button.get_commandName() == "authorize" && !clickCalledAfterRadconfirm) {
            //    radconfirm("Authorised Completed", confirmCallbackFunction2);
            //}
        }
    }

    var lastClickedItem = null;
    var clickCalledAfterRadprompt = false;
    var clickCalledAfterRadconfirm = false;

    function confirmCallbackFunction1(args) {
        radconfirm("Unauthorised overdraft of USD on account 050001688331", confirmCallbackFunction2); //" + amtFCYDeposited + "
    }

    function confirmCallbackFunction2(args) {
        clickCalledAfterRadconfirm = true;lastClickedItem.click();
        lastClickedItem = null;
    }

    function Customer_OnClientSelectedIndexChanged() {
        var customerElement = $find("<%= rcbCustomerID.ClientID %>");

        var customerNameElement = $find("<%= tbCustomerName.ClientID %>");
        customerNameElement.set_value(customerElement.get_selectedItem().get_attributes().getAttribute("CustomerName"));

        var AddressElement = $find("<%= tbAddress.ClientID %>");
        AddressElement.set_value(customerElement.get_selectedItem().get_attributes().getAttribute("Address"));

        var legalIdElement = $find("<%= tbLegalID.ClientID %>");
        legalIdElement.set_value(customerElement.get_selectedItem().get_attributes().getAttribute("IdentityNo"));

        var PlaceOfIssElement = $find("<%= tbPlaceOfIss.ClientID %>");
        PlaceOfIssElement.set_value(customerElement.get_selectedItem().get_attributes().getAttribute("IssuePlace"));

        var datesplit = customerElement.get_selectedItem().get_attributes().getAttribute("IssueDate").split('/');
        var IsssuedDateElement = $find("<%= tbIsssuedDate.ClientID %>");
        IsssuedDateElement.set_selectedDate(new Date(datesplit[2].substring(0, 4), datesplit[0], datesplit[1]));
    }

</script>