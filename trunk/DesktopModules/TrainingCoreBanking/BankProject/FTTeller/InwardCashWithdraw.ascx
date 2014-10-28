<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InwardCashWithdraw.ascx.cs" Inherits="BankProject.FTTeller.InwardCashWithdraw" %>
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
       OnButtonClick="RadToolBar1_ButtonClick" >
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
        <li><a href="#ChristopherColumbus">Account Transfer</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <fieldset>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Clearing ID <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator8"
                        ControlToValidate="rcbClearingID"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Clearing ID is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                    </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbClearingID"
                        MarkFirstMatch="True"
                        AllowCustomText="false" Width="220"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="rcbClearingID_OnSelectedIndexChanged"
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
                <td class="MyLable">Debit Currency
                    </td>
                <td class="MyContent">
                    <asp:Label ID="lbDebitCurrency" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
         <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Debit Account
                    </td>
                <td class="MyContent">
                    <asp:Label ID="lbDebitAccount" runat="server"></asp:Label> 
                </td>
            </tr>
        </table>

         <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Debit Amt LCY
                    </td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox id="tbDebitAmtLCY" runat="server" borderwidth="0" readonly="true"></telerik:RadNumericTextBox>
                   
                </td>
                 <td class="MyLable">Debit Amt FCY
                    </td>
                <td class="MyContent">
                   <telerik:RadNumericTextBox id="tbDebitAmtFCY" runat="server" borderwidth="0" readonly="true"></telerik:RadNumericTextBox>
                </td>
            </tr>
        </table>
                          
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Deal rate</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtDealRate" runat="server" NumberFormat-DecimalDigits="5" >
                        <ClientEvents onblur="Caculate_DebitAmt" />
                    </telerik:RadNumericTextBox>
                </td>
                <td class="MyContent" style="visibility:hidden;">
                    <telerik:RadNumericTextBox ID="tbAmount" runat="server" NumberFormat-DecimalDigits="4" ></telerik:RadNumericTextBox>
                </td>
            </tr>
        </table>
                </fieldset>
          <fieldset id="Fieldset3" runat="server">
         <legend>
              <div style="font-weight:bold; text-transform:uppercase;"><asp:Label ID="Label1" runat="server" Text="Credit Information"></asp:Label></div>
                                </legend>    
        <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
                <td class="MyLable">Credit Account <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator4"
                        ControlToValidate="rcbCreditAccount"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Credit Account is Required" ForeColor="Red"></asp:RequiredFieldValidator>

                </td>
                <td class="MyContent"  >
                    <telerik:RadComboBox ID="rcbCreditAccount"
                        MarkFirstMatch="True"
                        AllowCustomText="false" onClientSelectedIndexChanged="CheckCurrency_Match"
                        AppendDataBoundItems="true"
                        runat="server" Width="350" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                    <asp:Label ID="lblNote" runat="server" />
                </td>
            </tr>
        </table>
            
        <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Credit Currency
                    </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbCreditCurrency"
                        MarkFirstMatch="True" onClientSelectedIndexChanged="CheckCurrency_Match"
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
                <td class="MyLable">Credit Amt LCY</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox id="tbCreditAmtLCY" runat="server" borderwidth="0" readonly="true"></telerik:RadNumericTextBox>
                </td>

                <td class="MyLable">Credit Amt FCY</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox id="tbCreditAmtFCY" runat="server" borderwidth="0" readonly="true"></telerik:RadNumericTextBox>
                </td>
            </tr>
        </table>
                          
         </fieldset>

        <fieldset id="Fieldset1" runat="server">
         <legend>
              <div style="font-weight:bold; text-transform:uppercase;"><asp:Label ID="Label2" runat="server" Text="Sending Information"></asp:Label></div>
                                </legend>    
         
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                 <td class="MyLable">BO Name</td>
                <td class="MyContent"><telerik:RadTextBox ID="txtBOName" runat="server" Width="350" ></telerik:RadTextBox></td>
            </tr>
        </table>
                                 
            </fieldset>
 
  <fieldset id="Fieldset2" runat="server">
         <legend>
              <div style="font-weight:bold; text-transform:uppercase;"><asp:Label ID="Label3" runat="server" Text="Beneficiary Information"></asp:Label></div>
                                </legend>   

      <table width="100%" cellpadding="0" cellspacing="0">
          <tr>
              <td class="MyLable">FO Name</td>
              <td class="MyContent">
                  <telerik:radtextbox runat="server" id="txtFOName"  width="350" ></telerik:radtextbox> 
              </td>
          </tr>
          </table>
      <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Identity Card</td>
                <td class="MyContent"><telerik:RadTextBox ID="txtIdentityCard" runat="server" Width="160"></telerik:RadTextBox></td>
                <td class="MyLable">Isssue Date</td>
                <td class="MyContent"><telerik:RadDatePicker ID="txtIsssueDate" runat="server" Width="160"></telerik:RadDatePicker></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Telephone</td>
                <td class="MyContent"><telerik:RadMaskedTextBox ID="tbPhone" runat="server" Mask="###########"
                        EmptyMessage="-- Enter Phone Number --" HideOnBlur="true" ZeroPadNumericRanges="true" DisplayMask="###########">
                    </telerik:RadMaskedTextBox></td>
                <td class="MyLable">Isssue Place</td>
                <td class="MyContent"><telerik:RadTextBox ID="txtIsssuePlace" runat="server" Width="160"></telerik:RadTextBox></td>
            </tr>
        </table>
      </fieldset>
                <fieldset>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Narrative <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="txtNarrative"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Narrative is Required" ForeColor="Red"></asp:RequiredFieldValidator>

                </td>
                <td class="MyContent" style="width:350px; ">
                    <telerik:RadTextBox ID="txtNarrative" Width="350"
                        runat="server"  />
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
            <tr>
                <td class="MyLable"> 
                </td>
                <td class="MyContent" style="width:350px; ">
                    <telerik:RadTextBox ID="txtNarrative2" Width="350"
                        runat="server"  />
                </td>
                <td></td>
            </tr>
        </table>
            </fieldset>
</div>
</div>
<asp:HiddenField ID="hdfDisable" runat="server" Value="0" />
<telerik:RadCodeBlock id="codeblock" runat="server">
<script type="text/javascript">
    function Caculate_DebitAmt(sender, args)
    {
        var DealRate = $find("<%=txtDealRate.ClientID%>").get_value();
        var Amount = $find("<%=tbAmount.ClientID%>").get_value();
        var CreditCurrency = $find("<%=rcbCreditCurrency.ClientID%>").get_value();
        var CreditAcct_Currency = $find("<%=rcbCreditAccount.ClientID%>").get_selectedItem().get_text().substring(0, 3);
        var lblNote = $('#<%=lblNote.ClientID%>');
        var DebitCurrency = $('#<%=lbDebitCurrency.ClientID%>').html();
        var DebitAmtLCY = $find("<%=tbDebitAmtLCY.ClientID%>"); 
        var DebitAmtFCY = $find("<%=tbDebitAmtFCY.ClientID%>"); 
        var CreditAmtLCY = $find("<%=tbCreditAmtLCY.ClientID%>"); 
        var CreditAmtFCY = $find("<%=tbCreditAmtFCY.ClientID%>"); 
        if (DealRate == "") DealRate = 0;
        if (CreditAcct_Currency && CreditAcct_Currency == "VND" && CreditAcct_Currency == CreditCurrency)
        {
            $find("<%=tbDebitAmtLCY.ClientID%>").set_value((DealRate * Amount).toLocaleString("en-US"));
            $find("<%=tbCreditAmtLCY.ClientID%>").set_value((DealRate * Amount).toLocaleString("en-US"));
            DebitAmtFCY.set_value(""); CreditAmtFCY.set_value("");
        }
        if (CreditAcct_Currency && CreditAcct_Currency != "VND" && CreditAcct_Currency == CreditCurrency)
        {
            DebitAmtFCY.set_value((DealRate * Amount).toLocaleString("en-US")); DebitAmtLCY.set_value("");
            CreditAmtFCY.set_value((DealRate * Amount).toLocaleString("en-US")); CreditAmtLCY.set_value("");
        }
        if (DebitCurrency == "VND"  && CreditAcct_Currency == CreditCurrency && DebitCurrency == CreditCurrency)
        {
            DealRate = 1;
            $find("<%=tbDebitAmtLCY.ClientID%>").set_value((DealRate * Amount).toLocaleString("en-US")); DebitAmtFCY.set_value("");
            $find("<%=tbCreditAmtLCY.ClientID%>").set_value((DealRate * Amount).toLocaleString("en-US")); CreditAmtFCY.set_value("");
        }
        if(DebitCurrency != "VND"  && CreditAcct_Currency == CreditCurrency &&  CreditCurrency !="VND")
        {
            $find("<%=tbDebitAmtFCY.ClientID%>").set_value((DealRate * Amount).toLocaleString("en-US")); DebitAmtLCY.set_value("");
            $find("<%=tbCreditAmtFCY.ClientID%>").set_value((DealRate * Amount).toLocaleString("en-US")); CreditAmtLCY.set_value("");
        }
    }
    
    var lastClickedItem = null;
    var clickCalledAfterRadprompt = false;
    var clickCalledAfterRadconfirm = false;

    function confirmCallbackFunction1(args) {
        radconfirm("Unauthorised overdraft of USD on account 050001688331", confirmCallbackFunction2); //" + amtFCYDeposited + "
    }
   
    //function confirmCallbackFunction2(args) {
    //    if (args) {
    //        clickCalledAfterRadconfirm = true;
    //        lastClickedItem.click();
    //        lastClickedItem = null;
    //    }
    //}
    function confirmCallbackFunction2(args) {
        clickCalledAfterRadconfirm = true;
        if (lastClickedItem != null) {
            lastClickedItem.click();
            lastClickedItem = null;
        }
    }

    function confirmcallfail() {
        $('#<%= hdfDisable.ClientID%>').val(0);//cancel thi gan disable = 0 de khoi commit
        confirmCallbackFunction2();
    }
    function CheckCurrency_Match(sender, args)
    {
        var CreditCurrency = $find("<%=rcbCreditCurrency.ClientID%>").get_value();
        var CreditAcct_Currency = "";
        CreditAcct_Currency = $find("<%=rcbCreditAccount.ClientID%>").get_selectedItem().get_text().substring(0,3);
        var lblNote = $('#<%=lblNote.ClientID%>');
        var DebitCurrency = $('#<%=lbDebitCurrency.ClientID%>').html();
        var DebitAmtLCY = $find("<%=tbDebitAmtLCY.ClientID%>"); DebitAmtLCY.set_value("");
        var DebitAmtFCY = $find("<%=tbDebitAmtFCY.ClientID%>"); DebitAmtFCY.set_value("");
        var CreditAmtLCY = $find("<%=tbCreditAmtLCY.ClientID%>"); CreditAmtLCY.set_value("");
        var CreditAmtFCY = $find("<%=tbCreditAmtFCY.ClientID%>"); CreditAmtFCY.set_value("");
        var Amount = $find("<%=tbAmount.ClientID%>").get_value();

        lblNote.html();
        if (CreditCurrency && CreditAcct_Currency && CreditCurrency == CreditAcct_Currency)// && DebitCurrency == CreditCurrency
        {
            lblNote.html("RECORD.AUTOMATICALLY.OPENED");
            if (DebitCurrency == CreditCurrency) //currency giong nhau thi show so tien ra
            {
                if (DebitCurrency == "VND") {
                    DebitAmtLCY.set_value(Amount.toLocaleString("en-US")); CreditAmtLCY.set_value(Amount.toLocaleString("en-US"));
                } else { DebitAmtFCY.set_value(Amount.toLocaleString("en-US")); CreditAmtFCY.set_value(Amount.toLocaleString("en-US")); }//loai tien te #
            } else { Caculate_DebitAmt();} //goi ham tinh toan ty gia cho truong hop debit va credit khong cung currency
        }
        else {
            lblNote.html("");
            $find("<%=rcbCreditCurrency.ClientID%>").set_value("");
            if (CreditCurrency != CreditAcct_Currency && CreditCurrency && CreditAcct_Currency)
            { ShowMessageCurrencyNotMath(); }
        }
    }
        function ShowMessageCurrencyNotMath() {
            radconfirm("Credit Currency and Credit Account are not matched", confirmCallbackFunction2);
        }
  </script>
    </telerik:RadCodeBlock>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbClearingID">      
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lbDebitCurrency" />
                 <telerik:AjaxUpdatedControl ControlID="lbDebitAccount" />
                 <telerik:AjaxUpdatedControl ControlID="txtBOName" />
                 <telerik:AjaxUpdatedControl ControlID="txtFOName" />
                 <telerik:AjaxUpdatedControl ControlID="txtIdentityCard" />
                 <telerik:AjaxUpdatedControl ControlID="txtIsssueDate" />
                 <telerik:AjaxUpdatedControl ControlID="tbPhone" />   
                 <telerik:AjaxUpdatedControl ControlID="txtIsssuePlace" />
                 <telerik:AjaxUpdatedControl ControlID="tbAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        </AjaxSettings>
</telerik:RadAjaxManager>