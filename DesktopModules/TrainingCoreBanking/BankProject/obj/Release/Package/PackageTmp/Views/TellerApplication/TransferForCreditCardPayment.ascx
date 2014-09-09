<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransferForCreditCardPayment.ascx.cs" Inherits="BankProject.Views.TellerApplication.TransferForCreditCardPayment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />


<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>
<div>    
     
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
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
<asp:ValidationSummary ID="ValidationSummary" runat="server" showmessagebox="true"
     ShowSummary="false" ValidationGroup="Commit" />


<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtID" runat="server" Width="200" />
            <i>
                <asp:Label ID="lblID" runat="server" /></i></td>
    </tr>
</table>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Transfer For Credit Card Payment</a></li>
        <li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <fieldset>
            <legend style="text-transform:uppercase; font-weight:bold;">Debit Information</legend>

        <div class="dnnClear">
            <table width="100%" cellpadding="0" cellspacing="0">
                
                <tr>
                    <td class="MyLable">Customer:</td>
                    <td width="80px">
                        <asp:Label ID="lblCustomerID" runat="server" /></td>
                    <td>
                        <asp:Label ID="lblCustomerName" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Currency:</td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbDebitCurrency"
                            MarkFirstMatch="True" 
                            AllowCustomText="false"
                            runat="server" ValidationGroup="Group1" OnClientSelectedIndexChanged="OnIndexChanged_rcbDebitAccount" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                                <telerik:RadComboBoxItem Value="GBP" Text="GBP" />
                                <telerik:RadComboBoxItem Value="JPY" Text="JPY" />
                                <telerik:RadComboBoxItem Value="USD" Text="USD" />
                                <telerik:RadComboBoxItem Value="VND" Text="VND" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Debit Account:<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" display="None" 
                            ControltoValidate="rcbDebitAccount" ValidationGroup="Commit" InitialValue="" ErrorMessage="Debit Account is Required !"
                            ForeColor="Red" />
                    </td>
                    <td class="MyContent" width="300">
                        <telerik:RadComboBox ID="rcbDebitAccount" OnClientSelectedIndexChanged="OnIndexChanged_rcbDebitAccount"
                            MarkFirstMatch="True" 
                            AllowCustomText="false"
                            Width="300" runat="server" ValidationGroup="Group1">
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="0" Text="07.000259529.8 - TGTT" />
                                <telerik:RadComboBoxItem Value="1" Text="07.000259530.8 - TGTT" />
                                <telerik:RadComboBoxItem Value="2" Text="07.000259540.8 - TGTT" />
                                <telerik:RadComboBoxItem Value="3" Text="07.000259545.8 - TGTT" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td><a class="add">
                        <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Debit Amt:</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="txtDebitAmtLCY" Width="300"
                            NumberFormat-DecimalDigits="2"
                            runat="server" ValidationGroup="Group1"  
                            ClientEvents-OnValueChanged="txtDebitAmtLCYOnValueChanged" />
                        
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Next Trans Com:</td>
                </tr>
                <tr>
                    <td class="MyLable">Old Customer Balance:</td>
                    <td><asp:Label ID="lblOldCustBal" runat="server" />  </td>
                <tr>
                    <td class="MyLable">New Customer Balance:</td>
                    <td>
                        <asp:Label runat="server" ID="lblNewCustBal" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Value Date:</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="rdpValueDate" Width="150" runat="server" ValidationGroup="Group1" />
                    </td>
                </tr>
             <tr>
                   <%-- <td class="MyLable">Old Cus Acc:</td>--%>
                </tr>
            </table>
            </fieldset>
        <fieldset>
            <legend style="font-weight:bold; text-transform:uppercase" >Credit Information</legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Customer:</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCreditCustomerName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Credit Account:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" display="None" 
                            ControltoValidate="rcbDebitAccount" ValidationGroup="Commit" InitialValue="" 
                            ForeColor="Red" ErrorMessage="Credit Account is Required !"/>
                    </td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbCreditAccount" 
                            MarkFirstMatch="True"
                            AllowCustomText="false"
                            Width="300" runat="server" ValidationGroup="Group1" OnClientSelectedIndexChanged="OnCurrencyMatch" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="EUR" Text="EUR-19322-0002-0001 - Thu ho, chi ho EUR" />
                                <telerik:RadComboBoxItem Value="GBP" Text="GBP-12321-0002-0001 - Thu ho, chi ho GBP" />
                                <telerik:RadComboBoxItem Value="JPY" Text="JPY-14325-0002-0001 -  Thu ho, chi ho JPY" />
                                <telerik:RadComboBoxItem Value="USD" Text="USD-12346-0002-0001 - Thu ho, chi ho USD" />
                                <telerik:RadComboBoxItem Value="VND" Text="VND-22215-0002-0001 - Thu ho, chi ho VND" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                     <td>
                        <asp:Label ID="lblAutoRecord" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Currency:</td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbCreditCurrency"
                            MarkFirstMatch="True" 
                            AllowCustomText="false" Height="150"
                            runat="server" ValidationGroup="Group1" OnClientSelectedIndexChanged="OnCurrencyMatch" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                                <telerik:RadComboBoxItem Value="GBP" Text="GBP" />
                                <telerik:RadComboBoxItem Value="JPY" Text="JPY" />
                                <telerik:RadComboBoxItem Value="USD" Text="USD" />
                                <telerik:RadComboBoxItem Value="VND" Text="VND" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                   
                </tr>
                <tr>
                    <td class="MyLable">Deal Rate:</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="txtDealRate" runat="server" ValidationGroup="Group1" NumberFormat-DecimalDigits="6"
                              ClientEvents-OnValueChanged="txtDebitAmtLCYOnValueChanged">
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Amt Credit for Cust:</td>
                    <td>
                        <asp:label ID="lblAmForCust" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Value Date:</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="rdpValueDate2" runat="server" ValidationGroup="Group1" />
                    </td>
                </tr>
            </table>
            </fieldset>

        <fieldset>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Credit Card Number:<span class="Required">(*)</span> 
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" display="None" 
                            ControltoValidate="tbCreditCardNum" ValidationGroup="Commit" InitialValue="" 
                            ForeColor="Red" ErrorMessage="Credit Card Number is Required !"/>
                    </td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="tbCreditCardNum" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator=""
                             runat="server" ValidationGroup="Group1" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Waive Charges ?:</td>
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
                    <td class="MyLable">Narative:</td>
                    <td class="MyContent" width="300">
                        <telerik:RadTextBox ID="txtNarrative" Width="300"
                            runat="server" ValidationGroup="Group1" />
                    </td>
                    <td><a class="add">
                        <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>
            </fieldset>
        </div>
    </div>
    <div id="blank" />

<script type="text/javascript">
    function OnIndexChanged_rcbDebitAccount() {
         var lblCustomerIDElement = $('#<%=lblCustomerID.ClientID%>');
        var lblCustomerNameElement = $('#<%=lblCustomerName.ClientID%>');
        var lblCreditCustomerNameElement = $('#<%=lblCreditCustomerName.ClientID%>');

         var debitAccountElement = $find("<%= rcbDebitAccount.ClientID %>");
        var debitAccountValue = debitAccountElement.get_value();

        if (debitAccountValue.length == 0 || !debitAccountValue.trim()) {
            var NewCustBalElement = $('#<%= lblNewCustBal.ClientID%>');
            var amtforcustElement = $('#<%= lblAmForCust.ClientID%>');
            NewCustBalElement.html("");
            amtforcustElement.html("");
        }
        else
        {
            switch (debitAccountValue)
            {
                case "0":
                    lblCustomerIDElement.html("1101532");
                    lblCustomerNameElement.html("TRAN NHAT TAN");
                    lblCreditCustomerNameElement.html("TRAN NHAT TAN");
                    
                    break;
                case "1":
                    lblCustomerIDElement.html("1100002");
                    lblCustomerNameElement.html("DINH TIEN HOANG");
                    lblCreditCustomerNameElement.html("DINH TIEN HOANG");
                    break;
                case "2":
                    lblCustomerIDElement.html("2102925");
                    lblCustomerNameElement.html("CONG TY TNHH SONG HONG");
                    lblCreditCustomerNameElement.html("CONG TY TNHH SONG HONG");
                    break;
                case "3":
                    lblCustomerIDElement.html("1100005");
                    lblCustomerNameElement.html("TRUONG CONG DINH");
                    lblCreditCustomerNameElement.html("TRUONG CONG DINH");
                    break;

            }
            // lay gia tri va` hien len man hinh gia tri cua old Cus Balance, nho khai bao bien va chu y 
            // pham vi su dung cua cac bien cuc bo
            var oldCusBalElement = $('#<%=lblOldCustBal.ClientID%>');
            oldCusBalElement.html("");
            var oldcusBalValue = getOldCustBallance();
            oldCusBalElement.html(oldcusBalValue.toLocaleString("en-US"));
        }

        //gan cho credit account va readonly
        var DebitcurrencyDepositedElement = $find("<%= cmbDebitCurrency.ClientID %>");
        if (DebitcurrencyDepositedElement) {
            var CreditAccountElement = $find("<%= cmbCreditAccount.ClientID %>");
            var CreditCurrency = $find("<%= cmbCreditCurrency.ClientID %>")
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

                CreditCurrency.trackChanges();
                CreditCurrency.get_items().getItem(debititem.get_index()).select();
                CreditCurrency.updateClientState();
                CreditCurrency.commitChanges();
            }
        }
    }

    /////// ham tinh toan cho gia tri deal rate : ////////////
    function getDealRate()
    {
        var dealrateElement = $find("<%=txtDealRate.ClientID%>");
        var dealrateValue = dealrateElement.get_value();
        var debitCurElement = $find("<%=cmbDebitCurrency.ClientID%>");
        var debitCur = debitCurElement.get_value();
        var creditCurElement = $find("<%=cmbCreditCurrency.ClientID%>");
        var creditCur = creditCurElement.get_value();
        var debitAmountElement = $find("<%=txtDebitAmtLCY.ClientID%>");
        var debitAmount = debitAmountElement.get_value();
        var creditAmountElement = $('#<%=lblAmForCust.ClientID%>');
        
        if (!dealrateValue || creditCur == debitCur)
        {
            dealrateValue = 1;
            
        }
        return dealrateValue;
    }
   
    //////// tinh chuyen doi tien te ////////////
    function txtDebitAmtLCYOnValueChanged() {
         //// khai bao cac element va gia tri cho cac field can su dung
        var debitAmtLCYElement = $find("<%= txtDebitAmtLCY.ClientID%>");
        var debitAmtLCY = debitAmtLCYElement.get_value();
        var creditCurrencyElement = $find("<%= cmbCreditCurrency.ClientID%>");
        var creditCurrencyValue = creditCurrencyElement.get_value();
        var dealrateElement = $find("<%= txtDealRate.ClientID%>");
        var oldCusBalElement = $('#<%=lblOldCustBal.ClientID%>');
        var oldCusBalValue = getOldCustBallance();
        var creditAmtForCustElement = $('#<%=lblAmForCust.ClientID%>');  
        var newCusBalElement = $('#<%=lblNewCustBal.ClientID%>');
        newCusBalElement.html("");
        
        var dealrateValue="";       
        // tinh gia tri cho Amt Credit For Customer //////
        if (oldCusBalValue < debitAmtLCY ) { showMessage(); }
        else {
            
             dealrateValue = getDealRate();
            switch (creditCurrencyValue)
            {
                case "":
                    creditAmtForCustElement.html("");
                    break;
                case "VND":
                    var tempCreditAmt = dealrateValue * debitAmtLCY;
                    if (tempCreditAmt) { creditAmtForCustElement.html(tempCreditAmt.toLocaleString("en-US")); }
                default:
                    var tempCreditAmt = dealrateValue * debitAmtLCY;
                    if (tempCreditAmt) { creditAmtForCustElement.html(tempCreditAmt.toLocaleString("en-US", { useGrouping: true, minimumFractionDigits: 2, maximumFractionDigits: 2 })); }
            }
            /// tinh lai gia tri cho new cus Ballance
            if (debitAmtLCY) { newCusBalElement.html((oldCusBalValue - debitAmtLCY).toLocaleString("en-US")); }
        }
    }
       

    ////////////// so sanh tai khoan va loai tien te ////////////
    function OnCurrencyMatch(e) {
        var CreditAccountElement = $find("<%= cmbCreditAccount.ClientID %>");
        var CreditAccount = CreditAccountElement.get_value();
        var CreditCurrencyElement = $find("<%= cmbCreditCurrency.ClientID %>");
        var CreditCurreny = CreditCurrencyElement.get_value();
        var debitCurrencyElement = $find("<%=cmbDebitCurrency.ClientID%>");
        var debitCurrency = debitCurrencyElement.get_value();
        var debitAmtLCYElement = $find("<%= txtDebitAmtLCY.ClientID%>");
        var debitAmtLCY = debitAmtLCYElement.get_value();
        var creditAmtForCustElement = $('#<%=lblAmForCust.ClientID%>'); 

        //do tự nhay credit account nen không kiểm
        //if (CreditAccount && CreditCurreny && (CreditCurreny != CreditAccount)) {
        //    alert("Credit Account and Credit Curreny are not matched");
        //    CreditAccountElement.trackChanges();
        //    CreditAccountElement.get_items().getItem(0).select();
        //    CreditAccountElement.updateClientState();
        //    CreditAccountElement.commitChanges();

        //    CreditCurrencyElement.trackChanges();
        //    CreditCurrencyElement.get_items().getItem(0).select();
        //    CreditCurrencyElement.updateClientState();
        //    CreditCurrencyElement.commitChanges();
        //    return false;
        //}
        //////// neu cung loai tien te thi khong can them ty gia, gia tri cuoi = gia tri dau luon !!!
        if (debitCurrency && CreditCurreny && (CreditCurreny == debitCurrency)) 
        { creditAmtForCustElement.html(debitAmtLCY.toLocaleString("en-US")); }

        ShowLabelAutoRecord();
       // OnChangeDealRate();
        return true;
    }

    function ShowLabelAutoRecord() {
        var CreditAccountElement = $find("<%= cmbCreditAccount.ClientID %>");
        var CreditAccountValue = CreditAccountElement.get_value();

        var AutoRecordElement = $('#<%=lblAutoRecord.ClientID%>')
        if (CreditAccountValue) {
            //AutoRecordElement.html("RECORD.AUTOMATICALLY.OPENED");
        }
        else {
            AutoRecordElement.html("");
        }
    }
     
    

    function getOldCustBallance() {
        var debitCurElement = $find("<%=cmbDebitCurrency.ClientID%>");
         var debitCurValue = debitCurElement.get_value();
         var oldCusBal = 0;
         if (debitCurValue == "VND") oldCusBal = 1000000000; else oldCusBal = 50000;
         return oldCusBal;
    }
   
    function showMessage() {
        var oldCusBal = getOldCustBallance();
        var debitCurElement = $find("<%=cmbDebitCurrency.ClientID%>");
        var debitCurValue = debitCurElement.get_value();
        radconfirm("Can’t overdraft. Maximum debit amount is " + oldCusBal.toLocaleString("en-US") +" "+ debitCurValue.toLocaleString("en-US"), confirmCallbackFunction2);
    }
    function confirmCallbackFunction1(args)
        {
            radconfirm("Unauthorised overdraft of USD on account 050001688331", confirmCallbackFunction2); //" + amtFCYDeposited + "
        }
    
    function confirmCallbackFunction2(args)
    {
        clickCalledAfterRadconfirm = true;
        lastclickedItem.click();
        lastclickedItem = null;
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