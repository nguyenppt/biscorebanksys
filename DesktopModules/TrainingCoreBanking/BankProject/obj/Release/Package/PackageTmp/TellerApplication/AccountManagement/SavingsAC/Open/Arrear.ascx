<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Arrear.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Open.Arrear" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs({ selected: 0, cookieMilleseconds: 0 });
        $('#tabs-demo').bind('tabsselect', function (event, ui) {
            if (ui.index == 2) {
                LoadExtraInformationFor();
            }
        });

    });
</script>
<style>
    .cssProductLine:hover {
        background-color:#CCC;
    }
    #tabs-demo input {
        width: 315px;
    }

    #ChristopherColumbus .rcbInput {
        width: 298px;
    }

    #ChristopherInputColumbus .rcbInput, #ChristopherDepositColumbus .rcbInput {
        width: 293px;
    }
</style>
<telerik:RadWindowManager runat="server" id="RadWindowManager1"></telerik:RadWindowManager>
<telerik:radtoolbar runat="server" id="RadToolBar1" enableroundedcorners="true" enableshadows="true" width="100%" onbuttonclick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit" Enabled ="false"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png" Enabled ="false"
            ToolTip="Preview" Value="btPreview" CommandName="preview">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png" Enabled ="false"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png" Enabled ="false"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse">
        </telerik:RadToolBarButton>
       <%-- <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png" Enabled ="false"
            ToolTip="Search" Value="btSearch" CommandName="search">
        </telerik:RadToolBarButton>--%>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png" Enabled ="false"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print">
        </telerik:RadToolBarButton>
    </Items>
</telerik:radtoolbar>
<div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 200px; padding: 5px 0 5px 20px;">
                <asp:TextBox ID="tbDepositCode" runat="server" Width="200px" />
                <i>
                    <asp:Label ID="lblDepositCode" runat="server" />
                </i>
            </td>
        </tr>
    </table>
</div>
<%--<div>
    <h3>&nbsp;&nbsp;Open Traditional Term Savings Arrears/Gold/Gold_backed</h3>&nbsp;
</div>--%>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Details</a></li>
        <li><a href="#ChristopherInputColumbus">All in one Account</a></li>
        <li><a href="#ChristopherDepositColumbus">New Deposit - Term Savings</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="Commit" />
        <div style="padding-left: 10px; padding-top: 5px">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Customer ID 
                    <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator1"
                            ControlToValidate="rcbCustomerID"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Customer ID is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="350">
                                    <telerik:radcombobox appenddatabounditems="True"
                                        id="rcbCustomerID" runat="server"
                                        markfirstmatch="True" height="150px"
                                        autopostback="true"
                                        onselectedindexchanged="rcbCustomerID_SelectIndexChange"
                                        allowcustomtext="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="" />
                                    </Items>
                                </telerik:radcombobox>

                                    <%--<asp:Label ID="lblCustomerID" runat="server" />--%></td>
                                <td><i>
                                    <asp:Label ID="lblCustomer" style="display:none" runat="server" /></i></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Category
                    <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator3"
                            ControlToValidate="rcbCategoryCode"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Category is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:radcombobox appenddatabounditems="True"
                            id="rcbCategoryCode" runat="server"
                            markfirstmatch="True" height="150px"                            
                           
                            autopostback="true"
                            onselectedindexchanged="rcbCustomerID_SelectIndexChange"
                            allowcustomtext="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="" />
                                    </Items>
                    </telerik:radcombobox>
                        <%--<i>Traditional Savings INT in Arrears</i>--%>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Account Title 
                        <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator8"
                            ControlToValidate="tbAccountName"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Account title is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <asp:TextBox ID="tbAccountName" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Short Title</td>
                    <td class="MyContent">
                        <asp:TextBox ID="tbShortName" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Currency <span class="Required">(*)</span><asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator2"
                        ControlToValidate="rcbCurrentcy"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Currentcy is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:radcombobox appenddatabounditems="true"
                            id="rcbCurrentcy" runat="server"
                            markfirstmatch="True" height="150px"
                            allowcustomtext="false"
                            autopostback="true"
                            onclientselectedindexchanged="rcbCurrentcy_ClientSelectedIndexChanged"
                            onselectedindexchanged="rcbCurrentcy_SelectIndexChange">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None"/>
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:radcombobox>
                    </td>
                </tr>
                <%--<tr>
                <td class="MyLable">Account Mnemonic</td>
                <td class="MyContent">
                    <asp:TextBox ID="tbAccountMnemonic" runat="server" MaxLength="9" /></td>
            </tr>--%>
                <tr>
                    <td class="MyLable">Product Line 
                    </td>
                    <td class="MyContent">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="350">
                                    <telerik:radcombobox appenddatabounditems="True" 
                                        id="rcbProductLine" runat="server"
                                        markfirstmatch="True" height="150px"
                                        allowcustomtext="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />                                   
                                </telerik:radcombobox>
                                    <asp:Label ID="lblProductID" runat="server" />

                                </td>
                                <td>
                                    <i>
                                        <asp:Label ID="lblProduct" runat="server" /></i>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Join Account Infomation</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Joint A/c Holder</td>
                    <td class="MyContent">
                        <telerik:radcombobox 
                            id="rcbJointHolderID" runat="server" appenddatabounditems="True"
                            markfirstmatch="True"
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
                    <td class="MyLable">Relationship</td>
                    <td class="MyContent">
                        <telerik:radcombobox 
                            id="rcbRelationCode" runat="server" appenddatabounditems="True"
                            markfirstmatch="True"  height="200px"
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
                    <td class="MyLable">Notes</td>
                    <td class="MyContent">
                        <asp:TextBox ID="tbNotes" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Account Officer</td>
                    <td class="MyContent">
                        <telerik:radcombobox appenddatabounditems="True"
                            id="rcbAccOfficer" runat="server"
                            markfirstmatch="True"
                            allowcustomtext="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                             <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:radcombobox>
                    </td>
                    <%--<td class="MyContent">
                        <asp:Label ID="lblAccOfficer" runat="server" /></td>--%>
                </tr>
            </table>
        </fieldset>
        <fieldset style="visibility: hidden">
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
                    <td class="MyLable">Curr Number</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCurrNo" runat="server" /></td>
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
                    <td class="MyLable">Co.Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCoCode" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Dept.Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDeptCode" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Auditor Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAuditorCode" runat="server" /></td>
                </tr>
            </table>
        </fieldset>
    </div>

    <div id="ChristopherInputColumbus" class="dnnClear">

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Customer Infomation</div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Customer 
                    </td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="CustomerIdAZ"></asp:Label></td>
                </tr>

                <tr>
                    <td class="MyLable">Category 
                    </td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblCategoryAZ"></asp:Label></td>
                </tr>

                <tr>
                    <td class="MyLable">Currency 
                    </td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblCurrencyAZ"></asp:Label></td>
                </tr>

            </table>
        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Product Infomation</div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Product 
                        <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator4"
                            ControlToValidate="rcbProductAZ"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Product is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:radcombobox appenddatabounditems="True"
                            id="rcbProductAZ" runat="server"
                            markfirstmatch="True" height="150px"
                            autopostback="true"
                            onselectedindexchanged="rcbProductAZ_SelectIndexChange"
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
                    <td class="MyLable">Principal 
                        <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator5"
                            ControlToValidate="radNumPrincipalAZ"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Principal is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <%--<asp:TextBox ID="tbPrincipal" runat="server" Width="150" />--%>
                        <telerik:radnumerictextbox runat="server" id="radNumPrincipalAZ" minvalue="0" maxvalue="999999999999">
                            <NumberFormat GroupSeparator="," DecimalDigits="0" AllowRounding="true"   KeepNotRoundedValue="false"  />
                        </telerik:radnumerictextbox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Value Date</td>
                    <td class="MyContent">
                        <%--<customControl:CustomDataTimePicker ID="dtpValueDate1" Width="130" runat="server" MinDate="1900/1/1">
                            <ClientEvents OnDateSelected="dtpValueDate_DateSelected" />
                        </customControl:CustomDataTimePicker>--%>
                        <telerik:raddatepicker id="dtpValueDateAZ" width="130" runat="server" mindate="1/1/1900" maxdate="1/12/9999" dateinput-dateformat="dd/MM/yyyy">
                            <ClientEvents OnDateSelected="dtpValueDateAZ_DateSelected" />
                        </telerik:raddatepicker>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Term
                        <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator9"
                            ControlToValidate="radTermAZ"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Term is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                         <telerik:radcombobox appenddatabounditems="True"
                            id="radTermAZ" runat="server"
                            markfirstmatch="True" height="150px"    
                             onclientselectedindexchanged="radTermAZ_OnBlur"               
                            allowcustomtext="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="" />                                          
                                    </Items>
                             
                        </telerik:radcombobox>

                       <%-- <telerik:radtextbox id="radTermAZ" runat="server" text="">
                            <ClientEvents OnBlur="radTermAZ_OnBlur" />
                    </telerik:radtextbox>--%>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Maturity Date</td>
                    <td class="MyContent">
                        <%--<customControl:CustomDataTimePicker ID="dtpMaturityDate1" Width="130" runat="server" MinDate="1900/1/1">
                            <ClientEvents OnDateSelected="dtpValueDate_DateSelected" />
                        </customControl:CustomDataTimePicker>--%>
                        <telerik:raddatepicker id="dtpMaturityDateAZ" width="130" runat="server" mindate="1/1/1900" maxdate="1/12/9999" dateinput-dateformat="dd/MM/yyyy">
                            <ClientEvents OnDateSelected="dtpValueDateAZ_DateSelected" />
                        </telerik:raddatepicker>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Interest Rate</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox runat="server" id="tbInterestRateAZ" minvalue="0" maxvalue="999999999999">
                            <NumberFormat GroupSeparator="," DecimalDigits="5" AllowRounding="true"   KeepNotRoundedValue="false"  />
                        </telerik:radnumerictextbox>
                    </td>
                </tr>

            </table>
        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Payment Infomation</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Working Account 
                        <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator6"
                            ControlToValidate="rcbWorkingAccAZ"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Working Account is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:radcombobox appenddatabounditems="True"
                            id="rcbWorkingAccAZ" runat="server"
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
                    <td class="MyLable" aria-readonly="True"></td>
                    <td class="MyContent">
                         <i>
                            <asp:Label runat="server" ID="lblWokingAccNameAZ"></asp:Label>
                        </i>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" aria-readonly="True">Maturity Instr</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblMaturityInstrAZ"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Rollover PR only?</td>
                    <td class="MyContent">
                        <telerik:radcombobox
                            id="rcbRollOverPROnlyAZ" runat="server"
                            markfirstmatch="True" width="70" height="150px"
                            allowcustomtext="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="0" Text="" />
                                <telerik:RadComboBoxItem Value="1" Text="Y" />
                                <telerik:RadComboBoxItem Value="2" Text="N" />
                            </Items>
                        </telerik:radcombobox>
                    </td>
                </tr>
            </table>
        </fieldset>

        <fieldset style="visibility: hidden">
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Audit Information</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Overrides</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblOverridesAZ"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Record Status</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblRecordStatusAZ"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Curr Number</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCurrNumberAZ" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Inputter</td>
                    <td class="MyContent">
                        <asp:Label ID="lblInputterAZ" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Authoriser</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAuthoriserAZ" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Company Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCompanyCodeAZ" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Dept Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDeptCodeAZ" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Date.Time</td>
                    <td class="MyContent">
                        <asp:Label ID="Label6" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Date.Time</td>
                    <td class="MyContent">
                        <asp:Label ID="Label7" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Auditor Code</td>
                    <td class="MyContent">
                        <asp:Label ID="Label8" runat="server" /></td>
                </tr>
            </table>
        </fieldset>
    </div>

    <div id="ChristopherDepositColumbus" class="dnnClear">

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Acct No
                </td>
                <td class="MyContent">
                    <asp:Label runat="server" ID="TTlblAcctNo"></asp:Label>

                    <%--<telerik:radtextbox id="radAcctNo" runat="server" text="" width="300">
                        <ClientEvents OnKeyPress="radAcctNo_OnKeyPress" />
                    </telerik:radtextbox>--%>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Payment No
                </td>
                <td class="MyContent">
                    <telerik:radtextbox id="TTtbPaymentNo" runat="server" text="" width="300">
                    </telerik:radtextbox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Payment CCY
                </td>
                <td class="MyContent">
                    <telerik:radcombobox appenddatabounditems="true"
                        id="TTrcbPaymentCcy" runat="server" onclientselectedindexchanged="rcbPaymentCcy_OnClientSelectedIndexChanged"
                        markfirstmatch="True"  height="150px"
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
                <td class="MyLable">For Teller
                </td>
                <td class="MyContent">
                    <telerik:radtextbox id="TTtbTeller" runat="server" text="" width="50">
                    </telerik:radtextbox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Debit Account
                </td>
                <td class="MyContent">
                    <telerik:radcombobox appenddatabounditems="True"
                        id="TTrcbDebitAmmount" runat="server"
                        markfirstmatch="True"  height="150px"
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
                <td class="MyLable">Narative
                </td>
                <td class="MyContent">
                    <telerik:radtextbox id="TTtbNarative" runat="server" text="" width="300">
                    </telerik:radtextbox>
                </td>
            </tr>
        </table>
        <fieldset>
            <%--<legend>
                <div style="font-weight: bold; text-transform: uppercase;"></div>
            </legend>--%>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Account CCy</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblAccountCCy"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Customer ID</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="TTlblCustomerID"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Account No</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblAccountNo"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Account Lcy</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblAccountLcy"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Account Fcy</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblAccountFcy"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Narrative</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblAZDepodit" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Narrative</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblCurrency" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Narrative</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblAmmount" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Narrative</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblDate" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Deal Rate</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox runat="server" id="tbDealRate" minvalue="0" width="100"
                            maxvalue="999999999999">
                            <NumberFormat GroupSeparator="," DecimalDigits="2" AllowRounding="true"   KeepNotRoundedValue="false"  />
                        </telerik:radnumerictextbox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Account In LCY</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblAmountInLCY" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>

    </div>

</div>

<telerik:radajaxmanager id="RadAjaxManager1" runat="server" defaultloadingpanelid="AjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbCustomerID">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblCustomer" />
                <telerik:AjaxUpdatedControl ControlID="CustomerIdAZ" />
                <telerik:AjaxUpdatedControl ControlID="lblWokingAccNameAZ" />
                <telerik:AjaxUpdatedControl ControlID="rcbWorkingAccAZ" />
                <telerik:AjaxUpdatedControl ControlID="TTrcbDebitAmmount" />                
            </UpdatedControls>
        </telerik:AjaxSetting>       
       
        <telerik:AjaxSetting AjaxControlID="rcbProductAZ">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblMaturityInstrAZ" />                
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="rcbCurrentcy">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="TTrcbDebitAmmount" />                
                <telerik:AjaxUpdatedControl ControlID="lblCurrencyAZ" />  
                <telerik:AjaxUpdatedControl ControlID="CompareCurrencyVSPaymentCCY" />  
                <telerik:AjaxUpdatedControl ControlID="TTrcbPaymentCcy" />  
                 <telerik:AjaxUpdatedControl ControlID="rcbWorkingAccAZ" />      
                
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:radajaxmanager>

<telerik:radcodeblock id="RadCodeBlock1" runat="server">
   

    <script type="text/javascript">

        function rcbCategoryCode_SelectIndexChange(sender, eventArgs) {
            $("#<%= lblCategoryAZ.ClientID %>").text(eventArgs.get_item().get_text())
        }
        function radTermAZ_OnBlur(sender, eventArgs) {
            var term = $find('<%=radTermAZ.ClientID %>').get_value().toUpperCase();
            
             if (isNaN(term.substr(0, term.length - 1)) || isNaN(term) == false) {
                 radalert("Wrong Format", "Alert");
                 $find('<%=radTermAZ.ClientID %>').focus();
             } else {
                 var strD = term.indexOf("D");
                 var strM = term.indexOf("M");
                 var numD = Number(term.substr(0, strD));
                 var numM = Number(term.substr(0, strM));
                 var ValueDate = $find("<%= dtpValueDateAZ.ClientID %>");
                 var MaturityDate = $find("<%= dtpMaturityDateAZ.ClientID %>");
                 var date = ValueDate.get_selectedDate();
                 var newDate = new Date(Date.parse(date, "yyyy/MM/dd"));
                 if (Number(numD) > 0) {
                     newDate.setDate(newDate.getDate() + Number(numD));
                 } else if (Number(numM) > 0) {
                     newDate.setMonth(newDate.getMonth() + Number(numM));
                 }
                 MaturityDate.set_selectedDate(newDate);
             }

         }
        <%--
         function rcbProductAZ_OnClientSelectedIndexChanged(sender, eventArgs) {
             if ($find('<%=rcbProductAZ.ClientID %>').get_selectedItem().get_value() == "100")
                 $('#<%=lblMaturityInstrAZ.ClientID%>').text("AUTOMATIC ROLLOVER");
             else
                 $('#<%=lblMaturityInstrAZ.ClientID%>').text("PAYMENT TO NOMINATED ACCOUNT");
         }--%>

        function dtpValueDateAZ_DateSelected(sender, eventArgs) {

            var term = $find('<%=radTermAZ.ClientID %>').get_value().toUpperCase();
             if (term.length > 0 && (isNaN(term.substr(0, term.length - 1)) || isNaN(term) == false)) {
                 radalert("Wrong Format", "Alert");
                 //$find('<%=radTermAZ.ClientID %>').focus();
             } else {
                 var strD = term.indexOf("D");
                 var strM = term.indexOf("M");
                 var numD = Number(term.substr(0, strD));
                 var numM = Number(term.substr(0, strM));
                 var ValueDate = $find("<%= dtpValueDateAZ.ClientID %>");
                 var MaturityDate = $find("<%= dtpMaturityDateAZ.ClientID %>");                 
                 var newDate = ValueDate.get_selectedDate();
                 if (Number(numD) > 0) {
                     newDate.setDate(newDate.getDate() + Number(numD));
                 } else if (Number(numM) > 0) {
                     newDate.setMonth(newDate.getMonth() + Number(numM));
                 }
                 try{
                     MaturityDate.set_selectedDate(newDate);
                 }catch(err){}
             }
         }

         function LoadExtraInformationFor() {

            var Currency = $('#<%= rcbCurrentcy.ClientID %>').val();
            var CurrencyText = $('#<%= rcbCurrentcy.ClientID %>').text();
            var CustomerID = $('#<%= tbDepositCode.ClientID %>').val();
            var Account = $('#<%= radNumPrincipalAZ.ClientID %>').val(); //need format
            var AccountText = "Amount = " + $('#<%= radNumPrincipalAZ.ClientID %>').val();
            var DateText = "Date = " + new Date(Date.parse($('#<%= dtpValueDateAZ.ClientID %>').val())).format("yyyyMMdd");

            if ($('#<%= TTrcbPaymentCcy.ClientID %>').val() == "") {
                $find('<%=TTrcbPaymentCcy.ClientID %>').set_text(Currency);
            }
            if ($('#<%= TTtbTeller.ClientID %>').val() == "") {
                $find("<%= TTtbTeller.ClientID %>").set_value("0296");
            }
           
            $('#<%=lblAccountLcy.ClientID%>').text(Currency);
            $('#<%=lblCurrency.ClientID%>').text(Currency);
            $('#<%=TTlblCustomerID.ClientID%>').text(CustomerID);
            $('#<%=lblAccountNo.ClientID%>').text($('#<%= TTlblAcctNo.ClientID %>').text());
            $('#<%=lblAccountLcy.ClientID%>').text(Account);
            $('#<%=lblAmountInLCY.ClientID%>').text(Account);
            $('#<%=lblAmmount.ClientID%>').text(AccountText);
            $('#<%=lblDate.ClientID%>').text(DateText);

            $('#<%=lblAZDepodit.ClientID%>').text("AZ Deposit Credit");

        }

        $("#<%=tbDepositCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btSearch.ClientID %>").click();
            }

        });


        function rcbCurrentcy_ClientSelectedIndexChanged(sender, eventArgs) {            
            CompareCurrencyVSPayment();
        }

        function rcbPaymentCcy_OnClientSelectedIndexChanged(sender, eventArgs) {
            var currency = $("#<%=rcbCurrentcy.ClientID%>").val();
            var ttCurency = eventArgs.get_item().get_value();
            if (currency != ttCurency) {
                radalert("Payment CCY  does not same Currency", "Alert");
            }
            CompareCurrencyVSPayment();
        }

        function CompareCurrencyVSPayment() {
            
            if ($('#<%= rcbCurrentcy.ClientID %>').val() == $('#<%= TTrcbPaymentCcy.ClientID %>').val()) {
                $find('<%=CompareCurrencyVSPaymentCCY.ClientID %>').set_value("1");
            } else {
                $find('<%=CompareCurrencyVSPaymentCCY.ClientID %>').set_value("");
            }
        }
       
      
        function alert(m) {
            m = m.replace(/-/g, '<br>-') + "<br> &nbsp;<br> &nbsp;";
            radalert(m,"Alert");
        }
    </script>

   
</telerik:radcodeblock>

<div style="visibility: hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" />
    <asp:RequiredFieldValidator
        runat="server" Display="None"
        ID="RequiredFieldValidator7"
        ControlToValidate="CompareCurrencyVSPaymentCCY"
        ValidationGroup="Commit" InitialValue="" ErrorMessage="Payment CCY  does not same Currency" ForeColor="Red">
    </asp:RequiredFieldValidator>
    <telerik:radtextbox id="CompareCurrencyVSPaymentCCY" runat="server" text="1" width="50"></telerik:radtextbox>
</div>
