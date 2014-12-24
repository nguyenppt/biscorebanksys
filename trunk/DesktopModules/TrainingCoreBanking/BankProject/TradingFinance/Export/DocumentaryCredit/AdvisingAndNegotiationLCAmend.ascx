<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisingAndNegotiationLCAmend.ascx.cs" Inherits="BankProject.TradingFinance.Export.DocumentaryCredit.AdvisingAndNegotiationLCAmend" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"></telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />
<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
<script type="text/javascript">
    var tabId = '<%= TabId %>';
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Print%>") {
            args.set_cancel(false);
            radconfirm("Do you want to download Thu Thong Bao file ?", confirmCallbackFunction_ThuThongBao, 420, 150, null, 'Download');
        }
    }
    function confirmCallbackFunction_ThuThongBao(result) {
        clickCalledAfterRadconfirm = false;
        if (result) {
            $("#<%=btnReportThuThongBao.ClientID %>").click();
        }
        setTimeout(function () {
            radconfirm("Do you want to download Phieu Thu file?", confirmCallbackFunction_PhieuThu, 420, 150, null, 'Download');
        }, 5000);

        //radconfirm("Do you want to download Phieu Xuat Ngoai Bang file?", confirmCallbackFunction_PhieuXuatNgoaiBang, 420, 150, null, 'Download');
    }
    function confirmCallbackFunction_PhieuThu(result) {
        clickCalledAfterRadconfirm = false;
        if (result) {
            $("#<%=btnReportPhieuThu.ClientID %>").click();
    }
}
function confirmCallbackFunction_PhieuXuatNgoaiBang(result) {
    clickCalledAfterRadconfirm = false;
    if (result) {
        $("#<%=btnReportPhieuXuatNgoaiBang.ClientID %>").click();
        }
    }
    </script>
</telerik:RadCodeBlock>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%"
        OnClientButtonClicking="OnClientButtonClicking" OnButtonClick="RadToolBar1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
                ToolTip="Commit Data" Value="btCommitData" CommandName="commit">
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
                ToolTip="Search" Value="btSearch" CommandName="search" postback="false">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
                ToolTip="Print Deal Slip" Value="btPrint" CommandName="print" postback="false" Enabled="false">
            </telerik:RadToolBarButton>
        </Items>
    </telerik:RadToolBar>

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="padding-left:20px; padding-top:5px; padding-bottom:5px;"><asp:TextBox ID="tbLCCode" runat="server" Width="200" /> <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator1"
                            ControlToValidate="tbLCCode"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[LC Code] is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>&nbsp;<asp:Label ID="lblError" runat="server" ForeColor="red" /></td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="HiddenField1" runat="server" Value="0" /><asp:HiddenField ID="txtCustomerID" runat="server" Value="" /><asp:HiddenField ID="txtCustomerName" runat="server" Value="" />
        </td>
    </tr>
</table>
    <div class="dnnForm" id="tabs-demo">
        <ul class="dnnAdminTabNav">
            <li><a href="#Main">Main</a></li>
            <li><a href="#Charges">Charges</a></li>
        </ul>
        <div id="Main" class="dnnClear">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px">20.Sender's Reference</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderReference" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px">21.Receiver's Reference</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtReceiverReference" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px">23.Issuing Bank's Reference</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIssuingBankReference" runat="server" Width="355" />
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">52A.1 Issuing Bank Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox Width="195"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="rcbIssuingBankType_OnSelectedIndexChanged"
                            ID="rcbIssuingBankType" runat="server"
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
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">52A.2 Issuing Bank No.</td>
                    <td class="MyContent"><telerik:RadTextBox ID="txtIssuingBankNo" runat="server" Width="195" AutoPostBack="true" OnTextChanged="txtIssuingBankNo_TextChanged" />
                    </td>
                    <td><asp:Label ID="lblIssuingBankMessage" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px">52A.3 Issuing Bank Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIssuingBankName" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">52A.4 Issuing Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIssuingBankAddr1" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIssuingBankAddr2" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIssuingBankAddr3" runat="server" Width="355" />
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width:250px;" class="MyLable">31C. Date of Issue <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator4"
                            ControlToValidate="txtDateOfIssue"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[Date of Issue] is required" ForeColor="Red">
                        </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="txtDateOfIssue" Width="200" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">30.Date of Amendment <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator2"
                            ControlToValidate="txtDateOfAmendment"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[Date of Amendment] is required" ForeColor="Red">
                        </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="txtDateOfAmendment" Width="200" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">26E.Number of Amendment <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator3"
                            ControlToValidate="txtNumberOfAmendment"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[Number of Amendment] is required" ForeColor="Red">
                        </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="txtNumberOfAmendment" Width="200" runat="server" />
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px">59.1 Beneficiary No</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryNo" runat="server" Width="195" AutoPostBack="true" OnTextChanged="txtBeneficiaryNo_TextChanged" />
                    </td>
                    <td><asp:Label ID="lblBeneficiaryMessage" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px">59.2 Beneficiary Name <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator5"
                            ControlToValidate="txtBeneficiaryName"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[Beneficiary Name] is required" ForeColor="Red">
                        </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryName" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">59.3 Beneficiary Address</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryAddr1" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryAddr2" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryAddr3" runat="server" Width="355" />
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width:250px;" class="MyLable">31E.New Date of Expiry <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator6"
                            ControlToValidate="txtNewDateOfExpiry"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[New Date of Expiry] is required" ForeColor="Red">
                        </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="txtNewDateOfExpiry" Width="200" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">32B.Increase of Documentary Credit Amount <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator7"
                            ControlToValidate="txtIncreaseOfDocumentaryCreditAmount"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[Increase of Documentary Credit Amount] is required" ForeColor="Red">
                        </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="txtIncreaseOfDocumentaryCreditAmount" Width="200" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">33B.Decrease of Documentary Credit Amount <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator8"
                            ControlToValidate="txtDecreaseOfDocumentaryCreditAmount"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[Decrease of Documentary Credit Amount] is required" ForeColor="Red">
                        </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="txtDecreaseOfDocumentaryCreditAmount" Width="200" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">34B.New Documentary Credit Amount After Amendment <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator9"
                            ControlToValidate="txtNewDocumentaryCreditAmountAfterAmendment"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[New Documentary Credit Amount After Amendment] is required" ForeColor="Red">
                        </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="txtNewDocumentaryCreditAmountAfterAmendment" Width="200" runat="server" />
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">39A. Percentage Credit Amount Tolerance <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator10"
                            ControlToValidate="txtPercentCreditAmountTolerance1"
                            ValidationGroup="Commit"
                            InitialValue="0"
                            ErrorMessage="[Percentage Credit Amount Tolerance] is required" ForeColor="Red">
                        </asp:RequiredFieldValidator></td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadNumericTextBox Width="195" ID="txtPercentCreditAmountTolerance1" runat="server" Type="Percent" Value="0" MaxValue="100" />
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtPercentCreditAmountTolerance2" runat="server" Type="Percent" MaxValue="100" Value="0" Width="157" /></td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width:250px;" class="MyLable">44A. Place of taking in charge...</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="tbPlaceoftakingincharge" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">44E. Port of loading... <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator14"
                            ControlToValidate="tbPortofloading"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[Port of loading] is required" ForeColor="Red">
                        </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="tbPortofloading" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">44F. Port of Discharge... <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator13"
                            ControlToValidate="tbPortofDischarge"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[Port of Discharge] is required" ForeColor="Red">
                        </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="tbPortofDischarge" runat="server" />
                    </td>
                </tr>                
                <tr>
                    <td class="MyLable">44B. Place of final destination</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="tbPlaceoffinalindistination" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">44C. Latest Date of Shipment <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator15"
                            ControlToValidate="tbLatesDateofShipment"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[Latest Date of Shipment] is required" ForeColor="Red">
                        </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:RadDatePicker runat="server" ID="tbLatesDateofShipment" Width="200" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;" class="MyLable">79.Narrative</td>
                    <td class="MyContent" style="vertical-align: top;">
                        <telerik:RadTextBox ID="txtNarrative" runat="server" TextMode="MultiLine" Height="100" Width="355"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px; vertical-align: top;" class="MyLable">72. Sender to Receiver Information</td>
                    <td class="MyContent" style="vertical-align: top;">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation" runat="server" TextMode="MultiLine" Height="100" Width="355"></telerik:RadTextBox>
                    </td>
                </tr>
            </table>            
        </div>
        <div id="Charges" class="dnnClear">
            <fieldset>
                <legend>
                    <span style="font-weight: bold; text-transform: uppercase;">Charge Details</span>
                </legend>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable">Waive Charges</td>
                        <td class="MyContent">
                            <telerik:RadComboBox AutoPostBack="True"
                                OnSelectedIndexChanged="rcbWaiveCharges_OnSelectedIndexChanged"
                                ID="rcbWaiveCharges" runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                                <Items>
                                    <telerik:RadComboBoxItem Value="NO" Text="NO" />
                                    <telerik:RadComboBoxItem Value="YES" Text="YES" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="border-bottom: 1px solid #CCC;">
                    <tr>
                        <td class="MyLable">Charge Remarks</td>
                        <td class="MyContent">
                            <asp:TextBox ID="tbChargeRemarks" runat="server" Width="300" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">VAT No</td>
                        <td class="MyContent">
                            <asp:TextBox ID="tbVatNo" runat="server" Enabled="false" Width="300" />
                        </td>
                    </tr>
                </table>
                <telerik:RadTabStrip runat="server" ID="RadTabStrip3" SelectedIndex="0" MultiPageID="RadMultiPage1" Orientation="HorizontalTop">
                    <Tabs>
                        <telerik:RadTab Text="LC Advising Charge">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Courier Charge ">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Other Charge">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" >
                    <telerik:RadPageView runat="server" ID="RadPageView1" >
                        <div runat="server" ID="divCABLECHG">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="MyLable">Charge code</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox 
                                            ID="rcbChargeCode" runat="server"
                                            MarkFirstMatch="True" Enabled="false"
                                            AllowCustomText="false">
                                            <ExpandAnimation Type="None" />
                                            <CollapseAnimation Type="None" />
                                            <Items>
                                                <telerik:RadComboBoxItem Value="ELC.ADVISE" Text="ELC.ADVISE" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Charge Ccy</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox
                                            ID="rcbChargeCcy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rcbChargeCcy_OnSelectedIndexChanged"
                                            MarkFirstMatch="True"
                                            AllowCustomText="false">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Charge Acct</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox AppendDataBoundItems="True"
                                                ID="rcbChargeAcct" runat="server"
                                                MarkFirstMatch="True" width="300"
                                                AllowCustomText="false">
                                                <ExpandAnimation Type="None" />
                                                <CollapseAnimation Type="None" />
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Charge Amt</td>
                                    <td class="MyContent">
                                        <telerik:RadNumericTextBox runat="server" ID="tbChargeAmt" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Party Charged</td>
                                    <td class="MyContent" >
                                        <telerik:RadComboBox 
                                            ID="rcbPartyCharged" runat="server"
                                            MarkFirstMatch="True" 
                                            AllowCustomText="false">
                                            <ExpandAnimation Type="None" />
                                            <CollapseAnimation Type="None" />
                                            <Items>
                                                <telerik:RadComboBoxItem Value="B" Text="B" />
                                                <telerik:RadComboBoxItem Value="AC" Text="AC" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Amort Charges</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox
                                            ID="rcbOmortCharge" runat="server"
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
                                <tr>
                                    <td class="MyLable">Charge status</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox 
                                            ID="rcbChargeStatus" runat="server"
                                            MarkFirstMatch="True"
                                            AllowCustomText="false">
                                            <ExpandAnimation Type="None" />
                                            <CollapseAnimation Type="None" />
                                            <Items>
                                                <telerik:RadComboBoxItem Value="" Text="" />
                                                <telerik:RadComboBoxItem Value="CHARGE COLLECTED" Text="CHARGE COLLECTED" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr style="border-top: 1px solid #CCC;">
                                    <td class="MyLable">Tax Code</td>
                                    <td class="MyContent">
                                        <asp:Label ID="lblTaxCode" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Tax Amt</td>
                                    <td class="MyContent">
                                        <asp:Label ID="lblTaxAmt" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="RadPageView2" >
                        <div runat="server" ID="divPAYMENTCHG">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="MyLable">Charge code</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox 
                                            ID="rcbChargeCode2" runat="server"
                                            MarkFirstMatch="True" Enabled="false"
                                            AllowCustomText="false">
                                            <ExpandAnimation Type="None" />
                                            <CollapseAnimation Type="None" />
                                            <Items>
                                                <telerik:RadComboBoxItem Value="ELC.CONFIRM" Text="ELC.CONFIRM" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Charge Ccy</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox
                                            ID="rcbChargeCcy2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rcbChargeCcy2_OnSelectedIndexChanged"
                                            MarkFirstMatch="True"
                                            AllowCustomText="false">  
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Charge Acct</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox AppendDataBoundItems="True"
                                            ID="rcbChargeAcct2" runat="server"
                                            MarkFirstMatch="True" width="300"
                                            AllowCustomText="false">
                                            <ExpandAnimation Type="None" />
                                            <CollapseAnimation Type="None" />
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Charge Amt</td>
                                    <td class="MyContent">
                                        <telerik:RadNumericTextBox runat="server" ID="tbChargeAmt2" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Party Charged</td>
                                    <td class="MyContent" >
                                        <telerik:RadComboBox 
                                            ID="rcbPartyCharged2" runat="server"
                                            MarkFirstMatch="True" 
                                            AllowCustomText="false">
                                            <ExpandAnimation Type="None" />
                                            <CollapseAnimation Type="None" />
                                            <Items>
                                                <telerik:RadComboBoxItem Value="B" Text="B" />
                                                <telerik:RadComboBoxItem Value="AC" Text="AC" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Amort Charges</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox
                                            ID="rcbOmortCharge2" runat="server"
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
                                <tr>
                                    <td class="MyLable">Charge status</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox 
                                            ID="rcbChargeStatus2" runat="server"
                                            MarkFirstMatch="True"
                                            AllowCustomText="false">
                                            <ExpandAnimation Type="None" />
                                            <CollapseAnimation Type="None" />
                                            <Items>
                                                <telerik:RadComboBoxItem Value="" Text="" />
                                                <telerik:RadComboBoxItem Value="CHARGE COLLECTED" Text="CHARGE COLLECTED" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr style="border-top: 1px solid #CCC;">
                                    <td class="MyLable">Tax Code</td>
                                    <td class="MyContent">
                                        <asp:Label ID="lblTaxCode2" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Tax Amt</td>
                                    <td class="MyContent">
                                        <asp:Label ID="lblTaxAmt2" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="RadPageView3" >
                        <div runat="server" ID="divACCPTCHG">
	                        <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="MyLable">Charge code</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox 
                                            ID="rcbChargeCode3" runat="server"
                                            MarkFirstMatch="True" Enabled="false"
                                            AllowCustomText="false">
                                            <ExpandAnimation Type="None" />
                                            <CollapseAnimation Type="None" />
                                            <Items>
                                                <telerik:RadComboBoxItem Value="ELC.OTHER" Text="ELC.OTHER" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Charge Ccy</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox
                                            ID="rcbChargeCcy3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rcbChargeCcy3_OnSelectedIndexChanged"
                                            MarkFirstMatch="True"
                                            AllowCustomText="false">                                     
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Charge Acct</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox AppendDataBoundItems="True"
                                            ID="rcbChargeAcct3" runat="server"
                                            MarkFirstMatch="True" width="300"
                                            AllowCustomText="false">
                                            <ExpandAnimation Type="None" />
                                            <CollapseAnimation Type="None" />
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Charge Amt</td>
                                    <td class="MyContent">
                                        <telerik:RadNumericTextBox runat="server" ID="tbChargeAmt3" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Party Charged</td>
                                    <td class="MyContent" >
                                        <telerik:RadComboBox 
                                            AutoPostBack="True"
                                            ID="rcbPartyCharged3" runat="server"
                                            MarkFirstMatch="True" 
                                            AllowCustomText="false">
                                            <ExpandAnimation Type="None" />
                                            <CollapseAnimation Type="None" />
                                            <Items>
                                                <telerik:RadComboBoxItem Value="B" Text="B" />
                                                <telerik:RadComboBoxItem Value="AC" Text="AC" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Amort Charges</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox
                                            ID="rcbOmortCharge3" runat="server"
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
                                <tr>
                                    <td class="MyLable">Charge status</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox 
                                            ID="rcbChargeStatus3" runat="server"
                                            MarkFirstMatch="True"
                                            AllowCustomText="false">
                                            <ExpandAnimation Type="None" />
                                            <CollapseAnimation Type="None" />
                                            <Items>
                                                <telerik:RadComboBoxItem Value="" Text="" />
                                                <telerik:RadComboBoxItem Value="CHARGE COLLECTED" Text="CHARGE COLLECTED" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr style="border-top: 1px solid #CCC;">
                                    <td class="MyLable">Tax Code</td>
                                    <td class="MyContent">
                                        <asp:Label ID="lblTaxCode3" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Tax Amt</td>
                                    <td class="MyContent">
                                        <asp:Label ID="lblTaxAmt3" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </fieldset>
        </div>
</div>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var tabId = '<%= TabId %>';
        $("#<%=tbLCCode.ClientID%>").keyup(function (event) {
            if (event.keyCode == 13) {
                if ($("#<%=tbLCCode.ClientID %>").val() == "") {
                    alert("Please fill in the LCCode");
                }
                else {
                    window.location.href = "Default.aspx?tabid=" + tabId + "&LCCode=" + $("#<%=tbLCCode.ClientID %>").val();
                }
            }
        });
        $("#<%=txtNarrative.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=txtNarrative.ClientID %>").val($("#<%=txtNarrative.ClientID %>").val() + '\n');
            }
        });
        $("#<%=txtSenderToReceiverInformation.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=txtSenderToReceiverInformation.ClientID %>").val($("#<%=txtSenderToReceiverInformation.ClientID %>").val() + '\n');
            }
        });
    </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"><img src="icons/bank/ajax-loader-16x16.gif" /></telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbIssuingBankType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblIssuingBankMessage" />
                <telerik:AjaxUpdatedControl ControlID="txtIssuingBankNo" />
                <telerik:AjaxUpdatedControl ControlID="txtIssuingBankName" />
                <telerik:AjaxUpdatedControl ControlID="txtIssuingBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtIssuingBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtIssuingBankAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtBeneficiaryNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblBeneficiaryMessage" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryName" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtIssuingBankNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblIssuingBankMessage" />
                <telerik:AjaxUpdatedControl ControlID="txtIssuingBankName" />
                <telerik:AjaxUpdatedControl ControlID="txtIssuingBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtIssuingBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtIssuingBankAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<div style="visibility: hidden;">
    <asp:Button ID="btnReportThuThongBao" runat="server" OnClick="btnReportThuThongBao_Click" Text="PhieuXuatNgoaiBang" />
    <asp:Button ID="btnReportPhieuXuatNgoaiBang" runat="server" OnClick="btnReportPhieuXuatNgoaiBang_Click" Text="PhieuXuatNgoaiBang" />
    <asp:Button ID="btnReportPhieuThu" runat="server" OnClick="btnReportPhieuThu_Click" Text="PhieuXuatNgoaiBang" />
</div>
    