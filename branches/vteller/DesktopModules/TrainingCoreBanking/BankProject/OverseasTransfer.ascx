<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OverseasTransfer.ascx.cs" Inherits="BankProject.OverseasTransfer" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit"/>

<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <script type="text/javascript">
        var clickCalledAfterRadconfirm = false;
        
        jQuery(function ($) {
            $('#tabs-demo').dnnTabs();
        });
        
        function RadToolBar1_OnClientButtonClicking(sender, args) {
            var button = args.get_item();
            if (button.get_commandName() == "print" && !clickCalledAfterRadconfirm) {
                args.set_cancel(true);
                radconfirm("Do you want to download MT103 file?", confirmCallbackFunction1, 340, 150, null, 'Download');
            }
        }

        function confirmCallbackFunction1(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnMT103Report.ClientID %>").click();
            }
            radconfirm("Do you want to download PHIEU CHUYEN KHOAN file?", confirmCallbackFunction2, 420, 150, null, 'Download');
        }

        function confirmCallbackFunction2(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnPhieuCKReport.ClientID %>").click();
            }
            radconfirm("Do you want to download VAT file?", confirmCallbackFunctionVAT, 340, 150, null, 'Download');
        }
        
        function confirmCallbackFunctionVAT(result) {
            clickCalledAfterRadconfirm = false;
            if (result) {
                $("#<%=btnVAT.ClientID %>").click();
            }
        }
        
        function OnBlur(sender, args) {
            document.getElementById('<%= hf_AccountId.ClientID%>').value = sender.get_value();
        }
    </script>
</telerik:RadCodeBlock>
<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" width="100%"
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
            ToolTip="Print Deal Slip" Value="btprint" CommandName="print">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width:200px; padding:5px 0 5px 20px;"><asp:TextBox ID="txtCode" runat="server" Width="200"/></td>
    </tr>
</table>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#AccountTransfer" id="tabAccountTransfer">Account Transfer</a></li>
        <li><a href="#MT103" id="tabMT103">MT 103 Detail</a></li>
        <li><a href="#ChargeCommission" id="tabChargeCommission">Charge Commission</a></li>
    </ul>
    
    <div id="AccountTransfer" class="dnnClear">
        <fieldset>
            <legend>
              <div style="font-weight:bold; text-transform:uppercase;">Transfer Type</div>
            </legend>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 230px;" class="MyLable">Transaction Type<span class="Required"> (*)</span></td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadComboBox width="355" DropDownCssClass="KDDL"  AppendDataBoundItems="True" 
                            ID="comboTransactionType" Runat="server" AutoPostBack="True" OnSelectedIndexChanged="comboTransactionType_OnSelectedIndexChanged"
                            MarkFirstMatch="True" OnItemDataBound="commom_ItemDataBound"
                            AllowCustomText="false" >
                            <HeaderTemplate>
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
                           </ItemTemplate>
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator1"
                            ControlToValidate="comboTransactionType"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Transaction Type is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td><asp:Label ID="lbTransactionTypeName" runat="server" Text="" />
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
                <tr>    
                    <td style="width: 230px;" class="MyLable">Product Line</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboProductLine" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 230px;" class="MyLable">Country Code</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadComboBox Width="355" AppendDataBoundItems="True"
                            ID="comboCountryCode" Runat="server" 
                            AutoPostBack="False" 
                            OnSelectedIndexChanged="comboCountryCode_OnSelectedIndexChanged"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="lblCountryCodeName" runat="server"  />
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 230px;" class="MyLable">Commodity/Services</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadComboBox Width="355" AppendDataBoundItems="True"
                            ID="comboCommoditySer" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="lblCommoditySerName" runat="server" Text="" Visible="False" />
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 230px;" class="MyLable">Other Info</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtOtherInfo" Runat="server" Width="355"/>
                    </td>
                </tr>
            </table>
        </fieldset>
        
        <fieldset>
            <legend>
              <div style="font-weight:bold; text-transform:uppercase;">Debit Information</div>
            </legend>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 230px;" class="MyLable">Order customer ID</td>
                    <td style="width: 150px;" class="MyContent">
                        <telerik:RadComboBox width="355" DropDownCssClass="KDDL"
                            ID="comboOtherBy" Runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                            OnSelectedIndexChanged="comboOtherBy_OnSelectedIndexChanged"
                            MarkFirstMatch="True" OnItemDataBound="comboOtherBy_ItemDataBound"
                            AllowCustomText="false">
                            <HeaderTemplate>
                                <table cellpadding="0" cellspacing="0"> 
                                  <tr> 
                                     <td style="width:100px;"> 
                                        Customer Id
                                     </td> 
                                     <td> 
                                        Customer Name
                                     </td>
                                  </tr> 
                               </table> 
                           </HeaderTemplate>
                            <ItemTemplate>
                                    <table  cellpadding="0" cellspacing="0"> 
                                      <tr> 
                                         <td style="width:100px;"> 
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
                    <td><asp:Label ID="lblOtherByName" runat="server" />
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 230px;" class="MyLable">Order customer Name and Address</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtOtherBy2" runat="server" 
                            AutoPostBack="True" OnTextChanged="txtOtherBy2_OnTextChanged" Width="355" />
                    </td>
                </tr>
                
                <tr>    
                    <td style="width: 230px;" class="MyLable">Order customer Name and Address</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtOtherBy3" runat="server" AutoPostBack="True" OnTextChanged="txtOtherBy3_OnTextChanged" Width="355"/>
                    </td>
                </tr>
                
                <tr>    
                    <td style="width: 230px;" class="MyLable">Order customer Name and Address</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtOtherBy4" runat="server" AutoPostBack="True" OnTextChanged="txtOtherBy4_OnTextChanged" Width="355"/>
                    </td>
                </tr>
                
                <tr>    
                    <td style="width: 230px;" class="MyLable">Order customer Name and Address</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtOtherBy5" runat="server" AutoPostBack="True" OnTextChanged="txtOtherBy5_OnTextChanged" Width="355"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 230px;" class="MyLable">Debit Ref</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtDebitRef" Runat="server" />
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 230px;" class="MyLable">Debit Acct No</td>
                    <td class="MyContent">
                        <telerik:RadComboBox width="355"
                            AppendDataBoundItems="True"
                            DropDownCssClass="KDDL"
                            ID="comboDebitAcctNo" 
                            Runat="server"
                            MarkFirstMatch="True"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="comboDebitAcctNo_OnSelectedIndexChanged"
                            OnItemDataBound="comboDebitAcctNo_ItemDataBound"
                            AllowCustomText="false" >
                           <HeaderTemplate>
		                        <table cellpadding="0" cellspacing="0"> 
		                          <tr> 
			                         <td style="width:100px;"> 
				                        Id
			                         </td> 
                                      <td style="width:80px;"> 
				                        Currency
			                         </td>
			                         <td> 
				                        Name
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
                                        <td style="width:80px;"> 
					                        <%# DataBinder.Eval(Container.DataItem, "Currency")%> 
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
                <tr>    
                    <td style="width: 230px;" class="MyLable">Debit Currency</td>
                    <td class="MyContent">
                          <telerik:RadTextBox ID="txtDeitCurrency" runat="server" ReadOnly="true" />
                        <telerik:RadComboBox AppendDataBoundItems="True" Visible="false"
                            ID="comboDebitCurrency" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                             <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="USD" Text="USD" />
                                <telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                                <telerik:RadComboBoxItem Value="GBP" Text="GBP" />
                                <telerik:RadComboBoxItem Value="JPY" Text="JPY" />
                                <telerik:RadComboBoxItem Value="VND" Text="VND" />
                            </Items>
                        </telerik:RadComboBox>
                        <%--<asp:Label ID="lblDebitCurrency" runat="server" />--%>
                    </td>
                </tr>
                
                <tr>    
                    <td style="width: 230px;" class="MyLable">Debit Amount<span class="Required"> (*)</span></td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numDebitAmount" runat="server" 
                            AutoPostBack="True"
                             OnTextChanged="numDebitAmount_OnTextChanged" />
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator2"
                            ControlToValidate="numDebitAmount"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Debit Amount is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                
                <tr style="display: none;">    
                    <td style="width: 230px;" class="MyLable">Debit Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteDebitDate" runat="server" />
                    </td>
                </tr>
                
                <tr style="display: none">    
                    <td style="width: 230px;" class="MyLable">Amount Debited</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAmountDebited" runat="server" Text="0" />
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0" style="display: none;">
                <tr>    
                    <td style="width: 230px;" class="MyLable">TPKT</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadComboBox
                            ID="comboTPKT" Runat="server" AutoPostBack="True" OnSelectedIndexChanged="comboTPKT_OnSelectedIndexChanged"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="Doanh Nghiệp" Text="DN" />
                                <telerik:RadComboBoxItem Value="Tư Nhân" Text="TN" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="lblTPKTName" runat="server" Text="Doanh Nghiệp" />
                </tr>
            </table>
        </fieldset>
        
        <fieldset>
            <legend>
              <div style="font-weight:bold; text-transform:uppercase;">Credit Information</div>
            </legend>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 230px;" class="MyLable">Credit Account</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadComboBox width="355" 
                            AppendDataBoundItems="True"
                            DropDownCssClass="KDDL"
                            ID="comboCreditAccount" 
                            Runat="server" 
                            AutoPostBack="True" 
                            OnSelectedIndexChanged="comboCreditAccount_OnSelectedIndexChanged"
                            MarkFirstMatch="True" 
                            OnItemDataBound="commomSwiftCode_ItemDataBound"
                            AllowCustomText="false" >
                           <HeaderTemplate>
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
					                        <%# DataBinder.Eval(Container.DataItem, "AccountNo")%> 
				                         </td> 
				                         <td> 
					                        <%# DataBinder.Eval(Container.DataItem, "Description")%> 
				                         </td>
			                          </tr> 
		                           </table> 
                           </ItemTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="lblCreditAccount" runat="server"  />
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 230px;" class="MyLable">Credit Currency</td>
                    <td class="MyContent">
                        <telerik:RadComboBox AppendDataBoundItems="True"
                            ID="comboCreditCurrency" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
                
                <tr style="display: none">    
                    <td style="width: 230px;" class="MyLable">Treasury Rate</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numTreasuryRate" runat="server" />
                    </td>
                </tr>
                
                <tr>    
                    <td style="width: 230px;" class="MyLable">Credit Amount<span class="Required"> (*)</span></td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="numCreditAmount" runat="server" 
                            AutoPostBack="True"
                            OnTextChanged="numCreditAmount_OnTextChanged"  />
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator3"
                            ControlToValidate="numCreditAmount"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Credit Amount is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                
                <tr style="display: none;">    
                    <td style="width: 230px;" class="MyLable">Credit Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteCreditDate" runat="server" />
                    </td>
                </tr>
                <tr>    
                    <td style="width: 230px;" class="MyLable">Processing Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteProcessingDate" runat="server" Enabled="False" />
                    </td>
                </tr>
                <tr style="display: none;">    
                    <td style="width: 230px;" class="MyLable">Amount Credited</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAmtCredited" runat="server" Text="0" />
                    </td>
                </tr>
                <tr style="display: none;">    
                    <td  style="width: 230px;" class="MyLable">VAT Serial No.</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtVATSend" runat="server" Text="BA2014/789" />
                    </td>
                </tr>
                <tr>    
                    <td style="width: 230px;" class="MyLable">Add Remarks</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAddRemarks" runat="server" Width="355"/>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    
    <div id="MT103" class="dnnClear">
        <fieldset>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>    
                <td class="MyLable">Sender's Reference</td>
                <td class="MyContent">
                    <asp:Label ID="lblSenderReference" runat="server" />
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable">Bank Operation Code</td>
                <td class="MyContent">
                    <asp:Label ID="lblBankOperationCode" runat="server" Text="CRED" />
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable">Value Date</td>
                <td class="MyContent">
                    <telerik:RadDatePicker ID="dteValueDate" runat="server"  Enabled="False"/>
                </td>
            </tr>
            
            
            <tr>    
                <td class="MyLable" >Currency</td>
                <td class="MyContent">
                    <telerik:RadComboBox  AppendDataBoundItems="True"
                        ID="comboCurrency" Runat="server"
                        MarkFirstMatch="True"
                        AllowCustomText="false" >
                    </telerik:RadComboBox>
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable">InterBank Settle Amount</td>
                <td class="MyContent">
                    <asp:Label ID="lblInterBankSettleAmount" runat="server" Text="0" />
                </td>
            </tr>
            
            
            <tr>    
                <td class="MyLable">Instructed Amount</td>
                <td class="MyContent">
                    <asp:Label ID="lblInstancedAmount" runat="server" Text="0" />
                </td>
            </tr>
        </table>
        </fieldset>
        
        <fieldset>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>    
                <td class="MyLable">Ordering Customer account</td>
                <td class="MyContent">
                    <telerik:RadComboBox width="400"
                        AppendDataBoundItems="True"
                        ID="comboOrderingCustAcc" 
                        Runat="server"
                        MarkFirstMatch="True"
                        AllowCustomText="false" >
                    </telerik:RadComboBox>
                </td>
                <%--<td><asp:Label ID="lblOrderingCustAccName" runat="server" Text="KHOAN PTRA USD TRONG NV TT" />--%>
            </tr>
        </table>
        
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>    
                <td class="MyLable" >Ordering Customer</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtOrderingCustomer1" runat="server" Width="400"/>
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable" >Ordering Customer</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtOrderingCustomer2" runat="server" Width="400"/>
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable" >Ordering Customer</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtOrderingCustomer3" runat="server" Width="400"/>
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable" >Ordering Customer</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtOrderingCustomer4" runat="server" Width="400"/>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0"> 
            <tr>    
                <td class="MyLable">Ordering Institution</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtOrderingInstitution" runat="server" Width="400" />
                </td>
            </tr>
        </table>
    </fieldset>
        
    <fieldset>
        <table width="100%" cellpadding="0" cellspacing="0"> 
            <tr>    
                <td class="MyLable">Sender's Correspondent</td>
                <td class="MyContent">
                    <asp:Label ID="lblSenderCorrespondent" runat="server" />
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>    
                <td class="MyLable">Receiver's Correspondent</td>
                <td style="width: 150px" class="MyContent">
                    <telerik:RadComboBox DropDownCssClass="KDDL"
                        AutoPostBack="true" Width="400" AppendDataBoundItems="True"
                    OnSelectedIndexChanged="comboReceiverCorrespondent_SelectIndexChange"
                        ID="comboReceiverCorrespondent" Runat="server" OnItemDataBound="commomSwiftCode_ItemDataBound"
                        MarkFirstMatch="True"
                        AllowCustomText="false" >
                        <HeaderTemplate>
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
                <td><asp:Label ID="lblReceiverCorrespondentName" runat="server" />
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>    
                <td class="MyLable" >Receiver Corr Bank Acct</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtReceiverCorrBankAct" runat="server"/>
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable">Intermediary Type</td>
                <td class="MyContent">
                    <telerik:RadComboBox AutoPostBack="True" OnSelectedIndexChanged="comboIntermediaryType_OnSelectedIndexChanged"
                        ID="comboIntermediaryType" Runat="server"
                        MarkFirstMatch="True"
                        AllowCustomText="false" >
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
                <td class="MyLable">Intermediary Institution</td>
                <td style="width: 150px" class="MyContent">
                    <telerik:RadTextBox ID="txtIntermediaryInstitutionNo" runat="server" 
                        AutoPostBack="True" 
                        OnTextChanged="txtIntermediaryInstitutionNo_OnTextChanged" />
                </td>
                <td>
                    <asp:Label ID="lblIntermediaryInstitutionName" runat="server"/>
                    <asp:Label ID="lblIntermediaryInstitutionNoError" runat="server" ForeColor="red" />
                </td>
            </tr>
        </table>
        
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>    
                <td class="MyLable" >Intermediary Acct</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtIntermediaryInstruction1" runat="server" Width="400"/>
                </td>
            </tr>

            <tr>    
                <td class="MyLable" >Intermediary Acct</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtIntermediaryInstruction2" runat="server" Width="400"/>
                </td>
            </tr>

            <tr>    
                <td class="MyLable" >Intermediary Bank Acct</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtIntermediaryBankAcct" runat="server"/>
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable">Account Type</td>
                <td class="MyContent">
                    <telerik:RadComboBox AutoPostBack="True" OnSelectedIndexChanged="comboAccountType_OnSelectedIndexChanged"
                        ID="comboAccountType" Runat="server"
                        MarkFirstMatch="True"
                        AllowCustomText="false" >
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
                <td class="MyLable">Account With Institution</td>
                <td style="width: 150px" class="MyContent">
                    <telerik:RadTextBox ID="txtAccountWithInstitutionNo" runat="server" 
                        AutoPostBack="True" 
                        OnTextChanged="txtAccountWithInstitutionNo_OnTextChanged" />
                </td>
                <td><asp:Label ID="lblAccountWithInstitutionName" runat="server" />
                <td><asp:Label ID="lblAccountWithInstitutionNoError" runat="server" ForeColor="red" />
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0"> 
            <tr>    
                <td class="MyLable">Account With Bank Acct</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtAccountWithBankAcct" runat="server" Width="400"/>
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable">Account With Bank Acct</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtAccountWithBankAcct2" runat="server" Width="400"/>
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable">Beneficiary Account</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtBeneficiaryCustomer1" runat="server" Width="400" />
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable">Beneficiary Name & Addr.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtBeneficiaryCustomer2" runat="server" Width="400" />
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable">Beneficiary Name & Addr.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtBeneficiaryCustomer3" runat="server" Width="400" />
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable">Beneficiary Name & Addr.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtBeneficiaryCustomer4" runat="server" Width="400" />
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable">Beneficiary Name & Addr.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtBeneficiaryCustomer5" runat="server" Width="400" />
                </td>
            </tr>
        </table>
        </fieldset>
        
        <fieldset>
        <table width="100%" cellpadding="0" cellspacing="0"> 
            <tr>    
                <td class="MyLable">Remittance Information</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtRemittanceInformation" runat="server" Text="" Width="400" />
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable">Detail of Charges</td>
                <td class="MyContent">
                    <telerik:RadComboBox
                        ID="comboDetailOfCharges" 
                        AutoPostBack="True" 
                        OnSelectedIndexChanged="comboDetailOfCharges_OnSelectedIndexChanged"
                        Runat="server"
                        MarkFirstMatch="True"
                        AllowCustomText="false" >
                        <Items>
                            <telerik:RadComboBoxItem Value="BEN" Text="BEN" />
                            <telerik:RadComboBoxItem Value="OUR" Text="OUR" />
                            <telerik:RadComboBoxItem Value="SHA" Text="SHA" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable">Sender's Charges</td>
                <td class="MyContent">
                    <asp:Label ID="lblSenderCharges" runat="server" Text="0" />
                </td>
            </tr>

            <tr>    
                <td class="MyLable">Receiver's Charges</td>
                <td class="MyContent">
                    <asp:Label ID="lblReceiverCharges" runat="server" Text="0" />
                </td>
            </tr>
            
            <tr>    
                <td class="MyLable">Sender to Receiver Info</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtSenderToReceiverInfo" runat="server" Width="400" />
                </td>
            </tr>
        </table>
    </fieldset>
        
    </div>
    
    <div id="ChargeCommission" class="dnnClear">
        <fieldset>
             <table width="100%" cellpadding="0" cellspacing="0">
            <tr>    
                    <td class="MyLable">Waive Charges</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            AutoPostBack="True"
                            OnSelectedIndexChanged="comboCommissionCode_OnSelectedIndexChanged"
                            ID="comboCommissionCode" 
                            Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="NO" Text="NO" />
                                <telerik:RadComboBoxItem Value="YES" Text="YES" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">Charge Acct</td>
                    <td  class="MyContent">
                        <telerik:RadComboBox width="355" 
                            DropDownCssClass="KDDL"
                            ID="comboChargeAcct" Runat="server" 
                            AppendDataBoundItems="True" 
                            AutoPostBack="True" 
                            OnSelectedIndexChanged="comboChargeAcct_OnSelectedIndexChanged"
                            MarkFirstMatch="True" 
                            OnItemDataBound="comboDebitAcctNo_ItemDataBound" 
                            AllowCustomText="false">
                            <HeaderTemplate>
		                        <table cellpadding="0" cellspacing="0"> 
		                          <tr> 
			                         <td style="width:100px;"> 
				                        Id
			                         </td> 
                                      <td style="width:80px;"> 
				                        Currency
			                         </td>
			                         <td> 
				                        Name
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
                                        <td style="width:80px;"> 
					                        <%# DataBinder.Eval(Container.DataItem, "Currency")%> 
				                         </td>
				                         <td> 
					                        <%# DataBinder.Eval(Container.DataItem, "Name")%> 
				                         </td>
                                         
			                          </tr> 
		                           </table> 
                           </ItemTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td style="display: none"><asp:Label ID="txtChargeAcctName" runat="server" /></td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display:none">    
                    <td class="MyLable">Display Charges/Com</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboDisplayChargesCom" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
                
                
                
                <tr>    
                    <td class="MyLable">Commission Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboCommissionType"
                            Runat="server" 
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                             <Items>
                                <telerik:RadComboBoxItem Value="TTCHRGADV" Text="TTCHRGADV" />
                                <telerik:RadComboBoxItem Value="TTCHRGREC" Text="TTCHRGREC" />
                                <telerik:RadComboBoxItem Value="TTCHRGIND" Text="TTCHRGIND" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">Commission Amount<span class="Required"> (*)</span></td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadNumericTextBox ID="numCommissionAmount" runat="server"  
                            AutoPostBack="True" 
                            OnTextChanged="numCommissionAmount_OnTextChanged"/>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator4"
                            ControlToValidate="numCommissionAmount"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[Charge Commission]Commission Amount is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <telerik:RadTextBox runat="server" ID="txtCommissionCurrency" ReadOnly="true"></telerik:RadTextBox>
                        <telerik:RadComboBox Width="100" Visible="false"
                            ID="comboCommissionCurrency" Runat="server" 
                            AutoPostBack="True"
                            OnSelectedIndexChanged="comboCommissionCurrency_OnSelectedIndexChanged"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
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
                            ID="RequiredFieldValidator5"
                            ControlToValidate="comboCommissionCurrency"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[Charge Commission]Commission Currency is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0"> 
                <tr>    
                    <td class="MyLable">Commission For</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboCommissionFor" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="SENDER" Text="SENDER" />
                                <telerik:RadComboBoxItem Value="RECEIVER" Text="RECEIVER" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                
                <tr style="display: none;">    
                    <td class="MyLable">Charge Code</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboChargeCode" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="DEBIT PLUS CHARGES" Text="DEBIT PLUS CHARGES" />
                                <telerik:RadComboBoxItem Value="WAIVE" Text="WAIVE" />
                                <telerik:RadComboBoxItem Value="CREDIT LESS CHARGES" Text="CREDIT LESS CHARGES" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                
                <tr>    
                    <td class="MyLable">Charge Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboChargeType" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="TTCABLE" Text="TTCABLE" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">Charge Amount<span class="Required"> (*)</span></td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadNumericTextBox ID="numChargeAmount" Runat="server" AutoPostBack="True" 
                            OnTextChanged="numChargeAmount_OnTextChanged"/>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator6"
                            ControlToValidate="numChargeAmount"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[Charge Commission]Charge Amount is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <telerik:RadTextBox runat="server" ID="txtChargeCurrency" ReadOnly="true"></telerik:RadTextBox>
                        <telerik:RadComboBox Width="100"
                            ID="comboChargeCurrency" Visible="false"
                            Runat="server"
                            AutoPostBack="True"
                            OnSelectedIndexChanged="comboChargeCurrency_OnSelectedIndexChanged"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
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
                            ID="RequiredFieldValidator7"
                            ControlToValidate="comboChargeCurrency"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="[Charge Commission]Charge Currency is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">Charge For</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboChargeFor" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="SENDER" Text="SENDER" />
                                <telerik:RadComboBoxItem Value="RECEIVER" Text="RECEIVER" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                
                <tr style="display: none;">    
                    <td class="MyLable">Detail of Charges</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="comboDetailOfCharges_TabChargeInfo" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="BEN" Text="BEN" />
                                <telerik:RadComboBoxItem Value="OUR" Text="OUR" />
                                <telerik:RadComboBoxItem Value="SHA" Text="SHA" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                
                <tr>    
                    <td class="MyLable">VAT No</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtVATNo" runat="server" />
                    </td>
                </tr>
                
                <tr>    
                    <td class="MyLable">Add Remarks</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAddRemarks1" runat="server" Width="355" />
                    </td>
                </tr>
                
                <tr>    
                    <td class="MyLable">Add Remarks</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAddRemarks2" runat="server" Width="355" />
                    </td>
                </tr>
            </table> 
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">Account  Officer</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtProfitCenteCust" runat="server" Width="355" />
                    </td>
                </tr>
                
                <tr>    
                    <td class="MyLable">Total Charge Amount</td>
                    <td class="MyContent">
                        <asp:Label ID="lblTotalChargeAmount" runat="server" />
                    </td>
                </tr>
                
                <tr>    
                    <td class="MyLable">Total Tax Amount</td>
                    <td class="MyContent">
                        <asp:Label ID="lblTotalTaxAmount" runat="server" />
                    </td>
                </tr>

            </table>
        </fieldset>

    </div>
</div>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="comboCreditAccount">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblCreditAccount" />
                <telerik:AjaxUpdatedControl ControlID="comboReceiverCorrespondent" />
                <telerik:AjaxUpdatedControl ControlID="lblReceiverCorrespondentName" />
                <telerik:AjaxUpdatedControl ControlID="comboCreditCurrency" />
                <telerik:AjaxUpdatedControl ControlID="comboCurrency" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="comboCountryCode">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblCountryCodeName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="comboTPKT">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblTPKTName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="comboCommoditySer">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblCommoditySerName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
       
        <telerik:AjaxSetting AjaxControlID="comboOtherBy">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblOtherByName" />
                <telerik:AjaxUpdatedControl ControlID="txtOrderingCustomer1" />
                <telerik:AjaxUpdatedControl ControlID="txtOrderingCustomer2" />
                <telerik:AjaxUpdatedControl ControlID="txtOrderingCustomer3" />
                <telerik:AjaxUpdatedControl ControlID="txtOrderingCustomer4" />
                
                <telerik:AjaxUpdatedControl ControlID="txtOtherBy2" />
                <telerik:AjaxUpdatedControl ControlID="txtOtherBy3" />
                <telerik:AjaxUpdatedControl ControlID="txtOtherBy4" />
                <telerik:AjaxUpdatedControl ControlID="txtOtherBy5" />
                
                <telerik:AjaxUpdatedControl ControlID="comboDebitAcctNo" />
                <telerik:AjaxUpdatedControl ControlID="txtDeitCurrency" />
                <telerik:AjaxUpdatedControl ControlID="comboCreditCurrency" />
                <telerik:AjaxUpdatedControl ControlID="comboCreditAccount" />
                
                <telerik:AjaxUpdatedControl ControlID="comboOrderingCustAcc" />
                <telerik:AjaxUpdatedControl ControlID="comboChargeAcct" />
                <telerik:AjaxUpdatedControl ControlID="txtCommissionCurrency" />
                <telerik:AjaxUpdatedControl ControlID="txtChargeCurrency" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="comboTransactionType">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lbTransactionTypeName" />
                <telerik:AjaxUpdatedControl ControlID="txtCommissionType" />
                <telerik:AjaxUpdatedControl ControlID="comboCommoditySer" />
                
            </UpdatedControls>
        </telerik:AjaxSetting>

        <%--<telerik:AjaxSetting AjaxControlID="comboOrderingCustAcc">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblOrderingCustAccName" />
            </UpdatedControls>
        </telerik:AjaxSetting>--%>
        
        <telerik:AjaxSetting AjaxControlID="comboChargeAcct">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="txtChargeAcctName" />
                <telerik:AjaxUpdatedControl ControlID="comboCommissionCurrency" />
                <telerik:AjaxUpdatedControl ControlID="comboChargeCurrency" />
                <telerik:AjaxUpdatedControl ControlID="txtCommissionCurrency" />
                <telerik:AjaxUpdatedControl ControlID="txtChargeCurrency" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboProfitCenteCust">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblProfitCenteCustName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="txtOtherBy2">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="txtOrderingCustomer1" />
                <telerik:AjaxUpdatedControl ControlID="comboDebitAcctNo" />
                <telerik:AjaxUpdatedControl ControlID="txtDeitCurrency" />
                <telerik:AjaxUpdatedControl ControlID="comboCreditCurrency" />
                
                <telerik:AjaxUpdatedControl ControlID="comboOrderingCustAcc" />
                <telerik:AjaxUpdatedControl ControlID="comboChargeAcct" />
                <telerik:AjaxUpdatedControl ControlID="txtCommissionCurrency" />
                <telerik:AjaxUpdatedControl ControlID="txtChargeCurrency" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="txtOtherBy3">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="txtOrderingCustomer2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="txtOtherBy4">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="txtOrderingCustomer3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="txtOtherBy5">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="txtOrderingCustomer4" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboReceiverCorrespondent">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblReceiverCorrespondentName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboDetailOfCharges">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblInstancedAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblInterBankSettleAmount" />
                <telerik:AjaxUpdatedControl ControlID="comboChargeAcct" />
                <telerik:AjaxUpdatedControl ControlID="txtChargeAcctName" />
                
                <telerik:AjaxUpdatedControl ControlID="txtCommissionCurrency" />
                <telerik:AjaxUpdatedControl ControlID="txtChargeCurrency" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="numCreditAmount">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="numDebitAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblInstancedAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblInterBankSettleAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
         <telerik:AjaxSetting AjaxControlID="numDebitAmount">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="numCreditAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblInstancedAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblInterBankSettleAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="numCommissionAmount">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblInterBankSettleAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalTaxAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblSenderCharges" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="numChargeAmount">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblInterBankSettleAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalTaxAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblSenderCharges" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboAccountType">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="txtAccountWithInstitutionNo" />
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithBankAcct" />
                <telerik:AjaxUpdatedControl ControlID="txtAccountWithBankAcct2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboIntermediaryType">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="txtIntermediaryInstitutionNo" />
                <telerik:AjaxUpdatedControl ControlID="txtIntermediaryInstruction1" />
                <telerik:AjaxUpdatedControl ControlID="txtIntermediaryInstruction2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboChargeCurrency">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="comboCommissionCurrency" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboCommissionCurrency">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="comboChargeCurrency" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="txtIntermediaryInstitutionNo">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblIntermediaryInstitutionNoError" />
                <telerik:AjaxUpdatedControl ControlID="lblIntermediaryInstitutionName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="txtAccountWithInstitutionNo">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblAccountWithInstitutionNoError" />
                <telerik:AjaxUpdatedControl ControlID="lblAccountWithInstitutionName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboDebitAcctNo">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="txtDeitCurrency" />
                <telerik:AjaxUpdatedControl ControlID="comboChargeAcct" />
                
                <telerik:AjaxUpdatedControl ControlID="txtCommissionCurrency" />
                <telerik:AjaxUpdatedControl ControlID="txtChargeCurrency" />
                <telerik:AjaxUpdatedControl ControlID="txtChargeAcctName" />
                
                <telerik:AjaxUpdatedControl ControlID="comboCreditAccount" />
                <telerik:AjaxUpdatedControl ControlID="comboCreditCurrency" />
                
                <telerik:AjaxUpdatedControl ControlID="comboOrderingCustAcc" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="comboCommissionCode">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="comboCommissionType" />
                <telerik:AjaxUpdatedControl ControlID="numCommissionAmount" />
                <telerik:AjaxUpdatedControl ControlID="comboCommissionFor" />
                <telerik:AjaxUpdatedControl ControlID="comboChargeCode" />
                <telerik:AjaxUpdatedControl ControlID="comboChargeType" />
                <telerik:AjaxUpdatedControl ControlID="numChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="comboChargeFor" />
                <telerik:AjaxUpdatedControl ControlID="comboDetailOfCharges_TabChargeInfo" />
                <telerik:AjaxUpdatedControl ControlID="txtVATNo" />
                <telerik:AjaxUpdatedControl ControlID="txtAddRemarks1" />
                <telerik:AjaxUpdatedControl ControlID="txtAddRemarks2" />
                <telerik:AjaxUpdatedControl ControlID="txtProfitCenteCust" />
                <telerik:AjaxUpdatedControl ControlID="comboChargeAcct" />
                
            </UpdatedControls>
        </telerik:AjaxSetting>
        
    </AjaxSettings>
</telerik:RadAjaxManager>

<div style="visibility:hidden;"><asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnMT103Report" runat="server" OnClick="btnMT103Report_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnPhieuCKReport" runat="server" OnClick="btnPhieuCKReport_Click" Text="Search" /></div>
<div style="visibility:hidden;"><asp:Button ID="btnVAT" runat="server" OnClick="btnVATReport_Click" Text="Search" /></div>
<asp:HiddenField ID="hf_AccountId" runat="server" />
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
  <script type="text/javascript">
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
              window.location.href = "Default.aspx?tabid=251&CodeID=" + $("#<%=txtCode.ClientID %>").val();
          }
      });
  </script>
</telerik:RadCodeBlock>