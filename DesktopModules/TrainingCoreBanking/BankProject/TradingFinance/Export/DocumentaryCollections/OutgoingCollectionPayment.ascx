<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OutgoingCollectionPayment.ascx.cs" Inherits="BankProject.TradingFinance.Export.DocumentaryCollections.OutgoingCollectionPayment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:radwindowmanager id="RadWindowManager1" runat="server" enableshadow="true" />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />

<telerik:radcodeblock id="RadCodeBlock2" runat="server">
    <script type="text/javascript" src="DesktopModules/TrainingCoreBanking/BankProject/Scripts/Common.js"></script>
    <script type="text/javascript">
        jQuery(function ($) {
            $('#tabs-demo').dnnTabs({ selected: 0 });
        });




        function MainToolbar_OnClientButtonClicking(sender, args) {

            var button = args.get_item();
            if (button.get_commandName() == '<%=BankProject.Controls.Commands.Print%>') {
                args.set_cancel(true);
                radconfirm("Do you want to download PHIEU CHUYEN KHOAN file ?", confirmCallbackFunction_PhieuChuyenKhoan, 420, 150, null, 'Download');
            }
            //
            if (button.get_commandName() == "save") {
                args.set_cancel(true);//Khong cho commit
                //
                if (!MTIsValidInput('MT910', null)) return;
                //                
                args.set_cancel(false);//Cho phép commit
            }
            //

        }
        function confirmCallbackFunction_PhieuChuyenKhoan(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnReportPhieuChuyenKhoan.ClientID %>").click();
            }
            radconfirm("Do you want to download PHIEU VAT ?", confirmCallbackFunction_PhieuVAT, 365, 150, null, 'Download');
        }
        function confirmCallbackFunction_PhieuVAT(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnReportVATb.ClientID %>").click();
            }
            radconfirm("Do you want to download PHIEU XUAT NGOAI BANG ?", confirmCallbackFunction_PhieuXuatNgoaiBang, 420, 150, null, 'Download');
        }

        
        function confirmCallbackFunction_PhieuXuatNgoaiBang(result) {
            clickCalledAfterRadconfirm = false;
            if(result) {
                $("#<%=btnReportPhieuXuatNgoaiBang.ClientID %>").click();
            }
        }
    </script>
</telerik:radcodeblock>
<telerik:radtoolbar runat="server" id="mainToolbar" enableroundedcorners="true" enableshadows="true" width="100%"
    onbuttonclick="MainToolbar_ButtonClick" onclientbuttonclicking="MainToolbar_OnClientButtonClicking">
    <items>
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
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print" postback="false">
        </telerik:RadToolBarButton>
    </items>
</telerik:radtoolbar>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtCode" runat="server" Width="200" />
            &nbsp;<asp:Label ID="lblError" runat="server" ForeColor="red" />
        </td>
    </tr>
</table>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#Main" id="tabMain">Main</a></li>
        <li><a href="#MT910" id="A1">MT 910</a></li>
        <li><a href="#Charges" id="tabCharges">Charges</a></li>
    </ul>
    <div id="Main" class="dnnClear">
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Payment Information</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">1.1 Draw Type
                    </td>
                    <td style="width: 150px;" class="MyContent">
                        <telerik:radcombobox width="355" dropdowncssclass="KDDL" appenddatabounditems="True"
                            id="comboDrawType" runat="server"
                            autopostback="True"
                            markfirstmatch="True"
                            onselectedindexchanged="comboDrawType_OnSelectedIndexChanged"
                            onitemdatabound="commom_ItemDataBound"
                            allowcustomtext="false">
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
                        </telerik:radcombobox>

                        <td>
                            <asp:Label ID="lblDrawType" runat="server" />
                        </td>
                    </td>
                </tr>

            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tbody>
                    <tr>
                        <td class="MyLable">1.2 Value Date
                        </td>
                        <td class="MyContent">
                            <telerik:raddatepicker id="dtValueDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">1.3 Drawing Amount
                        </td>
                        <td class="MyContent">
                            <telerik:radnumerictextbox id="numDrawingAmount" runat="server" value="0" />
                            <asp:RangeValidator
                                runat="server" Display="None"
                                ID="rvDrawingAmount"
                                ControlToValidate="numDrawingAmount"
                                ValidationGroup="Commit"
                                MinimumValue="0.01"
                                MaximumValue="99999999999"
                                ErrorMessage="Drawing Amount must be greater than 0" ForeColor="Red">
                            </asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="MyLable">1.4 Country Code</td>
                        <td class="MyContent">
                            <telerik:radcombobox width="355" appenddatabounditems="True"
                                id="comboCountryCode" runat="server"
                                autopostback="False"
                                onselectedindexchanged="comboCountryCode_OnSelectedIndexChanged"
                                markfirstmatch="True"
                                allowcustomtext="false">
                        </telerik:radcombobox>
                        </td>
                        <td>
                            <asp:Label ID="lblCountryCodeName" runat="server" />
                    </tr>
                    <tr>
                        <td class="MyLable">1.5 Amount Credited
                        </td>
                        <td class="MyContent">
                            <asp:Label ID="lblCreditAmount" runat="server" />
                        </td>
                    </tr>
                </tbody>
            </table>

            <%-- Hien Nguyen _ comment code to fix bug 65 start --%>
            <%--<table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">1.6 Payment Method
                    </td>
                    <td style="width: 150px;" class="MyContent">
                        <telerik:radcombobox width="355" dropdowncssclass="KDDL" appenddatabounditems="True"
                            id="comboPaymentMethod" runat="server"
                            autopostback="True"
                            markfirstmatch="True"
                            onselectedindexchanged="comboPaymentMethod_OnSelectedIndexChanged"
                            onitemdatabound="comboPaymentMethod_ItemDataBound"
                            allowcustomtext="false">
                            <HeaderTemplate>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 100px;">Code
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
                                            <%# DataBinder.Eval(Container.DataItem, "Code")%> 
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Description")%> 
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:radcombobox>

                        <td>
                            <asp:Label ID="lblPaymentMethod" runat="server" />
                        </td>
                    </td>
                </tr>

            </table>--%>
            <%-- Hien Nguyen _ comment code to fix bug 65 ends --%>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">1.7 Credit Currency<span class="Required"> (*)</span></td>
                    <td class="MyContent">
                        <telerik:radcombobox
                            id="comboCreditCurrency" runat="server"
                            autopostback="True"
                            onselectedindexchanged="comboCreditCurrency_OnSelectedIndexChanged"
                            markfirstmatch="True"
                            allowcustomtext="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="USD" Text="USD" />
                                <telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                                <telerik:RadComboBoxItem Value="VND" Text="VND" />
                            </Items>
                        </telerik:radcombobox>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="rfvCreditCurrency"
                            ControlToValidate="comboCreditCurrency"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Currency is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">1.8 Nostro Account
                    </td>
                    <td style="width: 150px;" class="MyContent">
                        <telerik:radcombobox width="355" dropdowncssclass="KDDL" appenddatabounditems="True"
                            id="cbNostroAccount" runat="server"
                            autopostback="True"
                            markfirstmatch="True"
                            onselectedindexchanged="cbNostroAccount_OnSelectedIndexChanged"
                            onitemdatabound="cbNostroAccount_ItemDataBound"
                            ondatabound="cbNostroAccount_DataBound"
                            allowcustomtext="false">
                            <HeaderTemplate>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 100px;">Account No
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
                                            <%# DataBinder.Eval(Container.DataItem, "AccountNo")%> 
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Description")%> 
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:radcombobox>

                        <td>
                            <asp:Label ID="lblNostro" runat="server" />
                        </td>
                    </td>
                </tr>

            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">1.9 Credit Acct</td>
                    <td class="MyContent">
                        <telerik:radcombobox dropdowncssclass="KDDL"
                            appenddatabounditems="True"
                            onitemdatabound="comboCreditAcct_ItemDataBound"
                            id="comboCreditAcct" runat="server"
                            markfirstmatch="True" width="355"
                            allowcustomtext="false">
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
                                </telerik:radcombobox>
                    </td>
                </tr>

                <%-- Hien Nguyen fixed bug 66 start --%>
              <%--  <tr>
                    <td class="MyLable">1.10 Exchange Rate
                    </td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox id="numExchangeRate" runat="server" />
                    </td>
                </tr>--%>
                <%-- Hien Nguyen fixed bug 66 ends --%>

                <tr>
                    <td class="MyLable">1.11 Payment Remarks</td>
                    <td class="MyContent">
                        <telerik:radtextbox id="txtPaymentRemarks1" runat="server" width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:radtextbox id="txtPaymentRemarks2" runat="server" width="355" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Drawer Information</div>
            </legend>



            <div id="divCollectionType" runat="server">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable">2.1 Collection Type<span class="Required"> (*)</span></td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:radcombobox width="355" dropdowncssclass="KDDL" appenddatabounditems="True"
                                id="comboCollectionType" runat="server"
                                markfirstmatch="True"
                                enabled="false"
                                onitemdatabound="commom_ItemDataBound"
                                allowcustomtext="false">
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
                        </telerik:radcombobox>
                            <asp:RequiredFieldValidator
                                runat="server" Display="None"
                                ID="RequiredFieldValidator1"
                                ControlToValidate="comboCollectionType"
                                ValidationGroup="Commit"
                                InitialValue=""
                                Enabled="false"
                                ErrorMessage="Collection Type is required" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblCollectionTypeName" runat="server" Text="" />
                    </tr>
                </table>
            </div>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">2.2 Drawer Cus No.</td>
                    <td class="MyContent">
                        <telerik:radcombobox width="355" dropdowncssclass="KDDL"
                            appenddatabounditems="True" autopostback="true"
                            id="comboDrawerCusNo" runat="server"
                            onitemdatabound="comboDrawerCusNo_ItemDataBound"
                            markfirstmatch="True" height="150px"
                            enabled="false"
                            allowcustomtext="false">
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
                                            <%# DataBinder.Eval(Container.DataItem, "CustomerName")%> 
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:radcombobox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">2.3 Drawer Cus Name</td>
                    <td class="MyContent">
                        <telerik:radtextbox id="txtDrawerCusName" runat="server" width="355" enabled="false" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">2.4 Drawer Addr.</td>
                    <td class="MyContent">
                        <telerik:radtextbox id="txtDrawerAddr1" runat="server" width="355" enabled="false" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:radtextbox id="txtDrawerAddr2" runat="server" width="355" enabled="false" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:radtextbox id="txtDrawerAddr3" runat="server" width="355" enabled="false" />
                    </td>
                </tr>

                <tr style="display: none">
                    <td class="MyLable">2.4 Drawer Ref No.</td>
                    <td class="MyContent">
                        <telerik:radtextbox id="txtDrawerRefNo" runat="server" width="355" enabled="false" />
                    </td>
                </tr>
            </table>

        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Collecting Bank Details</div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">3.1 Collecting Bank No.</td>
                    <td class="MyContent">
                        <telerik:radcombobox dropdowncssclass="KDDL"
                            appenddatabounditems="True" width="450"
                            id="comboCollectingBankNo" runat="server"
                            markfirstmatch="True"
                            enabled="false"
                            allowcustomtext="false">
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
                                            <%# DataBinder.Eval(Container.DataItem, "Code")%> 
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Description")%> 
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:radcombobox>
                    </td>
                    <%--<td><asp:Label ID="lblCollectingBankName" runat="server"  />--%>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">3.2 Collecting Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:radtextbox id="txtCollectingBankName" runat="server" width="355" enabled="false" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:radtextbox id="txtCollectingBankAddr1" runat="server" width="355" enabled="false" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:radtextbox id="txtCollectingBankAddr2" runat="server" width="355" enabled="false" />
                    </td>
                </tr>

                <tr style="display: none">
                    <td class="MyLable">3.3 Collecting Bank Acct</td>
                    <td class="MyContent">
                        <telerik:radcombobox
                            id="comboCollectingBankAcct" runat="server"
                            markfirstmatch="True"
                            enabled="false"
                            allowcustomtext="false">
                        </telerik:radcombobox>
                    </td>
                </tr>
            </table>

        </fieldset>
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Drawee/Reimbursement Detail</div>
            </legend>


            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display: none">
                    <td class="MyLable">4.1 Drawee Cus No</td>
                    <td class="MyContent">
                        <telerik:radtextbox id="txtDraweeCusNo" runat="server"
                            autopostback="True" width="355" enabled="False" />
                    </td>

                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">4.2 Drawee Cus Name</td>
                    <td class="MyContent">
                        <telerik:radtextbox id="txtDraweeCusName" runat="server" width="355" enabled="False" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">4.3 Drawee Addr.</td>
                    <td class="MyContent">
                        <telerik:radtextbox id="txtDraweeAddr1" runat="server" width="355" enabled="False" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:radtextbox id="txtDraweeAddr2" runat="server" width="355" enabled="False" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:radtextbox id="txtDraweeAddr3" runat="server" width="355" enabled="False" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display: none">
                    <td class="MyLable">4.4 Nostro Cus No</td>
                    <td style="width: 150px;" class="MyContent">
                        <telerik:radcombobox width="400" dropdowncssclasscombocommodity="KDDL"
                            appenddatabounditems="True" autopostback="true"
                            id="comboNostroCusNo" runat="server" onitemdatabound="commomSwiftCode_ItemDataBound"
                            markfirstmatch="True" height="150px"
                            enabled="False"
                            allowcustomtext="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
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
                                            <%# DataBinder.Eval(Container.DataItem, "Code")%> 
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Description")%> 
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:radcombobox>
                    </td>
                    <td>
                        <asp:Label ID="lblNostroCusName" runat="server" Width="100%" /></td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Collection Information</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">5 Currency<span class="Required"> (*)</span></td>
                    <td class="MyContent">
                        <telerik:radcombobox
                            id="comboCurrency" runat="server"
                            markfirstmatch="True"
                            enabled="false"
                            allowcustomtext="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="USD" Text="USD" />
                                <telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                                <telerik:RadComboBoxItem Value="GBP" Text="GBP" />
                                <telerik:RadComboBoxItem Value="JPY" Text="JPY" />
                                <telerik:RadComboBoxItem Value="VND" Text="VND" />
                            </Items>
                        </telerik:radcombobox>
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
                    <td class="MyLable">6 Amount<span class="Required"> (*)</span></td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox id="numAmount" runat="server" enabled="false" />
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
                <tr>
                    <td class="MyLable">7 Docs Received Date</td>
                    <td class="MyContent">
                        <telerik:raddatepicker id="dteDocsReceivedDate" runat="server" autopostback="True" enabled="false" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">8 Maturity Date</td>
                    <td class="MyContent">
                        <telerik:raddatepicker id="dteMaturityDate" runat="server" enabled="false" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">9 Tenor</td>
                    <td class="MyContent">
                        <telerik:radtextbox id="txtTenor" runat="server" text="AT SIGHT" enabled="false" />
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


                <tr>
                    <td class="MyLable">10 Tracer Date</td>
                    <td class="MyContent">
                        <telerik:raddatepicker id="dteTracerDate" runat="server" enabled="false" />
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
                <tr style="display: none;">
                    <td class="MyLable">11 Reminder Days</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox id="numReminderDays" runat="server" maxvalue="999" enabled="false">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:radnumerictextbox>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">13 Commodity<span class="Required"> (*)</span></td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:radcombobox width="355" dropdowncssclass="KDDL"
                            appenddatabounditems="True" autopostback="true"
                            id="comboCommodity" runat="server" onitemdatabound="comboCommodity_ItemDataBound"
                            markfirstmatch="True"
                            enabled="false"
                            allowcustomtext="false">
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
                                            <%# DataBinder.Eval(Container.DataItem, "Name2")%> 
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:radcombobox>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator5"
                            ControlToValidate="comboCommodity"
                            ValidationGroup="Commit"
                            Enabled="false"
                            InitialValue=""
                            ErrorMessage="Commodity is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="txtCommodityName" runat="server" /></td>
                </tr>
            </table>
            <div runat="server" id="divDocsCode">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable">14.1 Docs Code</td>
                        <td class="MyContent">
                            <telerik:radcombobox width="355"
                                appenddatabounditems="True"
                                enabled="false"
                                id="comboDocsCode1" runat="server"
                                markfirstmatch="True"
                                allowcustomtext="false">
                        </telerik:radcombobox>
                        </td>
                    </tr>

                    <tr>
                        <td class="MyLable">14.2 No. of Originals</td>
                        <td class="MyContent">
                            <telerik:radnumerictextbox id="numNoOfOriginals1" runat="server" maxvalue="999" maxlength="3" enabled="false">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:radnumerictextbox>
                        </td>
                    </tr>

                    <tr>
                        <td class="MyLable">14.3 No. of Copies</td>
                        <td class="MyContent">
                            <telerik:radnumerictextbox id="numNoOfCopies1" runat="server" maxvalue="999" maxlength="3" enabled="false">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:radnumerictextbox>
                        </td>
                    </tr>
                </table>
            </div>
            <div runat="server" id="divDocsCode2">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable">14.1.1 Docs Code</td>
                        <td class="MyContent">
                            <telerik:radcombobox width="355"
                                appenddatabounditems="True"
                                id="comboDocsCode2" runat="server"
                                markfirstmatch="True"
                                enabled="false"
                                allowcustomtext="false">
                            </telerik:radcombobox>

                        </td>
                    </tr>

                    <tr>
                        <td class="MyLable">14.2.1 No. of Originals</td>
                        <td class="MyContent">
                            <telerik:radnumerictextbox id="numNoOfOriginals2" runat="server" maxvalue="999" maxlength="3" enabled="false">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </telerik:radnumerictextbox>
                        </td>
                    </tr>

                    <tr>
                        <td class="MyLable">14.3.1 No. of Copies</td>
                        <td class="MyContent">
                            <telerik:radnumerictextbox id="numNoOfCopies2" runat="server" maxvalue="999" maxlength="3" enabled="false">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </telerik:radnumerictextbox>
                        </td>
                    </tr>
                </table>
            </div>
            <div runat="server" id="divDocsCode3">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable">14.1.2 Docs Code</td>
                        <td class="MyContent">
                            <telerik:radcombobox width="355"
                                appenddatabounditems="True"
                                id="comboDocsCode3" runat="server"
                                markfirstmatch="True"
                                enabled="false"
                                allowcustomtext="false">
                            </telerik:radcombobox>

                        </td>
                    </tr>

                    <tr>
                        <td class="MyLable">14.2.2 No. of Originals</td>
                        <td class="MyContent">
                            <telerik:radnumerictextbox id="numNoOfOriginals3" runat="server" maxvalue="999" maxlength="3" enabled="false">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </telerik:radnumerictextbox>
                        </td>
                    </tr>

                    <tr>
                        <td class="MyLable">14.3.2 No. of Copies</td>
                        <td class="MyContent">
                            <telerik:radnumerictextbox id="numNoOfCopies3" runat="server" maxvalue="999" maxlength="3" enabled="false">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </telerik:radnumerictextbox>
                        </td>
                    </tr>
                </table>
            </div>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">15 Other Docs</td>
                    <td class="MyContent">
                        <telerik:radtextbox width="100%" height="100" id="txtOtherDocs" runat="server" textmode="MultiLine" enabled="false" />
                    </td>
                </tr>


                <tr>
                    <td class="MyLable">16 Remarks</td>
                    <td class="MyContent">
                        <telerik:radtextbox width="355" id="txtRemarks" runat="server" enabled="false" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div id="MT910" class="dnnClear">
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">MT 910 Information</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">20. Transaction Reference Number</td>
                    <td class="MyContent">
                        <asp:TextBox ID="txtTransactionRefNumber" runat="server" Width="300" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">21. Related Reference</td>
                    <td class="MyContent">
                        <asp:TextBox ID="txtRelatedRef" runat="server" Width="300" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">25. Account Indentification</td>
                    <td class="MyContent">
                        <asp:TextBox ID="txtAccountIndentification" runat="server" Width="300" />
                    </td>
                </tr>
                <%-- remove Nostro Acct to fix bug 46 --%>
               <%-- <tr>
                    <td class="MyLable">Nostro Acct</td>
                    <td class="MyContent">
                        <telerik:radcombobox autopostback="false"
                            onitemdatabound="cboNostroAcct_ItemDataBound"
                            id="cboNostroAcct" runat="server"
                            markfirstmatch="True" width="300"
                            allowcustomtext="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />                            
                        </telerik:radcombobox>
                        <asp:Label ID="lblNostroAcctName" runat="server" /></td>
                </tr>--%>
                <tr>
                    <td class="MyLable">32A. Value Date</td>
                    <td class="MyContent">
                        <telerik:raddatepicker id="dtValueDateMt910" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">32A. Currency</td>
                    <td class="MyContent">
                        <telerik:radcombobox
                            id="comboCurrencyMt910" runat="server"
                            markfirstmatch="True" width="150"
                            allowcustomtext="false"
                            Enabled ="false">
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
                                    </telerik:radcombobox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">32A. Amount</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox id="numAmountMt910" runat="server" value="0" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">52A. Ordering Institution Name</td>
                    <td class="MyContent">
                        <asp:TextBox ID="txtOrderingInstitutionName" runat="server" Width="300" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="padding-left:30px;">Address 1</td>
                    <td class="MyContent">
                        <asp:TextBox ID="txtOrderingInstitutionAddress1" runat="server" Width="300" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="padding-left:30px;">Address 2</td>
                    <td class="MyContent">
                        <asp:TextBox ID="txtOrderingInstitutionAddress2" runat="server" Width="300" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="padding-left:30px;">Address 3</td>
                    <td class="MyContent">
                        <asp:TextBox ID="txtOrderingInstitutionAddress3" runat="server" Width="300" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">56A. Intermediary Name</td>
                    <td class="MyContent">
                        <asp:TextBox ID="txtIntermediaryName" runat="server" Width="300" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="padding-left:30px;">Address 1</td>
                    <td class="MyContent">
                        <asp:TextBox ID="txtIntermediaryAddress1" runat="server" Width="300" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="padding-left:30px;">Address 2</td>
                    <td class="MyContent">
                        <asp:TextBox ID="txtIntermediaryAddress2" runat="server" Width="300" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="padding-left:30px;">Address 3</td>
                    <td class="MyContent">
                        <asp:TextBox ID="txtIntermediaryAddress3" runat="server" Width="300" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">72. Sender to Receiver Information</td>
                    <td class="MyContent">
                        <asp:TextBox ID="txtSendMessage" runat="server" Width="500" TextMode="MultiLine" />
                    </td>
                </tr>

            </table>
        </fieldset>
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
                        <telerik:radcombobox autopostback="True"
                            onselectedindexchanged="comboWaiveCharges_OnSelectedIndexChanged"
                            id="comboWaiveCharges" runat="server"
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
            <telerik:radtabstrip runat="server" id="RadTabStrip3" selectedindex="0" multipageid="RadMultiPage1" orientation="HorizontalTop">
            <Tabs>
                <telerik:RadTab Text="Receive Charge">
                </telerik:RadTab>
                <telerik:RadTab Text="Courier Charge ">
                </telerik:RadTab>
                <telerik:RadTab Text="Other Charge">
                </telerik:RadTab>
                <telerik:RadTab Text="Payment Charge">
                </telerik:RadTab>
                <%-- hide to fix bug 47 --%>
                <%--<telerik:RadTab Text="Overseas Plus Charge">
                </telerik:RadTab>
                <telerik:RadTab Text="Overseas Minus Charge">
                </telerik:RadTab>--%>
            </Tabs>
        </telerik:radtabstrip>
            <telerik:radmultipage runat="server" id="RadMultiPage1" selectedindex="0">
            <telerik:RadPageView runat="server" ID="RadPageView1" >
                <div runat="server" ID="divReceiveCharge">
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
                                <asp:Label ID="lblPartyCharged" runat="server" />
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
                <div runat="server" ID="divCourierCharge">
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
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent">
                                <telerik:RadComboBox DropDownCssClass="KDDL"
                                    AppendDataBoundItems="True"
                                    OnItemDataBound="rcbChargeAcct2_ItemDataBound"
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
                                <asp:Label ID="lblPartyCharged2" runat="server" />
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
                <div runat="server" ID="divOtherCharge">
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
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent">
                                <telerik:RadComboBox DropDownCssClass="KDDL"
                                    AppendDataBoundItems="True"
                                    OnItemDataBound="rcbChargeAcct3_ItemDataBound"
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
                                <asp:Label ID="lblPartyCharged3" runat="server" />
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
           <telerik:RadPageView runat="server" ID="RadPageView4" >
                <div runat="server" ID="divPaymentCharge">
	                <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Charge code</td>
                            <td class="MyContent">
                                <telerik:RadComboBox 
                                    ID="tbChargeCode4" runat="server"
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
                                        ID="rcbChargeCcy4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rcbChargeCcy4_OnSelectedIndexChanged"
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
                          <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent">
                                <telerik:RadComboBox DropDownCssClass="KDDL"
                                    AppendDataBoundItems="True"
                                    OnItemDataBound="rcbChargeAcct4_ItemDataBound"
                                    ID="rcbChargeAcct4" runat="server"
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
                        <tr>
                            <td class="MyLable">Charge Amt</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true" 
                                    IncrementSettings-InterceptMouseWheel="true" runat="server" 
                                    ID="tbChargeAmt4" AutoPostBack="true"
                                    OnTextChanged="tbChargeAmt4_TextChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Party Charged</td>
                            <td class="MyContent" >
                                <telerik:RadComboBox 
                                    AutoPostBack="True"
                                    OnSelectedIndexChanged="rcbPartyCharged4_SelectIndexChange"
                                    OnItemDataBound="rcbPartyCharged4_ItemDataBound"
                                    ID="rcbPartyCharged4" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                                <asp:Label ID="lblPartyCharged4" runat="server" />
                            </td>
       
                        </tr>
                        <tr>
                            <td class="MyLable">Amort Charges</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="rcbOmortCharge4" runat="server"
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
                                        ID="rcbChargeStatus4" runat="server"
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
                                <asp:Label ID="lblTaxCode4" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Tax Amt</td>
                            <td class="MyContent">
                                <asp:Label ID="lblTaxAmt4" runat="server" />
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

            <%-- Hide tabs to fix bug 47 start --%>
            <%--<telerik:RadPageView runat="server" ID="RadPageView5" >
                <div runat="server" ID="divOverseasPlusCharge">
	                <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable">Charge code</td>
                            <td class="MyContent">
                                <telerik:RadComboBox 
                                    ID="tbChargeCode5" runat="server"
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
                                    ID="rcbChargeCcy5" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rcbChargeCcy5_OnSelectedIndexChanged"
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
                          <tr>
                            <td class="MyLable">Charge Acct</td>
                            <td class="MyContent">
                                <telerik:RadComboBox DropDownCssClass="KDDL"
                                    AppendDataBoundItems="True"
                                    OnItemDataBound="rcbChargeAcct5_ItemDataBound"
                                    ID="rcbChargeAcct5" runat="server"
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
                        <tr>
                            <td class="MyLable">Charge Amt</td>
                            <td class="MyContent">
                                <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true" 
                                    IncrementSettings-InterceptMouseWheel="true" runat="server" 
                                    ID="tbChargeAmt5" AutoPostBack="true"
                                    OnTextChanged="tbChargeAmt5_TextChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Party Charged</td>
                            <td class="MyContent" >
                                <telerik:RadComboBox
                                    AutoPostBack="True"
                                    OnSelectedIndexChanged="rcbPartyCharged5_SelectIndexChange"
                                    OnItemDataBound="rcbPartyCharged5_ItemDataBound"
                                    ID="rcbPartyCharged5" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                </telerik:RadComboBox>
                                <asp:Label ID="lblPartyCharged5" runat="server" />
                            </td>
       
                        </tr>
                        <tr>
                            <td class="MyLable">Amort Charges</td>
                            <td class="MyContent">
                                <telerik:RadComboBox
                                    ID="rcbOmortCharge5" runat="server"
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
                                        ID="rcbChargeStatus5" runat="server"
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
                                <asp:Label ID="lblTaxCode5" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="MyLable">Tax Amt</td>
                            <td class="MyContent">
                                <asp:Label ID="lblTaxAmt5" runat="server" />
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

            <telerik:RadPageView runat="server" ID="RadPageView6" >
                <div runat="server" ID="divOverseasminusCharge">
	                <table width="100%" cellpadding="0" cellspacing="0">
		                <tr>
			                <td class="MyLable">Charge code</td>
			                <td class="MyContent">
				                <telerik:RadComboBox 
					                ID="tbChargeCode6" runat="server"
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
						                ID="rcbChargeCcy6" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rcbChargeCcy6_OnSelectedIndexChanged"
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
		                  <tr>
			                <td class="MyLable">Charge Acct</td>
			                <td class="MyContent">
				                <telerik:RadComboBox DropDownCssClass="KDDL"
					                AppendDataBoundItems="True"
					                OnItemDataBound="rcbChargeAcct6_ItemDataBound"
					                ID="rcbChargeAcct6" runat="server"
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
		                <tr>
			                <td class="MyLable">Charge Amt</td>
			                <td class="MyContent">
				                <telerik:RadNumericTextBox IncrementSettings-InterceptArrowKeys="true" 
					                IncrementSettings-InterceptMouseWheel="true" runat="server" 
					                ID="tbChargeAmt6" AutoPostBack="true"
					                OnTextChanged="tbChargeAmt6_TextChanged" />
			                </td>
		                </tr>
		                <tr>
			                <td class="MyLable">Party Charged</td>
			                <td class="MyContent" >
				                <telerik:RadComboBox 
					                AutoPostBack="True"
					                OnSelectedIndexChanged="rcbPartyCharged6_SelectIndexChange"
					                OnItemDataBound="rcbPartyCharged6_ItemDataBound"
					                ID="rcbPartyCharged6" runat="server"
					                MarkFirstMatch="True" 
					                AllowCustomText="false">
					                <ExpandAnimation Type="None" />
					                <CollapseAnimation Type="None" />
				                </telerik:RadComboBox>
				                <asp:Label ID="lblPartyCharged6" runat="server" />
			                </td>

		                </tr>
		                <tr>
			                <td class="MyLable">Amort Charges</td>
			                <td class="MyContent">
				                <telerik:RadComboBox
					                ID="rcbOmortCharge6" runat="server"
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
						                ID="rcbChargeStatus6" runat="server"
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
				                <asp:Label ID="lblTaxCode6" runat="server" />
			                </td>
		                </tr>
		                <tr>
			                <td class="MyLable">Tax Amt</td>
			                <td class="MyContent">
				                <asp:Label ID="lblTaxAmt6" runat="server" />
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
            </telerik:RadPageView>--%>
            <%-- Hide tab to fix bug 47 end --%>
        </telerik:radmultipage>
        </fieldset>
    </div>
</div>
<telerik:radajaxmanager id="RadAjaxManager1" runat="server" defaultloadingpanelid="AjaxLoadingPanel1">
    <ajaxsettings>
        <telerik:AjaxSetting AjaxControlID="comboCollectionType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblCollectionTypeName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="cbNostroAccount">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblNostro" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="comboCommodity">
            <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCommodityName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="comboDrawType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblDrawType" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <%-- Hien Nguyen fixed bug 65 start --%>
         <%--<telerik:AjaxSetting AjaxControlID="comboPaymentMethod">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblPaymentMethod" />
            </UpdatedControls>
        </telerik:AjaxSetting>--%>
        <%-- Hien Nguyen fixed bug 65 ends --%>

        <telerik:AjaxSetting AjaxControlID="comboCreditCurrency">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="comboCreditAcct" />
                <telerik:AjaxUpdatedControl ControlID="cbNostroAccount" />
                <telerik:AjaxUpdatedControl ControlID="comboCurrencyMt910" />
            </UpdatedControls>
        </telerik:AjaxSetting>
           <telerik:AjaxSetting AjaxControlID="comboWaiveCharges">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="divReceiveCharge" />
                <telerik:AjaxUpdatedControl ControlID="divCourierCharge" />
                <telerik:AjaxUpdatedControl ControlID="divOtherCharge" />    
                <telerik:AjaxUpdatedControl ControlID="divPaymentCharge" />    
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
        <telerik:AjaxSetting AjaxControlID="tbChargeAmt4">
        <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="lblTaxAmt4" />
            <telerik:AjaxUpdatedControl ControlID="lblTaxCode4" />
        </UpdatedControls>
    </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="tbChargeAmt5">
        <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="lblTaxAmt5" />
            <telerik:AjaxUpdatedControl ControlID="lblTaxCode5" />
        </UpdatedControls>
    </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="tbChargeAmt6">
        <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="lblTaxAmt6" />
            <telerik:AjaxUpdatedControl ControlID="lblTaxCode6" />
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
         <telerik:AjaxSetting AjaxControlID="rcbChargeCcy4">
        <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="rcbChargeAcct4" />
        </UpdatedControls>
    </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="rcbChargeCcy5">
        <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="rcbChargeAcct5" />
        </UpdatedControls>
    </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="rcbChargeCcy6">
        <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="rcbChargeAcct6" />
        </UpdatedControls>
    </telerik:AjaxSetting>
    </ajaxsettings>
</telerik:radajaxmanager>
<telerik:radcodeblock id="RadCodeBlock1" runat="server">
    
    <script type="text/javascript">
        var tabId = <%= TabId %>;
        var clickCalledAfterRadconfirm = false;
        $("#<%=txtCode.ClientID %>").keyup(function (event) {

            if (event.keyCode == 13) {
                window.location.href = "Default.aspx?tabid=" + tabId + "&CodeID=" + $("#<%=txtCode.ClientID %>").val();
            }
        });
    </script>
</telerik:radcodeblock>
<div style="visibility: hidden;">
    <asp:Button ID="btnReportPhieuChuyenKhoan" runat="server" OnClick="btnReportPhieuChuyenKhoan_Click" Text="PhieuChuyenKhoan" />
</div>
<div style="visibility: hidden;">
    <asp:Button ID="btnReportVATb" runat="server" OnClick="btnReportVATb_Click" Text="VATb" />
</div>
<div style="visibility: hidden;">
    <asp:Button ID="btnReportPhieuXuatNgoaiBang" runat="server" OnClick="btnReportPhieuXuatNgoaiBang_Click" Text="PhieuXuatNgoaiBang" />
</div>
