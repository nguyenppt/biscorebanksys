<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewNormalLoan.ascx.cs" Inherits="BankProject.Views.TellerApplication.NewNormalLoan" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ register Assembly="DotNetNuke.web" Namespace="DotNetNuke.Web.UI.WebControls" TagPrefix="dnn"%>
<%@ Register src="~/controls/LabelControl.ascx" TagPrefix="dnn" TagName="Label"%>
<%--<%@ Register src="../../Controls/NewLoanControls.ascx"  TagPrefix="UC" TagName="VVNewLoanControl"  %>--%>
<telerik:RadCodeBlock runat="server">
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    })
</script>
    </telerik:RadCodeBlock>
<telerik:RadToolBar runat="server" ID="RadToolBar1"  EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit" 
            ToolTip="Commit Data" Value="btnCommit" CommandName="commit">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="CommitFull"  Visible="false"
            ToolTip="Commit Data" Value="btnCommit2" CommandName="commit2">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btnPreview" CommandName="Preview">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btnAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btnReverse" CommandName="reverse">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btnSearch" CommandName="search">
        </telerik:RadToolBarButton>
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btnPrint" CommandName="print">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>

<div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:150px; padding: 5px 0 5px 20px;" >
                <asp:TextBox width="150" ID="tbNewNormalLoan" runat="server" ValidationGroup="Group1" />
               
                </td>
        </tr>
    </table>
</div>

<div  class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#Main">Main Info</a></li>
        <li><a href="#Other">Other Info</a></li>
        <telerik:RadCodeBlock runat="server">
      <%
           if (Request.Params["codeid"] == null && hfCommit2.Value.Equals("0"))
           { 
          %>
        <li><a id="linkFull" style="display:none;" href="#Full">Full View</a></li>
        <%}
          else{ %>
        <li><a id="linkFull" style="display:;" href="#Full">Full View</a></li>
        <%} %>
        </telerik:RadCodeBlock>
    </ul>
     
    <div id="Main" class="dnnClear">
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />
        <fieldset>
            <legend style="text-transform:uppercase; font-weight:bold">Contract Details</legend>
             <table width="100%" cellpadding="0" cellspacing="0">
                 <tr>
                     <td class="MyLable">Main Category:<span class="Required">(*)</span>
                     <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator1"
                     ControlToValidate="rcbMainCategory" ValidationGroup="Commit" InitialValue="" ErrorMessage="Main Category ID is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> </td>
                     <td class="MyContent"> 
                         <telerik:RadComboBox ID="rcbMainCategory" runat="server" width="330" AllowCustomText="false" AutoPostBack="true" OnSelectedIndexChanged="rcbMainCategory_SelectedIndexChanged"
                                         MarkFirstMatch="true">
                                         <ExpandAnimation Type="None" />
                                         <CollapseAnimation Type="None" />
                                         
                                     </telerik:RadComboBox> 

                     </td>
                     <td class="MyLable"></td>
                     <td class="MyContent"></td>
                 </tr>
                 <tr>
                 <td class="MyLable">Sub Category:<span class="Required">(*)</span>
                     <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator2"
                     ControlToValidate="rcbSubCategory" ValidationGroup="Commit" InitialValue="" ErrorMessage="Sub Category ID is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> 
                 </td>
                 <td class="MyContent">
                     <telerik:RadComboBox ID="rcbSubCategory" runat="server" AllowCustomText="false" MarkFirstMatch="true" width="330">
                     <ExpandAnimation Type="None" />
                     <CollapseAnimation Type="None" />
                                       
                                     </telerik:RadComboBox> 
                 </td>
                 <td class="MyLable"></td>
                 <td class="MyContent"></td>
             </tr>
             <tr>
                 <td class="MyLable">Purpose Code:<span class="Required">(*)</span>
                      <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator3"
                     ControlToValidate="rcbPurposeCode" ValidationGroup="Commit" InitialValue="" ErrorMessage="Purpose Code is required"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                 </td>
                 <td class="MyContent">
                     <telerik:RadComboBox ID="rcbPurposeCode" runat="server" AllowCustomText="false" MarkFirstMatch="true" width="330">
                     <ExpandAnimation Type="None" />
                     <CollapseAnimation Type="None" />
                        
                         </telerik:RadComboBox>
                 </td>
                 
             </tr>
             <tr>
                 <td class="MyLable">Customer ID:<span class="Required">(*)</span>
                       <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator4"
                     ControlToValidate="rcbCustomerID" ValidationGroup="Commit" InitialValue="" ErrorMessage="Customer ID is required"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                 </td>
                 <td class="MyContent">
                     
                                 <telerik:RadComboBox ID="rcbCustomerID" OnSelectedIndexChanged="rcbCustomerID_SelectedIndexChanged" runat="server" width="330" AllowCustomText="false" MarkFirstMatch="true">
                                     <ExpandAnimation Type="None" />
                                     <CollapseAnimation Type="None" />
                                     <Items>
                                         <telerik:RadComboBoxItem Value="" Text="" />
                                     </Items>
                                 </telerik:RadComboBox>
                 </td>
             </tr>
                 <tr>
                 <td class="MyLable">Loan Group:</td>
                 <td class="MyContent">
                     <telerik:RadComboBox ID="rcbLoadGroup" runat="server" width="330" AllowCustomText="false" MarkFirstMatch="true">
                     <ExpandAnimation Type="None" />
                     <CollapseAnimation Type="None" />
                         <Items>
                             <telerik:RadComboBoxItem Value="" Text=""  />
                             
                         </Items>
                         </telerik:RadComboBox>
                 </td>
                 
             </tr>
        <%--         <table width="100%" cellpadding="0" cellspacing="0">--%>
                     <tr>
                         <td class="MyLable">Currency:</td>
                         <td class="MyContent">
                             <telerik:RadComboBox ID="rcbCurrency" AutoPostBack="True" width="330"  OnSelectedIndexChanged="rcbCurrency_SelectedIndexChanged"  runat="server" AllowCustomText="false" MarkFirstMatch="true" >
                                 <ExpandAnimation Type="None" />
                                 <CollapseAnimation Type="None" />
                                 <Items>
                                     <telerik:RadComboBoxItem Value="" Text="" />
                                     <telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                                     <telerik:RadComboBoxItem Value="GBP" Text="GBP" />
                                     <telerik:RadComboBoxItem Value="JPY" Text="JPY" />
                                     <telerik:RadComboBoxItem Value="vnd" Text="VND" />
                                     <telerik:RadComboBoxItem Value="USD" Text="USD" />
                                  
                                 </Items>
                             </telerik:RadComboBox>
                         </td>
                        
                         <td class="MyLable">Bus Day Def:</td>
                         <td class="MyContent">
                             <telerik:RadTextBox ID="tbBusDayDef" runat="server"  ValidationGroup="Group1"  Text="VN" />
                             <i>VIET NAM</i>&nbsp;&nbsp;
                             <a style="display:none" class="add">
                                 <img src="Icons/Sigma/Add_16X16_Standard.png" />
                             </a>
                         </td>
                     </tr>
<%--                 </table>

                 <table width="100%" cellpadding="0" cellspacing="0">--%>
                     <tr>
                         <td class="MyLable">Loan Amount:<span class="Required">(*)</span>
                             <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator5"
                                 ControlToValidate="tbLoanAmount" ValidationGroup="Commit" InitialValue="" ErrorMessage="Loan Amount is required"
                                 ForeColor="Red"></asp:RequiredFieldValidator>
                         </td>
                         <td class="MyContent">
                             <telerik:RadTextBox ID="tbLoanAmount" runat="server"  ValidationGroup="Group1" >
                                 <ClientEvents OnBlur="SetNumber" OnFocus="ClearCommas" />
                             </telerik:RadTextBox>
                         </td>
                         <td class="MyLable">Approved Amt:</td>
                         <td class="MyContent">
                             <telerik:RadNumericTextBox ID="tbApprovedAmt" runat="server" ValidationGroup="Group1" ></telerik:RadNumericTextBox>
                         </td>
                     </tr>
                 <tr>
                 <td class="MyLable">Open Date:<span class="Required">(*)</span>
                     <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator6"
                     ControlToValidate="rdpOpenDate" ValidationGroup="Commit" InitialValue="" ErrorMessage="Open Date is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> 
                 </td>
                 <td class="MyContent">
                     <telerik:RadDatePicker ID="rdpOpenDate" runat="server" />
                 </td>
                 <td class="MyLable">Drawdown Date:</td>
                 <td class="MyContent">
                      <telerik:RadDatePicker ID="rdpDrawdown" runat="server" />
                 </td>
             </tr>
                 <tr>
                 <td class="MyLable">Value Date:</td>
                 <td class="MyContent">
                      <telerik:RadDatePicker ID="rdpValueDate" runat="server" />
                 </td>
                 <td class="MyLable">Maturity Date:<span class="Required">(*)</span></td>
                 <td class="MyContent">
                      <telerik:RadDatePicker ID="rdpMaturityDate" runat="server" />
                 </td>
             </tr>
                 <tr>
                 <td class="MyLable">Credit to Account:</td>
                 <td class="MyContent">
                     
                                 <telerik:RadComboBox Width="330"
                                     AppendDataBoundItems="True" AutoPostBack="False"
                                     ID="rcbCreditToAccount" runat="server"
                                     MarkFirstMatch="True" Height="150px"
                                     AllowCustomText="false">
                                     <ExpandAnimation Type="None" />
                                     <CollapseAnimation Type="None" />
                                 </telerik:RadComboBox>
                 </td>
                 <td class="MyLable"></td>
                 <td class="MyContent"></td>
             </tr>
             <tr>
                 <td class="MyLable">Commitment ID: </td>
                 <td class="MyContent">
                      <telerik:RadComboBox ID="rcbCommitmentID" runat="server" width="330" AllowCustomText="false"
                                         MarkFirstMatch="true"  AppendDataBoundItems="true">
                                         <ExpandAnimation Type="None" />
                                         <CollapseAnimation Type="None" />
                                         <Items>
                                             <telerik:RadComboBoxItem Value="" Text="" />
                                             <telerik:RadComboBoxItem Value="LD/14187/00052" Text="LD/14187/00052" />
                                         </Items>
                                     </telerik:RadComboBox>  
                 </td>
                 
             </tr>
             <tr>
                 <td class="MyLable">Limit Reference:<span class="Required">(*)</span>
                      <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator8"
                     ControlToValidate="rcbLimitReference" ValidationGroup="Commit" InitialValue="" ErrorMessage="Limit Reference:is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> </td>
                 <td class="MyContent">
                      <telerik:RadComboBox ID="rcbLimitReference" runat="server" width="330" AllowCustomText="false"
                                         MarkFirstMatch="true"  AppendDataBoundItems="true">
                                         <ExpandAnimation Type="None" />
                                         <CollapseAnimation Type="None" />
                                         <Items>
                                             <telerik:RadComboBoxItem Value="" Text="" />
                                             <telerik:RadComboBoxItem Value="1234.0010000.01" Text="1234.0010000.01" />
                                         </Items>
                                     </telerik:RadComboBox> 
                 </td>               
             </tr>    
             </table>

        </fieldset>
        <fieldset>
            <legend style="text-transform:uppercase; font-weight:bold">INTEREST Details</legend>
             <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                 <td class="MyLable">Rate Type:</td>
                 <td class="MyContent">
                     <telerik:RadComboBox ID="rcbRateType" runat="server" AllowCustomText="false"   MarkFirstMatch="true"> 
                         <CollapseAnimation Type="None" />
                         <CollapseAnimation Type="None" />
                         <Items>
                             <telerik:RadComboBoxItem Value="" Text="" />
                             <telerik:RadComboBoxItem Value="1" Text="1 - Fixed" />
                             <telerik:RadComboBoxItem Value="2" Text="2 - Periodic Automatic" />
                         </Items>
                     </telerik:RadComboBox>
                 </td>
                 <td class="MyLable"></td>
                 <td class="MyContent"></td>
             </tr>
                 <tr>
                 <td class="MyLable">Interest Basis:</td>
                 <td class="MyContent"><i>366/360</i></td>
                 <td class="MyLable">Annuity Rep Met:</td>
                 <td class="MyContent">
                     <telerik:RadComboBox ID="rcbAnnRepMet" runat="server" AllowCustomText="false" MarkFirstMatch="true" ValidationGroup="Group1">
                         <CollapseAnimation Type="None" />
                         <ExpandAnimation Type="None" />
                         <Items>
                             <telerik:RadComboBoxItem Value="" Text="" />
                         </Items>
                     </telerik:RadComboBox>
                 </td>
             </tr>
                 <table width="100%" cellpadding="0" cellspacing="0">
                     <tr>
                         <td class="MyLable">Int Pay Method:</td>
                         <td class="MyContent">
                             <asp:Label ID="lbl" Text="INTEREST BEARING" Width="150" runat="server" /></td>

                         <td class="MyLable">Interest Rate:<span class="Required">(*)</span>
                             <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator7"
                                 ControlToValidate="tbInterestRate" ValidationGroup="Commit" InitialValue="" ErrorMessage="Interest Rate is required"
                                 ForeColor="Red" /></td>

                         <td class="MyContent">
                             <telerik:RadNumericTextBox ID="tbInterestRate" runat="server" ValidationGroup="Group1" />
                         </td>
                     </tr>
                 
                 <tr>
                 <td class="MyLable">Interest Key:</td>
                 <td class="MyContent">
                     <telerik:RadComboBox ID="rcbInterestKey" runat="server" AllowCustomText="false" MarkFirstMatch="true" ValidationGroup="Group1">
                         <CollapseAnimation Type="None" />
                         <ExpandAnimation Type="None" />
                         <Items>
                             <telerik:RadComboBoxItem Value="" Text="" />
                         </Items>
                     </telerik:RadComboBox>
                 </td>
                 <td class="MyLable">Int Spread:</td>
                 <td class="MyContent">
                     <telerik:RadNumericTextBox ID="tbInSpread" runat="server" ValidationGroup="Group1"></telerik:RadNumericTextBox>
                 </td>
             </tr>
                 <tr>
                     <td class="MyLable">Auto Sch (Y/N):</td>
                     <td class="MyContent">
                         <telerik:RadComboBox ID="rcbAutoSch" runat="server" AllowCustomText="false" MarkFirstMatch="true" ValidationGroup="Group1">
                             <CollapseAnimation Type="None" />
                             <ExpandAnimation Type="None" />
                             <Items>
                                 <telerik:RadComboBoxItem Value="" Text="" />
                                 <telerik:RadComboBoxItem Value="Y" Text="Yes" />
                                 <telerik:RadComboBoxItem Value="N" Text="No" />
                             </Items>
                         </telerik:RadComboBox>
                     </td>
                     <td class="MyLable">Define Sch (Y/N):</td>
                     <td class="MyContent">
                         <telerik:RadComboBox ID="rcbDefineSch" runat="server" AllowCustomText="false" MarkFirstMatch="true" ValidationGroup="Group1">
                             <CollapseAnimation Type="None" />
                             <ExpandAnimation Type="None" />
                             <Items>
                                 <telerik:RadComboBoxItem Value="" Text="" />
                                 <telerik:RadComboBoxItem Value="Y" Text="Yes" />
                                 <telerik:RadComboBoxItem Value="N" Text="No" />
                             </Items>
                         </telerik:RadComboBox>
                     </td>
             </tr>
             <tr>
                 <td class="MyLable">Repay Sch Type:<span class="Required">(*)</span>
                      <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator9"
                     ControlToValidate="rcbRepaySchType" ValidationGroup="Commit" InitialValue="" ErrorMessage="Repay Sch Type is required"
                    ForeColor="Red"/>
                 </td>
                 <td class="MyContent">
                     <telerik:RadComboBox ID="rcbRepaySchType" runat="server" AllowCustomText="false" MarkFirstMatch="true" ValidationGroup="Group1">
                         <CollapseAnimation Type="None" />
                         <ExpandAnimation Type="None" />
                         <Items>
                             <telerik:RadComboBoxItem Value="" Text="" />
                             <telerik:RadComboBoxItem Value="I" Text="I - INSTALLMENT" />
                             <telerik:RadComboBoxItem Value="N" Text="N - NON-REDEMPTION" />
                         </Items>
                     </telerik:RadComboBox>
                 </td>
                 <td class="MyLable">Loan Status:</td>
                 <td class="MyContent"></td>
             </tr>
                  <tr>
                 <td class="MyLable">Total Interest Amt:</td>
                 <td class="MyContent"></td>
                 <td class="MyLable">PD Status:</td>
                 <td class="MyContent"></td>
             </tr>
                 </table>
            </fieldset>

    </div>
    <div id="Other" class="dnnClear">


        <table style="display:none" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Charge Code
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbChargeCode" runat="server" AllowCustomText="false" MarkFirstMatch="true" ValidationGroup="Group1">
                        <CollapseAnimation Type="None" />
                        <ExpandAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                    <a class="add">
                        <img src="Icons/Sigma/Add_16X16_Standard.png" />
                    </a>
                </td>
            </tr>
        </table>
        <table style="display:none" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Charge Amount</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="rcbChagreAmount" runat="server" ValidationGroup="Group1"></telerik:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Charge Date</td>
                <td class="MyContent">
                     <telerik:RadDatePicker ID="rdpChargeDate" runat="server" />
                </td>
            </tr>
        </table>


        <fieldset>
            <legend style="text-transform: uppercase; font-weight: bold">Repayment Details</legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Prin Rep Account</td>
                    <td class="MyContent">
                         <telerik:RadComboBox Width="350px"
                            AppendDataBoundItems="True" AutoPostBack="False"
                            ID="rcbPrinRepAccount" runat="server"
                            MarkFirstMatch="True" Height="150px"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                        </telerik:RadComboBox>

                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Int Rep Account</td>
                    <td class="MyContent">
                         <telerik:RadComboBox Width="350px"
                            AppendDataBoundItems="True" AutoPostBack="False"
                            ID="rcbIntRepAccount" runat="server"
                            MarkFirstMatch="True" Height="150px"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Chrg Rep Account</td>
                    <td class="MyContent">
                        <telerik:RadComboBox Width="350px"
                            AppendDataBoundItems="True" AutoPostBack="False"
                            ID="rcbChargRepAccount" runat="server"
                            MarkFirstMatch="True" Height="150px"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                        </telerik:RadComboBox>
                        
                    </td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend style="text-transform: uppercase; font-weight: bold">Credit Scoring Details</legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Expected Loss</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="RadNumericTextBox3" runat="server" ValidationGroup="Group1" Width="150"></telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Loss Given Def.</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="RadNumericTextBox4" runat="server" ValidationGroup="Group1" Width="150"></telerik:RadNumericTextBox>
                    </td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend style="text-transform: uppercase; font-weight: bold">Other Details</legend>
            <table width="100%" cellpadding="0" cellspacing="0">

                <tr>
                    <td class="MyLable">Customer Remarks <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator11"
                            ControlToValidate="tbCustomerRemarks" ValidationGroup="Commit" InitialValue="" ErrorMessage="Customer Remarks is required"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbCustomerRemarks" runat="server" ValidationGroup="Group1" Width="350"></telerik:RadTextBox>
                        <a class="add">
                            <img src="Icons/Sigma/Add_16X16_Standard.png" />
                        </a>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Account Officer<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator10"
                            ControlToValidate="cmbAccountOfficer" ValidationGroup="Commit" InitialValue="" ErrorMessage="Account Officer is required"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbAccountOfficer"
                            MarkFirstMatch="True"
                            AllowCustomText="false"
                            Width="250" runat="server" ValidationGroup="Group1">
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="312" Text="312 - Le Thi Hoa" />
                                <telerik:RadComboBoxItem Value="313" Text="313 - Tran Thi Lan" />
                                <telerik:RadComboBoxItem Value="314" Text="314 - Nguyen Thi Thu" />
                                <telerik:RadComboBoxItem Value="315" Text="315 - Nguyen Khoa Minh Tri" />
                                <telerik:RadComboBoxItem Value="316" Text="316 - Le Tran Hong Phuc" />
                                <telerik:RadComboBoxItem Value="317" Text="317 - Phan Minh Hoang" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Secured (Y/N)<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator12"
                            ControlToValidate="rcbSecured" ValidationGroup="Commit" InitialValue="" ErrorMessage="Secured (Y/N) is required"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="rcbSecured" runat="server" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" AllowCustomText="false" MarkFirstMatch="true" ValidationGroup="Group1">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="Y" Text="Yes" />
                                <telerik:RadComboBoxItem Value="N" Text="No" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table id="collateralID" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Collateral ID</td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="rcbCollateralID" runat="server" AllowCustomText="false" MarkFirstMatch="true" ValidationGroup="Group1">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="" Text="10034.1" />
                                <telerik:RadComboBoxItem Value="" Text="10075.1" />
                            </Items>
                        </telerik:RadComboBox>
                        <a class="add">
                            <img src="Icons/Sigma/Add_16X16_Standard.png" />
                        </a>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr id="amountAllocID">
                    <td class="MyLable">Amount Alloc</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="rtbAmountAlloc" runat="server" ValidationGroup="Group1">
                            <ClientEvents OnBlur="SetNumber" OnFocus="ClearCommas" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Country Risk</td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="rcbCountryRisk" runat="server" AllowCustomText="false" MarkFirstMatch="true" ValidationGroup="Group1">
                            
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Legacy.Ref</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="rtbLegacy" runat="server" ValidationGroup="Group1"></telerik:RadNumericTextBox>
                    </td>
                </tr>
            </table>
        </fieldset>

    </div>
    <%-- <div id="Audit" class="dnnClear">
         audit tab
     </div>--%>

    <div id="Full" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="CommitFull" />
        <p>&nbsp;</p>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Forward/Backward Key <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator15"
                        ControlToValidate="tbForwardBackWard" ValidationGroup="CommitFull" InitialValue="" ErrorMessage="Forward/Backward Key is required"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td style="width:150px;" class="MyContent">
                    <telerik:RadNumericTextBox Value="3" ID="tbForwardBackWard" runat="server" ValidationGroup="Group1"  Width="150"/>
                </td>
                <td style="text-align:left" class="MyLable"><i>FWD IN MONTH</i></td>
                <td class="MyLable">Base Date Key <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator14"
                        ControlToValidate="tbBaseDate" ValidationGroup="CommitFull" InitialValue="" ErrorMessage="Base Date Key is required"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td style="width:50px;" class="MyContent">
                    <telerik:RadNumericTextBox Value="1" ID="tbBaseDate" runat="server" ValidationGroup="Group1" Width="50" />
                </td>
                <td class="MyLable"><i>Base Date</i> </td>
            </tr>
        </table>
        <hr />
       <%--<UC:VVNewLoanControl runat="server" id="vvnbRate" VVTLabel="" Icon="false" />--%>
         <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                <ContentTemplate>
        <asp:ListView ID="ListView1" runat="server" DataKeyNames="ID" DataSourceID="SqlDataSource1xx" InsertItemPosition="LastItem">
    <AlternatingItemTemplate>
        <tr style="text-align:center">
           
            <td>
                <asp:Label ID="TypeLabel" runat="server" Text='<%# Eval("Type") %>' />
            </td>
            <td>
                <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />
            </td>
            <td>
                <%--<asp:Label ID="AmountActionLabel" runat="server" Text='<%# Eval("AmountAction") %>' />--%>
                <telerik:RadNumericTextBox ID="AmountActionLabel" Runat="server" ReadOnly="true" BorderWidth="0" Value='<%# Bind("AmountAction") %>'>
                    <EnabledStyle HorizontalAlign="Right" />
                    </telerik:RadNumericTextBox>
            </td>
            <td>
                <asp:Label ID="RateLabel" runat="server" Text='<%# Eval("Rate") %>' />
            </td>
            <td>
                <asp:Label ID="ChrgLabel" runat="server" Text='<%# Eval("Chrg") %>' />
            </td>
            <td>
                <asp:Label ID="NoLabel" runat="server" Text='<%# Eval("No") %>' />
            </td>
            <td>
                <asp:Label ID="FreqLabel" runat="server" Text='<%# Eval("Freq") %>' />
            </td>
             <td>
                <asp:ImageButton ImageUrl="~/Icons/Sigma/Delete_16X16_Standard.png" ID="Button3" runat="server" CommandName="Delete" Text="Delete" />&nbsp;&nbsp;&nbsp
                <asp:ImageButton ImageUrl="~/Icons/Sigma/Edit_16X16_Standard.png" ID="Button4" runat="server" CommandName="Edit" Text="Edit" />
            </td>
        </tr>
    </AlternatingItemTemplate>
    <EditItemTemplate>
         <tr style="text-align:center">
           
            <td>
                 <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("Type") %>'>
                        <asp:ListItem Selected="True"></asp:ListItem>
                        <asp:ListItem>I</asp:ListItem>
                        <asp:ListItem>P</asp:ListItem>
                        <asp:ListItem>AC</asp:ListItem>
                        <asp:ListItem>B</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td>
                
                 <telerik:RadDatePicker ID="RadDatePicker1" Runat="server" SelectedDate='<%# Bind("Date") %>'>
                </telerik:RadDatePicker>
            </td>
            <td>
                 <telerik:RadNumericTextBox ID="RadNumericTextBox1" Runat="server" Value='<%# Bind("AmountAction") %>'>
                    </telerik:RadNumericTextBox>
            </td>
            <td>
                 <telerik:RadNumericTextBox ID="TextBox1" Runat="server" Value='<%# Bind("Rate") %>'>
                    </telerik:RadNumericTextBox>
            </td>
            <td>
                  <asp:DropDownList ID="TextBox2" runat="server" SelectedValue='<%# Bind("Chrg") %>'>
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                
            </td>
            <td>
                 <telerik:RadNumericTextBox ID="TextBox3" Runat="server" Value='<%# Bind("No") %>'>
                    </telerik:RadNumericTextBox>
            </td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Freq") %>' />
            </td>
              <td>
                <asp:ImageButton ImageUrl="~/Icons/Sigma/Save_16X16_Standard.png" ID="Button1" runat="server" CommandName="Update" Text="Update" /> &nbsp;&nbsp;&nbsp;
                <asp:ImageButton ImageUrl="~/Icons/Sigma/Cancel_16X16_Standard.png" ID="Button2" runat="server" CommandName="Cancel" Text="Clear" />
            </td>
        </tr>
    </EditItemTemplate>
    <EmptyDataTemplate>
        <table runat="server" style="">
            <tr>
                <td>No data was returned.</td>
            </tr>
        </table>
    </EmptyDataTemplate>
    <InsertItemTemplate>
        <tr style="text-align:center">
            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="myVGInsert" />
            <td>
                 <asp:DropDownList ID="TypeTextBox" runat="server" SelectedValue='<%# Bind("Type") %>'>
                        <asp:ListItem Selected="True"></asp:ListItem>
                        <asp:ListItem>I</asp:ListItem>
                        <asp:ListItem>P</asp:ListItem>
                        <asp:ListItem>AC</asp:ListItem>
                        <asp:ListItem>B</asp:ListItem>
                    </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
            ErrorMessage="Please choose Type"
            ControlToValidate="TypeTextBox"  ForeColor="Red" Display="None"
            ValidationGroup="myVGInsert"
        />
            </td>
            <td>
                
                 <telerik:RadDatePicker ID="DateTextBox" Runat="server" SelectedDate='<%# Bind("Date") %>'>
                </telerik:RadDatePicker>
            </td>
            <td>
                 <telerik:RadNumericTextBox ID="AmountActionTextBox" Runat="server" Value='<%# Bind("AmountAction") %>'>
                    </telerik:RadNumericTextBox>
            </td>
            <td>
                  <telerik:RadNumericTextBox ID="RateTextBox" Runat="server" Value='<%# Bind("Rate") %>'>
                    </telerik:RadNumericTextBox>
                 
            </td>
            <td>
                <asp:DropDownList ID="ChrgTextBox" runat="server" SelectedValue='<%# Bind("Chrg") %>'>
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                
            </td>
            <td>
                 <telerik:RadNumericTextBox ID="NoTextBox" Runat="server" Value='<%# Bind("No") %>'>
                    </telerik:RadNumericTextBox>
            </td>
            <td>
                <asp:TextBox ID="FreqTextBox" runat="server" Text='<%# Bind("Freq") %>' />
            </td>
             <td>
                <asp:ImageButton  ImageUrl="~/Icons/Sigma/Save_16X16_Standard.png" ID="InsertButton" ValidationGroup="myVGInsert" runat="server" CommandName="Insert" Text="Insert" />&nbsp;&nbsp;&nbsp
                <asp:ImageButton ImageUrl="~/Icons/Sigma/Refresh_16x16_Standard.png" ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
            </td>
        </tr>
    </InsertItemTemplate>
    <ItemTemplate>
        <tr style=" text-align:center">
           
            <td>
                <asp:Label ID="TypeLabel" runat="server" Text='<%# Eval("Type") %>' />
            </td>
            <td>
                <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />
            </td>
            <td >
                <%--<asp:Label ID="AmountActionLabel"  runat="server" Text='<%# Eval("AmountAction") %>' />--%>
                 <telerik:RadNumericTextBox ID="AmountActionLabel" Runat="server" ReadOnly="true" BorderWidth="0" Value='<%# Bind("AmountAction") %>'>
                    <EnabledStyle HorizontalAlign="Right" />
                    </telerik:RadNumericTextBox>
            </td>
            <td>
                <asp:Label ID="RateLabel" runat="server" Text='<%# Eval("Rate") %>' />
            </td>
            <td>
                <asp:Label ID="ChrgLabel" runat="server" Text='<%# Eval("Chrg") %>' />
            </td>
            <td>
                <asp:Label ID="NoLabel" runat="server" Text='<%# Eval("No") %>' />
            </td>
            <td>
                <asp:Label ID="FreqLabel" runat="server" Text='<%# Eval("Freq") %>' />
            </td>
             <td>
                <asp:ImageButton ImageUrl="~/Icons/Sigma/Delete_16X16_Standard.png" ID="Button5" runat="server" CommandName="Delete" Text="Delete" />&nbsp;&nbsp;&nbsp
                <asp:ImageButton ImageUrl="~/Icons/Sigma/Edit_16X16_Standard.png" ID="Button6" runat="server" CommandName="Edit" Text="Edit" />
            </td>
        </tr>
    </ItemTemplate>
    <LayoutTemplate>
        <table runat="server">
            <tr runat="server">
                <td runat="server">
                    <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                        <tr runat="server" style="">
                            
                            <th runat="server">Type <span class="Required">(*)</span></th>
                            <th runat="server">Date</th>
                            <th runat="server">Amount - Diary Action</th>
                            <th runat="server">Rate</th>
                            <th runat="server">Chrg</th>
                            <th runat="server">No</th>
                            <th runat="server">Frequency</th>
                            <th runat="server"></th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </td>
            </tr>
            <tr runat="server">
                <td runat="server" style=""></td>
            </tr>
        </table>
    </LayoutTemplate>
    <SelectedItemTemplate>
        <tr style="text-align:center">
           
            <td>
                <asp:Label ID="TypeLabel" runat="server" Text='<%# Eval("Type") %>' />
            </td>
            <td>
                <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />
            </td>
            <td>
                <%--<asp:Label ID="AmountActionLabel" runat="server" Text='<%# Eval("AmountAction") %>' />--%>
                <telerik:RadNumericTextBox ID="AmountActionLabel" Runat="server" ReadOnly="true" BorderWidth="0" Value='<%# Bind("AmountAction") %>'>
                    <EnabledStyle HorizontalAlign="Right" />
                    </telerik:RadNumericTextBox>
            </td>
            <td>
                <asp:Label ID="RateLabel" runat="server" Text='<%# Eval("Rate") %>' />
            </td>
            <td>
                <asp:Label ID="ChrgLabel" runat="server" Text='<%# Eval("Chrg") %>' />
            </td>
            <td>
                <asp:Label ID="NoLabel" runat="server" Text='<%# Eval("No") %>' />
            </td>
            <td>
                <asp:Label ID="FreqLabel" runat="server" Text='<%# Eval("Freq") %>' />
            </td>
             <td>
                <asp:ImageButton ImageUrl="~/Icons/Sigma/Delete_16X16_Standard.png" ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                <asp:ImageButton ImageUrl="~/Icons/Sigma/Edit_16X16_Standard.png" ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
            </td>
        </tr>
    </SelectedItemTemplate>
</asp:ListView>
                    </ContentTemplate>
             </asp:UpdatePanel>
    </div>
    
    <asp:HiddenField ID="hfLoanAmount" runat="server" />
    <asp:HiddenField ID="hfCommitNumber" Value="0" runat="server" />
    <asp:HiddenField ID="hfCommit2" Value="0" runat="server" />
</div>
<div class="aspnet">

    <asp:SqlDataSource ID="SqlDataSource1xx" runat="server" ConnectionString="<%$ ConnectionStrings:VietVictoryCoreBanking %>" DeleteCommand="DELETE FROM [BNewLoanControl] WHERE [ID] = @ID" InsertCommand="INSERT INTO [BNewLoanControl] ([Type], [Date], [AmountAction], [Rate], [Chrg], [No], [Freq],[Code]) VALUES (@Type, @Date, @AmountAction, @Rate, @Chrg, @No, @Freq, @Code)" SelectCommand="SELECT * FROM [BNewLoanControl] WHERE [Code] = @Code" UpdateCommand="UPDATE [BNewLoanControl] SET [Type] = @Type, [Date] = @Date, [AmountAction] = @AmountAction, [Rate] = @Rate, [Chrg] = @Chrg, [No] = @No, [Freq] = @Freq WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Type" Type="String" />
            <asp:Parameter Name="Date" Type="DateTime" />
            <asp:Parameter Name="AmountAction" Type="Double" />
            <asp:Parameter Name="Rate" Type="Double" />
            <asp:Parameter Name="Chrg" Type="String" />
            <asp:Parameter Name="No" Type="Int32" />
            <asp:Parameter Name="Freq" Type="String" />
            <asp:ControlParameter ControlID="tbNewNormalLoan" Type="String" DefaultValue='<%=tbNewNormalLoan.Text%>' Name="Code" PropertyName="Text" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="tbNewNormalLoan" DbType="String" DefaultValue='<%=tbNewNormalLoan.Text%>' Name="Code" PropertyName="Text" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="Type" Type="String" />
            <asp:Parameter Name="Date" Type="DateTime" />
            <asp:Parameter Name="AmountAction" Type="Double" />
            <asp:Parameter Name="Rate" Type="Double" />
            <asp:Parameter Name="Chrg" Type="String" />
            <asp:Parameter Name="No" Type="Int32" />
            <asp:Parameter Name="Freq" Type="String" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    </div>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbMainCategory">
            <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="rcbSubCategory" />
            </UpdatedControls>
        </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="rcbCurrency">
            <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="rcbCreditToAccount" />
            <telerik:AjaxUpdatedControl ControlID="rcbPrinRepAccount" />
            <telerik:AjaxUpdatedControl ControlID="rcbIntRepAccount" />
            <telerik:AjaxUpdatedControl ControlID="rcbChargRepAccount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ListView1">
            <UpdatedControls>
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock runat="server">
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
    function GenerateZero(k) {
        n = 1;
        for (var i = 0; i < k.length; i++) {
            n *= 10;
        }
        return n;
    }
    function addCommas(str) {
        var parts = (str + "").split("."),
            main = parts[0],
            len = main.length,
            output = "",
            i = len - 1;

        while (i >= 0) {
            output = main.charAt(i) + output;
            if ((len - i) % 3 === 0 && i > 0) {
                output = "," + output;
            }
            --i;
        }
        // put decimal part back
        //if (parts.length > 1) {
        //    console.log(parts[1]);
        //    output += "." + parts[1];
        //}
        
        return output + ".00";
    } 
    function ClearCommas(sender, args) {
        var m = sender.get_value().replace(".00", "").replace(/,/g, '');
        console.log(m);
        sender.set_value(m);
    }
    function SetNumber(sender, args) {
        //sender.set_value(sender.get_value().toUpperCase());
        var number;
        var m = sender.get_value().substring(sender.get_value().length - 1);
        if (isNaN(m)) {
            var val = sender.get_value().substring(0, sender.get_value().length - 1).split(".");
            switch (m.toUpperCase()) {

                case "T":
                    var n1 = val[0] * 1000;
                    var n2 = 0;
                    if (val[1] != null)
                        n2 = (val[1] / GenerateZero(val[1])) * 1000;
                    number = n1 + n2;
                    sender.set_value(addCommas(number));
                    break;
                case "M":
                    var n1 = val[0] * 1000000;
                    var n2 = 0;
                    if (val[1] != null)
                        n2 = (val[1] / GenerateZero(val[1])) * 1000000;
                    number = n1 + n2;
                    sender.set_value(addCommas(number));
                    break;
                case "B":
                    var n1 = val[0] * 1000000000;
                    var n2 = 0;
                    if (val[1] != null)
                        n2 = (val[1] / GenerateZero(val[1])) * 1000000000;
                    number = n1 + n2;
                    sender.set_value(addCommas(number));
                    break;
                default:
                    alert("Character is not valid. Please use T, M and B character");
                    $find('<%=tbLoanAmount.ClientID %>').focus();
                    return false;
                    break;
            }
        } else {
            console.log("is number" + m);
            number = sender.get_value();
            sender.set_value(addCommas(number));

        }
        //var num = sender.get_value();
        document.getElementById("<%= hfLoanAmount.ClientID %>").value = number;
        $find("<%= tbApprovedAmt.ClientID %>").set_value(sender.get_value());
    }
    function clickFullTab() {
        document.getElementById("linkFull").style.display = "";
        $('#tabs-demo').tabs({ selected: 2 });
    }
    function clickMainTab() {
        $('#tabs-demo').tabs({ selected: 0 });
    }
    function OnClientSelectedIndexChanged(sender, eventArgs) {
        var item = eventArgs.get_item();
        if (item.get_value() == 'N') {
            document.getElementById("collateralID").style.display = "none"
            document.getElementById("amountAllocID").style.display = "none"
        } else {
            document.getElementById("collateralID").style.display = ""
            document.getElementById("amountAllocID").style.display = ""
        }
    }
  </script>

</telerik:RadCodeBlock>
