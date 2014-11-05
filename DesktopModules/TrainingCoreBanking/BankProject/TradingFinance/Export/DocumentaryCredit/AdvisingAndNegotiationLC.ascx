<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisingAndNegotiationLC.ascx.cs" Inherits="BankProject.TradingFinance.Export.DocumentaryCredit.AdvisingAndNegotiationLC" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"></telerik:RadWindowManager>
<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
<script type="text/javascript">
    var tabId = '<%= TabId %>';
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        var today = (month) + "/" + (day)+ "/" + now.getFullYear();
        $("#<%=DateConfirm.ClientID %>").val(today)
        $(".reEditorModesCell").hide();
        $(".reBottomZone").hide();
        $(".reResizeCell").hide();
        
    });
    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Print%>") {
            args.set_cancel(true);
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
    function confirmCallbackFunction_PhieuThu(result)
    {
        clickCalledAfterRadconfirm = false;
        if (result) {
            $("#<%=btnReportPhieuThu.ClientID %>").click();
        }
    }
    function confirmCallbackFunction_PhieuXuatNgoaiBang(result)
    {
        clickCalledAfterRadconfirm = false;
        if (result)
        {
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
                ToolTip="Print Deal Slip" Value="btPrint" CommandName="print" postback="false">
            </telerik:RadToolBarButton>
        </Items>
    </telerik:RadToolBar>

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width:200px; padding:5px 0 5px 20px;"><asp:TextBox ID="tbEssurLCCode" runat="server" Width="200" />&nbsp;<asp:Label ID="lblError" runat="server" ForeColor="red" /></td>

    </tr>
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:HiddenField ID="HiddenField1" runat="server" Value="0" /><asp:HiddenField ID="txtCustomerID" runat="server" Value="" /><asp:HiddenField ID="txtCustomerName" runat="server" Value="" />
            <%--<asp:TextBox ID="TextBox1" runat="server" Width="200" /><span class="Required"> (*)</span> &nbsp;<asp:Label ID="Label13" runat="server" ForeColor="red" />--%>
        </td>
    </tr>
</table>
    <div class="dnnForm" id="tabs-demo">
        <ul class="dnnAdminTabNav">
            <li><a href="#Main">Main</a></li>
           <!-- <li><a href="#MT710">MT700</a></li>
            <li><a href="#MT730">MT740</a></li>-->
            <li><a href="#Charges">Charges</a></li>
            <!--<li><a href="#DeliveryAudit">Delivery Audit</a></li>
            <li><a href="#FullView">Full View</a></li>
            -->
        </ul>
        <div id="Main" class="dnnClear">
            <div runat="server" id="divAcceptLC">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width:200px" class="MyLable">Generate Delivery?</td>
                    <td class="MyContent">  
                        <telerik:RadComboBox Width="200"
                            ID="RadComboBoxGD" runat="server"
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
                        <telerik:RadDateInput ID="DateConfirm" Width="200px" runat="server" readonly="true" />
                    </td>
                </tr>
                <tr style="display:none">
                    <td class="MyLable">External Reference</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtExternalReference" runat="server" width="200px" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Confirmation Instr.</td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="ComboConfirmInstr" runat="server"
                            MarkFirstMatch="True" 
                            AllowCustomText="false" width="200px">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr style="display:none">
                    <td class="MyLable">Limit Ref.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtLimitRef" runat="server" width="200px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
            </table>
            </div>
            <div runat="server" id="divCancelLC">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Cancel Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteCancelDate" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Contingent Expiry Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteContingentExpiryDate" runat="server" />
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
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display:none">
                    <td style="width:250px" class="MyLable">Receiving Bank</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtRevivingBank700" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable" style="color: #d0d0d0">27.1 Sequence of Total</td>
                    <td class="MyContent">
                        <asp:Label ID="tbBaquenceOfTotal" runat="server" Text="Populated by System" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable">40A. Form of Documentary Credit</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadComboBox Width="200"
                            ID="comboFormOfDocumentaryCredit" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <Items>
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
                    <td>
                        <asp:Label ID="tbFormOfDocumentaryCreditName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable">20. Documentary Credit Number</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDocumentaryCreditNumber" runat="server" />
                    </td>

                    <td></td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable">31C. Date of Issue</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteDateOfIssue" Width="200" runat="server" />
                    </td>
                    <td></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">31D. Date and Place of Expiry</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadDatePicker ID="dteMT700DateAndPlaceOfExpiry" Width="200" runat="server" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="tbPlaceOfExpiry" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable">40E. Applicable Rule</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadComboBox
                            ID="comboAvailableRule"
                            runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="comboAvailableRule_OnSelectedIndexChanged">
                            <Items>
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
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display: none;">
                    <td style="width: 250px" class="MyLable">50.1 Applicant Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            AutoPostBack="True"
                            OnSelectedIndexChanged="rcbApplicantBankType700_OnSelectedIndexChanged"
                            ID="rcbApplicantBankType700" runat="server"
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
                    <td style="width: 250px" class="MyLable">50.1 Applicant</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbApplicantNo700" runat="server" Width="355" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display:none">
                    <td style="width: 250px" class="MyLable">50.2 Applicant Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbApplicantName700" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width:250px">50.3 Applicant Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbApplicantAddr700_1" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbApplicantAddr700_2" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbApplicantAddr700_3" runat="server" Width="355" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0" style="display: none">
                <tr>
                    <td class="MyLable" style="width: 250px">59.1 Beneficiary Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboBeneficiaryType700" runat="server"
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
                <tr style="display:none">
                    <td class="MyLable" style="width: 250px">59.1 Beneficiary No.</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadTextBox ID="txtBeneficiaryNo700" runat="server" Width="355"
                            AutoPostBack="True" OnTextChanged="txtBeneficiaryNo700_OnTextChanged" />
                    </td>
                    <td>
                        <asp:Label ID="lblBeneficiaryNo700Error" runat="server" Text="" ForeColor="red" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px">59.2 Beneficiary Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryName700" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">59.3 Beneficiary Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryAddr700_1" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryAddr700_2" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryAddr700_3" runat="server" Width="355" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">32B. Currency Code, Amount<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator10"
                            ControlToValidate="comboCurrency700"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[MT700] Currency Code is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>

                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator11"
                            ControlToValidate="numAmount700"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[MT700] Amount is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadComboBox Width="200"
                            AppendDataBoundItems="True"
                            ID="comboCurrency700"
                            runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="numAmount700" runat="server" /></td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable">39A. Percentage Credit Amount Tolerance</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadNumericTextBox Width="200" ID="numPercentCreditAmount1" runat="server" Type="Percent" MaxValue="100" />
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="numPercentCreditAmount2" runat="server" Type="Percent" MaxValue="100" /></td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable">39B. Maximum Credit Amount</td>
                    <td class="MyContent">
                        <telerik:RadComboBox Width="200"
                            ID="comboMaximumCreditAmount700" runat="server"
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
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display:none">
                    <td style="width: 250px" class="MyLable">39C.1 Additional Amounts Covered</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtAdditionalAmountsCovered700_1" Width="355" />
                    </td>
                </tr>
                <tr style="display:none">
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtAdditionalAmountsCovered700_2" Width="355" />
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">41D.1 Available With Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
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
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">41D.2 Available With No.</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboAvailableWithNo"
                            runat="server"
                            AppendDataBoundItems="true"
                            AutoPostBack="True"
                            OnItemDataBound="SwiftCode_ItemDataBound"
                            OnSelectedIndexChanged="comboAvailableWithNo_OnSelectedIndexChanged"
                            MarkFirstMatch="True"
                            Width="355"
                            Height="150"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">41D.3 Available With Name</td>
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
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">41D.7 By</td>
                    <td class="MyContent">
                        <telerik:RadComboBox Width="200"
                            ID="comboAvailableWithBy" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="comboAvailableWithBy_OnSelectedIndexChanged"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="BY ACCEPTANCE" Text="BY ACCEPTANCE" />
                                <telerik:RadComboBoxItem Value="BY DEF PAYMENT" Text="BY DEF PAYMENT" />
                                <telerik:RadComboBoxItem Value="BY MIXED PYMT" Text="BY MIXED PYMT" />
                                <telerik:RadComboBoxItem Value="BY NEGOTIATION" Text="BY NEGOTIATION" />
                                <telerik:RadComboBoxItem Value="BY PAYMENT" Text="BY PAYMENT" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td hidden="hidden">
                        <telerik:RadTextBox ID="tbAvailableWithByName" runat="server" /></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">42C.1 Drafts At</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDraftsAt700_1" Width="355"
                            ClientEvents-OnValueChanged="txtDraftsAt700_1_OnClientSelectedIndexChanged" />
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDraftsAt700_2" Width="355" ClientEvents-OnValueChanged="txtDraftsAt700_2_OnClientSelectedIndexChanged" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display:none">
                    <td style="width: 250px" class="MyLable">42A.1 Drawee Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            AutoPostBack="True"
                            OnSelectedIndexChanged="comboDraweeCusType_OnSelectedIndexChanged"
                            ID="comboDraweeCusType" runat="server"
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
                <tr style="display:none">
                    <td class="MyLable">42A.2 Drawee No</td>
                    <td class="MyContent">
                        <%--<telerik:RadTextBox ID="txtDraweeCusNo" runat="server" Width="355" Text="VVTBVNVX" />--%>
                        <telerik:RadComboBox
                            ID="comboDraweeCusNo700"
                            runat="server"
                            AppendDataBoundItems="true"
                            
                            AutoPostBack="True"
                            OnItemDataBound="SwiftCode_ItemDataBound"
                            OnSelectedIndexChanged="comboDraweeCusNo700_OnSelectedIndexChanged"
                            MarkFirstMatch="True"
                            Width="355"
                            Height="150"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr style="display:none">
                    <td style="width: 250px" class="MyLable">42A.3 Drawee Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeCusName" runat="server" Width="355" />
                    </td>
                </tr>
                <tr style="display:none">
                    <td class="MyLable">42A.4 Drawee Addr.</td>
                    <td class="MyContent">
                         <telerik:RadTextBox ID="txtDraweeAddr1" runat="server" Width="355" />
                    </td>
                </tr>
                <tr style="display:none">
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeAddr2" runat="server" Width="355" />
                    </td>
                </tr>
                <tr style="display:none">
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeAddr3" runat="server" Width="355" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">42M. Mixed Payment Details</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtMixedPaymentDetails700_1" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtMixedPaymentDetails700_2" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtMixedPaymentDetails700_3" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtMixedPaymentDetails700_4" Width="355" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">42P. Deferred Payment Details</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDeferredPaymentDetails700_1" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDeferredPaymentDetails700_2" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDeferredPaymentDetails700_3" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDeferredPaymentDetails700_4" Width="355" />
                    </td>
                </tr>

            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">43P. Patial Shipment</td>
                    <td class="MyContent">
                        <telerik:RadComboBox Width="200"
                            ID="rcbPatialShipment" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="Allowed" Text="Y" />
                                <telerik:RadComboBoxItem Value="Not Allowed" Text="N" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">43T. Transhipment</td>
                    <td class="MyContent">
                        <telerik:RadComboBox Width="200"
                            ID="rcbTranshipment" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="Allowed" Text="Y" />
                                <telerik:RadComboBoxItem Value="Not Allowed" Text="N" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">44A. Place of taking in charge...</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="tbPlaceoftakingincharge" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">44F. Port of Discharge...</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="tbPortofDischarge" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">44E. Port of loading...</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="tbPortofloading" runat="server" />
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">44B. Place of final destination</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="tbPlaceoffinalindistination" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">44C. Latest Date of Shipment</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker runat="server" ID="tbLatesDateofShipment" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">44D. Shipment Period</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod700_1" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod700_2" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod700_3" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod700_4" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod700_5" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod700_6" Width="355" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px; vertical-align: top;" class="MyLable">45A. Description of Goods/Services</td>
                    <td class="MyContent" style="vertical-align: top;">
                        <telerik:RadEditor runat="server" ID="txtEdittor_DescrpofGoods" Height="200" ToolsFile="DesktopModules/TrainingCoreBanking/BankProject/TradingFinance/BasicTools.xml" />
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0" hidden="hidden">
                <tr>
                    <td style="width: 250px" class="MyLable">46A. Docs Required</td>
                    <td class="MyContent">
                        <telerik:RadComboBox Width="200"
                            ID="rcbDocsRequired" runat="server" AutoPostBack="True"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="AIRB" Text="AIRB" />
                                <telerik:RadComboBoxItem Value="ANAL" Text="ANAL" />
                                <telerik:RadComboBoxItem Value="AWB" Text="AWB" />
                                <telerik:RadComboBoxItem Value="AWB1" Text="AWB1" />
                                <telerik:RadComboBoxItem Value="AWB2" Text="AWB2" />
                                <telerik:RadComboBoxItem Value="AWB3" Text="AWB3" />
                                <telerik:RadComboBoxItem Value="BEN" Text="BEN" />
                                <telerik:RadComboBoxItem Value="BENOP" Text="BENOP" />
                                <telerik:RadComboBoxItem Value="BL" Text="BL" />
                                <telerik:RadComboBoxItem Value="C.O" Text="C.O" />
                                <telerik:RadComboBoxItem Value="COMIN" Text="COMIN" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px; vertical-align: top;" class="MyLable">46A. Docs required</td>
                    <td class="MyContent" style="vertical-align: top;">
                        <telerik:RadEditor runat="server" ID="txtEdittor_OrderDocs700" Height="150"
                            ToolsFile="DesktopModules/TrainingCoreBanking/BankProject/TradingFinance/BasicTools.xml"/>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px; vertical-align: top;" class="MyLable">47A. Additional Conditions</td>
                    <td class="MyContent" style="vertical-align: top;">
                        <telerik:RadEditor runat="server" ID="txtEdittor_AdditionalConditions700" Height="230"
                            ToolsFile="DesktopModules/TrainingCoreBanking/BankProject/TradingFinance/BasicTools.xml" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px; vertical-align: top;" class="MyLable">71B. Charges </td>
                    <td class="MyContent" style="vertical-align: top;">
                        <telerik:RadEditor runat="server" ID="txtEdittor_Charges700" Height="200"  
                            ToolsFile="DesktopModules/TrainingCoreBanking/BankProject/TradingFinance/BasicTools.xml" />
                        <%--<telerik:RadTextBox Width="700" TextMode="MultiLine" Height="75"
                            ID="tbCharges" runat="server" Text="ALL BANKING CHARGES OUTSIDE VIETNAM 
PLUS ISSUING BANK'S HANDLING FEE 
ARE FOR ACCOUNT OF BENEFICIARY " />--%>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px; vertical-align: top;" class="MyLable">48. Period for Presentation</td>
                    <td class="MyContent" style="vertical-align: top;">
                        <telerik:RadEditor runat="server" ID="txtEdittor_PeriodforPresentation700" Height="75"  
                            ToolsFile="DesktopModules/TrainingCoreBanking/BankProject/TradingFinance/BasicTools.xml" />

                        <%--<telerik:RadTextBox Width="700" TextMode="MultiLine" Height="75"
                            ID="tbPeriodforPresentation" runat="server" Text="NOT EARLIER THAN 21 DAYS AFTER SHIPMENT DATE BUT WITHIN THE VALIDITY OF THIS L/C. " />
                    </td>--%>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px; vertical-align: top;" class="MyLable">49. Confirmation Instructions </td>
                    <td class="MyContent" style="vertical-align: top;">
                        <telerik:RadComboBox Width="200"
                            ID="rcbConfimationInstructions" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="WITHOUT" Text="WITHOUT" />
                                <telerik:RadComboBoxItem Value="CONFIRM" Text="CONFIRM" />
                                <telerik:RadComboBoxItem Value="MAY ADD" Text="MAY ADD" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px;">53.1 Reimb. Bank Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboReimbBankType700" runat="server"
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
                    <td class="MyLable" style="width: 250px;">53.2 Reimb. Bank No</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="rcbReimbBankNo700"
                            runat="server"
                            AppendDataBoundItems="true"
                            OnItemDataBound="SwiftCode_ItemDataBound"
                            MarkFirstMatch="True"
                            Width="355"
                            Height="150"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;">53.3 Reimb. Bank Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbReimbBankName700" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;">53.4 Reimb. Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbReimbBankAddr700_1" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbReimbBankAddr700_2" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbReimbBankAddr700_3" runat="server" Width="355" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px; vertical-align: top;" class="MyLable">78. Instr to//Payg/Accptg/Negotg Bank</td>
                    <td class="MyContent" style="vertical-align: top;">
                        <telerik:RadEditor runat="server" ID="txtEdittor_NegotgBank700" Height="150"  
ToolsFile="DesktopModules/TrainingCoreBanking/BankProject/TradingFinance/BasicTools.xml" />

                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
                <tr>
                    <td class="MyLable" style="width: 250px;">57.1 Advise Through Bank Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboAdviseThroughBankType700" runat="server"
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
                    <td class="MyLable" style="width: 250px;">57.1 Advise Through No</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboAdviseThroughBankNo700"
                            runat="server"
                            AppendDataBoundItems="true"
                            AutoPostBack="False"
                            OnItemDataBound="SwiftCode_ItemDataBound"
                            MarkFirstMatch="True"
                            Width="355"
                            Height="150"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;">57.2 Advise Through Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdviseThroughBankName700" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;">57.3 Advise Through Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdviseThroughBankAddr700_1" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdviseThroughBankAddr700_2" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdviseThroughBankAddr700_3" runat="server" Width="355" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px; vertical-align: top;" class="MyLable">72. Sender to Receiver Information</td>
                    <td class="MyContent" style="vertical-align: top;">
                        <telerik:RadEditor runat="server" ID="txtEdittor_SendertoReceiverInfomation700" Height="75"  
ToolsFile="DesktopModules/TrainingCoreBanking/BankProject/TradingFinance/BasicTools.xml" />

                       <%-- <asp:TextBox Width="700" TextMode="MultiLine" Height="75"
                            ID="tbSendertoReceiverInfomation" runat="server">PLEASE ACKNOWLEDGE YOUR RECEIPT OF THIS L/C BY MT730.</asp:TextBox>--%>
                    </td>
                </tr>
            </table>
        </div>
        <div id="Charges" class="dnnClear">
            <fieldset>
                <legend>
                    <div style="font-weight: bold; text-transform: uppercase;">Charge Details</div>
                </legend>
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
                    <telerik:RadTab Text="Receive Charge">
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
                                <tr>
                                    <td class="MyLable">Charge Currency</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox
                                            ID="rcbChargeCcy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rcbChargeCcy_OnSelectedIndexChanged"
                                            MarkFirstMatch="True" Width="150"
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
                                    <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true" 
                                        IncrementSettings-InterceptMouseWheel="true" runat="server" 
                                        ID="tbChargeAmt" AutoPostBack="true"
                                        OnTextChanged="tbChargeAmt_TextChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td class="MyLable">Party Charged</td>
                                <td class="MyContent" >
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
                                    <asp:Label ID="lblPartyCharged" runat="server" Visible="false" />
                                </td>
                                <td>
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
                                <td class="MyLable">Amt. In Local CCY</td>
                                <td class="MyContent"></td>
                            </tr>
                            <tr>
                                <td class="MyLable">Amt DR from Acct</td>
                                <td class="MyContent"></td>
                            </tr>
                            <tr>
                                <td class="MyLable">Charge Status</td>
                                <td class="MyContent">
                                    <telerik:RadComboBox AutoPostBack="true"
                                        ID="rcbChargeStatus" runat="server"
                                        MarkFirstMatch="True" Width="150"
                                        AllowCustomText="false">
                                        <ExpandAnimation Type="None" />
                                        <CollapseAnimation Type="None" />
                                        <Items>
                                            <telerik:RadComboBoxItem Value="CHARGE COLECTED" Text="CHARGE COLECTED" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td style="display: none;">
                                    <asp:Label ID="lblChargeStatus" runat="server" Text="CHARGE COLECTED"/></td>
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
                            <tr>
                                <td class="MyLable">Tax in LCCY Amt</td>
                                <td class="MyContent"></td>
                            </tr>
                            <tr>
                                <td class="MyLable">Tax Date</td>
                                <td class="MyContent"></td>
                            </tr>
                            </table>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="RadPageView2" >
                        <div runat="server" ID="divPAYMENTCHG">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="MyLable">Charge code</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox 
                                            ID="tbChargeCode2" runat="server"
                                            MarkFirstMatch="True" 
                                            AllowCustomText="false">
                                            <ExpandAnimation Type="None" />
                                            <CollapseAnimation Type="None" />
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Charge Currency</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox
                                            ID="rcbChargeCcy2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rcbChargeCcy2_OnSelectedIndexChanged"
                                            MarkFirstMatch="True" Width="150"
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
                                        <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true" 
                                            IncrementSettings-InterceptMouseWheel="true" runat="server" 
                                            ID="tbChargeAmt2" AutoPostBack="true"
                                            OnTextChanged="tbChargeAmt2_TextChanged" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Party Charged</td>
                                    <td class="MyContent" >
                                        <telerik:RadComboBox 
                                            AutoPostBack="True"
                                            OnSelectedIndexChanged="rcbPartyCharged2_SelectIndexChange"
                                            OnItemDataBound="rcbPartyCharged2_ItemDataBound"
                                            ID="rcbPartyCharged2" runat="server"
                                            MarkFirstMatch="True" 
                                            AllowCustomText="false">
                                            <ExpandAnimation Type="None" />
                                            <CollapseAnimation Type="None" />
                                        </telerik:RadComboBox>
                                        <asp:Label ID="lblPartyCharged2" runat="server" Visible="false" />
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
                                    <td class="MyLable">Amt. In Local CCY</td>
                                    <td class="MyContent"></td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Amt DR from Acct</td>
                                    <td class="MyContent"></td>
                                </tr>
                        
                                <tr>
                                    <td class="MyLable">Charge Status</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox AutoPostBack="true"
                                            ID="rcbChargeStatus2" runat="server"
                                            MarkFirstMatch="True" Width="150"
                                            AllowCustomText="false">
                                            <ExpandAnimation Type="None" />
                                            <CollapseAnimation Type="None" />
                                            <Items>
                                                <telerik:RadComboBoxItem Value="CHARGE COLECTED" Text="CHARGE COLECTED" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="display: none;">
                                        <asp:Label ID="lblChargeStatus2" runat="server" Text="CHARGE COLECTED"/></td>
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
                                <tr>
                                    <td class="MyLable">Tax in LCCY Amt</td>
                                    <td class="MyContent"></td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Tax Date</td>
                                    <td class="MyContent"></td>
                                </tr>    
                            </table>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="RadPageView3" >
                        <div runat="server" ID="divACCPTCHG">
	                        <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="MyLable">Charge code</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox 
                                            ID="tbChargeCode3" runat="server"
                                            MarkFirstMatch="True" 
                                            AllowCustomText="false">
                                            <ExpandAnimation Type="None" />
                                            <CollapseAnimation Type="None" />
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Charge Currency</td>
                                    <td class="MyContent">
                                        <telerik:RadComboBox
                                            ID="rcbChargeCcy3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rcbChargeCcy3_OnSelectedIndexChanged"
                                            MarkFirstMatch="True" Width="150"
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
                                        <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true" 
                                            IncrementSettings-InterceptMouseWheel="true" runat="server" 
                                            ID="tbChargeAmt3" AutoPostBack="true"
                                            OnTextChanged="tbChargeAmt3_TextChanged" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="MyLable">Party Charged</td>
                                    <td class="MyContent" >
                                        <telerik:RadComboBox 
                                            AutoPostBack="True"
                                            OnSelectedIndexChanged="rcbPartyCharged3_SelectIndexChange"
                                            OnItemDataBound="rcbPartyCharged3_ItemDataBound"
                                            ID="rcbPartyCharged3" runat="server"
                                            MarkFirstMatch="True" 
                                            AllowCustomText="false">
                                            <ExpandAnimation Type="None" />
                                            <CollapseAnimation Type="None" />
                                        </telerik:RadComboBox>
                                        <asp:Label ID="lblPartyCharged3" runat="server" Visible="false" />
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
                                <td class="MyLable">Amt. In Local CCY</td>
                                <td class="MyContent"></td>
                            </tr>
                            <tr>
                                <td class="MyLable">Amt DR from Acct</td>
                                <td class="MyContent"></td>
                            </tr>
                        
                        <tr>
                                <td class="MyLable">Charge Status</td>
                                <td class="MyContent">
                                    <telerik:RadComboBox AutoPostBack="true"
                                        ID="rcbChargeStatus3" runat="server"
                                        MarkFirstMatch="True" Width="150"
                                        AllowCustomText="false">
                                        <ExpandAnimation Type="None" />
                                        <CollapseAnimation Type="None" />
                                        <Items>
                                            <telerik:RadComboBoxItem Value="CHARGE COLECTED" Text="CHARGE COLECTED" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td style="display: none;">
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
                            <tr>
                                <td class="MyLable">Tax in LCCY Amt</td>
                                <td class="MyContent"></td>
                            </tr>
                            <tr>
                                <td class="MyLable">Tax Date</td>
                                <td class="MyContent"></td>
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
        $("#<%=tbEssurLCCode.ClientID%>").keyup(function (event) {
            if (event.keyCode == 13) {
                if ($("#<%=tbEssurLCCode.ClientID %>").val() == "") {
                    alert("Please fill in the Essur LCCode");
                }
                else {
                    window.location.href = "Default.aspx?tabid=" + tabId + "&LCCode=" + $("#<%=tbEssurLCCode.ClientID %>").val();
                }
            }
        });
        function txtDraftsAt700_1_OnClientSelectedIndexChanged(sender, eventArgs) {

        }
        function txtDraftsAt700_2_OnClientSelectedIndexChanged(sender, eventArgs) {

        }
    </script>
</telerik:RadCodeBlock>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbChargeCcy">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbChargeAcct" />
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
        
        <telerik:AjaxSetting AjaxControlID="rcbChargeAcct">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbChargeAcct" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="rcbChargeAcct2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbChargeAcct2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="rcbChargeAcct3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbChargeAcct3" />
            </UpdatedControls>
        </telerik:AjaxSetting>


        <telerik:AjaxSetting AjaxControlID="rcbPartyCharged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbPartyCharged" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbPartyCharged2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbPartyCharged2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbPartyCharged3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbPartyCharged3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="tbChargeAmt">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbChargeAmt" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="tbChargeAmt2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbChargeAmt2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tbChargeAmt3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbChargeAmt3" />
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
        <telerik:AjaxSetting AjaxControlID="comboAvailableWithNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbAvailableWithType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="comboAvailableWithNo" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithName" />   
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<div style="visibility: hidden;">
    <asp:Button ID="btnReportThuThongBao" runat="server" OnClick="btnReportThuThongBao_Click" Text="PhieuXuatNgoaiBang" />
    <asp:Button ID="btnReportPhieuXuatNgoaiBang" runat="server" OnClick="btnReportPhieuXuatNgoaiBang_Click" Text="PhieuXuatNgoaiBang" />
    <asp:Button ID="btnReportPhieuThu" runat="server" OnClick="btnReportPhieuThu_Click" Text="PhieuXuatNgoaiBang" />
</div>
    