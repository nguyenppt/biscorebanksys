<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumetaryCollection.ascx.cs" Inherits="BankProject.TradingFinance.Import.DocumentaryCollections.DocumetaryCollection" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../../Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />

<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <script type="text/javascript">
        var lastClickedItem = null;
        var clickCalledAfterRadconfirm = false;
        var clickCalledAfterRadconfirm218 = false;
        var clickCalledAfterRadconfirm219 = false;
        var clickCalledAfterRadconfirm281 = false;
        
        var amount =  parseFloat(<%= Amount %>);
        var amount_Old = parseFloat(<%= Amount_Old %>);
        var chargeAmount = parseFloat(<%= ChargeAmount %>);
        var b4_AUT_Amount = parseFloat(<%= B4_AUT_Amount %>);
        var tabId = <%= TabId %>;
        var createMT410Tab = '<%= CreateMT410 %>';
        
        jQuery(function ($) {
            $('#tabs-demo').dnnTabs();
        });
        
        function RadToolBar1_OnClientButtonClicking(sender, args) {
            var button = args.get_item();
            if (button.get_commandName() == "save") {
                var collectionTypeVal = $find('<%=comboCollectionType.ClientID %>').get_selectedItem().get_value();
                var dteMaturityDate = $find('<%=dteMaturityDate.ClientID %>').get_selectedDate();
                var txtTenor = $find('<%=txtTenor.ClientID %>').get_value();
                
                if (collectionTypeVal.indexOf('DA') != -1) {
                    if (!dteMaturityDate || !txtTenor) {
                        args.set_cancel(true);
                        radalert("Maturity Date/Tenor is required", 340, 150, 'Error');
                    }
                }
            }
            
            if (tabId == 217) {// Register Documetary Collection
                if (button.get_commandName() == "print" && !clickCalledAfterRadconfirm) {
                    args.set_cancel(true);
                    if (createMT410Tab === 'YES') {
                        radconfirm("Do you want to download MT410 file?", confirmCallbackFunction1, 340, 150, null, 'Download');
                    } else {
                        radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file?", confirmCallbackFunction2, 420, 150, null, 'Download');
                    }
                }
            }

            if (tabId == 218) { // Incoming Collection Amendments
                if (button.get_commandName() == "print" && !clickCalledAfterRadconfirm218) {
                    args.set_cancel(true);

                    showPhieuNhap_Xuat();
                    //if (createMT410Tab === 'YES') {
                    //    radconfirm("Do you want to download MT410 file?", confirmCallbackFunction_MT410_Amendments, 350, 150, null, 'Download');
                    //} else {
                    //    showPhieuNhap_Xuat();
                    //}
                }
            }
            
            if (tabId == 219) { // Documentary Collection Cancel
                if (button.get_commandName() == "print" && !clickCalledAfterRadconfirm219) {
                    args.set_cancel(true);
                    radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file?", confirmCallbackFunction219_1, 420, 150, null, 'Download');
                }
            }
            
            if (tabId == 281) { // Incoming Collection Acception
                if (button.get_commandName() == "print" && !clickCalledAfterRadconfirm281) {
                    args.set_cancel(true);
                    radconfirm("Do you want to download MT412 file?", confirmCallbackFunction_IncomingCollectionAcception, 360, 150, null, 'Download');
                }
            }
        }
        
        function confirmCallbackFunction1(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnMT410Report.ClientID %>").click();
            }
            radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file?", confirmCallbackFunction2, 420, 150, null, 'Download');
        }
        
        function confirmCallbackFunction2(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnPhieuNgoaiBangReport.ClientID %>").click();
            }
            if (chargeAmount > 0) {
                radconfirm("Do you want to download VAT file?", confirmCallbackFunction3, 400, 150, null, 'Download');    
            }
        }
        
        function confirmCallbackFunction3(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnVATReport.ClientID %>").click();
            }
        }
        
        // Incoming Collection Amendments =================================================================================================
        function showPhieuNhap_Xuat() {
            // Neu amount > amount_old -> tu chinh tang tienb, xuat phieu [nhap ngoai bang]
            //amount < amount_Old -> tu chinh giam tien,xuat phieu [xuat phieu ngoai bang]
            // amount = amoun_old -> ko xuat phieu xuat nhap ngoai bang
            if (amount_Old == amount) {
                if (chargeAmount > 0) {
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
            } else if (chargeAmount > 0) {
                radconfirm("Do you want to download VAT file?", confirmCallbackFunction_VAT_Amendments, 350, 150, null, 'Download');
            }
        }

        function confirmCallbackFunction_MT410_Amendments(result) {
            clickCalledAfterRadconfirm218 = false;
            if (result) {
                $("#<%=btnIncomingCollectionAmendmentsMT410.ClientID %>").click();
            }

            showPhieuNhap_Xuat();
        }

        function confirmCallbackFunction_XuatNgoaiBang_Amendments(result) {
            clickCalledAfterRadconfirm218 = false;
            if (result) {
                $("#<%=btnIncomingCollectionAmendmentsPHIEUXUATNGOAIBANG.ClientID %>").click();
            }
            
            if (chargeAmount > 0) {
                radconfirm("Do you want to download VAT file?", confirmCallbackFunction_VAT_Amendments, 420, 150, null, 'Download');
            }
        }
        
        function confirmCallbackFunction_NhapNgoaiBang_Amendments(result) {
            clickCalledAfterRadconfirm218 = false;
            if (result) {
                $("#<%=btnIncomingCollectionAmendmentsPHIEUNHAPNGOAIBANG.ClientID %>").click();
            }
            if (chargeAmount > 0) {
                radconfirm("Do you want to download VAT file?", confirmCallbackFunction_VAT_Amendments, 420, 150, null, 'Download');    
            }
        }
        
        function confirmCallbackFunction_VAT_Amendments(result) {
            clickCalledAfterRadconfirm218 = false;
            if (result) {
                $("#<%=btnIncomingCollectionAmendmentsVAT.ClientID %>").click();
            }
        }
        // Incoming Collection Amendments =================================================================================================
        
        // Documentary Collection Cancel ==================================================================================================
        function confirmCallbackFunction219_1(result) {
            clickCalledAfterRadconfirm218 = false;
            if (result) {
                $("#<%=btnCancelDocumentaryPHIEUXUATNGOAIBANG.ClientID %>").click();
            }
            if (chargeAmount > 0) {
                radconfirm("Do you want to download VAT file?", confirmCallbackFunction219_2, 350, 150, null, 'Download');
            }
        }
        
        function confirmCallbackFunction219_2(result) {
            clickCalledAfterRadconfirm218 = false;
            if (result) {
                $("#<%=btnCancelDocumentaryVAT.ClientID %>").click();
            }
        }
        // Documentary Collection Cancel ==================================================================================================
        
        // Incoming Collection Acception ==================================================================================================
        function confirmCallbackFunction_IncomingCollectionAcception(result) {
            clickCalledAfterRadconfirm281 = false;
            if (result) {
                $("#<%=btnIncomingCollectionAcceptionMT412.ClientID %>").click();
            }
            if (chargeAmount > 0) {
                radconfirm("Do you want to download VAT file?", confirmCallbackFunction_IncomingCollectionAcception_2, 350, 150, null, 'Download');    
            }
        }
        
        function confirmCallbackFunction_IncomingCollectionAcception_1(result) {
            clickCalledAfterRadconfirm281 = false;
            if (result) {
                $("#<%=btnIncomingCollectionAcceptionPHIEUNHAPNGOAIBANG.ClientID %>").click();
            }
            if (chargeAmount > 0) {
                radconfirm("Do you want to download VAT file?", confirmCallbackFunction_IncomingCollectionAcception_2, 350, 150, null, 'Download');    
            }
        }
        
        function confirmCallbackFunction_IncomingCollectionAcception_2(result) {
            clickCalledAfterRadconfirm281 = false;
            if (result) {
                $("#<%=btnIncomingCollectionAcceptionVAT.ClientID %>").click();
            }
        }
        // Incoming Collection Acception ==================================================================================================
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
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtCode" runat="server" Width="200" />&nbsp;<asp:Label ID="lblError" runat="server" ForeColor="red" />
        </td>
    </tr>
</table>

<div class="dnnForm" id="tabs-demo">
    <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
    <ul class="dnnAdminTabNav">
        <li><a href="#Main" id="tabMain">Main</a></li>
        <% if (TabId == 217) %>
        <%{ %>
            <li><a href="#TabMT410">MT410</a></li>
        <% }
           else if (TabId == 281) %>
                <%{%>
               <li><a href="#TabMT410">MT412</a></li>
          <% } %>
        <li><a href="#Charges" id="tabCharges">Charges</a></li>
    </ul>
    </telerik:RadCodeBlock>
    <div id="Main" class="dnnClear">
        <div runat="server" id="divDocumentaryCollectionCancel">
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
        
        <div ID="divIncomingCollectionAcception" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Accepted Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteAcceptedDate" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Remarks</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAcceptedRemarks" runat="server" Width="355" />
                    </td>
                </tr>
            </table>
        </div>


        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Remitting Bank Information</div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Collection Type<span class="Required"> (*)</span></td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadComboBox Width="355" DropDownCssClass="KDDL" AppendDataBoundItems="True"
                            ID="comboCollectionType" runat="server" AutoPostBack="True" 
                            OnSelectedIndexChanged="comboCollectionType_OnSelectedIndexChanged"
                            MarkFirstMatch="True" OnItemDataBound="commom_ItemDataBound"
                            AllowCustomText="false">
                            <HeaderTemplate>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 100px;">Id
                                        </td>
                                        <td>Description
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
                                            <%# DataBinder.Eval(Container.DataItem, "Description")%> 
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator1"
                            ControlToValidate="comboCollectionType"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Collection Type is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblCollectionTypeName" runat="server" Text="" />
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Remitting Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox AutoPostBack="True" OnSelectedIndexChanged="comboRemittingType_OnSelectedIndexChanged"
                            ID="comboRemittingType" runat="server"
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
                    <td class="MyLable">Remitting Bank No</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadTextBox ID="txtRemittingBankNo" runat="server"
                            AutoPostBack="True" OnTextChanged="txtRemittingBankNo_OnTextChanged"/>
                    </td>
                    <td>
                        <asp:Label ID="lblRemittingBankNoError" runat="server" Text="" ForeColor="red" />
                        <asp:Label ID="lblRemittingBankName" runat="server" Text="" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Remitting Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtRemittingBankAddr1" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Remitting Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtRemittingBankAddr2" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Remitting Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtRemittingBankAddr3" runat="server" Width="355" />
                    </td>
                </tr>

                <tr style="display: none;">
                    <td class="MyLable">Remitting Bank Acct</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboRemittingBankAcct" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Remitting Bank Ref<span class="Required"> (*)</span></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtRemittingBankRef" runat="server" Width="355"
                            AutoPostBack="True" OnTextChanged="txtRemittingBankRef_OnTextChanged" />
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator2"
                            ControlToValidate="txtRemittingBankRef"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Remitting Bank Ref is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>

        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Drawee Information</div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
                <tr>
                    <td class="MyLable">Drawee Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox AutoPostBack="True" OnSelectedIndexChanged="comboDraweeType_OnSelectedIndexChanged"
                            ID="comboDraweeType" runat="server"
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
                    <td class="MyLable">Drawee Cus No</td>
                    <td class="MyContent">
                        <telerik:RadComboBox Width="355" DropDownCssClass="KDDL"
                            AppendDataBoundItems="True" AutoPostBack="true"
                            OnSelectedIndexChanged="comboDraweeCusNo_SelectIndexChange"
                            ID="comboDraweeCusNo" runat="server" OnItemDataBound="comboDraweeCusNo_ItemDataBound"
                            MarkFirstMatch="True" Height="150px"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <HeaderTemplate>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 100px;">Customer Id
                                        </td>
                                        <td>Customer Name
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 100px;">
                                            <%# DataBinder.Eval(Container.DataItem, "CustomerID")%> 
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "CustomerName2")%> 
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Drawee Cus Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeCusName" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Drawee Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeAddr1" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Drawee Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeAddr2" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Drawee Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeAddr3" runat="server" Width="355" />
                    </td>
                </tr>

                <tr style="display: none;">
                    <td class="MyLable">Reimb Drawee Acct</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboReimDraweeAcct" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>

        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Drawer Details</div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
                <tr>
                    <td class="MyLable">Drawer Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox AutoPostBack="True" OnSelectedIndexChanged="comboDrawerType_OnSelectedIndexChanged"
                            ID="comboDrawerType" runat="server"
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
                    <td class="MyLable">Drawer Cus No.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDrawerCusNo" runat="server" Width="355" ReadOnly="True" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Drawer Cus Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDrawerCusName" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Drawer Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDrawerAddr" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Drawer Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDrawerAddr1" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Drawer Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDrawerAddr2" runat="server" Width="355" />
                    </td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">General Collection Information</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Currency<span class="Required"> (*)</span></td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            AutoPostBack="True"
                            OnSelectedIndexChanged="comboCurrency_OnSelectedIndexChanged"
                            ID="comboCurrency" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
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
                            ID="RequiredFieldValidator3"
                            ControlToValidate="comboCurrency"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Currency is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Amount<span class="Required"> (*)</span></td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numAmount" runat="server" AutoPostBack="True" OnTextChanged="numAmount_OnTextChanged" />
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator4"
                            ControlToValidate="numAmount"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Amount is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>

                <div runat="server" id="divAmount">
                    <tr>
                        <td class="MyLable">Amount Old</td>
                        <td class="MyContent">
                            <asp:Label ID="lblAmount_New" runat="server" ForeColor="#0091E1" />
                        </td>
                    </tr>
                </div>

                <tr>
                    <td class="MyLable">Docs Received Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteDocsReceivedDate" runat="server" AutoPostBack="True"
                            OnSelectedDateChanged="dteDocsReceivedDate_OnSelectedDateChanged" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Maturity Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteMaturityDate" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Tenor</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtTenor" runat="server" Text="AT SIGHT" />
                    </td>
                    <td></td>
                </tr>

                <div runat="server" id="divTenor">
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <asp:Label ID="lblTenor_New" runat="server" ForeColor="#0091E1" />
                        </td>
                    </tr>
                </div>

                <tr style="display: none;">
                    <td class="MyLable">Days</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numDays" runat="server" MaxValue="999" MaxLength="3">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Tracer Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteTracerDate" runat="server" />
                    </td>
                </tr>

                <div runat="server" id="divTracerDate">
                    <tr>
                        <td class="MyLable"></td>
                        <td class="MyContent">
                            <asp:Label ID="lblTracerDate_New" runat="server" ForeColor="#0091E1" />
                        </td>
                    </tr>
                </div>
                <tr style="display: none">
                    <td class="MyLable">Reminder Days</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numReminderDays" runat="server" MaxValue="999">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Account Officer</td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="comboAccountOfficer" HighlightTemplatedItems="true" AppendDataBoundItems="True"
                            MarkFirstMatch="True"
                            AllowCustomText="false" runat="server">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Commodity<span class="Required"> (*)</span></td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadComboBox Width="355" DropDownCssClass="KDDL"
                            AppendDataBoundItems="True" AutoPostBack="true"
                            OnSelectedIndexChanged="comboCommodity_SelectIndexChange"
                            ID="comboCommodity" runat="server" OnItemDataBound="comboCommodity_ItemDataBound"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <HeaderTemplate>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 100px;">Id
                                        </td>
                                        <td>Description
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 100px;">
                                            <%# DataBinder.Eval(Container.DataItem, "ID")%> 
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Name")%> 
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator5"
                            ControlToValidate="comboCommodity"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Commodity is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="txtCommodityName" runat="server" /></td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Docs Code</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadComboBox Width="355"
                            AppendDataBoundItems="True"
                            ID="comboDocsCode1" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="btAddDocsCode" runat="server" OnClick="btAddDocsCode_Click" ImageUrl="~/Icons/Sigma/Add_16X16_Standard.png" />
                        <span id="spChargeCode">Add more</span></td>
                </tr>

                <tr>
                    <td class="MyLable">No. of Originals</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numNoOfOriginals1" runat="server" MaxValue="999" MaxLength="3">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">No. of Copies</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numNoOfCopies1" runat="server" MaxValue="999" MaxLength="3">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
            </table>

            <div runat="server" id="divDocsCode">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable">Docs Code</td>
                        <td class="MyContent">
                            <telerik:RadComboBox Width="355"
                                AppendDataBoundItems="True"
                                ID="comboDocsCode2" runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                                <%--<HeaderTemplate>
		                            <table cellpadding="0" cellspacing="0"> 
		                                <tr> 
			                                <td style="width:100px;"> 
				                            Id
			                                </td> 
			                                <td> 
				                            Description
			                                </td>
		                                </tr> 
	                                </table> 
                                </HeaderTemplate>
	                            <ItemTemplate>
			                        <table  cellpadding="0" cellspacing="0"> 
			                            <tr> 
				                            <td style="width:100px;"> 
					                        <%# DataBinder.Eval(Container.DataItem, "Id")%> 
				                            </td> 
				                            <td> 
					                        <%# DataBinder.Eval(Container.DataItem, "Description")%> 
				                            </td>
			                            </tr> 
		                            </table> 
                                </ItemTemplate>--%>
                            </telerik:RadComboBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="MyLable">No. of Originals</td>
                        <td class="MyContent">
                            <telerik:RadNumericTextBox ID="numNoOfOriginals2" runat="server" MaxValue="999" MaxLength="3">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="MyLable">No. of Copies</td>
                        <td class="MyContent">
                            <telerik:RadNumericTextBox ID="numNoOfCopies2" runat="server" MaxValue="999" MaxLength="3">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                </table>
            </div>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Other Docs</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" Height="100" ID="txtOtherDocs" runat="server" TextMode="MultiLine" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Instruction to Cus</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" Height="100" ID="txtInstructionToCus" runat="server" TextMode="MultiLine" />
                    </td>
                </tr>

                <tr style="display: none;">
                    <td class="MyLable">Remarks</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="txtRemarks" runat="server" />
                    </td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Other Infor</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Express No</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="txtExpressNo" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Invoice No</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="txtInvoiceNo" runat="server" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>

    <div id="TabMT410" class="dnnClear">
        <div ID="divTM410" runat="server">
            <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;"></div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable"><telerik:RadCodeBlock ID="RadCodeBlock4" runat="server"><%= TabId == 281 ? "Create MT412" : "Create MT410" %></telerik:RadCodeBlock></td>
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

                <tr style="display: none;">
                    <td class="MyLable">General MT 410/MTx99?</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtGeneralMT410_2" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Sending Bank's TRN</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtSendingBankTRN" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Related Reference</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtRelatedReference" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Currency Code</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboCurrency_TabMT410" runat="server"
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
                </tr>

                <tr>
                    <td class="MyLable">Amount</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numAmount_TabMT410" runat="server" />
                    </td>
                </tr>
            </table>
            <uc1:VVTextBox runat="server" id="txtSenderToReceiverInfo" VVTLabel="Sender to Receiver Info" Width="355" />
        </fieldset>
        </div>
    </div>

    <div id="Charges" class="dnnClear">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
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

                    <div id="tableANHien" runat="server">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="MyLable">Charge code</td>
                                <td class="MyContent">
                                    <telerik:RadComboBox AppendDataBoundItems="True"
                                        ID="tbChargeCode" runat="server"
                                        MarkFirstMatch="True" Width="150"
                                        AllowCustomText="false">
                                        <ExpandAnimation Type="None" />
                                        <CollapseAnimation Type="None" />
                                    </telerik:RadComboBox>
                                    <asp:ImageButton ID="btThem" runat="server" OnClick="btThem_Click" ImageUrl="~/Icons/Sigma/Add_16X16_Standard.png" />
                                </td>
                            </tr>

                            <tr>
                                <td class="MyLable">Charge Currency</td>
                                <td class="MyContent">
                                    <telerik:RadComboBox
                                        ID="rcbChargeCcy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rcbChargeCcy_OnSelectedIndexChanged"
                                        MarkFirstMatch="True" Width="150"
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
                            </tr>
                        </table>

                        <table width="100%" cellpadding="0" cellspacing="0" id="table1" runat="server">
                            <tr>
                                <td class="MyLable">Charge Acct</td>
                                <td class="MyContent">
                                    <telerik:RadComboBox AutoPostBack="true" DropDownCssClass="KDDL"
                                        AppendDataBoundItems="True"
                                        OnSelectedIndexChanged="rcbChargeAcct_SelectIndexChange"
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
                                    <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" ID="tbExcheRate" Width="200px" />
                                </td>
                            </tr>

                            <tr>
                                <td class="MyLable">Charge Amt</td>
                                <td class="MyContent">
                                    <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server" 
                                        ID="tbChargeAmt" Width="200px" AutoPostBack="true"
                                        OnTextChanged="tbChargeAmt_TextChanged" />

                                </td>
                            </tr>
                        </table>

                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="MyLable">Party Charged</td>
                                <td class="MyContent" style="width: 150px;">
                                    <telerik:RadComboBox AutoPostBack="true"
                                        OnSelectedIndexChanged="rcbPartyCharged_SelectIndexChange"
                                        ID="rcbPartyCharged" runat="server"
                                        MarkFirstMatch="True" Width="150"
                                        AllowCustomText="false">
                                        <ExpandAnimation Type="None" />
                                        <CollapseAnimation Type="None" />
                                        <Items>
                                            <telerik:RadComboBoxItem Value="Openner" Text="A" />
                                            <telerik:RadComboBoxItem Value="Correspondent Charges for the Openner" Text="AC" />
                                            <telerik:RadComboBoxItem Value="Beneficiary" Text="B" />
                                            <telerik:RadComboBoxItem Value="Correspondent Charges for the Beneficiary" Text="BC" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblPartyCharged" runat="server" Text="Openner" /></td>
                            </tr>
                        </table>

                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="MyLable">Amort Charges</td>
                                <td class="MyContent">
                                    <telerik:RadComboBox
                                        ID="rcbOmortCharge" runat="server"
                                        MarkFirstMatch="True" Width="150"
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
                        </table>
                    </div>

                    <div id="divCharge2" runat="server" style="border-top: 1px solid #CCC;">
                        <table width="100%" cellpadding="0" cellspacing="0" id="table2" runat="server">
                            <tr>
                                <td class="MyLable">Charge code</td>
                                <td class="MyContent">
                                    <telerik:RadComboBox
                                        ID="tbChargecode2" runat="server" AppendDataBoundItems="True"
                                        MarkFirstMatch="True" Width="150"
                                        AllowCustomText="false">
                                        <ExpandAnimation Type="None" />
                                        <CollapseAnimation Type="None" />
                                    </telerik:RadComboBox>
                                    <asp:ImageButton ID="btnChargecode2" runat="server" OnClick="btnChargecode2_Click" ImageUrl="~/Icons/Sigma/Delete_16X16_Standard.png" />
                                    <span id="spChargeCode2"></span>
                                </td>
                            </tr>

                            <tr>
                                <td class="MyLable">Charge Currency</td>
                                <td class="MyContent">
                                    <telerik:RadComboBox
                                        ID="rcbChargeCcy2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rcbChargeCcy2_OnSelectedIndexChanged"
                                        MarkFirstMatch="True" Width="150"
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
                            </tr>
                        </table>

                        <table width="100%" cellpadding="0" cellspacing="0" id="table3" runat="server">
                            <tr>
                                <td class="MyLable">Charge Acct</td>
                                <td class="MyContent">
                                    <telerik:RadComboBox AutoPostBack="true" AppendDataBoundItems="True" DropDownCssClass="KDDL"
                                        OnSelectedIndexChanged="rcbChargeAcct2_SelectIndexChange"
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
                                <%--<td><asp:Label ID="lblChargeAcct2" runat="server" /></td>--%>
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
                                    <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true" IncrementSettings-InterceptMouseWheel="true" runat="server"
                                        ID="tbChargeAmt2" Width="200px" AutoPostBack="true"
                                        OnTextChanged="tbChargeAmt2_TextChanged" />

                                </td>
                            </tr>
                        </table>

                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="MyLable">Party Charged</td>
                                <td class="MyContent" style="width: 150px;">
                                    <telerik:RadComboBox AutoPostBack="true"
                                        OnSelectedIndexChanged="rcbPartyCharged2_SelectIndexChange"
                                        ID="rcbPartyCharged2" runat="server"
                                        MarkFirstMatch="True" Width="150"
                                        AllowCustomText="false">
                                        <ExpandAnimation Type="None" />
                                        <CollapseAnimation Type="None" />
                                        <Items>
                                            <telerik:RadComboBoxItem Value="Openner" Text="A" />
                                            <telerik:RadComboBoxItem Value="Correspondent Charges for the Openner" Text="AC" />
                                            <telerik:RadComboBoxItem Value="Beneficiary" Text="B" />
                                            <telerik:RadComboBoxItem Value="Correspondent Charges for the Beneficiary" Text="BC" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblPartyCharged2" runat="server" Text="Openner" /></td>
                            </tr>
                        </table>

                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="MyLable">Amort Charges</td>
                                <td class="MyContent">
                                    <telerik:RadComboBox
                                        ID="rcbOmortCharges2" runat="server"
                                        MarkFirstMatch="True" Width="150"
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
                                        OnSelectedIndexChanged="rcbChargeStatus2_SelectIndexChange"
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
                        </table>
                    </div>

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
                                <asp:TextBox ID="tbVatNo" runat="server" ReadOnly="True" Width="300" />
                            </td>
                        </tr>
                    </table>

                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Tax Code</td>
                            <td class="MyContent">
                                <asp:Label ID="lblTaxCode" runat="server" />
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="MyLable">Tax Ccy</td>
                            <td class="MyContent">
                                <asp:Label ID="lblTaxCcy" runat="server" />
                            </td>
                        </tr>
                       <%-- <%if (TabId != 281) {%>--%>
                        <tr>
                            <td class="MyLable">Tax Amt</td>
                            <td class="MyContent">
                                <asp:Label ID="lblTaxAmt" runat="server" />
                            </td>
                        </tr>
                       <%--<% } %>--%>
                        <tr>
                            <td class="MyLable">Tax in LCCY Amt</td>
                            <td class="MyContent"></td>
                        </tr>
                        <tr>
                            <td class="MyLable">Tax Date</td>
                            <td class="MyContent"></td>
                        </tr>
                    </table>

                    <div id="divChargeInfo2" runat="server" style="border-top: 1px solid #CCC;">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="MyLable">Tax Code</td>
                                <td class="MyContent">
                                    <asp:Label ID="lblTaxCode2" runat="server" />
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td class="MyLable">Tax Ccy</td>
                                <td class="MyContent">
                                    <asp:Label ID="lblTaxCcy2" runat="server" />
                                </td>
                            </tr>
                            <%--<%if (TabId != 281) {%>--%>
                            <tr>
                                <td class="MyLable">Tax Amt</td>
                                <td class="MyContent">
                                    <asp:Label ID="lblTaxAmt2" runat="server" />
                                </td>
                            </tr>
                            <%--<% } %>--%>
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
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

<%--    <div id="DeliveryAudit" class="dnnClear"></div>

    <div id="FullView" class="dnnClear"></div>--%>
</div>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="comboDraweeCusNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtDraweeCusName" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="comboCommodity">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtCommodityName" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="comboDraweeType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="comboDraweeCusNo" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeCusName" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtDraweeAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="comboDrawerType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtDrawerCusNo" />
                <telerik:AjaxUpdatedControl ControlID="txtDrawerAddr" />
                <telerik:AjaxUpdatedControl ControlID="txtDrawerAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtDrawerAddr2" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="comboRemittingType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="comboRemittingBankNo" />
                <telerik:AjaxUpdatedControl ControlID="txtRemittingBankAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtRemittingBankAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtRemittingBankAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="dteDocsReceivedDate">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="dteTracerDate" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="txtRemittingBankRef">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtRelatedReference" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="comboCurrency">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="comboCurrency_TabMT410" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="numAmount">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="numAmount_TabMT410" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var tabId = <%= TabId %>;
        
        function ChargecodeChange(loai) {
            if (loai == 1) {
                var code = $("#<%=tbChargeCode.ClientID%>").val();
                if (code.toUpperCase() == "TFILCOPEN") {
                    $("#spChargeCode").text("LC Open Comm");
                }

            }
            else {
                var code = $("#<%=tbChargecode2.ClientID%>").val();
                if (code.toUpperCase() == "TFILCOPEN") {
                    $("#spChargeCode2").text("LC Open Comm");
                }

            }
        };
        
        $(document).ready(
            function () {
                $('a.add').live('click',
                    function () {
                        $(this)
                            .html('<img src="Icons/Sigma/Delete_16X16_Standard.png" />')
                            .removeClass('add')
                            .addClass('remove');
                        $(this)
                            .closest('tr')
                            .clone()
                            .appendTo($(this).closest('table'));
                        $(this)
                            .html('<img src="Icons/Sigma/Add_16X16_Standard.png" />')
                            .removeClass('remove')
                            .addClass('add');
                    });
                $('a.remove').live('click',
                    function () {
                        $(this)
                            .closest('tr')
                            .remove();
                    });
                $('input:text').each(
                    function () {
                        var thisName = $(this).attr('name'),
                            thisRrow = $(this)
                                .closest('tr')
                                .index();
                        $(this).attr('name', 'row' + thisRow + thisName);
                        $(this).attr('id', 'row' + thisRow + thisName);
                    });

            });
        $("#<%=txtCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                window.location.href = "Default.aspx?tabid=" + tabId + "&CodeID=" + $("#<%=txtCode.ClientID %>").val();
            }
        });
    </script>
</telerik:RadCodeBlock>
<div style="visibility: hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnMT410Report" runat="server" OnClick="btnMT410Report_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnPhieuNgoaiBangReport" runat="server" OnClick="btnPhieuNgoaiBangReport_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnVATReport" runat="server" OnClick="btnVATReport_Click" Text="Search" /></div>

<div style="visibility:hidden;"><asp:Button ID="btnIncomingCollectionAmendmentsPHIEUXUATNGOAIBANG" runat="server" OnClick="btnIncomingCollectionAmendmentsPHIEUXUATNGOAIBANG_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnIncomingCollectionAmendmentsPHIEUNHAPNGOAIBANG" runat="server" OnClick="btnIncomingCollectionAmendmentsPHIEUNHAPNGOAIBANG_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnIncomingCollectionAmendmentsVAT" runat="server" OnClick="btnIncomingCollectionAmendmentsVAT_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnIncomingCollectionAmendmentsMT410" runat="server" OnClick="btnIncomingCollectionAmendmentsMT410_Click" Text="Search" /></div>

<div style="visibility:hidden;"><asp:Button ID="btnCancelDocumentaryPHIEUXUATNGOAIBANG" runat="server" OnClick="btnCancelDocumentaryPHIEUXUATNGOAIBANG_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnCancelDocumentaryVAT" runat="server" OnClick="btnCancelDocumentaryVAT_Click" Text="Search" /></div>

<div style="visibility:hidden;"><asp:Button ID="btnIncomingCollectionAcceptionMT412" runat="server" OnClick="btnIncomingCollectionAcceptionMT412_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnIncomingCollectionAcceptionVAT" runat="server" OnClick="btnIncomingCollectionAcceptionVAT_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnIncomingCollectionAcceptionPHIEUNHAPNGOAIBANG" runat="server" OnClick="btnIncomingCollectionAcceptionPHIEUNHAPNGOAIBANG_Click" Text="Search" /></div>