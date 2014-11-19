<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InwardProcessCreditAcc.ascx.cs" Inherits="BankProject.FTTeller.InwardProcessCreditAcc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Controls/VVComboBox.ascx" TagPrefix="uc1" TagName="VVComboBox" %>
<%@ Register Src="../Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>
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
            <asp:TextBox ID="tbId" runat="server" Width="200" />
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
                        ControlToValidate="tbClearingID"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Clearing ID is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                    </td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbClearingID"
                        AutoPostBack="True"
                        OnTextChanged="tbClearing_TextChanged"
                        runat="server" />                        
                </td>
               
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Debit Currency
                    </td>
                <td class="MyContent">
                    <asp:Label ID="lblDebitCurrency" runat="server"></asp:Label>
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
                 <td class="MyLable"></td>
                 <td class="MyContent"></td>
            </tr>
             </table>
         <table width="100%" cellpadding="0" cellspacing="0">

             <tr>
                <td class="MyLable">Debit Amt LCY
                    </td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox id="tbDebitAmtLCY" runat="server" borderwidth="0" readonly="true"></telerik:RadNumericTextBox>
                </td>
                 </tr>
             <tr>
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
                             <ClientEvents onblur="Caculate_Amt" />
                    </telerik:RadNumericTextBox>
                </td>
                <td style="visibility:hidden;">
                    <telerik:RadNumericTextBox ID="tbDebitAmt" runat="server" NumberFormat-DecimalDigits="5" ></telerik:RadNumericTextBox>
                </td>
            </tr>
        </table>
                <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                 <td class="MyLable">BO Name</td>
                <td class="MyContent"><telerik:RadTextBox ID="txtBOName" runat="server" Width="350" ></telerik:RadTextBox></td>
            </tr>
        </table>
            </fieldset>
          <fieldset id="Fieldset3" runat="server">
         <legend>
              <div style="font-weight:bold; text-transform:uppercase;"><asp:Label ID="Label1" runat="server" Text="Credit Information"></asp:Label></div>
                                </legend>    
        <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Credit Currency
                    </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbCreditCurrency"
                        MarkFirstMatch="True" onClientSelectedIndexChanged="CurrencyMatch"
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
                        MarkFirstMatch="True" onClientSelectedIndexChanged="CurrencyMatch"
                        AllowCustomText="false"
                        AppendDataBoundItems="true"
                        runat="server" Width="250" >
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
                <td class="MyLable">Credit Amt LCY</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox id="tbCrAmtLCY" runat="server" borderwidth="0" readonly="true"></telerik:RadNumericTextBox>
                </td>
                </tr>
            <tr>
                <td class="MyLable">Credit Amt FCY</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox id="tbCrAmtFCY" runat="server" borderwidth="0" readonly="true"></telerik:RadNumericTextBox>
                </td>
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
                  <telerik:radtextbox runat="server" id="tbFOName" Width="350"></telerik:radtextbox>
              </td>
          </tr>
          <tr>
              <td class="MyLable"></td>
              <td class="MyContent">
                  <telerik:radtextbox runat="server" id="tbFOName2" Width="350"></telerik:radtextbox>
              </td>
          </tr>
        </table>
      <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Legal ID</td>
                <td class="MyContent"><telerik:RadTextBox ID="tbIdentityCard" runat="server" Width="160"></telerik:RadTextBox></td>
                <td class="MyLable">Isssue Date</td>
                <td class="MyContent"><telerik:RadDatePicker ID="tbIsssueDate" runat="server" Width="160"></telerik:RadDatePicker></td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Telephone</td>
                <td class="MyContent"><telerik:RadMaskedTextBox ID="tbPhone" runat="server" Mask="###########"
                        EmptyMessage="-- Enter Phone Number --" HideOnBlur="true" ZeroPadNumericRanges="true" DisplayMask="###########">
                    </telerik:RadMaskedTextBox></td>
                <td class="MyLable">Isssue Place</td>
                <td class="MyContent"><telerik:RadTextBox ID="tbIsssuePlace" runat="server" Width="160"></telerik:RadTextBox></td>
            </tr>
        </table>
      <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Narrative <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="tbNarrative"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Narrative is Required" ForeColor="Red"></asp:RequiredFieldValidator>

                </td>
                <td class="MyContent" style="width:350px; ">
                    <telerik:RadTextBox ID="tbNarrative" Width="350"
                        runat="server"  />
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
            <tr>
                <td class="MyLable"></td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbNarrative2" Width="350"
                        runat="server"  />
                </td>
            </tr>
        </table>
      </fieldset>
</div>
</div>
<asp:HiddenField ID="hdfDisable" runat="server" Value="0" />
<telerik:RadCodeBlock id="codeblock" runat="server" >
<script type="text/javascript">
    function CurrencyMatch(sender, args)
    {
        var CreditCurrency = "";
        CreditCurrency=  $find("<%=rcbCreditCurrency.ClientID%>").get_value();
        var Credit_Acct_Currency = "";
        Credit_Acct_Currency = $find("<%=rcbCreditAccount.ClientID%>").get_selectedItem().get_text().substring(0, 3);
        var lblNote = $('#<%=lblNote.ClientID%>');

        lblNote.html();
        if (CreditCurrency && Credit_Acct_Currency && CreditCurrency == Credit_Acct_Currency) {
            lblNote.html("RECORD.AUTOMATICALLY.OPENED");
            Caculate_Amt();
        } else
            if (CreditCurrency && Credit_Acct_Currency && CreditCurrency != Credit_Acct_Currency)
            {
                lblNote.html("");
                ShowMessageCurrencyNotMath();

            }

    }
    function Caculate_Amt(sender, args)
    {
        var CreditCurrency = $find("<%=rcbCreditCurrency.ClientID%>").get_value();
        var Credit_Acct_Currency = $find("<%=rcbCreditAccount.ClientID%>").get_selectedItem().get_text().substring(0, 3);
        var DebitCurrency = $('#<%=lblDebitCurrency.ClientID%>').html();
        var Amount = $find("<%=tbDebitAmt.ClientID%>").get_value();
        var Debit_Amt_LCY = $find("<%=tbDebitAmtLCY.ClientID%>"); Debit_Amt_LCY.set_value("");
        var Debit_Amt_FCY = $find("<%=tbDebitAmtFCY.ClientID%>"); Debit_Amt_FCY.set_value("");
        var Credit_Amt_LCY = $find("<%=tbCrAmtLCY.ClientID%>"); Credit_Amt_LCY.set_value("");
        var Credit_Amt_FCY = $find("<%=tbCrAmtFCY.ClientID%>"); Credit_Amt_FCY.set_value("");
        if (CreditCurrency && Credit_Acct_Currency && Credit_Acct_Currency == CreditCurrency)
        {
            if (CreditCurrency == "VND" || Credit_Acct_Currency == "VND") {
                if (DebitCurrency == "VND") { Debit_Amt_LCY.set_value(Amount.toLocaleString("en-US")); Credit_Amt_LCY.set_value(Amount.toLocaleString("en-US")); }
                else
                {
                    var dealrate_value = DealRate();
                    Debit_Amt_LCY.set_value((Amount * dealrate_value).toLocaleString("en-US"));
                    Credit_Amt_LCY.set_value((Amount * dealrate_value).toLocaleString("en-US"));
                }
            } else
            {
                var dealrate_value = DealRate();
                Debit_Amt_FCY.set_value((Amount * dealrate_value).toLocaleString("en-US"));
                Credit_Amt_FCY.set_value((Amount * dealrate_value).toLocaleString("en-US"));
            }
        } else
        {
            if (CreditCurrency || Credit_Acct_Currency)
            {
                var dealrate_value = DealRate();
                Debit_Amt_FCY.set_value((Amount * dealrate_value).toLocaleString("en-US"));
                Credit_Amt_FCY.set_value((Amount * dealrate_value).toLocaleString("en-US"));
            }
        }
    }
    function DealRate()
    {
        var CreditCurrency = $find("<%=rcbCreditCurrency.ClientID%>").get_value();
        var Credit_Acct_Currency = $find("<%=rcbCreditAccount.ClientID%>").get_selectedItem().get_text().substring(0, 3);
        var DebitCurrency = $('#<%=lblDebitCurrency.ClientID%>').html();
        var dealrate =$find("<%=txtDealRate.ClientID%>").get_value();
        if (CreditCurrency == Credit_Acct_Currency && DebitCurrency == Credit_Acct_Currency) dealrate = 1;
        return dealrate;
    }
    function confirmCallbackFunction1(args) {
        radconfirm("Unauthorised overdraft of USD on account 050001688331", confirmCallbackFunction2); //" + amtFCYDeposited + "
    }
   
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

    function OnChargeAmountValueChanged() {
        var AmountElement = $find("<%= tbDebitAmtLCY.ClientID%>");
        var Amount = AmountElement.get_value();

        var CreditCurrency = $find("<%= rcbCreditCurrency.ClientID%>");
        var DealRate = $find("<%= txtDealRate.ClientID%>");

        var dealratevalue = 1;

        if (DealRate.get_value() > 0 && rcbCreditCurrency != "VND") {
            dealratevalue = DealRate.get_value();
        }

        if (Amount) {
            var CrAmtLCY = $find("<%= tbCrAmtLCY.ClientID%>");
            var lcy = Amount * dealratevalue;
            CrAmtLCY.set_value(lcy.toLocaleString("en-US"));
        }
    }
        function ShowMessageCurrencyNotMath() {
            radconfirm("Credit Currency and Credit Account are not matched", confirmCallbackFunction2);
        }

  </script>
    </telerik:RadCodeBlock>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="tbClearingID">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblDebitCurrency" />
                <telerik:AjaxUpdatedControl ControlID="lbDebitAccount" />
                <telerik:AjaxUpdatedControl ControlID="tbDebitAmt" /> 
                <telerik:AjaxUpdatedControl ControlID="txtBOName" />
                <telerik:AjaxUpdatedControl ControlID="tbFOName" />
                <telerik:AjaxUpdatedControl ControlID="tbFOName2" />
                <telerik:AjaxUpdatedControl ControlID="tbIdentityCard" />
                <telerik:AjaxUpdatedControl ControlID="tbIsssueDate" />
                <telerik:AjaxUpdatedControl ControlID="tbPhone" />
                <telerik:AjaxUpdatedControl ControlID="tbIsssuePlace" />
            </UpdatedControls>
        </telerik:AjaxSetting> 
        </AjaxSettings>
    </telerik:RadAjaxManager>
