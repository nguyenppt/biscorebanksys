<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChequeTransferDrawnOnUs.ascx.cs" Inherits="BankProject.Views.TellerApplication.ChequeTransferDrawnOnUs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<telerik:RadWindowManager id="RadWindowManager1" runat="server"  EnableShadow="true" />
<asp:ValidationSummary ID="ValidationSummary" ValidationGroup="Commit" runat="server" ShowMessageBox="true" ShowSummary="false" />

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>

<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
       OnButtonClick="OnRadToolBarClick">
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
<div>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="tbID" runat="server" Width="200"/> </td>
        <td> <i> <asp:Label ID="lblCheqTransWithdrawn" runat="server" /></i></td>
    </tr>
</table>
</div>
<div id="tabs-demo" class="dnnForm"> 
    <ul class="dnnAdminTabNav">
        <li><a href="#Main">Cheque Transfer Withdrawal</a></li>
    </ul>
    <div id="Main" class="dnnClear">
        <fieldset>
            <legend style="text-transform:uppercase; font-weight:bold;">Debit Information </legend>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Debit Customer:</td>
                    <td class="MyContent" width="90px"><telerik:RadTextBox readonly="true" BOrderWidth="0" ID="tbCustomerID" runat="server" /></td>
                    <td class="MyContent">
                    <telerik:RadTextBox ID="tbCustomerName" readonly="true" BOrderWidth="0" runat="server" /></td>
                </tr>
               </table>
                
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Debit Currency:</td>
                    
                  <td class="MyContent">
                    <telerik:RadComboBox ID="rcbDebitCurrency" 
                        AppendDataBoundItems="true" 
                        Width="150"
                        MarkFirstMatch="True" AllowCustomText="false" runat="server" ValidationGroup="Group1">
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
                <td class="MyLable">Debit Account:<span class="Required"> (*)</span>
                    <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator1"
                     ControlToValidate="tbDebitAcct" ValidationGroup="Commit" InitialValue="" ErrorMessage="Debit Account is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> 
                </td>
                <td class="MyContent" width="400px">
                    <telerik:RadTextBox id="tbDebitAcct" runat="server" width="250"/>
                    <asp:Label ID="lblNote" runat="server" />
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
        </table>
            <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Debit Amount:</td>
                <td class="MyContent" >
                    <telerik:RadNumericTextBox ID="tbDebitAmountLCY" runat="server" Width="150"  NumberFormat-DecimalDigits="2"
                        ValidationGroup="Group1" ClientEvents-OnValueChanged="tbDebitAmountLCY_ClientEventsOnValueChanged"></telerik:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Cheque Type:<span class="Required">(*)</span> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" 
                     ControlToValidate="rcbChequeType" ValidationGroup="Commit" InitialValue="" ErrorMessage="Cheque Type is required"
                    ForeColor="Red" />
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbChequeType"
                        MarkFirstMatch="True" AppendDataBoundItems="true"
                        AllowCustomText="false" Width="250" onClientSelectedIndexchanged="FillNarrative"
                        runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
            <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Cheque No:</td>
                <td class="MyContent" width="250">
                    <telerik:RadNumericTextBox ID="tbChequeNo" Width="250" runat="server" ValidationGroup="Group1" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator=""
                         ClientEvents-OnValueChanged="tbChequeNo_OnValueChanged"  />
                </td>
                <td class="MyLable"><%--<a class="add"> <img src="Icons/Sigma/Add_16X16_Standard.png"></a>--%></td>
                <td class="MyContent"></td>
            </tr>
        </table>
             <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Old Customer Balance:</td>
                <td class="MyContent"><telerik:RadNumericTextBox ID="tbOldCustBal" readOnly="true" BorderWidth="0" runat="server" /></td>
            </tr>
            <tr>
                <td class="MyLable">New Customer Balance:</td>
                <td class="MyContent"><telerik:RadNumericTextBox ID="tbNewCustBal" readOnly="true" BorderWidth="0" runat="server" /></td>
            </tr>
            <tr>
                <td class="MyLable">Value Date:</td>
                <td class="MyContent" >
                    <telerik:RadDatePicker ID="rdpValueDate" runat="server" Width="150" />
                </td>
            </tr>
        </table>
        </fieldset>
        <fieldset>
            <legend style="font-weight:bold; text-transform:uppercase">Credit Information</legend>
           
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr style="visibility:hidden;">
                <td class="MyLable">Credit Customer:</td>
                <td ><asp:TextBox ID="tbCreditCustomerID" readOnly="true" BorderWidth="0" runat="server" /></td>
                <td width="350">
                    <asp:textBox ID="tbCreditCustomerName" readOnly="true" BorderWidth="0" runat="server" ></asp:textBox></td>
            </tr>
            <tr>
                <td class="MyLable">Credit Currency:<span class="Required">(*)</span></td>
                <asp:RequiredFieldValidator ID="rcbCreditCurrency123" runat="server" Display="None" 
                     ControlToValidate="rcbCreditCurrency" ValidationGroup="Commit" InitialValue="" ErrorMessage="Credit Currency is required"
                    ForeColor="Red" />
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbCreditCurrency" AppendDataBoundItems="true"
                        MarkFirstMatch="True" width="150"
                        AllowCustomText="false" 
                        AutoPostBack="true"
                        OnSelectedIndexChanged="rcbCreditCurrency_SelectedIndexChanged"
                        OnClientSelectedIndexChanged="OnMatchCurrencyValue"
                        runat="server" ValidationGroup="Group1">
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
            <tr hidden="hidden" >
                <td class="MyLable">Smartbank Branch:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbSmartBankBranch"
                        MarkFirstMatch="True" 
                        AllowCustomText="false" Width="320"
                        runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="0" Text="Chi Nhanh Tan Binh" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Credit Account:<span class="Required">(*)</span> </td>
                <asp:RequiredFieldValidator ID="rcbCreditAccount333" runat="server" Display="None" 
                     ControlToValidate="rcbCreditAccount" ValidationGroup="Commit" InitialValue="" ErrorMessage="Credit Currency is required"
                    ForeColor="Red" />
                <td class="MyContent" >
                    <telerik:RadComboBox ID="rcbCreditAccount" 
                        MarkFirstMatch="True" AppendDataBoundItems="true" Width="320"
                        AllowCustomText="false" OnClientSelectedIndexChanged="OnMatchCurrencyValue"
                        runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td>
                    <asp:Label ID="lblCreditAccountNote" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Deal Rate:</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="tbDealRate" runat="server" ValidationGroup="Group1"   
                              ClientEvents-OnValueChanged="tbDebitAmountLCY_ClientEventsOnValueChanged"
                        NumberFormat-DecimalDigits="5"></telerik:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Exposure Date:</td>
                <td class="MyContent">
                    <telerik:RadDatePicker ID="rdpExposureDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Amt Credit for Cust:</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="tbAmtCreditForCust" ReadOnly="true" BorderWidth="0"
                        runat="server" ValidationGroup="Group1"  />
                </td>
            </tr>

            <tr>
                <td class="MyLable">Value Date:</td>
                <td class="MyContent">
                    <telerik:RadDatePicker ID="rdpCreditValueDate" runat="server" />
                </td>
            </tr>
        </table>
               
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Waive Charges:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbWaiveCharges"
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
                <td class="MyLable">Narrative:</td>
                <td class="MyContent" width="300">
                    <telerik:RadTextBox ID="tbNarrative" Width="350" textMode="MultiLine" height="100"
                        runat="server" ValidationGroup="Group1" />
                </td>
                <td class="MyLable"><%--<a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a>--%></td>
                <td class="MyContent"></td>
            </tr>
        </table>
            </fieldset>

        <fieldset>
            <legend style="font-weight:bold; text-transform:uppercase;">Beneficiary Information</legend>
            <table width="100%" cellpading="0" cellspacing="0">
                <tr>
                   <td class="MyLable">Beneficiary Name:</td>
                   <td class="MyContent">
                       <telerik:RadTextBox ID="tbBeneName" runat="server" 
                         textMode="MultiLine" height="100" ClientEvents-OnValueChanged="FillNarrative" ValidationGroup="Group1" Width="350" />
                   </td>
                   <td class="MyLable"></td>
                   <td class="MyContent"></td>
               </tr>
                <tr>
                   <td class="MyLable">Address:</td>
                   <td class="MyContent">
                        <telerik:RadTextBox ID="tbAddress" runat="server" ClientEvents-OnValueChanged="FillNarrative" ValidationGroup="Group1" Width="350" />
                   </td>
                   
               </tr>
                <tr>
                   <td class="MyLable">Legal ID:</td>
                   <td class="MyContent">
                        <telerik:RadTextBox ID="tbLegalID" runat="server" ClientEvents-OnValueChanged="FillNarrative" ValidationGroup="Group1" Width="350" />
                   </td>
                   <td class="MyLable"></td>
                   <td class="MyContent"></td>
               </tr>
                <tr>
                   <td class="MyLable">Issued Date:</td>
                   <td class="MyContent">
                       <telerik:RadDatePicker ID="rdpIssDate" runat="server" ClientEvents-OnDateSelected="FillNarrative" validationGroup="Group1" />
                   </td>
                   <td class="MyLable">Place of Issue:</td>
                   <td class="MyContent">
                       <telerik:RadTextBox ID="tbPlaceOfIss" runat="server" ClientEvents-OnValueChanged="FillNarrative" ValidationGroup="Group1" />
                   </td>
               </tr>
                <tr style="visibility:hidden;">
                    <td> <telerik:radnumerictextbox id="tbChequeEnd" runat="server" />
                <telerik:radnumerictextbox id="tbChequeStart" runat="server" /> </td>
               
                    </tr>
                </table>
        </fieldset>
    </div>
</div>

<telerik:RadCodeBlock id="codebloac" runat="server">
<script type="text/javascript">
    
    /////// ham tinh toan cho gia tri deal rate : ////////////
    function getDealRate()
    {
        var dealrateElement = $find("<%=tbDealRate.ClientID%>");
        var dealrateValue = dealrateElement.get_value();
        var debitCurElement = $find("<%=rcbDebitCurrency.ClientID%>");
        var debitCur = debitCurElement.get_value();
        var creditCurElement = $find("<%=rcbCreditCurrency.ClientID%>");
        var creditCur = creditCurElement.get_value();
        if (!dealrateValue || creditCur == debitCur) {
            dealrateValue = 1;
        }
        return dealrateValue;
    }

    //////// tinh chuyen doi tien te //////////// 
    function tbDebitAmountLCY_ClientEventsOnValueChanged() {
        //// khai bao cac element va gia tri cho cac field can su dung
        var debitAmtLCYElement = $find("<%= tbDebitAmountLCY.ClientID%>");
        var debitAmtLCY = debitAmtLCYElement.get_value();
        var creditCurrencyElement = $find("<%= rcbCreditCurrency.ClientID%>");
        var creditCurrencyValue = creditCurrencyElement.get_value();
        var debitCurrency = $find("<%=rcbDebitCurrency.ClientID%>").get_value();
        var debitAccount = $find("<%=tbDebitAcct.ClientID%>").get_value();
        var OldCusBalElement = $find("<%=tbOldCustBal.ClientID%>");
        var OldCusBal = OldCusBalElement.get_value();
        var NewCustBalElement = $find("<%= tbNewCustBal.ClientID%>");
        var creditAccount = $find("<%=rcbCreditAccount.ClientID%>").get_value();
        var AmtPaidToCustElement = $find("<%=tbAmtCreditForCust.ClientID%>"); 
        // tinh gia tri cho Amt Credit For Customer //////
        if (debitCurrency && debitAccount) {
            if (OldCusBal < debitAmtLCY)
            {
                showMessage();
                debitAmtLCYElement.set_value("");
            }
            else
            {
                var dealrate = 0;
                dealrate = getDealRate();
                var NewCustBal = OldCusBal - debitAmtLCY;
                NewCustBalElement.set_value(NewCustBal.toLocaleString("en-US"));
                if (creditAccount && creditCurrencyValue) {
                    AmtPaidToCustElement.set_value((dealrate * debitAmtLCY).toLocaleString("en-US"));
                } else
                {
                    AmtPaidToCustElement.set_value("");
                }

            }
        } else {
            NewCustBalElement.set_value("");
            AmtforCustElement.set_value("");
            oldCusBalElement.set_value("");
            CustomerIDElement.set_value("");
            CustomerNameElement.set_value("");
        }
    }


    ////////////// so sanh tai khoan va loai tien te ////////////
    function OnMatchCurrencyValue(e)
    {
        var CreditAccountElement = $find("<%=rcbCreditAccount.ClientID%>");
        var CreditAccount = CreditAccountElement.get_value();
        var CreditCurrencyElement = $find("<%= rcbCreditCurrency.ClientID %>");
        var CreditCurreny = CreditCurrencyElement.get_value();
        var debitCurrencyElement = $find("<%=rcbDebitCurrency.ClientID%>");
        var debitCurrency = debitCurrencyElement.get_value();
        var debitAmtLCYElement = $find("<%= tbDebitAmountLCY.ClientID%>");
        var debitAmtLCY = debitAmtLCYElement.get_value();
        var creditAmtForCustElement = $find("<%=tbAmtCreditForCust.ClientID%>");
       
        //////// neu cung loai tien te thi khong can them ty gia, gia tri cuoi = gia tri dau luon !!!
        if (debitCurrency && CreditCurreny && (CreditCurreny == debitCurrency))
        { creditAmtForCustElement.set_value(debitAmtLCY.toLocaleString("en-US", { useGrouping: true, minimumFractionDigits: 2, maximumFractionDigits: 2 })); }

        ShowLabelAutoRecord();
        // OnChangeDealRate();
        return true;
    }

    function ShowLabelAutoRecord() {
        var CreditAccountElement = $find("<%= rcbCreditAccount.ClientID %>");
        var CreditAccountValue = CreditAccountElement.get_value();

        var AutoRecordElement = $('#<%=lblCreditAccountNote.ClientID%>');
        if (CreditAccountValue) {
            AutoRecordElement.html("RECORD.AUTOMATICALLY.OPENED");
        }
        else {
            AutoRecordElement.html("");
        }
    }
    
    function showMessage() {
        var debitCurElement = $find("<%=rcbDebitCurrency.ClientID%>");
        var debitCurValue = debitCurElement.get_value();
        var oldCustBalance = $find("<%=tbOldCustBal.ClientID%>").get_value();
        radconfirm("Can’t overdraft. Maximum debit amount is " + oldCustBalance.toLocaleString("en-US", { useGrouping: true, minimumFractionDigits: 2, maximumFractionDigits: 2 }) + " " + debitCurValue, confirmCallbackFunction2);
    }
    function confirmCallbackFunction1(args) {
        radconfirm("Unauthorised overdraft of USD on account 050001688331", confirmCallbackFunction2); //" + amtFCYDeposited + "
    }

    function confirmCallbackFunction2(args) {
        clickCalledAfterRadconfirm = true;
        lastclickedItem.click();
        lastclickedItem = null;
        var debitAmtLCYElement = $find("<%= tbDebitAmountLCY.ClientID%>");
        debitAmtLCYElement.focus();
        debitAmtLCYElement.set_value("");
    }
    function tbChequeNo_OnValueChanged(sender, args) {
        var ChequeStart = $find("<%=tbChequeStart.ClientID%>").get_value();
        var ChequeEnd = $find("<%=tbChequeEnd.ClientID%>").get_value();
        var ChequeNo = $find("<%=tbChequeNo.ClientID%>").get_value();
        if (ChequeNo < ChequeStart || ChequeNo > ChequeEnd) {
            radconfirm("Cheque No does not exists, please check again, ChequeNo must be within scale " + ChequeStart + " - " + ChequeEnd + " !", confirmCallbackFunction3);
        }
    }
    function confirmCallbackFunction3(args) {
        clickCalledAfterRadconfirm = true;
        if (lastClickedItem != null) {
            lastClickedItem.click();
            lastClickedItem = null;
        }
        var ChequeNo = $find("<%=tbChequeNo.ClientID%>");
        ChequeNo.focus();
        ChequeNo.set_value("");
    }
    
    function FillNarrative() {
        var tbBeneName = $find("<%=tbBeneName.ClientID%>");
         var tbAddress = $find("<%=tbAddress.ClientID%>");
         var tbLegalID = $find("<%=tbLegalID.ClientID%>");
         var rdpIssDate = $find("<%=rdpIssDate.ClientID%>");
         var tbPlaceOfIss = $find("<%=tbPlaceOfIss.ClientID%>");

        var tbDebitAcct = $find("<%=tbDebitAcct.ClientID%>");
         var rcbChequeType = $find("<%=rcbChequeType.ClientID%>");
         var tbChequeNo = $find("<%=tbChequeNo.ClientID%>");

        var strNarr = "RUT SEC Chuyen Khoang " + rcbChequeType.get_value() + ": " + tbChequeNo.get_value() + " TK " + tbDebitAcct.get_value() + " NN: ";

         if (tbBeneName.get_value()) {
             strNarr += " - " + tbBeneName.get_value();
         }

         if (tbLegalID.get_value()) {
             strNarr += " - " + tbLegalID.get_value();
         }

         if (rdpIssDate.get_selectedDate() != null && rdpIssDate.get_selectedDate() != "") {
             strNarr += " - " + rdpIssDate.get_selectedDate().toLocaleString("en-US");
         }

         if (tbPlaceOfIss.get_value()) {
             strNarr += " - " + tbPlaceOfIss.get_value();
         }

         if (tbAddress.get_value()) {
             strNarr += " - " + tbAddress.get_value();
         }
         $find("<%=tbNarrative.ClientID%>").set_value(strNarr);
    }
    $('#<%=tbDebitAcct.ClientID%>').keyup(function (event) {
        if (event.keyCode == 13) { $("#<%=btSearch.ClientID%>").click(); }
    });
</script>
</telerik:RadCodeBlock >
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbCreditCurrency">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="rcbCreditAccount" />
            </UpdatedControls>
        </telerik:AjaxSetting> 
    </AjaxSettings>
</telerik:RadAjaxManager>
<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" Text="Search" onclick="btSearch_Click" />
</div>