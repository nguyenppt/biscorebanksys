<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransferByAccount.ascx.cs" Inherits="BankProject.Views.TellerApplication.TransferByAccount" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register Src="../../Controls/VVComboBox.ascx" TagPrefix="uc1" TagName="VVComboBox" %>
<%@ Register Src="../../Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>

<telerik:RadWindowManager id="RadWindowManager1" runat="server"  EnableShadow="true" />
<asp:ValidationSummary ID="ValidationSummary" ValidationGroup="Commit" runat="server" ShowMessageBox="true" ShowSummary="false" />

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
       OnButtonClick="OnRadToolBarClick"   OnClientButtonClicking="OnClientButtonClicking" >
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
        <td> <i> <asp:Label ID="lblID" runat="server" /></i></td>
    </tr>
</table>
</div>
<div id="tabs-demo" class="dnnForm">
    <ul class="dnnAdminTabNav">
        <li><a href="#Main">Transfer by Account</a></li>
        
    </ul>
    <div id="Main" class="dnnClear">
        <fieldset>
            <legend style="text-transform:uppercase; font-weight:bold;">Debit Information</legend>
            <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Product ID:<span class="Required">(*) </span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidators" runat="server"  Display="None" ValidationGroup="Commit" ControlToValidate="rcbProductID"
                     InitialValue="" ForeColor="Red" ErrorMessage="Product ID is required" />
                </td>
                <td class="MyContent" width="250">
                    <telerik:RadComboBox id="rcbProductID" runat="server" appenddataboundItem="true" markFirstmatch="true" AllowCustomeText="false" width="250"
                        ValidationGroup="Group1" OnSelectedIndexChanged="rcbProductID_OnSelectedIndexChanged" autoPostBack="true">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="1000" Text="1000 - Điện CMND" />
                            <telerik:RadComboBoxItem Value="3000" Text="3000 - Chuyển đi thanh toán CI-TAD" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
                
                <tr>
                <td class="MyLable">Ben Com:<span class="Required">(*)</span>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  Display="None" ValidationGroup="Commit" 
                     ControlToValidate="rcbBenCom"
                     InitialValue="" ForeColor="Red" ErrorMessage="Ben Com is required" />
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbBenCom" autoPostBack="true"
                        AppendDataBoundItems="true" width="200"
                        MarkFirstMatch="True" AllowCustomText="false" runat="server" ValidationGroup="Group1" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
                <tr>
                <td class="MyLable">Currency:<span class="Required">(*)</span>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  Display="None" ValidationGroup="Commit" 
                     ControlToValidate="rcbCurrency"
                     InitialValue="" ForeColor="Red" ErrorMessage="Currency is required" />
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbCurrency"   autoPostBack="true"             
                        AppendDataBoundItems="true" OnSelectedIndexChanged="rcbCurrency_OnSelectedIndexChanged"
                        MarkFirstMatch="True" AllowCustomText="false" runat="server" ValidationGroup="Group1">
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
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  Display="None" ValidationGroup="Commit" 
                         ControlToValidate="rcbDebitAccount"
                         InitialValue="" ForeColor="Red" ErrorMessage="Debit Account is required" /></td>
                    <td class="MyContent" width="400" >
                         <telerik:RadComboBox ID="rcbDebitAccount"
                            MarkFirstMatch="True" width="350"
                            AllowCustomText="false"
                            runat="server">
                              <Items>
                                   <telerik:RadComboBoxItem Value="" Text="" />
                              </Items>
                              </telerik:RadComboBox>
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
            </table>
          
            <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Amount:</td>
                <td class="MyContent" width="400">
                    <telerik:RadNumericTextBox id="tbAmount" runat="server" ValidationGroup="Group1" />
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
                <tr>
                <td class="MyLable">Sending Name: <span class="Required">(*)</span>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  Display="None" ValidationGroup="Commit" 
                     ControlToValidate="tbSendingName"
                     InitialValue="" ForeColor="Red" ErrorMessage="Sending Name is required" />
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox id="tbSendingName" runat="server" ValidationGroup="Group1"  width="400"/>
                </td>
                <td class="MyLable"> </td>
                <td class="MyContent"></td>
            </tr>
                <tr>
                    <td class="MyLable">Sending Address</td>
                    <td class="MyContent">
                        <telerik:radTextBox id="tbSendingAddress" runat="server" width="400" />
                    </td>
                </tr>
                </table>
            <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">ID/Tax Code:</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox id="tbTaxCode" runat="server" NumberFormat-DecimalDigits="0"
                    NumberFormat-GroupSeparator=""     validationGroup="Group1" />
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
                <tr style="visibility:hidden;">
                <td class="MyLable">Connected Bank:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbConnectedBank" 
                        AppendDataBoundItems="true" 
                        MarkFirstMatch="True" AllowCustomText="false" runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
           </table>
        </fieldset>
        <fieldset>
            <legend style="text-transform:uppercase; font-weight:bold;">BENEFICIARY Information</legend>
             <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Receiving Name:<span class="Required">(*)</span>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"  Display="None" ValidationGroup="Commit" 
                     ControlToValidate="tbReceivingName"
                     InitialValue="" ForeColor="Red" ErrorMessage="Receiving Name is required" />
                </td>
                <td class="MyContent" width="400">
                    <telerik:RadTextBox                                     
                                    ID="tbReceivingName" runat="server"
                                     width="400">
                                </telerik:RadTextBox>
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
                 <tr>
                <td class="MyLable">
                </td>
                <td class="MyContent" width="400">
                    <telerik:RadTextBox                                     
                                    ID="tbReceivingName2" runat="server"
                                     width="400">
                                </telerik:RadTextBox>
                </td>
            </tr>
                 </table>
            <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Ben Account:
                    </td>
                <td class="MyContent" width="400">
                    <telerik:radtextbox runat="server" id="tbBenAccount" OnTextChanged="Load_BenAccount" AutoPostBack="true"></telerik:radtextbox>
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
                <tr>
                <td class="MyLable">ID Card:
                </td>
                <td class="MyContent">
                    <telerik:radtextbox runat="server" id="tbIDCard"></telerik:radtextbox>
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
                <tr>
                <td class="MyLable">Issue Date:
                </td>
                <td class="MyContent">
                      <telerik:RadDatePicker id="rdpIssueDate" runat="server" />
                </td>
                <td class="MyLable">Issue Place:</td>
                <td class="MyContent">
                    <telerik:RadTextBox id="tbIssuePlace" runat="server" validationGroup="Group1" />
                </td>
            </tr>
                <tr>
                <td class="MyLable">Province</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbProvince"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        AutoPostBack="true" width="200"
                        AppendDataBoundItems="true"
                        OnSelectedIndexChanged="rcbProvince_OnSelectedIndexChanged"
                        runat="server"  >
                     
                    </telerik:RadComboBox>
                </td>
                    <td class="MyLable">Phone</td>
                    <td class="MyContent">
                        <telerik:RadMaskedTextBox ID="tbPhone" runat="server" Mask="###########"
                        EmptyMessage="-- Enter Phone Number --" HideOnBlur="true" ZeroPadNumericRanges="true" DisplayMask="###########">
                    </telerik:RadMaskedTextBox>
                    </td>
            </tr>
                <tr>
                <td class="MyLable">Bank Code:</td>
                <td class="MyContent">
                    <telerik:RadComboBox AppendDataBoundItems="True"                                     
                                    ID="rcbBankCode" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false" width="400">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" /> 
                                </telerik:RadComboBox>
                </td>
                <td class="MyLable"> </td>
                <td class="MyContent"></td>
            </tr>
                </table>
            <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Bank Name:</td>
                <td class="MyContent" width="400" >
                    <telerik:RadTextBox id="tbBankName" runat="server" ValidationGroup="Group1" width="400" />
                </td>
                <td class="MyLable"> </td>
                <td class="MyContent"></td>
            </tr>
                 </table>
      </fieldset>
      <fieldset>
            <legend style="text-transform:uppercase; font-weight:bold;">OTHER Information</legend>
             
            <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Pay Number:</td>
                <td class="MyContent" width="400">
                    <telerik:RadNumericTextBox id="tbPayNumber" runat="server" />
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
                <tr>
                <td class="MyLable">Teller ID:<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"  Display="None" ValidationGroup="Commit" 
                     ControlToValidate="tbTellerID"
                     InitialValue="" ForeColor="Red" ErrorMessage="Teller ID is required" />
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox id="tbTellerID" runat="server" />
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
                <tr>
                <td class="MyLable">Narrative:<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"  Display="None" ValidationGroup="Commit" 
                     ControlToValidate="tbNarrative"
                     InitialValue="" ForeColor="Red" ErrorMessage="Narrative is required" />
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox id="tbNarrative" runat="server" validationGroup="Group1" width="400"/>
                </td>
            </tr>
             <tr>
                <td class="MyLable">     </td>
                <td class="MyContent">
                    <telerik:RadTextBox id="tbNarrative2" runat="server" validationGroup="Group1" width="400"/>
                </td>
            </tr>  
                </table>
          
            <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Waive Charge:</td>
                <td class="MyContent">
                    <telerik:RadComboBox AppendDataBoundItems="True"                                     
                                    ID="rcbWaiveCharge" runat="server"
                        OnSelectedIndexChanged="rcbWaiveCharge_OnSelectedIndexChanged" autoPostBack="true"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                   
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="" />
                                        <telerik:RadComboBoxItem Value="YES" Text="YES" />
                                        <telerik:RadComboBoxItem Value="NO" Text="NO" />
                                    </Items>
                                </telerik:RadComboBox>
                </td>
            </tr>
                <tr>
                <td class="MyLable">Save Template:</td>
                <td class="MyContent">
                     <telerik:RadComboBox AppendDataBoundItems="True"                                     
                                    ID="rcbSaveTemplate" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="" />
                                        <telerik:RadComboBoxItem Value="YES" Text="YES" />
                                        <telerik:RadComboBoxItem Value="NO" Text="NO" />
                                    </Items>
                                </telerik:RadComboBox>
                </td>
            </tr>
           <tr>
                <td class="MyLable">Vat Serial</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtVatSerial" runat="server"></telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Charge Amt LCY</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtChargeAmtLCY" runat="server" ClientEvents-OnValueChanged="OnChargeAmountValueChanged2" NumberFormat-DecimalDigits="0" ></telerik:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Charge Vat Amt</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtChargeVatAmt" runat="server" NumberFormat-DecimalDigits="0" ></telerik:RadNumericTextBox>
                </td>
            </tr>
                 </table>

      </fieldset>
           
    </div>
</div>
<asp:HiddenField ID="hdfDisable" runat="server" Value="1" />
<telerik:radcodeblock runat="server">
<script type="text/javascript">
    function ValidatorUpdateIsValid() {
        var ProductID = $('#<%= rcbProductID.ClientID%>').val();
        var Currency = $('#<%= rcbCurrency.ClientID%>').val();
        var BenCom = $('#<%= rcbBenCom.ClientID%>').val();
        var DebitAccount = $('#<%= rcbDebitAccount.ClientID%>').val();
        var SendingName = $('#<%= tbSendingName.ClientID%>').val();
        var ReceivingName = $('#<%= tbReceivingName.ClientID%>').val();
        Page_IsValid = ProductID != "" && Currency != "" && BenCom != "" && DebitAccount != ""
                        && SendingName != "" && ReceivingName != "";
    }

    function OnChargeAmountValueChanged2() {
        var percent = 0.1;//10%
        var AmountElement = $find("<%= txtChargeAmtLCY.ClientID%>");
        var Amount = AmountElement.get_value();

        var AmountPaidElement = $find("<%= txtChargeVatAmt.ClientID%>");

        if (Amount) {
            var lcy = Amount * percent;
            AmountPaidElement.set_value(lcy);
        }
    }

    function OnClientButtonClicking(sender, args) {
        $('#<%= hdfDisable.ClientID%>').val(1);
        var button = args.get_item();
        if (button.get_commandName() == "Preview") {
            window.location = "Default.aspx?tabid=159&ctl=TransferByAccount_PL&mid=860";
            return;
        }

        var ProductID = $find("<%= rcbProductID.ClientID%>").get_value();
        if (button.get_commandName() == "commit" && ProductID == "3000") {
            var tbBenAccount = $('#<%= tbBenAccount.ClientID%>').val();
            Page_IsValid = tbBenAccount != "";
            if (Page_IsValid == false) {
                alert("Ben Account is required");
                $('#<%= hdfDisable.ClientID%>').val(0);
            }
        }
        return; //an khong cho hien cau thong bao
        ValidatorUpdateIsValid();
        if (Page_IsValid) {
            $('#<%= hdfDisable.ClientID%>').val(1);

            if (button.get_commandName() == "commit" && !clickCalledAfterRadconfirm) {
                args.set_cancel(true);
                lastClickedItem = args.get_item();
                var isbool = radconfirm("Ch/exess Tt Amount Vnd 10000000", confirmCallbackFunction2);
                if (isbool == false) { confirmcallfail(); }
                return;
            }

            if (button.get_commandName() == "authorize" && !clickCalledAfterRadconfirm) {
                radconfirm("Authorised Completed", confirmCallbackFunction2);
            }
        }
    }
    var lastClickedItem = null;
    var clickCalledAfterRadprompt = false;
    var clickCalledAfterRadconfirm = false;
    function confirmCallbackFunction1(args) {
        radconfirm("Unauthorised overdraft of USD on account 050001688331", confirmCallbackFunction2); //" + amtFCYDeposited + "
    }

    function confirmCallbackFunction2(args) {
        if (args) {
            clickCalledAfterRadconfirm = true;
            lastClickedItem.click();
            lastClickedItem = null;
        }
    }
    function confirmcallfail() {
        $('#<%= hdfDisable.ClientID%>').val(0);//cancel thi gan disable = 0 de khoi commit
    confirmCallbackFunction2();
}
  
    function ShowMessageCurrencyNotMath() {
        radconfirm("Currency and Ben Com is not matched", confirmCallbackFunction2);
    }

    function OnCurrencyMatch(e) {
        var currencyDepositedElement = $find("<%= rcbCurrency.ClientID %>");
        var currencyDepositedValue = currencyDepositedElement.get_value();
        var cashAccountElement = $find("<%= rcbBenCom.ClientID %>");
        var cashAccountValue = cashAccountElement.get_value();
        //if (currencyDepositedValue && cashAccountValue && (currencyDepositedValue != cashAccountValue)) {
        //    ShowMessageCurrencyNotMath();
        //    currencyDepositedElement.trackChanges();
        //    currencyDepositedElement.get_items().getItem(0).select();
        //    currencyDepositedElement.updateClientState();
        //    currencyDepositedElement.commitChanges();

        //    cashAccountElement.trackChanges();
        //    cashAccountElement.get_items().getItem(0).select();
        //    cashAccountElement.updateClientState();
        //    cashAccountElement.commitChanges();
        //    return false;
        //}

        return true;
    }

    </script>
</telerik:radcodeblock>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>

        <telerik:AjaxSetting AjaxControlID="rcbProductID">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="rcbCurrency" />
                <telerik:AjaxUpdatedControl ControlID="rcbCashAccount" />
                <telerik:AjaxUpdatedControl ControlID="rcbProvince" /> 
                <telerik:AjaxUpdatedControl ControlID="rcbBankCode" />
                <telerik:AjaxUpdatedControl ControlID="tbBenAccount" />
            </UpdatedControls>
        </telerik:AjaxSetting> 

        <telerik:AjaxSetting AjaxControlID="rcbWaiveCharge">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="txtVatSerial" />
                <telerik:AjaxUpdatedControl ControlID="txtChargeAmtLCY" />
                <telerik:AjaxUpdatedControl ControlID="txtChargeVatAmt" /> 
            </UpdatedControls>
        </telerik:AjaxSetting> 

        <telerik:AjaxSetting AjaxControlID="rcbCurrency">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbDebitAccount" />
            </UpdatedControls>
        </telerik:AjaxSetting> 
        <telerik:AjaxSetting AjaxControlID="rcbBenCom">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbDebitAccount" />
            </UpdatedControls>
        </telerik:AjaxSetting> 

        <telerik:AjaxSetting AjaxControlID="rcbProvince">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbBankCode" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tbBenAccount">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbReceivingName" />
                <telerik:AjaxUpdatedControl ControlID="tbIDCard" />
                <telerik:AjaxUpdatedControl ControlID="tbIssuePlace" />
                <telerik:AjaxUpdatedControl ControlID="rdpIssueDate" />
                <telerik:AjaxUpdatedControl ControlID="tbPhone" />
            </UpdatedControls>
        </telerik:AjaxSetting> 
    </AjaxSettings>
</telerik:RadAjaxManager>
