<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IssueLC.ascx.cs" Inherits="BankProject.TradingFinance.Import.DocumentaryCredit.IssueLC" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"></telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />
<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <script type="text/javascript" src="DesktopModules/TrainingCoreBanking/BankProject/Scripts/Common.js"></script>
    <script type="text/javascript">
        var amount =  parseFloat(<%= Amount %>);
        var amount_Old = parseFloat(<%= Amount_Old %>);
        var chargeAmount = parseFloat(<%= TotalChargeAmt %>);
        var b4_AUT_Amount = parseFloat(<%= B4_AUT_Amount %>);
        
        var tabId = <%= TabId %>;
        var receivingBank_700 = '<%= ReceivingBank_700 %>';
        var receivingBank_740 = '<%= ReceivingBank_740 %>';
        var generateMT740 = '<%= Generate740 %>';
        var generateMT747 = '<%= Generate747 %>';
        var waiveCharges = '<%= WaiveCharges %>';
        
        jQuery(function ($) {
            $('#tabs-demo').dnnTabs();
        });

        function RadToolBar1_OnClientButtonClicking(sender, args) {
            var button = args.get_item();
            
            if (button.get_commandName() == "<%=BankProject.Controls.Commands.Print%>") {
                args.set_cancel(true);

                switch (tabId) {
                    case <%=TabIssueLCAddNew%>:
                        
                        if (receivingBank_700) {
                            radconfirm("Do you want to download MT700 file?", confirmCallbackFunction_IssueLC_MT700, 370, 150, null, 'Download');
                        } else if (generateMT740 === 'YES') {
                            radconfirm("Do you want to download MT740 file?", confirmCallbackFunction_IssueLC_MT740, 370, 150, null, 'Download');
                        } else {
                            radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file?", confirmCallbackFunction_IssueLC_NhapNgoaiBang, 430, 150, null, 'Download');
                        }
                        break;
                        
                    case <%=TabIssueLCAmend%>: // Amend LC
                        showPhieuNhap_Xuat();
                        break;
                        
                    case <%=TabIssueLCCancel%>: // Cancel LC
                        radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file?", confirmCallbackFunction_CancelLC_XuatNgoaiBang, 420, 150, null, 'Download');
                        break;
                }
            }
            //
            if (button.get_commandName() == "<%=BankProject.Controls.Commands.Commit%>") {
                args.set_cancel(true);
                var ExpiryDate = $find("<%= tbExpiryDate.ClientID %>").get_selectedDate();
                if (ExpiryDate != null){
                    var IssuingDate = $find("<%= tbIssuingDate.ClientID %>").get_selectedDate();
                    if (IssuingDate != null){
                        if (ExpiryDate < IssuingDate){
                            alert('ExpiryDate must be greater than IssuingDate');
                            return;
                        }
                    }
                }
                //
                <% if (TabId == TabIssueLCAddNew) %>
                <%{ %>
                if (!MTIsValidInput('MT700', new Array('<%=numPercentCreditAmount1.ClientID%>', '<%=numPercentCreditAmount2.ClientID%>'))) return;
                if (!MTIsValidInput('MT740', new Array('<%=numPercentageCreditAmountTolerance740_1.ClientID%>', '<%=numPercentageCreditAmountTolerance740_2.ClientID%>'))) return;
                    <% }
                    else if (TabId == TabIssueLCAmend) %>
                <%{%>
                if (!MTIsValidInput('MT707', new Array('<%=numPercentageCreditAmountTolerance_707_1.ClientID%>', '<%=numPercentageCreditAmountTolerance_707_2.ClientID%>'))) return;
                if (!MTIsValidInput('MT747', new Array('<%=numPercentageCreditTolerance_747_1.ClientID%>', '<%=numPercentageCreditTolerance_747_2.ClientID%>'))) return;
                <% } %>
                //                
                args.set_cancel(false);
            }
            //
            if (button.get_commandName() == "<%=BankProject.Controls.Commands.Hold%>") {
                //args.set_cancel(true);
                //alert('Chức năng này sẽ được thực hiện trong thời gian tới !');
            }
        }

        function showPhieuNhap_Xuat() {
            // Neu amount > amount_old -> tu chinh tang tienb, xuat phieu [nhap ngoai bang]
            //amount < amount_Old -> tu chinh giam tien,xuat phieu [xuat phieu ngoai bang]
            // amount = amoun_old -> ko xuat phieu xuat nhap ngoai bang
            if (amount_Old > 0 && amount > amount_Old) {//b4_AUT_Amount
                radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file?", confirmCallbackFunction_NhapNgoaiBang_Amendments, 430, 150, null, 'Download');
            } else if (amount_Old > 0 && amount < amount_Old) {
                radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file?", confirmCallbackFunction_XuatNgoaiBang_Amendments, 420, 150, null, 'Download');
            } 
            else if (amount_Old > 0 && amount > b4_AUT_Amount) {
                radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file?", confirmCallbackFunction_XuatNgoaiBang_Amendments, 420, 150, null, 'Download');
            }
            else if (amount_Old > 0 && amount < b4_AUT_Amount) {                
                radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file?", confirmCallbackFunction_NhapNgoaiBang_Amendments, 430, 150, null, 'Download');
            }
            else if (amount_Old === 0 && amount < b4_AUT_Amount) {
                radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file?", confirmCallbackFunction_NhapNgoaiBang_Amendments, 430, 150, null, 'Download');
            } else if (amount_Old === 0 && amount > b4_AUT_Amount) {
                radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file?", confirmCallbackFunction_XuatNgoaiBang_Amendments, 420, 150, null, 'Download');
            } else if (waiveCharges === 'NO' && chargeAmount > 0) {
                radconfirm("Do you want to download VAT file?", confirmCallbackFunction_VAT_Amendments, 350, 150, null, 'Download');
            } else {
                radconfirm("Do you want to download MT707 file?", confirmCallbackFunction_MT707_Amendments, 420, 150, null, 'Download');
            }
        }

        function confirmCallbackFunction_IssueLC_MT700(result) {
            if (result) {
                $("#<%=btnIssueLC_MT700Report.ClientID %>").click();
            }
            if (generateMT740 === 'YES') {
                radconfirm("Do you want to download MT740 file?", confirmCallbackFunction_IssueLC_MT740, 370, 150, null, 'Download');
            } else {
                radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file?", confirmCallbackFunction_IssueLC_NhapNgoaiBang, 430, 150, null, 'Download');
            }
        }
        
        function confirmCallbackFunction_IssueLC_MT740(result) {
            if (result) {
                $("#<%=btnIssueLC_MT740Report.ClientID %>").click();
            }
            radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file?", confirmCallbackFunction_IssueLC_NhapNgoaiBang, 420, 150, null, 'Download');
        }
        
        function confirmCallbackFunction_IssueLC_NhapNgoaiBang(result) {
            if (result) {
                $("#<%=btnIssueLC_NHapNgoaiBangReport.ClientID %>").click();
        }
        if (waiveCharges === 'NO' && chargeAmount > 0) {
            radconfirm("Do you want to download VAT file?", confirmCallbackFunction_IssueLC_VAT, 370, 150, null, 'Download');
        }
    }
        
    function confirmCallbackFunction_IssueLC_VAT(result) {
        if (result) {
            $("#<%=btnIssueLC_VATReport.ClientID %>").click();
            }
        }
        
        // -----Amend LC-----------------------------------------------------------------------
        function confirmCallbackFunction_XuatNgoaiBang_Amendments(result) {
            if (result) {
                $("#<%=btnAmentLCReport_XuatNgoaiBang.ClientID %>").click();
            }
            
            radconfirm("Do you want to download MT707 file?", confirmCallbackFunction_MT707_Amendments, 420, 150, null, 'Download');
        }
        
        function confirmCallbackFunction_NhapNgoaiBang_Amendments(result) {
            if (result) {
                $("#<%=btnAmentLCReport_NhapNgoaiBang.ClientID %>").click();
            }
            radconfirm("Do you want to download MT707 file?", confirmCallbackFunction_MT707_Amendments, 420, 150, null, 'Download');
        }

        function confirmCallbackFunction_MT707_Amendments(result) {
            if (result) {
                $("#<%=btnAmentLCReport_MT707.ClientID %>").click();
            }

            if (generateMT747 === 'YES') {
                radconfirm("Do you want to download MT747 file?", confirmCallbackFunction_MT747_Amendments, 420, 150, null, 'Download');
            } else if (waiveCharges === 'NO' && chargeAmount > 0) {
                radconfirm("Do you want to download VAT file?", confirmCallbackFunction_VAT_Amendments, 420, 150, null, 'Download');    
            }
        }

        function confirmCallbackFunction_MT747_Amendments(result) {
            if (result) {
                $("#<%=btnAmentLCReport_MT747.ClientID %>").click();
            }
            if (waiveCharges === 'NO' && chargeAmount > 0) {
                radconfirm("Do you want to download VAT file?", confirmCallbackFunction_VAT_Amendments, 420, 150, null, 'Download');    
            }
        }

        function confirmCallbackFunction_VAT_Amendments(result) {
            if (result) {
                $("#<%=btnAmentLCReport_VAT.ClientID %>").click();
            }
        }
        
        // -----Amend LC-----------------------------------------------------------------------
        
        // ---- Cancel LC ---------------------------------------------
        function confirmCallbackFunction_CancelLC_XuatNgoaiBang(result) {
            if (result) {
                $("#<%=btnCancelLC_XUATNGOAIBANG.ClientID %>").click();
            }
            if (waiveCharges === 'NO' && chargeAmount > 0) {
                radconfirm("Do you want to download VAT file?", confirmCallbackFunction_CancelLC_VAT, 350, 150, null, 'Download');
            }
        }
        
        function confirmCallbackFunction_CancelLC_VAT(result) {
            if (result) {
                $("#<%=btnCancelLC_VAT.ClientID %>").click();
            }
        }
        // ---- Cancel LC ---------------------------------------------
        
    </script>
</telerik:RadCodeBlock>

<telerik:RadToolBar runat="server" ID="RadToolBar1" OnClientButtonClicking="RadToolBar1_OnClientButtonClicking"
    EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/hold.png"
            ToolTip="Hold Data" Value="btHoldData" CommandName="hold" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btPreview" CommandName="preview">
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
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtCode" runat="server" Width="200" /><span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator4"
                        ControlToValidate="txtCode"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Code is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>&nbsp;<asp:Label ID="lblError" runat="server" ForeColor="red" /></td>
    </tr>
</table>
<div class="dnnForm" id="tabs-demo">
    <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
        <ul class="dnnAdminTabNav">
            <li><a href="#Main">Main</a></li>
            <% if (TabId == TabIssueLCAddNew) %>
            <%{ %>
            <li><a href="#MT700">MT700</a></li>
            <li><a href="#MT740">MT740</a></li>
            <% }
                else if (TabId == TabIssueLCAmend) %>
            <%{%>
            <li><a href="#MT707">MT707</a></li>
            <li><a href="#MT747">MT747</a></li>
            <% } %>
            <% if (TabId != TabIssueLCClose) %>
            <%{ %>
            <li><a href="#Charges">Charges</a></li>
            <% } %>
        </ul>
    </telerik:RadCodeBlock>
    <div id="Main" class="dnnClear">
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
        <div runat="server" id="divCloseLC" visible="false">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Generate Delivery ?</td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cboGenerateDelivery"
                            MarkFirstMatch="True"
                            AllowCustomText="false"
                            runat="server" Width="355">
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="YES" Text="YES" />
                                <telerik:RadComboBoxItem Value="NO" Text="NO" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr style="color: #CCC;">
                    <td class="MyLable">Closed Date</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtClosedDate" runat="server" Width="355" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Contingent Expiry Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="txtExternalReference" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Closed Remark</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtClosingRemark" runat="server" Width="355" />
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
            <tr>
                <td class="MyLable">1. LC Type <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="rcbLCType"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="LC Type is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="MyContent" style="width: 355px;">
                    <telerik:RadComboBox
                        AppendDataBoundItems="True"
                        DropDownCssClass="KDDL"
                        ID="rcbLCType" runat="server" Width="355"
                        MarkFirstMatch="True"
                        OnItemDataBound="rcbLCType_ItemDataBound"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="rcbLCType_SelectIndexChange"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <HeaderTemplate>
                            <table style="width: 305px" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 60px;">LC Type 
                                    </td>
                                    <td style="width: 200px;">Description
                                    </td>
                                    <td>Category
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="width: 305px" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 60px;">
                                        <%# DataBinder.Eval(Container.DataItem, "LCTYPE")%> 
                                    </td>
                                    <td style="width: 200px;">
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%> 
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "Category")%> 
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                </td>
                <td>
                    <asp:Label ID="lblLCType" runat="server" /></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">2.1 Applicant ID</td>
                <td class="MyContent">
                    <telerik:RadComboBox
                        AppendDataBoundItems="True"
                        OnItemDataBound="rcbApplicantID_ItemDataBound"
                        OnClientSelectedIndexChanged="rcbApplicantID_OnClientSelectedIndexChanged"
                        ID="rcbApplicantID" runat="server"
                        MarkFirstMatch="True"
                        Width="355"
                        Height="150"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">2.2 Applicant Name</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbApplicantName" runat="server" Width="355" ClientEvents-OnValueChanged="tbApplicantName_OnValueChanged" MaxLength="35"/>
                </td>
            </tr>
            <tr>
                <td class="MyLable">2.3 Applicant Address</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbApplicantAddr1" runat="server" Width="355" ClientEvents-OnValueChanged="tbApplicantAddr1_OnValueChanged" MaxLength="35"/>
                </td>
            </tr>

            <tr>
                <td class="MyLable"></td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbApplicantAddr2" runat="server" Width="355" ClientEvents-OnValueChanged="tbApplicantAddr2_OnValueChanged" MaxLength="35"/>
                </td>
            </tr>

            <tr>
                <td class="MyLable"></td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbApplicantAddr3" runat="server" Width="355" ClientEvents-OnValueChanged="tbApplicantAddr3_OnValueChanged" MaxLength="35"/>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr style="display:none;">
                <td class="MyLable">Applicant Acct</td>
                <td class="MyContent">
                    <telerik:RadComboBox
                        ID="rcbApplicantAcct" runat="server"
                        MarkFirstMatch="True" Width="150"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable" style="width:150px;">3. Currency Code <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator2"
                        ControlToValidate="rcbCcyAmount"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Ccy is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox
                        OnClientSelectedIndexChanged="rcbCcyAmount_OnClientSelectedIndexChanged"
                        AppendDataBoundItems="True" Width="135"
                        ID="rcbCcyAmount" runat="server"
                        MarkFirstMatch="True"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                    </telerik:RadComboBox>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Amount <span class="Required">(*)</span>
                    <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true"
                        IncrementSettings-InterceptMouseWheel="true"
                        runat="server" ID="ntSoTien" AutoPostBack="False"
                        OnTextChanged="ntSoTien_TextChanged" Width="100"
                        ClientEvents-OnValueChanged="ntSoTien_OnValueChanged" />
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator3"
                        ControlToValidate="ntSoTien"
                        ValidationGroup="Commit"
                        InitialValue="0"
                        ErrorMessage="Amount is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <div runat="server" id="divAmount">
                <tr>
                    <td class="MyLable">Amount Old</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAmount_Old" runat="server" ForeColor="#0091E1" />
                    </td>
                </tr>
            </div>
            <tr>
                <td class="MyLable">4.1 Tolerance Plus</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true"
                        IncrementSettings-InterceptMouseWheel="true" Type="Percent" MaxValue="100"
                        ClientEvents-OnValueChanged="tbcrTolerance_TextChanged"
                        runat="server" ID="tbcrTolerance" Width="135" />
                    4.2 Tolerance Minus &nbsp;<telerik:RadNumericTextBox
                        IncrementSettings-InterceptArrowKeys="true" Type="Percent" MaxValue="100"
                        IncrementSettings-InterceptMouseWheel="true" runat="server"
                        ClientEvents-OnValueChanged="tbdrTolerance_TextChanged"
                        ID="tbdrTolerance" Width="100" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">5. Issuing Date</td>
                <td class="MyContent">
                    <telerik:RadDatePicker runat="server" ID="tbIssuingDate">
                        <ClientEvents OnDateSelected="tbIssuingDate_DateSelected" />
                    </telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="MyLable">6. Expiry Date</td>
                <td class="MyContent">
                    <telerik:RadDatePicker runat="server" ID="tbExpiryDate">
                        <ClientEvents OnDateSelected="tbExpiryDate_DateSelected" />
                    </telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="MyLable">7. Expiry Place</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbExpiryPlace" Width="355" runat="server"
                        ClientEvents-OnValueChanged="tbExpiryPlace_OnValueChanged">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">8. Contingent Expiry</td>
                <td class="MyContent">
                    <telerik:RadDatePicker runat="server" MinDate="1/1/1900" ID="tbContingentExpiry">
                    </telerik:RadDatePicker>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">9. Account Officer</td>
                <td class="MyContent">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <telerik:RadComboBox
                                    AppendDataBoundItems="True"
                                    ID="rcbAccountOfficer"
                                    runat="server"
                                    MarkFirstMatch="True"
                                    Width="355"
                                    Height="150px"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">10. Contract No</td>
                <td class="MyContent">
                    <asp:TextBox ID="tbContactNo" runat="server" Width="350" />
                </td>
            </tr>
        </table>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Beneficiary Details</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display: none;">
                    <td class="MyLable">Beneficiary Type</td>
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

            <table cellpadding="0" cellspacing="0" style="display:none;">
                <tr>
                    <td class="MyLable">11.1 Beneficiary No.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryNo" runat="server" MaxLength="34" AutoPostBack="True" OnTextChanged="txtBeneficiaryNo_OnTextChanged" />
                    </td>
                    <td><asp:Label ID="lblBeneficiaryBankMessage" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">11.2 Beneficiary Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryBankName" runat="server" Width="355" ClientEvents-OnValueChanged="txtBeneficiaryBankName_OnValueChanged" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">11.3 Beneficiary Address</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryBankAddr1" runat="server" Width="355" ClientEvents-OnValueChanged="txtBeneficiaryBankAddr1_OnValueChanged" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryBankAddr2" runat="server" Width="355" ClientEvents-OnValueChanged="txtBeneficiaryBankAddr2_OnValueChanged" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryBankAddr3" runat="server" Width="355" ClientEvents-OnValueChanged="txtBeneficiaryBankAddr3_OnValueChanged" />
                    </td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Advising/Reimbursing Bank Details</div>
            </legend>

            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width:185px;">12.1 Advising Bank Code</td>
                    <td class="MyContent"><telerik:RadTextBox ID="txtAdviseBankNo" runat="server" AutoPostBack="True" OnTextChanged="txtAdviseBankNo_OnTextChanged" />
                    </td>
                    <td><asp:Label ID="lblAdviseBankMessage" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width:185px;">12.2 Advising Bank Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAdviseBankName" runat="server" Width="355" MaxLength="35" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">12.3 Advising Bank Address</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAdviseBankAddr1" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAdviseBankAddr2" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAdviseBankAddr3" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width:185px;">13.1 Reimbursing Bank Option</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            AutoPostBack="True"
                            OnSelectedIndexChanged="comboReimbBankType_OnSelectedIndexChanged"
                            OnClientSelectedIndexChanged="comboReimbBankType_OnClientSelectedIndexChanged"
                            ID="comboReimbBankType" runat="server"
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
                    <td class="MyLable" style="width:185px;">13.2 Reimbursing Bank Code</td>
                    <td class="MyContent"><telerik:RadTextBox ID="txtReimbBankNo" runat="server" AutoPostBack="True" OnTextChanged="txtReimbBankNo_OnTextChanged" /></td>
                    <td><asp:Label ID="lblReimbBankMessage" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width:185px;">13.3 Reimbursing Bank Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbReimbBankName" runat="server" Width="355"
                            AutoPostBack="False" OnTextChanged="tbReimbBankName_tbReimbBankName"
                            ClientEvents-OnValueChanged="tbReimbBankName_OnClientSelectedIndexChanged" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">13.4 Reimbursing Bank Address</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbReimbBankAddr1" runat="server" Width="355"
                            AutoPostBack="False" OnTextChanged="tbReimbBankAddr1_OnTextChanged"
                            ClientEvents-OnValueChanged="tbReimbBankAddr1_OnClientSelectedIndexChanged" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbReimbBankAddr2" runat="server" Width="355"
                            AutoPostBack="False" OnTextChanged="tbReimbBankAddr2_OnTextChanged"
                            ClientEvents-OnValueChanged="tbReimbBankAddr2_OnClientSelectedIndexChanged" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbReimbBankAddr3" runat="server" Width="355"
                            AutoPostBack="False" OnTextChanged="tbReimbBankAddr3_OnTextChanged"
                            ClientEvents-OnValueChanged="tbReimbBankAddr3_OnClientSelectedIndexChanged" MaxLength="35"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width:185px;">14.1 Advise through Option</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            AutoPostBack="True"
                            OnSelectedIndexChanged="rcbAdviseThruType_OnSelectedIndexChanged"
                            ID="rcbAdviseThruType" runat="server"
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
                    <td class="MyLable" style="width:185px;">14.2 Advise through Code</td>
                    <td class="MyContent"><telerik:RadTextBox ID="txtAdviseThruNo" runat="server" AutoPostBack="True" OnTextChanged="txtAdviseThruNo_OnTextChanged" /></td>
                    <td><asp:Label ID="lblAdviseThruMessage" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width:185px;">14.3 Advise through Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAdviseThruName" runat="server" Width="355" MaxLength="35" 
                            ClientEvents-OnValueChanged="tbAdviseThruName_OnClientSelectedIndexChanged"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">14.4 Advise through Address</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAdviseThruAddr1" runat="server" Width="355" MaxLength="35" 
                            ClientEvents-OnValueChanged="tbAdviseThruAddr1_OnClientSelectedIndexChanged"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAdviseThruAddr2" runat="server" Width="355" MaxLength="35" 
                            ClientEvents-OnValueChanged="tbAdviseThruAddr2_OnClientSelectedIndexChanged"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAdviseThruAddr3" runat="server" Width="355" MaxLength="35" 
                            ClientEvents-OnValueChanged="tbAdviseThruAddr3_OnClientSelectedIndexChanged"/>
                    </td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">OTHER</div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">15. Commodity <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator5"
                            ControlToValidate="rcCommodity"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Commodity is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            AutoPostBack="true"
                            DropDownCssClass="KDDL"
                            ID="rcCommodity" runat="server"
                            AppendDataBoundItems="True"
                            MarkFirstMatch="True"
                            OnItemDataBound="rcCommodity_ItemDataBound"
                            OnSelectedIndexChanged="rcCommodity_SelectIndexChange"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:Label ID="lblCommodity" runat="server" /></td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">16. Prov % 
                    </td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox runat="server" ID="numPro" Type="Percent" MaxValue="100" ClientEvents-OnValueChanged="numPro_OnValueChanged" />
                    </td>
                </tr>
            </table>
            <!-- -->
            <table width="100%" cellpadding="0" cellspacing="0" style="display:none;">
                <tr>
                    <td class="MyLable">16. Lc Amount Secured <span class="Required">(*)</span>
                    </td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox runat="server" ID="numLcAmountSecured" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">17. Lc Amt UnSecured <span class="Required">(*)</span>
                    </td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox runat="server" ID="numLcAmountUnSecured" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">17. Loan Principal</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox runat="server" ID="numLoanPrincipal" />
                    </td>
                </tr>
            </table>
            <!-- An F18 do phuc tap van de ngoai te -->
            <table cellpadding="0" cellspacing="0" style="display:none;">
                <tr>
                    <td class="MyLable">18. Import Limit</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox runat="server" ID="txtImportLimit" Enabled="false" />
                    </td>
                    <td style="color:GrayText;"><asp:Label ID="lblImportLimitMessage" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </fieldset>
    </div>

    <div id="MT700" class="dnnClear">
        <div runat="server" id="divMT700">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px">Receiving Bank</td>
                    <td class="MyContent">
                        <%--<telerik:RadComboBox 
                            width="355"
                            Height="150"
                            AppendDataBoundItems="true"
                            ID="comboRevivingBank" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>--%>
                        <telerik:RadTextBox ID="txtRevivingBank700" runat="server" Width="200" />
                    </td>
                    <td>
                        <asp:Label ID="tbRevivingBankName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="color: #d0d0d0">27.1 Sequence of Total</td>
                    <td class="MyContent">
                        <asp:Label ID="tbBaquenceOfTotal" runat="server" Text="1/1" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="MyLable">40A. Form of Documentary Credit</td>
                    <td class="MyContent">
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
                    <td class="MyLable">20. Documentary Credit Number</td>
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
            <table cellpadding="0" cellspacing="0">                
                <tr>
                    <td class="MyLable" style="width: 250px">40E. Applicable Rules</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboAvailableRule"
                            runat="server" Width="200"
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
                <tr>
                    <td class="MyLable">31D. Date and Place of Expiry</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="txtDateOfExpiry700" Width="200" runat="server"></telerik:RadDatePicker>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtPlaceOfExpiry700" runat="server" Width="155" /></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display: none;">
                    <td style="width: 250px" class="MyLable">50. Applicant Type</td>
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
                    <td style="width: 250px" class="MyLable">50. Applicant</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbApplicantNo700" runat="server" MaxLength="34"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbApplicantName700" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbApplicantAddr700_1" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbApplicantAddr700_2" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbApplicantAddr700_3" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0" style="display: none">
                <tr>
                    <td class="MyLable" style="width: 250px">59. Beneficiary</td>
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
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px">59. Beneficiary</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadTextBox ID="txtBeneficiaryNo700" runat="server" AutoPostBack="false" OnTextChanged="txtBeneficiaryNo700_OnTextChanged" MaxLength="34"/>
                    </td>
                    <td>
                        <asp:Label ID="lblBeneficiaryNo700Error" runat="server" Text="" ForeColor="red" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryName700" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryAddr700_1" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryAddr700_2" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryAddr700_3" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">32B. Currency Code, Amount <span class="Required">(*)</span>
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
                            InitialValue="0"
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
                <tr>
                    <td style="width: 250px" class="MyLable">39C. Additional Amounts Covered</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtAdditionalAmountsCovered700_1" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtAdditionalAmountsCovered700_2" Width="355" MaxLength="35"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">41D.1 Available With Option</td>
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
                    <td style="width: 250px" class="MyLable">41D.2 Available With Code</td>
                    <td class="MyContent"><telerik:RadTextBox ID="txtAvailableWithNo" runat="server" AutoPostBack="True" OnTextChanged="txtAvailableWithNo_OnTextChanged" MaxLength="14"/></td>
                    <td><asp:Label ID="lblAvailableWithMessage" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">41D.3 Available With Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAvailableWithName" runat="server" Width="355"  MaxLength="35" 
                            ClientEvents-OnValueChanged="tbAvailableWithName_OnValueChanged" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">41D.4 Available With Address</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAvailableWithAddr1" runat="server" Width="355" MaxLength="35" 
                            ClientEvents-OnValueChanged="tbAvailableWithAddr1_OnValueChanged"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAvailableWithAddr2" runat="server" Width="355" MaxLength="35" 
                            ClientEvents-OnValueChanged="tbAvailableWithAddr2_OnValueChanged"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAvailableWithAddr3" runat="server" Width="355" MaxLength="35"
                            ClientEvents-OnValueChanged="tbAvailableWithAddr3_OnValueChanged"/>
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
                    <td style="width: 250px" class="MyLable">42C. Drafts At</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDraftsAt700_1" Width="355" MaxLength="35"
                            ClientEvents-OnValueChanged="txtDraftsAt700_1_OnClientSelectedIndexChanged" />
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDraftsAt700_2" Width="355" MaxLength="35" ClientEvents-OnValueChanged="txtDraftsAt700_2_OnClientSelectedIndexChanged" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">42A.1 Drawee Option</td>
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
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width:250px;">42A.2 Drawee No</td>
                    <td class="MyContent"><telerik:RadTextBox ID="txtDraweeCusNo700" runat="server" AutoPostBack="True" OnTextChanged="txtDraweeCusNo700_OnTextChanged" MaxLength="34"/></td>
                    <td><asp:Label ID="lblDraweeCusNo700Message" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">42A.3 Drawee Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeCusName" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">42A.4 Drawee Address</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeAddr1" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeAddr2" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeAddr3" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">42M. Mixed Payment Details</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtMixedPaymentDetails700_1" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtMixedPaymentDetails700_2" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtMixedPaymentDetails700_3" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtMixedPaymentDetails700_4" Width="355" MaxLength="35"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">42P. Deferred Payment Details</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDeferredPaymentDetails700_1" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDeferredPaymentDetails700_2" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDeferredPaymentDetails700_3" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDeferredPaymentDetails700_4" Width="355" MaxLength="35"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">43P. Partial Shipment</td>
                    <td class="MyContent">
                        <telerik:RadComboBox Width="200"
                            ID="rcbPatialShipment" runat="server"
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
                                <telerik:RadComboBoxItem Value="Allowed" Text="Allowed" />
                                <telerik:RadComboBoxItem Value="Not Allowed" Text="Not Allowed" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">44A. Place of taking in charge...</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="tbPlaceoftakingincharge" runat="server" MaxLength="65"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">44E. Port of loading...</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="tbPortofloading" runat="server" MaxLength="65"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">44F. Port of Discharge...</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="tbPortofDischarge" runat="server" MaxLength="65"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">44B. Place of final destination</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="tbPlaceoffinalindistination" runat="server" MaxLength="65"/>
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
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod700_1" Width="355" MaxLength="65"/>
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod700_2" Width="355" MaxLength="65"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod700_3" Width="355" MaxLength="65"/>
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod700_4" Width="355" MaxLength="65"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod700_5" Width="355" MaxLength="65"/>
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod700_6" Width="355" MaxLength="65"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px; vertical-align: top;" class="MyLable">45A. Description of Goods/Services</td>
                    <td class="MyContent" style="vertical-align: top;">
                        <telerik:RadEditor runat="server" ID="txtEdittor_DescrpofGoods" Height="200" BorderWidth="0"
                            ToolsFile="DesktopModules/TrainingCoreBanking/BankProject/TradingFinance/BasicTools.xml" />
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
                        <telerik:RadEditor runat="server" ID="txtEdittor_OrderDocs700" Height="150" BorderWidth="0"
                            ToolsFile="DesktopModules/TrainingCoreBanking/BankProject/TradingFinance/BasicTools.xml" />

                        <%--<telerik:RadTextBox width="700" TextMode="MultiLine" Height="150"
                                            ID="tbOrderDocs" runat="server" Text="1. SIGNED COMMERCIAL INVOICE IN 03 ORIGINALS ISSUED BY THE BENEFICIARY 
2. FULL (3/3) SET OF ORIGINAL CLEAN SHIPPED ON BOARD BILL OF LADING MADE OUT TO ORDER OF TANPHU BRANCH, NOTIFY APPLICANT AND MARKED FREIGHT PREPAID, SHOWING THE NAME AND ADDRESS OF SHIPPING AGENT WHICH IS LOCATED IN VIETNAM. 
3. QUANTITY AND QUALITY CERTIFICATE IN 01 ORIGINAL AND 02 COPIES ISSUED BY THE BENEFICIARY 
4. CERTIFICATE OF ORIGIN IN 01 ORIGINAL AND 02 COPIES ISSUED BY ANY CHAMBER OF COMMERCE IN EUROPEAN COMMUNITY CERTIFYING THAT THE GOODS ARE OF EUROPEAN COMMUNITY ORIGIN 
5. DETAILED PACKING LIST IN 03 ORIGINALS ISSUED BY THE BENEFICIARY" />--%>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px; vertical-align: top;" class="MyLable">47A. Additional Conditions</td>
                    <td class="MyContent" style="vertical-align: top;">
                        <telerik:RadEditor runat="server" ID="txtEdittor_AdditionalConditions700" Height="230" BorderWidth="0"
                            ToolsFile="DesktopModules/TrainingCoreBanking/BankProject/TradingFinance/BasicTools.xml" />

                        <%--<telerik:RadTextBox width="700" TextMode="MultiLine" Height="230" 
                                            ID="tbAdditionalConditions" runat="server" Text="1. ALL REQUIRED DOCUMENTS AND ITS ATTACHED LIST (IF ANY) MUST BE SIGNED OR STAMPED BY ISSUER. 
2. ALL DRAFT(S) AND DOCUMENTS MUST BE MADE OUT IN ENGLISH. DOCUMENTS ISSUED IN ANY OTHER LANGUAGE THAN ENGLISH BUT WITH ENGLISH TRANSLATION ACCEPTABLE. PRE-PRINTED WORDING (IF ANY) ON DOCUMENTS MUST BE IN ENGLISH OR BILINGUAL BUT ONE OF ITS LANGUAGES MUST BE IN ENGLISH. 
3. ALL REQUIRED DOCUMENTS MUST INDICATE OUR L/C NUMBER. 
4. ALL REQUIRED DOCUMENTS MUST BE PRESENTED THROUGH BENEFICIARY'S BANK 
5. SHIPMENT MUST NOT BE EFFECTED BEFORE L/C ISSUANCE DATE. 
6. THE TIME OF RECEIVING AND HANDLING CREDIT DOCUMENTS AT ISSUING BANK ARE LIMITED FROM 7:30 AM TO 04:00 PM. DOCUMENTS ARRIVING AT OUR COUNTER AFTER 04:00 PM LOCAL TIME WILL BE CONSIDERED TO BE RECEIVED ON THE NEXT BANKING DAY. 
7. PLEASE BE INFORMED THAT SATURDAY IS CONSIDERED AS NON-BANKING BUSINESS DAY FOR OUR TRADE FINANCE PROCESSING/OPERATIONS UNIT ALTHOUGH OUR BANK MAY OTHERWISE BE OPENED FOR BUSINESS. 
8. THIRD PARTY DOCUMENTS ARE ACCEPTABLE." />--%>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px; vertical-align: top;" class="MyLable">71B. Charges </td>
                    <td class="MyContent" style="vertical-align: top;">
                        <telerik:RadEditor runat="server" ID="txtEdittor_Charges700" Height="200"   BorderWidth="0"
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
                        <telerik:RadEditor runat="server" ID="txtEdittor_PeriodforPresentation700" Height="170" BorderWidth="0"                             
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
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px;">53.2 Reimb. Bank No</td>
                    <td class="MyContent"><telerik:RadTextBox ID="txtReimbBankNo700" runat="server" AutoPostBack="True" OnTextChanged="txtReimbBankNo700_OnTextChanged" MaxLength="34"/></td>
                    <td><asp:Label ID="lblReimbBankNo700Message" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px;">53.3 Reimb. Bank Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbReimbBankName700" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;">53.4 Reimb. Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbReimbBankAddr700_1" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbReimbBankAddr700_2" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbReimbBankAddr700_3" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px; vertical-align: top;" class="MyLable">78. Instr to//Payg/Accptg/Negotg Bank</td>
                    <td class="MyContent" style="vertical-align: top;">
                        <telerik:RadEditor runat="server" ID="txtEdittor_NegotgBank700" Height="200" BorderWidth="0" 
ToolsFile="DesktopModules/TrainingCoreBanking/BankProject/TradingFinance/BasicTools.xml" />

                       <%-- <telerik:RadTextBox Width="700" TextMode="MultiLine" Height="150"
                            ID="tbNegotgBank" runat="server" Text="1. USD70.00 DISCREPANCY FEE WILL BE DEDUCTED FROM THE PROCEEDS FOR EACH DISCREPANT SET OF DOCUMENTS PRESENTED UNDER THIS L/C. THE RELATIVE TELEX EXPENSES USD25.00, IF ANY, WILL BE ALSO FOR THE ACCOUNT OF BENEFICIARY. 
2. EACH DRAWING MUST BE ENDORSED ON THE ORIGINAL L/C BY THE NEGOTIATING/PRESENTING BANK. 
3. PLEASE SEND ALL DOCS TO VIET VICTORY BANK AT PLOOR 9TH, NO.10 PHO QUANG STREET, TAN BINH DISTRICT, HOCHIMINH CITY, VIETNAM IN ONE LOT BY THE COURIER SERVICES. 
4. UPON RECEIPT OF DOCS REQUIRED IN COMPLIANCE WITH ALL TERMS AND CONDITIONS OF THE L/C, WE SHALL REMIT THE PROCEEDS TO YOU AS PER YOUR INSTRUCTIONS IN THE COVER LETTER." />--%>
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
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px;">57.1 Advise Through No</td>
                    <td class="MyContent"><telerik:RadTextBox ID="txtAdviseThroughBankNo700" runat="server" AutoPostBack="True" OnTextChanged="txtAdviseThroughBankNo700_OnTextChanged" MaxLength="34"/></td>
                    <td><asp:Label ID="lblAdviseThroughBankNo700" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px;">57.2 Advise Through Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdviseThroughBankName700" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;">57.3 Advise Through Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdviseThroughBankAddr700_1" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdviseThroughBankAddr700_2" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdviseThroughBankAddr700_3" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px; vertical-align: top;" class="MyLable">72. Sender to Receiver Information</td>
                    <td class="MyContent" style="vertical-align: top;">
                        <telerik:RadEditor runat="server" ID="txtEdittor_SendertoReceiverInfomation700" Height="75"  BorderWidth="0"
ToolsFile="DesktopModules/TrainingCoreBanking/BankProject/TradingFinance/BasicTools.xml" />

                       <%-- <asp:TextBox Width="700" TextMode="MultiLine" Height="75"
                            ID="tbSendertoReceiverInfomation" runat="server">PLEASE ACKNOWLEDGE YOUR RECEIPT OF THIS L/C BY MT730.</asp:TextBox>--%>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div id="MT740" class="dnnClear">
        <div runat="server" id="divMT740">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">Generate MT740</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            Width="200"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="comGenerate_OnSelectedIndexChanged"
                            ID="comGenerate" runat="server"
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

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">Receiving Bank</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadTextBox ID="txtRemittingBankNo" runat="server"
                            AutoPostBack="True" OnTextChanged="txtReceivingBankNo_OnTextChanged" />
                    </td>
                    <td>
                        <asp:Label ID="lblReceivingBankNoError" runat="server" Text="" ForeColor="red" />
                        <asp:Label ID="lblReceivingBankName" runat="server" Text="" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable" style="color: #d0d0d0">20. Documentary Credit Number</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDocumentaryCreditNumber740" runat="server" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">31D. Date and Place of Expiry</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadDatePicker Width="200" ID="txtDateOfExpiry740" runat="server"></telerik:RadDatePicker>
                    </td>
                    <td>
                        <telerik:RadTextBox Width="200" ID="txtPlaceOfExpiry740" runat="server" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px">59.1 Beneficiary Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            AutoPostBack="True"
                            OnSelectedIndexChanged="rcbBeneficiaryType740_OnSelectedIndexChanged"
                            ID="rcbBeneficiaryType740" runat="server"
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
                    <td style="width: 250px" class="MyLable">59.2 Beneficiary No.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbBeneficiaryNo740" runat="server"  />
                    </td>
                    <td>
                        <asp:Label ID="lblBeneficiaryError740" runat="server" Text="" ForeColor="red" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">59.3 Beneficiary Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbBeneficiaryName740" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 200px" class="MyLable">59.4 Beneficiary Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbBeneficiaryAddr740_1" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 200px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbBeneficiaryAddr740_2" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 200px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbBeneficiaryAddr740_3" runat="server" Width="355" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">32B. Credit Amount</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadComboBox Width="200"
                            ID="comboCreditCurrency"
                            AppendDataBoundItems="True"
                            runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="numCreditAmount" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable">39A. Percentage Credit Amount Tolerance</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadNumericTextBox Width="200" ID="numPercentageCreditAmountTolerance740_1"
                            runat="server" Type="Percent" MaxValue="100" />
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="numPercentageCreditAmountTolerance740_2" runat="server" Type="Percent" MaxValue="100" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable">40F. Applicable Rule</td>
                    <td class="MyContent">
                        <asp:Label ID="lblApplicableRule740" runat="server" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">41A.1 Available With Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            AutoPostBack="True"
                            OnSelectedIndexChanged="rcbAvailableWithType740_OnSelectedIndexChanged"
                            ID="rcbAvailableWithType740" runat="server"
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
                    <td style="width: 250px" class="MyLable">41A.2 Available With No.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAvailableWithNo740" runat="server" AutoPostBack="True" OnTextChanged="tbAvailableWithNo740_OnTextChanged" />
                    </td>
                    <td>
                        <asp:Label ID="lblAvailableWithNoError740" runat="server" Text="" ForeColor="red" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">41A.3 Available With Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAvailableWithName740" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">41A.4 Available With Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAvailableWithAddr740_1" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAvailableWithAddr740_2" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAvailableWithAddr740_3" runat="server" Width="355" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">42C. Drafts At</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDraftsAt740_1" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDraftsAt740_2" Width="355" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">

                <tr>
                    <td style="width: 250px" class="MyLable">42A.2 Drawee No</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbDraweeCusNo740" runat="server" Text="VVTBVNVX" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable">42A.3 Drawee Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbDraweeCusName740" runat="server" Width="355" Text="VIETVICTORY BANK" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">42A.4 Drawee Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbDraweeAddr740_1" runat="server" Width="355" Text="9th FLOOR, 10 PHO QUANG, WARD 2" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbDraweeAddr740_2" runat="server" Width="355" Text="TAN BINH DISTRICT" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbDraweeAddr740_3" runat="server" Width="355" Text="HCM CITY" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">71A. Reimbursing Bank's Charges</td>
                    <td class="MyContent">
                        <telerik:RadComboBox Width="200"
                            ID="comboReimbursingBankChange" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="CLM" Text="CLM" />
                                <telerik:RadComboBoxItem Value="OUR" Text="OUR" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">72.1 Sender to receiver information</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation740_1" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation740_2" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation740_3" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation740_4" runat="server" Width="355" />
                    </td>
                </tr>
            </table>

        </div>
    </div>

    <div id="MT707" class="dnnClear">
        <div runat="server" id="divMT707">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px">Receiving Bank</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadTextBox ID="txtReceivingBankId_707" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblReceivingBankName_707" runat="server" Text="" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">20. Sender's Reference</td>
                    <td class="MyContent">
                        <asp:Label ID="lblSenderReference_707" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">21. Receiver's Reference</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtReceiverReference_707" runat="server" Width="355" MaxLength="16"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">23. Reference To Pre-Advice</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtReferenceToPreAdvice_707" runat="server" Width="355" MaxLength="16"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">52A.1 Issuing Bank Reference</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIssuingBankReferenceNo_707" runat="server" MaxLength="34"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">52A.2 Issuing Bank Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIssuingBankReferenceName_707" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">52A.3 Issuing Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIssuingBankReferenceAddr_707_1" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIssuingBankReferenceAddr_707_2" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIssuingBankReferenceAddr_707_3" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">31C. Date of Issue</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker runat="server" ID="dteDateOfIssue_707" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable">40E. Applicable Rule</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadComboBox
                            ID="comboAvailableRule_707"
                            runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
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

                <tr>
                    <td class="MyLable">30. Date of Amendment</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker runat="server" ID="dteDateOfAmendment_707" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px">59.1 Beneficiary Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboBeneficiaryType_707" runat="server"
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

                <tr>
                    <td class="MyLable" style="width: 250px">59.1 Beneficiary No.</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadTextBox ID="txtBeneficiaryNo_707" runat="server" MaxLength="34"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px">59.2 Beneficiary Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryName_707" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">59.3 Beneficiary Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryAddr_707_1" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryAddr_707_2" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryAddr_707_3" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;">31E. New Date of Expiry</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker runat="server" ID="dteNewDateOfExpiry_707" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px;">39A. Percentage Credit Amount Tolerance</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true"
                            IncrementSettings-InterceptMouseWheel="true" Type="Percent" MaxValue="100"
                            runat="server" ID="numPercentageCreditAmountTolerance_707_1" />
                    </td>
                    <td>
                        <telerik:RadNumericTextBox
                            IncrementSettings-InterceptArrowKeys="true" Type="Percent" MaxValue="100"
                            IncrementSettings-InterceptMouseWheel="true" runat="server"
                            ID="numPercentageCreditAmountTolerance_707_2" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px;">39B. Maximum Credit Amount</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboMaximumCreditAmount_707" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="NOT EXCEEDING" Text="NOT EXCEEDING" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">39C. Additional Amounts Covered, if amd.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdditionalAmountsCovered_707_1" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdditionalAmountsCovered_707_2" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">44A. Place of taking in charge...</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="txtPlaceoftakingincharge_707" runat="server" MaxLength="65"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">44B. Place of final destination</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="txtPlaceoffinalindistination_707" runat="server" MaxLength="65"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">44C. Latest Date of Shipment, if amd.</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker runat="server" ID="dteLatesDateofShipment_707" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">44D. Shipment Period</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod_707_1" Width="355" MaxLength="65"/>
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod_707_2" Width="355" MaxLength="65"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod_707_3" Width="355" MaxLength="65"/>
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod_707_4" Width="355" MaxLength="65"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod_707_5" Width="355" MaxLength="65"/>
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtShipmentPeriod_707_6" Width="355" MaxLength="65"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">44E. Port of loading...</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="txtPortofloading_707" runat="server" MaxLength="65"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">44F. Port of Discharge...</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="txtPortofDischarge_707" runat="server" MaxLength="65"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="vertical-align: top">79. Narrative, if amended</td>
                    <td class="MyContent">
                        <telerik:RadEditor runat="server" ID="txtEdittor_Narrative_707" Height="200"   BorderWidth="0"
                            MaxLength="1750" Width="355"
ToolsFile="DesktopModules/TrainingCoreBanking/BankProject/TradingFinance/BasicTools.xml" />

                        <%--<telerik:RadTextBox ID="txtNarrative_707" runat="server" Width="355" TextMode="MultiLine" Height="200" MaxLength="1750" />--%>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">72. Sender to receiver information</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation_707_1" runat="server" Width="355" MaxLength="35" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation_707_2" runat="server" Width="355" MaxLength="35" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation_707_3" runat="server" Width="355" MaxLength="35" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation_707_4" runat="server" Width="355" MaxLength="35" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation_707_5" runat="server" Width="355" MaxLength="35" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation_707_6" runat="server" Width="355" MaxLength="35" />
                    </td>
                </tr>
            </table>

        </div>
    </div>

    <div id="MT747" class="dnnClear">
        <div runat="server" id="divMT747">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">Generate MT747</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            Width="200"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="comboGenerateMT747_OnSelectedIndexChanged"
                            ID="comboGenerateMT747" runat="server"
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

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">Receiving Bank</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtReceivingBank_747" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable">20. Documentary Credit Number</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDocumentaryCreditNumber_747" runat="server" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px;">21.1 Reimb. Bank Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboReimbBankType_747" runat="server"
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
                    <td class="MyLable" style="width: 250px;">21.2 Reimb. Bank No</td>
                    <td class="MyContent"><telerik:RadTextBox ID="txtReimbBankNo_747" runat="server" AutoPostBack="True" OnTextChanged="txtReimbBankNo_747_OnTextChanged" MaxLength="34"/></td>
                    <td><asp:Label ID="lblReimbBankNo_747" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px;">21.3 Reimb. Bank Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtReimbBankName_747" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;">21.4 Reimb. Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtReimbBankAddr_747_1" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtReimbBankAddr_747_2" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtReimbBankAddr_747_3" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;">30. Date of Original Authorization</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker runat="server" ID="dteDateOfOriginalAuthorization_747" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;">31E. New Date of Expiry</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker runat="server" ID="dteNewDateOfExpiry_747" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px;">34B. New Documentary Credit Amount</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadComboBox
                            AppendDataBoundItems="True"
                            ID="comboCurrency_747" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                        </telerik:RadComboBox>

                    </td>
                    <td>
                        <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true"
                            IncrementSettings-InterceptMouseWheel="true"
                            runat="server" ID="numAmount_747" />
                    </td>

                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px;">39A. Percentage Credit Amount Tolerance</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true"
                            IncrementSettings-InterceptMouseWheel="true" Type="Percent" MaxValue="100"
                            runat="server" ID="numPercentageCreditTolerance_747_1" />
                    </td>
                    <td>
                        <telerik:RadNumericTextBox
                            IncrementSettings-InterceptArrowKeys="true" Type="Percent" MaxValue="100"
                            IncrementSettings-InterceptMouseWheel="true" runat="server"
                            ID="numPercentageCreditTolerance_747_2" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px;">39B. Maximum Credit Amount</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboMaximumCreditAmount_747" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="NOT EXCEEDING" Text="NOT EXCEEDING" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;">39C. Additional Covered</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdditionalCovered_747_1" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdditionalCovered_747_2" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdditionalCovered_747_3" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdditionalCovered_747_4" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px;">72. Sender to Receiver Information</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInfomation_747_1" runat="server" Width="355" MaxLength="35" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInfomation_747_2" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInfomation_747_3" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInfomation_747_4" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable" style="width: 250px; vertical-align: top">77A. Narrative</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtNarrative_747_1" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px; vertical-align: top"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtNarrative_747_2" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px; vertical-align: top"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtNarrative_747_3" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px; vertical-align: top"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtNarrative_747_4" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px; vertical-align: top"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtNarrative_747_5" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px; vertical-align: top"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtNarrative_747_6" runat="server" Width="355" MaxLength="35"/>
                    </td>
                </tr>

            </table>
        </div>
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
                <telerik:RadTab Text="Cable Charge" />
                <telerik:RadTab Text="Open charge" />
            </Tabs>
        </telerik:RadTabStrip>

        <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
            <telerik:RadPageView runat="server" ID="RadPageView1">
                <div runat="server" id="divCABLECHG">
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
                            <td class="MyLable">Charge Ccy</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    AutoPostBack="True"
                                    OnSelectedIndexChanged="rcbChargeCcy_OnSelectedIndexChanged"
                                    AppendDataBoundItems="True"
                                    ID="rcbChargeCcy" runat="server"
                                    MarkFirstMatch="True"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>

                    <table width="100%" cellpadding="0" cellspacing="0" id="table1" runat="server">
                        <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent">
                                <telerik:RadComboBox DropDownCssClass="KDDL"
                                    AppendDataBoundItems="True"
                                    OnItemDataBound="rcbChargeAcct_ItemDataBound"
                                    ID="rcbChargeAcct" runat="server"
                                    MarkFirstMatch="True" Width="355"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 100px;">Id
                                                </td>
                                                <td>Name
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 100px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "Id")%> 
                                                </td>
                                                <td>
                                                    <%# DataBinder.Eval(Container.DataItem, "Name")%> 
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
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

                        <tr style="display: none">
                            <td class="MyLable">Exch. Rate</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true"
                                    IncrementSettings-InterceptMouseWheel="true" runat="server" ID="tbExcheRate" Width="200px" Value="1" />
                            </td>
                        </tr>

                        <tr>
                            <td class="MyLable">Charge Amt</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true"
                                    IncrementSettings-InterceptMouseWheel="true" runat="server"
                                    ID="tbChargeAmt" AutoPostBack="False"
                                    OnTextChanged="tbChargeAmt_TextChanged" />
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

                    <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
                        <tr>
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

            <telerik:RadPageView runat="server" ID="RadPageView2">
                <div runat="server" id="divPAYMENTCHG">
                    <table width="100%" cellpadding="0" cellspacing="0" id="table2" runat="server">
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
                                <span id="spChargeCode2"></span>
                            </td>
                        </tr>

                        <tr>
                            <td class="MyLable">Charge Ccy</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    AutoPostBack="True"
                                    OnSelectedIndexChanged="rcbChargeCcy2_OnSelectedIndexChanged"
                                    AppendDataBoundItems="True"
                                    ID="rcbChargeCcy2" runat="server"
                                    MarkFirstMatch="True"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>

                    <table width="100%" cellpadding="0" cellspacing="0" id="table3" runat="server">
                        <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent">
                                <telerik:RadComboBox DropDownCssClass="KDDL"
                                    AppendDataBoundItems="True"
                                    OnItemDataBound="rcbChargeAcct_ItemDataBound"
                                    ID="rcbChargeAcct2" runat="server"
                                    MarkFirstMatch="True" Width="355"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 100px;">Id
                                                </td>
                                                <td>Name
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 100px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "Id")%> 
                                                </td>
                                                <td>
                                                    <%# DataBinder.Eval(Container.DataItem, "Name")%> 
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
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
                                    ID="tbChargeAmt2" AutoPostBack="False"
                                    OnTextChanged="tbChargeAmt2_TextChanged" />
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
                            <td>
                                <asp:Label ID="lblPartyCharged2" runat="server" /></td>
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
                                <asp:Label ID="lblChargeStatus2" runat="server" />
                            </td>
                        </tr>
                    </table>

                    <table width="100%" cellpadding="0" cellspacing="0" style="display: none">
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
                    </table>
                </div>
            </telerik:RadPageView>

            <telerik:RadPageView runat="server" ID="RadPageView3">
                <div runat="server" id="divACCPTCHG">
                    <table width="100%" cellpadding="0" cellspacing="0" id="table4" runat="server">
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
                            <td class="MyLable">Charge Ccy</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    AutoPostBack="True"
                                    OnSelectedIndexChanged="rcbChargeCcy3_OnSelectedIndexChanged"
                                    AppendDataBoundItems="True"
                                    ID="rcbChargeCcy3" runat="server"
                                    MarkFirstMatch="True"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>

                    <table width="100%" cellpadding="0" cellspacing="0" id="table5" runat="server">
                        <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent">
                                <telerik:RadComboBox DropDownCssClass="KDDL"
                                    AppendDataBoundItems="True"
                                    OnItemDataBound="rcbChargeAcct_ItemDataBound"
                                    ID="rcbChargeAcct3" runat="server"
                                    MarkFirstMatch="True" Width="355"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 100px;">Id
                                                </td>
                                                <td>Name
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 100px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "Id")%> 
                                                </td>
                                                <td>
                                                    <%# DataBinder.Eval(Container.DataItem, "Name")%> 
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
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
                                    AutoPostBack="False"
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
                            <td>
                                <asp:Label ID="lblPartyCharged3" runat="server" /></td>
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

                    <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
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
                    </table>
                </div>
            </telerik:RadPageView>

        </telerik:RadMultiPage>
    </div>

</div>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function tbExpiryDate_DateSelected(sender, eventArgs) {            
            var txtExpiryDate = $find("<%= tbExpiryDate.ClientID %>");
            var ExpiryDate = txtExpiryDate.get_selectedDate();
            //alert(ExpiryDate.getDate());
            var ContingentExpiry = new Date(ExpiryDate);
            ContingentExpiry.setDate(ExpiryDate.getDate() + 15);
            $find("<%= tbContingentExpiry.ClientID %>").set_selectedDate(ContingentExpiry);

            var txtDateOfExpiry700 = $find("<%= txtDateOfExpiry700.ClientID %>");
            if (txtDateOfExpiry700) {
                txtDateOfExpiry700.set_selectedDate(ExpiryDate);
            }

            var txtDateOfExpiry740 = $find("<%= txtDateOfExpiry740.ClientID %>");
            if (txtDateOfExpiry740) {
                txtDateOfExpiry740.set_selectedDate(ExpiryDate);
            }
            
            var dteNewDateOfExpiry_707 = $find("<%= dteNewDateOfExpiry_707.ClientID %>");
            if (dteNewDateOfExpiry_707) {
                dteNewDateOfExpiry_707.set_selectedDate(ExpiryDate);
            }

            var NewDateOfExpiry747 = $find("<%= dteNewDateOfExpiry_747.ClientID %>");
            if (NewDateOfExpiry747) {
                NewDateOfExpiry747.set_selectedDate(ExpiryDate);
            }
        }
        
        function tbIssuingDate_DateSelected(sender, eventArgs) {
            var IssuingDate = $find("<%= tbIssuingDate.ClientID %>");
            var DateOfIssue = $find("<%= dteDateOfIssue.ClientID %>");
            var ExpiryDate = $find("<%= tbExpiryDate.ClientID %>");
            var ContingentExpiry = $find("<%= tbContingentExpiry.ClientID %>");
                
            var date = IssuingDate.get_selectedDate();
            if (DateOfIssue) {
                DateOfIssue.set_selectedDate(date);    
            }

            date.setDate(date.getDate() + 15);
            if (ExpiryDate) {
                ExpiryDate.set_selectedDate(date);    
            }
            
                
            var date2 = IssuingDate.get_selectedDate();
            date2.setDate(date2.getDate() + 30);
            if (ContingentExpiry) {
                ContingentExpiry.set_selectedDate(date2);    
            }
        }

        function tbExpiryPlace_OnValueChanged(sender, eventArgs) {
            var ExpiryPlace = $find('<%=tbExpiryPlace.ClientID %>').get_value();
            
            var txtPlaceOfExpiry700 = $find("<%= txtPlaceOfExpiry700.ClientID %>");
            if (txtPlaceOfExpiry700) {
                txtPlaceOfExpiry700.set_value(ExpiryPlace);    
            }

            var txtPlaceOfExpiry740 = $find("<%= txtPlaceOfExpiry740.ClientID %>");            
            if (txtPlaceOfExpiry740) {
                txtPlaceOfExpiry740.set_value(ExpiryPlace);    
            }
        }

        function txtDraftsAt700_1_OnClientSelectedIndexChanged(sender, eventArgs) {
            var draftsAt700_1 = $find('<%=txtDraftsAt700_1.ClientID %>');
            var draftsAt740_1 = $find('<%=txtDraftsAt740_1.ClientID %>');

            draftsAt740_1.set_value(draftsAt700_1.get_value());
        }
        
        function txtDraftsAt700_2_OnClientSelectedIndexChanged(sender, eventArgs) {
            var draftsAt700_2 = $find('<%=txtDraftsAt700_2.ClientID %>');
            var draftsAt740_2 = $find('<%=txtDraftsAt740_2.ClientID %>');

            draftsAt740_2.set_value(draftsAt700_2.get_value());
        }
        function rcbCcyAmount_OnClientSelectedIndexChanged(sender, eventArgs) {
            var comboCurrency700 = $find("<%= comboCurrency700.ClientID %>"),
                comboCreditCurrency = $find("<%= comboCreditCurrency.ClientID %>"),
                comboCurrency_747 = $find("<%= comboCurrency_747.ClientID %>");
            
            if (comboCurrency700) {
                comboCurrency700.set_value($find('<%=rcbCcyAmount.ClientID %>').get_value());
                comboCurrency700.set_text($find('<%=rcbCcyAmount.ClientID %>').get_selectedItem().get_text());
            }
            
            if (comboCreditCurrency) {
                comboCreditCurrency.set_value($find('<%=rcbCcyAmount.ClientID %>').get_value());
                comboCreditCurrency.set_text($find('<%=rcbCcyAmount.ClientID %>').get_selectedItem().get_text());
            }
            
            if (comboCurrency_747) {
                comboCurrency_747.set_value($find('<%=rcbCcyAmount.ClientID %>').get_value());
                comboCurrency_747.set_text($find('<%=rcbCcyAmount.ClientID %>').get_selectedItem().get_text());
            }
        }

        function ntSoTien_OnValueChanged(sender, eventArgs) {
            var numAmount700 = $find("<%= numAmount700.ClientID %>"),
                numCreditAmount = $find("<%= numCreditAmount.ClientID %>"),
                numAmount_747 = $find("<%= numAmount_747.ClientID %>"),
                amount = $find('<%=ntSoTien.ClientID %>').get_value();
            changeImportLimitMessage();
            if (numAmount700) {
                numAmount700.set_value(amount);
            }
            
            if (numAmount_747) {
                numAmount_747.set_value(amount);
            }
            
            if (numCreditAmount) {
                numCreditAmount.set_value(amount);
            }
        }

        function numPro_OnValueChanged(sender, eventArgs) {
            changeImportLimitMessage();
        }

        function tbcrTolerance_TextChanged(sender, eventArgs) {
            var numPercentCreditAmount1 = $find("<%= numPercentCreditAmount1.ClientID %>"),
                numPercentageCreditAmountTolerance740_1 = $find("<%= numPercentageCreditAmountTolerance740_1.ClientID %>"),
                numPercentageCreditAmountTolerance_707_1 = $find("<%= numPercentageCreditAmountTolerance_707_1.ClientID %>"),
                numPercentageCreditTolerance_747_1 = $find("<%= numPercentageCreditTolerance_747_1.ClientID %>"),
                toleranceVal = $find('<%=tbcrTolerance.ClientID %>').get_value();
            
            
            if (numPercentCreditAmount1) {
                numPercentCreditAmount1.set_value(toleranceVal);
            }
            
            if (numPercentageCreditAmountTolerance740_1) {
                numPercentageCreditAmountTolerance740_1.set_value(toleranceVal);
            }
            
            if (numPercentageCreditAmountTolerance_707_1) {
                numPercentageCreditAmountTolerance_707_1.set_value(toleranceVal);
            }
            
            if (numPercentageCreditTolerance_747_1) {
                numPercentageCreditTolerance_747_1.set_value(toleranceVal);
            }
        }

        function tbdrTolerance_TextChanged(sender, eventArgs) {
            var numPercentCreditAmount2 = $find("<%= numPercentCreditAmount2.ClientID %>"),
                numPercentageCreditAmountTolerance740_2 = $find("<%= numPercentageCreditAmountTolerance740_2.ClientID %>"),
                numPercentageCreditAmountTolerance_707_2 = $find("<%= numPercentageCreditAmountTolerance_707_2.ClientID %>"),
                numPercentageCreditTolerance_747_2 = $find("<%= numPercentageCreditTolerance_747_2.ClientID %>"),
                tbdrTolerance = $find('<%=tbdrTolerance.ClientID %>').get_value();
            
            if (numPercentCreditAmount2)
            {
                numPercentCreditAmount2.set_value(tbdrTolerance);
            }
            
            if (numPercentageCreditAmountTolerance740_2) {
                numPercentageCreditAmountTolerance740_2.set_value(tbdrTolerance);
            }
            
            if (numPercentageCreditAmountTolerance_707_2) {
                numPercentageCreditAmountTolerance_707_2.set_value(tbdrTolerance);
            }

            if (numPercentageCreditTolerance_747_2) {
                numPercentageCreditTolerance_747_2.set_value(tbdrTolerance);
            }
        }
        
        $("#<%=txtCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                window.location.href = "Default.aspx?tabid=" + tabId + "&CodeID=" + $("#<%=txtCode.ClientID %>").val();
            }
        });
        
        function txtBeneficiaryBankAddr1_OnValueChanged (sender, eventArgs) {
            var txtBeneficiaryAddr700_1 = $find('<%=txtBeneficiaryAddr700_1.ClientID %>');
            var txtBeneficiaryBankAddr1 = $find('<%=txtBeneficiaryBankAddr1.ClientID %>'),
                txtBeneficiaryAddr_707_1 = $find('<%=txtBeneficiaryAddr_707_1.ClientID %>'),
                tbBeneficiaryAddr740_1 = $find('<%=tbBeneficiaryAddr740_1.ClientID %>');

            if (txtBeneficiaryAddr700_1) {
                txtBeneficiaryAddr700_1.set_value(txtBeneficiaryBankAddr1.get_value());
            }
            
            if (txtBeneficiaryAddr_707_1) {
                txtBeneficiaryAddr_707_1.set_value(txtBeneficiaryBankAddr1.get_value());
            }

            if (tbBeneficiaryAddr740_1) {
                tbBeneficiaryAddr740_1.set_value(txtBeneficiaryBankAddr1.get_value());
            }
        }
        
        function txtBeneficiaryBankAddr2_OnValueChanged (sender, eventArgs) {
            var txtBeneficiaryAddr700_2 = $find('<%=txtBeneficiaryAddr700_2.ClientID %>');
            var txtBeneficiaryBankAddr2 = $find('<%=txtBeneficiaryBankAddr2.ClientID %>'),
                txtBeneficiaryAddr_707_2 = $find('<%=txtBeneficiaryAddr_707_2.ClientID %>'),
                tbBeneficiaryAddr740_2 = $find('<%=tbBeneficiaryAddr740_2.ClientID %>');

            if (txtBeneficiaryAddr700_2) {
                txtBeneficiaryAddr700_2.set_value(txtBeneficiaryBankAddr2.get_value());
            }
            
            if (txtBeneficiaryAddr_707_2) {
                txtBeneficiaryAddr_707_2.set_value(txtBeneficiaryBankAddr2.get_value());
            }

            if (tbBeneficiaryAddr740_2) {
                tbBeneficiaryAddr740_2.set_value(txtBeneficiaryBankAddr2.get_value());
            }
        }
        
        function txtBeneficiaryBankAddr3_OnValueChanged (sender, eventArgs) {
            var txtBeneficiaryAddr700_3 = $find('<%=txtBeneficiaryAddr700_3.ClientID %>');
            var txtBeneficiaryBankAddr3 = $find('<%=txtBeneficiaryBankAddr3.ClientID %>'),
                txtBeneficiaryAddr_707_3 = $find('<%=txtBeneficiaryAddr_707_3.ClientID %>'),
                tbBeneficiaryAddr740_3 = $find('<%=tbBeneficiaryAddr740_3.ClientID %>');

            if (txtBeneficiaryAddr700_3) {
                txtBeneficiaryAddr700_3.set_value(txtBeneficiaryBankAddr3.get_value());
            }
            
            if (txtBeneficiaryAddr_707_3) {
                txtBeneficiaryAddr_707_3.set_value(txtBeneficiaryBankAddr3.get_value());
            }            

            if (tbBeneficiaryAddr740_3) {
                tbBeneficiaryAddr740_3.set_value(txtBeneficiaryBankAddr3.get_value());
            } 
        }
        
        function txtBeneficiaryBankName_OnValueChanged (sender, eventArgs) {
            var txtBeneficiaryName700 = $find('<%=txtBeneficiaryName700.ClientID %>');
            var txtBeneficiaryBankName = $find('<%=txtBeneficiaryBankName.ClientID %>'),
                txtBeneficiaryName_707 = $find('<%=txtBeneficiaryName_707.ClientID %>'),
                tbBeneficiaryName740 = $find('<%=tbBeneficiaryName740.ClientID %>');

            
            if (txtBeneficiaryName700) {
                txtBeneficiaryName700.set_value(txtBeneficiaryBankName.get_value());
            }
            
            if (txtBeneficiaryName_707) {
                txtBeneficiaryName_707.set_value(txtBeneficiaryBankName.get_value());
            }            

            if (tbBeneficiaryName740) {
                tbBeneficiaryName740.set_value(txtBeneficiaryBankName.get_value());
            } 
        }

        function tbReimbBankName_OnClientSelectedIndexChanged(sender, eventArgs) {
            var reimbBankName = $find('<%=tbReimbBankName.ClientID %>'),
                tbReimbBankName700 = $find('<%=tbReimbBankName700.ClientID %>'),
                txtReimbBankName_747 = $find('<%=txtReimbBankName_747.ClientID %>');

            if (tbReimbBankName700) {
                tbReimbBankName700.set_value(reimbBankName.get_value());
            }
            
            if (txtReimbBankName_747) {
                txtReimbBankName_747.set_value(reimbBankName.get_value());
            }
        }

        function tbReimbBankAddr1_OnClientSelectedIndexChanged(sender, eventArgs) {
            var val = $find('<%=tbReimbBankAddr1.ClientID %>'),
                tbReimbBankAddr700_1 = $find('<%=tbReimbBankAddr700_1.ClientID %>'),
                txtReimbBankAddr_747_1 = $find('<%=txtReimbBankAddr_747_1.ClientID %>');

            if (tbReimbBankAddr700_1) {
                tbReimbBankAddr700_1.set_value(val.get_value());
            }

            if (txtReimbBankAddr_747_1) {
                txtReimbBankAddr_747_1.set_value(val.get_value());
            }
        }
        
        function tbReimbBankAddr2_OnClientSelectedIndexChanged(sender, eventArgs) {
            var val = $find('<%=tbReimbBankAddr2.ClientID %>'),
                tbReimbBankAddr700_2 = $find('<%=tbReimbBankAddr700_2.ClientID %>'),
                txtReimbBankAddr_747_2 = $find('<%=txtReimbBankAddr_747_2.ClientID %>');

            if (tbReimbBankAddr700_2) {
                tbReimbBankAddr700_2.set_value(val.get_value());
            }
            if (txtReimbBankAddr_747_2) {
                txtReimbBankAddr_747_2.set_value(val.get_value());    
            }
        }
        function tbReimbBankAddr3_OnClientSelectedIndexChanged(sender, eventArgs) {
            var val = $find('<%=tbReimbBankAddr3.ClientID %>'),
                tbReimbBankAddr700_3 = $find('<%=tbReimbBankAddr700_3.ClientID %>'),
                txtReimbBankAddr_747_3 = $find('<%=txtReimbBankAddr_747_3.ClientID %>');

            if (tbReimbBankAddr700_3) {
                tbReimbBankAddr700_3.set_value(val.get_value());
            }
            if (txtReimbBankAddr_747_3) {
                txtReimbBankAddr_747_3.set_value(val.get_value());    
            }
        }
        
        function changeImportLimitMessage(){
            var lblImportLimitMessage = $('#<%=lblImportLimitMessage.ClientID%>');
            var ImportLimit = $find('<%=txtImportLimit.ClientID %>').get_value();
            var LCAmount = $find('<%=ntSoTien.ClientID %>').get_value();
            if (ImportLimit == null || ImportLimit == 0){
                lblImportLimitMessage.text('');
                return;
            }
            if (LCAmount == null) LCAmount = 0;
            var ProvPercent = $find('<%=numPro.ClientID %>').get_value();
            if (ProvPercent != null){
                LCAmount = LCAmount + LCAmount * (ProvPercent/100);
            }
            ImportLimit = ImportLimit - LCAmount;
            lblImportLimitMessage.text('Available Import Limit : ' + ImportLimit);
        }
        function rcbApplicantID_OnClientSelectedIndexChanged() {
            var rcbApplicantID = $find('<%=rcbApplicantID.ClientID %>').get_selectedItem(),                
                
                tbApplicantName = $find('<%=tbApplicantName.ClientID %>'),
                tbApplicantAddr1 = $find('<%=tbApplicantAddr1.ClientID %>'),
                tbApplicantAddr2 = $find('<%=tbApplicantAddr2.ClientID %>'),
                tbApplicantAddr3 = $find('<%=tbApplicantAddr3.ClientID %>'),
                txtImportLimit = $find('<%=txtImportLimit.ClientID %>'),
                lblImportLimitMessage = $('#<%=lblImportLimitMessage.ClientID%>'),
                
                tbApplicantNo700 = $find('<%=tbApplicantNo700.ClientID %>'),
                tbApplicantName700 = $find('<%=tbApplicantName700.ClientID %>'),
                tbApplicantAddr700_1 = $find('<%=tbApplicantAddr700_1.ClientID %>'),
                tbApplicantAddr700_2 = $find('<%=tbApplicantAddr700_2.ClientID %>'),
                tbApplicantAddr700_3 = $find('<%=tbApplicantAddr700_3.ClientID %>');                       
            
            if (tbApplicantName) {
                tbApplicantName.set_value(rcbApplicantID.get_attributes().getAttribute("CustomerName"));
                tbApplicantAddr1.set_value(rcbApplicantID.get_attributes().getAttribute("Address"));
                tbApplicantAddr2.set_value(rcbApplicantID.get_attributes().getAttribute("City"));
                tbApplicantAddr3.set_value(rcbApplicantID.get_attributes().getAttribute("Country"));
                txtImportLimit.set_value(rcbApplicantID.get_attributes().getAttribute("ImportLimitAmt"));
            }
            else{
                tbApplicantName.set_value('');
                tbApplicantAddr1.set_value('');
                tbApplicantAddr2.set_value('');
                tbApplicantAddr3.set_value('');
                txtImportLimit.set_value(0);
            }
            changeImportLimitMessage();

            if (tbApplicantNo700) {
                tbApplicantNo700.set_value(rcbApplicantID.get_value());
                tbApplicantName700.set_value(rcbApplicantID.get_attributes().getAttribute("CustomerName"));
                tbApplicantAddr700_1.set_value(rcbApplicantID.get_attributes().getAttribute("Address"));
                tbApplicantAddr700_2.set_value(rcbApplicantID.get_attributes().getAttribute("City"));
                tbApplicantAddr700_3.set_value(rcbApplicantID.get_attributes().getAttribute("Country"));
            }
        }

        function tbApplicantName_OnValueChanged () {
            var val = $find('<%=tbApplicantName.ClientID %>').get_value(),
                tbApplicantName700 = $find('<%=tbApplicantName700.ClientID %>');

            if (tbApplicantName700) {
                tbApplicantName700.set_value(val);
            }
        }

        function tbApplicantAddr1_OnValueChanged () {
            var val = $find('<%=tbApplicantAddr1.ClientID %>').get_value(),
                tbApplicantAddr700_1 = $find('<%=tbApplicantAddr700_1.ClientID %>');

            if (tbApplicantAddr700_1) {
                tbApplicantAddr700_1.set_value(val);
            }
        }

        function tbApplicantAddr2_OnValueChanged () {
            var val = $find('<%=tbApplicantAddr2.ClientID %>').get_value(),
                tbApplicantAddr700_2 = $find('<%=tbApplicantAddr700_2.ClientID %>');

            if (tbApplicantAddr700_2) {
                tbApplicantAddr700_2.set_value(val);
            }
        }

        function tbApplicantAddr3_OnValueChanged () {
            var val = $find('<%=tbApplicantAddr3.ClientID %>').get_value(),
                tbApplicantAddr700_3 = $find('<%=tbApplicantAddr700_3.ClientID %>');

            if (tbApplicantAddr700_3) {
                tbApplicantAddr700_3.set_value(val);
            }
        }

        function tbAdviseThruName_OnClientSelectedIndexChanged () {
            var txtAdviseThroughBankName700 = $find('<%=txtAdviseThroughBankName700.ClientID %>'),
                tbAdviseThruName = $find('<%=tbAdviseThruName.ClientID %>');

            if (txtAdviseThroughBankName700) {
                txtAdviseThroughBankName700.set_value(tbAdviseThruName.get_value());
            }
        }

        function tbAdviseThruAddr1_OnClientSelectedIndexChanged () {
            var txtAdviseThroughBankAddr700_1 = $find('<%=txtAdviseThroughBankAddr700_1.ClientID %>'),
                tbAdviseThruAddr1 = $find('<%=tbAdviseThruAddr1.ClientID %>');

            if (txtAdviseThroughBankAddr700_1) {
                txtAdviseThroughBankAddr700_1.set_value(tbAdviseThruAddr1.get_value());
            }
        }

        function tbAdviseThruAddr2_OnClientSelectedIndexChanged () {
            var txtAdviseThroughBankAddr700_2 = $find('<%=txtAdviseThroughBankAddr700_2.ClientID %>'),
                 tbAdviseThruAddr2 = $find('<%= tbAdviseThruAddr2.ClientID %>');

            if (txtAdviseThroughBankAddr700_2) {
                txtAdviseThroughBankAddr700_2.set_value(tbAdviseThruAddr2.get_value());
            }
        }

        function tbAdviseThruAddr3_OnClientSelectedIndexChanged () {
            var txtAdviseThroughBankAddr700_3 = $find('<%=txtAdviseThroughBankAddr700_3.ClientID %>'),
                 tbAdviseThruAddr3 = $find('<%= tbAdviseThruAddr3.ClientID %>');

            if (txtAdviseThroughBankAddr700_3) {
                txtAdviseThroughBankAddr700_3.set_value(tbAdviseThruAddr3.get_value());
            }
        }

        function comboReimbBankType_OnClientSelectedIndexChanged () {
            var comboReimbBankType = $find('<%=comboReimbBankType.ClientID %>'),
                comboReimbBankType700 = $find('<%=comboReimbBankType700.ClientID %>'),
                comboReimbBankType_747 = $find('<%=comboReimbBankType_747.ClientID %>');                
            //13.1+14.1: khi chuyển từ type A sang D thì 13.2+14.2: phải xóa giá trị hiện có và làm mờ
            if (comboReimbBankType.get_selectedItem().get_text() == 'D'){
                $find('<%=txtReimbBankNo.ClientID %>').set_value('');
            }
            if (comboReimbBankType700) {
                comboReimbBankType700.set_value(comboReimbBankType.get_value());
                comboReimbBankType700.set_text(comboReimbBankType.get_selectedItem().get_text());

                $find('<%=txtReimbBankNo700.ClientID %>').set_value('');
                $find('<%=tbReimbBankName700.ClientID %>').set_value('');
                $find('<%=tbReimbBankAddr700_1.ClientID %>').set_value('');
                $find('<%=tbReimbBankAddr700_2.ClientID %>').set_value('');
                $find('<%=tbReimbBankAddr700_3.ClientID %>').set_value('');
            }

            if (comboReimbBankType_747) {
                comboReimbBankType_747.set_value(comboReimbBankType.get_value());
                comboReimbBankType_747.set_text(comboReimbBankType.get_selectedItem().get_text());

                $find('<%=txtReimbBankNo_747.ClientID %>').set_value('');
                $find('<%=txtReimbBankName_747.ClientID %>').set_value('');
                $find('<%=txtReimbBankAddr_747_1.ClientID %>').set_value('');
                $find('<%=txtReimbBankAddr_747_2.ClientID %>').set_value('');
                $find('<%=txtReimbBankAddr_747_3.ClientID %>').set_value('');
            }
        }

        function rcbAvailableWithType_OnSelectedIndexChanged (sender, eventArgs) {
            var rcbAvailableWithType = $find('<%=rcbAvailableWithType.ClientID %>'),
                rcbAvailableWithType740 = $find('<%=rcbAvailableWithType.ClientID %>');

            if (rcbAvailableWithType740) {
                rcbAvailableWithType740.set_value(rcbAvailableWithType.get_value());
                rcbAvailableWithType740.set_text(rcbAvailableWithType.get_value());
            }
        }

        function tbAvailableWithName_OnValueChanged (sender, eventArgs) {
            var tbAvailableWithName = $find('<%=tbAvailableWithName.ClientID %>'),
                tbAvailableWithName740 = $find('<%=tbAvailableWithName740.ClientID %>');

            
            if (tbAvailableWithName740) {
                tbAvailableWithName740.set_value(tbAvailableWithName.get_value());
            }
        }

        function tbAvailableWithAddr1_OnValueChanged (sender, eventArgs) {
            var tbAvailableWithAddr1 = $find('<%=tbAvailableWithAddr1.ClientID %>'),
                tbAvailableWithAddr740_1 = $find('<%=tbAvailableWithAddr740_1.ClientID %>');

            
            if (tbAvailableWithAddr740_1) {
                tbAvailableWithAddr740_1.set_value(tbAvailableWithAddr1.get_value());
            }
        }

        function tbAvailableWithAddr2_OnValueChanged (sender, eventArgs) {
            var tbAvailableWithAddr2 = $find('<%=tbAvailableWithAddr2.ClientID %>'),
                tbAvailableWithAddr740_2 = $find('<%=tbAvailableWithAddr740_2.ClientID %>');

            
            if (tbAvailableWithAddr740_2) {
                tbAvailableWithAddr740_2.set_value(tbAvailableWithAddr2.get_value());
            }
        }

        function tbAvailableWithAddr3_OnValueChanged (sender, eventArgs) {
            var tbAvailableWithAddr3 = $find('<%=tbAvailableWithAddr3.ClientID %>'),
                tbAvailableWithAddr740_3 = $find('<%=tbAvailableWithAddr740_3.ClientID %>');

            
            if (tbAvailableWithAddr740_3) {
                tbAvailableWithAddr740_3.set_value(tbAvailableWithAddr3.get_value());
            }
        }

    </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"><img src="icons/bank/ajax-loader-16x16.gif" /></telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbLCType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblLCType" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="rcbApplicantID">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbApplicantName" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantAddr1" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantAddr2" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantAddr3" />

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

        <telerik:AjaxSetting AjaxControlID="rcCommodity">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblCommodity" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="rcbPartyCharged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblPartyCharged" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="rcbPartyCharged2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblPartyCharged2" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="rcbPartyCharged3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblPartyCharged3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="tbChargeAmt">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode" />
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

        <telerik:AjaxSetting AjaxControlID="tbReimbBankName">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankName700" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="tbReimbBankAddr1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr700_1" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="tbReimbBankAddr2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr700_2" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="tbReimbBankAddr3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr700_3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="rcbAvailWithNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbAvailWithName" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="rcbApplicantBankType700">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbApplicantNo700" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantName700" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantAddr700_1" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantAddr700_2" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantAddr700_3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="txtAvailableWithNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAvailableWithMessage" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithName" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr1" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr2" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr3" />

                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithNo740" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithName740" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr740_1" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr740_2" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr740_3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="rcbAvailableWithType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtAvailableWithNo" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithName" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr1" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr2" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr3" />

                <telerik:AjaxUpdatedControl ControlID="rcbAvailableWithType740" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithNo740" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithName740" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr740_1" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr740_2" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr740_3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="rcbAvailableWithType740">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithNo740" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithName740" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr740_1" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr740_2" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr740_3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="tbAvailableWithNo740">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAvailableWithNoError740" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithName740" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="rcbBeneficiaryType740">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbBeneficiaryName740" />
                <telerik:AjaxUpdatedControl ControlID="tbBeneficiaryNo740" />
                <telerik:AjaxUpdatedControl ControlID="tbBeneficiaryAddr740_1" />
                <telerik:AjaxUpdatedControl ControlID="tbBeneficiaryAddr740_2" />
                <telerik:AjaxUpdatedControl ControlID="tbBeneficiaryAddr740_3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="tbBeneficiaryNo740">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbBeneficiaryName740" />
                <telerik:AjaxUpdatedControl ControlID="lblBeneficiaryError740" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="txtRemittingBankNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblReceivingBankNoError" />
                <telerik:AjaxUpdatedControl ControlID="lblReceivingBankName" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="ntSoTien">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="numAmount700" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="comboBeneficiaryType700">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryNo700" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryName700" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryAddr700_1" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryAddr700_2" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryAddr700_3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="txtBeneficiaryNo700">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblBeneficiaryNo700Error" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryName700" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="comGenerate">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtRemittingBankNo" />
                <telerik:AjaxUpdatedControl ControlID="txtPlaceOfExpiry740" />

                <telerik:AjaxUpdatedControl ControlID="rcbBeneficiaryType740" />
                <telerik:AjaxUpdatedControl ControlID="tbBeneficiaryNo740" />
                <telerik:AjaxUpdatedControl ControlID="tbBeneficiaryName740" />
                <telerik:AjaxUpdatedControl ControlID="tbBeneficiaryAddr740_1" />
                <telerik:AjaxUpdatedControl ControlID="tbBeneficiaryAddr740_2" />
                <telerik:AjaxUpdatedControl ControlID="tbBeneficiaryAddr740_3" />

                <telerik:AjaxUpdatedControl ControlID="numCreditAmount" />
                <telerik:AjaxUpdatedControl ControlID="comboCreditCurrency" />
                <telerik:AjaxUpdatedControl ControlID="rcbAvailableWithType740" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithNo740" />
                <telerik:AjaxUpdatedControl ControlID="comboReimbursingBankChange" />
                <telerik:AjaxUpdatedControl ControlID="txtDateOfExpiry740" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderToReceiverInformation740_1" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderToReceiverInformation740_2" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderToReceiverInformation740_3" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderToReceiverInformation740_4" />
                <telerik:AjaxUpdatedControl ControlID="numPercentageCreditAmountTolerance740_1" />
                <telerik:AjaxUpdatedControl ControlID="numPercentageCreditAmountTolerance740_2" />

                <telerik:AjaxUpdatedControl ControlID="txtDraftsAt740_1" />
                <telerik:AjaxUpdatedControl ControlID="txtDraftsAt740_2" />

            </UpdatedControls>
        </telerik:AjaxSetting>

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

        <telerik:AjaxSetting AjaxControlID="comboReimbBankType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtReimbBankNo" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankName" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="rcbAdviseThruType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThruNo" />
                <telerik:AjaxUpdatedControl ControlID="tbAdviseThruName" />
                <telerik:AjaxUpdatedControl ControlID="tbAdviseThruAddr1" />
                <telerik:AjaxUpdatedControl ControlID="tbAdviseThruAddr2" />
                <telerik:AjaxUpdatedControl ControlID="tbAdviseThruAddr3" />

                <telerik:AjaxUpdatedControl ControlID="comboAdviseThroughBankType700" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankNo700" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankName700" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankAddr700_1" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankAddr700_2" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankAddr700_3" />
            </UpdatedControls>
        </telerik:AjaxSetting>


        <%--<telerik:AjaxSetting AjaxControlID="comboAvailableRule">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblApplicableRule740" />
            </UpdatedControls>
        </telerik:AjaxSetting>--%>

        <telerik:AjaxSetting AjaxControlID="comboDraweeCusType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtDraweeCusNo700" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeCusName" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="txtDraweeCusNo700">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblDraweeCusNo700Message" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeCusName" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="comboGenerateMT747">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="dteNewDateOfExpiry_747" />
                <telerik:AjaxUpdatedControl ControlID="numAmount_747" />
                <telerik:AjaxUpdatedControl ControlID="numPercentageCreditTolerance_747_1" />
                <telerik:AjaxUpdatedControl ControlID="numPercentageCreditTolerance_747_2" />
                <telerik:AjaxUpdatedControl ControlID="comboMaximumCreditAmount_747" />
                <telerik:AjaxUpdatedControl ControlID="txtAdditionalCovered_747_1" />
                <telerik:AjaxUpdatedControl ControlID="txtAdditionalCovered_747_2" />
                <telerik:AjaxUpdatedControl ControlID="txtAdditionalCovered_747_3" />
                <telerik:AjaxUpdatedControl ControlID="txtAdditionalCovered_747_4" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderToReceiverInfomation_747_1" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderToReceiverInfomation_747_2" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderToReceiverInfomation_747_3" />
                <telerik:AjaxUpdatedControl ControlID="txtSenderToReceiverInfomation_747_4" />
                <telerik:AjaxUpdatedControl ControlID="txtNarrative_747_1" />
                <telerik:AjaxUpdatedControl ControlID="txtNarrative_747_2" />
                <telerik:AjaxUpdatedControl ControlID="txtNarrative_747_3" />
                <telerik:AjaxUpdatedControl ControlID="txtNarrative_747_4" />
                <telerik:AjaxUpdatedControl ControlID="txtNarrative_747_5" />
                <telerik:AjaxUpdatedControl ControlID="txtNarrative_747_6" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtBeneficiaryNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblBeneficiaryBankMessage" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryBankName" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryBankAddr3" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryNo700" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryName700" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryAddr700_1" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryAddr700_2" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryAddr700_3" />
                <telerik:AjaxUpdatedControl ControlID="tbBeneficiaryNo740" />
                <telerik:AjaxUpdatedControl ControlID="tbBeneficiaryName740" />
                <telerik:AjaxUpdatedControl ControlID="tbBeneficiaryAddr740_1" />
                <telerik:AjaxUpdatedControl ControlID="tbBeneficiaryAddr740_2" />
                <telerik:AjaxUpdatedControl ControlID="tbBeneficiaryAddr740_3" />
                <%--<telerik:AjaxUpdatedControl ControlID="txtBeneficiaryNo_707" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryName_707" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryAddr_707_1" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryAddr_707_2" />
                <telerik:AjaxUpdatedControl ControlID="txtBeneficiaryAddr_707_3" />--%>
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="txtAdviseBankNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAdviseBankMessage" />
                <telerik:AjaxUpdatedControl ControlID="tbAdviseBankName" />
                <telerik:AjaxUpdatedControl ControlID="tbAdviseBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="tbAdviseBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="tbAdviseBankAddr3" />
                <telerik:AjaxUpdatedControl ControlID="txtRevivingBank700" />
                <telerik:AjaxUpdatedControl ControlID="tbRevivingBankName" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="txtReimbBankNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblReimbBankMessage" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankName" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr3" />

                <telerik:AjaxUpdatedControl ControlID="txtReimbBankNo700" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankName700" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr700_1" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr700_2" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr700_3" />

                <telerik:AjaxUpdatedControl ControlID="txtRemittingBankNo" />
                <telerik:AjaxUpdatedControl ControlID="lblReceivingBankNoError" />
                <telerik:AjaxUpdatedControl ControlID="lblReceivingBankName" />

                <%--<telerik:AjaxUpdatedControl ControlID="txtReceivingBank_747" />
                <telerik:AjaxUpdatedControl ControlID="txtReimbBankNo_747" />
                <telerik:AjaxUpdatedControl ControlID="txtReimbBankName_747" />
                <telerik:AjaxUpdatedControl ControlID="txtReimbBankAddr_747_1" />
                <telerik:AjaxUpdatedControl ControlID="txtReimbBankAddr_747_2" />
                <telerik:AjaxUpdatedControl ControlID="txtReimbBankAddr_747_3" />--%>
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="txtAdviseThruNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAdviseThruMessage" />
                <telerik:AjaxUpdatedControl ControlID="tbAdviseThruName" />
                <telerik:AjaxUpdatedControl ControlID="tbAdviseThruAddr1" />
                <telerik:AjaxUpdatedControl ControlID="tbAdviseThruAddr2" />
                <telerik:AjaxUpdatedControl ControlID="tbAdviseThruAddr3" />

                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankNo700" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankName700" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankAddr700_1" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankAddr700_2" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankAddr700_3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="txtReimbBankNo700">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblReimbBankNo700Message" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankName700" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr700_1" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr700_2" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr700_3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="txtAdviseThroughBankNo700">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAdviseThroughBankNo700" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankName700" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankAddr700_1" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankAddr700_2" />
                <telerik:AjaxUpdatedControl ControlID="txtAdviseThroughBankAddr700_3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="txtReimbBankNo_747">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblReimbBankNo_747" />
                <telerik:AjaxUpdatedControl ControlID="txtReimbBankName_747" />
                <telerik:AjaxUpdatedControl ControlID="txtReimbBankAddr_747_1" />
                <telerik:AjaxUpdatedControl ControlID="txtReimbBankAddr_747_2" />
                <telerik:AjaxUpdatedControl ControlID="txtReimbBankAddr_747_3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<div style="visibility: hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnIssueLC_MT700Report" runat="server" OnClick="btnIssueLC_MT700Report_Click" Text="Search" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnIssueLC_MT740Report" runat="server" OnClick="btnIssueLC_MT740Report_Click" Text="Search" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnIssueLC_VATReport" runat="server" OnClick="btnIssueLC_VATReport_Click" Text="Search" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnIssueLC_NHapNgoaiBangReport" runat="server" OnClick="btnIssueLC_NHapNgoaiBangReport_Click" Text="Search" /></div>

<div style="visibility: hidden;">
    <asp:Button ID="btnAmentLCReport_XuatNgoaiBang" runat="server" OnClick="btnAmentLCReport_XuatNgoaiBang_Click" Text="Search" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnAmentLCReport_NhapNgoaiBang" runat="server" OnClick="btnAmentLCReport_NhapNgoaiBang_Click" Text="Search" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnAmentLCReport_VAT" runat="server" OnClick="btnAmentLCReport_VAT_Click" Text="Search" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnAmentLCReport_MT707" runat="server" OnClick="btnAmentLCReport_MT707_Click"  Text="Search" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnAmentLCReport_MT747" runat="server" OnClick="btnAmentLCReport_MT747_Click" Text="Search" /></div>

<div style="visibility: hidden;">
    <asp:Button ID="btnCancelLC_XUATNGOAIBANG" runat="server" OnClick="btnCancelLC_XUATNGOAIBANG_Click" Text="Search" /></div>
<div style="visibility: hidden;">
    <asp:Button ID="btnCancelLC_VAT" runat="server" OnClick="btnCancelLC_VAT_Click" Text="Search" /></div>
