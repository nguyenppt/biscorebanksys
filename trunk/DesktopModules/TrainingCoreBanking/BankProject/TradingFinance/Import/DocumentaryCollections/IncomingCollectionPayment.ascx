<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IncomingCollectionPayment.ascx.cs" Inherits="BankProject.TradingFinance.Import.DocumentaryCollections.IncomingCollectionPayment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />

<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <script type="text/javascript">
        var clickCalledAfterRadconfirm = false;
        var partyCharged = '<%= PartyCharged %>';
        var createMT400 = '<%= CreateMT400 %>';
        
        jQuery(function ($) {
            $('#tabs-demo').dnnTabs();
            $('#Charges').dnnTabs();
            
        });

        function RadToolBar1_OnClientButtonClicking(sender, args) {
            var button = args.get_item();

            if (button.get_commandName() == "print" && !clickCalledAfterRadconfirm) {
                args.set_cancel(true);
                radconfirm("Do you want to download MT202 file?", confirmCallbackFunction_MT202, 340, 150, null, 'Download');
            }
        }

        function confirmCallbackFunction_MT202(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnMT202Report.ClientID %>").click();
            }
            if (createMT400 == 'YES') {
                radconfirm("Do you want to download MT400 file?", confirmCallbackFunction_MT400, 340, 150, null, 'Download');
            } else {
                radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file?", confirmCallbackFunction_PhieuNgoaiBang, 420, 150, null, 'Download');
            }
        }

        function confirmCallbackFunction_MT400(result) {
                clickCalledAfterRadconfirm = false;
                if (result) {
                    $("#<%=btnMT400Report.ClientID %>").click();
                }
                radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file?", confirmCallbackFunction_PhieuNgoaiBang, 420, 150, null, 'Download');
            }
    
                function confirmCallbackFunction_PhieuNgoaiBang(result) {
                clickCalledAfterRadconfirm = false;
                if (result) {
                $("#<%=btnPHIEUNHAPNGOAIBANG.ClientID %>").click();
            }
                radconfirm("Do you want to download PHIEU CHUYEN KHHOAN file?", confirmCallbackFunction_PhieuCK, 420, 150, null, 'Download');
        
            }
    
                function confirmCallbackFunction_VAT(result) {
                clickCalledAfterRadconfirm = false;
                if (result) {
                $("#<%=btnVATReport.ClientID %>").click();
            }
            }
    
                function confirmCallbackFunction_VATB(result) {
                clickCalledAfterRadconfirm = false;
                if (result) {
                $("#<%=btnVAT_B_Report.ClientID %>").click();
            }
            }
    
                function confirmCallbackFunction_PhieuCK(result) {
                clickCalledAfterRadconfirm = false;
                if (result) {
                $("#<%=btnPhieuCK.ClientID %>").click();
            }
                // B/BC ko can in VAT
        //<telerik:RadComboBoxItem Value="Openner" Text="A" />
        //<telerik:RadComboBoxItem Value="Correspondent Charges for the Openner" Text="AC" />
        //<telerik:RadComboBoxItem Value="Beneficiary" Text="B" />
        //<telerik:RadComboBoxItem Value="Correspondent Charges for the Beneficiary" Text="BC" />
                if (partyCharged === 'B') {
                radconfirm("Do you want to download HOA DON VAT file?", confirmCallbackFunction_VATB, 365, 150, null, 'Download');
            }
                //else if (partyCharged === 'AC') {
        //    radconfirm("Do you want to download HOA DON VAT file?", confirmCallbackFunction_VATB, 365, 150, null, 'Download');
        //}
            }
    </script>
</telerik:RadCodeBlock>

<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%"
    OnButtonClick="RadToolBar1_ButtonClick" OnClientButtonClicking="RadToolBar1_OnClientButtonClicking">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btSave" CommandName="save">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btReview" CommandName="review">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Revert" Value="btRevert" CommandName="revert">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="search">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btprint" CommandName="print">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
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

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#Main" id="tabMain">Main</a></li>
        <li><a href="#MT202">MT 202</a></li>
        <li><a href="#MT400">MT 400</a></li>
        <li><a href="#Charges">Charges</a></li>
        <%--<li><a href="#DeliveryAudit">Delivery Audit</a></li>
        <li><a href="#FullView">Full View</a></li>--%>
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
                        <asp:Label ID="lblDrawType" runat="server"  />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Currency</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCurrency" runat="server"  />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Drawing Amount<span class="Required"> (*)</span></td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numDrawingAmount" runat="server" AutoPostBack="True"  
                            OnTextChanged="numDrawingAmount_OnTextChanged"/>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator2"
                            ControlToValidate="numDrawingAmount"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Drawing Amount is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Value Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteValueDate" runat="server" />
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
            <tr>
                <td class="MyLable">Presentor Cus No</td>
                <td class="MyContent">
                    <telerik:RadComboBox Width="355"
                        ID="comboPresentorCusNo" runat="server" AppendDataBoundItems="True"
                        MarkFirstMatch="True"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                    </telerik:RadComboBox>
                </td>
                <td>
                    <asp:Label ID="lblPresentorCusName" runat="server" /></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Country Code</td>
                <td class="MyContent">
                    <telerik:RadComboBox Width="355"
                        ID="comboCountryCode" runat="server" AppendDataBoundItems="True"
                        AutoPostBack="False" 
                        MarkFirstMatch="True"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                    </telerik:RadComboBox>
                </td>
                <td>
                    <asp:Label ID="lblCountryName" runat="server" /></td>
            </tr>
        </table>
        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Payment Instructions</div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">DR from Account</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDRFromAccount" runat="server" Width="355"/>
                    </td>
                    <td>
                        <asp:Label ID="lblDRFromAccountName" runat="server" /></td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display: none;">
                    <td class="MyLable">Exch. Rate</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numExchRate" runat="server" />
                    </td>
                </tr>

                <tr style="display: none">
                    <td class="MyLable">Amt DR Fr Acct Ccy</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAmtDRFrAcctCcy" runat="server" Text="USD" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Amt DR from Acct <span class="Required">(*)</span></td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numAmtDrFromAcct" runat="server" AutoPostBack="True"
                            OnTextChanged="numAmtDrFromAcct_OnTextChanged">
                        </telerik:RadNumericTextBox>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator1"
                            ControlToValidate="numAmtDrFromAcct"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Amt DR from Acct is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
                <tr>
                    <td class="MyLable">Payment Method</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboPaymentMethod" runat="server" AutoPostBack="False"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="N" Text="NOSTRO" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Nostro Acct</td>
                    <td style="width: 150px;" class="MyContent">
                        <telerik:RadComboBox Width="355" 
                            AppendDataBoundItems="True" 
                            AutoPostBack="true"
                            OnSelectedIndexChanged="comboNostroAcct_OnSelectedIndexChanged"
                            ID="comboNostroAcct" runat="server" 
                            MarkFirstMatch="True" Height="150px"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:Label ID="lblNostroAcctName" runat="server" /></td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Total Amount</td>
                    <td class="MyContent">
                        <asp:Label ID="lblTotalPaymentAmount" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Credited Amount</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAmtCredited" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Payment Remarks</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtPaymentRemarks1" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Payment Remarks</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtPaymentRemarks2" runat="server" Width="355" />
                    </td>
                </tr>
            </table>
        </fieldset>

    </div>

    <div id="MT202" class="dnnClear">
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;"></div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">Transaction Reference Number</td>
                    <td class="MyContent">
                        <asp:Label ID="lblTransactionReferenceNumber" runat="server" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">Related Reference<span class="Required"> (*)</span></td>
                    <td  class="MyContent">
                        <telerik:RadTextBox ID="txtRelatedReference" runat="server" Width="355" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">Value Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteValueDate_MT202" runat="server" />
                    </td>
                </tr>
                </table>

                <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">Currency Code/Amount<span class="Required"> (*)</span></td>
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
            </table>

            <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
                <tr>
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
            </table>

            <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
                

                <tr style="display: none">
                    <td style="width: 200px" class="MyLable">Receiver's Correspondent</td>
                    <td class="MyContent">
                        <asp:Label ID="lblReceiverCorrespondentName2" runat="server" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 200px">Intermediary Bank Type</td>
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
                        <td style="width: 200px" class="MyLable">Intermediary Bank No.</td>
                        <td class="MyContent" style="width: 150px">
                            <telerik:RadTextBox ID="txtIntermediaryBank" runat="server" Width="400" 
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
                    <td style="width: 200px" class="MyLable">Intermediary Bank Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIntermediaryBankName" runat="server" Width="400" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px" class="MyLable">Intermediary Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIntermediaryBankAddr1" runat="server" Width="400" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px" class="MyLable">Intermediary Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIntermediaryBankAddr2" runat="server" Width="400" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px" class="MyLable">Intermediary Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIntermediaryBankAddr3" runat="server" Width="400" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">Account With Institution Type</td>
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
                    <td style="width: 200px" class="MyLable">Account With Institution No.</td>
                    <td class="MyContent">
                        <telerik:radtextbox ID="txtAccountWithInstitution" runat="server" Width="400"
                            AutoPostBack="True" OnTextChanged="txtAccountWithInstitution_OnTextChanged"/>
                    </td>
                    <td>
                        <asp:Label ID="lblAccountWithInstitutionError" runat="server" ForeColor="red" />
                    </td>
                </tr>
            </table>
           
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">Account With Institution Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAccountWithInstitutionName" runat="server" Width="400" />
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 200px" class="MyLable">Account With Institution Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAccountWithInstitutionAddr1" runat="server" Width="400" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 200px" class="MyLable">Account With Institution Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAccountWithInstitutionAddr2" runat="server" Width="400" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 200px" class="MyLable">Account With Institution Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAccountWithInstitutionAddr3" runat="server" Width="400" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 200px">Beneficiary Bank Type</td>
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
                        <td style="width: 200px" class="MyLable">Beneficiary Bank No.</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtBeneficiaryBank" runat="server" Width="400" AutoPostBack="True" OnTextChanged="txtBeneficiaryBank_OnTextChanged"/>
                        </td>
                        <td>
                            <asp:Label ID="lblBeneficiaryBankError" runat="server" Text="" ForeColor="red" />
                        </td>
                    </tr>
                </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">Beneficiary Bank Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryBankName" runat="server" Width="400" />
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 200px" class="MyLable">Beneficiary Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryBankAddr1" runat="server" Width="400" />
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 200px" class="MyLable">Beneficiary Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryBankAddr2" runat="server" Width="400" />
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 200px" class="MyLable">Beneficiary Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryBankAddr3" runat="server" Width="400" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 200px" class="MyLable">Sender to Receiver Information</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation" runat="server" Width="400" />
                    </td>
                </tr>
            </table>

        </fieldset>
    </div>

    <div id="MT400" class="dnnClear">
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;"></div>
            </legend>
            
            <div ID="divMT400">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 200px" class="MyLable">Create MT400</td>
                        <td class="MyContent">
                            <telerik:RadComboBox AutoPostBack="True"
                                OnSelectedIndexChanged="comboCreateMT410_OnSelectedIndexChanged"
                                ID="comboCreateMT410" runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                                <Items>
                                    <telerik:RadComboBoxItem Value="YES" Text="YES" />
                                    <telerik:RadComboBoxItem Value="NO" Text="NO" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 200px" class="MyLable">Sending Bank's TRN</td>
                        <td class="MyContent">
                            <asp:Label ID="txtSendingBankTRN" runat="server" />
                        </td>
                    </tr>
                </table>

                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 200px" class="MyLable">Related Reference</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtRelatedReferenceMT400" runat="server" Width="355" />
                        </td>
                    </tr>
                </table>

                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 200px" class="MyLable">Amount Collected</td>
                        <td class="MyContent">
                            <telerik:RadNumericTextBox ID="numAmountCollected" runat="server" Enabled="False" />
                        </td>
                    </tr>
                </table>

                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 200px" class="MyLable">Value Date</td>
                        <td class="MyContent">
                            <telerik:RadDatePicker ID="dteValueDate_MT400" runat="server" />
                        </td>
                    </tr>
                </table>

                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 200px" class="MyLable">Currency Code/Amount</td>
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
                        <td style="width: 200px" class="MyLable">Sender's Correspondent Type</td>
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
                        <td style="width: 200px" class="MyLable">Sender's Correspondent No.</td>
                        <td class="MyContent" style="width: 150px">
                            <telerik:RadTextBox ID="txtSenderCorrespondentNo" runat="server" Width="355" 
                                AutoPostBack="True" 
                                OnTextChanged="txtSenderCorrespondentNo_OnTextChanged" />
                        </td>
                        <td><asp:Label ID="lblSenderCorrespondentNoError" runat="server" ForeColor="red"/></td>
                    </tr>
                </table>

                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr >
                        <td style="width: 200px" class="MyLable">Sender's Correspondent Name</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtSenderCorrespondentName" runat="server" Width="355" />
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 200px" class="MyLable">Sender's Correspondent  Addr.</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtSenderCorrespondentAddress1" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px" class="MyLable">Sender's Correspondent  Addr.</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtSenderCorrespondentAddress2" runat="server" Width="355" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="width: 200px" class="MyLable">Sender's Correspondent  Addr.</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtSenderCorrespondentAddress3" runat="server" Width="355" />
                        </td>
                    </tr>
                </table>

                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 200px" class="MyLable">Receiver's Correspondent Type</td>
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
                        <td style="width: 200px" class="MyLable">Receiver's Correspondent No.</td>
                        <td class="MyContent" style="width: 150px">
                            <telerik:RadTextBox ID="txtReceiverCorrespondentNo" runat="server" Width="355" 
                                AutoPostBack="True" 
                                OnTextChanged="txtReceiverCorrespondentNo_OnTextChanged" />
                        </td>
                        <td><asp:Label ID="lblReceiverCorrespondentError" runat="server" ForeColor="red"/></td>
                    </tr>
                </table>
                
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr >
                        <td style="width: 200px" class="MyLable">Receiver's Correspondent Name</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtReceiverCorrespondentName" runat="server" Width="355" />
                        </td>
                    </tr>
                
                    <tr>
                        <td style="width: 200px" class="MyLable">Receiver's Correspondent Addr.</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtReceiverCorrespondentAddr1" runat="server" Width="355" />
                        </td>
                    </tr>
                
                    <tr>
                        <td style="width: 200px" class="MyLable">Receiver's Correspondent Addr.</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtReceiverCorrespondentAddr2" runat="server" Width="355" />
                        </td>
                    </tr>
                
                    <tr>
                        <td style="width: 200px" class="MyLable">Receiver's Correspondent Addr.</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtReceiverCorrespondentAddr3" runat="server" Width="355" />
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
                    <tr>
                        <td style="width: 200px" class="MyLable">Detail of Charges</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtDetailOfCharges1" runat="server" Width="400" />
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 200px" class="MyLable">Sender to Receiver Information</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtDetailOfCharges2" runat="server" Width="400" />
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
                    <telerik:RadComboBox AutoPostBack="True"
                        OnSelectedIndexChanged="comboWaiveCharges_OnSelectedIndexChanged"
                        ID="comboWaiveCharges" runat="server"
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
        
        <table width="100%" cellpadding="0" cellspacing="0" style="border-bottom: 1px solid #CCC;">
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
                <telerik:RadTab Text="Cable Charge">
                </telerik:RadTab>
                <telerik:RadTab Text="Payment Charge">
                </telerik:RadTab>
                <telerik:RadTab Text="Accept Charge">
                </telerik:RadTab>
                <telerik:RadTab Text="Other Charge">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>

        <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" >
            <telerik:RadPageView runat="server" ID="RadPageView1" >
                <div runat="server" ID="divCABLECHG">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Charge code</td>
                            <td class="MyContent">
                                <telerik:RadComboBox 
                                    ID="tbChargeCode" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                                <span id="Span1"></span>
                            </td>
                        </tr>
                    </table>

                    <table width="100%" cellpadding="0" cellspacing="0" id="table1" runat="server" style="display: none;">
                        <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent">
                                <telerik:RadTextBox ID="txtChargeAcct1" runat="server" />
                            </td>
                        </tr>
                    </table>

                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr style="display: none">
                            <td class="MyLable">Charge Period</td>
                            <td class="MyContent">
                                <asp:TextBox ID="tbChargePeriod" Text="1" runat="server" Width="100" />
                            </td>
                        </tr>

                        <tr>
                            <td class="MyLable">Charge Ccy</td>
                            <td class="MyContent">
                                <telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="rcbChargeCcy" runat="server"
                                    MarkFirstMatch="True"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>

                        <tr style="display: none">
                            <td class="MyLable">Exch. Rate</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="tbExcheRate" Width="200px" Value="1" />
                            </td>
                        </tr>

                        <tr>
                            <td class="MyLable">Charge Amt</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true" 
                                    IncrementSettings-InterceptMouseWheel="true" runat="server" 
                                    ID="tbChargeAmt" AutoPostBack="true"
                                    OnTextChanged="tbChargeAmt_TextChanged" />
                                <telerik:RadNumericTextBox ID="numChargeAmtFCY1" runat="server" Visible="True"/>
                            </td>
                        </tr>
                    </table>

                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Party Charged</td>
                            <td class="MyContent" style="width: 150px;">
                                <telerik:RadComboBox 
                                    AutoPostBack="True"
                                    OnSelectedIndexChanged="rcbPartyCharged_SelectIndexChange"
                                    OnItemDataBound="rcbPartyCharged_ItemDataBound"
                                    ID="rcbPartyCharged" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:Label ID="lblPartyCharged" runat="server" /></td>
                        </tr>
                    </table>

                    <table width="100%" cellpadding="0" cellspacing="0">
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

                        <tr style="display: none;">
                            <td class="MyLable">Amt. In Local CCY</td>
                            <td class="MyContent"></td>
                        </tr >
                        <tr style="display: none;">
                            <td class="MyLable">Amt DR from Acct</td>
                            <td class="MyContent"></td>
                        </tr>

                        <tr style="display: none">
                            <td class="MyLable">Charge Status</td>
                            <td class="MyContent" style="width: 150px;">
                                <telerik:RadComboBox AutoPostBack="true"
                                    OnSelectedIndexChanged="rcbChargeStatus_SelectIndexChange"
                                    ID="rcbChargeStatus" runat="server"
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
                            <td>
                                <asp:Label ID="lblChargeStatus" runat="server" /></td>
                        </tr>
                    </table>
                
                    <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="MyLable">Tax Code</td>
                                <td class="MyContent">
                                    <asp:Label ID="lblTaxCode" runat="server" />
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="MyLable">Tax Ccy</td>
                                <td class="MyContent">
                                    <asp:Label ID="lblTaxCcy" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="MyLable">Tax Amt</td>
                                <td class="MyContent">
                                    <asp:Label ID="lblTaxAmt" runat="server" />
                                </td>
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
                <div runat="server" ID="divPAYMENTCHG">
                    <table width="100%" cellpadding="0" cellspacing="0" id="table2" runat="server">
                        <tr>
                            <td class="MyLable">Charge code</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="tbChargecode2" runat="server" 
                                    MarkFirstMatch="True"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                                <span id="spChargeCode2"></span>
                            </td>
                        </tr>
                    </table>

                    <table width="100%" cellpadding="0" cellspacing="0" id="table3" runat="server" style="display: none;">
                        <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent" >
                                <telerik:RadTextBox ID="txtChargeAcct2" runat="server" />
                            </td>
                        </tr>
                    </table>

                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr style="display: none">
                            <td class="MyLable">Charge Period</td>
                            <td class="MyContent">
                                <asp:TextBox ID="tbChargePeriod2" Text="1" runat="server" Width="100" />
                            </td>
                        </tr>

                        <tr>
                            <td class="MyLable">Charge Ccy</td>
                            <td class="MyContent">
                                <telerik:RadComboBox AppendDataBoundItems="True"
                                    ID="rcbChargeCcy2" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>

                        <tr style="display: none">
                            <td class="MyLable">Exch. Rate</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server"
                                    ID="tbExcheRate2" Width="200px" />
                            </td>
                        </tr>

                        <tr>
                            <td class="MyLable">Charge Amt</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true" 
                                    IncrementSettings-InterceptMouseWheel="true" runat="server"
                                    ID="tbChargeAmt2" AutoPostBack="true"
                                    OnTextChanged="tbChargeAmt2_TextChanged" />
                                <telerik:RadNumericTextBox ID="numChargeAmtFCY2" runat="server" Visible="True"/>
                            </td>
                        </tr>
                    </table>

                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Party Charged</td>
                            <td class="MyContent" style="width: 150px;">
                                <telerik:RadComboBox 
                                    AutoPostBack="True"
                                    OnSelectedIndexChanged="rcbPartyCharged2_SelectIndexChange"
                                    OnItemDataBound="rcbPartyCharged_ItemDataBound"
                                    ID="rcbPartyCharged2" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                            <td><asp:Label ID="lblPartyCharged2" runat="server" /></td>
                        </tr>
                    </table>

                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Amort Charges</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="rcbOmortCharges2" runat="server"
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
                        </tr>

                        <tr style="display: none;">
                            <td class="MyLable">Amt DR from Acct</td>
                            <td class="MyContent"></td>
                        </tr>

                        <tr style="display: none">
                            <td class="MyLable">Charge Status</td>
                            <td class="MyContent" style="width: 150px;">
                                <telerik:RadComboBox AutoPostBack="true"
                                    OnSelectedIndexChanged="rcbChargeStatus2_SelectIndexChange"
                                    ID="rcbChargeStatus2" runat="server"
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
                            <td>
                                <asp:Label ID="lblChargeStatus2" runat="server" /></td>
                        </tr>
                    </table>
                
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Tax Code</td>
                            <td class="MyContent">
                                <asp:Label ID="lblTaxCode2" runat="server" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="MyLable">Tax Ccy</td>
                            <td class="MyContent">
                                <asp:Label ID="lblTaxCcy2" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Tax Amt</td>
                            <td class="MyContent">
                                <asp:Label ID="lblTaxAmt2" runat="server" />
                            </td>
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
                <div runat="server" ID="divACCPTCHG">
	                <table width="100%" cellpadding="0" cellspacing="0" id="table4" runat="server">
		                <tr>
			                <td class="MyLable">Charge code</td>
			                <td class="MyContent">
				                <telerik:RadComboBox
					                ID="tbChargecode3" runat="server" 
					                MarkFirstMatch="True"
					                AllowCustomText="false">
					                <ExpandAnimation Type="None" />
					                <CollapseAnimation Type="None" />
				                </telerik:RadComboBox>
			                </td>
		                </tr>
	                </table>

	                <table width="100%" cellpadding="0" cellspacing="0" id="table5" runat="server" style="display: none;">
		                <tr>
			                <td class="MyLable">Charge Acct</td>
			                <td class="MyContent" >
				                <telerik:RadTextBox ID="txtChargeAcct3" runat="server" />
			                </td>
		                </tr>
	                </table>

	                <table width="100%" cellpadding="0" cellspacing="0">
		                <tr style="display: none">
			                <td class="MyLable">Charge Period</td>
			                <td class="MyContent">
				                <asp:TextBox ID="tbChargePeriod3" Text="1" runat="server" Width="100" />
			                </td>
		                </tr>

		                <tr>
			                <td class="MyLable">Charge Ccy</td>
			                <td class="MyContent">
				                <telerik:RadComboBox AppendDataBoundItems="True"
					                ID="rcbChargeCcy3" runat="server"
					                MarkFirstMatch="True"
					                AllowCustomText="false">
					                <ExpandAnimation Type="None" />
					                <CollapseAnimation Type="None" />
				                </telerik:RadComboBox>
			                </td>
		                </tr>

		                <tr style="display: none">
			                <td class="MyLable">Exch. Rate</td>
			                <td class="MyContent">
				                <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server"
					                ID="tbExcheRate3" Width="200px" />
			                </td>
		                </tr>

		                <tr>
			                <td class="MyLable">Charge Amt</td>
			                <td class="MyContent">
				                <telerik:RadNumericTextBox 
					                IncrementSettings-InterceptArrowKeys="true" 
					                IncrementSettings-InterceptMouseWheel="true" 
					                runat="server"
					                ID="tbChargeAmt3" 
					                AutoPostBack="true"
					                OnTextChanged="tbChargeAmt3_TextChanged" />
			                </td>
		                </tr>
	                </table>

	                <table width="100%" cellpadding="0" cellspacing="0">
		                <tr>
			                <td class="MyLable">Party Charged</td>
			                <td class="MyContent" style="width: 150px;">
				                <telerik:RadComboBox
					                AutoPostBack="True"
					                OnSelectedIndexChanged="rcbPartyCharged3_SelectIndexChange"
					                OnItemDataBound="rcbPartyCharged_ItemDataBound"
					                ID="rcbPartyCharged3" runat="server"
					                MarkFirstMatch="True"
					                AllowCustomText="false">
					                <ExpandAnimation Type="None" />
					                <CollapseAnimation Type="None" />
				                </telerik:RadComboBox>
			                </td>
			                <td><asp:Label ID="lblPartyCharged3" runat="server" /></td>
		                </tr>
	                </table>

	                <table width="100%" cellpadding="0" cellspacing="0">
		                <tr>
			                <td class="MyLable">Amort Charges</td>
			                <td class="MyContent">
				                <telerik:RadComboBox
					                ID="rcbOmortCharges3" runat="server"
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
		                </tr>

		                <tr style="display: none;">
			                <td class="MyLable">Amt DR from Acct</td>
			                <td class="MyContent"></td>
		                </tr>

		                <tr style="display: none">
			                <td class="MyLable">Charge Status</td>
			                <td class="MyContent" style="width: 150px;">
				                <telerik:RadComboBox 
					                ID="rcbChargeStatus3" runat="server"
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
			                <td>
				                <asp:Label ID="lblChargeStatus3" runat="server" /></td>
		                </tr>
	                </table>
           
	                <table width="100%" cellpadding="0" cellspacing="0">
		                <tr>
			                <td class="MyLable">Tax Code</td>
			                <td class="MyContent">
				                <asp:Label ID="lblTaxCode3" runat="server" />
			                </td>
		                </tr>
		                <tr style="display: none">
			                <td class="MyLable">Tax Ccy</td>
			                <td class="MyContent">
				                <asp:Label ID="lblTaxCcy3" runat="server" />
			                </td>
		                </tr>
		                <tr>
			                <td class="MyLable">Tax Amt</td>
			                <td class="MyContent">
				                <asp:Label ID="lblTaxAmt3" runat="server" />
			                </td>
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
                <div runat="server" ID="divOTHERCHG">
	                <table width="100%" cellpadding="0" cellspacing="0" id="table6" runat="server">
		                <tr>
			                <td class="MyLable">Charge code</td>
			                <td class="MyContent">
				                <telerik:RadComboBox 
					                ID="tbChargecode4" runat="server" 
					                MarkFirstMatch="True"
					                AllowCustomText="false">
					                <ExpandAnimation Type="None" />
					                <CollapseAnimation Type="None" />
				                </telerik:RadComboBox>
			                </td>
		                </tr>
	                </table>

	                <table width="100%" cellpadding="0" cellspacing="0" id="table7" runat="server" style="display: none;">
		                <tr>
			                <td class="MyLable">Charge Acct</td>
			                <td class="MyContent" >
				                <telerik:RadTextBox ID="txtChargeAcct4" runat="server" />
			                </td>
		                </tr>
	                </table>

	                <table width="100%" cellpadding="0" cellspacing="0">
		                <tr style="display: none">
			                <td class="MyLable">Charge Period</td>
			                <td class="MyContent">
				                <asp:TextBox ID="tbChargePeriod4" Text="1" runat="server" />
			                </td>
		                </tr>

		                <tr>
			                <td class="MyLable">Charge Ccy</td>
			                <td class="MyContent">
				                <telerik:RadComboBox AppendDataBoundItems="True"
					                ID="rcbChargeCcy4" runat="server"
					                MarkFirstMatch="True"
					                AllowCustomText="false">
					                <ExpandAnimation Type="None" />
					                <CollapseAnimation Type="None" />
				                </telerik:RadComboBox>
			                </td>
		                </tr>

		                <tr style="display: none">
			                <td class="MyLable">Exch. Rate</td>
			                <td class="MyContent">
				                <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server"
					                ID="tbExcheRate4" Width="200px" />
			                </td>
		                </tr>

		                <tr>
			                <td class="MyLable">Charge Amt</td>
			                <td class="MyContent">
				                <telerik:RadNumericTextBox 
					                IncrementSettings-InterceptArrowKeys="true" 
					                IncrementSettings-InterceptMouseWheel="true" 
					                runat="server"
					                ID="tbChargeAmt4" 
					                AutoPostBack="true"
					                OnTextChanged="tbChargeAmt4_TextChanged" />
			                </td>
		                </tr>
	                </table>

	                <table width="100%" cellpadding="0" cellspacing="0">
		                <tr>
			                <td class="MyLable">Party Charged</td>
			                <td class="MyContent" style="width: 150px;">
				                <telerik:RadComboBox
					                AutoPostBack="False"
					                OnSelectedIndexChanged="rcbPartyCharged4_SelectIndexChange"
					                OnItemDataBound="rcbPartyCharged_ItemDataBound"
					                ID="rcbPartyCharged4" runat="server"
					                MarkFirstMatch="True"
					                AllowCustomText="false">
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
					                ID="rcbOmortCharges4" runat="server"
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
		                </tr>

		                <tr style="display: none;">
			                <td class="MyLable">Amt DR from Acct</td>
			                <td class="MyContent"></td>
		                </tr>

		                <tr style="display: none">
			                <td class="MyLable">Charge Status</td>
			                <td class="MyContent" style="width: 150px;">
				                <telerik:RadComboBox
					                ID="rcbChargeStatus4" runat="server"
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
			                <td>
				                <asp:Label ID="lblChargeStatus4" runat="server" /></td>
		                </tr>
	                </table>

	                <table width="100%" cellpadding="0" cellspacing="0">
		                <tr>
			                <td class="MyLable">Tax Code</td>
			                <td class="MyContent">
				                <asp:Label ID="lblTaxCode4" runat="server" />
			                </td>
		                </tr>
		                <tr style="display: none">
			                <td class="MyLable">Tax Ccy</td>
			                <td class="MyContent">
				                <asp:Label ID="lblTaxCcy4" runat="server" />
			                </td>
		                </tr>
		                <tr>
			                <td class="MyLable">Tax Amt</td>
			                <td class="MyContent">
				                <asp:Label ID="lblTaxAmt4" runat="server" />
			                </td>
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
        </telerik:RadMultiPage>
                
    </div>

    <%--<div id="DeliveryAudit" class="dnnClear"></div>

    <div id="FullView" class="dnnClear"></div>--%>
</div>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="numExchRate">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="numAmtDrFromAcct" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <%--<telerik:AjaxSetting AjaxControlID="comboAccountWithInstitution">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAccountWithInstitutionName" />
                <telerik:AjaxUpdatedControl ControlID="comboReceiverCorrespondentMT400" />
            </UpdatedControls>
        </telerik:AjaxSetting>--%>
        
        <telerik:AjaxSetting AjaxControlID="comboAccountWithInstitutionType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithInstitution" />
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithInstitutionName" />
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithInstitutionAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithInstitutionAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithInstitutionAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboNostroAcct">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblSenderCorrespondent1" />
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
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        
        <telerik:AjaxSetting AjaxControlID="txtBeneficiaryBank">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblBeneficiaryBankError" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryBankName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="txtAccountWithInstitution">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAccountWithInstitutionError" />
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithInstitutionName" />
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
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="numDrawingAmount">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="numAmtDrFromAcct" />
                <telerik:AjaxUpdatedControl ControlID="numAmountCollected" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="numAmtDrFromAcct">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="numDrawingAmount" />
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="tbChargeAmt">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="tbChargeAmt2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt2" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        
        <telerik:AjaxSetting AjaxControlID="tbChargeAmt3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt3" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="tbChargeAmt4">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt4" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode4" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="rcbPartyCharged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="rcbPartyCharged2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt2" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="rcbPartyCharged3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt3" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="rcbPartyCharged4">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="numAmount_MT400" />
                <telerik:AjaxUpdatedControl ControlID="numAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt4" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode4" />
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
                <telerik:AjaxUpdatedControl ControlID="txtSenderCorrespondentName" />
                <telerik:AjaxUpdatedControl ControlID="lblSenderCorrespondentNoError" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        $("#<%=txtCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btSearch.ClientID %>").click();
            }
        });
    </script>
</telerik:RadCodeBlock>

<div style="visibility: hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" /></div>


<div style="visibility: hidden;">
    <asp:Button ID="btnMT202Report" runat="server" OnClick="btnMT202Report_Click" Text="Search" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnMT400Report" runat="server" OnClick="btnMT400Report_Click" Text="Search" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnVATReport" runat="server" OnClick="btnVATReport_Click" Text="Search" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnPHIEUNHAPNGOAIBANG" runat="server" OnClick="btnPHIEUNHAPNGOAIBANGReport_Click" Text="Search" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnVAT_B_Report" runat="server" OnClick="btnVAT_B_Report_Click" Text="Search" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnPhieuCK" runat="server" OnClick="btnPhieuCK_Report_Click" Text="Search" /></div>

