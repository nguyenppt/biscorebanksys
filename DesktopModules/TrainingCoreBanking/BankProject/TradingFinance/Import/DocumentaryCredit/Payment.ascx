﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Payment.ascx.cs" Inherits="BankProject.TradingFinance.Import.DocumentaryCredit.Payment" %>
<telerik:radwindowmanager id="RadWindowManager1" runat="server" enableshadow="true" />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit"/>
<style>
    .labelDisabled {
        color: darkgray !important;
    }
    .paymentControlWidth {
        width:200px !important;
    }
</style>
<telerik:radtoolbar runat="server" id="RadToolBar1" enableroundedcorners="true" enableshadows="true" width="100%"
    OnButtonClick="RadToolBar1_ButtonClick" OnClientButtonClicking="RadToolBar1_OnClientButtonClicking">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommit" CommandName="commit" Enabled="false">
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
            <asp:HiddenField ID="txtPaymentId" runat="server" Value="0" /><asp:HiddenField ID="txtCustomerID" runat="server" Value="" /><asp:HiddenField ID="txtCustomerName" runat="server" Value="" />
            <asp:TextBox ID="txtCode" runat="server" Width="200" /><span class="Required"> (*)</span> &nbsp;<asp:Label ID="lblError" runat="server" ForeColor="red" />
        </td>
        <asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator6"
            ControlToValidate="txtCode"
            ValidationGroup="Commit"
            InitialValue=""
            ErrorMessage="LC Number is required" ForeColor="Red">
        </asp:RequiredFieldValidator>
    </tr>
</table>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
        $('#Charges').dnnTabs();
    });
</script>
<div class="dnnForm" id="tabs-demo">    
    <ul class="dnnAdminTabNav">
        <li><a href="#Main" id="tabMain">Main</a></li>
        <li><a href="#MT202">MT 202</a></li>
        <li><a href="#MT756">MT 756</a></li>
        <li><a href="#Charges">Charges</a></li>
    </ul>
    <div id="Main" class="dnnClear">
        <fieldset>
            <legend>
                <span style="font-weight: bold; text-transform: uppercase;"></span>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">1 Draw Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboDrawType" 
                            Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false"
                            CssClass="paymentControlWidth" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">2 Currency</td>
                    <td class="MyContent"><asp:Label ID="lblCurrency" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">3 Drawing Amount<span class="Required"> (*)</span></td>
                    <td class="MyContent"><telerik:radnumerictextbox id="txtDrawingAmount" runat="server" Enabled="false" CssClass="paymentControlWidth" />
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator2"
                            ControlToValidate="txtDrawingAmount"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Drawing Amount is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">4 Value Date</td>
                    <td class="MyContent"><telerik:raddatepicker id="txtValueDate" runat="server" Width="180px" /></td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <span style="font-weight: bold; text-transform: uppercase;">Payment Instructions</span>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">20 Deposit Account</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboDepositAccount" 
                            Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" width="300" >
                        </telerik:RadComboBox> <asp:Label ID="lblDepositAccountName" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">21 Exchange Rate</td>
                    <td class="MyContent"><telerik:radnumerictextbox id="txtExchangeRate" runat="server" CssClass="paymentControlWidth" /></td>
                </tr>
                <tr>
                    <td class="MyLable labelDisabled">22 Amt DR Fr Acct Ccy</td>
                    <td class="MyContent labelDisabled"><telerik:radnumerictextbox ID="txtAmtDRFrAcctCcy" runat="server" CssClass="paymentControlWidth labelDisabled" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td class="MyLable labelDisabled">23 Prov Amt Release</td>
                    <td class="MyContent labelDisabled"><telerik:radnumerictextbox id="txtProvAmtRelease" runat="server" CssClass="paymentControlWidth labelDisabled" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td class="MyLable">24 Amt DR from Acct <span class="Required">(*)</span></td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox id="txtAmtDrFromAcct" runat="server" Enabled="false" CssClass="paymentControlWidth" />
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator1"
                            ControlToValidate="txtAmtDrFromAcct"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Amt DR from Acct is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="display:none;"><!-- Ẩn theo y/c của Nguyên -->
                    <td class="MyLable">Prov Cover Acct</td>
                    <td class="MyContent">
                        <telerik:radcombobox
                            id="cboProvCoverAcct" runat="server" autopostback="False"
                            markfirstmatch="True"
                            allowcustomtext="false" width="300">
                        </telerik:radcombobox>
                    </td>
                </tr>
                <tr style="display:none;"><!-- Ẩn theo y/c của Nguyên -->
                    <td class="MyLable">Prov Exchange Rate</td>
                    <td class="MyContent"><telerik:radnumerictextbox id="txtProvExchangeRate" runat="server" CssClass="paymentControlWidth" /></td>
                </tr>
                <tr style="display:none;"><!-- Ẩn theo y/c của Nguyên -->
                    <td class="MyLable labelDisabled">Cover Amount</td>
                    <td class="MyContent labelDisabled"><telerik:radnumerictextbox id="txtCoverAmount" runat="server" CssClass="paymentControlWidth labelDisabled" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td class="MyLable">25 Payment Method</td>
                    <td class="MyContent">
                        <telerik:radcombobox
                            id="cboPaymentMethod" runat="server" autopostback="False"
                            markfirstmatch="True" CssClass="paymentControlWidth"
                            allowcustomtext="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="N" Text="NOSTRO" />
                            </Items>
                        </telerik:radcombobox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">26 Nostro Acct</td>
                    <td class="MyContent">
                        <telerik:radcombobox AutoPostBack="true"
                            OnItemDataBound="cboNostroAcct_ItemDataBound"
                            id="cboNostroAcct" runat="server"
                            markfirstmatch="True" width="300"
                            allowcustomtext="false" OnSelectedIndexChanged="cboNostroAcct_SelectIndexChange">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />                            
                        </telerik:radcombobox> <asp:Label ID="lblNostroAcctName" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable labelDisabled">27 Amount Credited</td>
                    <td class="MyContent labelDisabled"><telerik:radnumerictextbox id="txtAmountCredited" runat="server" CssClass="paymentControlWidth labelDisabled" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td class="MyLable">28 Payment Remarks</td>
                    <td class="MyContent"><telerik:radtextbox id="txtPaymentRemarks" runat="server" width="300" /></td>
                </tr>
            </table>
        </fieldset>
        <fieldset style="display:none">
            <legend>
                <span style="font-weight: bold; text-transform: uppercase;">Utilisation Details</span>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">30 Fully Utilised</td>
                    <td class="MyContent"><asp:TextBox ID="txtFullyUtilised" runat="server" CssClass="paymentControlWidth"></asp:TextBox></td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div id="MT202" class="dnnClear">
        <fieldset>
            <legend>
                <span style="font-weight: bold; text-transform: uppercase;"></span>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">20. Transaction Reference Number</td>
                    <td class="MyContent">
                        <asp:Label ID="lblTransactionReferenceNumber" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px" class="MyLable">21. Related Reference<span class="Required"> (*)</span></td>
                    <td  class="MyContent">
                        <telerik:RadTextBox ID="txtRelatedReference" runat="server" Width="355" ClientEvents-OnValueChanged ="txtRelatedReference_OnValueChanged" MaxLength="16"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px" class="MyLable">32A.1 Value Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteValueDate_MT202" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">32A.2 Currency Code/Amount<span class="Required"> (*)</span></td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadComboBox
                            ID="comboCurrency" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
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
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator4"
                            ControlToValidate="comboCurrency"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[Tab MT202] Currency is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="numAmount" runat="server" />
                         <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator5"
                            ControlToValidate="numAmount"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[Tab MT202] Amount is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display: none;">
                    <td style="width: 200px" class="MyLable">Ordering Institution</td>
                    <td class="MyContent">
                        <asp:Label ID="lblOrderingInstitution" runat="server" Text="OURSELVES" />
                    </td>
                </tr>
                <tr style="display: none;">
                    <td style="width: 200px" class="MyLable">Sender's Correspondent</td>
                    <td class="MyContent">
                        <asp:Label ID="lblSenderCorrespondent1" runat="server" />
                    </td>
                </tr>
                <tr style="display: none;">
                    <td style="width: 200px" class="MyLable">Sender's Correspondent</td>
                    <td class="MyContent">
                        <asp:Label ID="lblSenderCorrespondent2" runat="server" />
                    </td>
                </tr>               
                <tr style="display: none">
                    <td style="width: 200px" class="MyLable">Receiver's Correspondent</td>
                    <td class="MyContent">
                        <asp:Label ID="lblReceiverCorrespondentName2" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 200px">56A.1 Intermediary Bank Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                            AutoPostBack="True" 
                            OnSelectedIndexChanged="comboIntermediaryBankType_OnSelectedIndexChanged"
                            ID="comboIntermediaryBankType" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="A" Text="A" />
                                <telerik:RadComboBoxItem Value="B" Text="B" />
                                <telerik:RadComboBoxItem Value="D" Text="D" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">56A.2 Intermediary Bank No.</td>
                    <td class="MyContent" style="width: 400px">
                        <telerik:RadTextBox ID="txtIntermediaryBank" runat="server" Width="400" MaxLength="34"
                            AutoPostBack="True" 
                            OnTextChanged="txtIntermediaryBank_OnTextChanged" />
                    </td>
                    <td>
                        <asp:Label ID="lblIntermediaryBankNoError" runat="server" Text="" ForeColor="red" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">56A.3 Intermediary Bank Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIntermediaryBankName" runat="server" Width="400" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px" class="MyLable">56A.4 Intermediary Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIntermediaryBankAddr1" runat="server" Width="400" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIntermediaryBankAddr2" runat="server" Width="400" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIntermediaryBankAddr3" runat="server" Width="400" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px" class="MyLable">57A.1 Account With Institution Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                            AutoPostBack="True" 
                            OnSelectedIndexChanged="comboAccountWithInstitutionType_OnSelectedIndexChanged"
                            ID="comboAccountWithInstitutionType" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="A" Text="A" />
                                <telerik:RadComboBoxItem Value="B" Text="B" />
                                <telerik:RadComboBoxItem Value="D" Text="D" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">57A.2 Account With Institution No.</td>
                    <td class="MyContent" style="width: 400px">
                        <telerik:radtextbox ID="txtAccountWithInstitution" runat="server" Width="400" MaxLength="34"
                            AutoPostBack="True" OnTextChanged="txtAccountWithInstitution_OnTextChanged"/>
                    </td>
                    <td>
                        <asp:Label ID="lblAccountWithInstitutionError" runat="server" ForeColor="red" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">57A.3 Account With Institution Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAccountWithInstitutionName" runat="server" Width="400" MaxLength="35" ClientEvents-OnValueChanged="txtAccountWithInstitutionName_OnValueChanged" />
                    </td>
                </tr>                
                <tr>
                    <td style="width: 200px" class="MyLable">57A.4 Account With Institution Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAccountWithInstitutionAddr1" runat="server" Width="400" MaxLength="35" ClientEvents-OnValueChanged="txtAccountWithInstitutionAddr1_OnValueChanged" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAccountWithInstitutionAddr2" runat="server" Width="400" MaxLength="35" ClientEvents-OnValueChanged="txtAccountWithInstitutionAddr2_OnValueChanged" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAccountWithInstitutionAddr3" runat="server" Width="400" MaxLength="35" ClientEvents-OnValueChanged="txtAccountWithInstitutionAddr3_OnValueChanged" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 200px">58A.1 Beneficiary Bank Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                            AutoPostBack="True" 
                            OnSelectedIndexChanged="comboBeneficiaryBankType_OnSelectedIndexChanged"
                            ID="comboBeneficiaryBankType" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="A" Text="A" />
                                <telerik:RadComboBoxItem Value="B" Text="B" />
                                <telerik:RadComboBoxItem Value="D" Text="D" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">58A.2 Beneficiary Bank No.</td>
                    <td class="MyContent" style="width: 400px">
                        <telerik:RadTextBox ID="txtBeneficiaryBank" runat="server" Width="400" MaxLength="34" AutoPostBack="True" OnTextChanged="txtBeneficiaryBank_OnTextChanged"/>
                    </td>
                    <td>
                        <asp:Label ID="lblBeneficiaryBankError" runat="server" Text="" ForeColor="red" />
                    </td>
                </tr>
            </table>            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">58A.3 Beneficiary Bank Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryBankName" runat="server" Width="400" MaxLength="35"/>
                    </td>
                </tr>                
                <tr>
                    <td style="width: 200px" class="MyLable">58A.4 Beneficiary Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryBankAddr1" runat="server" Width="400" MaxLength="35"/>
                    </td>
                </tr>                
                <tr>
                    <td style="width: 200px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryBankAddr2" runat="server" Width="400" MaxLength="35"/>
                    </td>
                </tr>                
                <tr>
                    <td style="width: 200px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryBankAddr3" runat="server" Width="400" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px" class="MyLable">72. Sender to Receiver Information</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation" runat="server" Width="400" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation2" runat="server" Width="400" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation3" runat="server" Width="400" MaxLength="35"/>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div id="MT756" class="dnnClear">
        <fieldset>
            <legend>
                <span style="font-weight: bold; text-transform: uppercase;"></span>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">Create MT756</td>
                    <td class="MyContent">
                        <telerik:RadComboBox AutoPostBack="false"
                            OnClientSelectedIndexChanged="comboCreateMT756_OnSelectedIndexChanged"
                            ID="comboCreateMT756" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="YES" Text="YES" />
                                <telerik:RadComboBoxItem Value="NO" Text="NO" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <div ID="divMT756" runat="server">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 200px" class="MyLable">20. Related Reference</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtRelatedReferenceMT400" runat="server" Width="355" MaxLength="16"/>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 200px" class="MyLable">21. Sending Bank's TRN</td>
                        <td class="MyContent">
                            <asp:Label ID="txtSendingBankTRN" runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 200px" class="MyLable">32B. Amount Collected</td>
                        <td class="MyContent">
                            <telerik:RadNumericTextBox ID="numAmountCollected" runat="server" Enabled="False" />
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 200px" class="MyLable">33A.1 Value Date</td>
                        <td class="MyContent">
                            <telerik:RadDatePicker ID="dteValueDate_MT400" runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 200px" class="MyLable">33A.2 Currency Code/Amount</td>
                        <td class="MyContent" style="width: 150px">
                            <telerik:RadComboBox
                                ID="comboCurrency_MT400" runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
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
                            </telerik:RadComboBox>                            
                        </td>
                        <td><telerik:RadNumericTextBox ID="numAmount_MT400" runat="server" /></td>
                    </tr>
                </table>                
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 200px" class="MyLable">53A.1 Sender's Correspondent Type</td>
                        <td class="MyContent">
                            <telerik:RadComboBox 
                                AutoPostBack="True" 
                                OnSelectedIndexChanged="comboSenderCorrespondentType_OnSelectedIndexChanged"
                                ID="comboSenderCorrespondentType" runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                                <Items>
                                    <telerik:RadComboBoxItem Value="A" Text="A" />
                                    <telerik:RadComboBoxItem Value="B" Text="B" />
                                    <telerik:RadComboBoxItem Value="D" Text="D" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>                
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr >
                        <td style="width: 200px" class="MyLable">53A.2 Sender's Correspondent No.</td>
                        <td class="MyContent" style="width: 150px">
                            <telerik:RadTextBox ID="txtSenderCorrespondentNo" runat="server" Width="355" MaxLength="34"
                                AutoPostBack="True" 
                                OnTextChanged="txtSenderCorrespondentNo_OnTextChanged" />
                        </td>
                        <td><asp:Label ID="lblSenderCorrespondentNoError" runat="server" ForeColor="red"/></td>
                    </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr >
                        <td style="width: 200px" class="MyLable">53A.3 Sender's Correspondent Name</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtSenderCorrespondentName" runat="server" Width="355" MaxLength="35"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px" class="MyLable">53A.4 Sender's Correspondent  Addr.</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtSenderCorrespondentAddress1" runat="server" Width="355" MaxLength="35"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px" class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtSenderCorrespondentAddress2" runat="server" Width="355" MaxLength="35"/>
                        </td>
                    </tr>                    
                    <tr>
                        <td style="width: 200px" class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtSenderCorrespondentAddress3" runat="server" Width="355" MaxLength="35"/>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 200px" class="MyLable">54A.1 Receiver's Correspondent Type</td>
                        <td class="MyContent">
                            <telerik:RadComboBox 
                                AutoPostBack="True" 
                                OnSelectedIndexChanged="comboReceiverCorrespondentType_OnSelectedIndexChanged"
                                ID="comboReceiverCorrespondentType" 
                                runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                                <Items>
                                    <telerik:RadComboBoxItem Value="A" Text="A" />
                                    <telerik:RadComboBoxItem Value="B" Text="B" />
                                    <telerik:RadComboBoxItem Value="D" Text="D" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr >
                        <td style="width: 200px" class="MyLable">54A.2 Receiver's Correspondent No.</td>
                        <td class="MyContent" style="width: 150px">
                            <telerik:RadTextBox ID="txtReceiverCorrespondentNo" runat="server" Width="355" MaxLength="34"
                                AutoPostBack="True" 
                                OnTextChanged="txtReceiverCorrespondentNo_OnTextChanged" />
                        </td>
                        <td><asp:Label ID="lblReceiverCorrespondentError" runat="server" ForeColor="red"/></td>
                    </tr>
                </table>                
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr >
                        <td style="width: 200px" class="MyLable">54A.3 Receiver's Correspondent Name</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtReceiverCorrespondentName" runat="server" Width="355" MaxLength="35"/>
                        </td>
                    </tr>                
                    <tr>
                        <td style="width: 200px" class="MyLable">54A.4 Receiver's Correspondent Addr.</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtReceiverCorrespondentAddr1" runat="server" Width="355" MaxLength="35"/>
                        </td>
                    </tr>                
                    <tr>
                        <td style="width: 200px" class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtReceiverCorrespondentAddr2" runat="server" Width="355" MaxLength="35"/>
                        </td>
                    </tr>                
                    <tr>
                        <td style="width: 200px" class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtReceiverCorrespondentAddr3" runat="server" Width="355" MaxLength="35"/>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td style="width: 200px" class="MyLable">Receiver's Correspondent</td>
                        <td class="MyContent">
                            <asp:Label ID="lblReceiverCorrespondentNameMT4001" runat="server" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td style="width: 200px" class="MyLable">Receiver's Correspondent</td>
                        <td class="MyContent">
                            <asp:Label ID="lblReceiverCorrespondentNameMT4002" runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr style="display:none;">
                        <td style="width: 200px" class="MyLable">Detail of Charges</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtDetailOfCharges1" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td style="width: 200px" class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtDetailOfCharges2" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td style="width: 200px" class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtDetailOfCharges3" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px" class="MyLable">72. Sender to Receiver Information</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtSenderToReceiverInformation1_400_1" runat="server" Width="355" MaxLength="35"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px" class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtSenderToReceiverInformation1_400_2" runat="server" Width="355" MaxLength="35"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px" class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtSenderToReceiverInformation1_400_3" runat="server" Width="355" MaxLength="35"/>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
    <div id="Charges" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Waive Charges</td>
                <td class="MyContent">
                    <telerik:radcombobox
                        id="cboWaiveCharges" runat="server"
                        markfirstmatch="True" OnClientSelectedIndexChanged="cboWaiveCharges_OnClientSelectedIndexChanged"
                        allowcustomtext="false">
                        <Items>
                            <telerik:RadComboBoxItem Value="YES" Text="NO" />
                            <telerik:RadComboBoxItem Value="NO" Text="YES" />                            
                        </Items>
                    </telerik:radcombobox>
                </td>
            </tr>
        </table>
        <div id="divWaiveCharges" runat="server">
        <table width="100%" cellpadding="0" cellspacing="0" style="border-bottom: 1px solid #CCC;">
            <tr>
                <td class="MyLable">Charge Remarks</td>
                <td class="MyContent">
                    <asp:TextBox ID="txtChargeRemarks" runat="server" Width="300" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">VAT No</td>
                <td class="MyContent">
                    <asp:TextBox ID="txtVatNo" runat="server" Enabled="false" Width="300" />
                </td>
            </tr>
        </table>
        <telerik:radtabstrip runat="server" id="RadTabStrip3" selectedindex="0" multipageid="RadMultiPage1" orientation="HorizontalTop">
            <Tabs>
                <telerik:RadTab Text="Cable Charge">
                </telerik:RadTab>
                <telerik:RadTab Text="Payment Charge">
                </telerik:RadTab>
                <telerik:RadTab Text="Handling Charge">
                </telerik:RadTab>
                <telerik:RadTab Text="Discrepencies Charge">
                </telerik:RadTab>
                <telerik:RadTab Text="Other Charge">
                </telerik:RadTab>
            </Tabs>
        </telerik:radtabstrip>
        <telerik:radmultipage runat="server" id="RadMultiPage1" selectedindex="0">
            <telerik:RadPageView runat="server" ID="RadPageView1" >
                <div runat="server" ID="tabCableCharge" style="padding-top:10px;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Charge Code</td>
                            <td class="MyContent">
                                <telerik:RadComboBox 
                                    ID="tabCableCharge_cboChargeCode" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false" enabled="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="ILC.CABLE" Text="ILC.CABLE" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>                                       
                        <tr>
                            <td class="MyLable">Charge Ccy</td>
                            <td class="MyContent">
                                <telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="tabCableCharge_cboChargeCcy" runat="server"
                                    MarkFirstMatch="True" AllowCustomText="false" AutoPostBack="True" OnSelectedIndexChanged="tabCableCharge_cboChargeCcy_SelectIndexChange">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent"><telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="tabCableCharge_cboChargeAcc" runat="server"
                                    MarkFirstMatch="True" width="300"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox></td>
                        </tr>  
                        <tr style="display:none;">
                            <td class="MyLable">Exchange Rate</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox runat="server" ID="tabCableCharge_txtExchangeRate" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Charge Amt</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox runat="server" ID="tabCableCharge_txtChargeAmt" AutoPostBack="true" OnTextChanged="tabCableCharge_txtChargeAmt_TextChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Party Charged</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="tabCableCharge_cboPartyCharged" runat="server"
                                    MarkFirstMatch="True" AllowCustomText="false"
                                    AutoPostBack="True" OnSelectedIndexChanged="tabCableCharge_cboPartyCharged_SelectIndexChange">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Amort Charges</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="tabCableCharge_cboAmortCharge" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="NO" Text="NO" />
                                        <telerik:RadComboBoxItem Value="YES" Text="YES" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="MyLable">Amt. In Local CCY</td>
                            <td class="MyContent"></td>
                        </tr >
                        <tr style="display: none;">
                            <td class="MyLable">Amt DR from Acct</td>
                            <td class="MyContent"></td>
                        </tr>
                        <tr style="display:none;">
                            <td class="MyLable">Charge Status</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="tabCableCharge_cboChargeStatus" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="" />
                                        <telerik:RadComboBoxItem Value="CHARGE COLECTED" Text="2" />
                                        <telerik:RadComboBoxItem Value="CHARGE UNCOLECTED" Text="3" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Tax Code</td>
                            <td class="MyContent"><telerik:RadTextBox ID="tabCableCharge_txtTaxCode" runat="server" readonly="true" /></td>
                        </tr>
                        <tr style="display: none">
                            <td class="MyLable">Tax Ccy</td>
                            <td class="MyContent"><asp:Label ID="lblTaxCcy" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="MyLable">Tax Amt</td>
                            <td class="MyContent"><telerik:RadNumericTextBox ID="tabCableCharge_txtTaxAmt" runat="server" readonly="true" /></td>
                        </tr>
                        <tr style="display: none;">
                            <td class="MyLable">Tax in LCCY Amt</td>
                            <td class="MyContent"></td>
                        </tr>
                        <tr style="display: none;">
                            <td class="MyLable">Tax Date</td>
                            <td class="MyContent"></td>
                        </tr>
                    </table>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="RadPageView2" >
                <div runat="server" ID="tabPaymentCharge" style="padding-top:10px;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Charge Code</td>
                            <td class="MyContent">
                                <telerik:RadComboBox 
                                    ID="tabPaymentCharge_cboChargeCode" runat="server"
                                    MarkFirstMatch="True"
                                    AllowCustomText="false" enabled="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="ILC.PAYMENT" Text="ILC.PAYMENT" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>                        
                        <tr>
                            <td class="MyLable">Charge Ccy</td>
                            <td class="MyContent">
                                <telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="tabPaymentCharge_cboChargeCcy" runat="server"
                                    MarkFirstMatch="True" AllowCustomText="false" AutoPostBack="True" OnSelectedIndexChanged="tabPaymentCharge_cboChargeCcy_SelectIndexChange">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent"><telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="tabPaymentCharge_cboChargeAcc" runat="server"
                                    MarkFirstMatch="True" width="300"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox></td>
                        </tr>
                        <tr style="display:none;">
                            <td class="MyLable">Exchange Rate</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox runat="server" ID="tabPaymentCharge_txtExchangeRate" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Charge Amt</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox runat="server" ID="tabPaymentCharge_txtChargeAmt" AutoPostBack="true" OnTextChanged="tabPaymentCharge_txtChargeAmt_TextChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Party Charged</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="tabPaymentCharge_cboPartyCharged" runat="server"
                                    MarkFirstMatch="True" AllowCustomText="false"
                                    AutoPostBack="True" OnSelectedIndexChanged="tabPaymentCharge_cboPartyCharged_SelectIndexChange">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Amort Charges</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="tabPaymentCharge_cboAmortCharge" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="NO" Text="NO" />
                                        <telerik:RadComboBoxItem Value="YES" Text="YES" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="MyLable">Amt. In Local CCY</td>
                            <td class="MyContent"></td>
                        </tr >
                        <tr style="display: none;">
                            <td class="MyLable">Amt DR from Acct</td>
                            <td class="MyContent"></td>
                        </tr>
                        <tr style="display:none;">
                            <td class="MyLable">Charge Status</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="tabPaymentCharge_cboChargeStatus" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="" />
                                        <telerik:RadComboBoxItem Value="CHARGE COLECTED" Text="2" />
                                        <telerik:RadComboBoxItem Value="CHARGE UNCOLECTED" Text="3" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Tax Code</td>
                            <td class="MyContent"><telerik:RadTextBox ID="tabPaymentCharge_txtTaxCode" runat="server" readonly="true" /></td>
                        </tr>
                        <tr style="display: none">
                            <td class="MyLable">Tax Ccy</td>
                            <td class="MyContent"><asp:Label ID="Label9" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="MyLable">Tax Amt</td>
                            <td class="MyContent"><telerik:RadNumericTextBox ID="tabPaymentCharge_txtTaxAmt" runat="server" readonly="true" /></td>
                        </tr>
                        <tr style="display: none;">
                            <td class="MyLable">Tax in LCCY Amt</td>
                            <td class="MyContent"></td>
                        </tr>
                        <tr style="display: none;">
                            <td class="MyLable">Tax Date</td>
                            <td class="MyContent"></td>
                        </tr>
                    </table>
                </div> 
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="RadPageView3" >
                <div runat="server" ID="tabHandlingCharge" style="padding-top:10px;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Charge Code</td>
                            <td class="MyContent">
                                <telerik:RadComboBox 
                                    ID="tabHandlingCharge_cboChargeCode" runat="server"
                                    MarkFirstMatch="True"
                                    AllowCustomText="false" enabled="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="ILC.HANDLING" Text="ILC.HANDLING" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>                        
                        <tr>
                            <td class="MyLable">Charge Ccy</td>
                            <td class="MyContent">
                                <telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="tabHandlingCharge_cboChargeCcy" runat="server"
                                    MarkFirstMatch="True" AllowCustomText="false" AutoPostBack="True" OnSelectedIndexChanged="tabHandlingCharge_cboChargeCcy_SelectIndexChange">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent"><telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="tabHandlingCharge_cboChargeAcc" runat="server"
                                    MarkFirstMatch="True" width="300"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox></td>
                        </tr>
                        <tr style="display:none;">
                            <td class="MyLable">Exchange Rate</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox runat="server" ID="tabHandlingCharge_txtExchangeRate" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Charge Amt</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox runat="server" ID="tabHandlingCharge_txtChargeAmt" AutoPostBack="true" OnTextChanged="tabHandlingCharge_txtChargeAmt_TextChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Party Charged</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="tabHandlingCharge_cboPartyCharged" runat="server"
                                    MarkFirstMatch="True" AllowCustomText="false"
                                    AutoPostBack="True" OnSelectedIndexChanged="tabHandlingCharge_cboPartyCharged_SelectIndexChange">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Amort Charges</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="tabHandlingCharge_cboAmortCharge" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="NO" Text="NO" />
                                        <telerik:RadComboBoxItem Value="YES" Text="YES" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="MyLable">Amt. In Local CCY</td>
                            <td class="MyContent"></td>
                        </tr >
                        <tr style="display: none;">
                            <td class="MyLable">Amt DR from Acct</td>
                            <td class="MyContent"></td>
                        </tr>
                        <tr style="display:none;">
                            <td class="MyLable">Charge Status</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="tabHandlingCharge_cboChargeStatus" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="" />
                                        <telerik:RadComboBoxItem Value="CHARGE COLECTED" Text="2" />
                                        <telerik:RadComboBoxItem Value="CHARGE UNCOLECTED" Text="3" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Tax Code</td>
                            <td class="MyContent"><telerik:RadTextBox ID="tabHandlingCharge_txtTaxCode" runat="server" readonly="true" /></td>
                        </tr>
                        <tr style="display: none">
                            <td class="MyLable">Tax Ccy</td>
                            <td class="MyContent"><asp:Label ID="Label12" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="MyLable">Tax Amt</td>
                            <td class="MyContent"><telerik:RadNumericTextBox ID="tabHandlingCharge_txtTaxAmt" runat="server" readonly="true" /></td>
                        </tr>
                        <tr style="display: none;">
                            <td class="MyLable">Tax in LCCY Amt</td>
                            <td class="MyContent"></td>
                        </tr>
                        <tr style="display: none;">
                            <td class="MyLable">Tax Date</td>
                            <td class="MyContent"></td>
                        </tr>
                    </table>
                </div> 
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="RadPageView4" >
                <div runat="server" ID="tabDiscrepenciesCharge" style="padding-top:10px;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Charge Code</td>
                            <td class="MyContent">
                                <telerik:RadComboBox 
                                    ID="tabDiscrepenciesCharge_cboChargeCode" runat="server"
                                    MarkFirstMatch="True"
                                    AllowCustomText="false" enabled="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="ILC.DISCRP" Text="ILC.DISCRP" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>                        
                        <tr>
                            <td class="MyLable">Charge Ccy</td>
                            <td class="MyContent">
                                <telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="tabDiscrepenciesCharge_cboChargeCcy" runat="server"
                                    MarkFirstMatch="True" AllowCustomText="false" AutoPostBack="True" OnSelectedIndexChanged="tabDiscrepenciesCharge_cboChargeCcy_SelectIndexChange">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent"><telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="tabDiscrepenciesCharge_cboChargeAcc" runat="server"
                                    MarkFirstMatch="True" width="300"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox></td>
                        </tr>
                        <tr style="display:none;">
                            <td class="MyLable">Exchange Rate</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox runat="server" ID="tabDiscrepenciesCharge_txtExchangeRate" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Charge Amt</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox runat="server" ID="tabDiscrepenciesCharge_txtChargeAmt" AutoPostBack="true" OnTextChanged="tabDiscrepenciesCharge_txtChargeAmt_TextChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Party Charged</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="tabDiscrepenciesCharge_cboPartyCharged" runat="server"
                                    MarkFirstMatch="True" AllowCustomText="false"
                                    AutoPostBack="True" OnSelectedIndexChanged="tabDiscrepenciesCharge_cboPartyCharged_SelectIndexChange">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Amort Charges</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="tabDiscrepenciesCharge_cboAmortCharge" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="NO" Text="NO" />
                                        <telerik:RadComboBoxItem Value="YES" Text="YES" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="MyLable">Amt. In Local CCY</td>
                            <td class="MyContent"></td>
                        </tr >
                        <tr style="display: none;">
                            <td class="MyLable">Amt DR from Acct</td>
                            <td class="MyContent"></td>
                        </tr>
                        <tr style="display:none;">
                            <td class="MyLable">Charge Status</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="tabDiscrepenciesCharge_cboChargeStatus" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="" />
                                        <telerik:RadComboBoxItem Value="CHARGE COLECTED" Text="2" />
                                        <telerik:RadComboBoxItem Value="CHARGE UNCOLECTED" Text="3" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Tax Code</td>
                            <td class="MyContent"><telerik:RadTextBox ID="tabDiscrepenciesCharge_txtTaxCode" runat="server" readonly="true" /></td>
                        </tr>
                        <tr style="display: none">
                            <td class="MyLable">Tax Ccy</td>
                            <td class="MyContent"><asp:Label ID="Label4" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="MyLable">Tax Amt</td>
                            <td class="MyContent"><telerik:RadNumericTextBox ID="tabDiscrepenciesCharge_txtTaxAmt" runat="server" readonly="true" /></td>
                        </tr>
                        <tr style="display: none;">
                            <td class="MyLable">Tax in LCCY Amt</td>
                            <td class="MyContent"></td>
                        </tr>
                        <tr style="display: none;">
                            <td class="MyLable">Tax Date</td>
                            <td class="MyContent"></td>
                        </tr>
                    </table>
                </div> 
            </telerik:RadPageView>            
            <telerik:RadPageView runat="server" ID="RadPageView5" >
                <div runat="server" ID="tabOtherCharge" style="padding-top:10px;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Charge Code</td>
                            <td class="MyContent">
                                <telerik:RadComboBox 
                                    ID="tabOtherCharge_cboChargeCode" runat="server"
                                    MarkFirstMatch="True"
                                    AllowCustomText="false" enabled="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="ILC.OTHER" Text="ILC.OTHER" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>                        
                        <tr>
                            <td class="MyLable">Charge Ccy</td>
                            <td class="MyContent">
                                <telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="tabOtherCharge_cboChargeCcy" runat="server"
                                    MarkFirstMatch="True" AllowCustomText="false" AutoPostBack="True" OnSelectedIndexChanged="tabOtherCharge_cboChargeCcy_SelectIndexChange">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent"><telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="tabOtherCharge_cboChargeAcc" runat="server"
                                    MarkFirstMatch="True" width="300"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox></td>
                        </tr>
                        <tr style="display:none;">
                            <td class="MyLable">Exchange Rate</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox runat="server" ID="tabOtherCharge_txtExchangeRate" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Charge Amt</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox runat="server" ID="tabOtherCharge_txtChargeAmt" AutoPostBack="true" OnTextChanged="tabOtherCharge_txtChargeAmt_TextChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Party Charged</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="tabOtherCharge_cboPartyCharged" runat="server"
                                    MarkFirstMatch="True" AllowCustomText="false"
                                    AutoPostBack="True" OnSelectedIndexChanged="tabOtherCharge_cboPartyCharged_SelectIndexChange">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Amort Charges</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="tabOtherCharge_cboAmortCharge" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="NO" Text="NO" />
                                        <telerik:RadComboBoxItem Value="YES" Text="YES" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="MyLable">Amt. In Local CCY</td>
                            <td class="MyContent"></td>
                        </tr >
                        <tr style="display: none;">
                            <td class="MyLable">Amt DR from Acct</td>
                            <td class="MyContent"></td>
                        </tr>
                        <tr style="display:none;">
                            <td class="MyLable">Charge Status</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="tabOtherCharge_cboChargeStatus" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="" />
                                        <telerik:RadComboBoxItem Value="CHARGE COLECTED" Text="2" />
                                        <telerik:RadComboBoxItem Value="CHARGE UNCOLECTED" Text="3" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Tax Code</td>
                            <td class="MyContent"><telerik:RadTextBox ID="tabOtherCharge_txtTaxCode" runat="server" readonly="true" /></td>
                        </tr>
                        <tr style="display: none">
                            <td class="MyLable">Tax Ccy</td>
                            <td class="MyContent"><asp:Label ID="Label6" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="MyLable">Tax Amt</td>
                            <td class="MyContent"><telerik:RadNumericTextBox ID="tabOtherCharge_txtTaxAmt" runat="server" readonly="true" /></td>
                        </tr>
                        <tr style="display: none;">
                            <td class="MyLable">Tax in LCCY Amt</td>
                            <td class="MyContent"></td>
                        </tr>
                        <tr style="display: none;">
                            <td class="MyLable">Tax Date</td>
                            <td class="MyContent"></td>
                        </tr>
                    </table>
                </div> 
            </telerik:RadPageView>
        </telerik:radmultipage>
            </div>
    </div>
</div>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" src="DesktopModules/TrainingCoreBanking/BankProject/Scripts/Common.js"></script>
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
                radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file ?", confirmCallbackFunction_PhieuNgoaiBang, 420, 150, null, 'Download');
            }
            if (button.get_commandName() == '<%=BankProject.Controls.Commands.Commit%>') {
                if (!MTIsValidInput('MT202', null)) {
                    args.set_cancel(true);
                    return;
                }
                if (!MTIsValidInput('MT756', null)) {
                    args.set_cancel(true);
                    return;
                }
            }
        }
        function confirmCallbackFunction_PhieuNgoaiBang(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnReportPhieuXuatNgoaiBang.ClientID %>").click();
            }
            radconfirm("Do you want to download PHIEU CHUYEN KHOAN file?", confirmCallbackFunction_PhieuChuyenKhoan, 420, 150, null, 'Download');
        }
        function confirmCallbackFunction_PhieuChuyenKhoan(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnReportPhieuChuyenKhoan.ClientID %>").click();
            }
            radconfirm("Do you want to download MT202?", confirmCallbackFunction_MT202, 365, 150, null, 'Download');
        }        
        function confirmCallbackFunction_MT202(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnReportMT202.ClientID %>").click();
            }
            if ($find("<%=comboCreateMT756.ClientID%>").get_value() == "YES")
                radconfirm("Do you want to download MT756?", confirmCallbackFunction_MT756, 365, 150, null, 'Download');
            else if ($find("<%=cboWaiveCharges.ClientID%>").get_value() == "YES")
                radconfirm("Do you want to download HOA DON VAT file?", confirmCallbackFunction_VATB, 365, 150, null, 'Download');
        }
        function confirmCallbackFunction_MT756(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnReportMT756.ClientID %>").click();
            }
            if ($find("<%=cboWaiveCharges.ClientID%>").get_value() == "YES")
                radconfirm("Do you want to download HOA DON VAT file?", confirmCallbackFunction_VATB, 365, 150, null, 'Download');
        }
        function confirmCallbackFunction_VATB(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnReportVATb.ClientID %>").click();
            }            
        }
        //
        $("#<%=txtCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $('#<%=btnLoadDocsInfo.ClientID%>').click();
            }
        });
        function txtRelatedReference_OnValueChanged() {
            $find("<%=txtRelatedReferenceMT400.ClientID%>").set_value($find("<%=txtRelatedReference.ClientID%>").get_value());
        }
        //
        function cboWaiveCharges_OnClientSelectedIndexChanged() {
            var objW = $('#<%=divWaiveCharges.ClientID%>');
            if ($find("<%=cboWaiveCharges.ClientID%>").get_value() == "NO")
                objW.css("display", "none");
            else
                objW.css("display", "");
        }
        function comboCreateMT756_OnSelectedIndexChanged() {
            var objW = $('#<%=divMT756.ClientID%>');
            if ($find("<%=comboCreateMT756.ClientID%>").get_value() == "NO")
                objW.css("display", "none");
            else
                objW.css("display", "");
        }

        function autoCompleteReceiverCorrespondent() {
            if ($find("<%=comboAccountWithInstitutionType.ClientID%>").get_selectedItem().get_value() == "D") {
                $find('<%=txtReceiverCorrespondentName.ClientID %>').set_value($find('<%=txtAccountWithInstitutionName.ClientID %>').get_value());
                $find('<%=txtReceiverCorrespondentAddr1.ClientID %>').set_value($find('<%=txtAccountWithInstitutionAddr1.ClientID %>').get_value());
                $find('<%=txtReceiverCorrespondentAddr2.ClientID %>').set_value($find('<%=txtAccountWithInstitutionAddr2.ClientID %>').get_value());
                $find('<%=txtReceiverCorrespondentAddr3.ClientID %>').set_value($find('<%=txtAccountWithInstitutionAddr3.ClientID %>').get_value());
            }
        }
        function txtAccountWithInstitutionName_OnValueChanged(sender, eventArgs) {
            autoCompleteReceiverCorrespondent();
        }
        function txtAccountWithInstitutionAddr1_OnValueChanged(sender, eventArgs) {
            autoCompleteReceiverCorrespondent();
        }
        function txtAccountWithInstitutionAddr2_OnValueChanged(sender, eventArgs) {
            autoCompleteReceiverCorrespondent();
        }
        function txtAccountWithInstitutionAddr3_OnValueChanged(sender, eventArgs) {
            autoCompleteReceiverCorrespondent();
        }
    </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"><img src="icons/bank/ajax-loader-16x16.gif" /></telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
    <AjaxSettings>        
        <telerik:AjaxSetting AjaxControlID="tabCableCharge_txtChargeAmt">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabCableCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabCableCharge_txtTaxCode" />        
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" />
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabPaymentCharge_txtChargeAmt">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabPaymentCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabPaymentCharge_txtTaxCode" />    
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" />    
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabHandlingCharge_txtChargeAmt">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabHandlingCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabHandlingCharge_txtTaxCode" />  
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" />      
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabDiscrepenciesCharge_txtChargeAmt">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabDiscrepenciesCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabDiscrepenciesCharge_txtTaxCode" /> 
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" />      
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" /> 
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabOtherCharge_txtChargeAmt">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabOtherCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabOtherCharge_txtTaxCode" /> 
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" /> 
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />      
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabCableCharge_cboPartyCharged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabCableCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabCableCharge_txtTaxCode" />
                <telerik:AjaxUpdatedControl ControlID="tabCableCharge_cboChargeAcc" />  
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" />  
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />    
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabPaymentCharge_cboPartyCharged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabPaymentCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabPaymentCharge_txtTaxCode" /> 
                <telerik:AjaxUpdatedControl ControlID="tabPaymentCharge_cboChargeAcc" />   
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" />  
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />    
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabHandlingCharge_cboPartyCharged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabHandlingCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabHandlingCharge_txtTaxCode" /> 
                <telerik:AjaxUpdatedControl ControlID="tabHandlingCharge_cboChargeAcc" />    
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" /> 
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />     
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabDiscrepenciesCharge_cboPartyCharged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabDiscrepenciesCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabDiscrepenciesCharge_txtTaxCode" /> 
                <telerik:AjaxUpdatedControl ControlID="tabDiscrepenciesCharge_cboChargeAcc" />
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" />    
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" /> 
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabOtherCharge_cboPartyCharged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabOtherCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabOtherCharge_txtTaxCode" />   
                <telerik:AjaxUpdatedControl ControlID="tabOtherCharge_cboChargeAcc" />
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" />  
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />   
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabCableCharge_cboChargeCcy">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabCableCharge_cboChargeAcc" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabPaymentCharge_cboChargeCcy">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabPaymentCharge_cboChargeAcc" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabHandlingCharge_cboChargeCcy">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabHandlingCharge_cboChargeAcc" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabDiscrepenciesCharge_cboChargeCcy">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabDiscrepenciesCharge_cboChargeAcc" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabOtherCharge_cboChargeCcy">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabOtherCharge_cboChargeAcc" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="comboIntermediaryBankType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtIntermediaryBank" />
                <telerik:AjaxUpdatedControl ControlID="txtIntermediaryBankName" />
                <telerik:AjaxUpdatedControl ControlID="txtIntermediaryBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtIntermediaryBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtIntermediaryBankAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtIntermediaryBank">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblIntermediaryBankNoError" />
                <telerik:AjaxUpdatedControl ControlID="txtIntermediaryBankName" />
                <telerik:AjaxUpdatedControl ControlID="txtIntermediaryBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtIntermediaryBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtIntermediaryBankAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="comboAccountWithInstitutionType">
            <UpdatedControls>                
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithInstitution" />
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithInstitutionName" />
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithInstitutionAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithInstitutionAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithInstitutionAddr3" />

                <telerik:AjaxUpdatedControl ControlID="comboReceiverCorrespondentType" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentNo" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentName" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtAccountWithInstitution">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAccountWithInstitutionError" />
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithInstitutionName" />
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithInstitutionAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithInstitutionAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithInstitutionAddr3" />

                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentNo" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentName" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="comboBeneficiaryBankType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryBank" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryBankName" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryBankAddr3" />                
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtBeneficiaryBank">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblBeneficiaryBankError" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryBankName" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryBankAddr3" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentNo" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="comboSenderCorrespondentType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtSenderCorrespondentNo" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderCorrespondentName" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderCorrespondentAddress1" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderCorrespondentAddress2" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderCorrespondentAddress3" />                
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtSenderCorrespondentNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblSenderCorrespondentNoError" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderCorrespondentName" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderCorrespondentAddress1" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderCorrespondentAddress2" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderCorrespondentAddress3" />                
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="comboReceiverCorrespondentType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentNo" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentName" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentAddr3" />                
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtReceiverCorrespondentNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblReceiverCorrespondentError" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentName" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtReceiverCorrespondentAddr3" />                
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboNostroAcct">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtSenderCorrespondentNo" />
                <telerik:AjaxUpdatedControl ControlID="lblSenderCorrespondentNoError" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderCorrespondentName" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderCorrespondentAddress1" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderCorrespondentAddress2" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderCorrespondentAddress3" />
                
                <telerik:AjaxUpdatedControl ControlID="tabCableCharge_cboChargeAcc" />
                <telerik:AjaxUpdatedControl ControlID="tabPaymentCharge_cboChargeAcc" />
                <telerik:AjaxUpdatedControl ControlID="tabHandlingCharge_cboChargeAcc" />
                <telerik:AjaxUpdatedControl ControlID="tabDiscrepenciesCharge_cboChargeAcc" />
                <telerik:AjaxUpdatedControl ControlID="tabOtherCharge_cboChargeAcc" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<div style="visibility: hidden;">
    <asp:Button ID="btnReportPhieuXuatNgoaiBang" runat="server" OnClick="btnReportPhieuXuatNgoaiBang_Click" Text="PhieuXuatNgoaiBang" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnReportPhieuChuyenKhoan" runat="server" OnClick="btnReportPhieuChuyenKhoan_Click" Text="PhieuChuyenKhoan" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnReportMT202" runat="server" OnClick="btnReportMT202_Click" Text="MT202" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnReportMT756" runat="server" OnClick="btnReportMT756_Click" Text="MT756" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnReportVATb" runat="server" OnClick="btnReportVATb_Click" Text="VATb" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnLoadDocsInfo" runat="server" OnClick="btnLoadDocsInfo_Click" Text="LoadDocsInfo" /></div>