<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NormalLCEdit.ascx.cs" Inherits="BankProject.NormalLCEdit" %>
<%@ Register Src="Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>
<%@ Register src="Controls/VVComboBox.ascx" tagname="VVComboBox" tagprefix="uc2" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<%@ Register src="NormalLCChargeAmend.ascx" tagname="NormalLCChargeAmend" tagprefix="uc3" %>
<%@ Register src="NormalLCChargeAuthorize.ascx" tagname="NormalLCChargeAuthorize" tagprefix="uc3" %>
<%@ Register src="NormalLCChargeCancel.ascx" tagname="NormalLCChargeCancel" tagprefix="uc3" %>
<%@ Register src="NormalLCChargeIssue.ascx" tagname="NormalLCChargeIssue" tagprefix="uc3" %>

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
    function OnClientButtonClicking(sender, args) {
       
        
        var button = args.get_item();
        if (button.get_commandName() == "doclines") {
            var hv = $('#<%= hdfDisable.ClientID %>').val();
            var rcbLCType = $find('<%=rcbLCType.ClientID %>').get_selectedItem().get_value();
            var rcbCcyAmount = $find('<%=rcbCcyAmount.ClientID %>').get_selectedItem().get_value();

            var rcCommodity = $find('<%=rcCommodity.ClientID %>').get_selectedItem().get_value();
            var ntSoTien = $find("<%= ntSoTien.ClientID %>").get_value();
            if (rcbLCType != "" && rcbCcyAmount != "" && ntSoTien != "" &&
            rcCommodity != "" && ntSoTien != "" && hv == "0" && !clickCalledAfterRadconfirm) {

                var d = new Date();

                var month = d.getMonth() + 1;
                var day = d.getDate();

                var output = (day < 10 ? '0' : '') + day + '/' +
                    (month < 10 ? '0' : '') + month + '/' +
                     d.getFullYear();
                output = output + " " + $find('<%= rcbChargeAcct.ClientID %>').get_selectedItem().get_text() + " Overdraf On \n Available Blance VND - 55497272";
                var chuoithongbao = output.toString();

                //args.set_cancel(!confirm(chuoithongbao));
                args.set_cancel(true);
                lastClickedItem = args.get_item();
                radconfirm(chuoithongbao, confirmCallbackFunction3);
            }
        }
        
    }
    var lastClickedItem = null;
    var clickCalledAfterRadprompt = false;
    var clickCalledAfterRadconfirm = false;
    function confirmCallbackFunction3(args) {
        clickCalledAfterRadconfirm = true;
        lastClickedItem.click();

        lastClickedItem = null;
    }
    
</script> 
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
          </telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />
<div>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
<telerik:RadToolBar runat="server" ID="RadToolBar1" Style="z-index: -1;" 
    OnClientButtonClicking="OnClientButtonClicking"
    EnableRoundedCorners="true" EnableShadows="true" width="100%" OnButtonClick="RadToolBar1_ButtonClick" >
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
        </Items>

    </telerik:RadToolBar>  
    </div>
    <table width="100%" cellpadding="0" cellspacing="0">
<tr>
    <td style="width:200px; padding:5px 0 5px 20px;"><asp:TextBox ID="tbEssurLCCode" runat="server" Width="200" /></td>
</tr>
</table>
    <div class="dnnForm" id="tabs-demo">
        <ul class="dnnAdminTabNav">
            <li><a href="#Main">Main</a></li>
            <li><a href="#MT700">MT700</a></li>
            <%--<li><a href="#MT740">MT740</a></li>--%>
            <li><a href="#Charges">Charges</a></li>
            <li><a href="#DeliveryAudit">Delivery Audit</a></li>
        </ul>
        <div id="Main" class="dnnClear">
           
             <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                    <td class="MyLable">LC Type <span class="Required">(*)</span><asp:RequiredFieldValidator 
                runat="server" Display="None"
                ID="RequiredFieldValidator1" 
                ControlToValidate="rcbLCType" 
                        ValidationGroup="Commit"
                InitialValue="" 
                ErrorMessage="LC Type is Required" ForeColor="Red">
            </asp:RequiredFieldValidator></td>
                    <td class="MyContent" style="width:355px;">
                        <telerik:RadComboBox  AppendDataBoundItems="True" DropDownCssClass="KDDL"
                        ID="rcbLCType" Runat="server"  width="355"
                        MarkFirstMatch="True"  OnItemDataBound="rcbLCType_ItemDataBound"  AutoPostBack="true" 
                    OnSelectedIndexChanged="rcbLCType_SelectIndexChange"
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
                    </td>
                  <td><asp:Label ID="lblLCType" runat="server" /></td>
                </tr>
                 </table>
            <asp:UpdatePanel ID="UpdatePanelApplicantID" runat="server">
                <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Applicant ID</td>
                    <td class="MyContent">
                    <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td  Width="355"><telerik:RadComboBox AppendDataBoundItems="True"   AutoPostBack="true" 
                    OnSelectedIndexChanged="rcbApplicantID_SelectIndexChange"
                            OnItemDataBound="rcbApplicantID_ItemDataBound"
                    ID="rcbApplicantID" Runat="server"
                    MarkFirstMatch="True" Width="355" Height="150px" 
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox><asp:Label ID="lblCustomerID" runat="server" /></td>
                        <td> <i><asp:Label ID="lblCustomer" runat="server" /></i></td>
                    </tr>
                    </table>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Applicant Name</td>
                    <td class="MyContent"><telerik:RadTextBox ID="tbApplicantName" runat="server" Width="100%"></telerik:RadTextBox></td>
                    </tr>
                 </table>

                <uc1:VVTextBox runat="server" id="tbApplicantAddr" VVTLabel="Applicant (Name-Addr)"  VVTDataKey='tbEssurLCCode' />
                    </ContentTemplate>
            </asp:UpdatePanel>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr hidden="hidden">
                    <td class="MyLable">Applicant Acct</td>
                    <td class="MyContent"><telerik:RadComboBox 
                    ID="rcbApplicantAcct" Runat="server" 
                    MarkFirstMatch="True" Width="150" 
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Ccy, Amount <span class="Required">(*)</span><asp:RequiredFieldValidator 
                runat="server" Display="None"
                ID="RequiredFieldValidator2" 
                ControlToValidate="rcbCcyAmount" 
                        ValidationGroup="Commit"
                InitialValue="" 
                ErrorMessage="Ccy is Required" ForeColor="Red">
            </asp:RequiredFieldValidator></td>
                    <td class="MyContent"><telerik:RadComboBox onclientselectedindexchanged="rcbCcyAmount_OnClientSelectedIndexChanged"
                    ID="rcbCcyAmount" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
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
                </telerik:RadComboBox><span class="Required">(*)</span>
                <telerik:radnumerictextbox incrementsettings-interceptarrowkeys="true" incrementsettings-interceptmousewheel="true"  runat="server" id="ntSoTien" width="200px" AutoPostBack="true" 
                        OnTextChanged="ntSoTien_TextChanged"
                    ClientEvents-OnValueChanged ="ntSoTien_OnValueChanged" />
                        <asp:RequiredFieldValidator 
                runat="server" Display="None"
                ID="RequiredFieldValidator3" 
                ControlToValidate="ntSoTien" 
                        ValidationGroup="Commit"
                InitialValue="" 
                ErrorMessage="Amount is Required" ForeColor="Red">
            </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Cr.Tolerance</td>
                    <td class="MyContent">
                    <telerik:radnumerictextbox incrementsettings-interceptarrowkeys="true" incrementsettings-interceptmousewheel="true"  ClientEvents-OnValueChanged="tbcrTolerance_TextChanged"   runat="server" id="tbcrTolerance" width="80px" />
                     51 Dr.Tolerance <telerik:radnumerictextbox incrementsettings-interceptarrowkeys="true" incrementsettings-interceptmousewheel="true"  runat="server"  ClientEvents-OnValueChanged="tbdrTolerance_TextChanged"  id="tbdrTolerance" width="80px" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Issuing Date</td>
                    <td class="MyContent">
                        <%--<asp:TextBox ID="tbIssuingDate" runat="server" Width="150" />--%>
                    <telerik:RadDatePicker runat="server" ID="tbIssuingDate">
                        <ClientEvents OnDateSelected="tbIssuingDate_DateSelected" />
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Expiry Date</td>
                    <td class="MyContent">
                        <%--<asp:TextBox ID="tbExpiryDate" runat="server" Width="150" />--%>
                     <telerik:RadDatePicker runat="server" ID="tbExpiryDate">
                         <ClientEvents OnDateSelected="tbExpiryDate_DateSelected" />
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Expiry Place</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbExpiryPlace" Width="150" Runat="server" ClientEvents-OnValueChanged="rcbExpiryPlace_OnClientSelectedIndexChanged" ></telerik:RadTextBox>
                </telerik:RadTextBox>
                    </td>
                </tr>
                </table>
            <table width="100%" cellpadding="0" cellspacing="0"
                <tr>
                    <td class="MyLable">Contingent Expiry</td>
                    <td Width="150" class="MyContent">
                        <%--<asp:TextBox ID="tbContingentExpiry" runat="server" AutoPostBack="true" Width="150" OnTextChanged="tbContingentExpiry_TextChanged" />--%>
                        <telerik:RadDatePicker runat="server" MinDate="1/1/1900" ID="tbContingentExpiry">
                        </telerik:RadDatePicker>
                    </td>
                    <td>30 Archve Date(Sys.Field)</td>
                </tr>
                </table>
            <table width="100%" cellpadding="0" cellspacing="0"
                <tr>
                    <td class="MyLable">Pay Type</td>
                    <td class="MyContent">
                        <asp:Label ID="lblPayType" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Payment pCt</td>
                    <td class="MyContent">
                        <asp:Label ID="lblPaymentpCt" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Payment Portion</td>
                    <td class="MyContent">
                        <asp:Label ID="lblPaymentPortion" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Accpt Time Band</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr hidden="hidden">
                    <td class="MyLable">Limit Ref <span class="Required">(*)</span><asp:RequiredFieldValidator 
                runat="server" Display="None"
                ID="RequiredFieldValidator4" 
                ControlToValidate="tbLimitRef" 
                        ValidationGroup="Commit"
                InitialValue="" 
                ErrorMessage="Limit Ref is Required" ForeColor="Red">
            </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                    ID="tbLimitRef" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                        <telerik:RadComboBoxItem Value="24001.01" Text="24001.01" />
                        <telerik:RadComboBoxItem Value="24001.02" Text="24001.02" />
                        <telerik:RadComboBoxItem Value="24002.01" Text="24002.01" />
                        <telerik:RadComboBoxItem Value="24002.02" Text="24002.02" />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                </tr>
                 </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Account Officer</td>
                    <td class="MyContent">
                    <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td  Width="355"><telerik:RadComboBox AppendDataBoundItems="True"  
                    ID="rcbAccountOfficer" Runat="server"
                    MarkFirstMatch="True" Width="355" Height="150px" 
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox><asp:Label ID="Label1" runat="server" /></td>
                        <td> <i><asp:Label ID="Label2" runat="server" /></i></td>
                    </tr>
                    </table>
                    </td>
                </tr>
                 </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Contact No</td>
                    <td class="MyContent"><asp:TextBox ID="tbContactNo" runat="server" Width="300" />
                    </td>
                </tr>
                    </table>
            <fieldset>
                            <legend>
              <div style="font-weight:bold; text-transform:uppercase;">Beneficiary Details</div>
                                </legend>
                <div>
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr hidden="hidden">
                    <td class="MyLable">Beneficiary No</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                    ID="rcbBeneficiaryDetails" Runat="server" 
                    MarkFirstMatch="True" Width="300"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                             <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="ASIA COMMERCIAL BANK" Text="ASCBVNVX - ASIA COMMERCIAL BANK" />
                            <telerik:RadComboBoxItem Value="CITIBANK N.A." Text="CITIVNVX - CITIBANK N.A." />
                            <telerik:RadComboBoxItem Value="HSBC BANK (VIETNAM) LTD." Text="HSBCVNVX - HSBC BANK (VIETNAM) LTD." />
                            <telerik:RadComboBoxItem Value="CITIBANK N.A." Text="CITIUS33 - CITIBANK N.A." />
                            <telerik:RadComboBoxItem Value="BANK OF AMERICA, N.A." Text="BOFAUS6H - BANK OF AMERICA, N.A." />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                </tr>
                </table>
                    <uc1:VVTextBox runat="server" id="tbBeneficiaryNameAddr" OnBlur="tbBeneficiaryNameAddr_OnBlur" Width="300" VVTLabel="Beneficiary Name-Addr"  VVTDataKey='tbEssurLCCode' />
                </div>
                </fieldset>
            <fieldset>
                            <legend>
              <div style="font-weight:bold; text-transform:uppercase;">Advising/Reimbursing Bank Details</div></legend>
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr hidden="hidden">
                    <td class="MyLable">Advise Bank Ref</td>
                    <td class="MyContent"><asp:TextBox ID="tbAdviseBankRef" runat="server" Width="300" />
                    </td>
                </tr>
                    </table>
            <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Advise Bank No</td>
                    <td class="MyContent" style="width:150px;"><telerik:RadComboBox 
                    ID="rcbAdviseBankNo" Runat="server" AppendDataBoundItems="true"
                        OnClientSelectedIndexChanged="AdviseBank_OnClientSelectedIndexChanged"
                    MarkFirstMatch="True" Width="450"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                           
                    </Items>
                </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="lblAdviseBankNo" runat="server" /></td>
                </tr>
                     </table>
                <uc1:VVTextBox runat="server" id="tbAdviseBankAddr" VVTLabel="Advise Bank Addr" Width="300"  VVTDataKey='tbEssurLCCode' />
            <table width="100%" cellpadding="0" cellspacing="0" hidden="hidden" >
                <tr>
                    <td class="MyLable">Advise Bank Acct</td>
                    <td class="MyContent"><telerik:RadComboBox 
                    ID="rcbAdviseBankAcct" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                </telerik:RadComboBox>
                    </td>
                </tr>
                </table>
            <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Reimb. Bank No</td>
                    <td class="MyContent" style="width:150px;"><telerik:RadComboBox 
                    ID="rcbReimbBankNo" Runat="server" AppendDataBoundItems="true"
                    MarkFirstMatch="True" Width="450"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                         <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="lblReimbBankNo" runat="server" /></td>
                </tr>
                </table>
                <uc1:VVTextBox runat="server" id="tbReimbBankAddr" VVTLabel="Reimb Bank Addr" Width="300"  VVTDataKey='tbEssurLCCode' />
            <table width="100%" cellpadding="0" cellspacing="0" hidden="hidden" >
                <tr>
                    <td class="MyLable">Reimb. Bank Acct</td>
                    <td class="MyContent"><telerik:RadComboBox 
                    ID="tbReimbBankAcct" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                </telerik:RadComboBox>
                    </td>
                </tr>
                 </table>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
            <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Advise Thru No. (swfit code)</td>
                    <td class="MyContent" style="width:150px;" ><telerik:RadComboBox 
                    ID="tbAdviseThruNo" AutoPostBack="true" Runat="server" AppendDataBoundItems="true"
                        OnSelectedIndexChanged="tbAdviseThruNo_SelectedIndexChanged"
                    MarkFirstMatch="True" Width="450" 
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="lblAdviseThruNo" runat="server" /></td>
                </tr>
                </table>
                <uc1:VVTextBox runat="server" id="tbAdviseThruAddr" VVTLabel="Advise Thru bank (Name-Addr)" Width="300"  VVTDataKey='tbEssurLCCode' />
                    </ContentTemplate>
                   </asp:UpdatePanel>
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr hidden="hidden">
                    <td class="MyLable">Advise Thru Acct</td>
                    <td class="MyContent"><telerik:RadComboBox 
                    ID="rcbAdviseThruAcct" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                </telerik:RadComboBox>
                    </td>
                </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0" hidden="hidden" >
                <tr>
                    <td class="MyLable">Avail With No.</td>
                    <td width="150" class="MyContent"><telerik:RadComboBox 
                    ID="rcbAvailWithNo" Runat="server" AppendDataBoundItems="true"
                    MarkFirstMatch="True" Width="300"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                         <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                           
                    </Items>
                </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="lblAvailWithNo" runat="server" /></td>
                </tr>
                    </table>
                <uc1:VVTextBox runat="server" id="tbAvailWithNameAddr" VVTLabel="Avail With Name.Addr" Width="300"  VVTDataKey='tbEssurLCCode' />
                <table width="100%" cellpadding="0" cellspacing="0">
                </table>
                </fieldset>
            <fieldset>
                            <legend>
              <div style="font-weight:bold;text-transform:uppercase;">Commodity/Prov Details</div></legend>
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Commodity <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator 
                runat="server" Display="None"
                ID="RequiredFieldValidator5" 
                ControlToValidate="rcCommodity" 
                        ValidationGroup="Commit"
                InitialValue="" 
                ErrorMessage="Commodity is Required" ForeColor="Red">
            </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent" width="250"><telerik:RadComboBox AutoPostBack="true" DropDownCssClass="KDDL"
                        ID="rcCommodity" Runat="server"  width="280"  AppendDataBoundItems="True"
                        MarkFirstMatch="True"  OnItemDataBound="rcCommodity_ItemDataBound"
                        OnSelectedIndexChanged="rcCommodity_SelectIndexChange"
                        AllowCustomText="false" >
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <HeaderTemplate>
                            <table style="width:250px" cellpadding="0" cellspacing="0"> 
                              <tr> 
                                 <td style="width:60px;"> 
                                    ID 
                                 </td> 
                                 <td style="width:200px;"> 
                                    Name
                                 </td> 
                              </tr> 
                           </table> 
                       </HeaderTemplate>
                        <ItemTemplate>
                        <table style="width:250px"  cellpadding="0" cellspacing="0"> 
                          <tr> 
                             <td style="width:60px;"> 
                                <%# DataBinder.Eval(Container.DataItem, "ID")%> 
                             </td> 
                             <td style="width:200px;"> 
                                <%# DataBinder.Eval(Container.DataItem, "Name2")%> 
                             </td> 
                          </tr> 
                       </table> 
               </ItemTemplate>
                    </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="lblCommodity" runat="server" /></td>
                </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Prov % <span class="Required">(*)</span><asp:RequiredFieldValidator 
                runat="server" Display="None"
                ID="RequiredFieldValidator6" 
                ControlToValidate="tbProv" 
                        ValidationGroup="Commit"
                InitialValue="" 
                ErrorMessage="Prov % is Required" ForeColor="Red">
            </asp:RequiredFieldValidator></td>
                    <td class="MyContent"><asp:TextBox ID="tbProv" runat="server" Text="0" Width="100" />
                    </td>
                </tr>
            </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Lc Amount Secured<span class="Required">(*)</span><asp:RequiredFieldValidator 
                runat="server" Display="None"
                ID="RequiredFieldValidator7" 
                ControlToValidate="tbLcAmountSecured" 
                        ValidationGroup="Commit"
                InitialValue="" 
                ErrorMessage="Prov % is Lc Amount Secured" ForeColor="Red">
            </asp:RequiredFieldValidator></td>
                    <td class="MyContent"><asp:TextBox ID="tbLcAmountSecured" runat="server" Text="0" Width="100" />
                    </td>
                </tr>
            </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Lc Amount UnSecured<span class="Required">(*)</span><asp:RequiredFieldValidator 
                runat="server" Display="None"
                ID="RequiredFieldValidator8" 
                ControlToValidate="tbLcAmountUnSecured" 
                        ValidationGroup="Commit"
                InitialValue="" 
                ErrorMessage="Prov % is Lc Amount Secured" ForeColor="Red">
            </asp:RequiredFieldValidator></td>
                    <td class="MyContent"><asp:TextBox ID="tbLcAmountUnSecured" runat="server" Text="0" Width="100" />
                    </td>
                </tr>
            </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Loan Principal</td>
                    <td class="MyContent"><asp:TextBox ID="tbLoanPrincipal" runat="server" Text="0" Width="100" />
                    </td>
                </tr>
            </table>
                </fieldset>
        </div>
        <div id="MT700" class="dnnClear">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Receiving Bank<span class="Required">(*)</span><asp:RequiredFieldValidator 
                runat="server" Display="None"
                ID="RequiredFieldValidator9" 
                ControlToValidate="comboRevivingBank" 
                        ValidationGroup="Commit"
                InitialValue="" 
                ErrorMessage="Prov % is Reviving Bank" ForeColor="Red">
            </asp:RequiredFieldValidator></td>
                    <td style="width: 450px" class="MyContent">
                        <telerik:RadComboBox width="450" AppendDataBoundItems="true"
                            ID="comboRevivingBank" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:Label ID="tbRevivingBankName" runat="server" />
                    </td>
                </tr>
                
                <tr>    
                    <td style="width: 250px" class="MyLable" style="color: #d0d0d0">Sequence of Total</td>
                    <td class="MyContent">
                        <asp:Label ID="tbBaquenceOfTotal" runat="server"  Text="Populated by System" />
                    </td>
                    <td></td>
                </tr>
                
                <tr>    
                    <td style="width: 250px" class="MyLable">Form of Documentary Credit</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="comboFormOfDocumentaryCredit" Runat="server" 
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="IRREVOCABLE" Text="IRREVOCABLE"/>
                                <telerik:RadComboBoxItem Value="REVOCABLE" Text="REVOCABLE"/>
                                <telerik:RadComboBoxItem Value="IRREVOCABLE TRANSFERABLE" Text="IRREVOCABLE TRANSFERABLE"/>
                                <telerik:RadComboBoxItem Value="REVOCABLE TRANSFERABLE" Text="REVOCABLE TRANSFERABLE"/>

                                <telerik:RadComboBoxItem Value="IRREVOCABLE STANDBY" Text="IRREVOCABLE STANDBY"/>
                                <telerik:RadComboBoxItem Value="REVOCABLE STANDBY" Text="REVOCABLE STANDBY"/>
                                <telerik:RadComboBoxItem Value="IRREVOC TRANS STANDBY" Text="IRREVOC TRANS STANDBY"/>
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:Label id="tbFormOfDocumentaryCreditName" runat="server" />
                    </td>
                </tr>
                <tr>    
                    <td style="width: 250px" class="MyLable">Documentary Credit Number</td>
                    <td class="MyContent">
                        LC Reference number
                    </td>
                    <td></td>
                </tr>
                <tr hidden="hidden" >    
                    <td style="width: 250px" class="MyLable">Documentary Credit Number</td>
                    <td class="MyContent">
                        Reference to Pre-Advice
                    </td>
                    <td></td>
                </tr>
                <tr>    
                    <td style="width: 250px" class="MyLable">Date of Issue</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dteDateOfIssue" width="200" runat="server" />
                    </td>
                    <td></td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Date and Place of Expiry</td>
                    <td style="width: 200px" class="MyContent">
                       <telerik:RadDatePicker ID="dteMT700DateAndPlaceOfExpiry" width="200" runat="server" />
                    </td>
                    <td><telerik:RadTextBox ID="tbPlaceOfExpiry" runat="server" />
                </tr>
                <tr>    
                    <td style="width: 250px" class="MyLable">Application Rule</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="comboAvailableRule" Runat="server" 
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="EUCP LASTED VERSION" Text="EUCP LASTED VERSION" />
                                <telerik:RadComboBoxItem Value="EUCPURR LASTED VERSION" Text="EUCPURR LASTED VERSION" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:Label ID="tbAvailableRuleName" runat="server" />
                    </td>
                </tr>
            </table>
              <uc1:VVTextBox runat="server" id="tbApplicantBank" LBWidth="250" VVTLabel="Applicant Bank"  />
            <table width="100%" cellpadding="0" cellspacing="0" hidden="hidden" >
                <tr>    
                    <td style="width: 250px" class="MyLable">Applicant</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadTextBox ID="tbApplicant50" width="200" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblApplicant50" runat="server" />
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0" hidden="hidden" >
                <tr>    
                    <td style="width: 250px" class="MyLable">Beneficiary (Customer No)</td>
                    <td class="MyContent">
                        <telerik:RadComboBox AppendDataBoundItems="True" 
                    ID="comBeneficiary59" Runat="server"
                    MarkFirstMatch="True" Width="355" Height="150px" 
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0" hidden="hidden" >
                <tr>    
                    <td style="width: 250px" class="MyLable">Beneficiary (Name and Address)</td>
                    <td style="width:355px" class="MyContent">
                        <telerik:RadTextBox width="355" ID="tbDocumentary_NameAndAddress" runat="server" />
                    </td>
                    <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Currency Code, Amount
                        <span class="Required">(*)</span><asp:RequiredFieldValidator 
                runat="server" Display="None"
                ID="RequiredFieldValidator10" 
                ControlToValidate="comboCurrencyCode32B" 
                        ValidationGroup="Commit"
                InitialValue="" 
                ErrorMessage="Prov % is Currency Code, Amount" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator 
                runat="server" Display="None"
                ID="RequiredFieldValidator11" 
                ControlToValidate="numAmount" 
                        ValidationGroup="Commit"
                InitialValue="" 
                ErrorMessage="Prov % is Currency Code, Amount" ForeColor="Red">
            </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="comboCurrencyCode32B" Runat="server"
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
                        <telerik:RadNumericTextBox ID="numAmount" runat="server" />
                    </td>
                </tr>
                
                <tr>    
                    <td style="width: 250px" class="MyLable">Percentage Credit Amount Tolerance</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox width="200" ID="numPercentCreditAmount1" runat="server" />
                        <telerik:RadNumericTextBox ID="numPercentCreditAmount2" runat="server" />
                    </td>
                </tr>
                
                <tr>    
                    <td style="width: 250px" class="MyLable">Maximum Credit Amount</td>
                    <td class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="comboMaximumCreditAmount39B" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="NOT EXCEEDING" Text="NOT EXCEEDING" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <uc1:VVTextBox runat="server" id="tbAdditionalAmountComment" LBWidth="250" VVTLabel="Additional Amounts Covered"  />
            <table width="100%" cellpadding="0" cellspacing="0">
                
                <tr>    
                    <td style="width: 250px" class="MyLable">Available With</td>
                    <td class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="comboAvailableWith" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="ASCBVNVX" Text="ASIA COMMERCIAL BANK" />
                                <telerik:RadComboBoxItem Value="CITIVNVX" Text="CITIBANK N.A." />
                                <telerik:RadComboBoxItem Value="HSBCVNVX" Text="HSBC BANK (VIETNAM) LTD." />
                                <telerik:RadComboBoxItem Value="CITIUS33" Text="CITIBANK N.A." />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td></td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Available With (Name and Addr.)</td>
                    <td class="MyContent">
                        <telerik:RadTextBox width="200" ID="tbAvailableWithNameAddress" runat="server" />
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">By</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="comboAvailableWithBy" Runat="server" AutoPostBack="True" OnSelectedIndexChanged="comboAvailableWithBy_OnSelectedIndexChanged"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="BY ACCEPTANCE" Text="BY ACCEPTANCE" />
                                <telerik:RadComboBoxItem Value="BY DEF PAYMENT" Text="BY DEF PAYMENT" />
                                <telerik:RadComboBoxItem Value="BY MIXED PYMT" Text="BY MIXED PYMT" />
                                <telerik:RadComboBoxItem Value="BY NEGOTIATION" Text="BY NEGOTIATION" />
                                <telerik:RadComboBoxItem Value="BY PAYMENT" Text="BY PAYMENT" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td hidden="hidden" ><telerik:RadTextBox ID="tbAvailableWithByName" runat="server" /></td>
                </tr>
            </table>
                    <uc1:VVTextBox runat="server" id="tb42CDraftsAt" LBWidth="250" VVTLabel="Drafts At"  />
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Drawee</td>
                    <td class="MyContent">
                         <telerik:RadComboBox AppendDataBoundItems="True" 
                    ID="combo42DDrawee" Runat="server"
                    MarkFirstMatch="True" Width="355" Height="150px" 
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                    
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Mixed Payment Details</td>
                    <td class="MyContent">
                        <telerik:RadTextBox width="200" ID="tbMixedPaymentDetails" runat="server"  />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Deferred Payment Details</td>
                    <td class="MyContent">
                        <telerik:RadTextBox width="200" ID="tbDeferredPaymentDetails" runat="server"  />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Patial Shipment</td>
                    <td class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="rcbPatialShipment" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="Allowed" Text="Y" />
                                <telerik:RadComboBoxItem Value="Not Allowed" Text="N" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Transhipment</td>
                    <td class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="rcbTranshipment" Runat="server" 
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="Allowed" Text="Y" />
                                <telerik:RadComboBoxItem Value="Not Allowed" Text="N" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Place of taking in charge...</td>
                    <td class="MyContent">
                        <telerik:RadTextBox width="400" ID="tbPlaceoftakingincharge" runat="server"  />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Port of loading...</td>
                    <td class="MyContent">
                        <telerik:RadTextBox width="400" ID="tbPortofloading" runat="server"  />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Port of Discharge...</td>
                    <td class="MyContent">
                        <telerik:RadTextBox width="400" ID="tbPortofDischarge" runat="server"  />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Place of final in distination</td>
                    <td class="MyContent">
                        <telerik:RadTextBox width="400" ID="tbPlaceoffinalindistination" runat="server"  />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Lates Date of Shipment</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker runat="server" ID="tbLatesDateofShipment">
                        </telerik:RadDatePicker>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Shipment Period</td>
                    <td class="MyContent">
                        <telerik:RadTextBox width="400" ID="tbShipmentPeriod" runat="server"  />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Descrp of Goods/Bervices</td>
                    <td class="MyContent" style="vertical-align:top;">
                       <asp:TextBox width="96%" TextMode="MultiLine" Height="100" ID="tbDescrpofGoods" runat="server">+ COMMODITY 	: HOT ROLLED STEEL COILS 
+ DESCRIPTION : 
. THICKNESS 	: FROM 3.00 MM UP 
. WIDTH 	: FROM 1,500 MM DOWN 
. COIL WEIGHT : FROM 0.1 MT UP 
+ QUANTITY 	: 55.00 MTS (+/-10PCT) 
+ UNIT PRICE 	: USD530.00/MT 
+ TOTAL AMOUNT : USD29,150.00 (+/-10PCT) 
+ TRADE TERMS : CFR HOCHIMINH CITY PORT, VIETNAM (INCOTERMS 2010) 
+ ORIGIN 	: EUROPEAN COMMUNITY 
+ PACKING 	: IN CONTAINERS </asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0" hidden="hidden" >
                <tr>    
                    <td style="width: 250px" class="MyLable">Docs Required</td>
                    <td class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="rcbDocsRequired" Runat="server" AutoPostBack="True" 
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="AIRB" Text="AIRB" />
                                <telerik:RadComboBoxItem Value="ANAL" Text="ANAL" />
                                <telerik:RadComboBoxItem Value="AWB" Text="AWB" />
                                <telerik:RadComboBoxItem Value="AWB1" Text="AWB1" />
                                <telerik:RadComboBoxItem Value="AWB2" Text="AWB2" />
                                <telerik:RadComboBoxItem Value="AWB3" Text="AWB3" />
                                <telerik:RadComboBoxItem Value="BEN" Text="BEN" />
                                <telerik:RadComboBoxItem Value="BENOP" Text="BENOP" />
                                <telerik:RadComboBoxItem Value="BL" Text="BL" />
                                <telerik:RadComboBoxItem Value="C.O" Text="C.O" />
                                <telerik:RadComboBoxItem Value="COMIN" Text="COMIN" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Docs required</td>
                   <td class="MyContent" style="vertical-align:top;">
                       <asp:TextBox width="96%" TextMode="MultiLine" Height="100" ID="tbOrderDocs" runat="server">1. SIGNED COMMERCIAL INVOICE IN 03 ORIGINALS ISSUED BY THE BENEFICIARY 
2. FULL (3/3) SET OF ORIGINAL CLEAN SHIPPED ON BOARD BILL OF LADING MADE OUT TO ORDER OF TANPHU BRANCH, NOTIFY APPLICANT AND MARKED FREIGHT PREPAID, SHOWING THE NAME AND ADDRESS OF SHIPPING AGENT WHICH IS LOCATED IN VIETNAM. 
3. QUANTITY AND QUALITY CERTIFICATE IN 01 ORIGINAL AND 02 COPIES ISSUED BY THE BENEFICIARY 
4. CERTIFICATE OF ORIGIN IN 01 ORIGINAL AND 02 COPIES ISSUED BY ANY CHAMBER OF COMMERCE IN EUROPEAN COMMUNITY CERTIFYING THAT THE GOODS ARE OF EUROPEAN COMMUNITY ORIGIN 
5. DETAILED PACKING LIST IN 03 ORIGINALS ISSUED BY THE BENEFICIARY</asp:TextBox>
                    </td>
                </tr>
            </table>
             <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Additional Conditions</td>
                    <td class="MyContent" style="vertical-align:top;">
                       <asp:TextBox width="96%" TextMode="MultiLine" Height="100" ID="tbAdditionalConditions" runat="server" >1. ALL REQUIRED DOCUMENTS AND ITS ATTACHED LIST (IF ANY) MUST BE SIGNED OR STAMPED BY ISSUER. 
2. ALL DRAFT(S) AND DOCUMENTS MUST BE MADE OUT IN ENGLISH. DOCUMENTS ISSUED IN ANY OTHER LANGUAGE THAN ENGLISH BUT WITH ENGLISH TRANSLATION ACCEPTABLE. PRE-PRINTED WORDING (IF ANY) ON DOCUMENTS MUST BE IN ENGLISH OR BILINGUAL BUT ONE OF ITS LANGUAGES MUST BE IN ENGLISH. 
3. ALL REQUIRED DOCUMENTS MUST INDICATE OUR L/C NUMBER. 
4. ALL REQUIRED DOCUMENTS MUST BE PRESENTED THROUGH BENEFICIARY'S BANK 
5. SHIPMENT MUST NOT BE EFFECTED BEFORE L/C ISSUANCE DATE. 
6. THE TIME OF RECEIVING AND HANDLING CREDIT DOCUMENTS AT ISSUING BANK ARE LIMITED FROM 7:30 AM TO 04:00 PM. DOCUMENTS ARRIVING AT OUR COUNTER AFTER 04:00 PM LOCAL TIME WILL BE CONSIDERED TO BE RECEIVED ON THE NEXT BANKING DAY. 
7. PLEASE BE INFORMED THAT SATURDAY IS CONSIDERED AS NON-BANKING BUSINESS DAY FOR OUR TRADE FINANCE PROCESSING/OPERATIONS UNIT ALTHOUGH OUR BANK MAY OTHERWISE BE OPENED FOR BUSINESS. 
8. THIRD PARTY DOCUMENTS ARE ACCEPTABLE.</asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Charges </td>
                    <td class="MyContent" style="vertical-align:top;">
                       <asp:TextBox width="96%" TextMode="MultiLine" Height="100" ID="tbCharges" runat="server">ALL BANKING CHARGES OUTSIDE VIETNAM 
PLUS ISSUING BANK'S HANDLING FEE 
ARE FOR ACCOUNT OF BENEFICIARY 
</asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Period for Presentation </td>
                    <td class="MyContent" style="vertical-align:top;">
                       <asp:TextBox width="96%" TextMode="MultiLine" Height="100" ID="tbPeriodforPresentation" runat="server">NOT EARLIER THAN 21 DAYS AFTER 
SHIPMENT DATE BUT WITHIN THE 
VALIDITY OF THIS L/C. </asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Confimation Instructions </td>
                    <td class="MyContent" style="vertical-align:top;">
                        <telerik:RadComboBox width="200"
                            ID="rcbConfimationInstructions" Runat="server" 
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="Without" Text="Without" />
                                <telerik:RadComboBoxItem Value="Confirm" Text="Confirm" />
                                <telerik:RadComboBoxItem Value="May add" Text="May add" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Instr to//Payg/Accptg/Negotg Bank</td>
                    <td class="MyContent" style="vertical-align:top;">
                       <asp:TextBox width="96%" TextMode="MultiLine" Height="100" ID="tbNegotgBank" runat="server">1. USD70.00 DISCREPANCY FEE WILL BE DEDUCTED FROM THE PROCEEDS FOR EACH DISCREPANT SET OF DOCUMENTS PRESENTED UNDER THIS L/C. THE RELATIVE TELEX EXPENSES USD25.00, IF ANY, WILL BE ALSO FOR THE ACCOUNT OF BENEFICIARY. 
2. EACH DRAWING MUST BE ENDORSED ON THE ORIGINAL L/C BY THE NEGOTIATING/PRESENTING BANK. 
3. PLEASE SEND ALL DOCS TO VIET VICTORY BANK AT PLOOR 9TH, NO.10 PHO QUANG STREET, TAN BINH DISTRICT, HOCHIMINH CITY, VIETNAM IN ONE LOT BY THE COURIER SERVICES. 
4. UPON RECEIPT OF DOCS REQUIRED IN COMPLIANCE WITH ALL TERMS AND CONDITIONS OF THE L/C, WE SHALL REMIT THE PROCEEDS TO YOU AS PER YOUR INSTRUCTIONS IN THE COVER LETTER.</asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Sender to Receiver Infomation</td>
                    <td class="MyContent" style="vertical-align:top;">
                       <asp:TextBox width="96%" TextMode="MultiLine" Height="100" ID="tbSendertoReceiverInfomation" runat="server">PLEASE ACKNOWLEDGE YOUR RECEIPT 
OF THIS L/C BY MT730.</asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div id="MT740" runat="server" class="dnnClear" visible="false">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                <ContentTemplate>
	        <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Generate MT40 YES/NO</td>
                    <td class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="comGenerate" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="YES" Text="YES" />
                                <telerik:RadComboBoxItem Value="NO" Text="NO" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                
                <tr>    
                    <td style="width: 250px" class="MyLable">Receiving Bank</td>
                    <td style="width: 200px;" class="MyContent">
                        <telerik:RadComboBox AutoPostBack="True" OnSelectedIndexChanged="comboReceivingBankMT740_OnSelectedIndexChanged"
                            ID="comboReceivingBankMT740" Runat="server" width="200"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
	                        <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="CITI BANK NEWYORK" Text="21447" />
                                <telerik:RadComboBoxItem Value="ASIA COMMERCIAL BANK" Text="21448" />
                                <telerik:RadComboBoxItem Value="HSBC BANK (VIETNAM) LTD." Text="21449" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
					<td>
                        <asp:Label ID="tbReceivingBankMT740Name" runat="server" />
					</td>
                </tr>
                
                <tr>    
                    <td style="width: 250px" class="MyLable" style="color: #d0d0d0">20 Documentary Credit Number</td>
                    <td class="MyContent">
                        <telerik:RadTextBox width="200" ID="tbDocumentaryCreditNumber" runat="server" Enabled="False"/>
                    </td>
                </tr>
              </table>
              
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">31D Date and Place of Expiry</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadDatePicker width="200" ID="dte31DDate" runat="server"></telerik:RadDatePicker>
                    </td>
                    <td><telerik:RadTextBox width="200" ID="tb31DPlaceOfExpiry" runat="server" /> </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Beneficial</td>
                    <td class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="comboBeneficial" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
                
                <tr>    
                    <td style="width: 250px" class="MyLable">Beneficial (Name and Addr.)</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadTextBox width="200" ID="tbBeneficialNameAddress" runat="server" />
                    </td>
                    <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>
              
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">32 Credit Amount</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadComboBox  width="200"
                            ID="comboCredit32USD" Runat="server" 
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
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
                    <td>
                        <telerik:RadNumericTextBox ID="numUSDAmount" runat="server" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">41A Available With</td>
                    <td class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="comboAvailableWith_MT740" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
	                        <Items>
                                <telerik:RadComboBoxItem Value="ASCBVNVX" Text="ASIA COMMERCIAL BANK" />
                                <telerik:RadComboBoxItem Value="CITIVNVX" Text="CITIBANK N.A." />
                                <telerik:RadComboBoxItem Value="HSBCVNVX" Text="HSBC BANK (VIETNAM) LTD." />
                                <telerik:RadComboBoxItem Value="CITIUS33" Text="CITIBANK N.A." />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
                
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Available (Name and Addr.)</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadTextBox width="200" ID="tbAvailableNameAddr_MT740" runat="server" />
                    </td>
                    <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>
                
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Draffy At</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadTextBox width="200" ID="tb42CDraff" runat="server" />
                    </td>
                    <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Drawee</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadComboBox AutoPostBack="True" OnSelectedIndexChanged="comboDrawee42D_MT740_OnSelectedIndexChanged"
                            ID="comboDrawee42D_MT740" Runat="server" width="200"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
	                        <Items>
                                <telerik:RadComboBoxItem Value="SGTTVNVX" Text="1" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td><telerik:RadTextBox ID="tbDraweeName42D_MT740" runat="server" Enabled="False"/>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Name Addr.</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadTextBox width="200" ID="tbNameAddress" runat="server" />
                    </td>
                    <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Bank Changes</td>
                    <td class="MyContent">
                        <telerik:RadComboBox width="200"
                            ID="comboBankChange" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td style="width: 250px" class="MyLable">Sender to receiver information</td>
                    <td style="width: 200px" class="MyContent">
                        <telerik:RadTextBox width="200" ID="tbSenderReceiverInformation" runat="server" />
                    </td>
                    <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>
                    </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="Charges" class="dnnClear">
            <uc3:NormalLCChargeAmend ID="NormalLCChargeAmend" runat="server" Visible="false" />
            <uc3:NormalLCChargeAuthorize ID="NormalLCChargeAuthorize" runat="server" Visible="false" />
            <uc3:NormalLCChargeCancel ID="NormalLCChargeCancel" runat="server" Visible="false" />
            <uc3:NormalLCChargeIssue ID="NormalLCChargeIssue" runat="server" Visible="false" />
        </div>

        <div id="DeliveryAudit" class="dnnClear">
            <%--<asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                <ContentTemplate>--%>
        <fieldset>
                      <legend>
                      <div style="font-weight:bold;text-transform:uppercase;">Delivery Details</div></legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Active Code</td>
                    <td class="MyContent">
                    </td>
                </tr>
                </table>
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Messeger Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                    ID="rcbMessengerType" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                </tr>
              </table>
                    <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Messeger Class</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                    ID="rcbMessengerClass" Runat="server" 
                    MarkFirstMatch="True" Width="200"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                </tr>
              </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Override Carrier</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                    ID="rcbOverrideCarrier" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                </tr>
              </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Altermate Addr.</td>
                    <td class="MyContent">
                        <asp:TextBox ID="tbAltermateAddr" runat="server" Width="300" />
                    </td>
                </tr>
              </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Send Messager(Y/N)</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                    ID="rcbSendMessager" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
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
                      <legend>
                      <div style="font-weight:bold;text-transform:uppercase;">Audit Details</div></legend>
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Override</td>
                    <td class="MyContent">
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Record Status</td>
                    <td class="MyContent">
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Current No</td>
                    <td class="MyContent">
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Inputter</td>
                    <td class="MyContent">
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Date : Time</td>
                    <td class="MyContent">
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Department Code</td>
                    <td class="MyContent">
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Company Code</td>
                    <td class="MyContent">
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Delivery Ref</td>
                    <td class="MyContent">
                    </td>
                </tr>
              </table>
            </fieldset>
                    <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
  </div>
 </telerik:RadCodeBlock>
  <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbLCType">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblLCType" />
            </UpdatedControls>
        </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="ntSoTien">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblPaymentpCt" />
                <telerik:AjaxUpdatedControl ControlID="lblPaymentPortion" />
                <telerik:AjaxUpdatedControl ControlID="lblPayType" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcCommodity">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblCommodity" />
            </UpdatedControls>
        </telerik:AjaxSetting>
     
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadInputManager ID="RadInputManager1" runat="server">
        <telerik:DateInputSetting BehaviorID="DateInputBehavior2" DateFormat="dd MMM yyyy">
            <TargetControls>
                <telerik:TargetInput ControlID="tbIssuingDate"></telerik:TargetInput>
                <telerik:TargetInput ControlID="tbExpiryDate"></telerik:TargetInput>
                <telerik:TargetInput ControlID="tbContingentExpiry"></telerik:TargetInput>
            </TargetControls>
        </telerik:DateInputSetting>
    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior1"
            ValidationExpression="[0-9]{5}.[0-9]{2}" ErrorMessage="">
            <TargetControls>
                <telerik:TargetInput ControlID="tbLimitRef"></telerik:TargetInput>
            </TargetControls>
        </telerik:RegExpTextBoxSetting>
    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior2"
            ValidationExpression="[0-9]{1,2}" ErrorMessage="">
            <TargetControls>
                <telerik:TargetInput ControlID="tbProv"></telerik:TargetInput>
            </TargetControls>
        </telerik:RegExpTextBoxSetting>
    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior3"
            ValidationExpression="[0-9]{1,2}" ErrorMessage="">
            <TargetControls>
                <telerik:TargetInput ControlID="tbChargePeriod"></telerik:TargetInput>
            </TargetControls>
        </telerik:RegExpTextBoxSetting>
    </telerik:RadInputManager>
<div style="visibility:hidden;">
<asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" />
    </div>
<asp:HiddenField ID="hdfDisable" runat="server" Value="0" />
  <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
  <script type="text/javascript">
      function SetValue() {
          var rcbApplicantID = document.getElementById("<%=rcbApplicantID.ClientID%>");
          $("#spCustomerID").html(rcbApplicantID.value);
      }
      function ChargecodeChange(loai) {

          if (loai == 1) {
              var code = $("#<%=tbChargeCode.ClientID%>").val();
              if (code.toUpperCase() == "TFILCOPEN") {
                  $("#spChargeCode").text("LC Open Comm");
              }

          }
          else {
              var code = $("#<%=tbChargecode2.ClientID%>").val();
              if (code.toUpperCase() == "TFILCOPEN") {
                  $("#spChargeCode2").text("LC Open Comm");
              }

          }


      }
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
                //$(this).attr('name', 'row' + thisRow + thisName);
                //$(this).attr('id', 'row' + thisRow + thisName);
            });
        
    });
      $("#<%=tbEssurLCCode.ClientID %>").keyup(function (event) {
         
          if (event.keyCode == 13) {
              window.location.href = "Default.aspx?tabid=92&CodeID=" + $("#<%=tbEssurLCCode.ClientID %>").val();
            }
      });
  </script>
      <script type="text/javascript">
          function tbExpiryDate_DateSelected(sender, eventArgs) {
              var datePicker = $find("<%= tbContingentExpiry.ClientID %>");
              var ExpiryDate = $find("<%= tbExpiryDate.ClientID %>");
              var PlaceOfExpiry = $find("<%= dteMT700DateAndPlaceOfExpiry.ClientID %>");
              
              var date = ExpiryDate.get_selectedDate();
              var dateP = ExpiryDate.get_selectedDate();
               
              date.setDate(date.getDate() + 15);
              //alert(date);
              datePicker.set_selectedDate(date);
              PlaceOfExpiry.set_selectedDate(dateP);
             // alert("The date was just changed from " +
             //eventArgs.get_oldValue() + " to " + eventArgs.get_newValue());
          }
          function tbIssuingDate_DateSelected(sender, eventArgs) {
              var datePicker = $find("<%= dteDateOfIssue.ClientID %>");
              var ExpiryDate = $find("<%= tbIssuingDate.ClientID %>");
              var date = ExpiryDate.get_selectedDate();

              //date.setDate(date.getDate());
              //alert(date);
              datePicker.set_selectedDate(date);
              // alert("The date was just changed from " +
              //eventArgs.get_oldValue() + " to " + eventArgs.get_newValue());
          }

          function rcbExpiryPlace_OnClientSelectedIndexChanged(sender, eventArgs) {
              var combo = $find('<%=tbExpiryPlace.ClientID %>');
              //sender.set_text("You selected " + item.get_text());
              var txtDate = $find("<%= tbPlaceOfExpiry.ClientID %>");
              txtDate.set_value(combo.get_value());
          }

          function rcbCcyAmount_OnClientSelectedIndexChanged(sender, eventArgs) {
              $find("<%= comboCurrencyCode32B.ClientID %>").set_text($find('<%=rcbCcyAmount.ClientID %>').get_selectedItem().get_value());
          }

          function ntSoTien_OnValueChanged(sender, eventArgs) {
              $find("<%= numAmount.ClientID %>").set_value($find('<%=ntSoTien.ClientID %>').get_value());
          }

          function tbcrTolerance_TextChanged(sender, eventArgs) {
              $find("<%= numPercentCreditAmount1.ClientID %>").set_value($find('<%=tbcrTolerance.ClientID %>').get_value());
          }

          function tbdrTolerance_TextChanged(sender, eventArgs) {
              $find("<%= numPercentCreditAmount2.ClientID %>").set_value($find('<%=tbdrTolerance.ClientID %>').get_value());
          }

          function tbBeneficiaryNameAddr_OnBlur(textbox, tbName) {
              var txtDate = $find("<%= tbDocumentary_NameAndAddress.ClientID %>");
              txtDate.set_value($(textbox).val());
              
          }

          function AdviseBank_OnClientSelectedIndexChanged() {
              $find("<%= comboRevivingBank.ClientID %>").set_text($find("<%=rcbAdviseBankNo.ClientID %>").get_selectedItem().get_text());
              $find("<%= comboRevivingBank.ClientID %>").set_value($find("<%=rcbAdviseBankNo.ClientID %>").get_value());
          }
</script>
  </telerik:RadCodeBlock>

</div>


        <div id="Charges-old" class="dnnClear" style="visibility:hidden">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                <ContentTemplate>
            <fieldset>
              <legend>
              <div style="font-weight:bold;text-transform:uppercase;">Charge Details</div></legend>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox runat="server" ID="chkWaiveCharges" CssClass="TbHoa"  AutoPostBack="true" Text="Waive Charges ?" OnCheckedChanged="OnWaiveChargesChange" />
                        </td>
                    </tr>
                <tr>
                    <td class="MyLable">Waive Charges</td>
                    <td class="MyContent">
                        <asp:TextBox ID="tbWaiveCharges" CssClass="TbHoa" AutoPostBack="true" OnTextChanged="tbWaiveCharges_TextChanged" runat="server" Width="150" />
                    </td>
                </tr>
                    </table>
                <div id="tableANHien" runat="server">
              <table width="100%" cellpadding="0" cellspacing="0" >
                    <tr>
                    <td class="MyLable">Charge code</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                    ID="tbChargeCode" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                        <telerik:RadComboBoxItem Value="TFILCOPEN" Text="TFILCOPEN" />
                        <telerik:RadComboBoxItem Value="TFILCISU" Text="TFILCISU" />
                        <telerik:RadComboBoxItem Value="TFILCISS" Text="TFILCISS" />
                        <telerik:RadComboBoxItem Value="TFIAAMORT" Text="TFIAAMORT" />
                        <telerik:RadComboBoxItem Value="TFIAMEND" Text="TFIAMEND" />
                        <telerik:RadComboBoxItem Value="TFSWISSU" Text="TFSWISSU" />
                    </Items>
                </telerik:RadComboBox>
                        <%--<asp:TextBox ID="tbChargeCode" runat="server" Text="TFILCOPEN" CssClass="TbHoa" Width="150"  OnBlur="ChargecodeChange(1);" />--%>
                        <asp:ImageButton ID="btThem" runat="server" OnClick="btThem_Click" ImageUrl="~/Icons/Sigma/Add_16X16_Standard.png" />
                        <span id="spChargeCode">LC Open Comm</span>
                    </td>
                </tr>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0" id="table1" runat="server">
                    <tr>
                    <td class="MyLable">Charge Acct</td>
                    <td class="MyContent" width="150">
                        <telerik:RadComboBox AutoPostBack="true" 
                    OnSelectedIndexChanged="rcbChargeAcct_SelectIndexChange"
                    ID="rcbChargeAcct" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="CTY TNHH SONG HONG" Text="03.000237869.4" />
                            <telerik:RadComboBoxItem Value="CTY TNHH PHAT TRIEN PHAN MEM ABC" Text="03.000237870.4" />
                    </Items>
                </telerik:RadComboBox>
                        <%--<asp:TextBox ID="tbChargeAcct" runat="server" CssClass="TbHoa" Width="150" AutoPostBack="true"  OnTextChanged="tbChargeAcct_TextChanged" />--%>
                    </td>
                        <td><asp:Label ID="lblChargeAcct" runat="server" /></td>
                </tr>
                     </table>
                        <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                    <td class="MyLable">Charge Period</td>
                    <td class="MyContent">
                        <asp:TextBox ID="tbChargePeriod" Text="1" runat="server" Width="100" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Charge Ccy</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                    ID="rcbChargeCcy" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
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
                    <td class="MyLable">Exch. Rate</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox incrementsettings-interceptarrowkeys="true" incrementsettings-interceptmousewheel="true"  runat="server" id="tbExcheRate" width="200px"/>
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Charge Amt</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox incrementsettings-interceptarrowkeys="true" incrementsettings-interceptmousewheel="true"  runat="server" id="tbChargeAmt" width="200px" AutoPostBack="true" 
                        OnTextChanged="tbChargeAmt_TextChanged"/>
                        
                    </td>
                </tr>
                            </table>
                    <table width="100%" cellpadding="0" cellspacing="0" >
                    <tr>
                    <td class="MyLable">Party Charged</td>
                    <td class="MyContent" style="width:150px;">
                        <telerik:RadComboBox AutoPostBack="true" 
                    OnSelectedIndexChanged="rcbPartyCharged_SelectIndexChange"
                    ID="rcbPartyCharged" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                        <telerik:RadComboBoxItem Value="BEN" Text="B" />
                            <telerik:RadComboBoxItem Value="BEN Charges for the Beneficiary" Text="BB" />
                            <telerik:RadComboBoxItem Value="Correspondent Charges for the Applicant" Text="AA" />
                            <telerik:RadComboBoxItem Value="Applicant" Text="A" />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                        <td><asp:Label ID="lblPartyCharged" runat="server" /></td>
                </tr>
                            </table>
                    <table width="100%" cellpadding="0" cellspacing="0" >
                    <tr>
                    <td class="MyLable">Amort Charges</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                    ID="rcbOmortCharge" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                         <telerik:RadComboBoxItem Value="YES" Text="YES" />
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Amt. In Local CCY</td>
                    <td class="MyContent">
                        
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Amt DR from Acct</td>
                    <td class="MyContent">
                        
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Charge Status</td>
                    <td class="MyContent" style="width:150px;">
                        <telerik:RadComboBox AutoPostBack="true" 
                    OnSelectedIndexChanged="rcbChargeStatus_SelectIndexChange"
                    ID="rcbChargeStatus" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                        <telerik:RadComboBoxItem Value="CHARGE COLECTED" Text="2" />
                        <telerik:RadComboBoxItem Value="CHARGE UNCOLECTED" Text="3" />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                        <td><asp:Label ID="lblChargeStatus" runat="server" /></td>
                </tr>
                            </table>
                    </div>
                <div id="divCharge2" runat="server" style="border-top:1px solid #CCC;">
              <table width="100%" cellpadding="0" cellspacing="0" id="table2" runat="server">
                    <tr>
                    <td class="MyLable">Charge code</td>
                    <td class="MyContent">
                        <%--<asp:TextBox ID="tbChargecode2" Text="LC Open Comm" runat="server" CssClass="TbHoa" Width="150"  OnBlur="ChargecodeChange(2);" />--%>
                        <telerik:RadComboBox 
                    ID="tbChargecode2" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                        <telerik:RadComboBoxItem Value="TFILCOPEN" Text="TFILCOPEN" />
                        <telerik:RadComboBoxItem Value="TFILCISU" Text="TFILCISU" />
                        <telerik:RadComboBoxItem Value="TFILCISS" Text="TFILCISS" />
                        <telerik:RadComboBoxItem Value="TFIAAMORT" Text="TFIAAMORT" />
                        <telerik:RadComboBoxItem Value="TFIAMEND" Text="TFIAMEND" />
                        <telerik:RadComboBoxItem Value="TFSWISSU" Text="TFSWISSU" />
                    </Items>
                </telerik:RadComboBox>
                        <span id="spChargeCode2"></span>
                    </td>
                </tr>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0" id="table3" runat="server">
                    <tr>
                    <td class="MyLable">Charge Acct</td>
                    <td class="MyContent" width="150">
                        <%--<asp:TextBox ID="tbChargeAcct2" runat="server" CssClass="TbHoa" Width="150" AutoPostBack="true"  OnTextChanged="tbChargeAcct2_TextChanged" />--%>
                         <telerik:RadComboBox AutoPostBack="true" 
                    OnSelectedIndexChanged="rcbChargeAcct2_SelectIndexChange"
                    ID="rcbChargeAcct2" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                     <telerik:RadComboBoxItem Value="CTY TNHH SONG HONG" Text="03.000237869.4" />
                            <telerik:RadComboBoxItem Value="CTY TNHH PHAT TRIEN PHAN MEM ABC" Text="03.000237870.4" />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                        <td><asp:Label ID="lblChargeAcct2" runat="server" /></td>
                </tr>
                     </table>
                        <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                    <td class="MyLable">Charge Period</td>
                    <td class="MyContent">
                        <asp:TextBox ID="tbChargePeriod2" Text="1" runat="server" Width="100" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Charge Ccy</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                    ID="rcbChargeCcy2" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
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
                    <td class="MyLable">Exch. Rate</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox incrementsettings-interceptarrowkeys="true" incrementsettings-interceptmousewheel="true"  runat="server"
                             id="tbExcheRate2" width="200px"/>
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Charge Amt</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox incrementsettings-interceptarrowkeys="true" incrementsettings-interceptmousewheel="true"  runat="server" 
                            id="tbChargeAmt2" width="200px" AutoPostBack="true" 
                        OnTextChanged="tbChargeAmt2_TextChanged"/>
                        
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Party Charged</td>
                    <td class="MyContent" style="width:150px;">
                        <telerik:RadComboBox AutoPostBack="true" 
                    OnSelectedIndexChanged="rcbPartyCharged2_SelectIndexChange"
                    ID="rcbPartyCharged2" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                         <telerik:RadComboBoxItem Value="BEN" Text="B" />
                            <telerik:RadComboBoxItem Value="BEN Charges for the Beneficiary" Text="BB" />
                            <telerik:RadComboBoxItem Value="Correspondent Charges for the Applicant" Text="AA" />
                            <telerik:RadComboBoxItem Value="Applicant" Text="A" />
                    </Items>
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
                    ID="rcbOmortCharges2" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                         <telerik:RadComboBoxItem Value="YES" Text="YES" />
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Amt. In Local CCY</td>
                    <td class="MyContent">
                        
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Amt DR from Acct</td>
                    <td class="MyContent">
                        
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Charge Status</td>
                    <td class="MyContent" style="width:150px;">
                        <telerik:RadComboBox AutoPostBack="true" 
                    OnSelectedIndexChanged="rcbChargeStatus2_SelectIndexChange"
                    ID="rcbChargeStatus2" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                         <telerik:RadComboBoxItem Value="CHARGE COLECTED" Text="2" />
                        <telerik:RadComboBoxItem Value="CHARGE UNCOLECTED" Text="3" />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                        <td><asp:Label ID="lblChargeStatus2" runat="server" /></td>
                </tr>
                            </table>
                    </div>
                <table width="100%" cellpadding="0" cellspacing="0"   style="border-bottom:1px solid #CCC;">
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
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                    <td class="MyLable">Tax Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblTaxCode" runat="server" />
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Tax Ccy</td>
                    <td class="MyContent">
                        <asp:Label ID="lblTaxCcy" runat="server" />
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
                    <td class="MyContent">
                        
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Tax Date</td>
                    <td class="MyContent">
                        
                    </td>
                 </tr>
                </table>
                <div id="divChargeInfo2" runat="server" style="border-top:1px solid #CCC;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                    <td class="MyLable">Tax Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblTaxCode2" runat="server" />
                    </td>
                </tr>
                    <tr>
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
                    <tr>
                    <td class="MyLable">Tax in LCCY Amt</td>
                    <td class="MyContent">
                        
                    </td>
                </tr>
                    <tr>
                    <td class="MyLable">Tax Date</td>
                    <td class="MyContent">
                        
                    </td>
                 </tr>
                </table>
                </div>
            </fieldset>
                    </ContentTemplate>
            </asp:UpdatePanel>
        </div>
