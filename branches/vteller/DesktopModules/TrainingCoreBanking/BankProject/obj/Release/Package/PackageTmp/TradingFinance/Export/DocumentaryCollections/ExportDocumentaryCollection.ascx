<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExportDocumentaryCollection.ascx.cs" Inherits="BankProject.TradingFinance.Export.DocumentaryCollections.ExportDocumentaryCollection" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />

<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <script type="text/javascript">
        jQuery(function ($) {
            $('#tabs-demo').dnnTabs({selected:0});
            if (<%=TabId%> == 229) {
                $('#tabCharges').hide();
                $('#Charges').hide();
            }
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
        }

        

    </script>
</telerik:RadCodeBlock>

<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" width="100%"
    OnButtonClick="RadToolBar1_ButtonClick" OnClientButtonClicking="RadToolBar1_OnClientButtonClicking">
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
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print">
        </telerik:RadToolBarButton>
    </items>
</telerik:RadToolBar>

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
        <li><a href="#Charges" id="tabCharges">Charges</a></li>
        <%--<li><a href="#DeliveryAudit">Delivery Audit</a></li>--%>
    </ul>

    <div id="Main" class="dnnClear">
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Drawer Information</div>
            </legend>
            <div runat="server" id="divDocumentaryCollectionCancel" Visible="false">
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
                    <td class="MyLable">1 Collection Type<span class="Required"> (*)</span></td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadComboBox Width="355" DropDownCssClass="KDDL" AppendDataBoundItems="True"
                            ID="comboCollectionType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="comboCollectionType_OnSelectedIndexChanged"
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
                    <td class="MyLable">2.1 Drawer Cus No.</td>
                    <td class="MyContent">
                        <telerik:RadComboBox Width="355" DropDownCssClass="KDDL"
                            AppendDataBoundItems="True" AutoPostBack="true"
                            OnSelectedIndexChanged="comboDrawerCusNo_SelectIndexChange"
                            ID="comboDrawerCusNo" runat="server" OnItemDataBound="comboDrawerCusNo_ItemDataBound"
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

                <tr>
                    <td class="MyLable">2.2 Drawer Cus Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDrawerCusName" runat="server" Width="355" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">2.3 Drawer Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDrawerAddr1" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDrawerAddr2" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDrawerAddr3" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">2.4 Drawer Ref No.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDrawerRefNo" runat="server" Width="355" />
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
                        <telerik:RadComboBox DropDownCssClass="KDDL"
                            AppendDataBoundItems="True" Width="450" OnItemDataBound="commomSwiftCode_ItemDataBound"
                            ID="comboCollectingBankNo" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="comboCollectingBankNo_OnSelectedIndexChanged"
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
                                            <%# DataBinder.Eval(Container.DataItem, "Code")%> 
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Description")%> 
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <%--<td><asp:Label ID="lblCollectingBankName" runat="server"  />--%>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">3.2 Collecting Bank Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtCollectingBankName" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtCollectingBankAddr1" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtCollectingBankAddr2" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">3.3 Collecting Bank Acct</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboCollectingBankAcct" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>

        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Drawee/Reimbursement Detail</div>
            </legend>


            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">4.1 Drawee Cus No</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeCusNo" runat="server"
                            AutoPostBack="True" OnTextChanged="txtDraweeCusNo_OnTextChanged" Width="355" />
                    </td>
                    <td>
                        <asp:Label ID="lblRemittingBankNoError" runat="server" Text="" ForeColor="red" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">4.2 Drawee Cus Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeCusName" runat="server" Width="355" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">4.3 Drawee Addr.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeAddr1" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeAddr2" runat="server" Width="355" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDraweeAddr3" runat="server" Width="355" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">5 Nostro Cus No</td>
                    <td style="width: 150px;" class="MyContent">
                        <telerik:RadComboBox Width="400" DropDownCssClass="KDDL"
                            AppendDataBoundItems="True" AutoPostBack="true"
                            OnSelectedIndexChanged="comboNostroCusNo_SelectIndexChange"
                            ID="comboNostroCusNo" runat="server" OnItemDataBound="commomSwiftCode_ItemDataBound"
                            MarkFirstMatch="True" Height="150px"
                            AllowCustomText="false">
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
                        </telerik:RadComboBox>
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
                    <td class="MyLable">6 Currency<span class="Required"> (*)</span></td>
                    <td class="MyContent">
                        <telerik:RadComboBox
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
                    <td class="MyLable">7 Amount<span class="Required"> (*)</span></td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numAmount" runat="server" />
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
                    <td class="MyLable">8 Docs Received Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteDocsReceivedDate" runat="server" AutoPostBack="True" OnSelectedDateChanged="dteDocsReceivedDate_OnSelectedDateChanged" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">9 Maturity Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteMaturityDate" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">10 Tenor</td>
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


                <tr>
                    <td class="MyLable">11 Tracer Date</td>
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
                <tr style="display: none;">
                    <td class="MyLable">12 Reminder Days</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numReminderDays" runat="server" MaxValue="999">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">13 Commodity<span class="Required"> (*)</span></td>
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
                                            <%# DataBinder.Eval(Container.DataItem, "Name2")%> 
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
                    <td class="MyLable">14.1 Docs Code</td>
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
                    <td class="MyLable">14.2 No. of Originals</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numNoOfOriginals1" runat="server" MaxValue="999" MaxLength="3">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">14.3 No. of Copies</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numNoOfCopies1" runat="server" MaxValue="999" MaxLength="3">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
            </table>

            <div runat="server" id="divDocsCode2">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable">14.1 Docs Code</td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadComboBox Width="355"
                                AppendDataBoundItems="True"
                                ID="comboDocsCode2" runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                            </telerik:RadComboBox>

                        </td>
                        <td>
                            <asp:ImageButton ID="btRemoveDocsCode2" runat="server" OnClick="btRemoveDocsCode2_Click" ImageUrl="~/Icons/Sigma/Delete_16X16_Standard.png" />
                            <span>Remove</span></td>
                    </tr>

                    <tr>
                        <td class="MyLable">14.2 No. of Originals</td>
                        <td class="MyContent">
                            <telerik:RadNumericTextBox ID="numNoOfOriginals2" runat="server" MaxValue="999" MaxLength="3">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="MyLable">14.3 No. of Copies</td>
                        <td class="MyContent">
                            <telerik:RadNumericTextBox ID="numNoOfCopies2" runat="server" MaxValue="999" MaxLength="3">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div runat="server" id="divDocsCode3">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable">14.1 Docs Code</td>
                        <td style="width: 150px" class="MyContent">
                            <telerik:RadComboBox Width="355"
                                AppendDataBoundItems="True"
                                ID="comboDocsCode3" runat="server"
                                MarkFirstMatch="True"
                                AllowCustomText="false">
                            </telerik:RadComboBox>

                        </td>
                        <td>
                            <asp:ImageButton ID="btRemoveDocsCode3" runat="server" OnClick="btRemoveDocsCode3_Click" ImageUrl="~/Icons/Sigma/Delete_16X16_Standard.png" />
                            <span>Remove</span></td>
                    </tr>

                    <tr>
                        <td class="MyLable">14.2 No. of Originals</td>
                        <td class="MyContent">
                            <telerik:RadNumericTextBox ID="numNoOfOriginals3" runat="server" MaxValue="999" MaxLength="3">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="MyLable">14.3 No. of Copies</td>
                        <td class="MyContent">
                            <telerik:RadNumericTextBox ID="numNoOfCopies3" runat="server" MaxValue="999" MaxLength="3">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">15 Other Docs</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="100%" Height="100" ID="txtOtherDocs" runat="server" TextMode="MultiLine" />
                    </td>
                </tr>


                <tr>
                    <td class="MyLable">16 Remarks</td>
                    <td class="MyContent">
                        <telerik:RadTextBox Width="355" ID="txtRemarks" runat="server" />
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
           
        </telerik:RadMultiPage>
                </fieldset>
</div>

<%--<div id="DeliveryAudit" class="dnnClear">
</div>--%>

</div>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1">
    <ajaxsettings>
        
        <telerik:AjaxSetting AjaxControlID="comboCollectionType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblCollectionTypeName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboDrawerType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="comboDrawerCusNo" />
                <telerik:AjaxUpdatedControl ControlID="txtDrawerCusName" />
                <telerik:AjaxUpdatedControl ControlID="txtDrawerAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtDrawerAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtDrawerAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtDraweeCusNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtDraweeCusName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        
         <telerik:AjaxSetting AjaxControlID="comboCollectingBankNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtCollectingBankName" />
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
        
        <telerik:AjaxSetting AjaxControlID="comboDrawerCusNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtDrawerCusName" />
                <telerik:AjaxUpdatedControl ControlID="txtDrawerAddr1" />
                <telerik:AjaxUpdatedControl ControlID="txtDrawerAddr2" />
                <telerik:AjaxUpdatedControl ControlID="txtDrawerAddr3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        

        
        <telerik:AjaxSetting AjaxControlID="comboNostroCusNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblNostroCusName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboCommodity">
            <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCommodityName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="dteDocsReceivedDate">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="dteTracerDate" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboWaiveCharges">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="divACCPTCHG" />
                <telerik:AjaxUpdatedControl ControlID="divCABLECHG" />
                <telerik:AjaxUpdatedControl ControlID="divPAYMENTCHG" />    
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
    </ajaxsettings>
</telerik:RadAjaxManager>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var amount =  parseFloat(<%= Amount %>);
        var amountOld = parseFloat(<%= AmountOld %>);
        var tabId = <%= TabId %>;
        var clickCalledAfterRadconfirm = false;
        $("#<%=txtCode.ClientID %>").keyup(function (event) {

            if (event.keyCode == 13) {
                window.location.href = "Default.aspx?tabid=" + tabId + "&CodeID=" + $("#<%=txtCode.ClientID %>").val();
            }
        });

        function RadToolBar1_OnClientButtonClicking(sender, args) {
            var button = args.get_item();
            if (tabId == 226) { // Register Documetary Collection
                if (button.get_commandName() == "print" && !clickCalledAfterRadconfirm) {
                    args.set_cancel(true);
                    radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file from Customer?", confirmCallbackRegisterNNB1, 340, 150, null, 'Download');
                }
            }
            if (tabId == 229) { // Incoming Collection Amendments
                if (button.get_commandName() == "print") {
                    args.set_cancel(true);

                    showPhieuNhap_Xuat();

                }
            }
            if (tabId == 230) { // Incoming Collection Amendments
                if (button.get_commandName() == "print") {
                    args.set_cancel(true);
                    radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file?", confirmCallbackFunctionCancel, 420, 150, null, 'Download');
                }
            }
        }
        function confirmCallbackFunctionCancel(result) {
            if (result) {
                $("#<%=btnCancelPHIEUXUATNGOAIBANG.ClientID %>").click();
            }
        }
        function showPhieuNhap_Xuat() {
                // Neu amount > amount_old -> tu chinh tang tienb, xuat phieu [nhap ngoai bang]
                //amount < amount_Old -> tu chinh giam tien,xuat phieu [xuat phieu ngoai bang]
                // amount = amoun_old -> ko xuat phieu xuat nhap ngoai bang
            if (amount > 0 && amountOld > 0 && amount > amountOld) {//b4_AUT_Amount
                    radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file?", confirmCallbackFunction_NhapNgoaiBang_Amendments, 420, 150, null, 'Download');
                } else if (amountOld > 0 && amount < amountOld) {
                    radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file?", confirmCallbackFunction_XuatNgoaiBang_Amendments, 420, 150, null, 'Download');
                }

            }

        function confirmCallbackFunction_NhapNgoaiBang_Amendments(result) {
            clickCalledAfterRadconfirm218 = false;
            if (result) {
                $("#<%=btnAmendNhapNgoaiBang.ClientID %>").click();
            }
        }

        function confirmCallbackFunction_XuatNgoaiBang_Amendments(result) {
            clickCalledAfterRadconfirm218 = false;
            if (result) {
                $("#<%=btnAmendXuatNgoaiBang.ClientID %>").click();
            }

        }

        function confirmCallbackRegisterNNB1(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnRegisterNhapNgoaiBang1.ClientID %>").click();
            }
            radconfirm("Do you want to download PHIEU XUAT NGOAI BANG file to Collecting Bank?", confirmCallbackRegisterXNB1, 420, 150, null, 'Download');
        }
        function confirmCallbackRegisterXNB1(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnRegisterXuatNgoaiBang1.ClientID %>").click();
            }
            radconfirm("Do you want to download PHIEU NHAP NGOAI BANG file from Collecting Bank?", confirmCallbackRegisterNNB2, 420, 150, null, 'Download');
        }
        function confirmCallbackRegisterNNB2(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnRegisterNhapNgoaiBang2.ClientID %>").click();
            }
            
        }
        
    </script>
</telerik:RadCodeBlock>
<div style="visibility:hidden;"><asp:Button ID="btnRegisterNhapNgoaiBang1" runat="server" OnClick="btnRegisterNhapNgoaiBang1_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnRegisterNhapNgoaiBang2" runat="server" OnClick="btnRegisterNhapNgoaiBang2_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnRegisterXuatNgoaiBang1" runat="server" OnClick="btnRegisterXuatNgoaiBang1_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnAmendXuatNgoaiBang" runat="server" OnClick="btnAmendXuatNgoaiBang_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnAmendNhapNgoaiBang" runat="server" OnClick="btnAmendNhapNgoaiBang_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnCancelPHIEUXUATNGOAIBANG" runat="server" OnClick="btnCancelPHIEUXUATNGOAIBANG_Click" Text="Search" /></div>