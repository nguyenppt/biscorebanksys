<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IssueLC.ascx.cs" Inherits="BankProject.TradingFinance.Import.DocumentaryCredit.IssueLC" %>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
          </telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />

<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <script type="text/javascript">
        var amount =  parseFloat(<%= Amount %>);
        var amount_Old = parseFloat(<%= Amount_Old %>);
        var chargeAmount = parseFloat(<%= TotalChargeAmt %>);
        var b4_AUT_Amount = parseFloat(<%= B4_AUT_Amount %>);
        
        var tabId = <%= TabId %>;
        var receivingBank_700 = '<%= ReceivingBank_700 %>';
        var receivingBank_740 = '<%= ReceivingBank_740 %>';
        var generateMT740 = '<%= Generate740 %>';
        var waiveCharges = '<%= WaiveCharges %>';
        
        jQuery(function ($) {
            $('#tabs-demo').dnnTabs();
        });

        function RadToolBar1_OnClientButtonClicking(sender, args) {
            var button = args.get_item();
            
            if (button.get_commandName() == "print") {
                args.set_cancel(true);

                switch (tabId) {
                    case 92:
                        
                        if (receivingBank_700) {
                            radconfirm("Do you want to download MT700 file?", confirmCallbackFunction_IssueLC_MT700, 370, 150, null, 'Download');
                        } else if (generateMT740 === 'YES') {
                            radconfirm("Do you want to download MT740 file?", confirmCallbackFunction_IssueLC_MT740, 370, 150, null, 'Download');
                        } else {
                            radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file?", confirmCallbackFunction_IssueLC_NhapNgoaiBang, 420, 150, null, 'Download');
                        }
                        break;
                        
                    case 204: // Amend LC
                        showPhieuNhap_Xuat();
                        break;
                        
                    case 205: // Cancel LC
                        radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file?", confirmCallbackFunction_CancelLC_XuatNgoaiBang, 420, 150, null, 'Download');
                        break;
                }
            }
        }
        
        function showPhieuNhap_Xuat() {
            // Neu amount > amount_old -> tu chinh tang tienb, xuat phieu [nhap ngoai bang]
            //amount < amount_Old -> tu chinh giam tien,xuat phieu [xuat phieu ngoai bang]
            // amount = amoun_old -> ko xuat phieu xuat nhap ngoai bang
            if (amount_Old == amount) {
                if (waiveCharges === 'NO' && chargeAmount > 0) {
                    radconfirm("Do you want to download VAT file?", confirmCallbackFunction_VAT_Amendments, 350, 150, null, 'Download');
                }
            } else if (amount_Old > 0 && amount > amount_Old) {//b4_AUT_Amount
                radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file?", confirmCallbackFunction_NhapNgoaiBang_Amendments, 420, 150, null, 'Download');
            } else if (amount_Old > 0 && amount < amount_Old) {
                radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file?", confirmCallbackFunction_XuatNgoaiBang_Amendments, 420, 150, null, 'Download');
            } else if (amount_Old === 0 && amount < b4_AUT_Amount) {
                radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file?", confirmCallbackFunction_NhapNgoaiBang_Amendments, 420, 150, null, 'Download');
            } else if (amount_Old === 0 && amount > b4_AUT_Amount) {
                radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file?", confirmCallbackFunction_XuatNgoaiBang_Amendments, 420, 150, null, 'Download');
            } else if (waiveCharges === 'NO' && chargeAmount > 0) {
                radconfirm("Do you want to download VAT file?", confirmCallbackFunction_VAT_Amendments, 350, 150, null, 'Download');
            }
        }

        function confirmCallbackFunction_IssueLC_MT700(result) {
            if (result) {
                $("#<%=btnIssueLC_MT700Report.ClientID %>").click();
            }
            if (generateMT740 === 'YES') {
                radconfirm("Do you want to download MT740 file?", confirmCallbackFunction_IssueLC_MT740, 370, 150, null, 'Download');
            } else {
                radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file?", confirmCallbackFunction_IssueLC_NhapNgoaiBang, 420, 150, null, 'Download');
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
            
            if (waiveCharges === 'NO' && chargeAmount > 0) {
                radconfirm("Do you want to download VAT file?", confirmCallbackFunction_VAT_Amendments, 420, 150, null, 'Download');
            }
        }
        
        function confirmCallbackFunction_NhapNgoaiBang_Amendments(result) {
            if (result) {
                $("#<%=btnAmentLCReport_NhapNgoaiBang.ClientID %>").click();
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
    EnableRoundedCorners="true" EnableShadows="true" width="100%" OnButtonClick="RadToolBar1_ButtonClick" >
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

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width:200px; padding:5px 0 5px 20px;"><asp:TextBox ID="txtCode" runat="server" Width="200" />&nbsp;<asp:Label ID="lblError" runat="server" ForeColor="red" /></td>
    </tr>
</table>

<div class="dnnForm" id="tabs-demo">
    <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
        <ul class="dnnAdminTabNav">
            <li><a href="#Main">Main</a></li>
            <% if (TabId == 92) %>
                <%{ %>
                <li><a href="#MT700">MT700</a></li>
                <li><a href="#MT740">MT740</a></li>
            <% }
                else if (TabId == 204) %>
                    <%{%>
                        <li><a href="#MT707">MT707</a></li>
                        <li><a href="#MT747">MT747</a></li>
            <% } %>
            <li><a href="#Charges">Charges</a></li>
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

                <td class="MyContent" style="width:355px;">
                    <telerik:RadComboBox  
                    AppendDataBoundItems="True"
                    DropDownCssClass="KDDL"
                    ID="rcbLCType" Runat="server"  width="355"
                    MarkFirstMatch="True"
                    OnItemDataBound="rcbLCType_ItemDataBound"  
                    AutoPostBack="true" 
                    OnSelectedIndexChanged="rcbLCType_SelectIndexChange"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <HeaderTemplate>
                        <table style="width:305px" cellpadding="0" cellspacing="0"> 
                            <tr> 
                                <td style="width:60px;"> 
                                LC Type 
                                </td> 
                                <td style="width:200px;"> 
                                Description
                                </td> 
                                <td > 
                                Category
                                </td> 
                            </tr> 
                        </table> 
                    </HeaderTemplate>
                    <ItemTemplate>
                    <table style="width:305px"  cellpadding="0" cellspacing="0"> 
                        <tr> 
                            <td style="width:60px;"> 
                            <%# DataBinder.Eval(Container.DataItem, "LCTYPE")%> 
                            </td> 
                            <td style="width:200px;"> 
                            <%# DataBinder.Eval(Container.DataItem, "Description")%> 
                            </td> 
                            <td > 
                            <%# DataBinder.Eval(Container.DataItem, "Category")%> 
                            </td> 
                        </tr> 
                    </table> 
                </ItemTemplate>
                </telerik:RadComboBox>
            </td>
                <td><asp:Label ID="lblLCType" runat="server" /></td>
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
                        ID="rcbApplicantID" Runat="server"
                        MarkFirstMatch="True"
                        Width="355"
                        Height="150"
                        AllowCustomText="false" >
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                    </telerik:RadComboBox>
                 </td>
            </tr>
            <tr>
                <td class="MyLable">2.2 Applicant Name</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbApplicantName" runat="server" Width="355" />
                </td>
            </tr>
             <tr>
                <td class="MyLable">2.3 Applicant Addr.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbApplicantAddr1" runat="server" Width="355" />
                </td>
            </tr>
            
             <tr>
                <td class="MyLable">2.4 Applicant Addr.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbApplicantAddr2" runat="server" Width="355" />
                </td>
            </tr>
            
             <tr>
                <td class="MyLable">2.5 Applicant Addr.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbApplicantAddr3" runat="server" Width="355" />
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr hidden="hidden">
                <td class="MyLable">Applicant Acct</td>
                <td class="MyContent">
                    <telerik:RadComboBox 
                        ID="rcbApplicantAcct" Runat="server" 
                        MarkFirstMatch="True" Width="150" 
                        AllowCustomText="false" >
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">3. Ccy, Amount <span class="Required">(*)</span>
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
                        onclientselectedindexchanged="rcbCcyAmount_OnClientSelectedIndexChanged"
                        AppendDataBoundItems="True"
                        ID="rcbCcyAmount" Runat="server" 
                        MarkFirstMatch="True"
                        AllowCustomText="false" >
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                    </telerik:RadComboBox><span class="Required">(*)</span>
                    <telerik:radnumerictextbox incrementsettings-interceptarrowkeys="true" 
                        incrementsettings-interceptmousewheel="true"  
                        runat="server" id="ntSoTien" AutoPostBack="False" 
                        OnTextChanged="ntSoTien_TextChanged"
                        ClientEvents-OnValueChanged ="ntSoTien_OnValueChanged" />
                    <asp:RequiredFieldValidator 
                        runat="server" Display="None"
                        ID="RequiredFieldValidator3" 
                        ControlToValidate="ntSoTien" 
                        ValidationGroup="Commit"
                        InitialValue="" 
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
                <td class="MyLable">4. Cr.Tolerance</td>
                <td class="MyContent">
                <telerik:radnumerictextbox incrementsettings-interceptarrowkeys="true" 
                    incrementsettings-interceptmousewheel="true"  Type="Percent" MaxValue="100"
                    ClientEvents-OnValueChanged="tbcrTolerance_TextChanged"   
                    runat="server" id="tbcrTolerance" Width="80" />
                    51 Dr.Tolerance <telerik:radnumerictextbox 
                        incrementsettings-interceptarrowkeys="true" Type="Percent" MaxValue="100"
                        incrementsettings-interceptmousewheel="true"  runat="server"  
                        ClientEvents-OnValueChanged="tbdrTolerance_TextChanged"  
                        id="tbdrTolerance" Width="120"/>
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
                        <telerik:RadTextBox ID="tbExpiryPlace" Width="355" Runat="server" 
                            ClientEvents-OnValueChanged="rcbExpiryPlace_OnClientSelectedIndexChanged" >
                        </telerik:RadTextBox>
                    </td>
                </tr>
            </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">8. Contingent Expiry</td>
                <td Width="150" class="MyContent">
                    <telerik:RadDatePicker runat="server" MinDate="1/1/1900" ID="tbContingentExpiry">
                    </telerik:RadDatePicker>
                </td>
                <td>30 Archve Date(Sys.Field)</td>
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
                                    Runat="server"
                                    MarkFirstMatch="True" 
                                    Width="355" 
                                    Height="150px" 
                                    AllowCustomText="false" >
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
                <td class="MyLable">10. Contact No</td>
                <td class="MyContent">
                    <asp:TextBox ID="tbContactNo" runat="server" Width="355"/>
                </td>
            </tr>
        </table>

        <fieldset>
            <legend>
                <div style="font-weight:bold; text-transform:uppercase;">Beneficiary Details</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display: none;">
                    <td class="MyLable" >Beneficiary Type</td>
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
                    <td class="MyLable">11.1 Beneficiary No.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryNo" runat="server" Width="355" ClientEvents-OnValueChanged="txtBeneficiaryNo_OnValueChanged"/>
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">11.2 Beneficiary Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryBankName" runat="server" Width="355" ClientEvents-OnValueChanged="txtBeneficiaryBankName_OnValueChanged"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">11.3 Beneficiary Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryBankAddr1" runat="server" Width="355" ClientEvents-OnValueChanged="txtBeneficiaryBankAddr1_OnValueChanged"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">11.4 Beneficiary Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryBankAddr2" runat="server" Width="355" ClientEvents-OnValueChanged="txtBeneficiaryBankAddr2_OnValueChanged" />
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">11.5 Beneficiary Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryBankAddr3" runat="server" Width="355" ClientEvents-OnValueChanged="txtBeneficiaryBankAddr3_OnValueChanged" />
                    </td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight:bold; text-transform:uppercase;">Advising/Reimbursing Bank Details</div>
            </legend>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">12.1 Advise Bank No</td>
                    <td class="MyContent" >
                        <telerik:RadComboBox 
                            ID="rcbAdviseBankNo"
                            Runat="server" 
                            AppendDataBoundItems="true"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="rcbAdviseBankNo_OnSelectedIndexChanged"
                            OnItemDataBound="SwiftCode_ItemDataBound"  
                            MarkFirstMatch="True"
                            Width="355"
                            Height="150"
                            AllowCustomText="false" >
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">12.2 Advise Bank Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAdviseBankName" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">12.3 Advise Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAdviseBankAddr1" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">12.4 Advise Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAdviseBankAddr2" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">12.5 Advise Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAdviseBankAddr3" runat="server" Width="355"/>
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">13.1 Reimb. Bank Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                            AutoPostBack="True"
                            OnSelectedIndexChanged="comboReimbBankType_OnSelectedIndexChanged"
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

            <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">13.2 Reimb. Bank No</td>
                <td class="MyContent">
                    <telerik:RadComboBox 
                        ID="rcbReimbBankNo" 
                        Runat="server" 
                        AppendDataBoundItems="true"
                        OnItemDataBound="SwiftCode_ItemDataBound"  
                        MarkFirstMatch="True"
                        Width="355"
                        Height="150"
                        AllowCustomText="false" OnClientSelectedIndexChanged="rcbReimbBankNo_OnClientSelectedIndexChanged">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None"  />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
                
            <tr >
                <td class="MyLable">13.3 Reimb. Bank Name</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbReimbBankName" runat="server" Width="355"
                        AutoPostBack="False" OnTextChanged="tbReimbBankName_tbReimbBankName"
                        ClientEvents-OnValueChanged="tbReimbBankName_OnClientSelectedIndexChanged" />
                </td>
            </tr>
                
            <tr>
                <td class="MyLable">13.4 Reimb. Bank Addr.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbReimbBankAddr1" runat="server" Width="355"
                        AutoPostBack="False" OnTextChanged="tbReimbBankAddr1_OnTextChanged"
                        ClientEvents-OnValueChanged="tbReimbBankAddr1_OnClientSelectedIndexChanged"/>
                </td>
            </tr>
                
            <tr>
                <td class="MyLable">13.5 Reimb. Bank Addr.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbReimbBankAddr2" runat="server" Width="355"
                        AutoPostBack="False" OnTextChanged="tbReimbBankAddr2_OnTextChanged"
                        ClientEvents-OnValueChanged="tbReimbBankAddr2_OnClientSelectedIndexChanged"/>
                </td>
            </tr>
                
            <tr>
                <td class="MyLable">13.6 Reimb. Bank Addr.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbReimbBankAddr3" runat="server" Width="355"
                        AutoPostBack="False" OnTextChanged="tbReimbBankAddr3_OnTextChanged"
                        ClientEvents-OnValueChanged="tbReimbBankAddr3_OnClientSelectedIndexChanged"/>
                </td>
            </tr>
        </table>
            
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">14.1 Advise Thru Type</td>
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

            <tr>
                <td class="MyLable">14.2 Advise Thru No.</td>
                <td class="MyContent" >
                    <telerik:RadComboBox 
                        ID="rcbAdviseThruNo" 
                        Runat="server" 
                        AppendDataBoundItems="true"
                        OnItemDataBound="SwiftCode_ItemDataBound"  
                        OnClientSelectedIndexChanged="rcbAdviseThruNo_OnClientSelectedIndexChanged"
                        MarkFirstMatch="True"
                        Width="355"
                        Height="150"
                        AllowCustomText="false" >
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            
            <tr >
                <td class="MyLable">14.3 Advise Thru Name</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbAdviseThruName" runat="server" Width="355"/>
                </td>
            </tr>
                
            <tr>
                <td class="MyLable">14.4 Advise Thru Addr.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbAdviseThruAddr1" runat="server" Width="355"/>
                </td>
            </tr>
                
            <tr>
                <td class="MyLable">14.5 Advise Thru Addr.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbAdviseThruAddr2" runat="server" Width="355"/>
                </td>
            </tr>
                
            <tr>
                <td class="MyLable">14.6 Advise Thru Addr.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbAdviseThruAddr3" runat="server" Width="355"/>
                </td>
            </tr>
        </table>   

        <table width="100%" cellpadding="0" cellspacing="0">
        </table>
    </fieldset>

        <fieldset>
            <legend>
              <div style="font-weight:bold;text-transform:uppercase;">Commodity/Prov Details</div>
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
                    <td class="MyContent" width="250">
                        <telerik:RadComboBox
                            AutoPostBack="true"
                            DropDownCssClass="KDDL"
                            ID="rcCommodity" Runat="server"  width="280" 
                            AppendDataBoundItems="True"
                            MarkFirstMatch="True"
                            OnItemDataBound="rcCommodity_ItemDataBound"
                            OnSelectedIndexChanged="rcCommodity_SelectIndexChange"
                            AllowCustomText="false" >
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <HeaderTemplate>
                                <table style="width:250px" cellpadding="0" cellspacing="0"> 
                                  <tr> 
                                     <td style="width:60px;"> 
                                        ID 
                                     </td> 
                                     <td style="width:200px;"> 
                                        Name
                                     </td> 
                                  </tr> 
                               </table> 
                           </HeaderTemplate>
                            <ItemTemplate>
                                <table style="width:250px"  cellpadding="0" cellspacing="0"> 
                                    <tr> 
                                        <td style="width:60px;"> 
                                        <%# DataBinder.Eval(Container.DataItem, "ID")%> 
                                        </td> 
                                        <td style="width:200px;"> 
                                        <%# DataBinder.Eval(Container.DataItem, "Name")%> 
                                        </td> 
                                    </tr> 
                                </table> 
                           </ItemTemplate>
                        </telerik:RadComboBox>
                </td>
                <td><asp:Label ID="lblCommodity" runat="server" /></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">16. Prov % <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator 
                        runat="server" Display="None"
                        ID="RequiredFieldValidator6" 
                        ControlToValidate="numPro" 
                        ValidationGroup="Commit"
                        InitialValue="" 
                        ErrorMessage="Prov % is required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:radnumerictextbox runat="server" ID="numPro" Type="Percent" MaxValue="100"/>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">16. Lc Amount Secured<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator 
                            runat="server" Display="None"
                            ID="RequiredFieldValidator7" 
                            ControlToValidate="numLcAmountSecured" 
                            ValidationGroup="Commit"
                            InitialValue="" 
                            ErrorMessage="PLc Amount Secured is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>

                    </td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox runat="server" ID="numLcAmountSecured" />
                    </td>
                </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">17. Lc Amount UnSecured<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator 
                        runat="server" Display="None"
                        ID="RequiredFieldValidator8" 
                        ControlToValidate="numLcAmountUnSecured" 
                        ValidationGroup="Commit"
                        InitialValue="" 
                        ErrorMessage="Lc Amount UnSecured is required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:radnumerictextbox runat="server" ID="numLcAmountUnSecured" />
                </td>
                </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">18. Loan Principal</td>
                <td class="MyContent">
                    <telerik:radnumerictextbox runat="server" ID="numLoanPrincipal" />
                </td>
            </tr>
        </table>
    </fieldset>
    </div>
    
    <div id="MT700" class="dnnClear">
        <div runat="server" ID="divMT700">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Receiving Bank</td>
                    <td style="width: 450px" class="MyContent">
                        <telerik:RadComboBox 
                            width="355"
                            Height="150"
                            AppendDataBoundItems="true"
                            ID="comboRevivingBank" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:Label ID="tbRevivingBankName" runat="server" />
                    </td>
                </tr>
                
                <tr>    
                    <td style="width: 250px" class="MyLable" style="color: #d0d0d0">27.1 Sequence of Total</td>
                    <td class="MyContent">
                        <asp:Label ID="tbBaquenceOfTotal" runat="server"  Text="Populated by System" />
                    </td>
                    <td></td>
                </tr>
                
                <tr>    
                    <td style="width: 250px" class="MyLable">40A. Form of Documentary Credit</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="comboFormOfDocumentaryCredit" Runat="server" 
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="IRREVOCABLE" Text="IRREVOCABLE"/>
                                <telerik:RadComboBoxItem Value="REVOCABLE" Text="REVOCABLE"/>
                                <telerik:RadComboBoxItem Value="IRREVOCABLE TRANSFERABLE" Text="IRREVOCABLE TRANSFERABLE"/>
                                <telerik:RadComboBoxItem Value="REVOCABLE TRANSFERABLE" Text="REVOCABLE TRANSFERABLE"/>
                                <telerik:RadComboBoxItem Value="IRREVOCABLE STANDBY" Text="IRREVOCABLE STANDBY"/>
                                <telerik:RadComboBoxItem Value="REVOCABLE STANDBY" Text="REVOCABLE STANDBY"/>
                                <telerik:RadComboBoxItem Value="IRREVOC TRANS STANDBY" Text="IRREVOC TRANS STANDBY"/>
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:Label id="tbFormOfDocumentaryCreditName" runat="server" />
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
                        <telerik:RadDatePicker ID="dteDateOfIssue" width="200" runat="server" />
                    </td>
                    <td></td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">31D. Date and Place of Expiry</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadDatePicker ID="dteMT700DateAndPlaceOfExpiry" width="200" runat="server" />
                    </td>
                    <td><telerik:RadTextBox ID="tbPlaceOfExpiry" runat="server" />
                </tr>
                <tr>    
                    <td style="width: 250px" class="MyLable">40E. Applicable Rule</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadComboBox
                            ID="comboAvailableRule"
                            Runat="server" 
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
                    <td style="width: 250px" class="MyLable" >50.1 Applicant Type</td>
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
                        <telerik:RadComboBox 
                            AppendDataBoundItems="True"   
                            AutoPostBack="true" 
                            OnSelectedIndexChanged="rcbApplicant700_SelectIndexChange"
                            OnItemDataBound="rcbApplicantID_ItemDataBound"
                            ID="rcbApplicant700" Runat="server"
                            MarkFirstMatch="True" Width="355"
                            AllowCustomText="false" >
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">50.2 Applicant Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbApplicantName700" runat="server" Width="355" />
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">50.3 Applicant Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbApplicantAddr700_1" runat="server" Width="355" />
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">50.4 Applicant Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbApplicantAddr700_2" runat="server" Width="355" />
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">50.5 Applicant Addr.</td>
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
                <tr>
                    <td class="MyLable" style="width: 250px">59.1 Beneficiary No.</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadTextBox ID="txtBeneficiaryNo700" runat="server" Width="355" 
                            AutoPostBack="True" OnTextChanged="txtBeneficiaryNo700_OnTextChanged"/>
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
                        <td class="MyLable">59.4 Beneficiary Addr.</td>
                        <td class="MyContent">
                            <telerik:RadTextBox ID="txtBeneficiaryAddr700_2" runat="server" Width="355" />
                        </td>
                    </tr>
                
                    <tr>
                        <td class="MyLable">59.5 Beneficiary Addr.</td>
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
                        <telerik:RadComboBox width="200"
                            AppendDataBoundItems="True"
                            ID="comboCurrency700" 
                            Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                    <td><telerik:RadNumericTextBox ID="numAmount700" runat="server" /></td>
                </tr>
                
                <tr>    
                    <td style="width: 250px" class="MyLable">39A. Percentage Credit Amount Tolerance</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadNumericTextBox width="200" ID="numPercentCreditAmount1" runat="server" Type="Percent" MaxValue="100" />
                    </td>
                    <td><telerik:RadNumericTextBox ID="numPercentCreditAmount2" runat="server" Type="Percent" MaxValue="100"/></td>
                </tr>
                
                <tr>    
                    <td style="width: 250px" class="MyLable">39B. Maximum Credit Amount</td>
                    <td class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="comboMaximumCreditAmount700" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
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
                    <td style="width: 250px" class="MyLable">39C.1 Additional Amounts Covered</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtAdditionalAmountsCovered700_1" Width="355" />
                    </td>
                </tr>
            
                <tr>
                    <td style="width: 250px" class="MyLable">39C.2 Additional Amounts Covered</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtAdditionalAmountsCovered700_2" Width="355" />
                    </td>
                </tr>
            </table>
        
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable" >41D.1 Available With Type</td>
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
                            Runat="server" 
                            AppendDataBoundItems="true"
                            AutoPostBack="True"
                            OnItemDataBound="SwiftCode_ItemDataBound"  
                            OnSelectedIndexChanged="comboAvailableWithNo_OnSelectedIndexChanged"
                            MarkFirstMatch="True"
                            Width="355"
                            Height="150"
                            AllowCustomText="false" >
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
                    <td class="MyLable">41D.5 Available With Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAvailableWithAddr2" runat="server" Width="355" />
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">41D.6 Available With Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAvailableWithAddr3" runat="server" Width="355" />
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">41D.7 By</td>
                    <td class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="comboAvailableWithBy" Runat="server" AutoPostBack="True" 
                            OnSelectedIndexChanged="comboAvailableWithBy_OnSelectedIndexChanged"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="BY ACCEPTANCE" Text="BY ACCEPTANCE" />
                                <telerik:RadComboBoxItem Value="BY DEF PAYMENT" Text="BY DEF PAYMENT" />
                                <telerik:RadComboBoxItem Value="BY MIXED PYMT" Text="BY MIXED PYMT" />
                                <telerik:RadComboBoxItem Value="BY NEGOTIATION" Text="BY NEGOTIATION" />
                                <telerik:RadComboBoxItem Value="BY PAYMENT" Text="BY PAYMENT" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td hidden="hidden" ><telerik:RadTextBox ID="tbAvailableWithByName" runat="server" /></td>
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
                    <td style="width: 250px" class="MyLable">42C.2 Drafts At</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDraftsAt700_2" Width="355" ClientEvents-OnValueChanged="txtDraftsAt700_2_OnClientSelectedIndexChanged"  />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable" >42A.1 Drawee Type</td>
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

                <tr>
                    <td class="MyLable">42A.2 Drawee No</td>
                    <td class="MyContent">
                        <%--<telerik:RadTextBox ID="txtDraweeCusNo" runat="server" Width="355" Text="VVTBVNVX" />--%>
                        <telerik:RadComboBox 
                            ID="comboDraweeCusNo700" 
                            Runat="server" 
                            AppendDataBoundItems="true"
                            AutoPostBack="True"
                            OnItemDataBound="SwiftCode_ItemDataBound"  
                            OnSelectedIndexChanged="comboDraweeCusNo700_OnSelectedIndexChanged"
                            MarkFirstMatch="True"
                            Width="355"
                            Height="150"
                            AllowCustomText="false" >
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td style="width: 250px" class="MyLable">42A.3 Drawee Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeCusName" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">42A.4 Drawee Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeAddr1" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">42A.5 Drawee Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeAddr2" runat="server" Width="355"  />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">42A.6 Drawee Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeAddr3" runat="server" Width="355"  />
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
                        <telerik:RadComboBox width="200"
                            ID="rcbPatialShipment" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
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
                        <telerik:RadComboBox width="200"
                            ID="rcbTranshipment" Runat="server" 
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
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
                        <telerik:RadTextBox width="355" ID="tbPlaceoftakingincharge" runat="server"  />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">44E. Port of loading...</td>
                    <td class="MyContent">
                        <telerik:RadTextBox width="355" ID="tbPortofloading" runat="server"  />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">44F. Port of Discharge...</td>
                    <td class="MyContent">
                        <telerik:RadTextBox width="355" ID="tbPortofDischarge" runat="server"  />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">44B. Place of final destination</td>
                    <td class="MyContent">
                        <telerik:RadTextBox width="355" ID="tbPlaceoffinalindistination" runat="server"  />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">44C. Latest Date of Shipment</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker runat="server" ID="tbLatesDateofShipment"/>
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
                    <td style="width: 250px;vertical-align:top;" class="MyLable">45A. Description of Goods/Services</td>
                    <td class="MyContent" style="vertical-align:top;">
                        <telerik:RadTextBox width="700" TextMode="MultiLine" Height="200" 
                                            ID="tbDescrpofGoods" runat="server" Text="
+ COMMODITY: HOT ROLLED STEEL COILS 
+ DESCRIPTION : 
	. THICKNESS: FROM 3.00 MM UP 
	. WIDTH: FROM 1,500 MM DOWN 
	. COIL WEIGHT: FROM 0.1 MT UP 
+ QUANTITY: 55.00 MTS (+/-10PCT) 
+ UNIT PRICE: USD530.00/MT 
+ TOTAL AMOUNT: USD29,150.00 (+/-10PCT) 
+ TRADE TERMS: CFR HOCHIMINH CITY PORT, VIETNAM (INCOTERMS 2010) 
+ ORIGIN: EUROPEAN COMMUNITY 
+ PACKING: IN CONTAINERS "/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0" hidden="hidden" >
                <tr>    
                    <td style="width: 250px" class="MyLable">46A. Docs Required</td>
                    <td class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="rcbDocsRequired" Runat="server" AutoPostBack="True" 
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
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
                    <td style="width: 250px;vertical-align:top;" class="MyLable">46A. Docs required</td>
                    <td class="MyContent" style="vertical-align:top;">
                        <telerik:RadTextBox width="700" TextMode="MultiLine" Height="150"
                                            ID="tbOrderDocs" runat="server" Text="1. SIGNED COMMERCIAL INVOICE IN 03 ORIGINALS ISSUED BY THE BENEFICIARY 
2. FULL (3/3) SET OF ORIGINAL CLEAN SHIPPED ON BOARD BILL OF LADING MADE OUT TO ORDER OF TANPHU BRANCH, NOTIFY APPLICANT AND MARKED FREIGHT PREPAID, SHOWING THE NAME AND ADDRESS OF SHIPPING AGENT WHICH IS LOCATED IN VIETNAM. 
3. QUANTITY AND QUALITY CERTIFICATE IN 01 ORIGINAL AND 02 COPIES ISSUED BY THE BENEFICIARY 
4. CERTIFICATE OF ORIGIN IN 01 ORIGINAL AND 02 COPIES ISSUED BY ANY CHAMBER OF COMMERCE IN EUROPEAN COMMUNITY CERTIFYING THAT THE GOODS ARE OF EUROPEAN COMMUNITY ORIGIN 
5. DETAILED PACKING LIST IN 03 ORIGINALS ISSUED BY THE BENEFICIARY" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px;vertical-align:top;" class="MyLable">47A. Additional Conditions</td>
                    <td class="MyContent" style="vertical-align:top;">
                        <telerik:RadTextBox width="700" TextMode="MultiLine" Height="230" 
                                            ID="tbAdditionalConditions" runat="server" Text="1. ALL REQUIRED DOCUMENTS AND ITS ATTACHED LIST (IF ANY) MUST BE SIGNED OR STAMPED BY ISSUER. 
2. ALL DRAFT(S) AND DOCUMENTS MUST BE MADE OUT IN ENGLISH. DOCUMENTS ISSUED IN ANY OTHER LANGUAGE THAN ENGLISH BUT WITH ENGLISH TRANSLATION ACCEPTABLE. PRE-PRINTED WORDING (IF ANY) ON DOCUMENTS MUST BE IN ENGLISH OR BILINGUAL BUT ONE OF ITS LANGUAGES MUST BE IN ENGLISH. 
3. ALL REQUIRED DOCUMENTS MUST INDICATE OUR L/C NUMBER. 
4. ALL REQUIRED DOCUMENTS MUST BE PRESENTED THROUGH BENEFICIARY'S BANK 
5. SHIPMENT MUST NOT BE EFFECTED BEFORE L/C ISSUANCE DATE. 
6. THE TIME OF RECEIVING AND HANDLING CREDIT DOCUMENTS AT ISSUING BANK ARE LIMITED FROM 7:30 AM TO 04:00 PM. DOCUMENTS ARRIVING AT OUR COUNTER AFTER 04:00 PM LOCAL TIME WILL BE CONSIDERED TO BE RECEIVED ON THE NEXT BANKING DAY. 
7. PLEASE BE INFORMED THAT SATURDAY IS CONSIDERED AS NON-BANKING BUSINESS DAY FOR OUR TRADE FINANCE PROCESSING/OPERATIONS UNIT ALTHOUGH OUR BANK MAY OTHERWISE BE OPENED FOR BUSINESS. 
8. THIRD PARTY DOCUMENTS ARE ACCEPTABLE." />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px;vertical-align:top;" class="MyLable">71B. Charges </td>
                    <td class="MyContent" style="vertical-align:top;">
                        <telerik:RadTextBox width="700" TextMode="MultiLine" Height="75" 
                                            ID="tbCharges" runat="server" Text="ALL BANKING CHARGES OUTSIDE VIETNAM 
PLUS ISSUING BANK'S HANDLING FEE 
ARE FOR ACCOUNT OF BENEFICIARY " />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px;vertical-align:top;" class="MyLable">48. Period for Presentation</td>
                    <td class="MyContent" style="vertical-align:top;">
                        <telerik:RadTextBox width="700" TextMode="MultiLine" Height="75" 
                                            ID="tbPeriodforPresentation" runat="server" Text="NOT EARLIER THAN 21 DAYS AFTER SHIPMENT DATE BUT WITHIN THE VALIDITY OF THIS L/C. "/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px;vertical-align:top;" class="MyLable">49. Confirmation Instructions </td>
                    <td class="MyContent" style="vertical-align:top;">
                        <telerik:RadComboBox width="200"
                            ID="rcbConfimationInstructions" Runat="server" 
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
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
                            Runat="server" 
                            AppendDataBoundItems="true"
                            OnItemDataBound="SwiftCode_ItemDataBound" 
                            MarkFirstMatch="True"
                            Width="355"
                            Height="150"
                            AllowCustomText="false" >
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                
                <tr >
                    <td class="MyLable" style="width: 250px;">53.3 Reimb. Bank Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbReimbBankName700" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable" style="width: 250px;">53.4 Reimb. Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbReimbBankAddr700_1" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable" style="width: 250px;">53.5 Reimb. Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbReimbBankAddr700_2" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable" style="width: 250px;">53.6 Reimb. Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbReimbBankAddr700_3" runat="server" Width="355"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px;vertical-align:top;" class="MyLable">78. Instr to//Payg/Accptg/Negotg Bank</td>
                    <td class="MyContent" style="vertical-align:top;">
                        <telerik:RadTextBox width="700" TextMode="MultiLine" Height="150" 
                                            ID="tbNegotgBank" runat="server" Text="1. USD70.00 DISCREPANCY FEE WILL BE DEDUCTED FROM THE PROCEEDS FOR EACH DISCREPANT SET OF DOCUMENTS PRESENTED UNDER THIS L/C. THE RELATIVE TELEX EXPENSES USD25.00, IF ANY, WILL BE ALSO FOR THE ACCOUNT OF BENEFICIARY. 
2. EACH DRAWING MUST BE ENDORSED ON THE ORIGINAL L/C BY THE NEGOTIATING/PRESENTING BANK. 
3. PLEASE SEND ALL DOCS TO VIET VICTORY BANK AT PLOOR 9TH, NO.10 PHO QUANG STREET, TAN BINH DISTRICT, HOCHIMINH CITY, VIETNAM IN ONE LOT BY THE COURIER SERVICES. 
4. UPON RECEIPT OF DOCS REQUIRED IN COMPLIANCE WITH ALL TERMS AND CONDITIONS OF THE L/C, WE SHALL REMIT THE PROCEEDS TO YOU AS PER YOUR INSTRUCTIONS IN THE COVER LETTER." />
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
                            Runat="server" 
                            AppendDataBoundItems="true"
                            AutoPostBack="False"
                            OnItemDataBound="SwiftCode_ItemDataBound"  
                            MarkFirstMatch="True"
                            Width="355"
                            Height="150"
                            AllowCustomText="false" >
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                
                <tr >
                    <td class="MyLable" style="width: 250px;">57.2 Advise Through Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdviseThroughBankName700" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable" style="width: 250px;">57.3 Advise Through Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdviseThroughBankAddr700_1" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable" style="width: 250px;">57.4 Advise Through Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdviseThroughBankAddr700_2" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable" style="width: 250px;">57.5 Advise Through Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdviseThroughBankAddr700_3" runat="server" Width="355"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px;vertical-align:top;" class="MyLable">72. Sender to Receiver Information</td>
                    <td class="MyContent" style="vertical-align:top;">
                        <asp:TextBox width="700" TextMode="MultiLine" Height="75" 
                            ID="tbSendertoReceiverInfomation" runat="server">PLEASE ACKNOWLEDGE YOUR RECEIPT OF THIS L/C BY MT730.</asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    
    <div id="MT740" class="dnnClear">
        <div runat="server" ID="divMT740">
	        <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Generate MT740</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                            width="200"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="comGenerate_OnSelectedIndexChanged"
                            ID="comGenerate" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
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
                            AutoPostBack="True" OnTextChanged="txtReceivingBankNo_OnTextChanged"/>
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
                        <telerik:RadDatePicker width="200" ID="tbExpiryDate740" runat="server"></telerik:RadDatePicker>
                    </td>
                    <td><telerik:RadTextBox width="200" ID="tb31DPlaceOfExpiry" runat="server" /> </td>
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
                        <telerik:RadTextBox ID="tbBeneficiaryNo740" runat="server" Width="355" AutoPostBack="True" 
                            OnTextChanged="tbBeneficiaryNo740_OnTextChanged"/>
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
                    <td style="width: 200px" class="MyLable">59.5 Beneficiary Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbBeneficiaryAddr740_2" runat="server" Width="355" />
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 200px" class="MyLable">59.6 Beneficiary Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbBeneficiaryAddr740_3" runat="server" Width="355" />
                    </td>
                </tr>
            </table>
              
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">32B. Credit Amount</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadComboBox  width="200"
                            ID="comboCreditCurrency" 
                            AppendDataBoundItems="True"
                            Runat="server" 
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
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
                        <telerik:RadNumericTextBox width="200" ID="numPercentageCreditAmountTolerance740_1" 
                            runat="server" Type="Percent" MaxValue="100" />
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="numPercentageCreditAmountTolerance740_2" runat="server" Type="Percent" MaxValue="100"/>
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
                    <td style="width: 250px" class="MyLable" >41A.1 Available With Type</td>
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
                        <telerik:RadTextBox ID="tbAvailableWithNo740" runat="server" Width="355" 
                            AutoPostBack="True" OnTextChanged="tbAvailableWithNo740_OnTextChanged"/>
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
                    <td class="MyLable">41A.5 Available With Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAvailableWithAddr740_2" runat="server" Width="355" />
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">41A.6 Available With Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbAvailableWithAddr740_3" runat="server" Width="355" />
                    </td>
                </tr>
            </table>
        
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 250px" class="MyLable">42C.1 Drafts At</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDraftsAt740_1" Width="355" />
                    </td>
                </tr>
            
                <tr>
                    <td style="width: 250px" class="MyLable">42C.2 Drafts At</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" ID="txtDraftsAt740_2" Width="355" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
            
                <tr>
                    <td style="width: 250px" class="MyLable">42A.2 Drawee No</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbDraweeCusNo740" runat="server" Width="355" Text="VVTBVNVX" />
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
                        <telerik:RadTextBox ID="tbDraweeAddr740_1" runat="server" Width="355" Text="9th FLOOR, 10 PHO QUANG, WARD 2"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">42A.5 Drawee Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbDraweeAddr740_2" runat="server" Width="355" Text="TAN BINH DISTRICT"/>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">42A.6 Drawee Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbDraweeAddr740_3" runat="server" Width="355" Text="HCM CITY" />
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">71A. Reimbursing Bank's Changes</td>
                    <td class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="comboReimbursingBankChange" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
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
                    <td class="MyLable">72.2 Sender to receiver information</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation740_2" runat="server" Width="355" />
                    </td>
                </tr>
            
                <tr>
                    <td class="MyLable">72.3 Sender to receiver information</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation740_3" runat="server" Width="355" />
                    </td>
                </tr>
            
                <tr>
                    <td class="MyLable">72.4 Sender to receiver information</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInformation740_4" runat="server" Width="355" />
                    </td>
                </tr>
            </table>

        </div>
    </div>
    
    <div id="MT707" class="dnnClear">
        <div runat="server" ID="divMT707">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px">Receiving Bank</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadTextBox ID="txtReceivingBankId_707" runat="server"/>
                    </td>
                    <td>
                        <asp:Label ID="lblReceivingBankName_707" runat="server" Text="" />
                    </td>
                </tr>
                
                <tr>    
                    <td  class="MyLable">20. Sender's Reference</td>
                    <td class="MyContent">
                        <asp:Label ID="lblSenderReference_707" runat="server" />
                    </td>
                </tr>
                
                <tr>    
                    <td class="MyLable">21. Receiver's Reference</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtReceiverReference_707" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>    
                    <td class="MyLable">23. Reference To Pre-Advice</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtReferenceToPreAdvice_707" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>    
                    <td class="MyLable">52A.1 Issuing Bank Reference</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIssuingBankReferenceNo_707" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>    
                    <td class="MyLable">52A.2 Issuing Bank Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIssuingBankReferenceName_707" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>    
                    <td class="MyLable">52A.3 Issuing Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIssuingBankReferenceAddr_707_1" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>    
                    <td class="MyLable">52A.4 Issuing Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIssuingBankReferenceAddr_707_2" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>    
                    <td class="MyLable">52A.5 Issuing Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtIssuingBankReferenceAddr_707_3" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>    
                    <td class="MyLable">31C. Date of Issue</td>
                    <td class="MyContent">
                        <telerik:Raddatepicker runat="server" ID="dteDateOfIssue_707" />
                    </td>
                </tr>
                
                <tr>    
                    <td style="width: 250px" class="MyLable">40E. Applicable Rule</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadComboBox
                            ID="comboAvailableRule_707"
                            Runat="server" 
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
                        <telerik:Raddatepicker runat="server" ID="dteDateOfAmendment_707" />
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
                        <telerik:RadTextBox ID="txtBeneficiaryNo_707" runat="server" Width="355" />
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable" style="width: 250px">59.2 Beneficiary Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryName_707" runat="server" Width="355" />
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">59.3 Beneficiary Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryAddr_707_1" runat="server" Width="355" />
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">59.4 Beneficiary Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryAddr_707_2" runat="server" Width="355" />
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">59.5 Beneficiary Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtBeneficiaryAddr_707_3" runat="server" Width="355" />
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable" style="width: 250px;">33E. New Date of Expiry</td>
                    <td class="MyContent">
                        <telerik:Raddatepicker runat="server"  ID="dteNewDateOfExpiry_707"/>
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px;">39A. Percentage Credit Amount Tolerance</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:Radnumerictextbox incrementsettings-interceptarrowkeys="true" 
                            incrementsettings-interceptmousewheel="true"  Type="Percent" MaxValue="100"
                            runat="server" id="numPercentageCreditAmountTolerance_707_1" />
                    </td>
                    <td>
                        <telerik:Radnumerictextbox 
                            incrementsettings-interceptarrowkeys="true" Type="Percent" MaxValue="100"
                            incrementsettings-interceptmousewheel="true"  runat="server"  
                            id="numPercentageCreditAmountTolerance_707_2" />
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
                        <telerik:RadTextBox ID="txtAdditionalAmountsCovered_707_1" runat="server" Width="355" />
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdditionalAmountsCovered_707_2" runat="server" Width="355" />
                    </td>
                </tr>
                
                <tr>    
	                <td class="MyLable">44A. Place of taking in charge...</td>
	                <td class="MyContent">
		                <telerik:RadTextBox width="355" ID="txtPlaceoftakingincharge_707" runat="server"  />
	                </td>
                </tr>
                <tr>    
	                <td class="MyLable">44B. Place of final destination</td>
	                <td class="MyContent">
		                <telerik:RadTextBox width="355" ID="txtPlaceoffinalindistination_707" runat="server"  />
	                </td>
                </tr>

                <tr>    
	                <td class="MyLable">44C. Latest Date of Shipment, if amd.</td>
	                <td class="MyContent">
		                <telerik:RadDatePicker runat="server" ID="dteLatesDateofShipment_707"/>
	                </td>
                </tr>
                <tr>
	                <td class="MyLable">44D. Shipment Period</td>
	                <td class="MyContent">
		                <telerik:RadTextBox runat="server" ID="txtShipmentPeriod_707_1" Width="355" />
	                </td>
                </tr>

                <tr>
	                <td style="width: 250px" class="MyLable"></td>
	                <td class="MyContent">
		                <telerik:RadTextBox runat="server" ID="txtShipmentPeriod_707_2" Width="355" />
	                </td>
                </tr>
                <tr>
	                <td style="width: 250px" class="MyLable"></td>
	                <td class="MyContent">
		                <telerik:RadTextBox runat="server" ID="txtShipmentPeriod_707_3" Width="355" />
	                </td>
                </tr>

                <tr>
	                <td style="width: 250px" class="MyLable"></td>
	                <td class="MyContent">
		                <telerik:RadTextBox runat="server" ID="txtShipmentPeriod_707_4" Width="355" />
	                </td>
                </tr>
                <tr>
	                <td style="width: 250px" class="MyLable"></td>
	                <td class="MyContent">
		                <telerik:RadTextBox runat="server" ID="txtShipmentPeriod_707_5" Width="355" />
	                </td>
                </tr>

                <tr>
	                <td style="width: 250px" class="MyLable"></td>
	                <td class="MyContent">
		                <telerik:RadTextBox runat="server" ID="txtShipmentPeriod_707_6" Width="355" />
	                </td>
                </tr>
                <tr>    
	                <td class="MyLable">44E. Port of loading...</td>
	                <td class="MyContent">
		                <telerik:RadTextBox width="355" ID="txtPortofloading_707" runat="server"  />
	                </td>
                </tr>
                <tr>    
	                <td class="MyLable">44F. Port of Discharge...</td>
	                <td class="MyContent">
		                <telerik:RadTextBox width="355" ID="txtPortofDischarge_707" runat="server"  />
	                </td>
                </tr>
                <tr>
	                <td class="MyLable" style="vertical-align: top ">77A. Narrative, if amended</td>
	                <td class="MyContent">
		                <telerik:RadTextBox ID="txtNarrative_707" runat="server" Width="355" TextMode="MultiLine" Height="200"/>
	                </td>
                </tr>
                <tr>
	                <td class="MyLable">72. Sender to receiver information</td>
	                <td class="MyContent">
		                <telerik:RadTextBox ID="txtSenderToReceiverInformation_707_1" runat="server" Width="355" />
	                </td>
                </tr>

                <tr>
	                <td class="MyLable"></td>
	                <td class="MyContent">
		                <telerik:RadTextBox ID="txtSenderToReceiverInformation_707_2" runat="server" Width="355" />
	                </td>
                </tr>

                <tr>
	                <td class="MyLable"></td>
	                <td class="MyContent">
		                <telerik:RadTextBox ID="txtSenderToReceiverInformation_707_3" runat="server" Width="355" />
	                </td>
                </tr>

                <tr>
	                <td class="MyLable"></td>
	                <td class="MyContent">
		                <telerik:RadTextBox ID="txtSenderToReceiverInformation_707_4" runat="server" Width="355" />
	                </td>
                </tr>
            </table>

        </div>
    </div>
    
    <div id="MT747" class="dnnClear">
        <div runat="server" ID="divMT747">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Generate MT747</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                            width="200"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="comboGenerateMT747_OnSelectedIndexChanged"
                            ID="comboGenerateMT747" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
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
                        <telerik:RadTextBox ID="txtReceivingBank_747" runat="server" Width="355"/>
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

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px;">21.2 Reimb. Bank No</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                            ID="comboReimbBankNo_747" 
                            Runat="server" 
                            AppendDataBoundItems="true"
                            OnItemDataBound="SwiftCode_ItemDataBound" 
                            MarkFirstMatch="True"
                            Width="355"
                            Height="150"
                            AllowCustomText="false" >
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                
                <tr >
                    <td class="MyLable" style="width: 250px;">21.3 Reimb. Bank Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtReimbBankName_747" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable" style="width: 250px;">21.4 Reimb. Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtReimbBankAddr_747_1" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable" style="width: 250px;">21.5 Reimb. Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtReimbBankAddr_747_2" runat="server" Width="355"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable" style="width: 250px;">21.6 Reimb. Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtReimbBankAddr_747_3" runat="server" Width="355"/>
                    </td>
                </tr>
            
                <tr>
                    <td class="MyLable" style="width: 250px;">30. Date of Original Authorization</td>
                    <td class="MyContent">
                        <telerik:Raddatepicker runat="server"  ID="dteDateOfOriginalAuthorization_747"/>
                    </td>
                </tr>
            
                <tr>
                    <td class="MyLable" style="width: 250px;">33E. New Date of Expiry</td>
                    <td class="MyContent">
                        <telerik:Raddatepicker runat="server"  ID="dteNewDateOfExpiry_747"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px;">34B. New Documentary Credit Amount</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadComboBox 
                            AppendDataBoundItems="True"
                            ID="comboCurrency_747" Runat="server" 
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                        </telerik:RadComboBox>
                        
                    </td>
                    <td>
                        <telerik:Radnumerictextbox incrementsettings-interceptarrowkeys="true" 
                            incrementsettings-interceptmousewheel="true"  
                            runat="server" id="numAmount_747" />
                    </td>
                    
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 250px;">39A. Percentage Credit Amount Tolerance</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:Radnumerictextbox incrementsettings-interceptarrowkeys="true" 
                            incrementsettings-interceptmousewheel="true"  Type="Percent" MaxValue="100"
                            runat="server" id="numPercentageCreditTolerance_747_1" />
                    </td>
                    <td>
                        <telerik:Radnumerictextbox 
                            incrementsettings-interceptarrowkeys="true" Type="Percent" MaxValue="100"
                            incrementsettings-interceptmousewheel="true"  runat="server"  
                            id="numPercentageCreditTolerance_747_2" />
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
                        <telerik:RadTextBox ID="txtAdditionalCovered_747_1" runat="server" Width="355"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdditionalCovered_747_2" runat="server" Width="355"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdditionalCovered_747_3" runat="server" Width="355"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdditionalCovered_747_4" runat="server" Width="355"/>
                    </td>
                </tr>
            
                <tr>
                    <td class="MyLable" style="width: 250px;">72. Sender to Receiver Information</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInfomation_747_1" runat="server" Width="355"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInfomation_747_2" runat="server" Width="355"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInfomation_747_3" runat="server" Width="355"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="width: 250px;"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSenderToReceiverInfomation_747_4" runat="server" Width="355"/>
                    </td>
                </tr>
            
                <tr>
                    <td class="MyLable" style="width: 250px;vertical-align: top ">77A. Narrative</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtNarrative_747" runat="server" Width="355" TextMode="MultiLine" Height="200"/>
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
                <telerik:RadTab Text="Cable Charge"/>
                <telerik:RadTab Text="Open charge"/>
                <telerik:RadTab Text="Open Charge for Import LC (Amort)" />
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

                    <table width="100%" cellpadding="0" cellspacing="0" id="table1" runat="server" >
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

                    <table width="100%" cellpadding="0" cellspacing="0" id="table3" runat="server" >
                        <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent" >
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

	                <table width="100%" cellpadding="0" cellspacing="0" id="table5" runat="server" >
		                <tr>
			                <td class="MyLable">Charge Acct</td>
			                <td class="MyContent" >
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
            var datePicker = $find("<%= tbContingentExpiry.ClientID %>");
            var ExpiryDate = $find("<%= tbExpiryDate.ClientID %>");
            var PlaceOfExpiry = $find("<%= dteMT700DateAndPlaceOfExpiry.ClientID %>");
            
            var ExpiryDate740 = $find("<%= tbExpiryDate740.ClientID %>");
            

            var date = ExpiryDate.get_selectedDate();
            var dateP = ExpiryDate.get_selectedDate();

            date.setDate(date.getDate() + 15);
            datePicker.set_selectedDate(date);
            
            if (PlaceOfExpiry) {
                PlaceOfExpiry.set_selectedDate(dateP);
            }

            if (ExpiryDate740) {
                ExpiryDate740.set_selectedDate(ExpiryDate.get_selectedDate());
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

        function rcbExpiryPlace_OnClientSelectedIndexChanged(sender, eventArgs) {
            var combo = $find('<%=tbExpiryPlace.ClientID %>');
            var txtDate = $find("<%= tbPlaceOfExpiry.ClientID %>");
            var PlaceOfExpiry740 = $find("<%= tb31DPlaceOfExpiry.ClientID %>");
            
            txtDate.set_value(combo.get_value());
            if (PlaceOfExpiry740) {
                PlaceOfExpiry740.set_value(combo.get_value());    
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

        function tbcrTolerance_TextChanged(sender, eventArgs) {
            var numPercentCreditAmount1 = $find("<%= numPercentCreditAmount1.ClientID %>"),
                numPercentageCreditAmountTolerance740_1 = $find("<%= numPercentageCreditAmountTolerance740_1.ClientID %>"),
                numPercentageCreditAmountTolerance_707_1 = $find("<%= numPercentageCreditAmountTolerance_707_1.ClientID %>"),
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
        }

        function tbdrTolerance_TextChanged(sender, eventArgs) {
            var numPercentCreditAmount2 = $find("<%= numPercentCreditAmount2.ClientID %>"),
                numPercentageCreditAmountTolerance740_2 = $find("<%= numPercentageCreditAmountTolerance740_2.ClientID %>"),
                numPercentageCreditAmountTolerance_707_2 = $find("<%= numPercentageCreditAmountTolerance_707_2.ClientID %>"),
                crToleranceVal = $find('<%=tbcrTolerance.ClientID %>').get_value();
            
            if (numPercentCreditAmount2)
            {
                numPercentCreditAmount2.set_value(crToleranceVal);
            }
            
            if (numPercentageCreditAmountTolerance740_2) {
                numPercentageCreditAmountTolerance740_2.set_value(crToleranceVal);
            }
            
            if (numPercentageCreditAmountTolerance_707_2) {
                numPercentageCreditAmountTolerance_707_2.set_value(crToleranceVal);
            }
        }


        function AdviseBank_OnClientSelectedIndexChanged() {
            var comboRevivingBank = $find("<%= comboRevivingBank.ClientID %>"),
                txtRemittingBankNo = $find("<%= txtRemittingBankNo.ClientID %>");

            if (comboRevivingBank) {
                comboRevivingBank.set_text($find("<%=rcbAdviseBankNo.ClientID %>").get_selectedItem().get_text());
                comboRevivingBank.set_value($find("<%=rcbAdviseBankNo.ClientID %>").get_value());
            }

            if (txtRemittingBankNo) {
                txtRemittingBankNo.set_value($find("<%=rcbAdviseBankNo.ClientID %>").get_value());
            }
        }
        
        function rcbReimbBankNo_OnClientSelectedIndexChanged() {
            var rcbReimbBankNo = $find("<%= rcbReimbBankNo.ClientID %>"),
                tbReimbBankName = $find("<%= tbReimbBankName.ClientID %>"),
                rcbReimbBankNo700 = $find("<%= rcbReimbBankNo700.ClientID %>"),
                tbReimbBankName700 = $find("<%= tbReimbBankName700.ClientID %>"),
                comboRevivingBank = $find("<%= comboRevivingBank.ClientID %>"),
                txtRemittingBankNo = $find("<%= txtRemittingBankNo.ClientID %>"),
                lblReceivingBank_747 = $find("<%= txtReceivingBank_747.ClientID %>"),
                comboReimbBankNo_747 = $find("<%= comboReimbBankNo_747.ClientID %>"),
                txtReimbBankName_747 = $find("<%= txtReimbBankName_747.ClientID %>");

            if (rcbReimbBankNo700) {
                rcbReimbBankNo700.set_text(rcbReimbBankNo.get_selectedItem().get_text());
                rcbReimbBankNo700.set_value(rcbReimbBankNo.get_value());
                
                tbReimbBankName700.set_value(rcbReimbBankNo.get_selectedItem().get_attributes().getAttribute("BankName"));
            }
            
            if (comboRevivingBank) {
                comboRevivingBank.set_text(rcbReimbBankNo.get_selectedItem().get_text());
                comboRevivingBank.set_value(rcbReimbBankNo.get_value());
            }

            if (txtRemittingBankNo) {
                txtRemittingBankNo.set_value(rcbReimbBankNo.get_value());
            }
            
            if (lblReceivingBank_747) {
                lblReceivingBank_747.set_value(rcbReimbBankNo.get_value());
            }
            
            if (comboReimbBankNo_747) {
                comboReimbBankNo_747.set_text(rcbReimbBankNo.get_selectedItem().get_text());
                comboReimbBankNo_747.set_value(rcbReimbBankNo.get_value());
                
                txtReimbBankName_747.set_value(rcbReimbBankNo.get_selectedItem().get_attributes().getAttribute("BankName"));
            }

            if (tbReimbBankName) {
                tbReimbBankName.set_value(rcbReimbBankNo.get_selectedItem().get_attributes().getAttribute("BankName"));
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
                txtBeneficiaryAddr_707_1 = $find('<%=txtBeneficiaryAddr_707_1.ClientID %>');

            if (txtBeneficiaryAddr700_1) {
                txtBeneficiaryAddr700_1.set_value(txtBeneficiaryBankAddr1.get_value());
            }
            
            if (txtBeneficiaryAddr_707_1) {
                txtBeneficiaryAddr_707_1.set_value(txtBeneficiaryBankAddr1.get_value());
            }
        }
        
        function txtBeneficiaryBankAddr2_OnValueChanged (sender, eventArgs) {
            var txtBeneficiaryAddr700_2 = $find('<%=txtBeneficiaryAddr700_2.ClientID %>');
            var txtBeneficiaryBankAddr2 = $find('<%=txtBeneficiaryBankAddr2.ClientID %>'),
                txtBeneficiaryAddr_707_2 = $find('<%=txtBeneficiaryAddr_707_2.ClientID %>');

            if (txtBeneficiaryAddr700_2) {
                txtBeneficiaryAddr700_2.set_value(txtBeneficiaryBankAddr2.get_value());
            }
            
            if (txtBeneficiaryAddr_707_2) {
                txtBeneficiaryAddr_707_2.set_value(txtBeneficiaryBankAddr2.get_value());
            }
        }
        
        function txtBeneficiaryBankAddr3_OnValueChanged (sender, eventArgs) {
            var txtBeneficiaryAddr700_3 = $find('<%=txtBeneficiaryAddr700_3.ClientID %>');
            var txtBeneficiaryBankAddr3 = $find('<%=txtBeneficiaryBankAddr3.ClientID %>'),
                txtBeneficiaryAddr_707_3 = $find('<%=txtBeneficiaryAddr_707_3.ClientID %>');

            if (txtBeneficiaryAddr700_3) {
                txtBeneficiaryAddr700_3.set_value(txtBeneficiaryBankAddr3.get_value());
            }
            
            if (txtBeneficiaryAddr_707_3) {
                txtBeneficiaryAddr_707_3.set_value(txtBeneficiaryBankAddr3.get_value());
            }
        }
        
        function txtBeneficiaryBankName_OnValueChanged (sender, eventArgs) {
            var txtBeneficiaryName700 = $find('<%=txtBeneficiaryName700.ClientID %>');
            var txtBeneficiaryBankName = $find('<%=txtBeneficiaryBankName.ClientID %>'),
                txtBeneficiaryName_707 = $find('<%=txtBeneficiaryName_707.ClientID %>');

            if (txtBeneficiaryName700) {
                txtBeneficiaryName700.set_value(txtBeneficiaryBankName.get_value());
            }
            
            if (txtBeneficiaryName_707) {
                txtBeneficiaryName_707.set_value(txtBeneficiaryBankName.get_value());
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
        
        function rcbApplicantID_OnClientSelectedIndexChanged() {
            var rcbApplicantID = $find('<%=rcbApplicantID.ClientID %>'),
                rcbApplicant700 = $find('<%=rcbApplicant700.ClientID %>'),
                tbApplicantName700 = $find('<%=tbApplicantName700.ClientID %>'),
                tbApplicantAddr700_1 = $find('<%=tbApplicantAddr700_1.ClientID %>'),
                tbApplicantAddr700_2 = $find('<%=tbApplicantAddr700_2.ClientID %>'),
                tbApplicantAddr700_3 = $find('<%=tbApplicantAddr700_3.ClientID %>'),
                
                tbApplicantName = $find('<%=tbApplicantName.ClientID %>'),
                tbApplicantAddr1 = $find('<%=tbApplicantAddr1.ClientID %>'),
                tbApplicantAddr2 = $find('<%=tbApplicantAddr2.ClientID %>'),
                tbApplicantAddr3 = $find('<%=tbApplicantAddr3.ClientID %>');
            

            if (rcbApplicant700) {
                rcbApplicant700.set_text(rcbApplicantID.get_selectedItem().get_text());
                rcbApplicant700.set_value(rcbApplicantID.get_value());
                
                tbApplicantName700.set_value(rcbApplicantID.get_selectedItem().get_attributes().getAttribute("CustomerName"));
                tbApplicantAddr700_1.set_value(rcbApplicantID.get_selectedItem().get_attributes().getAttribute("Address"));
                tbApplicantAddr700_2.set_value(rcbApplicantID.get_selectedItem().get_attributes().getAttribute("City"));
                tbApplicantAddr700_3.set_value(rcbApplicantID.get_selectedItem().get_attributes().getAttribute("Country"));
            }
            
            if (tbApplicantName) {
                tbApplicantName.set_value(rcbApplicantID.get_selectedItem().get_attributes().getAttribute("CustomerName"));
                tbApplicantAddr1.set_value(rcbApplicantID.get_selectedItem().get_attributes().getAttribute("Address"));
                tbApplicantAddr2.set_value(rcbApplicantID.get_selectedItem().get_attributes().getAttribute("City"));
                tbApplicantAddr3.set_value(rcbApplicantID.get_selectedItem().get_attributes().getAttribute("Country"));
            }
        }
        
        function txtBeneficiaryNo_OnValueChanged() {
            var txtBeneficiaryNo = $find('<%=txtBeneficiaryNo.ClientID %>'),
                bankCode = txtBeneficiaryNo.get_value(),
                txtBeneficiaryNo700 = $find('<%=txtBeneficiaryNo700.ClientID %>'),
                
                tbBeneficiaryNo740 = $find('<%=tbBeneficiaryNo740.ClientID %>'),
                
                txtBeneficiaryNo_707 = $find('<%=txtBeneficiaryNo_707.ClientID %>');
            
            if (txtBeneficiaryNo700) {
                txtBeneficiaryNo700.set_value(bankCode);
                //txtBeneficiaryName700.set_value(bankName);
            }
            
            if (tbBeneficiaryNo740) {
                tbBeneficiaryNo740.set_value(bankCode);
                //tbBeneficiaryName740.set_value(bankName);
            }
            
            if (txtBeneficiaryNo_707) {
                txtBeneficiaryNo_707.set_value(bankCode);
                //txtBeneficiaryName_707.set_value(bankName);
            }
        }
        
        function rcbAdviseThruNo_OnClientSelectedIndexChanged() {
            var rcbAdviseThruNo = $find('<%=rcbAdviseThruNo.ClientID %>'),
                bankCode = rcbAdviseThruNo.get_value(),
                bankName = rcbAdviseThruNo.get_selectedItem().get_attributes().getAttribute('BankName'),
                tbAdviseThruName = $find('<%=tbAdviseThruName.ClientID %>'),
                comboAdviseThroughBankNo700 = $find('<%=comboAdviseThroughBankNo700.ClientID %>'),
                txtAdviseThroughBankName700 = $find('<%=txtAdviseThroughBankName700.ClientID %>');
            
            if (tbAdviseThruName) {
                tbAdviseThruName.set_value(bankName);
            }
            
            if (comboAdviseThroughBankNo700) {
                comboAdviseThroughBankNo700.set_text(rcbAdviseThruNo.get_selectedItem().get_text());
                comboAdviseThroughBankNo700.set_value(bankCode);
                
                txtAdviseThroughBankName700.set_value(bankName);
            }
        }

    </script>
</telerik:RadCodeBlock>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1">
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
                
                <telerik:AjaxUpdatedControl ControlID="rcbApplicant700" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantName700" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantAddr700_1" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantAddr700_2" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantAddr700_3" />
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
        
        <telerik:AjaxSetting AjaxControlID="rcbAdviseBankNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbAdviseBankName" />
                
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
                <telerik:AjaxUpdatedControl ControlID="rcbApplicant700" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantName700" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantAddr700_1" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantAddr700_2" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantAddr700_3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="rcbApplicant700">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbApplicantName700" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantAddr700_1" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantAddr700_2" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantAddr700_3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboAvailableWithNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithName" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithNo740" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithName740" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="rcbAvailableWithType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="comboAvailableWithNo" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithName" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr1" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr2" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailableWithAddr3" />

                <telerik:AjaxUpdatedControl ControlID="rcbAvailableWithType740" />
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
                <telerik:AjaxUpdatedControl ControlID="tb31DPlaceOfExpiry" />

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
                <telerik:AjaxUpdatedControl ControlID="tbExpiryDate740" />
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
                <telerik:AjaxUpdatedControl ControlID="rcbReimbBankNo" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankName" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr3" />
                
                <telerik:AjaxUpdatedControl ControlID="comboReimbBankType700" />
                <telerik:AjaxUpdatedControl ControlID="rcbReimbBankNo700" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankName700" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr700_1" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr700_2" />
                <telerik:AjaxUpdatedControl ControlID="tbReimbBankAddr700_3" />
                
                <telerik:AjaxUpdatedControl ControlID="comboReimbBankType_747" />
                <telerik:AjaxUpdatedControl ControlID="comboReimbBankNo_747" />
                <telerik:AjaxUpdatedControl ControlID="txtReimbBankName_747" />
                <telerik:AjaxUpdatedControl ControlID="txtReimbBankAddr_747_1" />
                <telerik:AjaxUpdatedControl ControlID="txtReimbBankAddr_747_2" />
                <telerik:AjaxUpdatedControl ControlID="txtReimbBankAddr_747_3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="rcbAdviseThruType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbAdviseThruNo" />
                <telerik:AjaxUpdatedControl ControlID="tbAdviseThruName" />
                <telerik:AjaxUpdatedControl ControlID="tbAdviseThruAddr1" />
                <telerik:AjaxUpdatedControl ControlID="tbAdviseThruAddr2" />
                <telerik:AjaxUpdatedControl ControlID="tbAdviseThruAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        
        <telerik:AjaxSetting AjaxControlID="comboAvailableRule">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblApplicableRule740" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboDraweeCusType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="comboDraweeCusNo700" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeCusName" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboDraweeCusNo700">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtDraweeCusName" />
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
                <telerik:AjaxUpdatedControl ControlID="txtNarrative_747" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        
    </AjaxSettings>
</telerik:RadAjaxManager>

<div style="visibility: hidden;"><asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnIssueLC_MT700Report" runat="server" OnClick="btnIssueLC_MT700Report_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnIssueLC_MT740Report" runat="server" OnClick="btnIssueLC_MT740Report_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnIssueLC_VATReport" runat="server" OnClick="btnIssueLC_VATReport_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnIssueLC_NHapNgoaiBangReport" runat="server" OnClick="btnIssueLC_NHapNgoaiBangReport_Click" Text="Search" /></div>

<div style="visibility:hidden;"><asp:Button ID="btnAmentLCReport_XuatNgoaiBang" runat="server" OnClick="btnAmentLCReport_XuatNgoaiBang_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnAmentLCReport_NhapNgoaiBang" runat="server" OnClick="btnAmentLCReport_NhapNgoaiBang_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnAmentLCReport_VAT" runat="server" OnClick="btnAmentLCReport_VAT_Click" Text="Search" /></div>

<div style="visibility:hidden;"><asp:Button ID="btnCancelLC_XUATNGOAIBANG" runat="server" OnClick="btnCancelLC_XUATNGOAIBANG_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnCancelLC_VAT" runat="server" OnClick="btnCancelLC_VAT_Click" Text="Search" /></div>