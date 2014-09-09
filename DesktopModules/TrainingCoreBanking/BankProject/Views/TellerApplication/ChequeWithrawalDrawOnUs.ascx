<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChequeWithrawalDrawOnUs.ascx.cs" Inherits="BankProject.Views.TellerApplication.ChequeWithrawalDrawOnUs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<telerik:RadWindowManager id="RadWindowManager1" runat="server"  EnableShadow="true" />
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>

<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%"
         OnButtonClick="RadToolBar1_ButtonClick">
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
    <table width="100%" cellpadding="0" cellspacing="0" >
        <tr>
            <td style="width: 200px; padding: 5px 0 5px 20px;">
                <asp:TextBox ID="tbID" runat="server" ValidationGroup="Group1" />
                <i><asp:Label ID="lblID" runat="server" ></asp:Label></i>
            </td>
        </tr>
    </table>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server"  ShowMessageBox="true" ShowSummary="false" ValidationGroup="Commit" />

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="# blank1">Cheque Withdrawal</a></li>
    </ul>
    <div id="blank1" class="dnnClear">
        <table width="100%" cellpading="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer:</td>
                <td class="MyContent" width="90"><telerik:RadTextBox ID="tbCustomerID" readonly="true" BorderWidth="0" runat="server" /></td>
                <td class="MyContent" ><telerik:RadTextBox ID="tbCustomerName" readonly="true" BorderWidth="0" runat="server" /></td>
            </tr>
        </table>
        <fieldset>
            <legend></legend>
           <table width="100%" cellpading="0" cellspacing="0">
               <tr>
                   <td class="MyLable">Currency:<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator7"
                     ControlToValidate="rcbCurrency" ValidationGroup="Commit" InitialValue="" 
                    ErrorMessage="Account Customer is required"  ForeColor="Red" />
                   </td>
                   <td class="MyContent">
                        <telerik:RadComboBox ID="rcbCurrency"
                        MarkFirstMatch="True" AppendDataBoundItems="true"
                        AllowCustomText="false" 
                        runat="server" ValidationGroup="Group1" 
                            autopostback="true"
                            OnSelectedIndexChanged ="rcbCurrency_OnSelectedIndexChanged" 
                            OnClientSelectedIndexChanged="rcbAccCustomer_OnClientSelectedIndexChanged">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                   </td>
                   <td class="MyLable"></td>
                   <td class="MyContent"></td>
               </tr>
               <tr>
                   <td class="MyLable">Account Customer:<span class="Required"> (*)</span>
                    <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator1"
                     ControlToValidate="rcbAccCustomer" ValidationGroup="Commit" InitialValue="" 
                    ErrorMessage="Account Customer is required"  ForeColor="Red" /></td>
                   <td class="MyContent"  width="350">
                       <telerik:RadComboBox ID="rcbAccCustomer" runat="server" AllowCustomText="false"
                           width="350" MarkFirstMatch="true" onItemDataBOund="rcbAccCustomer_onItemDataBOund"
                           AppendDataBoundItems="true" OnClientSelectedIndexChanged="rcbAccCustomer_OnClientSelectedIndexChanged">
                           <CollapseAnimation Type="None" />
                           <ExpandAnimation Type="None" />
                       </telerik:RadComboBox></td>
                   <td>    
                  <%-- <a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a>--%>
                   </td>
                   
               </tr>
               </table>
           <table id="tblAmount" width="100%" cellpading="0" cellspacing="0">
                <tr>
                   <td class="MyLable">Amount LCY:</td>
                   <td class="MyContent">
                       <telerik:RadNumericTextBox id="tbAmountLocal" runat="server" validationGroup="Group1"  ClientEvents-OnValueChanged="AmountLocal_OnValueChanged" >
                         
                       </telerik:RadNumericTextBox>
                   </td>
               </tr>
               
                <tr>
                   <td class="MyLable">Old Customer Balance:</td>
                   <td class="MyContent">
                       <telerik:RadNumericTextBox ID="tbOldCustBal" borderWidth="0" readonly="true" runat="server" ValidationGroup="Group1" />
                   </td>
               </tr>
                <tr>
                   <td class="MyLable">New Customer Balance:</td>
                   <td class="MyContent">
                       <telerik:RadNumericTextBox ID="tbNewCustBal" borderWidth="0" readonly="true" runat="server" ValidationGroup="Group1" />
                   </td>
               </tr>

                <tr>
                   <td class="MyLable">Cheque Type:<span class="Required">(*)</span>
                       <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator2"
                     ControlToValidate="rcbChequeType" ValidationGroup="Commit" InitialValue="" 
                    ErrorMessage="Cheque Type is required"  ForeColor="Red" />
                   </td>
                   <td class="MyContent" width="300">
                       <telerik:RadComboBox ID="rcbChequeType" runat="server"  AllowCustomText="false"
                        MarkFirstMatch="true"  AppendDataBoundItems="true" Width="300">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>               
                   </td>
                   
               </tr>
                <tr>
                   <td class="MyLable">Cheque No:<span class="Required">(*)</span>
                       <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator3"
                     ControlToValidate="tbChequeNo" ValidationGroup="Commit" InitialValue="" 
                    ErrorMessage="Cheque No is required"  ForeColor="Red"  />
                   </td>
                   <td class="MyContent">
                       <telerik:RadNumericTextBox ID="tbChequeNo" runat="server" NumberFormat-DecimalDigits="0"
                          ClientEvents-OnValueChanged="tbChequeNo_OnValueChanged"   NumberFormat-GroupSeparator="" width="200"/>
                   </td>
                   <td class="MyLable"><%--<a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a>--%></td>
                   <td class="MyContent"></td>
               </tr>
           </table>
        </fieldset>
        <fieldset>
            <legend></legend>
            <table width="100%" cellpading="0" cellspacing="0">
                <tr>
                   <td class="MyLable">Teller ID:<span class="Required">(*)</span>
                       <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator4"
                     ControlToValidate="tbTellerID" ValidationGroup="Commit" InitialValue="" 
                    ErrorMessage="Teller ID is required"  ForeColor="Red" />
                   </td>
                   <td class="MyContent">
                       <telerik:RadTextBox ID="tbTellerID" runat="server" ValidationGroup="Group1" />
                   </td>
                   <td class="MyLable"></td>
                   <td class="MyContent"></td>
               </tr>
                <tr>
                   <td class="MyLable">Currency Paid:<span class="Required">(*)</span>
                       <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator5"
                     ControlToValidate="rcbCurrencyPaid" ValidationGroup="Commit" InitialValue="" 
                    ErrorMessage="Currency Paid is required"  ForeColor="Red" />
                   </td>
                   <td class="MyContent">
                       <telerik:RadComboBox ID="rcbCurrencyPaid" runat="server" AllowCustomText="false" AUtoPostBack="true" 
                           OnSelectedIndexChanged="rcbCurrencyPaid_OnSelectedIndexChanged" OnClientSelectedIndexchanged="Caculate_AmtPaid"
                        MarkFirstMatch="true"  AppendDataBoundItems="true" >
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" /> 
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>                                     
                   </td>
                   <td class="MyLable"></td>
                   <td class="MyContent"></td>
               </tr>
                <tr>
                   <td class="MyLable">Account Paid: <span class="Required">(*)</span>
                       <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator6"
                     ControlToValidate="rcbAccountPaid" ValidationGroup="Commit" InitialValue="" 
                    ErrorMessage="Account Paid is required"  ForeColor="Red" />
                   </td>
                   <td class="MyContent"> 
                       <telerik:RadComboBox ID="rcbAccountPaid" runat="server" AllowCustomText="false" OnClientSelectedIndexchanged="Caculate_AmtPaid"
                        MarkFirstMatch="true"  AppendDataBoundItems="true" width="350">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>                                     
                   </td>
                   <td class="MyContent">
                       <asp:label ID="lblAccountPaidNote" runat="server" />
                   </td>
                   <td class="MyContent"></td>
               </tr>
                <tr>
                   <td class="MyLable">Deal Rate:</td>
                   <td class="MyContent">
                       <telerik:RadNumericTextBox ID="tbDealRate" runat="server" ValidationGroup="Group1" Numberformat-decimaldigits="5"
                            ClientEvents-OnValueChanged="AmountLocal_OnValueChanged">
                          
                       </telerik:RadNumericTextBox>
                   </td>
                   <td class="MyLable"></td>
                   <td class="MyContent"></td>
               </tr>
                <tr>
                   <td class="MyLable">Amt Paid to Cust:</td>
                   <td class="MyContent">
                       <telerik:RadNumericTextBox ID="tbAmtPaidToCust" readOnly="true" borderWidth="0" runat="server" ValidationGroup="Group1" />
                      
                   </td>
                   <td class="MyLable"></td>
                   <td class="MyContent"></td>
               </tr>
                <tr>
                   <td class="MyLable">Waive Charges:</td>
                   <td class="MyContent">
                        <telerik:RadComboBox ID="rcbWaiveCharges" runat="server" AllowCustomText="false"
                        MarkFirstMatch="true"  AppendDataBoundItems="true" >
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="YES" Text="YES" />
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                        </Items>
                    </telerik:RadComboBox>    
                   </td>
                   <td class="MyLable"></td>
                   <td class="MyContent"></td>
               </tr>
                <tr>
                   <td class="MyLable">Narrative:</td>
                   <td class="MyContent" width="350">
                       <telerik:RadTextBox ID="tbNarrative" runat="server" ValidationGroup="Group1" textMode="Multiline"
                           Text="" width="350"/>
                   </td>
                   <td class="MyLable"><%--<a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a>--%></td>
                   
               </tr>
           </table>
        </fieldset>
        <fieldset>
            <legend style="font-weight:bold; text-transform:uppercase;">Beneficiary Information</legend>
            <table width="100%" cellpading="0" cellspacing="0">
                <tr>
                   <td class="MyLable">Beneficiary Name:</td>
                   <td class="MyContent">
                       <telerik:RadTextBox ID="tbBeneName" runat="server" ClientEvents-OnValueChanged="FillNarrative" ValidationGroup="Group1" 
                          textMode="Multiline" Width="350" />
                   </td>
                   <td class="MyLable"></td>
                   <td class="MyContent"></td>
               </tr>
                <tr>
                   <td class="MyLable">Address:</td>
                   <td class="MyContent">
                        <telerik:RadTextBox ID="tbAddress" runat="server" ClientEvents-OnValueChanged="FillNarrative" ValidationGroup="Group1" 
                            textMode="Multiline" Width="350" />
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
                       <telerik:RadDatePicker ID="rdpIssDate" runat="server" ClientEvents-OnDateSelected="FillNarrative" validationGroup="Group1" >
                           <DateInput DateFormat="MM/dd/yyyy" runat="server"> 
            </DateInput> 
                       </telerik:RadDatePicker>
                   </td>
                   <td class="MyLable">Place of Issue:</td>
                   <td class="MyContent">
                       <telerik:RadTextBox ID="tbPlaceOfIssue" runat="server" ClientEvents-OnValueChanged="FillNarrative" ValidationGroup="Group1" />
                   </td>
               </tr>
                </table>
        </fieldset>
    </div>
</div>
    <telerik:radcodeblock runat="server">
<script type="text/javascript">
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

    ////khai bao bien de lay ten khach hang ////

    function rcbAccCustomer_OnClientSelectedIndexChanged() {
        var AcctCustomerElement = $find("<%=rcbAccCustomer.ClientID%>");
        var AcctCustomer = AcctCustomerElement.get_value();
        var CustomerIDElement = $find("<%=tbCustomerID.ClientID%>");
        var CustomerNameElement = $find("<%=tbCustomerName.ClientID%>");
        var CurrencyElement = $find("<%=rcbCurrency.ClientID%>");
        var CurrencyValue = CurrencyElement.get_value();
        var OldCustBalElement = $find("<%=tbOldCustBal.ClientID%>");
        var NewCustBalElement = $find("<%=tbNewCustBal.ClientID%>");
        var AmtPaidtoCustElement = $find("<%=tbAmtPaidToCust.ClientID%>");
        var AmtLocalElement = $find("<%=tbAmountLocal.ClientID%>");
        
        OldCustBalElement.set_value(""); // change currency thi clear cac gia tri truoc' do'
        NewCustBalElement.set_value("");
        AmtPaidtoCustElement.set_value("");
        AmtLocalElement.set_value(""); // end clear
        if (AcctCustomer.length == 0 || !AcctCustomer.trim()) {// clear text box
            CustomerIDElement.set_value("");
            CustomerNameElement.set_value("");
            OldCustBalElement.set_value("");
            NewCustBalElement.set_value("");
            AmtPaidtoCustElement.set_value("");
            AmtLocalElement.set_value("");
            $find("<%=tbNarrative.ClientID%>").set_value("");
            $find("<%=tbBeneName.ClientID%>").set_value("");
            $find("<%=tbAddress.ClientID%>").set_value("");
            $find("<%=tbPlaceOfIssue.ClientID%>").set_value("");
            $find("<%=tbLegalID.ClientID%>").set_value("");
            $find("<%=rdpIssDate.ClientID%>").set_selectedDate(null);
        }
        else {
            var OldCustBal = AcctCustomerElement.get_selectedItem().get_attributes().getAttribute("WorkingAmount");
            OldCustBalElement.set_value(OldCustBal.toLocaleString("en-US"));
            var CustomerID = AcctCustomerElement.get_selectedItem().get_attributes().getAttribute("CustomerID");
            CustomerIDElement.set_value(CustomerID);
            var CustomerName = AcctCustomerElement.get_selectedItem().get_attributes().getAttribute("CustomerName");
            CustomerNameElement.set_value(CustomerName);
            $find("<%=tbBeneName.ClientID%>").set_value(CustomerName);
            $find("<%=tbAddress.ClientID%>").set_value(AcctCustomerElement.get_selectedItem().get_attributes().getAttribute("Address"));
            $find("<%=tbLegalID.ClientID%>").set_value(AcctCustomerElement.get_selectedItem().get_attributes().getAttribute("DocID"));
            $find("<%=tbPlaceOfIssue.ClientID%>").set_value(AcctCustomerElement.get_selectedItem().get_attributes().getAttribute("DocIssuePlace"));
            if (AcctCustomerElement.get_selectedItem().get_attributes().getAttribute("DocIssueDate") != "") {
                var datesplit = AcctCustomerElement.get_selectedItem().get_attributes().getAttribute("DocIssueDate").split('/');
                $find("<%=rdpIssDate.ClientID%>").set_selectedDate(new Date(datesplit[2].substring(0, 4), datesplit[0], datesplit[1]));
            }
        }
        if (CurrencyValue.length != 0) {// xoa het khi currency = null
            
        } else
        {
            CustomerIDElement.set_value("");
            CustomerNameElement.set_value("");
            OldCustBalElement.set_value("");
            NewCustBalElement.set_value("");
            AmtPaidtoCustElement.set_value("");
            AmtLocalElement.set_value("");
            $find("<%=tbNarrative.ClientID%>").set_value("");
            $find("<%=tbBeneName.ClientID%>").set_value("");
            $find("<%=tbAddress.ClientID%>").set_value("");
            $find("<%=tbPlaceOfIssue.ClientID%>").set_value("");
            $find("<%=tbLegalID.ClientID%>").set_value("");
            $find("<%=rdpIssDate.ClientID%>").set_selectedDate(null);
        }
    }
    
    function getOldCustBalance() {
        var CurrencyElement = $find("<%=rcbCurrency.ClientID%>");
        var Currency = CurrencyElement.get_value();
        var OldCustBal = 0;
       
        var AccountCustomerElement= $find("<%=rcbAccCustomer.ClientID%>");
        var AcountCustomer = AccountCustomerElement.get_value();
    }
   
    ////// tinh lai gia tri moi cho tai khoan New Cust Balance va chuyen doi tien te /////
    function AmountLocal_OnValueChanged(sender, args) {
        var AmountLocalElement = $find("<%= tbAmountLocal.ClientID%>");
        var AmountLocal = AmountLocalElement.get_value();

        var OldCustBalanceElement = $find("<%=tbOldCustBal.ClientID%>");
        var OldCustBalance = $find("<%=tbOldCustBal.ClientID%>").get_value();
        var NewCustBalanceElement = $find("<%=tbNewCustBal.ClientID%>");
        var CurrencyPaidElement = $find("<%=rcbCurrencyPaid.ClientID%>");
        var CurrencyPaidValue = CurrencyPaidElement.get_value();
        var AmtPaidToCust = $find("<%=tbAmtPaidToCust.ClientID%>");
        var AccountPaidElement = $find("<%=rcbAccountPaid.ClientID%>");
        var AccountPaid = AccountPaidElement.get_value();

        var AcctCustomerElement = $find("<%=rcbAccCustomer.ClientID%>");// lay gia tri AcctCustomer,CurrencyValue de check, phong truong hop client chua chon 
        var AcctCustomer = AcctCustomerElement.get_value();             // ma nhap gia tri cho Amount Local
        var CurrencyElement = $find("<%=rcbCurrency.ClientID%>");
        var CurrencyValue = CurrencyElement.get_value();
        if (AcctCustomer && CurrencyValue) {
            if ((AmountLocal) && (OldCustBalance < AmountLocal)) {
                ShowMessage();
                AmountLocalElement.set_value("");
            }
            else {
                var dealrate = 0;
                dealrate = getDealRate();
                NewCustBalanceElement.set_value((OldCustBalance - AmountLocal).toLocaleString("en-US"));
                if (!AmountLocal) { AmountLocal = 0; }
                if (CurrencyPaidValue && AccountPaid) {
                    AmtPaidToCust.set_value((dealrate * AmountLocal).toLocaleString("en-US"));
                }
            }
        }
        else
        {
            radconfirm("Check Currency, Account Customer and Amount Local again.", confirmCallbackFunction2);
        }
    }
    function Caculate_AmtPaid(sender, args)
    {
        var AutoRecordElement = $('#<%=lblAccountPaidNote.ClientID%>');
        AutoRecordElement.html("");// gan mac dinh la null;
        $find("<%=tbAmtPaidToCust.ClientID%>").set_value("");
        $find("<%=tbDealRate.ClientID%>").set_value("");
        var CurrencyPaid = $find("<%=rcbCurrencyPaid.ClientID%>").get_value();
        var Currency = $find("<%=rcbCurrency.ClientID%>").get_value();
        var AccountPaid = $find("<%=rcbAccountPaid.ClientID%>").get_value();
        
        if (Currency && CurrencyPaid && Currency == CurrencyPaid && AccountPaid) {
            //tinh Amount paid
            $find("<%=tbAmtPaidToCust.ClientID%>").set_value($find("<%=tbAmountLocal.ClientID%>").get_value().toLocaleString("en-US"));
        } else { $find("<%=tbAmtPaidToCust.ClientID%>").set_value(""); }
        if (AccountPaid)
        {
            AutoRecordElement.html("RECORD.AUTOMATICALLY.OPENED");
        } else { AutoRecordElement.html(""); }
    }
    function tbChequeNo_OnValueChanged(sender, args)
    {
        var ChequeStart = $find("<%=rcbAccCustomer.ClientID%>").get_selectedItem().get_attributes().getAttribute("ChequeNoStart");
        var ChequeEnd = $find("<%=rcbAccCustomer.ClientID%>").get_selectedItem().get_attributes().getAttribute("ChequeNoEnd");
        var ChequeNo = $find("<%=tbChequeNo.ClientID%>").get_value();
        if (ChequeNo < ChequeStart || ChequeNo > ChequeEnd)
        {
            radconfirm("Cheque No does not exists, please check again, ChequeNo must be within scale "+ChequeStart+" - "+ChequeEnd+" !", confirmCallbackFunction3);
        }
    }
    ////////// lay gia tri deal rate ///////////////
    function getDealRate() {
        var dealrateElement = $find("<%=tbDealRate.ClientID%>");
        var dealrateValue = dealrateElement.get_value();
        var CurrencyElement = $find("<%=rcbCurrency.ClientID%>");
        var CurrencyValue = CurrencyElement.get_value();
        var CurrencyPaidElement = $find("<%=rcbCurrencyPaid.ClientID%>");
        var CurrencyPaidValue = CurrencyPaidElement.get_value();

        if (dealrateValue == null || CurrencyValue == CurrencyPaidValue)
        { dealrateValue = 1; }
        return dealrateValue;
    }
    
    function showLabelAutoRecord() {
        var AccountPaidElement = $find("<%=rcbAccountPaid.ClientID%>");
        var AccountPaid = AccountPaidElement.get_value();
        var AutoRecordElement = $('#<%=lblAccountPaidNote.ClientID%>');
        if (AccountPaid) { AutoRecordElement.html("RECORD.AUTOMATICALLY.OPENED"); }
        else { AutoRecordElement.html(""); }
    }

    function ShowMessage() {
        var OldCustBallance = $find("<%=tbOldCustBal.ClientID%>").get_value();
        var CurrencyElement = $find("<%=rcbCurrency.ClientID%>");
        var Currency = CurrencyElement.get_value();
        radconfirm("Can't overdraft. Maximum Amount Local is " + OldCustBallance.toLocaleString("en-US") + " " + Currency.toLocaleString("en-US"), confirmCallbackFunction2);
    }
    var lastClickedItem = null;
    var clickCalledAfterRadprompt = false;
    var clickCalledAfterRadconfirm = false;


    function confirmCallbackFunction2(args) {
        clickCalledAfterRadconfirm = true;
        if (lastClickedItem != null) {
            lastClickedItem.click();
            lastClickedItem = null;
        }

        //Dat sau khi da hoan thanh confirm xong thi moi set focus duoc o text box local Amount
        var LocalAmtElement = $find("<%=tbAmountLocal.ClientID%>");
        LocalAmtElement.focus();
        LocalAmtElement.set_value("");
    }
    function confirmCallbackFunction3(args) { 
        clickCalledAfterRadconfirm = true;      
        if (lastClickedItem != null)
        {
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
        var tbPlaceOfIss = $find("<%=tbPlaceOfIssue.ClientID%>");

        var rcbAccCustomer = $find("<%=rcbAccCustomer.ClientID%>");
        var rcbChequeType = $find("<%=rcbChequeType.ClientID%>");
        var tbChequeNo = $find("<%=tbChequeNo.ClientID%>");

        var strNarr = "RUT SEC TM " + rcbChequeType.get_value() + ": " + tbChequeNo.get_value() + " TK " + rcbAccCustomer.get_text() + " NN: ";

        if (tbBeneName.get_value()) {
            strNarr += tbBeneName.get_value();
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
   
</script>
    </telerik:radcodeblock>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbCurrency">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="rcbAccCustomer" />
            </UpdatedControls>
        </telerik:AjaxSetting> 
        <telerik:AjaxSetting AjaxControlID="rcbCurrencyPaid">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="rcbAccountPaid" />
            </UpdatedControls>
        </telerik:AjaxSetting> 
    </AjaxSettings>
</telerik:RadAjaxManager>
