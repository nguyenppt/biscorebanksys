<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Discounted.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Open.Discounted" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%--<%@ Register Src="../../../../Controls/VVComboBox.ascx" TagPrefix="uc1" TagName="VVComboBox" %>--%>
<%@ Register src="../../../../Controls/VVComboBox.ascx" tagname="VVComboBox" tagprefix="uc2" %>
<%@ Register Src="../../../../Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>


<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs({ selected: 0, cookieMilleseconds: 0 });
    });
</script>

<style>
    .cssProductLine:hover {
        background-color:#CCC;
    }
    #tabs-demo input {
        width: 315px;
    }
    #tabs-demo .riTextBox {
        width: 320px;
    }
    #ChristopherColumbus .rcbInput  {
        width: 298px;
    }

    #TradSav .rcbInput, #DiscInterest .rcbInput {
        width: 298px;
    }
    
</style>
<telerik:RadWindowManager runat="server" id="RadWindowManager2"></telerik:RadWindowManager>


<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit" Enabled ="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btPreview" CommandName="preview" Enabled ="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize" Enabled ="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse" Enabled ="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="search" Enabled ="false">
        </telerik:RadToolBarButton>
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print" Enabled ="false">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
    

         
<div>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 200px; padding: 5px 0 5px 20px;">                
                <asp:TextBox ID="tbDepositCode" runat="server" Width="200" ></asp:TextBox>
            </tr>
        </table>
  
</div>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a id="DepositTermSavings" href="#ChristopherColumbus">Deposit - Term Savings</a></li>
        <li><a id="TrandSavDeposited" href="#TradSav">Trand Sav - Deposited</a></li>
        <li><a id="DiscInterestPayment" href="#DiscInterest">Disc Interest Payment</a></li>
       <%-- <li><a href="#FullView">Full View</a></li>--%>
    </ul>

    <div id="ChristopherColumbus" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="Commit" />
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Customer </td>
                    <td class="MyContent">
                        <asp:Label ID="lblCustomerName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Working AC Currency </td>
                    <td class="MyContent">
                        <asp:Label ID="lblCurrency" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Working account <span class="Required">(*)</span>
                         <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator6"
                            ControlToValidate="rcbWorkingAccount"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Working Account is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:RadComboBox appenddatabounditems="True"
                            ID="rcbWorkingAccount" runat="server" OnSelectedIndexChanged="rcbWorkingAccount_SelectedIndexChanged"
                            MarkFirstMatch="True" AutoPostBack="true" height="200"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                             <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />                          
                            </Items>
                        </telerik:RadComboBox>
                        <%--<i>Traditional Savings INT in Periodic</i>--%>
                    </td>
                </tr>            
    
                <tr>
                    <td class="MyLable">Amount LCY</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox runat="server" Enabled ="false"  id="rtbAccountLCY" minvalue="0" maxvalue="999999999999"
                             AutoPostBack="true" OnTextChanged="rtbAccountLCY_TextChanged">
                            <NumberFormat GroupSeparator="," DecimalDigits="5" AllowRounding="true"    KeepNotRoundedValue="false"   />                         
                        </telerik:radnumerictextbox>                       
                    </td>
                </tr>
                   <tr>
                    <td class="MyLable">Amount FCY</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox  runat="server" Enabled ="false"  id="rtbAccountFCY" minvalue="0" maxvalue="999999999999"
                            AutoPostBack="true" OnTextChanged="rtbAccountLCY_TextChanged">
                            <NumberFormat GroupSeparator="," DecimalDigits="5" AllowRounding="true"   KeepNotRoundedValue="false"  />
                            
                        </telerik:radnumerictextbox>                       
                    </td>
                </tr>
                <%--<tr>
                    <td class="MyLable">Amount FCY</td>
                    <td class="MyContent">
                        <asp:TextBox ID="rtbAmountFCY" runat="server" Width="150" /></td>
                </tr>--%>
                <tr>
                    <td class="MyLable">Narrative</td>
                    <td class="MyContent">
                        <asp:TextBox ID="rtbNarrative" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Deal Rate</td>
                    <td class="MyContent">
                         <telerik:radnumerictextbox  runat="server" Enabled ="false"  id="rnbDealRate" minvalue="0" maxvalue="999999999999"
                             AutoPostBack="true" OnTextChanged="rtbAccountLCY_TextChanged">
                            <NumberFormat GroupSeparator="," DecimalDigits="5" AllowRounding="true"   KeepNotRoundedValue="false"  />
                           
                        </telerik:radnumerictextbox>   
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Amt Credit for Cust </td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="lblAmtCreditForCust" ReadOnly="true" BorderWidth="0" runat="server"></telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">New Cust Bal </td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="lblNewCustBal" ReadOnly="true" BorderWidth="0" runat="server"></telerik:RadNumericTextBox>
                    </td>
                </tr>
            </table>
            <hr />
            <table style="width:100%; padding:0px;">
                <tr>
                    <td class="MyLable">Payment CCY </td>
                    <td class="MyContent">
                      <telerik:RadComboBox 
                            ID="rcbPaymentCCY" runat="server" appenddatabounditems="True"
                            MarkFirstMatch="True" Height="150px"
                           
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
                    <td class="MyLable">For Teller</td>
                    <td class="MyContent">
                        <telerik:RadTextBox
                            ID="rtbForTeller" runat="server" >                        
                        </telerik:RadTextBox>
                    </td>
                    <%--<td class="MyContent">
                            <asp:Label ID="lblAccOfficer" runat="server" /></td>--%>
                </tr>

                <tr>
                    <td class="MyLable">
                        Debit Account
                    </td>
                 <td class="MyContent" >
                    <telerik:RadComboBox ID="rcbDebitAccount" appenddatabounditems="True"
                        MarkFirstMatch="True" 
                        AllowCustomText="false"
                        runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />                          
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
    </div>

    <div id="TradSav" class="dnnClear">      
        <fieldset>
            <legend style="text-transform:uppercase; font-weight:bold">Customer Details</legend>
             <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="display:none">
                      <td class="MyLable">LD id </td>
                      <td class="MyContent"   >
                          <asp:Label ID="LDid"  runat="server" text =""/>
                      </td>
                  </tr>
                 <tr>
                     <td class="MyLable">Customer.<span class="Required">(*)</span></td>
                     <td class="MyContent">
                          <asp:Label ID="lblCustomerNo" style="display:none" runat="server" text =""/>                        
                         <asp:Label ID="lblCustomerNoTitle" runat="server" text =""/>                        
                    </td>
                 </tr>
                 <tr>
                     <td class="MyLable">ID Join Holder </td>
                     <td class="MyContent">
                         <telerik:RadComboBox appenddatabounditems="True"
                             ID="rcbJoinHolder" runat="server"
                             MarkFirstMatch="True" height="200"
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
            </fieldset>

         <fieldset>
             <legend style="text-transform: uppercase; font-weight: bold">Customer Details</legend>
                <table width="100%" cellpadding="0" cellspacing="0">                
                  <tr>
                      <td class="MyLable">Product Line </td>
                      <td class="MyContent" >
                           <telerik:RadComboBox appenddatabounditems="True"
                             ID="rcbProductLine" runat="server"
                             MarkFirstMatch="True" height="200"
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
                      <td class="MyLable"  >Currency</td>
                      <td class="MyContent">
                           <telerik:RadComboBox appenddatabounditems="True" Enabled ="false"
                             ID="rcbCurrencyAmount" runat="server"
                             MarkFirstMatch="True"  
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
                        <td class="MyLable"  >Amount </td>
                        <td class="MyContent">     
                            <telerik:radnumerictextbox width="50"  runat="server" Enabled ="false"  id="rnbAmount" minvalue="0" maxvalue="999999999999">
                                <NumberFormat GroupSeparator="," DecimalDigits="2" AllowRounding="true"   KeepNotRoundedValue="false"  />                                
                            </telerik:radnumerictextbox>    
                        </td>                    
                  </tr>            
                  <tr>
                      <td class="MyLable">Value Date </td>
                      <td class="MyContent"  >
                           <telerik:RadDatePicker dateinput-dateformat="dd/MM/yyyy" runat="server" ID="rdpValueDate"></telerik:RadDatePicker>
                      </td>
                  </tr>                
                 <tr>
                      <td class="MyLable">Term </td>
                      <td class="MyContent" >
                           <telerik:radcombobox appenddatabounditems="True"
                            id="rtbTerm" runat="server"
                            onclientselectedindexchanged="TermOnBlur"
                            markfirstmatch="True" height="150px" 
                            allowcustomtext="false">
                                <ExpandAnimation Type="None" />
                                <CollapseAnimation Type="None" />
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />                                          
                                </Items>                           
                        </telerik:radcombobox>
                         
                      </td>
                  </tr>
                  <tr>
                      <td class="MyLable">Final Mat Date <span class="Required">(*)</span>
                           <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator1"
                            ControlToValidate="rdpFinalMatDate"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Final Mat Date is required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                      </td>
                      <td class="MyContent" >
                           <telerik:RadDatePicker dateinput-dateformat="dd/MM/yyyy" runat="server" ID="rdpFinalMatDate"></telerik:RadDatePicker>                          
                      </td>
                  </tr>
                  <tr>
                      <td class="MyLable">Interest Rate </td>
                      <td class="MyContent" >
                           <telerik:radnumerictextbox ClientEvents-OnBlur="CalculatorAmmount" runat="server"  id="rtbInterestRate" minvalue="0" maxvalue="999999999999">
                                <NumberFormat GroupSeparator="," DecimalDigits="5" AllowRounding="true"   KeepNotRoundedValue="false"  />                                
                            </telerik:radnumerictextbox>    
                           <%--<telerik:RadTextBox runat="server" ID="rtbInterestRate" ClientEvents-OnBlur="CalculatorAmmount"></telerik:RadTextBox>--%>
                      </td>
                  </tr>
                  <tr>
                      <td class="MyLable">Total Int amt</td>
                      <td class="MyContent" >
                           <asp:Label runat="server" ID="lblTotalInt"></asp:Label>
                      </td>
                  </tr>

                  <tr>
                      <td class="MyLable">Working Account</td>
                      <td class="MyContent" >
                            <telerik:RadTextBox runat="server" ReadOnly="true" ID="rtbWorkingAccount"></telerik:RadTextBox>
                            <div style="display:none">
                                <telerik:RadTextBox style="display:none" runat="server" ID="rtbWorkingAccountId"></telerik:RadTextBox>
                                <telerik:RadTextBox style="display:none" runat="server" ID="rtbWorkingAccountName"></telerik:RadTextBox>
                            </div>
                      </td>
                  </tr>
                   <tr>
                      <td class="MyLable">Account Officer</td>
                      <td class="MyContent" >
                           <telerik:RadComboBox appenddatabounditems="True"
                             ID="rcbAccountOfficer" runat="server"
                             MarkFirstMatch="True" 
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
         </fieldset>

    </div>
    
    <div id="DiscInterest" class="dnnClear">
        <div style="padding-left:10px">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer</td>
                <td class="MyContent" >
                    <asp:Label runat="server" ID="lblCustomerName2"  ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Currency</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="lblCurrency2" ReadOnly="true" BorderWidth="0" runat="server"></telerik:RadTextBox>                    
                </td>
            </tr>
        
            <tr>
                <td class="MyLable">Dr Account</td>
                <td class="MyContent">
                    <asp:Label runat="server" ID="rcbDrAccount"></asp:Label>
                    <asp:Label runat="server" style="display:none" ID="rcbDrAccountId"></asp:Label>
                    <asp:Label runat="server" style="display:none" ID="rcbDrAccountName"></asp:Label>
                </td>
            </tr>
       
            <tr>
                <td class="MyLable">Amount LCY</td>
                <td class="MyContent">
                 <telerik:RadNumericTextBox ID="txtAmountLCY" Enabled ="false" NumberFormat-DecimalDigits="0" runat="server">
                          <ClientEvents OnBlur="txtAmountLCY_OnclientValueChanged"  />
                 </telerik:RadNumericTextBox>
                </td>
            </tr>
      
            <tr>
                <td class="MyLable">Amount FCY</td>
                <td class="MyContent">
                 <telerik:RadNumericTextBox ID="txtAmountFCY" Enabled ="false"  runat="server">
                          <ClientEvents OnBlur="txtAmountLCY_OnclientValueChanged" />
                 </telerik:RadNumericTextBox>
                </td>
            </tr>
       
            <tr>
                <td class="MyLable">Narrative</td>
                <td class="MyContent"> 
                    <asp:TextBox ID="lbNarrative" BorderWidth="0" ReadOnly="true"  runat="server" />
                </td>
            </tr>
                   
            <tr>
                <td class="MyLable">Payment Ccy</td>
                <td class="MyContent">
                   <telerik:RadComboBox appenddatabounditems="True"
                        ID="rcbPaymentCcy2" runat="server"  onclientselectedindexchanged="Exch"
                        MarkFirstMatch="True"                        
                        autopostback="true"
                        onselectedindexchanged="rcbCurrentcy_SelectIndexChange"
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
                <td class="MyLable">For Teller</td>
                <td class="MyContent">
                 <telerik:RadTextBox ID="txtForTeller"  runat="server" ></telerik:RadTextBox>
                </td>
            </tr>
    
            <tr>
                <td class="MyLable">Credit Account</td>
                <td class="MyContent">
                     <telerik:RadComboBox appenddatabounditems="True"
                        ID="rcbCreditAccount" runat="server" 
                        MarkFirstMatch="True" 
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
                <td class="MyLable">Exch Rate</td>
                <td class="MyContent">
                 <telerik:RadNumericTextBox ID="txtExchRate" ClientEvents-OnBlur="Exch"  runat="server"></telerik:RadNumericTextBox>
                </td>
            </tr>
       
            <tr>
                <td class="MyLable">New Cust Bal</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="lbNewCustBal" ReadOnly="true" BorderWidth="0" runat="server"></telerik:RadNumericTextBox>
                </td>
            </tr>
      
            <tr>
                <td class="MyLable">Amt paid</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="lbAmtPaid" ReadOnly="true" BorderWidth="0" runat="server"></telerik:RadNumericTextBox>
                </td>
            </tr>
        </table>
        </div>


    
</div>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"
    DefaultLoadingPanelID="rcbupdatepanel">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbWorkingAccount">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblCustomerName" />
                <telerik:AjaxUpdatedControl ControlID="lblCustomerName2" />
                <telerik:AjaxUpdatedControl ControlID="lblCurrency" />
                <telerik:AjaxUpdatedControl ControlID="lblCurrency2" />
                <telerik:AjaxUpdatedControl ControlID="rcbDebitAccount" />
                <telerik:AjaxUpdatedControl ControlID="rcbCurrencyAmount" />
                <telerik:AjaxUpdatedControl ControlID="rcbPaymentCCY" />
                <telerik:AjaxUpdatedControl ControlID="lblCustomerNo" />
                <telerik:AjaxUpdatedControl ControlID="lblCustomerNoTitle" />
                <telerik:AjaxUpdatedControl ControlID="rtbWorkingAccount" />
                <telerik:AjaxUpdatedControl ControlID="rtbWorkingAccountId" />
                <telerik:AjaxUpdatedControl ControlID="rtbWorkingAccountName" />
                <telerik:AjaxUpdatedControl ControlID="rcbDrAccount" />
                <telerik:AjaxUpdatedControl ControlID="rtbAccountFCY" />
                <telerik:AjaxUpdatedControl ControlID="rtbAccountLCY" />
                <telerik:AjaxUpdatedControl ControlID="rnbDealRate" />                
                <telerik:AjaxUpdatedControl ControlID="rcbProductLine" />
                <telerik:AjaxUpdatedControl ControlID="rcbDrAccount" />    
                <telerik:AjaxUpdatedControl ControlID="rcbDrAccountId" />    
                <telerik:AjaxUpdatedControl ControlID="rcbDrAccountName" />    
                <telerik:AjaxUpdatedControl ControlID="rcbAccountOfficer" />   
                <telerik:AjaxUpdatedControl ControlID="rcbJoinHolder" />   
                <telerik:AjaxUpdatedControl ControlID="hidbal" />        
                <telerik:AjaxUpdatedControl ControlID="rcbPaymentCcy2" />  
                <telerik:AjaxUpdatedControl ControlID="tbcustomId" />  
                <telerik:AjaxUpdatedControl ControlID="rcbCreditAccount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="rcbPaymentCcy2">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="rcbCreditAccount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rtbAccountLCY">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAmtCreditForCust" />
                <telerik:AjaxUpdatedControl ControlID="lblNewCustBal" />
                <telerik:AjaxUpdatedControl ControlID="rnbAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rtbAccountFCY">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAmtCreditForCust" />
                <telerik:AjaxUpdatedControl ControlID="lblNewCustBal" />
                <telerik:AjaxUpdatedControl ControlID="rtbAccountLCY" />
                <telerik:AjaxUpdatedControl ControlID="rnbAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rnbDealRate">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblAmtCreditForCust" />
                <telerik:AjaxUpdatedControl ControlID="lblNewCustBal" />
                <telerik:AjaxUpdatedControl ControlID="rtbAccountLCY" />
                <telerik:AjaxUpdatedControl ControlID="rnbAmount" />
                 
            </UpdatedControls>
        </telerik:AjaxSetting>
        
    </AjaxSettings>

</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        $("#<%=tbDepositCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btSearch.ClientID %>").click();
            }

        });

        function AmountOnBlur(sender, eventArgs) {
            var fcyValue = $find("<%= rtbAccountFCY.ClientID %>").get_value(); 
            var dealValue = $find("<%= rnbDealRate.ClientID %>").get_value();
            if (fcyValue != '' && dealValue != '') {
                var value = fcyValue * dealValue;
                $find("<%= rtbAccountLCY.ClientID %>").set_value(value);
                $find("<%= lblAmtCreditForCust.ClientID %>").set_value(value);
                $find("<%= lblNewCustBal.ClientID %>").set_value(parseFloat(value) + parseFloat($find("<%= hidbal.ClientID %>").get_value()));
                if ($("#<%= lblCurrency.ClientID %>").text() == "VND") {
                    $find("<%= rnbAmount.ClientID %>").set_value(value);
                } else {
                    $find("<%= rnbAmount.ClientID %>").set_value(fcyValue);
                }
            }
        }

       // function AmountLCYOnBlur(sender, eventArgs) {          
       //     var value = sender.get_value();               
       //     $find("<%= lblAmtCreditForCust.ClientID %>").set_value(value);
       //     $find("<%= lblNewCustBal.ClientID %>").set_value(parseFloat(value) + parseFloat($find("<%= hidbal.ClientID %>").get_value()));   
       //     $find("<%= rnbAmount.ClientID %>").set_value(value);               
       // }

        function TermOnBlur(sender, eventArgs) {
            var term = sender.get_value().toUpperCase();
          
            if (term == "" ){             
                return;
            }           
            var strD = term.indexOf("D");
            var strM = term.indexOf("M");
            if (strD < 0 && strM < 0) return;
            var numD = Number(term.substr(0, strD));
            var numM = Number(term.substr(0, strM));
            var valueDate = $find("<%= rdpValueDate.ClientID %>");
            var maturityDate = $find("<%= rdpFinalMatDate.ClientID %>");
         
            var newDate = valueDate.get_selectedDate();
            if (Number(numD) > 0) {
                newDate.setDate(newDate.getDate() + Number(numD));
            } else if (Number(numM) > 0) {
                newDate.setMonth(newDate.getMonth() + Number(numM));
            }
            maturityDate.set_selectedDate(newDate);
            CalculatorAmmount();
          
         }

        function DatedIf(dateStart, dateEnd) {
            return (dateEnd - dateStart) / (24 * 60 * 60 * 1000);
        }

        function CalculatorAmmount() {          
            var maturityDate = $find("<%= rdpFinalMatDate.ClientID %>").get_selectedDate();
            var valueDate = $find("<%= rdpValueDate.ClientID %>").get_selectedDate();
            var interestRate = $find("<%= rtbInterestRate.ClientID %>").get_value();
            var amount = $find("<%= rnbAmount.ClientID %>").get_value();
            if (valueDate != null && valueDate != '' && amount != '' && maturityDate != null &&
                interestRate != null && maturityDate != "" && interestRate != "") {
                var interestRateDay = interestRate / 100 / 360;
                var interestAmmount = DatedIf(valueDate, maturityDate) * interestRateDay * amount
                if ($find("<%= rcbCurrencyAmount.ClientID %>").get_value() == "VND") {
                    $find("<%= txtAmountLCY.ClientID %>").set_value(interestAmmount);
                    $find("<%= txtAmountFCY.ClientID %>").set_value();
                    $('#<%= ctxtAmountLCY.ClientID %>').val(interestAmmount)
                    $('#<%= ctxtAmountFCY.ClientID %>').val()
                } else {
                    $find("<%= txtAmountFCY.ClientID %>").set_value(interestAmmount);
                    $find("<%= txtAmountLCY.ClientID %>").set_value();
                    $('#<%= ctxtAmountFCY.ClientID %>').val(interestAmmount)
                    $('#<%= ctxtAmountLCY.ClientID %>').val()
                }                
                $("#<%= lbNarrative.ClientID%>").val('ID: [' + $("#<%= LDid.ClientID%>").text() + ']-P: [' + amount + ']-R: [' + interestRate + ']-T: [' + $find("<%= rtbTerm.ClientID %>").get_value() + ']');
            }
        }

        function rcbPaymentCcy_OnClientSelectedIndexChanged(sender, eventArgs) {
            $find("<%= rcbPaymentCcy2.ClientID %>").set_value(sender.get_value()); 
            $("#<%= rcbPaymentCcy2.ClientID %>").val(sender.get_value());             
        }

        function Exch(sender, eventArgs) {
         
            var paymentCCY = $find("<%= rcbPaymentCcy2.ClientID %>").get_value(); 
            var currentcyUser = $("#<%= lblCurrency2.ClientID %>").val(); 
            if (currentcyUser == paymentCCY) {
                $find("<%= txtExchRate.ClientID %>").set_value();
             
                var value = $find("<%= txtAmountFCY.ClientID %>").get_value();
                if (paymentCCY == "VND") {
                    value = $find("<%= txtAmountLCY.ClientID %>").get_value();
                } else {
                    $find("<%= txtAmountLCY.ClientID %>").set_value();
                }
                $find("<%= lbNewCustBal.ClientID %>").set_value(-1* value); 
                $find("<%= lbAmtPaid.ClientID %>").set_value(value); 
                return;
            }

            if (paymentCCY == "VND") {
                var amountfcy = $find("<%= txtAmountFCY.ClientID %>").get_value();
                var exchRate = $find("<%= txtExchRate.ClientID %>").get_value();
                if (amountfcy != null && exchRate != null && amountfcy != '' && exchRate != '') {
                    var value = amountfcy * exchRate;
                    $find("<%= txtAmountLCY.ClientID %>").set_value(value);
                    $find("<%= lbNewCustBal.ClientID %>").set_value(-1 * value);
                    $find("<%= lbAmtPaid.ClientID %>").set_value(value); 
                }
            } else {
                var amountlcy = $find("<%= txtAmountLCY.ClientID %>").get_value();
                var exchRate = $find("<%= txtExchRate.ClientID %>").get_value();
                if (amountlcy != null && exchRate != null && amountlcy != '' && exchRate != '') {
                    var value = amountlcy / exchRate;
                    $find("<%= txtAmountFCY.ClientID %>").set_value(value);
                    $find("<%= lbNewCustBal.ClientID %>").set_value(-1 * value);
                    $find("<%= lbAmtPaid.ClientID %>").set_value(value); 
                }
            }
            
        }
        
        function txtAmountLCY_OnclientValueChanged() {
            var Currency = $find("<%= lblCurrency2.ClientID%>").get_value();
            var amount = 0;
            switch (Currency) {
                case "VND":
                    amount = $find("<%= txtAmountLCY.ClientID%>").get_value();
                    break;

                default:
                    amount = $find("<%= txtAmountFCY.ClientID%>").get_value();
                    break;
            }

            $find("<%= lbNewCustBal.ClientID%>").set_value(-1 * amount);
            $find("<%= lbAmtPaid.ClientID%>").set_value(amount);
        }


        function alert(m) {
            m = m.replace(/-/g, '<br>-') + "<br> &nbsp;<br> &nbsp;";
            radalert(m, "Alert");
        }
    </script>
</telerik:RadCodeBlock>
<div style="visibility: hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" />

    <telerik:RadTextBox ID="hidbal" runat="server" ></telerik:RadTextBox>
    <asp:TextBox ID="tbcustomId" runat="server" Width="200" ></asp:TextBox>
    <asp:TextBox ID="ctxtAmountLCY" runat="server" Width="200" ></asp:TextBox>
    <asp:TextBox ID="ctxtAmountFCY" runat="server" Width="200" ></asp:TextBox>
</div>