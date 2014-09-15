<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Payment.ascx.cs" Inherits="BankProject.TradingFinance.Import.DocumentaryCredit.Payment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:radwindowmanager id="RadWindowManager1" runat="server" enableshadow="true" />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />
<style>
    .labelDisabled {
        color: darkgray !important;
    }
    .paymentControlWidth {
        width:200px !important;
    }
</style>
<telerik:radtoolbar runat="server" id="RadToolBar1" enableroundedcorners="true" enableshadows="true" width="100%"
    onbuttonclick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommit" CommandName="commit" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btPreview" CommandName="preview" postback="false" Enabled="true">
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
            <asp:HiddenField ID="txtPaymentId" runat="server" Value="0" /><asp:HiddenField ID="txtDocId" runat="server" />
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
        <li><a href="#Charges">Charges</a></li>
    </ul>
    <div id="Main" class="dnnClear">
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;"></div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Draw Type</td>
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
                    <td class="MyLable">Currency</td>
                    <td class="MyContent"><asp:Label ID="lblCurrency" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Drawing Amount<span class="Required"> (*)</span></td>
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
                    <td class="MyLable">Value Date</td>
                    <td class="MyContent"><telerik:raddatepicker id="txtValueDate" runat="server" Width="180px" /></td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Payment Instructions</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Deposit Account</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboDepositAccount" 
                            Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" width="300" >
                        </telerik:RadComboBox> <asp:Label ID="lblDepositAccountName" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Exchange Rate</td>
                    <td class="MyContent"><telerik:radnumerictextbox id="txtExchangeRate" runat="server" CssClass="paymentControlWidth" /></td>
                </tr>
                <tr>
                    <td class="MyLable labelDisabled">Amt DR Fr Acct Ccy</td>
                    <td class="MyContent labelDisabled"><telerik:radnumerictextbox ID="txtAmtDRFrAcctCcy" runat="server" CssClass="paymentControlWidth labelDisabled" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td class="MyLable labelDisabled">Prov Amt Release</td>
                    <td class="MyContent labelDisabled"><telerik:radnumerictextbox id="txtProvAmtRelease" runat="server" CssClass="paymentControlWidth labelDisabled" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Amt DR from Acct <span class="Required">(*)</span></td>
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
                <tr>
                    <td class="MyLable">Prov Cover Acct</td>
                    <td class="MyContent">
                        <telerik:radcombobox
                            id="cboProvCoverAcct" runat="server" autopostback="False"
                            markfirstmatch="True"
                            allowcustomtext="false" width="300">
                        </telerik:radcombobox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Prov Exchange Rate</td>
                    <td class="MyContent"><telerik:radnumerictextbox id="txtProvExchangeRate" runat="server" CssClass="paymentControlWidth" /></td>
                </tr>
                <tr>
                    <td class="MyLable labelDisabled">Cover Amount</td>
                    <td class="MyContent labelDisabled"><telerik:radnumerictextbox id="txtCoverAmount" runat="server" CssClass="paymentControlWidth labelDisabled" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Payment Method</td>
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
                    <td class="MyLable">Nostro Acct</td>
                    <td class="MyContent">
                        <telerik:radcombobox
                            appenddatabounditems="True"
                            id="cboNostroAcct" runat="server"
                            markfirstmatch="True" width="300"
                            allowcustomtext="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            
                        </telerik:radcombobox> <asp:Label ID="lblNostroAcctName" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable labelDisabled">Amount Credited</td>
                    <td class="MyContent labelDisabled"><telerik:radnumerictextbox id="txtAmountCredited" runat="server" CssClass="paymentControlWidth labelDisabled" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Payment Remarks</td>
                    <td class="MyContent"><telerik:radtextbox id="txtPaymentRemarks" runat="server" CssClass="paymentControlWidth" /></td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Utilisation Details</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Fully Utilised</td>
                    <td class="MyContent"><asp:TextBox ID="txtFullyUtilised" runat="server" CssClass="paymentControlWidth"></asp:TextBox></td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div id="Charges" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Waive Charges</td>
                <td class="MyContent">
                    <telerik:radcombobox
                        id="cboWaiveCharges" runat="server"
                        markfirstmatch="True"
                        allowcustomtext="false">
                        <Items>
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                            <telerik:RadComboBoxItem Value="YES" Text="YES" />
                        </Items>
                    </telerik:radcombobox>
                </td>
            </tr>
        </table>
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
                                    MarkFirstMatch="True" width="300" 
                                    AllowCustomText="false" enabled="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="ILC.CABLE" Text="CABLE CHARGE FOR IMPORT LC" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0" id="table1" runat="server" style="display:none;">
                        <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent"><telerik:RadTextBox ID="tabCableCharge_txtChargeAcct" runat="server" /></td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">                        
                        <tr>
                            <td class="MyLable">Charge Ccy</td>
                            <td class="MyContent">
                                <telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="tabCableCharge_cboChargeCcy" runat="server"
                                    MarkFirstMatch="True"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
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
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Party Charged</td>
                            <td class="MyContent" style="width: 150px;">
                                <telerik:RadComboBox
                                    ID="tabCableCharge_cboPartyCharged" runat="server"
                                    MarkFirstMatch="True" AllowCustomText="false"
                                    AutoPostBack="True" OnSelectedIndexChanged="tabCableCharge_cboPartyCharged_SelectIndexChange">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                            <td><asp:Label ID="lblPartyCharged" runat="server" /></td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">
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
                            <td class="MyContent" style="width: 150px;">
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
                            <td><asp:Label ID="lblChargeStatus" runat="server" /></td>
                        </tr>
                    </table>                
                    <table width="100%" cellpadding="0" cellspacing="0">
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
                                    MarkFirstMatch="True" width="300" 
                                    AllowCustomText="false" enabled="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="ILC.PAYMENT" Text="PAYMENT CHARGE FOR IMPORT LC" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0" id="table2" runat="server" style="display:none;">
                        <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent"><telerik:RadTextBox ID="tabPaymentCharge_txtChargeAcct" runat="server" /></td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">                        
                        <tr>
                            <td class="MyLable">Charge Ccy</td>
                            <td class="MyContent">
                                <telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="tabPaymentCharge_cboChargeCcy" runat="server"
                                    MarkFirstMatch="True"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
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
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Party Charged</td>
                            <td class="MyContent" style="width: 150px;">
                                <telerik:RadComboBox
                                    ID="tabPaymentCharge_cboPartyCharged" runat="server"
                                    MarkFirstMatch="True" AllowCustomText="false"
                                    AutoPostBack="True" OnSelectedIndexChanged="tabPaymentCharge_cboPartyCharged_SelectIndexChange">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                            <td><asp:Label ID="Label7" runat="server" /></td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">
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
                            <td class="MyContent" style="width: 150px;">
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
                            <td><asp:Label ID="Label8" runat="server" /></td>
                        </tr>
                    </table>                
                    <table width="100%" cellpadding="0" cellspacing="0">
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
                                    MarkFirstMatch="True" width="300"
                                    AllowCustomText="false" enabled="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="ILC.HANDLING" Text="HANDLING  CHARGE FOR IMPORT LC" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0" id="table3" runat="server" style="display:none;">
                        <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent"><telerik:RadTextBox ID="tabHandlingCharge_txtChargeAcct" runat="server" /></td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">                        
                        <tr>
                            <td class="MyLable">Charge Ccy</td>
                            <td class="MyContent">
                                <telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="tabHandlingCharge_cboChargeCcy" runat="server"
                                    MarkFirstMatch="True"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
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
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Party Charged</td>
                            <td class="MyContent" style="width: 150px;">
                                <telerik:RadComboBox
                                    ID="tabHandlingCharge_cboPartyCharged" runat="server"
                                    MarkFirstMatch="True" AllowCustomText="false"
                                    AutoPostBack="True" OnSelectedIndexChanged="tabHandlingCharge_cboPartyCharged_SelectIndexChange">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                            <td><asp:Label ID="Label10" runat="server" /></td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">
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
                            <td class="MyContent" style="width: 150px;">
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
                            <td><asp:Label ID="Label11" runat="server" /></td>
                        </tr>
                    </table>                
                    <table width="100%" cellpadding="0" cellspacing="0">
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
                                    MarkFirstMatch="True" width="300"
                                    AllowCustomText="false" enabled="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="ILC.DISCRP" Text="DISCREPANCY  CHARGE FOR IMPORT LC" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0" id="table4" runat="server" style="display:none;">
                        <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent"><telerik:RadTextBox ID="tabDiscrepenciesCharge_txtChargeAcct" runat="server" /></td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">                        
                        <tr>
                            <td class="MyLable">Charge Ccy</td>
                            <td class="MyContent">
                                <telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="tabDiscrepenciesCharge_cboChargeCcy" runat="server"
                                    MarkFirstMatch="True"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
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
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Party Charged</td>
                            <td class="MyContent" style="width: 150px;">
                                <telerik:RadComboBox
                                    ID="tabDiscrepenciesCharge_cboPartyCharged" runat="server"
                                    MarkFirstMatch="True" AllowCustomText="false"
                                    AutoPostBack="True" OnSelectedIndexChanged="tabDiscrepenciesCharge_cboPartyCharged_SelectIndexChange">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                            <td><asp:Label ID="Label2" runat="server" /></td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">
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
                            <td class="MyContent" style="width: 150px;">
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
                            <td><asp:Label ID="Label3" runat="server" /></td>
                        </tr>
                    </table>                
                    <table width="100%" cellpadding="0" cellspacing="0">
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
                                    MarkFirstMatch="True" width="300" 
                                    AllowCustomText="false" enabled="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="ILC.OTHER" Text="OTHER CHARGE  CHARGE FOR IMPORT LC" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0" id="table5" runat="server" style="display:none;">
                        <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent"><telerik:RadTextBox ID="tabOtherCharge_txtChargeAcct" runat="server" /></td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">                        
                        <tr>
                            <td class="MyLable">Charge Ccy</td>
                            <td class="MyContent">
                                <telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="tabOtherCharge_cboChargeCcy" runat="server"
                                    MarkFirstMatch="True"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
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
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Party Charged</td>
                            <td class="MyContent" style="width: 150px;">
                                <telerik:RadComboBox
                                    ID="tabOtherCharge_cboPartyCharged" runat="server"
                                    MarkFirstMatch="True" AllowCustomText="false"
                                    AutoPostBack="True" OnSelectedIndexChanged="tabOtherCharge_cboPartyCharged_SelectIndexChange">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                            <td><asp:Label ID="Label1" runat="server" /></td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">
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
                            <td class="MyContent" style="width: 150px;">
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
                            <td><asp:Label ID="Label5" runat="server" /></td>
                        </tr>
                    </table>                
                    <table width="100%" cellpadding="0" cellspacing="0">
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
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function RadToolBar1_OnClientButtonClicking(sender, args) {
            var button = args.get_item();
            //
            if (button.get_commandName() == '<%=BankProject.Controls.Commands.Print%>') {
                args.set_cancel(true);
                radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file ?", confirmCallbackFunction_PhieuNgoaiBang, 420, 150, null, 'Download');
            }
        }
        function confirmCallbackFunction_PhieuNgoaiBang(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnReportPhieuXuatNgoaiBang.ClientID %>").click();
            }
            //Xu ly in hoa don VAT
            //radconfirm("Do you want to download PHIEU CHUYEN KHHOAN file?", confirmCallbackFunction_PhieuCK, 420, 150, null, 'Download');
        }
        //
        $("#<%=txtCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                window.location = 'Default.aspx?tabid=<%=this.TabId%>&tid=' + $('#<%=txtCode.ClientID%>').val();
            }
        });
    </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"><img src="icons/bank/ajax-loader-16x16.gif" />
</telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
    <AjaxSettings>        
        <telerik:AjaxSetting AjaxControlID="tabCableCharge_txtChargeAmt">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabCableCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabCableCharge_txtTaxCode" />        
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabPaymentCharge_txtChargeAmt">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabPaymentCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabPaymentCharge_txtTaxCode" />    
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" />    
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
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabOtherCharge_txtChargeAmt">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabOtherCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabOtherCharge_txtTaxCode" /> 
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" />       
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabCableCharge_cboPartyCharged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabCableCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabCableCharge_txtTaxCode" />  
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" />      
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabPaymentCharge_cboPartyCharged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabPaymentCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabPaymentCharge_txtTaxCode" />  
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" />      
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabHandlingCharge_cboPartyCharged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabHandlingCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabHandlingCharge_txtTaxCode" />  
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" />      
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabDiscrepenciesCharge_cboPartyCharged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabDiscrepenciesCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabDiscrepenciesCharge_txtTaxCode" />   
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" />     
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tabOtherCharge_cboPartyCharged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tabOtherCharge_txtTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="tabOtherCharge_txtTaxCode" />   
                <telerik:AjaxUpdatedControl ControlID="txtAmountCredited" />     
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<div style="visibility: hidden;">
    <asp:Button ID="btnReportPhieuXuatNgoaiBang" runat="server" OnClick="btnReportPhieuXuatNgoaiBang_Click" Text="Search" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnReportVAT" runat="server" OnClick="btnReportVAT_Click" Text="Search" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnReportVATb" runat="server" OnClick="btnReportVATb_Click" Text="Search" /></div>