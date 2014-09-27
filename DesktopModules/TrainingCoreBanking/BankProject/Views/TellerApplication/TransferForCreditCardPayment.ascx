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
            <asp:TextBox ID="txtID" runat="server" Width="200" ReadOnly="true" />
            <i>
                <asp:Label ID="lblID" runat="server" /></i></td>
    </tr>
</table>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Transfer For Credit Card Payment</a></li>
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
                    <td class="MyLable">Debit Currency:</td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbDebitCurrency"
                            MarkFirstMatch="True" AutoPostBack="true" OnselectedIndexChanged="cmbDebitCurrency_OnselectedIndexChanged"
                            AllowCustomText="false"
                            runat="server" ValidationGroup="Group1" OnClientSelectedIndexChanged="OnIndexChanged_rcbDebitAccount" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Debit Account:<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" display="None" 
                            ControltoValidate="tbDebitAccount" ValidationGroup="Commit" InitialValue="" ErrorMessage="Debit Account is Required !"
                            ForeColor="Red" />
                    </td>
                    <td class="MyContent" width="300">
                        <telerik:RadTextBox ID="tbDebitAccount" 
                            Width="300" runat="server" ValidationGroup="Group1">
                        </telerik:RadTextBox>
                    </td>
                    <td class="MyContent">
                        <asp:Label ID="lblAccountTitle" runat="server" Width="250"></asp:Label>
                    </td>
                    <td class="MyContent"></td>
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
                    
                    <td><telerik:radnumerictextbox runat="server" id="tbOldBalance" borderwidth="0" readonly="true"></telerik:radnumerictextbox>
                       </td>
                <tr>
                    <td class="MyLable">New Customer Balance:</td>
                    <td><telerik:radnumerictextbox runat="server" id="tbNewBalance" borderwidth="0" readonly="true"></telerik:radnumerictextbox>
                        </td>
                </tr>
                <tr>
                    <td class="MyLable">Value Date:</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="rdpValueDate" Width="150" runat="server" ValidationGroup="Group1" />
                    </td>
                </tr>
             <tr>
                 
                </tr>
            </table>
            </fieldset>
        <fieldset>
            <legend style="font-weight:bold; text-transform:uppercase" >Credit Information</legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                
                <tr>
                    <td class="MyLable">Credit Account:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" display="None" 
                            ControltoValidate="cmbCreditAccount" ValidationGroup="Commit" InitialValue="" 
                            ForeColor="Red" ErrorMessage="Credit Account is Required !"/>
                    </td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbCreditAccount" 
                            MarkFirstMatch="True"
                            AllowCustomText="false"
                            Width="300" runat="server" ValidationGroup="Group1" OnClientSelectedIndexChanged="OnCurrencyMatch" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                     <td>
                        <asp:Label ID="lblAutoRecord" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Credit Currency:</td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbCreditCurrency" enabled="false"
                            MarkFirstMatch="True"  readOnly="true"
                            AllowCustomText="false" Height="150"
                            runat="server" ValidationGroup="Group1" OnClientSelectedIndexChanged="OnCurrencyMatch" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                   
                </tr>
                
                <tr>
                    <td class="MyLable">Amt Credit for Cust:</td>
                    <td>
                        <telerik:radnumerictextbox runat="server" id="tbAmtCredit" borderwidth="0" readonly="true" />
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
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
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
                <tr style="visibility:hidden;">
                    <td class="MyLable">Customer:</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCreditCustomerName" runat="server" />
                    </td>
                </tr>
                <tr style="visibility:hidden;">
                    <td class="MyLable">Deal Rate:</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="txtDealRate" runat="server" ValidationGroup="Group1" NumberFormat-DecimalDigits="5"
                              ClientEvents-OnValueChanged="txtDebitAmtLCYOnValueChanged">
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
            </table>
            </fieldset>
        </div>
    </div>
    <div id="blank" />
<telerik:RadCodeBlock runat="server">
<script type="text/javascript">
    $("#<%=tbDebitAccount.ClientID%>").keyup(function (event) {

        if (event.keyCode == 13) {
            $("#<%=btSearch.ClientID%>").click();
        }
    });
    function OnIndexChanged_rcbDebitAccount() {
        var lblCustomerIDElement = $('#<%=lblCustomerID.ClientID%>');
        var lblCustomerNameElement = $('#<%=lblCustomerName.ClientID%>');
        var lblCreditCustomerNameElement = $('#<%=lblCreditCustomerName.ClientID%>');

        var debitAccountElement = $find("<%= tbDebitAccount.ClientID %>");
        var debitAccountValue = debitAccountElement.get_value();
        var DebitCurrency = $find("<%=cmbDebitCurrency.ClientID%>").get_value();
        if (DebitCurrency.length == 0 || !DebitCurrency.trim()) {
            var NewCustBalElement = $find("<%= tbNewBalance.ClientID%>");
            var AmtCredit = $find("<%= tbAmtCredit.ClientID%>");
            NewCustBalElement.set_value("");
            AmtCredit.set_value("");
            $find("<%=tbOldBalance.ClientID%>").set_value("")
        }
        else
        {
            var oldCusBalElement = $find("<%=tbOldBalance.ClientID%>");
            oldCusBalElement.set_value("");
            var oldcusBalValue = getOldCustBallance();
            oldCusBalElement.set_value(oldcusBalValue.toLocaleString("en-US"));
        }
    }
    //////// tinh chuyen doi tien te ////////////
    function txtDebitAmtLCYOnValueChanged() {
         //// khai bao cac element va gia tri cho cac field can su dung
        var debitAmtLCYElement = $find("<%= txtDebitAmtLCY.ClientID%>");
        var debitAmtLCY = debitAmtLCYElement.get_value();
        var creditCurrencyElement = $find("<%= cmbCreditCurrency.ClientID%>");
        var creditCurrencyValue = creditCurrencyElement.get_value();
        var dealrateElement = $find("<%= txtDealRate.ClientID%>");
        var oldCusBalElement = $find("<%=tbOldBalance.ClientID%>");
        var oldCusBalValue = getOldCustBallance();
        var AmtCredit = $find("<%=tbAmtCredit.ClientID%>");  
        var newCusBalElement = $find("<%=tbNewBalance.ClientID%>");
        newCusBalElement.set_value("");
        // tinh gia tri cho Amt Credit For Customer //////
        if (oldCusBalValue < debitAmtLCY ) { showMessage(); }
        else {
             var NewBalance = oldCusBalValue - debitAmtLCY;
             AmtCredit.set_value(debitAmtLCY.toLocaleString("en-US"));
            /// tinh lai gia tri cho new cus Ballance
             if (debitAmtLCY) { newCusBalElement.set_value(NewBalance.toLocaleString("en-US")); }
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
        var creditAmtForCustElement = $find("<%=tbAmtCredit.ClientID%>"); 

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
        { creditAmtForCustElement.set_value(debitAmtLCY.toLocaleString("en-US")); }

        ShowLabelAutoRecord();
        return true;
    }

    function ShowLabelAutoRecord() {
        var CreditAccountElement = $find("<%= cmbCreditAccount.ClientID %>");
        var CreditAccountValue = CreditAccountElement.get_value();

        var AutoRecordElement = $('#<%=lblAutoRecord.ClientID%>')
        if (CreditAccountValue) {
        }
        else {
            AutoRecordElement.html("");
        }
    }
    function getOldCustBallance() {
         var oldCusBal = $find("<%=tbOldBalance.ClientID%>").get_value();
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
    </telerik:RadCodeBlock>
<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" />
</div>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>

        <telerik:AjaxSetting AjaxControlID="cmbDebitCurrency">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="cmbCreditCurrency" />
                 <telerik:AjaxUpdatedControl ControlID="cmbCreditAccount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>