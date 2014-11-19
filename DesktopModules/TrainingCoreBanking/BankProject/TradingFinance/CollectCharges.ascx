<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollectCharges.ascx.cs" Inherits="BankProject.TradingFinance.CollectCharges" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit"  />
<style>
    .addChargeType, .removeChargeType {
        cursor:hand; cursor:pointer;
    }
</style>
<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
         OnClientButtonClicking="RadToolBar1_OnClientButtonClicking" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommit" CommandName="commit" Enabled="true">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btPreview" CommandName="preview" postback="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="search" postback="false" Enabled="true">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print" postback="false" Enabled="false">
        </telerik:RadToolBarButton>
    </Items>
</telerik:radtoolbar>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtCode" runat="server" Width="200" /><span class="Required"> (*)</span><asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator6"
            ControlToValidate="txtCode"
            ValidationGroup="Commit"
            InitialValue=""
            ErrorMessage="FT Number is required" ForeColor="Red">
        </asp:RequiredFieldValidator> &nbsp;<asp:Label ID="lblError" runat="server" ForeColor="red" />
        </td>        
    </tr>
</table>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-main').dnnTabs();
    });
</script>
<div class="dnnForm" id="tabs-main">    
    <ul class="dnnAdminTabNav">
        <li><a href="#divAccountTransfer">Account Transfer</a></li>
        <li><a href="#divMT103">MT 103</a></li>
        <li><a href="#divChargeCommission">Charge Commission</a></li>
    </ul>
    <div id="divAccountTransfer" class="dnnClear">
        <fieldset>
            <legend>
                <span style="font-weight: bold; text-transform: uppercase;">TRANSFER TYPE</span>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Transaction Type <span class="Required">(*)</span><asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator1"
            ControlToValidate="cboTransactionType"
            ValidationGroup="Commit"
            InitialValue=""
            ErrorMessage="[Account Transfer] Transaction Type is required" ForeColor="Red">
        </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboTransactionType" AutoPostBack="True"
                            Runat="server" OnSelectedIndexChanged="cboTransactionType_OnSelectedIndexChanged"
                            MarkFirstMatch="True"
                            AllowCustomText="false" width="300" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Country Code</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboCountryCode" 
                            Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" width="300" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Commodity/Services</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboCommodityServices" 
                            Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" width="300" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Other Info</td>
                    <td class="MyContent"><telerik:radtextbox id="txtOtherInfo" runat="server" width="300" /></td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <span style="font-weight: bold; text-transform: uppercase;">DEBIT INFORMATION</span>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Order Customer ID <span class="Required">(*)</span><asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator2"
            ControlToValidate="txtOrderCustomerID"
            ValidationGroup="Commit"
            InitialValue=""
            ErrorMessage="[Account Transfer] Order Customer ID is required" ForeColor="Red">
        </asp:RequiredFieldValidator></td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtOrderCustomerID" runat="server" AutoPostBack="True" OnTextChanged="txtOrderCustomerID_OnTextChanged" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Order Customer Name</td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtOrderCustomerName" runat="server" width="300" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Order Customer Address</td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtOrderCustomerAddr1" runat="server" width="300" /></td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtOrderCustomerAddr2" runat="server" width="300" /></td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtOrderCustomerAddr3" runat="server" width="300" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Debit Ref</td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtDebitRef" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Debit Acct No <span class="Required">(*)</span><asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator3"
            ControlToValidate="cboDebitAcctNo"
            ValidationGroup="Commit"
            InitialValue=""
            ErrorMessage="[Account Transfer] Debit Acct No is required" ForeColor="Red">
        </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboDebitAcctNo" AutoPostBack="True"
                            Runat="server" OnSelectedIndexChanged="cboDebitAcctNo_OnSelectedIndexChanged"
                            MarkFirstMatch="True" OnItemDataBound="BDRFROMACCOUNT_ItemDataBound"
                            AllowCustomText="false" width="300" >
                        </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="lblDebitAcctName" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Debit Currency</td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtDebitCurrency" runat="server" Enabled="false" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Debit Amount <span class="Required">(*)</span><asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator4"
            ControlToValidate="txtDebitAmount"
            ValidationGroup="Commit"
            InitialValue="0"
            ErrorMessage="[Account Transfer] Debit Amount No is required" ForeColor="Red">
        </asp:RequiredFieldValidator></td>
                    <td class="MyContent" colspan="2"><telerik:radnumerictextbox id="txtDebitAmount" runat="server" /></td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <span style="font-weight: bold; text-transform: uppercase;">CREDIT INFORMATION</span>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Credit Account <span class="Required">(*)</span><asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator5"
            ControlToValidate="cboCreditAccount"
            ValidationGroup="Commit"
            InitialValue=""
            ErrorMessage="[Account Transfer] Credit Account is required" ForeColor="Red">
        </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboCreditAccount" AutoPostBack="True"
                            Runat="server" OnSelectedIndexChanged="cboCreditAccount_OnSelectedIndexChanged"
                            MarkFirstMatch="True" OnItemDataBound="BSWIFTCODE_ItemDataBound"
                            AllowCustomText="false" width="300" >
                            <ItemTemplate>
			                    <%# DataBinder.Eval(Container.DataItem, "AccountNo")%> - <%# DataBinder.Eval(Container.DataItem, "Description")%> 
                           </ItemTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="lblCreditAccountName" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Credit Currency</td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtCreditCurrency" runat="server" Enabled="false" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Credit Amount <span class="Required">(*)</span><asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator7"
            ControlToValidate="txtCreditAmount"
            ValidationGroup="Commit"
            InitialValue="0"
            ErrorMessage="[Account Transfer] Credit Amount is required" ForeColor="Red">
        </asp:RequiredFieldValidator></td>
                    <td class="MyContent" colspan="2"><telerik:radnumerictextbox id="txtCreditAmount" runat="server" AutoPostBack="True" OnTextChanged="txtCreditAmount_OnTextChanged" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Processing Date</td>
                    <td class="MyContent" colspan="2"><telerik:raddatepicker id="txtProcessingDate" runat="server" Enabled="false" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Add Remarks</td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtAddRemarks" runat="server" width="300" /></td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div id="divMT103" class="dnnClear">
        <fieldset>
            <legend>
                <span style="font-weight: bold; text-transform: uppercase;"></span>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width:160px;">Sender's Reference</td>
                    <td class="MyContent"><asp:Label ID="lblSenderReference" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Bank Operation Code</td>
                    <td class="MyContent"><asp:Label ID="lblBankOperationCode" runat="server" Text="CRED"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Value Date</td>
                    <td class="MyContent"><telerik:raddatepicker id="txtValueDate" runat="server" Enabled="false" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Currency</td>
                    <td class="MyContent"><asp:Label ID="lblCurrency" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">InterBank Settle Amount</td>
                    <td class="MyContent"><telerik:radnumerictextbox id="txtInterBankSettleAmount" runat="server" Enabled="false" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Instructed Amount</td>
                    <td class="MyContent"><telerik:radnumerictextbox id="txtInstructedAmount" runat="server" Enabled="false" /></td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <span style="font-weight: bold; text-transform: uppercase;"></span>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width:160px;">Ordering Customer Acc</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboOrderingCustomerAcc" 
                            Runat="server" width="300"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Ordering Customer Name</td>
                    <td class="MyContent"><telerik:radtextbox id="txtOrderingCustomerName" runat="server" width="300" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Ordering Customer Addr</td>
                    <td class="MyContent"><telerik:radtextbox id="txtOrderingCustomerAddr1" runat="server" width="300" /></td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent"><telerik:radtextbox id="txtOrderingCustomerAddr2" runat="server" width="300" /></td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent"><telerik:radtextbox id="txtOrderingCustomerAddr3" runat="server" width="300" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Ordering Institution</td>
                    <td class="MyContent"><telerik:radtextbox id="txtOrderingInstitution" runat="server" width="300" /></td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <span style="font-weight: bold; text-transform: uppercase;"></span>
            </legend>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width:160px;">Sender's Correspondent</td>
                    <td class="MyContent" colspan="2"><asp:Label ID="lblSenderCorrespondent" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Receiver's Correspondent</td>
                    <td class="MyContent"><telerik:radtextbox id="txtReceiverCorrespondent" runat="server" Enabled="false" /></td>
                    <td><asp:Label ID="lblReceiverCorrespondentName" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Receiver Corr Bank Acct</td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtReceiverCorrBankAcct" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Intermediary Type</td>
                    <td class="MyContent" colspan="2">
                        <telerik:RadComboBox
                            ID="cboIntermediaryType" 
                            Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
	                            <telerik:RadComboBoxItem Value="A" Text="A" />	
	                            <telerik:RadComboBoxItem Value="B" Text="B" />
	                            <telerik:RadComboBoxItem Value="D" Text="D" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Intermediary Institution</td>
                    <td class="MyContent"><telerik:radtextbox id="txtIntermediaryInstitution" runat="server" AutoPostBack="True" OnTextChanged="txtIntermediaryInstitution_OnTextChanged" /></td>
                    <td class="MyLable"><asp:Label ID="lblIntermediaryInstitutionName" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Intermediary Acct</td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtIntermediaryAcct1" runat="server" width="300" /></td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtIntermediaryAcct2" runat="server" width="300" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Intermediary Bank Acct</td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtIntermediaryBankAcct" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Account Type</td>
                    <td class="MyContent" colspan="2">
                        <telerik:RadComboBox
                            ID="cboAccountType" 
                            Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
	                            <telerik:RadComboBoxItem Value="A" Text="A" />	
	                            <telerik:RadComboBoxItem Value="B" Text="B" />
	                            <telerik:RadComboBoxItem Value="D" Text="D" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Account With Institution</td>
                    <td class="MyContent"><telerik:radtextbox id="txtAccountWithInstitution" runat="server" AutoPostBack="True" OnTextChanged="txtAccountWithInstitution_OnTextChanged" /></td>
                    <td class="MyLable"><asp:Label ID="lblAccountWithInstitutionName" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Account With Bank Acct</td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtAccountWithBankAcct1" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtAccountWithBankAcct2" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Beneficiary Account</td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtBeneficiaryAccount" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Beneficiary Name</td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtBeneficiaryName" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Beneficiary Addr</td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtBeneficiaryAddr1" runat="server" width="300" /></td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtBeneficiaryAddr2" runat="server" width="300" /></td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent" colspan="2"><telerik:radtextbox id="txtBeneficiaryAddr3" runat="server" width="300" /></td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <span style="font-weight: bold; text-transform: uppercase;"></span>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width:160px;">Remittance Information</td>
                    <td class="MyContent"><telerik:radtextbox id="txtRemittanceInformation" runat="server" width="300" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Detail of Charges</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboDetailOfCharges" AutoPostBack="True" Runat="server" OnSelectedIndexChanged="cboDetailOfCharges_OnSelectedIndexChanged"
                            MarkFirstMatch="True" AllowCustomText="false" >
                            <Items>
	                            <telerik:RadComboBoxItem Value="BEN" Text="BEN" />	
	                            <telerik:RadComboBoxItem Value="OUT" Text="OUT" />
	                            <telerik:RadComboBoxItem Value="SHA" Text="SHA" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Sender's Charges</td>
                    <td class="MyContent"><asp:Label ID="lblSenderCharges" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Receiver's  Charges</td>
                    <td class="MyContent"><asp:Label ID="lblReceiverCharges" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Sender to Receiver Info</td>
                    <td class="MyContent"><telerik:radtextbox id="txtSendertoReceiverInfo" runat="server" width="300" /></td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div id="divChargeCommission" class="dnnClear">
        <fieldset>
            <legend>
                <span style="font-weight: bold; text-transform: uppercase;"></span>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Charge Acct</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboChargeAcct" AutoPostBack="True"
                            Runat="server" width="300" OnSelectedIndexChanged="cboChargeAcct_OnSelectedIndexChanged"
                            MarkFirstMatch="True" OnItemDataBound="BDRFROMACCOUNT_ItemDataBound"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="lblChargeAcctName" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Charge Currency</td>
                    <td class="MyContent" colspan="2"><asp:Label ID="lblChargeCurrency" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Transaction Type</td>
                    <td class="MyContent" colspan="2">
                        <telerik:RadComboBox
                            ID="cboTransactionType_ChargeCommission" 
                            Runat="server" OnSelectedIndexChanged="cboTransactionType_ChargeCommission_OnSelectedIndexChanged"
                            MarkFirstMatch="True" AutoPostBack="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="TT" Text="TT" />
	                            <telerik:RadComboBoxItem Value="LC" Text="LC" />
	                            <telerik:RadComboBoxItem Value="DP/DA" Text="DP/DA" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" id="divChargeType" runat="server">
                <tr>    
                    <td class="MyLable">Charge Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboChargeType" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                    <td><div id="divCmdChargeType" runat="server"><a class="addChargeType" index="1"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></div></td>
                </tr>
                <tr>    
                    <td class="MyLable">Charge Amount</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadNumericTextBox ID="txtChargeAmount" Runat="server" AutoPostBack="True" OnTextChanged="txtChargeAmount_OnTextChanged" />
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" id="divChargeType1" runat="server" style="display:none;">
                <tr>    
                    <td class="MyLable">Charge Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboChargeType1" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                    <td><div id="divCmdChargeType1" runat="server"><a class="removeChargeType" index="2"><img src="Icons/Sigma/Delete_16X16_Standard.png" /></a><a class="addChargeType" index="2"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></div></td>
                </tr>
                <tr>    
                    <td class="MyLable">Charge Amount</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadNumericTextBox ID="txtChargeAmount1" Runat="server" AutoPostBack="True" OnTextChanged="txtChargeAmount1_OnTextChanged" />
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" id="divChargeType2" runat="server" style="display:none;">
                <tr>    
                    <td class="MyLable">Charge Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboChargeType2" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                    <td><div id="divCmdChargeType2" runat="server"><a class="removeChargeType" index="3"><img src="Icons/Sigma/Delete_16X16_Standard.png" /></a></div></td>
                </tr>
                <tr>    
                    <td class="MyLable">Charge Amount</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadNumericTextBox ID="txtChargeAmount2" Runat="server" AutoPostBack="True" OnTextChanged="txtChargeAmount2_OnTextChanged" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">Charge For</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboChargeFor" Runat="server" OnSelectedIndexChanged="cboChargeFor_ChargeCommission_OnSelectedIndexChanged"
                            MarkFirstMatch="True" AutoPostBack="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="A" Text="A" />
                                <telerik:RadComboBoxItem Value="AC" Text="AC" />
                                <telerik:RadComboBoxItem Value="B" Text="B" />
                                <telerik:RadComboBoxItem Value="BC" Text="BC" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>    
                    <td class="MyLable">VAT No</td>
                    <td class="MyContent"><telerik:RadTextBox ID="txtVATNo" runat="server" Enabled="false" /></td>
                </tr>
                <tr>    
                    <td class="MyLable">Add Remarks</td>
                    <td class="MyContent"><telerik:RadTextBox ID="txtAddRemarks_Charges1" runat="server" Width="300" /></td>
                </tr>                
                <tr>    
                    <td class="MyLable"></td>
                    <td class="MyContent"><telerik:RadTextBox ID="txtAddRemarks_Charges2" runat="server" Width="300" /></td>
                </tr>
                <tr>    
                    <td class="MyLable">Account Officer</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboAccountOfficer" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>    
                    <td class="MyLable">Total Charge Amount</td>
                    <td class="MyContent"><asp:Label ID="lblTotalChargeAmount" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>    
                    <td class="MyLable">Total Tax Amount</td>
                    <td class="MyContent"><asp:Label ID="lblTotalTaxAmount" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </fieldset>
    </div>
</div>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function RadToolBar1_OnClientButtonClicking(sender, args) {
            var button = args.get_item();
            //
            if (button.get_commandName() == '<%=BankProject.Controls.Commands.Preview%>') {
                window.location = '<%=EditUrl("list")%>&lst=4appr';
            }
            if (button.get_commandName() == '<%=BankProject.Controls.Commands.Search%>') {
                window.location = '<%=EditUrl("list")%>';
            }
            if (button.get_commandName() == '<%=BankProject.Controls.Commands.Print%>') {
                args.set_cancel(true);
                radconfirm("Do you want to download PHIEU CHUYEN KHOAN file ?", showReport1, 420, 150, null, 'Download');
            }
        }
        function showReport1(result) {
            if (result) {
                $("#<%=btnReportPhieuCK.ClientID %>").click();
            }
            radconfirm("Do you want to download MT103 file ?", showReport2, 420, 150, null, 'Download');
        }
        function showReport2(result) {
            if (result) {
                $("#<%=btnReportMT103.ClientID %>").click();
            }
            radconfirm("Do you want to download VAT file ?", showReport3, 420, 150, null, 'Download');
        }
        function showReport3(result) {
            if (result) {
                $("#<%=btnReportVAT.ClientID %>").click();
            }
        }
        //
        $("#<%=txtCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $('#<%=btnLoadCodeInfo.ClientID%>').click();
            }
        });
        //
        $(document).ready(
          function () {
              $('a.addChargeType').live('click',
                  function () {
                      var index = $(this).attr('index');
                      if (index == "1") {
                          if ($('#<%=divChargeType2.ClientID%>').css('display') == 'none')
                              $('#<%=divChargeType1.ClientID%> .addChargeType').css('display', '');
                          else
                              $('#<%=divChargeType1.ClientID%> .addChargeType').css('display', 'none');
                          $find("<%=cboChargeType1.ClientID%>").set_value('');
                          $('#<%=divChargeType1.ClientID%>').css('display', '');
                      }
                      else if (index == "2") {
                          $('#<%=divChargeType2.ClientID%> .addChargeType').css('display', '');
                          $find("<%=cboChargeType2.ClientID%>").set_value('');
                          $('#<%=divChargeType2.ClientID%>').css('display', '');
                      }
                      $(this).css('display', 'none');
                  });
              $('a.removeChargeType').live('click',
                  function () {
                      var index = $(this).attr('index');
                      if (index == "2") {
                          $('#<%=divChargeType.ClientID%> .addChargeType').css('display', '');
                          $find("<%=cboChargeType1.ClientID%>").set_value('');
                          $('#<%=divChargeType1.ClientID%>').css('display', 'none');
                      }
                      else if (index == "3") {
                          $('#<%=divChargeType1.ClientID%> .addChargeType').css('display', '');
                          $find("<%=cboChargeType2.ClientID%>").set_value('');
                          $('#<%=divChargeType2.ClientID%>').css('display', 'none');
                      }
                  });
          });
    </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"><img src="icons/bank/ajax-loader-16x16.gif" /></telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
    <AjaxSettings>        
        <telerik:AjaxSetting AjaxControlID="cboTransactionType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="cboCommodityServices" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtOrderCustomerID">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtOrderCustomerName" />
                <telerik:AjaxUpdatedControl ControlID="txtOrderCustomerAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtOrderCustomerAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtOrderCustomerAddr3" />
                <telerik:AjaxUpdatedControl ControlID="cboDebitAcctNo" />
                <telerik:AjaxUpdatedControl ControlID="cboOrderingCustomerAcc" />
                <telerik:AjaxUpdatedControl ControlID="cboChargeAcct" />
                <telerik:AjaxUpdatedControl ControlID="txtOrderingCustomerName" />
                <telerik:AjaxUpdatedControl ControlID="txtOrderingCustomerAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtOrderingCustomerAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtOrderingCustomerAddr3" />                
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboDebitAcctNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtDebitCurrency" />
                <telerik:AjaxUpdatedControl ControlID="cboCreditAccount" />
                <telerik:AjaxUpdatedControl ControlID="txtCreditCurrency" />
                <telerik:AjaxUpdatedControl ControlID="lblCurrency" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboCreditAccount">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtCreditCurrency" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondent" />
                <telerik:AjaxUpdatedControl ControlID="lblReceiverCorrespondentName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtCreditAmount">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtInstructedAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtIntermediaryInstitution">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblIntermediaryInstitutionName" />
                <telerik:AjaxUpdatedControl ControlID="txtIntermediaryAcct1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtAccountWithInstitution">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAccountWithInstitutionName" />
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithBankAcct1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboDetailOfCharges">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblSenderCharges" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboChargeAcct">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblChargeCurrency" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboTransactionType_ChargeCommission">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="cboChargeType" />
                <telerik:AjaxUpdatedControl ControlID="cboChargeType1" />
                <telerik:AjaxUpdatedControl ControlID="cboChargeType2" />                
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtChargeAmount">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTotalChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalTaxAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtChargeAmount1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTotalChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalTaxAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtChargeAmount2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTotalChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalTaxAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboChargeFor">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTotalChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalTaxAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboChargeType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTotalChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalTaxAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboChargeType1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTotalChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalTaxAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboChargeType2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTotalChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalTaxAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<div style="visibility: hidden;"><asp:Button ID="btnLoadCodeInfo" runat="server" OnClick="btnLoadCodeInfo_Click" Text="" /></div>
<div style="visibility: hidden;"><asp:Button ID="btnReportPhieuCK" runat="server" OnClick="btnReportPhieuCK_Click" Text="" /></div>
<div style="visibility: hidden;"><asp:Button ID="btnReportMT103" runat="server" OnClick="btnReportMT103_Click" Text="" /></div>
<div style="visibility: hidden;"><asp:Button ID="btnReportVAT" runat="server" OnClick="btnReportVAT_Click" Text="" /></div>