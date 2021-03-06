﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentProcessing.ascx.cs" Inherits="BankProject.TradingFinance.Export.DocumentaryCredit.DocumentProcessing" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"></telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />
<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
<style>
    .NoDisplay {
        display:none;
    }
    .addDocs, .removeDocs {
        cursor:hand; cursor:pointer;
    }
</style>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
    function RadToolBar1_OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Print%>") {
            showCoverLeter();
        }
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Search%>" ||
            button.get_commandName() == "<%=BankProject.Controls.Commands.Preview%>") {
            var url = '<%=EditUrl("list")%>&refid=<%= TabId %>';
            if (button.get_commandName() == "<%=BankProject.Controls.Commands.Preview%>") {
                url += '&lst=4appr';
            }
            window.location = url;
        }
    }
    function confirmCallbackFunction_VAT(result) {
        if (result) {
            $("#<%=btnVAT.ClientID %>").click();
        }
    }
    function confirmCallbackFunction_XuatNgoaiBang(result) {
        if (result) {
            $("#<%=btnXuatNgoaiBang.ClientID %>").click();
        }
        if ($find("<%=rcbWaiveCharges.ClientID%>").get_value() == "NO"
             && (parseInt($("#<%=tbChargeAmt1.ClientID %>").val()) > 0
                || parseInt($("#<%=tbChargeAmt2.ClientID %>").val()) > 0
                || parseInt($("#<%=tbChargeAmt3.ClientID %>").val()) > 0))
            radconfirm("Do you want to download 'VAT' file ?", confirmCallbackFunction_VAT, 420, 150, null, 'Download');
    }

    function confirmCallbackFunction_NhapNgoaiBang(result) {
        if (result) {
            $("#<%=btnNhapNgoaiBang.ClientID %>").click();
        }
        if ($find("<%=rcbWaiveCharges.ClientID%>").get_value() == "NO"
             && (parseInt($("#<%=tbChargeAmt1.ClientID %>").val()) > 0
                || parseInt($("#<%=tbChargeAmt2.ClientID %>").val()) > 0
                || parseInt($("#<%=tbChargeAmt3.ClientID %>").val()) > 0))
            radconfirm("Do you want to download 'VAT' file ?", confirmCallbackFunction_VAT, 420, 150, null, 'Download');
    }

    function showCoverLeter(){
        radconfirm("Do you want to download Cover letter?", confirmCallbackFunction_Cover, 420, 150, null, 'Download');
    }
    function confirmCallbackFunction_Cover(result) {
        if (result) {
            $("#<%=btnCover.ClientID %>").click();
            showPhieuNhap_Xuat();
        }else
        {
            showPhieuNhap_Xuat();;
        }
    }

    function showPhieuNhap_Xuat() {
       
        if(<%= TabId %> == 239)
        {
            radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file?", confirmCallbackFunction_NhapNgoaiBang, 420, 150, null, 'Download');
        }else if (<%= TabId %> == 241){
            radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file?", confirmCallbackFunction_XuatNgoaiBang, 420, 150, null, 'Download');
        }
        else if ($find("<%=txtNewAmount.ClientID%>").get_value() > 0 
                && $find("<%=txtOriginalAmount.ClientID%>").get_value() > 0 
                && $find("<%=txtNewAmount.ClientID%>").get_value() >= $find("<%=txtOriginalAmount.ClientID%>").get_value()) {
            radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file?", confirmCallbackFunction_NhapNgoaiBang, 420, 150, null, 'Download');
        } else if ($find("<%=txtOriginalAmount.ClientID%>").get_value() > 0 
                && $find("<%=txtNewAmount.ClientID%>").get_value() < $find("<%=txtOriginalAmount.ClientID%>").get_value()) {
            radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file?", confirmCallbackFunction_XuatNgoaiBang, 420, 150, null, 'Download');
        }

    }
</script>
</telerik:RadCodeBlock>
<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
         OnClientButtonClicking="RadToolBar1_OnClientButtonClicking" OnButtonClick="RadToolBar1_ButtonClick">
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
            ToolTip="Search" Value="btSearch" CommandName="search" Enabled="true" postback="false">
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
            <li><a href="#Parties">Parties</a></li>
            <li><a href="#Details">Details</a></li>
            <li><a href="#OtherInformation">Other Information</a></li>
            <li><a  id="tabCharge"  href="#Charges">Charges</a></li>
        </ul>
        <div id="Parties" class="dnnClear">
            <fieldset>
                <legend>
                    <span style="font-weight: bold; text-transform: uppercase;">Parties</span>
                </legend>
                <table cellpadding="0" cellspacing="0">
                    
                    <tr>
                        <td class="MyLable">Accept Date</td>
                        <td class="MyContent">
                            <telerik:RadDatePicker ID="txtAcceptDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">Accept Remarks</td>
                        <td class="MyContent">
                            <telerik:Radtextbox runat="server" ID="txtAcceptRemarks" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable" style="width: 170px">1.LC reference no </td>
                        <td class="MyContent">
                            <asp:Label ID="lblLCReferenceNo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable" style="width: 170px">59.1 Beneficiary Number </td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtBeneficiaryNumber" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable" style="width: 170px">59.2 Beneficiary Name </td>
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
                        <td style="width: 170px" class="MyLable">50.1 Applicant Name </td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtApplicantName" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">50.2 Applicant Address</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtApplicantAddr1" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtApplicantAddr2" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtApplicantAddr3" runat="server" Width="355" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 170px" class="MyLable">2.1 Issuing Bank No.</td>
                        <td class="MyContent"><telerik:RadTextBox ID="txtIssuingBankNo" runat="server" Width="195" />
                        </td>
                        <td><asp:Label ID="lblIssuingBankMessage" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable" style="width: 170px">2.2 Issuing Bank Name</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtIssuingBankName" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">2.3 Issuing Bank Addr.</td>
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
                        <td style="width: 170px" class="MyLable">3.1 Nostro Agent Bank No.</td>
                        <td class="MyContent"><telerik:RadTextBox ID="txtNostroAgentBankNo" runat="server" Width="195" AutoPostBack="true" OnTextChanged="txtNostroAgentBankNo_TextChanged" />
                        </td>
                        <td><asp:Label ID="lblNostroAgentBankMessage" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable" style="width: 170px">3.2 Nostro Agent Bank Name</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtNostroAgentBankName" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">3.3 Nostro Agent Bank Addr.</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtNostroAgentBankAddr1" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtNostroAgentBankAddr2" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtNostroAgentBankAddr3" runat="server" Width="355" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable" style="width: 170px">4.1 Receiving Bank Name</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtReceivingBankName" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">4.2 Receiving Bank Addr</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtReceivingBankAddr1" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtReceivingBankAddr2" runat="server" Width="355" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtReceivingBankAddr3" runat="server" Width="355" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div id="Details" class="dnnClear">
            <fieldset>
                <legend>
                    <span style="font-weight: bold; text-transform: uppercase;">Details</span>
                </legend>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable" style="width: 170px">5. Documentary Credit No</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtDocumentaryCreditNo" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">6. Commodity
                        </td>
                        <td class="MyContent"><asp:HiddenField ID="txtCommodity" runat="server" /><telerik:RadTextBox ID="txtCommodityName" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="MyLable">7. Currency</td>
                        <td class="MyContent">
                            <telerik:RadComboBox
                                AutoPostBack="true"
                                ID="rcbCurrency" runat="server"
                                AppendDataBoundItems="True"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                                <ExpandAnimation Type="None" />
                                <CollapseAnimation Type="None" />
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" id="divAmountRegister" runat="server">
                    <tr>
                        <td class="MyLable" style="width: 170px">8. Amount</td>
                        <td class="MyContent">
                            <telerik:RadNumericTextBox ID="txtAmount" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" id="divAmountAmend" runat="server">
                    <tr>
                        <td class="MyLable" style="width: 170px">8.1 Original Amount</td>
                        <td class="MyContent">
                            <telerik:RadNumericTextBox ID="txtOriginalAmount" runat="server" Value="0" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">8.2 New Amount</td>
                        <td class="MyContent">
                            <telerik:RadNumericTextBox ID="txtNewAmount" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable" style="width: 170px">9. Document received date</td>
                        <td class="MyContent">
                            <telerik:RadDatePicker runat="server" ID="txtDocumentReceivedDate" Width="160" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">10. Proccessing date</td>
                        <td class="MyContent">
                            <telerik:RadDatePicker runat="server" ID="txtProccessingDate" Width="160" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">11. Acceptance Date</td>
                        <td class="MyContent">
                            <telerik:RadDatePicker runat="server" ID="txtAcceptanceDate" Width="160" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">12. Maturity Date</td>
                        <td class="MyContent">
                            <telerik:RadDatePicker runat="server" ID="txtMaturityDate" Width="160" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" id="divTenorRegister" runat="server">
                    <tr>
                        <td class="MyLable" style="width: 170px">13. Tenor</td>
                        <td class="MyContent"><telerik:RadTextBox ID="txtTenor" runat="server" /></td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" id="divTenorAmend" runat="server">
                    <tr>
                        <td class="MyLable" style="width: 170px">13.1 Original Tenor</td>
                        <td class="MyContent"><telerik:RadTextBox ID="txtOriginalTenor" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="MyLable">13.2 New Tenor</td>
                        <td class="MyContent"><telerik:RadComboBox
                                ID="rcbNewTenor" runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="SIGHT" Text="SIGHT" />
                                    <telerik:RadComboBoxItem Value="USANCE" Text="USANCE" />
                                </Items>
                            </telerik:RadComboBox></td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable" style="width: 170px">14. Invoice No</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtInvoiceNo" runat="server" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div id="OtherInformation" class="dnnClear">
            <fieldset>
                <legend>
                    <span style="font-weight: bold; text-transform: uppercase;">Other Information</span>
                </legend>
                <table cellpadding="0" cellspacing="0" id="divDocs1" runat="server">
                    <tr>    
                        <td class="MyLable">13.1.1 Docs Code</td>
                        <td class="MyContent"><telerik:RadComboBox
                            ID="rcbDocsCode1" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                        </telerik:RadComboBox></td>
                        <td><div id="divCmdDocs1" runat="server"><a class="addDocs" index="1"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></div></td>
                    </tr>
                    <tr>    
                        <td class="MyLable">13.1.2 No. of Originals</td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadNumericTextBox ID="txtNoOfOriginals1" Runat="server" NumberFormat-DecimalDigits="0" />
                        </td>
                    </tr>
                    <tr>    
                        <td class="MyLable">13.1.3 No. of Copies</td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadNumericTextBox ID="txtNoOfCopies1" Runat="server" NumberFormat-DecimalDigits="0" />
                        </td>
                    </tr>
                    
                </table>
                <table cellpadding="0" cellspacing="0" id="divDocs2" runat="server" style="display:none;">
                    <tr>    
                        <td class="MyLable">13.2.1 Docs Code</td>
                        <td class="MyContent"><telerik:RadComboBox
                            ID="rcbDocsCode2" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                        </telerik:RadComboBox></td>
                        <td><div id="divCmdDocs2" runat="server"><a class="removeDocs" index="2"><img src="Icons/Sigma/Delete_16X16_Standard.png" /></a><a class="addDocs" index="2"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></div></td>
                    </tr>
                    <tr>    
                        <td class="MyLable">13.2.2 No. of Originals</td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadNumericTextBox ID="txtNoOfOriginals2" Runat="server" NumberFormat-DecimalDigits="0" />
                        </td>
                    </tr>
                    <tr>    
                        <td class="MyLable">13.2.3 No. of Copies</td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadNumericTextBox ID="txtNoOfCopies2" Runat="server" NumberFormat-DecimalDigits="0" />
                        </td>
                    </tr>
                    <%--<tr>    
                        <td class="MyLable" style="vertical-align:top;">13.2.4 Other Docs</td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadTextBox ID="txtOtherDocs2" runat="server" TextMode="MultiLine" Height="100" Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>--%>
                </table>
                <table cellpadding="0" cellspacing="0" id="divDocs3" runat="server" style="display:none;">
                    <tr>    
                        <td class="MyLable">13.3.1 Docs Code</td>
                        <td class="MyContent"><telerik:RadComboBox
                            ID="rcbDocsCode3" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                        </telerik:RadComboBox></td>
                        <td><div id="divCmdDocs3" runat="server"><a class="removeDocs" index="3"><img src="Icons/Sigma/Delete_16X16_Standard.png" /></a></div></td>
                    </tr>
                    <tr>    
                        <td class="MyLable">13.3.2 No. of Originals</td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadNumericTextBox ID="txtNoOfOriginals3" Runat="server" NumberFormat-DecimalDigits="0" />
                        </td>
                    </tr>
                    <tr>    
                        <td class="MyLable">13.3.3 No. of Copies</td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadNumericTextBox ID="txtNoOfCopies3" Runat="server" NumberFormat-DecimalDigits="0" />
                        </td>
                    </tr>
                    <%--<tr>    
                        <td class="MyLable" style="vertical-align:top;">13.3.4 Other Docs</td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadTextBox ID="txtOtherDocs3" runat="server" TextMode="MultiLine" Height="100" Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>--%>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr>    
                        <td class="MyLable" style="vertical-align:top;">14. Other Docs</td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadTextBox ID="txtOtherDocs1" runat="server"  Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>    
                        <td class="MyLable" style="vertical-align:top;"></td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadTextBox ID="txtOtherDocs2" runat="server"  Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                     <tr>    
                        <td class="MyLable" style="vertical-align:top;"></td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadTextBox ID="txtOtherDocs3" runat="server"  Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                     <tr>    
                        <td class="MyLable" style="vertical-align:top;"></td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadTextBox ID="txtOtherDocs4" runat="server"  Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                     <tr>    
                        <td class="MyLable" style="vertical-align:top;"></td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadTextBox ID="txtOtherDocs5" runat="server"  Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                     <tr>    
                        <td class="MyLable" style="vertical-align:top;"></td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadTextBox ID="txtOtherDocs6" runat="server"  Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                     <tr>    
                        <td class="MyLable" style="vertical-align:top;"></td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadTextBox ID="txtOtherDocs7" runat="server"  Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                     <tr>    
                        <td class="MyLable" style="vertical-align:top;"></td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadTextBox ID="txtOtherDocs8" runat="server"  Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                     <tr>    
                        <td class="MyLable" style="vertical-align:top;"></td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadTextBox ID="txtOtherDocs9" runat="server"  Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                     <tr>    
                        <td class="MyLable" style="vertical-align:top;"></td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadTextBox ID="txtOtherDocs10" runat="server"  Width="355"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>    
                        <td class="MyLable" style="vertical-align:top;">15. Remark</td>
                        <td class="MyContent"><telerik:RadTextBox ID="txtRemark" runat="server" TextMode="MultiLine" Height="100" Width="355"></telerik:RadTextBox></td>
                    </tr>
                    <tr>    
                        <td class="MyLable" style="vertical-align:top;">16. Settlement Instruction </td>
                        <td class="MyContent"><telerik:RadTextBox ID="txtSettlementInstruction" runat="server" TextMode="MultiLine" Height="100" Width="355"></telerik:RadTextBox></td>
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
                        <td class="MyContent"><telerik:RadTextBox ID="txtChargeRemarks" runat="server" Width="300" /></td>
                    </tr>
                    <tr>
                        <td class="MyLable">VAT No</td>
                        <td class="MyContent"><telerik:RadTextBox ID="txtVATNo" runat="server" Width="300" /></td>
                    </tr>
                </table>
                <telerik:RadTabStrip runat="server" ID="RadTabStrip3" SelectedIndex="0" MultiPageID="RadMultiPage1" Orientation="HorizontalTop">
                    <Tabs>
                        <telerik:RadTab Text="Commission">
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
                                    <td class="MyContent"><telerik:RadTextBox ID="txtChargeCode1" runat="server" Text="ELC.ADVISE" /></td>
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
                                        <telerik:RadNumericTextBox runat="server" ID="tbChargeAmt1" 
                                            AutoPostBack="true"
                                            OnTextChanged="tbChargeAmt1_TextChanged"/>
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
                                                <telerik:RadComboBoxItem Value="A" Text="A" />
                                                <telerik:RadComboBoxItem Value="AC" Text="AC" />
                                                <telerik:RadComboBoxItem Value="B" Text="B" />
                                                <telerik:RadComboBoxItem Value="BC" Text="BC" />
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
                                                <telerik:RadComboBoxItem Value="CHARGE COLLECTED" Text="CHARGE COLLECTED" />
                                                <telerik:RadComboBoxItem Value="" Text="" />
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
                                    <td class="MyContent"><telerik:RadTextBox ID="txtChargeCode2" runat="server" Text="ELC.CONFIRM" /></td>
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
                                        <telerik:RadNumericTextBox runat="server" ID="tbChargeAmt2" 
                                            AutoPostBack="true"
                                            OnTextChanged="tbChargeAmt2_TextChanged"/>
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
                                                <telerik:RadComboBoxItem Value="A" Text="A" />
                                                <telerik:RadComboBoxItem Value="AC" Text="AC" />
                                                <telerik:RadComboBoxItem Value="B" Text="B" />
                                                <telerik:RadComboBoxItem Value="BC" Text="BC" />
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
                                                <telerik:RadComboBoxItem Value="CHARGE COLLECTED" Text="CHARGE COLLECTED" />
                                                <telerik:RadComboBoxItem Value="" Text="" />
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
                                    <td class="MyContent"><telerik:RadTextBox ID="txtChargeCode3" runat="server" Text="ELC.OTHER" /></td>
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
                                        <telerik:RadNumericTextBox runat="server" ID="tbChargeAmt3" 
                                            AutoPostBack="true"
                                            OnTextChanged="tbChargeAmt3_TextChanged"/>
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
                                                <telerik:RadComboBoxItem Value="A" Text="A" />
                                                <telerik:RadComboBoxItem Value="AC" Text="AC" />
                                                <telerik:RadComboBoxItem Value="B" Text="B" />
                                                <telerik:RadComboBoxItem Value="BC" Text="BC" />
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
                                                <telerik:RadComboBoxItem Value="CHARGE COLLECTED" Text="CHARGE COLLECTED" />
                                                <telerik:RadComboBoxItem Value="" Text="" />
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
        $(function(){
            
            if(<%= TabId %> == 244)//Hide tabCharge when page is Acept
            {
                Charges.style.display = 'none';
                tabCharge.style.display = 'none';
            }else{
                Charges.style.display = 'block';
                tabCharge.style.display = 'block';
            }
        });

        

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
        $("#<%=txtRemark.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=txtRemark.ClientID %>").val($("#<%=txtRemark.ClientID %>").val() + '\n');
            }
        });
        $("#<%=txtSettlementInstruction.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=txtSettlementInstruction.ClientID %>").val($("#<%=txtSettlementInstruction.ClientID %>").val() + '\n');
            }
        });
        $("#<%=txtOtherDocs1.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=txtOtherDocs1.ClientID %>").val($("#<%=txtOtherDocs1.ClientID %>").val() + '\n');
            }
        });
        
        $('a.addDocs').click(function () {
            var index = $(this).attr('index');
            if (index == "1") {
                if ($('#<%=divDocs3.ClientID%>').css('display') == 'none')
                    $('#<%=divDocs2.ClientID%> .addDocs').css('display', '');
                else
                    $('#<%=divDocs2.ClientID%> .addDocs').css('display', 'none');
                $('#<%=divDocs2.ClientID%>').css('display', '');
            }
            else if (index == "2") {
                $('#<%=divDocs3.ClientID%> .addDocs').css('display', '');
                $('#<%=divDocs3.ClientID%>').css('display', '');
            }
            $(this).css('display', 'none');
        });
        $('a.removeDocs').click(function () {
            var index = $(this).attr('index');
            if (index == "2") {
                $('#<%=divDocs1.ClientID%> .addDocs').css('display', '');
                $find("<%=rcbDocsCode2.ClientID%>").get_items().getItem(0).select();
                $find("<%=txtNoOfOriginals2.ClientID%>").set_value();
                $find("<%=txtNoOfCopies2.ClientID%>").set_value();
                $('#<%=divDocs2.ClientID%>').css('display', 'none');
            }
            else if (index == "3") {
                $('#<%=divDocs2.ClientID%> .addDocs').css('display', '');
                $find("<%=rcbDocsCode3.ClientID%>").get_items().getItem(0).select();
                $find("<%=txtNoOfOriginals3.ClientID%>").set_value();
                $find("<%=txtNoOfCopies3.ClientID%>").set_value();
                $('#<%=divDocs3.ClientID%>').css('display', 'none');
            }
        });
    </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"><img src="icons/bank/ajax-loader-16x16.gif" /></telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
    <AjaxSettings> 
        <telerik:AjaxSetting AjaxControlID="txtNostroAgentBankNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblNostroAgentBankMessage" />
                <telerik:AjaxUpdatedControl ControlID="txtNostroAgentBankName" />
                <telerik:AjaxUpdatedControl ControlID="txtNostroAgentBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtNostroAgentBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtNostroAgentBankAddr3" />
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
        <telerik:AjaxSetting AjaxControlID="tbChargeAmt1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt1" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tbChargeAmt2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt2" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tbChargeAmt3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt3" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<div style="visibility: hidden;">
    <asp:Button ID="btnVAT" runat="server" OnClick="btnVAT_Click" />
    <asp:Button ID="btnXuatNgoaiBang" runat="server" OnClick="btnXuatNgoaiBang_Click" />
    <asp:Button ID="btnNhapNgoaiBang" runat="server" OnClick="btnNhapNgoaiBang_Click" />
    <asp:Button ID="btnCover" runat="server" OnClick="btnCover_Click" />
</div>