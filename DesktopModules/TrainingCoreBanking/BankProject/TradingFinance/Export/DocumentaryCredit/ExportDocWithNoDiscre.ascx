<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExportDocWithNoDiscre.ascx.cs" Inherits="BankProject.TradingFinance.Export.DocumentaryCredit.ExportDocWithNoDiscre" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"></telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />
<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <script type="text/javascript">
        var tabId = <%=TabId%>;
        
        jQuery(function($) {
            $('#tabs-demo').dnnTabs();
            
        });
        function OnclientSelectedIndexChanged(sender, eventArgs)
        {
            var item = eventArgs.get_item();
            if (item.get_text() == "YES") {
                $("#<%=TCharge.ClientID %>").show();
                
                
            }
            else {
                $("#<%=TCharge.ClientID %>").hide();
                
                
            }
        }
        function RadToolBar1_OnClientButtonClicking(sender, args) {
            var button = args.get_item();
            //
            if (button.get_commandName() == '<%=BankProject.Controls.Commands.Preview%>') {
                window.location = '<%=EditUrl("List")%>&lst=4appr';
            }
            if (button.get_commandName() == '<%=BankProject.Controls.Commands.Search%>') {
                window.location = '<%=EditUrl("List")%>';
            }
            if (button.get_commandName() == '<%=BankProject.Controls.Commands.Print%>') {
                //MT734 chua co mau : Nguyen dang xin
                //radconfirm("Do you want to download MT734 file?", confirmCallbackFunction_MT734, 340, 150, null, 'Download');
                radconfirm("Do you want to download THU GOI CHUNG TU file?", confirmCallbackFunction_ThuGoiChungTu, 340, 150, null, 'Download');
            }
        }
        function confirmCallbackFunction_ThuGoiChungTu(result)
        {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnReportThuGoiChungTu.ClientID %>").click();
            }
            setTimeout(function(){
                radconfirm("Do you want to download PhieuXuatNgoaiBang file?", confirmCallbackFunction_PhieuXuatNgoaiBang, 340, 150, null, 'Download');
            },4000);
        }
        function confirmCallbackFunction_PhieuXuatNgoaiBang(result)
        {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnReportPhieuXuatNgoaiBang.ClientID %>").click();
            }
            if(tabId==240)
            {
                setTimeout(function(){
                    radconfirm("Do you want to download PhieuThu file?", confirmCallbackFunction_PhieuThu, 340, 150, null, 'Download');
                },4000);
            }
        }
        function confirmCallbackFunction_PhieuThu(result)
        {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnReportPhieuThu.ClientID %>").click();
            }
        }
        function OnDateSelected(sender, e) {
            alert(e.get_date());
        }
        
    </script>
</telerik:RadCodeBlock>
<telerik:RadToolBar runat="server" ID="RadToolBar1" OnClientButtonClicking="RadToolBar1_OnClientButtonClicking"
    EnableRoundedCorners="true" EnableShadows="true" width="100%" OnButtonClick="RadToolBar1_ButtonClick" >
        <Items>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit" enabled="false">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
                ToolTip="Preview" Value="btPreview" CommandName="preview" postback="false" enabled="true">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
                ToolTip="Authorize" Value="btAuthorize" CommandName="authorize" enabled="false">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
                ToolTip="Reverse" Value="btReverse" CommandName="reverse" enabled="false">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
                ToolTip="Search" Value="btSearch" CommandName="search" postback="false" enabled="true">
            </telerik:RadToolBarButton>
             <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
                ToolTip="Print Deal Slip" Value="btPrint" CommandName="print" postback="false" enabled="false">
            </telerik:RadToolBarButton>
        </Items>
</telerik:RadToolBar>

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width:200px; padding:5px 0 5px 20px;"><asp:TextBox ID="txtCode" runat="server" Width="200" />&nbsp;<asp:Label ID="lblError" runat="server" ForeColor="red" /></td>
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
            <% if (TabId == 240) %>
                <%{ %>
                    <li><a href="#tabCharge">Charge</a></li>
            <% }
            else if (TabId == 241) %>
                    <%{%>
                        <li><a href="#tabCharge">Charge</a></li>
            <% } %>
        </ul>
        <div id="Main" class="dnnClear">
            <table width="100%" cellpadding="0" cellspacing="0">
                <%if (TabId == TabDocsAccept){ %>
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
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
            <%} %>
                <tr>
                    <td class="MyLable" style="width: 180px">1. Draw Type (CO)</td>
                    <td class="MyContent">
                    <telerik:RadComboBox  Width="355"
                        ID="comboDrawType" 
                        Runat="server"
                        MarkFirstMatch="True"
                        AllowCustomText="false" >
                    </telerik:RadComboBox>
                </td>
                </tr>
            </table>
            <div runat="server" ID="divPresentorNo">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable" style="width:180px">29. Presentor Ref No.</td>
                        <td class="MyContent">
                            <telerik:Radtextbox runat="server" ID="comboPresentorNo" Width="355" />
                        </td>
                    </tr>
                <%--<tr>
                    <td class="MyLable" style="width: 180px">29. Presentor Ref No.</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                            ID="comboPresentorNo" 
                            AutoPostBack="true" 
                            Runat="server" 
                            AppendDataBoundItems="true"
                            OnSelectedIndexChanged="comboPresentorNo_SelectedIndexChanged"
                            OnItemDataBound="SwiftCode_ItemDataBound"  
                            MarkFirstMatch="True"
                            Width="355"
                            Height="150"
                            AllowCustomText="false" >
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                        </telerik:RadComboBox>
                    </td>
                </tr>--%>
                <tr style="display:none">
                    <td class="MyLable">5 Parent Drawing</td>
                    <td class="MyContent">
                        <telerik:Radtextbox runat="server" ID="txtPresentorName" Width="355" />
                    </td>
                </tr>
                <tr style="display:none">
                    <td class="MyLable">69.7 Discounted Loan Ref</td>
                    <td class="MyContent">
                        <telerik:Radtextbox runat="server" ID="txtPresentorRefNo" Width="355" />
                    </td>
                </tr>
                </table>
            </div>
            <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable" style="width: 180px">2. Currency</td>
                <td class="MyContent">
                    <asp:Label ID="lblCurrency" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">3. Document Amt<span class="Required"> (*)</span></td>
                <td class="MyContent">
                    <telerik:Radnumerictextbox runat="server" ID="numAmount"/>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="numAmount"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Document Amount is required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="display:none">
                <td class="MyLable">11. Booking Date</td>
                <td class="MyContent">
                    <telerik:RadDatePicker ID="dteBookingDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">69.4 Docs Received Date<span class="Required"> (*)</span></td>
                <td class="MyContent">
                    <telerik:RadDatePicker ID="dteDocsReceivedDate" AutoPostBack="True" runat="server" OnSelectedDateChanged="RadDatePicker1_SelectedDateChanged" >
                    </telerik:RadDatePicker>
                        
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator2"
                        ControlToValidate="dteDocsReceivedDate"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Docs Received Date is required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            </table>
        
        <div runat="server" ID="divDocCode">
        <div runat="server" ID="divDocsCode1">
        <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 180px">38.1 Docs Code</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadComboBox Width="355"
                            AppendDataBoundItems="True"
                            ID="comboDocsCode1" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="btAddDocsCode" runat="server" 
                            OnClick="btAddDocsCode_Click" ImageUrl="~/Icons/Sigma/Add_16X16_Standard.png" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">39.1 No. of Originals</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numNoOfOriginals1" runat="server" MaxValue="999" MaxLength="3">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">40.1 No. of Copies</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numNoOfCopies1" runat="server" MaxValue="999" MaxLength="3">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                
            </table>
        </div>
            <table width="100%" cellpadding="0" cellspacing="0">
            <tr style="display:none">
                <td class="MyLable" style="width: 180px">69.3.1 Other Docs</td>
                <td class="MyContent">
                    <telerik:Radtextbox runat="server" ID="txtOtherDocs1" Width="355" />
                </td>
            </tr>
            
            <tr style="display:none;">
                <td class="MyLable">69.3.2 Other Docs</td>
                <td class="MyContent">
                    <telerik:Radtextbox runat="server" ID="txtOtherDocs2" Width="355" />
                </td>
            </tr>
            
            <tr style="display:none;">
                <td class="MyLable">69.3.3 Other Docs</td>
                <td class="MyContent">
                    <telerik:Radtextbox runat="server" ID="txtOtherDocs3" Width="355" />
                </td>
            </tr>
        </table>
            <table width="100%" cellpadding="0" cellspacing="0">
            <tr style="display:none">
                <td class="MyLable" style="width: 180px">69.3.1 Other Docs</td>
                <td class="MyContent">
                    <telerik:Radtextbox runat="server" ID="Radtextbox1" Width="355" />
                </td>
            </tr>
            
            <tr style="display:none;">
                <td class="MyLable">69.3.2 Other Docs</td>
                <td class="MyContent">
                    <telerik:Radtextbox runat="server" ID="Radtextbox2" Width="355" />
                </td>
            </tr>
            
            <tr style="display:none;">
                <td class="MyLable">69.3.3 Other Docs</td>
                <td class="MyContent">
                    <telerik:Radtextbox runat="server" ID="Radtextbox3" Width="355" />
                </td>
            </tr>
        </table>
        </div>
            <div runat="server" ID="divDocsCode2" visible="false">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 180px">41.1 Docs Code</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadComboBox Width="355"
                            AppendDataBoundItems="True"
                            ID="comboDocsCode2" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                       <asp:ImageButton ID="ImageButton1" runat="server" OnClick="btDeleteDocsCode2_Click" ImageUrl="~/Icons/Sigma/Delete_16X16_Standard.png" /> 
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">42.1 No. of Originals</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numNoOfOriginals2" runat="server" MaxValue="999" MaxLength="3">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">43.1 No. of Copies</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numNoOfCopies2" runat="server" MaxValue="999" MaxLength="3">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
            </table>
        </div>        
        <div runat="server" ID="divDocsCode3" visible="false">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 180px">44.1 Docs Code</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadComboBox Width="355"
                            AppendDataBoundItems="True"
                            ID="comboDocsCode3" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="ImageButton2" runat="server" OnClick="btDeleteDocsCode3_Click" ImageUrl="~/Icons/Sigma/Delete_16X16_Standard.png" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">45.1 No. of Originals</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numNoOfOriginals3" runat="server" MaxValue="999" MaxLength="3">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">46.1 No. of Copies</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numNoOfCopies3" runat="server" MaxValue="999" MaxLength="3">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
            </table>
            
        </div>
        <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display:none">
                    <td class="MyLable" style="width: 180px">69.3.1 Full docs amount</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox runat="server" ID="txtFullDocsAmount" Width="355" />
                    </td>
                </tr>
        </table>
        <fieldset runat="server" ID="fieldsetDiscrepancies" visible="false">
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Discrepancies and Disposal of Docs</div>
            </legend>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 170px">33.1 Discrepancies</td>
                    <td class="MyContent">
                        <telerik:Radtextbox runat="server" ID="txtDiscrepancies" Width="355"/>
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">69.5.1 Disposal of Docs</td>
                    <td class="MyContent">
                        <telerik:Radtextbox runat="server" ID="txtDisposalOfDocs" Width="355" />
                    </td>
                </tr>
            </table>
        </fieldset>
        
        <div runat="server" ID="divLast">
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Trace Date</div>
            </legend>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 170px">29. Trace Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteTraceDate" runat="server" />
                    </td>
                </tr>
            </table>
        </fieldset>
        
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Supplemental Docs</div>
            </legend>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 170px">69.1.1 Docs Received Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteDocsReceivedDate_Supplemental" runat="server" />
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">69.2.1 Presentor Ref. No</td>
                    <td class="MyContent">
                        <telerik:Radtextbox runat="server" ID="txtPresentorRefNo_Supplemental" Width="355" />
                    </td>
                </tr>
                
                <tr>
                    <td class="MyLable">42.1 Docs</td>
                    <td class="MyContent">
                        <telerik:Radtextbox runat="server" ID="txtDocs_Supplemental1" Width="355"  />
                    </td>
                </tr>
            </table>
        </fieldset>
        </div>
        </div>
        <div id="tabCharge" class="dnnClear">
            <div runat="server" ID="divCharge">
                <asp:HiddenField ID="hiddenCustomerName" runat="server" />
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable">Waive Charges</td>
                        <td class="MyContent">
                            <telerik:RadComboBox AutoPostBack="false"
                               
                                Onclientselectedindexchanged="OnclientSelectedIndexChanged"
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
                <div id="TCharge"  runat="server">
                <telerik:RadTabStrip runat="server" ID="RadTabStrip3" SelectedIndex="0" MultiPageID="RadMultiPage1" Orientation="HorizontalTop">
                <Tabs>
                    <telerik:RadTab Text="Cable Charge">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Open Charge ">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Open Charge for Import LC (Amort)">
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
                                <td class="MyLable">Charge Ccy</td>
                                <td class="MyContent">
                                    <telerik:RadComboBox AppendDataBoundItems="True"
                                        ID="rcbChargeCcy" runat="server"
                                        MarkFirstMatch="True" AllowCustomText="false" AutoPostBack="True" OnSelectedIndexChanged="rcbChargeCcy_OnSelectedIndexChanged">
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
                                    <telerik:RadComboBox AppendDataBoundItems="True"
                                        ID="rcbChargeAcct" runat="server"
                                        MarkFirstMatch="True" width="300"
                                        AllowCustomText="false">
                                        <ExpandAnimation Type="None" />
                                        <CollapseAnimation Type="None" />
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
                                        ID="tbChargeAmt" AutoPostBack="true"
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
                
                        <table width="100%" cellpadding="0" cellspacing="0">
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
                                    <telerik:RadComboBox AppendDataBoundItems="True"
                                        ID="rcbChargeCcy2" runat="server"
                                        MarkFirstMatch="True" AllowCustomText="false" AutoPostBack="True" OnSelectedIndexChanged="rcbChargeCcy2_OnSelectedIndexChanged">
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
                                    <telerik:RadComboBox AppendDataBoundItems="True"
                                        ID="rcbChargeAcct2" runat="server"
                                        MarkFirstMatch="True" width="300"
                                        AllowCustomText="false">
                                        <ExpandAnimation Type="None" />
                                        <CollapseAnimation Type="None" />
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
                                        ID="tbChargeAmt2" AutoPostBack="true"
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
				                    <telerik:RadComboBox AppendDataBoundItems="True"
                                        ID="rcbChargeCcy3" runat="server"
                                        MarkFirstMatch="True" AllowCustomText="false" AutoPostBack="True" OnSelectedIndexChanged="rcbChargeCcy3_OnSelectedIndexChanged">
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
				                    <telerik:RadComboBox AppendDataBoundItems="True"
                                        ID="rcbChargeAcct3" runat="server"
                                        MarkFirstMatch="True" width="300"
                                        AllowCustomText="false">
                                        <ExpandAnimation Type="None" />
                                        <CollapseAnimation Type="None" />
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
	                    </table>
                    </div>
                </telerik:RadPageView>           
                </telerik:RadMultiPage>
            </div>
            </div>
        </div>
        
</div>

<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"><img src="icons/bank/ajax-loader-16x16.gif" /></telerik:RadAjaxLoadingPanel>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
    <AjaxSettings>        
        <telerik:AjaxSetting AjaxControlID="comboPresentorNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtPresentorName" />
            </UpdatedControls>
        </telerik:AjaxSetting>        
       
        
        <telerik:AjaxSetting AjaxControlID="comboDocsCode">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="comboDocsCode" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="comboDocsCode2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="comboDocsCode2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboDocsCode3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="comboDocsCode3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboDocsCode3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="comboDocsCode3" />
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
        
        <telerik:AjaxSetting AjaxControlID="rcbOmortCharge">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbOmortCharge" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="rcbOmortCharges2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbOmortCharges2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="rcbOmortCharges3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbOmortCharges3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="tbChargeCode">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbChargeCode" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="tbChargeCode2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbChargeCode2" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="tbChargeCode3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbChargeCode3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="tbChargeAmt">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tbChargeAmt2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tbChargeAmt3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="tbChargeAmt">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tbChargeAmt2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tbChargeAmt3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt3" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="dteDocsReceivedDate">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="dteTraceDate" />
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
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" />
    <asp:Button ID="btnReportThuGoiChungTu" runat="server" OnClick="btnReportThuGoiChungTu_Click" Text="ThuGoiChungTu" />
    <asp:Button ID="btnReportPhieuXuatNgoaiBang" runat="server" OnClick="btnReportPhieuXuatNgoaiBang_Click" Text="ThuGoiChungTu" />
    <asp:Button ID="btnReportPhieuThu" runat="server" OnClick="btnReportPhieuThu_Click" Text="ThuGoiChungTu" />
</div>