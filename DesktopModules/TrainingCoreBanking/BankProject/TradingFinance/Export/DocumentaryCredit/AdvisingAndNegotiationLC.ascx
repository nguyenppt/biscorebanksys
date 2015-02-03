<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisingAndNegotiationLC.ascx.cs" Inherits="BankProject.TradingFinance.Export.DocumentaryCredit.AdvisingAndNegotiationLC" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"></telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />
<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <style>
        .NoDisplay {display:none;
        }
    </style>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
    function RadToolBar1_OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Print%>") {
            //args.set_cancel(false);
            radconfirm("Do you want to download 'Mau Bia Hs Lc' file ?", confirmCallbackFunction_MauBiaHsLc, 420, 150, null, 'Download');
        }
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Search%>" ||
            button.get_commandName() == "<%=BankProject.Controls.Commands.Preview%>") {
            var url = 'Default.aspx?tabid=278&refid=<%= TabId %>';
            if (button.get_commandName() == "<%=BankProject.Controls.Commands.Preview%>") {
                url += '&lst=4appr';
            }
            window.location.href = url;
        }        
    }
    function confirmCallbackFunction_MauBiaHsLc(result) {
        if (result) {
            $("#<%=btnReportMauBiaHsLc.ClientID %>").click();
        }
        radconfirm("Do you want to download 'Mau Thong Bao Lc' file ?", confirmCallbackFunction_MauThongBaoLc, 420, 150, null, 'Download');
    }
    function confirmCallbackFunction_MauThongBaoLc(result) {
        if (result) {
            $("#<%=btnReportMauThongBaoLc.ClientID %>").click();
        }
        if ($find("<%=rcbWaiveCharges.ClientID%>").get_value() == "NO")
            radconfirm("Do you want to download 'VAT' file ?", confirmCallbackFunction_VAT, 420, 150, null, 'Download');
    }
    function confirmCallbackFunction_VAT(result) {
        if (result) {
            $("#<%=btnVAT.ClientID %>").click();
        }
    }
</script>
</telerik:RadCodeBlock>
<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
         OnClientButtonClicking="RadToolBar1_OnClientButtonClicking" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommit" CommandName="commit" Enabled="true">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/hold.png"
            ToolTip="Hold Data" Value="btHoldData" CommandName="hold" Enabled="false">
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
            ToolTip="Search" Value="btSearch" CommandName="search" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print" Value="btPrint" CommandName="print" postback="false" Enabled="false">
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
                        </asp:RequiredFieldValidator>&nbsp;<asp:Label ID="lblLCCodeMessage" runat="server" ForeColor="red" /></td>
    </tr>
</table>
    <div class="dnnForm" id="tabs-demo">
        <ul class="dnnAdminTabNav" style="margin-bottom:10px;">
            <li><a href="#Main">Main</a></li>
            <li><a href="#Charges">Charges</a></li>
        </ul>
        <div id="Main" class="dnnClear">
            <fieldset>
                <legend>
                    <span style="font-weight: bold; text-transform: uppercase;">main</span>
                </legend>
                <div runat="server" id="divConfirmLC" style="display:none;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width:200px" class="MyLable">Generate Delivery?</td>
                            <td class="MyContent">  
                                <telerik:RadComboBox Width="200"
                                    ID="rcbGenerateDelivery" runat="server"
                                    MarkFirstMatch="True"
                                    AllowCustomText="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="Yes" Text="YES" />
                                        <telerik:RadComboBoxItem Value="No" Text="NO" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Date</td>
                            <td class="MyContent">  
                                <telerik:RadDateInput ID="txtDateConfirm" Width="200px" runat="server" readonly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Confirmation Instr.</td>
                            <td class="MyContent">
                                <telerik:RadComboBox ID="rcbConfirmInstr" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false" width="200px">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                    </table>
                </div>
                <div runat="server" id="divCancelLC" style="display:none;">
                    <table style="width:100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Cancel Date</td>
                            <td class="MyContent">
                                <telerik:RadDatePicker ID="txtCancelDate" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <td class="MyLable">Contingent Expiry Date</td>
                            <td class="MyContent">
                                <telerik:RadDatePicker ID="txtContingentExpiryDate" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <td class="MyLable">Cancel Remark</td>
                            <td class="MyContent">
                                <telerik:RadTextBox ID="txtCancelRemark" runat="server" Width="355" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                    </table>
                </div>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 250px" class="MyLable">20. Documentary Credit Number <span class="Required">(*)</span>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator20"
                                ControlToValidate="txtImportLCNo"
                                ValidationGroup="Commit"
                                InitialValue=""
                                ErrorMessage="[Documentary Credit Number] is required" ForeColor="Red">
                            </asp:RequiredFieldValidator><asp:TextBox ID="txtCustomerName" runat="server" CssClass="NoDisplay"></asp:TextBox>
                        </td>
                        <td class="MyContent"><telerik:RadTextBox ID="txtImportLCNo" runat="server" Width="195" AutoPostBack="false" OnTextChanged="txtImportLCNo_TextChanged" />
                        </td>
                        <td><asp:Label ID="lblImportLCNoMessage" runat="server" Text="" ForeColor="Red"></asp:Label></td>
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
                        <td style="width: 250px" class="MyLable">Advising Bank Type</td>
                        <td class="MyContent">
                            <telerik:RadComboBox Width="195"
                                AutoPostBack="True"
                                OnSelectedIndexChanged="rcbAdvisingBankType_OnSelectedIndexChanged"
                                ID="rcbAdvisingBankType" runat="server"
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
                        <td style="width: 250px" class="MyLable">Advising Bank No.</td>
                        <td class="MyContent"><telerik:RadTextBox ID="txtAdvisingBankNo" runat="server" Width="195" AutoPostBack="true" OnTextChanged="txtAdvisingBankNo_TextChanged" />
                        </td>
                        <td><asp:Label ID="lblAdvisingBankMessage" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable" style="width: 250px">Advising Bank Name</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtAdvisingBankName" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">Advising Bank Addr.</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtAdvisingBankAddr1" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtAdvisingBankAddr2" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtAdvisingBankAddr3" runat="server" Width="355" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 250px;" class="MyLable">27.1 Sequence of Total</td>
                        <td class="MyContent">
                            <asp:Label ID="tbBaquenceOfTotal" runat="server" Text="Populated by System" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">40A. Form of Documentary Credit</td>
                        <td class="MyContent">
                            <telerik:RadComboBox Width="195"
                                ID="rcbFormOfDocumentaryCredit" runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="IRREVOCABLE" Text="IRREVOCABLE" />
                                    <telerik:RadComboBoxItem Value="REVOCABLE" Text="REVOCABLE" />
                                    <telerik:RadComboBoxItem Value="IRREVOCABLE TRANSFERABLE" Text="IRREVOCABLE TRANSFERABLE" />
                                    <telerik:RadComboBoxItem Value="REVOCABLE TRANSFERABLE" Text="REVOCABLE TRANSFERABLE" />
                                    <telerik:RadComboBoxItem Value="IRREVOCABLE STANDBY" Text="IRREVOCABLE STANDBY" />
                                    <telerik:RadComboBoxItem Value="REVOCABLE STANDBY" Text="REVOCABLE STANDBY" />
                                    <telerik:RadComboBoxItem Value="IRREVOC TRANS STANDBY" Text="IRREVOC TRANS STANDBY" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">31C. Date of Issue <span class="Required">(*)</span>
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
                </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 250px" class="MyLable">31D. Date and Place of Expiry <span class="Required">(*)</span>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator5"
                                ControlToValidate="txtDateOfExpiry"
                                ValidationGroup="Commit"
                                InitialValue=""
                                ErrorMessage="[Date of Expiry] is required" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator6"
                                ControlToValidate="txtPlaceOfExpiry"
                                ValidationGroup="Commit"
                                InitialValue=""
                                ErrorMessage="[Place of Expiry] is required" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 200px" class="MyContent">
                            <telerik:RadDatePicker ID="txtDateOfExpiry" Width="200" runat="server" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtPlaceOfExpiry" runat="server" Width="155" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 250px" class="MyLable">40E. Applicable Rule <span class="Required">(*)</span>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator7"
                                ControlToValidate="rcbApplicableRule"
                                ValidationGroup="Commit"
                                InitialValue=""
                                ErrorMessage="[Applicable Rule] is required" ForeColor="Red">
                            </asp:RequiredFieldValidator></td>
                        <td class="MyContent">
                            <telerik:RadComboBox Width="195"
                                ID="rcbApplicableRule"
                                runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="EUCP LATEST VERSION" Text="EUCP LATEST VERSION" />
                                    <telerik:RadComboBoxItem Value="EUCPURR LATEST VERSION" Text="EUCPURR LATEST VERSION" />
                                    <telerik:RadComboBoxItem Value="ISP LATEST VERSION " Text="ISP LATEST VERSION " />
                                    <telerik:RadComboBoxItem Value="OTHR" Text="OTHR" />
                                    <telerik:RadComboBoxItem Value="UCP LATEST VERSION" Text="UCP LATEST VERSION" />
                                    <telerik:RadComboBoxItem Value="UCPURR LATEST VERSION" Text="UCPURR LATEST VERSION" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 250px" class="MyLable">50.1 Applicant Name <span class="Required">(*)</span>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator3"
                                ControlToValidate="txtApplicantName"
                                ValidationGroup="Commit"
                                InitialValue=""
                                ErrorMessage="[Applicant Name] is required" ForeColor="Red">
                            </asp:RequiredFieldValidator></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtApplicantName" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">50.2 Applicant Address</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="tbApplicantAddr1" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="tbApplicantAddr2" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="tbApplicantAddr3" runat="server" Width="355" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable" style="width: 250px">59.1 Beneficiary Number <span class="Required">(*)</span>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator2"
                                ControlToValidate="rcbBeneficiaryNumber"
                                ValidationGroup="Commit"
                                InitialValue=""
                                ErrorMessage="[Beneficiary Number] is required" ForeColor="Red">
                            </asp:RequiredFieldValidator></td>
                        <td class="MyContent"><telerik:RadComboBox
                                AppendDataBoundItems="True"
                                ID="rcbBeneficiaryNumber" runat="server"
                                MarkFirstMatch="True"
                                Width="355"
                                Height="150"
                                AllowCustomText="false"
                            AutoPostBack="True"
                                OnSelectedIndexChanged="rcbBeneficiaryNumber_OnSelectedIndexChanged">
                                <ExpandAnimation Type="None" />
                                <CollapseAnimation Type="None" />
                            </telerik:RadComboBox>
                        </td>
                        <td><asp:Label ID="lblBeneficiaryMessage" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable" style="width: 250px">59.2 Beneficiary Name</td>
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
                        <td style="width: 250px" class="MyLable">32B. Currency Code, Amount <span class="Required">(*)</span>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator10"
                                ControlToValidate="rcbCurrency"
                                ValidationGroup="Commit"
                                InitialValue=""
                                ErrorMessage="[Currency Code] is required" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator11"
                                ControlToValidate="txtAmount"
                                ValidationGroup="Commit"
                                InitialValue="0"
                                ErrorMessage="[Amount] is required" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <telerik:RadComboBox Width="195"
                                AppendDataBoundItems="True"
                                ID="rcbCurrency"
                                runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                            </telerik:RadComboBox>
                        </td>
                        <td><telerik:RadNumericTextBox ID="txtAmount" runat="server" Value="0" Width="157" /></td>
                        <td>
                            <asp:Label ID="lblPaymentAmount" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 250px" class="MyLable">39A. Percentage Credit Amount Tolerance <span class="Required">(*)</span>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator9"
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
                        <td style="width: 250px" class="MyLable">39B. Maximum Credit Amount</td>
                        <td class="MyContent">
                            <telerik:RadComboBox Width="195"
                                ID="rcbMaximumCreditAmount" runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="NOT EXCEEDING" Text="NOT EXCEEDING" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 250px" class="MyLable">41D.1 Available With Type</td>
                        <td class="MyContent">
                            <telerik:RadComboBox Width="195"
                                AutoPostBack="True"
                                OnSelectedIndexChanged="rcbAvailableWithType_OnSelectedIndexChanged"
                                ID="rcbAvailableWithType" runat="server"
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
                        <td style="width: 250px" class="MyLable">41D.2 Available With No.</td>
                        <td class="MyContent"><telerik:RadTextBox ID="txtAvailableWithNo" runat="server" Width="195" AutoPostBack="true" OnTextChanged="txtAvailableWithNo_TextChanged" />
                        </td>
                        <td><asp:Label ID="lblAvailableWithMessage" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable" style="width: 250px">41D.3 Available With Name</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="tbAvailableWithName" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">41D.4 Available With Addr.</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="tbAvailableWithAddr1" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="tbAvailableWithAddr2" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="tbAvailableWithAddr3" runat="server" Width="355" />
                        </td>
                    </tr>
                </table>            
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 250px" class="MyLable">41D.7 By</td>
                        <td class="MyContent">
                            <telerik:RadComboBox Width="195"
                                ID="rcbAvailableWithBy" runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="BY ACCEPTANCE" Text="BY ACCEPTANCE" />
                                    <telerik:RadComboBoxItem Value="BY DEF PAYMENT" Text="BY DEF PAYMENT" />
                                    <telerik:RadComboBoxItem Value="BY MIXED PYMT" Text="BY MIXED PYMT" />
                                    <telerik:RadComboBoxItem Value="BY NEGOTIATION" Text="BY NEGOTIATION" />
                                    <telerik:RadComboBoxItem Value="BY PAYMENT" Text="BY PAYMENT" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">42C.1 Drafts At</td>
                        <td class="MyContent">
                            <telerik:RadTextBox runat="server" ID="txtDraftsAt1" Width="355" />
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox runat="server" ID="txtDraftsAt2" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">42D. Tenor <span class="Required">(*)</span>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator8"
                                ControlToValidate="rcbTenor"
                                ValidationGroup="Commit"
                                InitialValue=""
                                ErrorMessage="[42D. Tenor] is required" ForeColor="Red">
                            </asp:RequiredFieldValidator></td>
                        <td class="MyContent">
                            <telerik:RadComboBox Width="195"
                                ID="rcbTenor" runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="SIGHT" Text="SIGHT" />
                                    <telerik:RadComboBoxItem Value="USANCE" Text="USANCE" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 250px" class="MyLable">42M. Mixed Payment Details</td>
                        <td class="MyContent">
                            <telerik:RadTextBox runat="server" ID="txtMixedPaymentDetails1" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox runat="server" ID="txtMixedPaymentDetails2" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox runat="server" ID="txtMixedPaymentDetails3" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox runat="server" ID="txtMixedPaymentDetails4" Width="355" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 250px" class="MyLable">42P. Deferred Payment Details</td>
                        <td class="MyContent">
                            <telerik:RadTextBox runat="server" ID="txtDeferredPaymentDetails1" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox runat="server" ID="txtDeferredPaymentDetails2" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox runat="server" ID="txtDeferredPaymentDetails3" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox runat="server" ID="txtDeferredPaymentDetails4" Width="355" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 250px" class="MyLable">43P. Partial Shipment <span class="Required">(*)</span>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator18"
                                ControlToValidate="rcbPartialShipment"
                                ValidationGroup="Commit"
                                InitialValue=""
                                ErrorMessage="[43P. Partial Shipment] is required" ForeColor="Red">
                            </asp:RequiredFieldValidator></td>
                        <td class="MyContent">
                            <telerik:RadComboBox Width="195"
                                ID="rcbPartialShipment" runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="Allowed" Text="Allowed" />
                                    <telerik:RadComboBoxItem Value="Not Allowed" Text="Not Allowed" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">43T. Transhipment <span class="Required">(*)</span>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator12"
                                ControlToValidate="rcbTranshipment"
                                ValidationGroup="Commit"
                                InitialValue=""
                                ErrorMessage="[Transhipment] is required" ForeColor="Red">
                            </asp:RequiredFieldValidator></td>
                        <td class="MyContent">
                            <telerik:RadComboBox Width="195"
                                ID="rcbTranshipment" runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="Allowed" Text="Allowed" />
                                    <telerik:RadComboBoxItem Value="Not Allowed" Text="Not Allowed" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width:250px;" class="MyLable">44A. Place of taking in charge...</td>
                        <td class="MyContent">
                            <telerik:RadTextBox Width="355" ID="txtPlaceOfTakingInCharge" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">44E. Port of loading... <span class="Required">(*)</span>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator14"
                                ControlToValidate="txtPortOfLoading"
                                ValidationGroup="Commit"
                                InitialValue=""
                                ErrorMessage="[Port of loading] is required" ForeColor="Red">
                            </asp:RequiredFieldValidator></td>
                        <td class="MyContent">
                            <telerik:RadTextBox Width="355" ID="txtPortOfLoading" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">44F. Port of Discharge... <span class="Required">(*)</span>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator13"
                                ControlToValidate="txtPortOfDischarge"
                                ValidationGroup="Commit"
                                InitialValue=""
                                ErrorMessage="[Port of Discharge] is required" ForeColor="Red">
                            </asp:RequiredFieldValidator></td>
                        <td class="MyContent">
                            <telerik:RadTextBox Width="355" ID="txtPortOfDischarge" runat="server" />
                        </td>
                    </tr>                
                    <tr>
                        <td class="MyLable">44B. Place of final destination</td>
                        <td class="MyContent">
                            <telerik:RadTextBox Width="355" ID="txtPlaceOfFinalDestination" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">44C. Latest Date of Shipment <span class="Required">(*)</span>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator15"
                                ControlToValidate="txtLatesDateOfShipment"
                                ValidationGroup="Commit"
                                InitialValue=""
                                ErrorMessage="[Latest Date of Shipment] is required" ForeColor="Red">
                            </asp:RequiredFieldValidator></td>
                        <td class="MyContent">
                            <telerik:RadDatePicker runat="server" ID="txtLatesDateOfShipment" Width="200" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 250px" class="MyLable">44D. Shipment Period</td>
                        <td class="MyContent">
                            <telerik:RadTextBox runat="server" ID="txtShipmentPeriod1" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox runat="server" ID="txtShipmentPeriod2" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox runat="server" ID="txtShipmentPeriod3" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox runat="server" ID="txtShipmentPeriod4" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox runat="server" ID="txtShipmentPeriod5" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox runat="server" ID="txtShipmentPeriod6" Width="355" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 250px; vertical-align: top;" class="MyLable">45A. Description of Goods/Services <span class="Required">(*)</span>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator16"
                                ControlToValidate="txtDescriptionOfGoodsServices"
                                ValidationGroup="Commit"
                                InitialValue=""
                                ErrorMessage="[Description of Goods/Services] is required" ForeColor="Red">
                            </asp:RequiredFieldValidator></td>
                        <td class="MyContent" style="vertical-align: top;">
                            <telerik:RadTextBox ID="txtDescriptionOfGoodsServices" runat="server" TextMode="MultiLine" Height="100" Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">45A.1 Commodity <span class="Required">(*)</span>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator19"
                                ControlToValidate="rcbCommodity"
                                ValidationGroup="Commit"
                                InitialValue=""
                                ErrorMessage="Commodity is Required" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="MyContent">
                            <telerik:RadComboBox
                                ID="rcbCommodity" runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                                <ExpandAnimation Type="None" />
                                <CollapseAnimation Type="None" />
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;" class="MyLable">46A. Docs required</td>
                        <td class="MyContent" style="vertical-align: top;">
                            <telerik:RadTextBox ID="txtDocsRequired" runat="server" TextMode="MultiLine" Height="100" Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;" class="MyLable">47A. Additional Conditions</td>
                        <td class="MyContent" style="vertical-align: top;">
                            <telerik:RadTextBox ID="txtAdditionalConditions" runat="server" TextMode="MultiLine" Height="100" Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;" class="MyLable">71B. Charges </td>
                        <td class="MyContent" style="vertical-align: top;">
                            <telerik:RadTextBox ID="txtCharges" runat="server" TextMode="MultiLine" Height="100" Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;" class="MyLable">48. Period for Presentation <span class="Required">(*)</span>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator17"
                                ControlToValidate="txtPeriodForPresentation"
                                ValidationGroup="Commit"
                                InitialValue=""
                                ErrorMessage="[Period for Presentation] is required" ForeColor="Red">
                            </asp:RequiredFieldValidator></td>
                        <td class="MyContent" style="vertical-align: top;">
                            <telerik:RadTextBox ID="txtPeriodForPresentation" runat="server" TextMode="MultiLine" Height="100" Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 250px;" class="MyLable">49. Confirmation Instructions </td>
                        <td class="MyContent">
                            <telerik:RadComboBox Width="195"
                                ID="rcbConfimationInstructions" runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="WITHOUT" Text="WITHOUT" />
                                    <telerik:RadComboBoxItem Value="CONFIRM" Text="CONFIRM" />
                                    <telerik:RadComboBoxItem Value="MAY ADD" Text="MAY ADD" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable" style="width: 250px;">53.1 Reimb. Bank Type</td>
                        <td class="MyContent">
                            <telerik:RadComboBox Width="195"
                                AutoPostBack="True"
                                OnSelectedIndexChanged="rcbReimbBankType_OnSelectedIndexChanged"
                                ID="rcbReimbBankType" runat="server"
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
                        <td style="width: 250px" class="MyLable">53.2 Reimb. Bank No</td>
                        <td class="MyContent"><telerik:RadTextBox ID="txtReimbBankNo" runat="server" Width="195" AutoPostBack="true" OnTextChanged="txtReimbBankNo_TextChanged" />
                        </td>
                        <td><asp:Label ID="lblReimbBankMessage" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 250px;" class="MyLable">53.3 Reimb. Bank Name</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="tbReimbBankName" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">53.4 Reimb. Bank Addr.</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="tbReimbBankAddr1" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="tbReimbBankAddr2" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="tbReimbBankAddr3" runat="server" Width="355" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 250px; vertical-align: top;" class="MyLable">78. Instr to//Payg/Accptg/Negotg Bank</td>
                        <td class="MyContent" style="vertical-align: top;">
                            <telerik:RadTextBox ID="txtInstrToPaygAccptgNegotgBank" runat="server" TextMode="MultiLine" Height="100" Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable" style="width: 250px;">57.1 Advise Through No</td>
                        <td class="MyContent"><telerik:RadTextBox ID="txtAdviseThroughBankNo" runat="server" Width="195" AutoPostBack="true" OnTextChanged="txtAdviseThroughBankNo_TextChanged" />
                        </td>
                        <td><asp:Label ID="lblAdviseThroughBankMessage" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable" style="width: 250px;">57.2 Advise Through Name</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtAdviseThroughBankName" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">57.3 Advise Through Addr.</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtAdviseThroughBankAddr1" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtAdviseThroughBankAddr2" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtAdviseThroughBankAddr3" runat="server" Width="355" />
                        </td>
                    </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 250px; vertical-align: top;" class="MyLable">72. Sender to Receiver Information</td>
                        <td class="MyContent" style="vertical-align: top;">
                            <telerik:RadTextBox ID="txtSenderToReceiverInformation" runat="server" TextMode="MultiLine" Height="100" Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
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
                                    <td class="MyContent"><telerik:RadTextBox ID="txtChargeCode1" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Charge Ccy</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox
                                            ID="rcbChargeCcy1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rcbChargeCcy1_OnSelectedIndexChanged"
                                            MarkFirstMatch="True"
                                            AllowCustomText="false">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Charge Acct</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox AppendDataBoundItems="True"
                                                ID="rcbChargeAcct1" runat="server"
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
                                        <telerik:RadNumericTextBox runat="server" ID="tbChargeAmt1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Party Charged</td>
                                    <td class="MyContent" >
                                        <telerik:RadComboBox 
                                            ID="rcbPartyCharged1" runat="server"
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
                                            ID="rcbAmortCharge1" runat="server"
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
                                            ID="rcbChargeStatus1" runat="server"
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
                                        <asp:Label ID="lblTaxCode1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Tax Amt</td>
                                    <td class="MyContent">
                                        <asp:Label ID="lblTaxAmt1" runat="server" />
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
                                    <td class="MyContent"><telerik:RadTextBox ID="txtChargeCode2" runat="server" /></td>
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
                                            ID="rcbAmortCharge2" runat="server"
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
                                    <td class="MyContent"><telerik:RadTextBox ID="txtChargeCode3" runat="server" /></td>
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
                                            ID="rcbAmortCharge3" runat="server"
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
        $("#<%=tbLCCode.ClientID%>").keyup(function (event) {
            if (event.keyCode == 13) {
                if ($("#<%=tbLCCode.ClientID %>").val() == "") {
                    alert("Please fill in the LCCode");
                }
                else {
                    window.location.href = "Default.aspx?tabid=<%= TabId %>&Code=" + $("#<%=tbLCCode.ClientID %>").val();
                }
            }
        });
        $("#<%=txtDescriptionOfGoodsServices.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=txtDescriptionOfGoodsServices.ClientID %>").val($("#<%=txtDescriptionOfGoodsServices.ClientID %>").val() + '\n');
            }
        });
        $("#<%=txtDocsRequired.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=txtDocsRequired.ClientID %>").val($("#<%=txtDocsRequired.ClientID %>").val() + '\n');
            }
        });
        $("#<%=txtAdditionalConditions.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=txtAdditionalConditions.ClientID %>").val($("#<%=txtAdditionalConditions.ClientID %>").val() + '\n');
            }
        });
        $("#<%=txtCharges.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=txtCharges.ClientID %>").val($("#<%=txtCharges.ClientID %>").val() + '\n');
            }
        });
        $("#<%=txtPeriodForPresentation.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=txtPeriodForPresentation.ClientID %>").val($("#<%=txtPeriodForPresentation.ClientID %>").val() + '\n');
            }
        });
        $("#<%=txtInstrToPaygAccptgNegotgBank.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=txtInstrToPaygAccptgNegotgBank.ClientID %>").val($("#<%=txtInstrToPaygAccptgNegotgBank.ClientID %>").val() + '\n');
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
        <telerik:AjaxSetting AjaxControlID="rcbAdvisingBankType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAdvisingBankMessage" />
                <telerik:AjaxUpdatedControl ControlID="txtAdvisingBankNo" />
                <telerik:AjaxUpdatedControl ControlID="txtAdvisingBankName" />
                <telerik:AjaxUpdatedControl ControlID="txtAdvisingBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtAdvisingBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtAdvisingBankAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
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
        <telerik:AjaxSetting AjaxControlID="rcbAvailableWithType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAvailableWithMessage" />
                <telerik:AjaxUpdatedControl ControlID="txtAvailableWithNo" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithName" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr1" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr2" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbReimbBankType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblReimbBankMessage" />
                <telerik:AjaxUpdatedControl ControlID="txtReimbBankNo" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankName" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbBeneficiaryNumber">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblBeneficiaryMessage" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryName" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtAvailableWithNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAvailableWithMessage" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithName" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr1" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr2" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtReimbBankNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblReimbBankMessage" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankName" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtAdviseThroughBankNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAdviseThroughBankMessage" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankName" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtAdvisingBankNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAdvisingBankMessage" />
                <telerik:AjaxUpdatedControl ControlID="txtAdvisingBankName" />
                <telerik:AjaxUpdatedControl ControlID="txtAdvisingBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtAdvisingBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtAdvisingBankAddr3" />
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
        <telerik:AjaxSetting AjaxControlID="txtImportLCNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblImportLCNoMessage" />
                <telerik:AjaxUpdatedControl ControlID="txtCustomerName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbWaiveCharges">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadTabStrip3" />
                <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbChargeCcy1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbChargeAcct1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbChargeCcy2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbChargeAcct2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbChargeCcy3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbChargeAcct3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<div style="visibility: hidden;">
    <asp:Button ID="btnReportMauBiaHsLc" runat="server" OnClick="btnReportMauBiaHsLc_Click" />
    <asp:Button ID="btnReportMauThongBaoLc" runat="server" OnClick="btnReportMauThongBaoLc_Click" />
    <asp:Button ID="btnVAT" runat="server" OnClick="btnVAT_Click" />
</div>
    