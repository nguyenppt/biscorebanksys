<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Amendment.ascx.cs" Inherits="BankProject.Amendment" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>

<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" width="100%">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/doc_lines_icon.png"
                ToolTip="Commit Data" Value="btdoclines" CommandName="doclines">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/doc_new_icon.png"
                ToolTip="Back" Value="btdocnew" CommandName="docnew">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/cursor_drag_hand_icon.png"
                ToolTip="Back" Value="btdraghand" CommandName="draghand">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
                ToolTip="Back" Value="btsearch" CommandName="search">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar> 

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width:200px; padding:5px 0 5px 20px;"><asp:TextBox ID="tbEssurLCCode" runat="server" Width="200" AutoPostBack="True" OnTextChanged="tbEssurLCCode_OnTextChanged" /></td>
    </tr>
    <%--<tr>
        <td style="padding:5px 0 5px 20px;">Amendments</td>
    </tr>--%>
</table>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#Main">Main</a></li>
        <li><a href="#MT707">MT707</a></li>
        <li><a href="#MT747">MT747</a></li>
        <li><a href="#Charges">Charges</a></li>
        <li><a href="#DeliveryAudit">Delivery Audit</a></li>
    </ul>

    <div id="Main" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                 <td class="MyLable">1 Operation</td>
                 <td class="MyContent">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                             <td  Width="150">
                                <telerik:RadComboBox
                                      OnSelectedIndexChanged="comboOperation_SelectIndexChange"
                                     AutoPostBack="true" 
                                    ID="comboOperation" Runat="server"
                                    MarkFirstMatch="True"
                                    AllowCustomText="false" >
                                </telerik:RadComboBox>
                             </td>
                            <td><i><asp:Label ID="lblOperationName" runat="server" style="margin-left: 5px;"/></i></td>
                        </tr>
                    </table>
                 </td>
            </tr>
            <tr>    
                <td class="MyLable">139 Generate Delivery?</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtGenerateDelivery" Runat="server" MaxLength="100" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable" style="color: #d0d0d0">138 Amendment Date</td>
                <td class="MyContent">
                    <telerik:RadDatePicker ID="dteAmendmentDate" runat="server" Enabled="False" />
                </td>
            </tr>
        </table>

        <fieldset>
            <legend>
              <div style="font-weight:bold; text-transform:uppercase;">Advising Bank And Applicant Detail</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">3 Ext. Ref.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtExtRef" runat="server" />
                    </td>
                </tr>
                <tr>    
                    <td class="MyLable" style="color: #d0d0d0">15 Advising Bank No.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAdvisingBankNo" runat="server" Enabled="False" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">16.1 Advising Bank Addr.</td>
                    <td Width="150" class="MyContent">
                        <asp:TextBox ID="txtAdvisingBankAddr" runat="server" MaxLength="500" />
                    </td>
                    <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">17 Advising Bank Acct</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboAdvisingBankAcct" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable" style="color: #d0d0d0">9. Applicant No</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtApplicantNo" runat="server"  Enabled="False" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">10.1 Applicant Addr.</td>
                    <td Width="150" class="MyContent">
                        <telerik:RadTextBox ID="txtApplicantAddr1" runat="server" />
                    </td>
                    <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">11 Applicant Acct.</td>
                    <td class="MyContent">
                         <telerik:RadComboBox
                            ID="comboApplicantAcct" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        
        <fieldset>
            <legend>
              <div style="font-weight:bold; text-transform:uppercase;">LC Detail</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">2 LC Type</td>
                    <td class="MyContent">
                    
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                 <td  Width="150">
                                     <telerik:RadComboBox  AutoPostBack="True" OnSelectedIndexChanged="comboLCType_OnSelectedIndexChanged"
                                    ID="comboLCType" Runat="server" width="355"
                                    MarkFirstMatch="True"  OnItemDataBound="comboLCType_ItemDataBound"
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
                                     <%--<telerik:RadComboBox AutoPostBack="True" OnSelectedIndexChanged="comboLCType_OnSelectedIndexChanged"
                                        ID="comboLCType" Runat="server"
                                        MarkFirstMatch="True"
                                        AllowCustomText="false" >
                                    </telerik:RadComboBox>--%>
                                 </td>
                                <td ><i><asp:Label ID="lblLcTypeName" runat="server" style="margin-left: 5px;"/></i></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>    
                    <td class="MyLable" style="color: #d0d0d0">20 Currency</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtCurrency" runat="server" Enabled="False" />
                    </td>
                </tr>
       
                <tr>    
                    <td class="MyLable">21 LC Amount</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numLcAmount" runat="server" CssClass="dnnFormRequired"/>
                    </td>
                </tr>
                <tr>    
                    <td class="MyLable" style="color: #d0d0d0">27 Issue Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteIssueDate" runat="server" Enabled="False" />
                    </td>
                </tr>
                <tr>    
                    <td class="MyLable">160 Expiry Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteExpiryDate" runat="server" />
                    </td>
                </tr>
                <tr>    
                    <td class="MyLable">28 Contingent Expiry Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteContingentExpiryDate" runat="server" /> 30 Archive Date (Sys.Field)
                    </td>
                </tr>
                <tr>    
                    <td class="MyLable">29 Expiry Place</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtExpiryPlace" runat="server" />
                    </td>
                </tr>
                <tr>    
                    <td class="MyLable">52 Cr.Tolerance</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="num52CrTolerance" runat="server" />
                        51 Dr.Tolerance <telerik:RadNumericTextBox ID="num51CrTolerance" runat="server" />
                    </td>
                </tr>
                <tr>    
                    <td class="MyLable">84 Lasted Ship Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteLastedShipDate" runat="server" />
                    </td>
                </tr>
            </table>
        
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">85.1 Shipment Mdethod</td>
                    <td width="150" class="MyContent">
                        <telerik:RadTextBox ID="txtShipmentMdethod" runat="server" MaxLength="500" Width="420"/>
                    </td>
                    <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>
        </fieldset>
        
        <fieldset>
            <legend>
              <div style="font-weight:bold; text-transform:uppercase;">Benificiary/ Advice Thru/ Reimbursing Bank</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">12 Beneficiary Cust. No</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboBeneficiaryCustNo" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">13.1 Beneficiary Addr</td>
                    <td style="width: 150px;" class="MyContent">
                        <telerik:RadTextBox ID="txt131BeneficiaryAddr" runat="server" MaxLength="250" />
                    </td>
                    <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>
        
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable" style="color: #d0d0d0">44 Reimb. Bank No.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtReimbBankNo" runat="server" Enabled="False" />
                    </td>
                </tr>
            </table>
        
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">45.1 Reimb. Bank Addr.</td>
                    <td style="width: 150px;" class="MyContent">
                        <telerik:RadTextBox ID="txtReimbBankAddr" runat="server"  />
                    </td>
                    <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>
        
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">47 Advise Thru. Bank</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboAdviseThruBank" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
        
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">48.1 Advise Thru. Addr.</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadTextBox ID="txtAdviseThruAddr" runat="server"  />
                    </td>
                    <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">55 Avail With Cust.</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboAvailWithCust" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">56.1 Availble With</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadTextBox ID="txtAvailbleWith" runat="server"  />
                    </td>
                    <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>
        </fieldset>
            
        <fieldset>
            <legend>
              <div style="font-weight:bold; text-transform:uppercase;">Prov Details</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
            <tr>    
                <td class="MyLable">176.22 Prov %</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="numProvPercent" runat="server" MaxValue="100" Type="Percent"/>
                </td>
            </tr>
        </table>
        </fieldset>
        
        <fieldset>
            <legend>
              <div style="font-weight:bold; text-transform:uppercase;">Draw Type propotion and Limit</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
            <tr>    
                <td class="MyLable">63.1 Draw Type</td>
                <td class="MyContent">
                    <telerik:RadComboBox AppendDataBoundItems="True"
                        ID="comboAvailbleWith" Runat="server"
                        MarkFirstMatch="True"
                        AllowCustomText="false" >
                        <Items>
                            <telerik:RadComboBoxItem Value="SP" Text="SP" />
                            <telerik:RadComboBoxItem Value="MA" Text="MA" />
                            <telerik:RadComboBoxItem Value="MD" Text="MD" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>    
                <td class="MyLable">64.1 Payment %</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="numPaymentPercent" runat="server" MaxValue="100" Type="Percent" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">65.1 Pay Portion</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="numPayPortion" runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">66.1 Accept. Mature Date</td>
                <td class="MyContent">
                    <telerik:RadDatePicker ID="dteAcceptMatureDate" runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable" style="color: #d0d0d0">72.1 Liab Portion Amount</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="numLiabPortionAmount" runat="server" Enabled="False"/>
                </td>
            </tr>
            <tr>    
                <td class="MyLable">74.1 Limit Reference</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtLimitReference" runat="server"/>
                </td>
            </tr>
        </table>
        </fieldset>
        
        <fieldset>
            <legend>
              <div style="font-weight:bold; text-transform:uppercase;">Amendment Remarks</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">127.1 Amendment Remarks</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAmendmentRemarks" runat="server" MaxLength="500" Width="420" TextMode="MultiLine" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>

    <div id="MT707"></div>
    
    <div id="MT747"></div>
    
    <div id="Charges">
        <fieldset>
            <legend>
              <div style="font-weight:bold; text-transform:uppercase;">Charge Details</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
            <tr>    
                <td class="MyLable">96 Waive Charges</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtWaiveCharges" Runat="server" MaxLength="50" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">97.1 Charge Code</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtChargeCode97_1" Runat="server" MaxLength="150" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">98.1 Charge Acct.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtChargeAcct98_1" Runat="server" MaxLength="200" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">100.1 Charge Period</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="numChargePeriod100_1" runat="server" MaxValue="100"/>
                </td>
            </tr>
            <tr>    
                <td class="MyLable">101.1 Charge Ccy</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtChargeCcy" Runat="server" MaxLength="100" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">102.1 Exch. Rate</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtExchRate102_1" Runat="server" MaxLength="250" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">103.1 Charge Amount</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="numChargeAmount103_1" runat="server"/>
                </td>
            </tr>
            <tr>    
                <td class="MyLable">104.1 Party Charged</td>
                <td class="MyContent">
                    <telerik:RadComboBox AppendDataBoundItems="False"
                        ID="comboPartyCharge104_1" Runat="server"
                        MarkFirstMatch="True"
                        AllowCustomText="false" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="B" Text="B" />
                            <telerik:RadComboBoxItem Value="CB" Text="CB" />
                            <telerik:RadComboBoxItem Value="CO" Text="CO" />
                            <telerik:RadComboBoxItem Value="O" Text="O" />
                            <telerik:RadComboBoxItem Value="T" Text="T" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>    
                <td class="MyLable">105.1 Amort Charges</td>
                <td class="MyContent">
                    <telerik:RadComboBox AppendDataBoundItems="True"
                        ID="comboAmortCharges105_1" Runat="server" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                            <telerik:RadComboBoxItem Value="YES" Text="YES" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>    
                <td class="MyLable" style="color: #d0d0d0">107.1 Amt. in Local CCY</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="numAmtInLocalCCY107_1" Runat="server" Enabled="False" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable" style="color: #d0d0d0">108.1 Amt. DR from Acct.</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="numAmtDRFromAcct108_1" Runat="server" Enabled="False" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">109.1 Charge Status</td>
                <td class="MyContent">
                    <telerik:RadComboBox AppendDataBoundItems="True"
                        ID="comboChargeStatus109_1" Runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="2" Text="2" />
                            <telerik:RadComboBoxItem Value="3" Text="3" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>    
                <td class="MyLable">97.2 Charge Code</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtChargeCode97_2" Runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">98.2 Charge Acct.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtChargeAcct98_2" Runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">100.2 Charge Period</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="numChargePeriod100_2" runat="server" MaxValue="100"/>
                </td>
            </tr>
            <tr>    
                <td class="MyLable">101.2 Charge Ccy</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtChargeCcy101_2" runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">102.2 Exch. Rate</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="numExchRate102_2" runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">103.2 Charge Amount</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="numChargeAmount103_2" runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">104.2 Party Charged</td>
                <td class="MyContent">
                    <telerik:RadComboBox AppendDataBoundItems="True"
                        ID="comboPartyCharge104_2" Runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="B" Text="B" />
                            <telerik:RadComboBoxItem Value="CB" Text="CB" />
                            <telerik:RadComboBoxItem Value="CO" Text="CO" />
                            <telerik:RadComboBoxItem Value="O" Text="O" />
                            <telerik:RadComboBoxItem Value="T" Text="T" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>    
                <td class="MyLable">105.2 Amort Charges</td>
                <td class="MyContent">
                    <telerik:RadComboBox AppendDataBoundItems="True"
                        ID="comboAmortCharges105_2" Runat="server" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                            <telerik:RadComboBoxItem Value="YES" Text="YES" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>    
                <td class="MyLable" style="color: #d0d0d0">107.2 Amt. in Local CCY</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="numAmtInLocalCCY107_2" runat="server" Enabled="False"/>
                </td>
            </tr>
            <tr>    
                <td class="MyLable" style="color: #d0d0d0">108.2 Amt. DR from Acct.</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="numAmtDRFromAcct108_2" runat="server" Enabled="False" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">109.2 Charge Status</td>
                <td class="MyContent">
                    <telerik:RadComboBox AppendDataBoundItems="True"
                        ID="comboChargeStatus109_2" Runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="2" Text="2" />
                            <telerik:RadComboBoxItem Value="3" Text="3" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>    
                <td class="MyLable">176.6.1 Charge Remasks</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtChargeRemasks" runat="server" MaxValue="250"/>
                </td>
            </tr>
            <tr>    
                <td class="MyLable">176.10.1 VAT No.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtVatNo" runat="server" MaxValue="250"/>
                </td>
            </tr>
            <tr>    
                <td class="MyLable">111.1 Tax Code</td>
                <td class="MyContent">
                    <asp:Label ID="lbTaxCode" runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">112.1 Tax Ccy</td>
                <td class="MyContent">
                    <asp:Label ID="lbTaxCcy" runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">113.1 Tax Amt</td>
                <td class="MyContent">
                    <asp:Label ID="lbTaxAmt" runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">116.1 Tax in LCCY Amt</td>
                <td class="MyContent">
                    <asp:Label ID="lbTaxinLCCYAnt" runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">117.1 Tax Acct Amt</td>
                <td class="MyContent">
                    <asp:Label ID="lbTaxAcctAmt" runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">120.1 Tax Date</td>
                <td class="MyContent">
                    <asp:Label ID="lbTaxDate" runat="server" />
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable">111.2 Tax Code</td>
                <td class="MyContent">
                    <asp:Label ID="lbTaxCode111_2" runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">112.2 Tax Ccy</td>
                <td class="MyContent">
                    <asp:Label ID="lbTaxCcy112_2" runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">113.2 Tax Amt</td>
                <td class="MyContent">
                    <asp:Label ID="lbTaxAmt113_2" runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">116.2 Tax in LCCY Amt</td>
                <td class="MyContent">
                    <asp:Label ID="lbTaxinLCCYAnt116_2" runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">117.2 Tax Acct Amt</td>
                <td class="MyContent">
                    <asp:Label ID="lbTaxAcctAmt117_2" runat="server" />
                </td>
            </tr>
            <tr>    
                <td class="MyLable">120.2 Tax Date</td>
                <td class="MyContent">
                    <asp:Label ID="lbTaxDate120_2" runat="server" />
                </td>
            </tr>
        </table>
        </fieldset>
    </div>
    
    <div id="DeliveryAudit"></div>
</div>

  <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="comboOperation">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblOperationName" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="comboLCType">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblLcTypeName" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="tbEssurLCCode">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblLcTypeName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
  <script type="text/javascript">
      $(document).ready(
    function () {
        $('a.add').live('click',
            function () {
                $(this)
                    .closest('tr')
                    .clone()
                    .appendTo($(this).closest('table'));
                $(this)
                    .html('<img src="Icons/Sigma/Delete_16X16_Standard.png"/>')
                    .removeClass('add')
                    .addClass('remove');
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
                    thisRrow = $(this).closest('tr').index();
                
                $(this).attr('name', 'row' + thisRow + thisName);
                $(this).attr('id', 'row' + thisRow + thisName);
            });
    });
  </script>
</telerik:RadCodeBlock>