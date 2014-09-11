<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreclosureInput.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Close.PreclosureInput" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs({ selected: 0, cookieMilleseconds: 0 });       
    });
</script>

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
                <asp:TextBox ID="tbDepositCode" runat="server" Width="400" />
                <i>
                    <asp:Label ID="lblDepositCode" runat="server" Enabled ="false" /></i></td>
        </tr>
    </table>
</div>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#Preclose">Pre close</a></li>
        <li><a href="#Precloseview">Pre close view</a></li>
        <li><a href="#WithdrawalTermSavings">Withdrawal - Term Savings</a></li>
    </ul>
    <div id="Preclose" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="Commit" />

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Customer Infomation</div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Customer 
                    </td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblCustomer"></asp:Label></td>
                </tr>

                <tr>
                    <td class="MyLable">Category 
                    </td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblCategory"></asp:Label></td>
                </tr>

                <tr>
                    <td class="MyLable">Currency 
                    </td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblCurrency"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Product Code 
                    </td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblProductCode"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Principal 
                    </td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblPrincipal"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Value Date 
                    </td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblValueDate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Maturity Date 
                    </td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblMaturityDate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Interested Rate
                    </td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblInterestedRate"></asp:Label></td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Pre Close</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Preclose (Y/N) 
                        <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None" ID="RequiredFieldValidator2" ControlToValidate="rcbPrecloseYN"
                            ValidationGroup="Commit" InitialValue="" ErrorMessage="Preclose (Y/N) is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:radcombobox
                            id="rcbPrecloseYN" runat="server" markfirstmatch="True" width="50" height="50px" allowcustomtext="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="1" Text="Y" />
                            </Items>
                        </telerik:radcombobox>
                        <i>
                            <asp:Label runat="server" ID="Label1"></asp:Label></i>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Working Account 
                        <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None" ID="RequiredFieldValidator1" ControlToValidate="rcbWorkingAcc"
                            ValidationGroup="Commit" InitialValue="" ErrorMessage="Working Account is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:radcombobox
                            id="rcbWorkingAcc" runat="server"
                            markfirstmatch="True" height="150px"
                            allowcustomtext="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />                           
                        </telerik:radcombobox>                       
                        <i>
                            <asp:Label runat="server" ID="lblWokingAccName"></asp:Label></i>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Waive Charges</td>
                    <td class="MyContent">
                        <telerik:radcombobox
                            id="rcbWaiveCharges" runat="server"
                            markfirstmatch="True" width="50" height="150px"
                            allowcustomtext="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="1" Text="YES" />
                            </Items>
                        </telerik:radcombobox>
                    </td>
                </tr>
            </table>
        </fieldset>

        <fieldset style="visibility:hidden">
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Audit Information</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Overrides</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblOverrides"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Record Status</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblRecordStatus"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Curr Number</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCurrNumber" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Inputter</td>
                    <td class="MyContent">
                        <asp:Label ID="lblInputter" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Authoriser</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAuthoriser" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Company Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCoCode" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Dept Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDeptCode" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Date.Time</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDateTime" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Date.Time</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDateTime2" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Auditor Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAuditorCode" runat="server" /></td>
                </tr>
            </table>
        </fieldset>
    </div>

    <div id="Precloseview" class="dnnClear">
      
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Customer 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblCustomer"></asp:Label></td>
                </tr>

                <tr>
                    <td class="MyLable">Category 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblCategory"></asp:Label></td>
                </tr>

                <tr>
                    <td class="MyLable">Currency 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblCurrency"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Product Code 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblProductCode"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Open Date 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblOpenDate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Maturity Date 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblMaturityDate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Principal 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblPrincipal"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Original Principal 
                    </td>
                    
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblOrgPrincipal"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Interest Rate
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblInterestedRate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Value Date                        
                    </td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="VWdtpValueDate" Width="130" runat="server" MinDate="1/1/1900" MaxDate="1/12/9999" 
                            DateInput-DateFormat="dd/MM/yyyy" >                            
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                         <telerik:radgrid runat="server" autogeneratecolumns="False"
                             id="grdSavingAccInterestList" allowpaging="false" onneeddatasource="grdSavingAccInterestList_OnNeedDataSource">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Start Date" DataField="StartDate" 
                                       DataFormatString="{0:dd/MM/yyyy}" />
                                    <telerik:GridBoundColumn HeaderText="End Date " DataField="EndDate" 
                                       DataFormatString="{0:dd/MM/yyyy}"/>
                                    <telerik:GridBoundColumn HeaderText="Interest Rate" DataField="InterestRate" 
                                        DataType="System.Decimal" DataFormatString="{0:N}"/>
                                    <telerik:GridBoundColumn HeaderText="Interest Amount" DataField="InterestAmt"
                                        DataType="System.Decimal" DataFormatString="{0:N}"/>
                                </Columns>
                            </MasterTableView>
                        </telerik:radgrid>
                    </td>
                </tr>
             <%--   <tr>
                    <td class="MyLable">Start Date
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblStartDate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">End Date
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblEndDate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Interest Rate
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblInterestRate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Interest Amount
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblInterestAmount"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Start Date 2
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblStartDate2"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">End Date 2
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblEndDate2"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Interest Rate 2
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblInterestRate2"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Interest Amount 2
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblInterestAmount2"></asp:Label></td>
                </tr>--%>
                <tr>
                    <td class="MyLable">Total in Amt
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblTotalInAmt"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Next Teller Tran
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="VWlblNextTeller"></asp:Label></td>
                </tr>
            </table>
       
        
    </div>

    <div id="WithdrawalTermSavings" class="dnnClear">
      
        <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Payment No</td>
                <td class="MyContent">
                    <asp:Label runat="server" ID ="WDlblTTPaymentNo"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Currency</td>
                <td class="MyContent">
                    <asp:Label runat="server" ID ="WDlblCurrency"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Customer</td>
                <td class="MyContent">
                    <asp:Label runat="server" ID ="WDlblCustomer"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Dr Account
                </td>
                <td class="MyContent">
                        <telerik:RadComboBox
                            ID="WDcmbDrAccount" runat="server" MarkFirstMatch="True" Width="200" Height="150px" AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="1" Text="07.000168836.8" />
                                <telerik:RadComboBoxItem Value="1" Text="07.000164412.7" />
                            </Items>
                        </telerik:RadComboBox>
                        <i><asp:Label runat="server" ID ="WDlblDrAccName"></asp:Label></i>
                    </td>
            </tr>
            <tr>
                <td class="MyLable">Amount Lcy
                </td>
                <td class="MyContent">
                        <telerik:RadNumericTextBox runat="server" ID="WDnumAcmountLcy" MinValue="0"  width="200"    
                            MaxValue="999999999999" >
                            <NumberFormat GroupSeparator="," DecimalDigits="2" AllowRounding="true"   KeepNotRoundedValue="false"  />                            
                        </telerik:RadNumericTextBox>
                    </td>
            </tr>
            <tr>
                <td class="MyLable">Amount Fcy
                </td>
                <td class="MyContent">
                        <telerik:RadNumericTextBox runat="server" ID="WDnumAcmountFcy" MinValue="0" width="200"
                            MaxValue="999999999999" >
                            <NumberFormat GroupSeparator="," DecimalDigits="2" AllowRounding="true"   KeepNotRoundedValue="false"  />
                        </telerik:RadNumericTextBox>
                    </td>
            </tr>
            <tr>
                <td class="MyLable">Narative
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="WDtbNarative" runat="server" Text="" Width ="250" >
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">New Customer Balance</td>
                <td class="MyContent">
                    <asp:Label runat="server" ID ="WDlblNewCustBal"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Amount Paid to Customer</td>
                <td class="MyContent">
                    &nbsp;<asp:Label runat="server" ID ="WDlblAmtPaidToCust"></asp:Label>
                </td>
            </tr>
        </table>
        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Deal Rate</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox runat="server" ID="WDtbDealRate" MinValue="0"   Width= "200"
                        MaxValue="999999999999" >
                        <NumberFormat GroupSeparator="," DecimalDigits="2" AllowRounding="true"   KeepNotRoundedValue="false"  />
                    </telerik:RadNumericTextBox>
                </td>
            </tr>
        </table>
        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">For Teller
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="WDtbTeller" runat="server" Text="" Width="50">
                    </telerik:RadTextBox>
                    <i><asp:Label runat="server" ID ="WDlblTellerName"></asp:Label></i>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Credit CCY
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox 
                        ID="WDcmbCreditCCY" runat="server" 
                        MarkFirstMatch="True" Width="200" Height="150px"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <%--<tr>
                <td class="MyLable">Smartbank Branch
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox
                            ID="WDcmbSmartBank" runat="server" 
                            MarkFirstMatch="True" Width="200" Height="150px"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="0" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                </td>
            </tr>--%>
            <tr>
                <td class="MyLable">Credit Account
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox
                            ID="WDcmbCreditAcc" runat="server" 
                            MarkFirstMatch="True" Width="200" Height="150px"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="1" Text="EUR-30001-2054-3210" />
                                <telerik:RadComboBoxItem Value="2" Text="GBP-40001-2054-4242" />
                                <telerik:RadComboBoxItem Value="3" Text="JPY-50001-2054-6428" />
                                <telerik:RadComboBoxItem Value="4" Text="USD-20001-2054-1221" />
                                <telerik:RadComboBoxItem Value="5" Text="VND-10001-0695-1221" />
                            </Items>
                        </telerik:RadComboBox>
                        <i><asp:Label runat="server" ID ="WDlblCreditAccName"></asp:Label></i>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Print Ln No of
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="WDtbPrintLnNoOf" runat="server" Text="" Width="200">
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>
        <hr />
    </div>

</div>

<telerik:radajaxmanager id="RadAjaxManager1" runat="server"
    defaultloadingpanelid="AjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbCustomerID">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblCustomer" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:radajaxmanager>
<telerik:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript">       
        
        //    function OnClientButtonClicking(sender, args) {
        //        var button = args.get_item();
        //        <%--var PreclosureType = $('#<%= hdfPreclosureType.ClientID %>').val();--%>
        //        if (button.get_commandName() == "commit") {
        //            if (!clickCalledAfterRadconfirm) {

        //                if (PreclosureType == "1") {
        //                    output = " Charges, Red in Amount 0. Total Amount Deal 50,000,000 VND";
        //                } else {
        //                    output = " Charges, Red in Amount 0. Total Amount Deal 250,000,000 VND";
        //                }
        //                var message = output.toString();
        //                args.set_cancel(true);
        //                lastClickedItem = args.get_item();
        //                radconfirm(message, confirmCallbackFunction2, 350, 150, null, 'Override');
        //            }
        //        }
        //    }
        //    var lastClickedItem = null;
        //    var clickCalledAfterRadprompt = false;
        //    var clickCalledAfterRadconfirm = false;
        //    function confirmCallbackFunction2(isSuccess) {
        //        if (isSuccess) {
        //            clickCalledAfterRadconfirm = true;
        //            lastClickedItem.click();
        //            lastClickedItem = null;
        //        }
        //    }

    </script>
</telerik:radcodeblock>
<div style="visibility: hidden;">
     <telerik:radcombobox appenddatabounditems="True"
            id="rcbCustomerID" runat="server"
            markfirstmatch="True" height="150px"
            autopostback="true"          
            allowcustomtext="false">
        <ExpandAnimation Type="None" />
        <CollapseAnimation Type="None" />
        <Items>
            <telerik:RadComboBoxItem Value="" Text="" />
        </Items>
    </telerik:radcombobox>
    <telerik:radcombobox appenddatabounditems="True"
        id="radTermAZ" runat="server"
        markfirstmatch="True" height="150px"                            
        allowcustomtext="false">
                <ExpandAnimation Type="None" />
                <CollapseAnimation Type="None" />
                <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />                                          
                </Items>
                             
    </telerik:radcombobox>
      <telerik:radnumerictextbox runat="server" id="tbInterestRateAZ" minvalue="0" maxvalue="999999999999">
        <NumberFormat GroupSeparator="," DecimalDigits="5" AllowRounding="true"   KeepNotRoundedValue="false"  />
    </telerik:radnumerictextbox>
    <telerik:raddatepicker id="dtpValueDateAZ" width="130" runat="server" mindate="1/1/1900" maxdate="1/12/9999" dateinput-dateformat="dd/MM/yyyy">
        
    </telerik:raddatepicker>
    <telerik:raddatepicker id="dtpMaturityDateAZ" width="130" runat="server" mindate="1/1/1900" maxdate="1/12/9999" dateinput-dateformat="dd/MM/yyyy">
        
    </telerik:raddatepicker>
     <telerik:radnumerictextbox runat="server" id="radNumPrincipalAZ" minvalue="0" maxvalue="999999999999">
        <NumberFormat GroupSeparator="," DecimalDigits="0" AllowRounding="true"   KeepNotRoundedValue="false"  />
    </telerik:radnumerictextbox>
</div>

