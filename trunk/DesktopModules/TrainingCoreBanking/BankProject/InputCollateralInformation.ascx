<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InputCollateralInformation.ascx.cs" Inherits="BankProject.InputCollateralInformation" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>
<telerik:radwindowmanager id="RadWindowManager1" runat="server" enableshadow="true" />
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    })
</script>

<telerik:radtoolbar runat="server" id="RadToolBar1" enableroundedcorners="true" enableshadows="true" width="100%" onbuttonclick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btPreview" CommandName="Preview">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="search">
        </telerik:RadToolBarButton>
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/edit.png" 
            ToolTip="Edit Data" Value="btEdit" CommandName="edit" />
    </Items>
</telerik:radtoolbar>

<div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 260px; padding: 5px 0 5px 20px">
                <telerik:radtextbox id="tbCollInfoID" runat="server" forecolor="Black" validationgroup="Group1" />
                <i>
                    <asp:Label ID="lblCheckCustomer" runat="server" Text="" ForeColor="Black" /></i>
            </td>
        </tr>
    </table>
</div>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#blank1">Collateral Information</a></li>
        <li style="visibility: hidden;"><a href="#blank2">Contingent Entry Information</a></li>
    </ul>
    <div id="blank1" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="Commit" />
        <fieldset>
            <legend style="text-transform: uppercase; font-weight: bold;">Collateral Detais</legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Collateral Type:</td>
                    <td class="MyContent" width="350">
                        <telerik:radcombobox id="rcbCollateralType" runat="server" markfirstmatch="true" allowcustomtext="false" appenddatabounditems="true"
                            autopostback="true" width="350" onselectedindexchanged="rcbCollateralType_OnselectedIndexchanged" forecolor="Black">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:radcombobox>
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
                <tr>
                    <td class="MyLable">Collateral Code:</td>
                    <td class="MyContent">
                        <telerik:radcombobox id="rcbCollateralCode" runat="server" markfirstmatch="true" allowcustomtext="false"
                            width="350" autopostback="true" onselectedindexchanged="rcbCollateralCode_OnSelectedIndexChanged"
                            forecolor="Black">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:radcombobox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Currency:</td>
                    <td class="MyContent" width="200">
                        <telerik:radcombobox id="rcbCurrency" runat="server" markfirstmatch="true" allowcustomtext="false" forecolor="Black"
                            width="150" autopostback="true" onselectedindexchanged="rcbCurrency_OnClientSelectedIndexChanged">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>                     
                                    <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:radcombobox>
                    </td>

                </tr>
                <tr>
                    <td class="MyLable">Contingent Acct:<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator1"
                            ControlToValidate="rcbContingentAcct" ValidationGroup="Commit" InitialValue="" ErrorMessage="Contingent Account is required"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent" width="350">
                        <telerik:radcombobox id="rcbContingentAcct" runat="server" markfirstmatch="true" allowcustomtext="false"
                            width="350" forecolor="Black">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:radcombobox>
                    </td>

                </tr>
                <tr>
                    <td class="MyLable">Description:</td>
                    <td class="MyContent" width="350">
                        <telerik:radtextbox id="tbDescription" runat="server" validationgroup="Group1" width="350" textmode="MultiLine" forecolor="Black" />
                    </td>
                    <td><%--<a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a>--%></td>

                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Address:<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator2" ControlToValidate="tbAddress"
                            ValidationGroup="Commit" InitialValue="" ErrorMessage="Address is Required !" ForeColor="red" />
                    </td>
                    <td class="MyContent" width="350">
                        <telerik:radtextbox id="tbAddress" runat="server" validationgroup="Group1" width="350" forecolor="Black" />
                    </td>
                    <td class="MyLable"><%--<a class="add"> <img src="Icons/Sigma/Add_16X16_Standard.png" /></a>--%></td>
                    <td class="MyContent"></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">

                <tr>
                    <td class="MyLable">Collateral Status:<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None" ControlToValidate="rcbCollateralStatus"
                            ValidationGroup="Commit" InitialValue="" ErrorMessage="Collateral Status is Required !" />
                    </td>
                    <td class="MyContent">
                        <telerik:radcombobox id="rcbCollateralStatus" runat="server" markfirstmatch="true" allowcustomtext="false"
                            width="150" forecolor="Black">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:radcombobox>
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
                <tr>
                    <td class="MyLable">Customer ID/ Name:</td>
                    <td class="MyContent" width="350">
                        <telerik:radtextbox id="tbCustomerIDName" runat="server" width="350" validationgroup="Group1" forecolor="Black" />
                    </td>

                </tr>
                <tr>
                    <td class="MyLable">Collateral Owner:</td>
                    <td class="MyContent">
                        <telerik:radtextbox id="tbNotes" runat="server" width="350" validationgroup="Group1" textmode="MultiLine" forecolor="Black" />
                    </td>
                    <td><%--<a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png">--%></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Company Storage:<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None" ControlToValidate="rcbCompanyStorage"
                            ValidationGroup="Commit" InitialValue="" ErrorMessage="Company Storage is Required !" />
                    </td>
                    <td class="MyContent" width="250">
                        <telerik:radcombobox id="rcbCompanyStorage" runat="server" markfirstmatch="true" allowcustomtext="false"
                            width="350" forecolor="Black">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />                           
                            </Items>
                        </telerik:radcombobox>
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
            </table>
            <table id="divGlobalID1" width="100%" cellpadding="0" cellspacing="0">
                <tr >
                    <td class="MyLable">Global Limit ID:</td>
                    <td class="MyContent">
                        <telerik:radcombobox id="rcbGlobalLimitID" runat="server" markfirstmatch="true" allowcustomtext="false" appenddatabounditems="true"
                            forecolor="Black">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:radcombobox>
                        <asp:ImageButton ID="btGlobalLimitID" runat="server" OnClick="btGlobalLimitID_Click" ImageUrl="~/Icons/Sigma/Add_16X16_Standard.png" />

                    </td>
                </tr>
            </table>
            <table id="divGlobalID2" width="100%" cellpadding="0" cellspacing="0">
                <tr >
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:radcombobox id="rcbGlobalLimitID2" runat="server" markfirstmatch="true" allowcustomtext="false" appenddatabounditems="true"
                            forecolor="Black">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:radcombobox>
                        <asp:ImageButton ID="btRemoveGlobalLimitID" runat="server" OnClick="btRemoveGlobalLimitID_Click" ImageUrl="~/Icons/Sigma/Delete_16X16_Standard.png" />

                    </td>
                </tr>

            </table>
        </fieldset>
        <fieldset>
            <legend style="text-transform: uppercase; font-weight: bold;">Value Detais</legend>
            <table width="100%" cellpadding="0" cellspacing="0">

                <tr>
                    <td class="MyLable">Country:</td>
                    <td class="MyContent">
                        <telerik:radcombobox id="rcbCountry" runat="server" markfirstmatch="true" allowcustomtext="false"
                            width="200" forecolor="Black" appenddatabounditems="true">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>                  
                               <telerik:RadComboBoxItem Value="" Text="" /> 
                            </Items>
                        </telerik:radcombobox>
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
                <tr>
                    <td class="MyLable">Nominal Value:<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="None" ControlToValidate="tbNominalValue"
                            ValidationGroup="Commit" InitialValue="" ErrorMessage="Nominal Value is Required !" />
                    </td>
                    <td class="MyContent">
                        <telerik:radtextbox id="tbNominalValue" runat="server" validationgroup="Group1" width="150"
                            forecolor="Black">
                             <ClientEvents  OnBlur="SetNumber" OnFocus="ClearCommas"/> 
                        </telerik:radtextbox>

                    </td>
                </tr>
                <asp:HiddenField ID="hfInternalLimit" runat="server" />

                <tr>
                    <td class="MyLable">Provision Value:</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox runat="server" id="tbProvisionValue" borderwidth="0" readonly="true" forecolor="Black"></telerik:radnumerictextbox>
                        <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
                <tr>
                    <td class="MyLable">Maximum Loan Value:</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox id="tbExeValue" runat="server" validationgroup="Group1" width="150"
                            numberformat-decimaldigits="2" forecolor="Black">
                        </telerik:radnumerictextbox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Allocated Amt:</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAllocatedAmt" runat="server" ForeColor="Black" />
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
                <tr>
                    <td class="MyLable">Value Date:</td>
                    <td class="MyContent">
                        <telerik:raddatepicker id="rdpValueDate" width="150" runat="server" forecolor="Black"></telerik:raddatepicker>
                    </td>
                    <td class="MyLable">Expiry Date:</td>
                    <td class="MyContent">
                        <telerik:raddatepicker id="rdpExpiryDate" width="150" runat="server" forecolor="Black"></telerik:raddatepicker>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Review Date Freq:</td>
                    <td class="MyContent">
                        <telerik:raddatepicker runat="server" id="rdpReviewDate" width="150" forecolor="Black"></telerik:raddatepicker>
                    </td>
                    <td style="visibility: hidden;">
                        <telerik:radnumerictextbox runat="server" id="tbRate" numberformat-decimaldigits="5"
                            clientevents-onblur="clientEvent_NominalValue" forecolor="Black">
                         </telerik:radnumerictextbox>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset style="visibility: hidden;">
            <legend style="text-transform: uppercase; font-weight: bold;">Credit Card Information</legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr style="visibility: hidden;">
                    <td class="MyLable">Maximum Value:</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox id="tbMaxValue" numberformat-decimaldigits="0" runat="server" validationgroup="Group1" width="150"></telerik:radnumerictextbox>

                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Credit Card No:</td>
                    <td class="MyContent" width="150">
                        <telerik:radnumerictextbox id="tbCreditCardNo" runat="server" validationgroup="Group1" width="150"
                            numberformat-groupseparator="" numberformat-decimaldigits="0"></telerik:radnumerictextbox>
                    </td>
                    <td><a class="add">
                        <img src="Icons/Sigma/Add_16X16_Standard.png" /></a></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Card Type:</td>
                    <td class="MyContent">
                        <telerik:radcombobox id="rcbCardType" runat="server" markfirstmatch="true" allowcustomtext="false"
                            width="150">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" /> 
                            </Items>
                        </telerik:radcombobox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Cardholder:</td>
                    <td class="MyContent">
                        <telerik:radcombobox id="rcbCardholder" runat="server" markfirstmatch="true" allowcustomtext="false"
                            width="150">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />  
                            </Items>
                        </telerik:radcombobox>
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
                <tr>
                    <td class="MyLable">Total Col Amt:</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox id="tbTotalColAmt" runat="server" validationgroup="Group1"
                            numberformat-decimaldigits="0" width="150"></telerik:radnumerictextbox>

                    </td>
                </tr>
            </table>
        </fieldset>
    </div>

<%-- Contingent Entry information --%>
<div id="blank2" class="dnnClear">
    <fieldset>
        <legend style="text-transform: uppercase; font-weight: bold;">Customer Information</legend>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Contingent Entry ID: </td>
                <td class="MyContent">
                    <asp:TextBox ID="tbContingentEntryID" runat="server" ValidationGroup="Group1" ReadOnly="true" />
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer ID:</td>
                <td class="MyContent">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="350">
                                <telerik:radtextbox id="tbCustomerIDName_Cont" runat="server" validationgroup="Group1" readonly="true" width="350" />
                                <%--<telerik:RadComboBox ID="rcbCustomerID" runat="server" AllowCustomText="false"
                                         MarkFirstMatch="true" width="350" AppendDataBoundItems="true" >                                          
                                         <ExpandAnimation Type="None" />
                                         <CollapseAnimation Type="None" />
                                         <Items>
                                             <telerik:RadComboBoxItem Value="" Text="" />
                                         </Items>
                                     </telerik:RadComboBox> --%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Address:</td>
                <td class="MyContent">
                    <telerik:radtextbox id="tbAddress_cont" runat="server" validationgroup="Group1" width="350" textmode="MultiLine" readonly="true" />
                    <%--<a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a>--%>
                </td>
                <td></td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">

            <tr>
                <td class="MyLable">ID / Tax Code:</td>
                <td class="MyContent" width="350">
                    <telerik:radtextbox id="tbIDTaxCode" runat="server" validationgroup="Group1" width="150" readonly="true" />
                </td>
                <td class="MyLable">Date Of Issue:</td>
                <td class="MyContent">
                    <telerik:radtextbox id="tbDateOfIssue" runat="server" readonly="true" />
                    <%-- <telerik:RadDatePicker ID="rdpDateOfIssue" runat="server" />--%>
                </td>
            </tr>
        </table>

    </fieldset>
    <fieldset>
        <legend style="text-transform: uppercase; font-weight: bold;">CONTINGENT&nbsp; Information</legend>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Transaction Code:<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator5"
                        ControlToValidate="rcbContingentAcct" ValidationGroup="Commit" InitialValue="" ErrorMessage="Transaction Code is required"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:radcombobox id="rcbTransactionCode" runat="server" markfirstmatch="true" allowcustomtext="false"
                        width="150" onselectedindexchanged="rcbTransactionCode_OnSelectedIndexChanged" autopostback="true">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />   
                                <telerik:RadComboBoxItem Value="901" Text="901 - Nhap ngoai bang" />                               
                                <telerik:RadComboBoxItem Value="902" Text="902 - Xuat ngoai bang" />                               
                            </Items>
                        </telerik:radcombobox>
                </td>

            </tr>
            <tr>
                <td class="MyLable">Debit or Credit:<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator6"
                        ControlToValidate="rcbContingentAcct" ValidationGroup="Commit" InitialValue="" ErrorMessage="Debit or Credit is required"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:radcombobox id="rcbDebitOrCredit" runat="server" markfirstmatch="true" allowcustomtext="false"
                        width="150" enabled="false">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />  
                                <telerik:RadComboBoxItem Value="D" Text="D - Debit" /> 
                                <telerik:RadComboBoxItem Value="C" Text="C - Credit" />                               
                            </Items>
                        </telerik:radcombobox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Currency:</td>
                <td class="MyContent">
                    <telerik:radcombobox id="rcbFreignCcy" runat="server" markfirstmatch="true" allowcustomtext="false"
                        width="150">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />                                
                            </Items>
                        </telerik:radcombobox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Account No:<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator7"
                        ControlToValidate="rcbContingentAcct" ValidationGroup="Commit" InitialValue="" ErrorMessage="CategoryID is required"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:radcombobox id="rcbAccountNo" runat="server" allowcustomtext="false" markfirstmatch="true"
                        width="350">
                             <CollapseAnimation Type="None" />
                             <ExpandAnimation Type="None" />
                             <Items>
                                 <telerik:RadComboBoxItem value="" text=""/>
                             </Items>
                         </telerik:radcombobox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Amount:</td>
                <td class="MyContent">
                    <telerik:radnumerictextbox id="tbAmount" runat="server" width="150" validationgroup="Group1"></telerik:radnumerictextbox>

                </td>
                <td class="MyLable">Deal Rate:</td>
                <td class="MyContent">
                    <telerik:radnumerictextbox id="tbDealRate" runat="server" validationgroup="Group1"
                        numberformat-decimaldigits="5"></telerik:radnumerictextbox>

                </td>

            </tr>

            <tr>
                <td class="MyLable">Value Date:</td>
                <td class="MyContent">
                    <telerik:raddatepicker width="150" id="rdpValuedate_cont" runat="server" validationgroup="Group1"></telerik:raddatepicker>

                </td>
            </tr>
            <tr>
                <td class="MyLable">Reference No:<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator8"
                        ControlToValidate="rcbContingentAcct" ValidationGroup="Commit" InitialValue="" ErrorMessage="Reference No is required"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:radtextbox id="tbReferenceNo" runat="server" validationgroup="Group1" width="150" readonly="true" />
                </td>

            </tr>
            <tr>
                <td class="MyLable">Narrative:<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator9"
                        ControlToValidate="rcbContingentAcct" ValidationGroup="Commit" InitialValue="" ErrorMessage="Narrative is required"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td class="MyContent" width="350">
                    <telerik:radtextbox id="tbNarrative" runat="server" validationgroup="Group1" width="350" textmode="MultiLine" />
                </td>


            </tr>
        </table>

    </fieldset>
</div>
</div>

<telerik:radcodeblock id="RadCodeBlock2" runat="server"> 
<script type="text/javascript">
    $("#<%=tbCollInfoID.ClientID%>").keyup(function (event) {

        if (event.keyCode == 13) {
            $("#<%=btSearch.ClientID%>").click();
        }
    });
    function clientEvent_NominalValue()//(sender, args)
    {
        var NominalValue = $find("<%= tbNominalValue.ClientID%>");
        var ExecutionValue = $find("<%= tbExeValue.ClientID%>");
        var Nominal_Temp = NominalValue.get_value();
        ExecutionValue.set_value(parseInt(NominalValue.get_value().replace(',', '').replace(',', '').replace(',', '').replace(',', '').replace(',', ''), 10) * 0.7);
        $find("<%=tbAmount.ClientID%>").set_value(parseInt(NominalValue.get_value().replace(',', ''), 10));
        var Rate = $find("<%=tbRate.ClientID%>").get_value();
        if (Rate != "0.00000") {
            $find("<%= tbProvisionValue.ClientID%>").set_value(Rate * parseInt(NominalValue.get_value().replace(',', '').replace(',', '').replace(',', '').replace(',', '').replace(',', ''), 10));
        } else { $find("<%= tbProvisionValue.ClientID%>").set_value(""); }
    }

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
        return output + ".00";
    }
    function ClearCommas(sender, args) {
        var m = sender.get_value().replace(".00", "").replace(/,/g, '');
        console.log(m);
        sender.set_value(m);
    }



    function DisableGlobleID2() {
        divGlobalID2.style.display = 'none';
    }

    function EnableGlobleID2() {
        divGlobalID2.style.display = 'block';
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
                    $find('<%=tbNominalValue.ClientID %>').focus();
                    return false;
                    break;
            }
        } else {
            console.log("is number" + m);
            number = sender.get_value();
            sender.set_value(addCommas(number));

        }
        //var num = sender.get_value();
        document.getElementById("<%= hfInternalLimit.ClientID%>").value = number;
        clientEvent_NominalValue();
    }
  </script>
    </telerik:radcodeblock>
<telerik:radajaxmanager id="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbCollateralType">
            <UpdatedControls>  
                 <telerik:AjaxUpdatedControl ControlID="rcbCollateralCode" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbTransactionCode">
            <UpdatedControls>  
                 <telerik:AjaxUpdatedControl ControlID="rcbDebitOrCredit" />
            </UpdatedControls>
        </telerik:AjaxSetting> 
        <telerik:AjaxSetting AjaxControlID="rcbCollateralType">
            <UpdatedControls>  
                 <telerik:AjaxUpdatedControl ControlID="rcbContingentAcct" />
                 <telerik:AjaxUpdatedControl ControlID="rcbAccountNo" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbCurrency">
            <UpdatedControls>  
                 <telerik:AjaxUpdatedControl ControlID="rcbContingentAcct" /> 
                <telerik:AjaxUpdatedControl ControlID="rcbAccountNo" />
            </UpdatedControls>
        </telerik:AjaxSetting> 
        <telerik:AjaxSetting AjaxControlID="rcbCollateralCode">
            <UpdatedControls>  
                <telerik:AjaxUpdatedControl ControlID="tbRate" />
                <telerik:AjaxUpdatedControl ControlID="tbProvisionValue" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:radajaxmanager>
<div style="visibility: hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click1" Text="Search" />
</div>
