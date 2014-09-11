<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DiscountedClose.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Close.DiscountedClose" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%--<%@ Register Src="../../../../Controls/VVComboBox.ascx" TagPrefix="uc1" TagName="VVComboBox" %>--%>
<%@ Register src="../../../../Controls/VVComboBox.ascx" tagname="VVComboBox" tagprefix="uc2" %>
<%@ Register Src="../../../../Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>

<asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs({ selected: 0, cookieMilleseconds: 0 });
    });
</script>
<style>
 #tabs-demo input {
        width: 315px;
    }

    #ChristopherColumbus .rcbInput {
        width: 298px;
    }

    #TradSav .rcbInput {
        width: 293px;
    }
</style>

<telerik:radtoolbar runat="server" id="RadToolBar1"
    enableroundedcorners="true" enableshadows="true" width="100%" onbuttonclick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit" Enabled ="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btPreview" CommandName="Preview" Enabled ="true">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize" Enabled ="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse" Enabled ="false">
        </telerik:RadToolBarButton>      
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print" Enabled ="false">
        </telerik:RadToolBarButton>
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/cursor_drag_hand_icon.png"
            ToolTip="Hold" Value="btHold" CommandName="hold" Enabled ="false">
        </telerik:RadToolBarButton>
    </Items>
</telerik:radtoolbar>
   
         
<div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 200px; padding: 5px 0 5px 20px;">                
                <asp:TextBox ID="tbDepositCode" runat="server" AutoPostBack="true" Width="200" />
            </td>
        </tr>
    </table>
                   
</div>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a id="TrandSavDeposited" href="#ChristopherColumbus">Trad Sav - Deposited</a></li>
        <li><a id="DiscInterestPayment" href="#TradSav">Disc Interest Payment</a></li>
       <%-- <li><a href="#FullView">Full View</a></li>--%>
    </ul>

<div id="ChristopherColumbus" class="dnnClear">
    <div style="padding-left:10px">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer No</td>
                <td class="MyContent" >
                    <asp:Label ID="lblCustomerName" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable"  >Currency / Amount </td>
                <td class="MyContent">
                    <asp:Label ID="lblCurrencyAmount"  runat="server"></asp:Label> /             
                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                </td>
            </tr>       
            <tr>
                <td class="MyLable">Value Date </td>
                <td class="MyContent"   >
                    <asp:Label ID="lbValueDate"  runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">New Mat Date </td>
                <td class="MyContent">
                        <telerik:RadDatePicker dateinput-dateformat="dd/MM/yyyy" runat="server"
                            AutoPostBack="true" OnSelectedDateChanged="ServerComAmount" 
                            ID="rdpNewMatDate" ></telerik:RadDatePicker>
                    <%-- <telerik:RadDatePicker runat="server" ID="rdpNewMatDate">
                        <DateInput ID="DateInput2" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" runat="server"></DateInput>
                    </telerik:RadDatePicker>--%>
                </td>
            </tr>        
            <tr>
                <td class="MyLable">Int Pymt Method</td>
                <td class="MyContent"   >
                    <asp:Label ID="lbIntPymtMethod"  runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Interest Basis</td>
                <td class="MyContent"   >
                    <asp:Label ID="lbInterestBasis"  runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Interest Rate</td>
                <td class="MyContent"   >
                    <asp:Label ID="lbInterestRate"  runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Total Int Amt</td>
                <td class="MyContent"   >
                    <asp:Label ID="lbTotalIntAmt"  runat="server" />
                </td>
            </tr>
                  
        </table>
    </div>
    <fieldset>
        <legend style="text-transform:uppercase; font-weight:bold">Eligible Interest</legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Eligible Interest </td>
                    <td class="MyContent" >
                        <telerik:RadNumericTextBox
                           
                             runat="server" NumberFormat-DecimalDigits="4" ID="rtbEligibleInterest" ></telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Int Rate V Date</td>
                    <td class="MyContent" >
                        <telerik:RadDatePicker  AutoPostBack="true" OnSelectedDateChanged="ServerComAmountNewInterestRate" 
                            dateinput-dateformat="dd/MM/yyyy" runat="server" ID="RdpIntRateVDate"></telerik:RadDatePicker>
                       <%-- <telerik:RadDatePicker runat="server" ID="txtIntRateVDate">
                            <DateInput ID="DateInput1" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" runat="server"></DateInput>
                        </telerik:RadDatePicker>--%>
                    </td>
                </tr>
        </table>

    </fieldset>

    <fieldset>
        <legend style="text-transform:uppercase; font-weight:bold">Account Information</legend>
            <table style="width:100%; padding:0px;">
                <tr>
                    <td class="MyLable">Debit Account</td>
                    <td class="MyContent" >
                        <asp:Label  runat="server"  ID="lblDebitAccount" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Cr Acc for Principal</td>
                    <td class="MyContent" >
                        <asp:Label runat="server"  ID="lblCrAccforPrincipal" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Cr Acc for Interest</td>
                    <td class="MyContent" >
                        <asp:Label runat="server"  ID="txtCrAccforInterest" ></asp:Label>
                    </td>
                </tr>
            </table>
    </fieldset>
        
</div>
<div id="TradSav" class="dnnClear">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
             <table width="100%" cellpadding="0" cellspacing="0">
                 <tr>
                     <td class="MyLable">Deposit No</td>
                     <td class="MyContent">
                        <asp:Label ID="DItbDepositNo" runat="server" >
                        </asp:Label>
                     </td>
                </tr>                 
                <tr>
                    <td class="MyLable">Customer</td>
                    <td class="MyContent">
                        <asp:Label ID="DIlbCustomer" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Currency</td>
                    <td class="MyContent">
                        <asp:Label ID="DIlblCurrency" runat="server" />                  
                    </td>
                </tr>        
                <tr>
                    <td class="MyLable">Dr Account</td>
                    <td class="MyContent">
                        <asp:Label ID="DILblDrAccount" runat="server" />                     
                    </td>
                </tr>       
                <tr>
                    <td class="MyLable">Amount LCY</td>
                    <td class="MyContent">
                     <telerik:RadNumericTextBox ID="DItxtAmountLCY" Enabled ="false" NumberFormat-DecimalDigits="0" runat="server">
                     </telerik:RadNumericTextBox>
                    </td>
                </tr>       
                <tr>
                    <td class="MyLable">Amount FCY</td>
                    <td class="MyContent">
                     <telerik:RadNumericTextBox ID="DItxtAmountFCY" Enabled ="false" runat="server">
                     </telerik:RadNumericTextBox>
                    </td>
                </tr>       
                <tr>
                    <td class="MyLable">Narrative</td>
                    <td class="MyContent"> 
                        <asp:Label ID="DIlbNarrative"  runat="server" />
                    </td>
                </tr>       
                <tr>
                    <td class="MyLable">Deal Rate</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="DItxtExchRate" runat="server"></telerik:RadNumericTextBox>
                    </td>
                </tr>      
                <tr>
                    <td class="MyLable">Currency Paid</td>
                    <td class="MyContent">
                       <telerik:RadComboBox
                            ID="DIrcbCurrencyPaid" runat="server" 
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                        </telerik:RadComboBox>
                    </td>
                </tr>       
                <tr>
                    <td class="MyLable">For Teller</td>
                    <td class="MyContent">
                     <telerik:RadTextBox ID="txtForTeller"  runat="server"></telerik:RadTextBox>
                    </td>
                </tr>      
                <tr>
                    <td class="MyLable">Credit Account</td>
                    <td class="MyContent">
                         <telerik:RadComboBox
                            ID="DIrcbCreditAccount" runat="server" 
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />                            
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Narrative</td>
                    <td class="MyContent">
                         <telerik:RadTextBox 
                            ID="DItxtNarrative" runat="server" />
                    </td>
                </tr>      
                <tr>
                    <td class="MyLable">Waive Charge:</td>
                    <td class="MyContent">
                        <telerik:RadComboBox AppendDataBoundItems="True"                                     
                            ID="DIrcbWaiveCharge" runat="server"
                            OnSelectedIndexChanged="rcbWaiveCharge_OnSelectedIndexChanged" 
                            MarkFirstMatch="True" 
                            AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="YES" Text="YES" />
                                <telerik:RadComboBoxItem Value="NO" Text="NO" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>      
                <tr>
                    <td class="MyLable">New Cust Bal</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="DIlbNewCustBal" ReadOnly="true" BorderWidth="0" runat="server"></telerik:RadNumericTextBox>
                    </td>
                </tr>      
                <tr>
                    <td class="MyLable">Amt paid</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="DIlbAmtPaid" ReadOnly="true" BorderWidth="0" runat="server"></telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Print Ln No of</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="PrintLnNoof" ReadOnly="false" value="1"  runat="server"></telerik:RadNumericTextBox>
                    </td>
                </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
       
    </div>
   
    <%--<fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Audit Information</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Overrides</td>
                    <td class="MyContent"></td>
                </tr>
                <tr>
                    <td class="MyLable">Record Status</td>
                    <td class="MyContent"></td>
                </tr>
                <tr>
                    <td class="MyLable">Curr No</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCurrNo" Text="2" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Inputter</td>
                    <td class="MyContent">
                        <asp:Label ID="lblInputter" Text="1006_ID5135_I_INAU" runat="server" /></td>
                </tr>

                <tr>
                    <td class="MyLable">Date.Time</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDateTime" text="03/08/2014" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Date.Time</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDateTime2" text="03/08/2014" runat="server" /></td>
                </tr>
               <tr>
                    <td class="MyLable">Authoriser</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAuthoriser" Text="1031_ID4258" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Company Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCoCode" Text="VN-001-1311" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Dept Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDeptCode" Text="1" runat="server" /></td>
                </tr>
            </table>
        </fieldset>--%>
</div>
<telerik:radajaxmanager id="RadAjaxManager2" runat="server" defaultloadingpanelid="AjaxLoadingPanel1">
    <AjaxSettings>
       <%-- <telerik:AjaxSetting AjaxControlID="rdpNewMatDate">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="DItxtAmountLCY" />
                <telerik:AjaxUpdatedControl ControlID="DItxtAmountFCY" />
             
            </UpdatedControls>
        </telerik:AjaxSetting>       --%>
       
    </AjaxSettings>
</telerik:radajaxmanager>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

<script>
     
</script>
    
</telerik:RadCodeBlock>

<div style="visibility: hidden;">
    <telerik:RadDatePicker runat="server" ID="startDate" />
    <telerik:RadDatePicker runat="server" ID="endDate" />
    <telerik:RadNumericTextBox ID="amount" runat="server" />
    <telerik:RadNumericTextBox ID="amountPri" runat="server" />
    <telerik:RadNumericTextBox ID="interestRate" runat="server" />
    <asp:Label ID="accountCurrency" runat="server" />
    <asp:TextBox ID="tbcustomId" runat="server" Width="200" ></asp:TextBox>
    <asp:TextBox ID="rtbTerm" runat="server" Width="200" ></asp:TextBox>
    <asp:TextBox ID="rdpFinalMatDate" runat="server" Width="200" ></asp:TextBox>
    
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" />    
    
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>          
            <telerik:RadTextBox ID="hf_numberCommit"  runat="server" ></telerik:RadTextBox>
            <telerik:RadTextBox ID="hdfIDLD" runat="server" ></telerik:RadTextBox>
         </ContentTemplate>
    </asp:UpdatePanel>
</div>