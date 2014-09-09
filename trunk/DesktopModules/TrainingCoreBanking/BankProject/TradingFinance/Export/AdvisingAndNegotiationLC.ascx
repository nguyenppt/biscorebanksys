<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisingAndNegotiationLC.ascx.cs" Inherits="BankProject.AdvisingAndNegotiationLC" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
    function OnClientButtonClicking(sender, args) {
       <%-- var button = args.get_item();
        if (button.get_commandName() == "doclines") {
            var d = new Date();

            var month = d.getMonth() + 1;
            var day = d.getDate();

            var output = (day < 10 ? '0' : '') + day + '/' +
                (month < 10 ? '0' : '') + month + '/' +
                 d.getFullYear();
            output = output + " " + $find('<%= rcbChargeAcct.ClientID %>').get_selectedItem().get_text() + " Overdraf On \n Available Blance VND - 55497272";
            var chuoithongbao = output.toString();
            args.set_cancel(!confirm(chuoithongbao));
        }
    --%>
    }
</script> 
<asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit"/>

<div>
    

  <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
     <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
         OnClientButtonClicking="OnClientButtonClicking" OnButtonClick="RadToolBar1_ButtonClick">
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
    <table width="100%" cellpadding="0" cellspacing="0">
<tr>
    <td style="width:200px; padding:5px 0 5px 20px;"><asp:TextBox ID="tbEssurLCCode" runat="server" Width="200" /></td>
</tr>
</table>
    <div class="dnnForm" id="tabs-demo">
        <ul class="dnnAdminTabNav">
            <li><a href="#Main">Main</a></li>
           <!-- <li><a href="#MT710">MT700</a></li>
            <li><a href="#MT730">MT740</a></li>-->
            <li><a href="#Charges">Charges</a></li>
            <!--<li><a href="#DeliveryAudit">Delivery Audit</a></li>
            <li><a href="#FullView">Full View</a></li>
            -->
        </ul>
        <div id="Main" class="dnnClear">
            <fieldset id="fAmendment" runat="server">
         <legend>
              <div style="font-weight:bold; text-transform:uppercase;"><asp:Label ID="titleAmend_Confirm" runat="server" Text="Amendments"></asp:Label></div>
                                </legend>
                 <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Generate Delivery</td>
                    <td class="MyContent"><asp:TextBox ID="tbGenerateDelivery" runat="server" Width="100" /></td>
                </tr>
                   
            </table>
                </fieldset>
    <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                    <td class="MyLable">LC Type <span class="Required">(*)</span></td>
                    <td class="MyContent" style="width:250px;">
                        <telerik:RadComboBox  AppendDataBoundItems="True" DropDownCssClass="KDDL"
                        ID="rcbLCType" Runat="server"  width="250"
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
                    <td>
                        <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="rcbLCType"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="LC Type is Required"
                        ForeColor="Red">
                        </asp:RequiredFieldValidator>

                    </td>
                  <td><asp:Label ID="lblLCType" runat="server" /></td>
                </tr>
                 </table>
    <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                    <td class="MyLable">LC Number</td>
                    <td class="MyContent"">
                        <telerik:radnumerictextbox incrementsettings-interceptarrowkeys="true" incrementsettings-interceptmousewheel="true"  runat="server" id="tbLCNumber" width="200px" AutoPostBack="false" 
                        />
                    </td>
                </tr>
                 </table>

    <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Beneficiary Cust.No</td>
                    <td class="MyContent">
                    <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td  Width="150"><telerik:RadComboBox AppendDataBoundItems="True"   AutoPostBack="true" 
                    OnSelectedIndexChanged="rcbBeneficiaryCustNo_SelectIndexChange"
                    ID="rcbBeneficiaryCustNo" Runat="server"
                    MarkFirstMatch="True" Width="300" Height="150px" 
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
                 </table>

    <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Beneficiary Addr.</td>
                    <td Width="300" class="MyContent"><asp:TextBox ID="tbBeneficiaryAddr" runat="server" Width="300" /></td>
                    <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>

    <table id="tbAppAddr" runat="server" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Beneficiary Addr.</td>
                    <td  class="MyContent"><asp:TextBox ID="tbBeneficiaryAddr2" runat="server" Width="300" Text="AP TRUNG BINH II, VINH TRACH" /><a class="remove"><img src="Icons/Sigma/Delete_16X16_Standard.png"></a></td>
                </tr>
                <tr>
                    <td class="MyLable">Beneficiary Addr. </td>
                    <td  class="MyContent"><asp:TextBox ID="tbBeneficiaryAddr3" runat="server" Width="300" Text="THOAI SON" /><a class="remove"><img src="Icons/Sigma/Delete_16X16_Standard.png"></a></td>
                </tr>
            </table>

    <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Beneficiary Acct</td>
                    <td class="MyContent" style="width:150px;"><telerik:RadComboBox 
                    ID="rcbBeneficiaryAcct" Runat="server" 
                    MarkFirstMatch="True" Width="300"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="ASCBVNVX" Text="ASCBVNVX - ASIA COMMERCIAL BANK" />
                            <telerik:RadComboBoxItem Value="CITIVNVX" Text="CITIVNVX - CITIBANK N.A." />
                            <telerik:RadComboBoxItem Value="HSBCVNVX" Text="HSBCVNVX - HSBC BANK (VIETNAM) LTD." />
                            <telerik:RadComboBoxItem Value="CITIUS33" Text="CITIUS33 - CITIBANK N.A." />
                            <telerik:RadComboBoxItem Value="BOFAUS6H" Text="BOFAUS6H - BANK OF AMERICA, N.A." />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="Label2" runat="server" /></td>
                </tr>
                     </table>

     <fieldset>
         <legend>
              <div style="font-weight:bold; text-transform:uppercase;">Issuing/Application/Other Inf</div>
                                </legend>
         <div>
                <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Issuing Bank No.</td>
                    <td class="MyContent" style="width:150px;"><telerik:RadComboBox 
                    ID="rcbIssuingBankNo" Runat="server" 
                    MarkFirstMatch="True" Width="280"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="ASCBVNVX" Text="ASCBVNVX - ASIA COMMERCIAL BANK" />
                            <telerik:RadComboBoxItem Value="CITIVNVX" Text="CITIVNVX - CITIBANK N.A." />
                            <telerik:RadComboBoxItem Value="HSBCVNVX" Text="HSBCVNVX - HSBC BANK (VIETNAM) LTD." />
                            <telerik:RadComboBoxItem Value="CITIUS33" Text="CITIUS33 - CITIBANK N.A." />
                            <telerik:RadComboBoxItem Value="BOFAUS6H" Text="BOFAUS6H - BANK OF AMERICA, N.A." />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="Label1" runat="server" /></td>
                </tr>
                     </table>
            <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Iss. Bank Addr.</td>
                    <td  class="MyContent"><asp:TextBox type="text" runat="server" id="tbIssuingBankAddr" Width="280" /><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></td>

                </tr>
            </table>

                    
                    <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Iss. Bank Acct</td>
                    <td class="MyContent" style="width:150px;"><telerik:RadComboBox 
                    ID="rcbIssBankAcct" Runat="server" 
                    MarkFirstMatch="True" Width="280"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="ASCBVNVX" Text="ASCBVNVX - ASIA COMMERCIAL BANK" />
                            <telerik:RadComboBoxItem Value="CITIVNVX" Text="CITIVNVX - CITIBANK N.A." />
                            <telerik:RadComboBoxItem Value="HSBCVNVX" Text="HSBCVNVX - HSBC BANK (VIETNAM) LTD." />
                            <telerik:RadComboBoxItem Value="CITIUS33" Text="CITIUS33 - CITIBANK N.A." />
                            <telerik:RadComboBoxItem Value="BOFAUS6H" Text="BOFAUS6H - BANK OF AMERICA, N.A." />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="Label3" runat="server" /></td>
                </tr>
                     </table>
                    
                    <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Applicant No.</td>
                    <td class="MyContent" style="width:150px;"><telerik:RadComboBox 
                    ID="rcbApplicantNo" Runat="server" 
                    MarkFirstMatch="True" Width="280"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="ASCBVNVX" Text="ASCBVNVX - ASIA COMMERCIAL BANK" />
                            <telerik:RadComboBoxItem Value="CITIVNVX" Text="CITIVNVX - CITIBANK N.A." />
                            <telerik:RadComboBoxItem Value="HSBCVNVX" Text="HSBCVNVX - HSBC BANK (VIETNAM) LTD." />
                            <telerik:RadComboBoxItem Value="CITIUS33" Text="CITIUS33 - CITIBANK N.A." />
                            <telerik:RadComboBoxItem Value="BOFAUS6H" Text="BOFAUS6H - BANK OF AMERICA, N.A." />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="Label4" runat="server" /></td>
                </tr>
                     </table>
                <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Applicant Addr.</td>
                    <td  class="MyContent"><asp:TextBox type="text" runat="server" id="tbApplicantAddr" Width="280"/><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></td>
                </tr>
            </table>
                <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Applicant Bank</td>
                    <td  class="MyContent"><asp:TextBox type="text" runat="server" id="tbApplicantBank" Width="280"/><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></td>
                </tr>
            </table>

                    <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Reimb. Bank Ref.</td>
                    <td class="MyContent"><asp:TextBox ID="tbReimbBankRef" runat="server" Width="280" />
                    </td>
                </tr>
                    </table>

<table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Reimb. Bank No</td>
                    <td class="MyContent" style="width:150px;"><telerik:RadComboBox
                    ID="rcbReimbBankNo" Runat="server" 
                    MarkFirstMatch="True" Width="280"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="ASCBVNVX" Text="ASCBVNVX - ASIA COMMERCIAL BANK" />
                            <telerik:RadComboBoxItem Value="CITIVNVX" Text="CITIVNVX - CITIBANK N.A." />
                            <telerik:RadComboBoxItem Value="HSBCVNVX" Text="HSBCVNVX - HSBC BANK (VIETNAM) LTD." />
                            <telerik:RadComboBoxItem Value="CITIUS33" Text="CITIUS33 - CITIBANK N.A." />
                            <telerik:RadComboBoxItem Value="BOFAUS6H" Text="BOFAUS6H - BANK OF AMERICA, N.A." />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="lblReimbBankNo" runat="server" /></td>
                </tr>
                </table>

                <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Reimb. Bank Addr.</td>
                    <td  class="MyContent"><asp:TextBox type="text" runat="server" id="tbReimbBankAddr" Width="280"/><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></td>
                </tr>
            </table>

                    <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Advice Thru. Bank</td>
                    <td class="MyContent" style="width:150px;"><telerik:RadComboBox
                    ID="rcbAdviceThruBank" Runat="server" 
                    MarkFirstMatch="True" Width="280"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="ASCBVNVX" Text="ASCBVNVX - ASIA COMMERCIAL BANK" />
                            <telerik:RadComboBoxItem Value="CITIVNVX" Text="CITIVNVX - CITIBANK N.A." />
                            <telerik:RadComboBoxItem Value="HSBCVNVX" Text="HSBCVNVX - HSBC BANK (VIETNAM) LTD." />
                            <telerik:RadComboBoxItem Value="CITIUS33" Text="CITIUS33 - CITIBANK N.A." />
                            <telerik:RadComboBoxItem Value="BOFAUS6H" Text="BOFAUS6H - BANK OF AMERICA, N.A." />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="Label5" runat="server" /></td>
                </tr>
                </table>

                <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Adv. Thru. Addr.</td>
                    <td  class="MyContent"><asp:TextBox type="text" runat="server" id="tbAdvThruAddr" Width="280"/><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></td>
                </tr>
            </table>

                    <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Available with Custno</td>
                    <td class="MyContent" style="width:150px;"><telerik:RadComboBox
                    ID="rcbAvailableWithCustno" Runat="server" 
                    MarkFirstMatch="True" Width="280"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="ASCBVNVX" Text="ASCBVNVX - ASIA COMMERCIAL BANK" />
                            <telerik:RadComboBoxItem Value="CITIVNVX" Text="CITIVNVX - CITIBANK N.A." />
                            <telerik:RadComboBoxItem Value="HSBCVNVX" Text="HSBCVNVX - HSBC BANK (VIETNAM) LTD." />
                            <telerik:RadComboBoxItem Value="CITIUS33" Text="CITIUS33 - CITIBANK N.A." />
                            <telerik:RadComboBoxItem Value="BOFAUS6H" Text="BOFAUS6H - BANK OF AMERICA, N.A." />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                    <td><asp:Label ID="Label6" runat="server" /></td>
                </tr>
                </table>

                <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Available with Addr.</td>
                    <td  class="MyContent"><asp:TextBox type="text" runat="server" id="tbAvailableWithAddr" Width="280"/><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></td>
                </tr>
            </table>
                </div>


     </fieldset>
    <fieldset>
        <legend>
              <div style="font-weight:bold; text-transform:uppercase;">LC Details</div></legend>

         <table width="100%" cellpadding="0" cellspacing="0">

             <tr>
                    <td class="MyLable">Currency <span class="Required">(*)</span></td>
                    <td class="MyContent"><telerik:RadComboBox 
                    ID="rcbCurrency" Runat="server" 
                    MarkFirstMatch="True" Width="100"
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
                        <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldrcbCurrency"
                        ControlToValidate="rcbCurrency"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Currency is Required"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>

             <tr>
                    <td class="MyLable">Amount <span class="Required">(*)</span></td>
                    <td class="MyContent"><asp:TextBox id="tbAmount" runat="server" AutoPostBack="true" OnTextChanged="tbAmount_TextChanged" ></asp:TextBox>
                    </td>
                        <td>
                        <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator3"
                        ControlToValidate="tbAmount"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Amount is Required"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                        </td>
                </tr>
             <tr>
                    <td class="MyLable">Tolerance +/%</td>
                    <td class="MyContent"><telerik:RadNumericTextBox id="tbToleranceIncrease" Width="100" runat="server" ></telerik:RadNumericTextBox>
                        </td>
                </tr>

              <tr>
                    <td class="MyLable">Tolerance -/%</td>
                    <td class="MyContent"><telerik:RadNumericTextBox id="tbToleranceDecrease" Width="100" runat="server" ></telerik:RadNumericTextBox>
                        </td>
                </tr>

             <tr>
                    <td class="MyLable">Issuing Date</td>
                    <td class="MyContent">
                    <telerik:RadDatePicker runat="server" ID="tbIssuingDate">
                        </telerik:RadDatePicker>
                    </td>
                </tr>

             <tr>
                    <td class="MyLable">Expiry Date</td>
                    <td class="MyContent">
                    <telerik:RadDatePicker runat="server" ID="tbExpiryDate">
                        </telerik:RadDatePicker>
                    </td>
                </tr>


             <tr>
                    <td class="MyLable">Expiry Place</td>
                    <td class="MyContent">
                    <telerik:RadComboBox 
                    ID="rcbExpiryPlace" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                        <telerik:RadComboBoxItem Value="USA" Text="USA" />
                        <telerik:RadComboBoxItem Value="UK" Text="UK" />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                </tr>

             </table>
                <table width="100%" cellpadding="0" cellspacing="0"
                <tr>
                    <td class="MyLable">Contingent Expiry Date</td>
                    <td Width="150" class="MyContent">
                        <telerik:RadDatePicker runat="server" ID="tbContingentExpiryDate">
                        </telerik:RadDatePicker>
                    </td>
                    <td>30 Archve Date(Sys.Field)</td>
                </tr>
                </table>
         <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Commodity <span class="Required">(*)</span></td>
                    <td class="MyContent"><telerik:RadComboBox DropDownCssClass="KDDL"
                        ID="rcbCommodity" Runat="server"  width="280"  AppendDataBoundItems="True"
                        MarkFirstMatch="True"  OnItemDataBound="rcCommodity_ItemDataBound"
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
                                <%# DataBinder.Eval(Container.DataItem, "Name")%> 
                             </td> 
                          </tr> 
                       </table> 
               </ItemTemplate>
                    </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldrcbCommodity"
                        ControlToValidate="rcbCommodity"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Commodity is Required"
                        ForeColor="Red">
                        </asp:RequiredFieldValidator>

                    </td>
                    <td><asp:Label ID="lblCommodity" runat="server" /></td>
                </tr>
                    </table>
    </fieldset>
</div>
        <div id="Charges" class="dnnClear">
            <fieldset>
              <legend>
              <div style="font-weight:bold;text-transform:uppercase;">Charge Details</div></legend>
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Waive Charges</td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbWaiveCharges"
                            OnSelectedIndexChanged="tbWaiveCharges_TextChanged"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                            AutoPostBack="true"
                            with="150"
                        runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="YES" Text="YES" />
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                        </Items>
                    </telerik:RadComboBox>
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
                        <asp:ImageButton ID="btThem" runat="server" Visible="false" OnClick="btThem_Click" ImageUrl="~/Icons/Sigma/Add_16X16_Standard.png" />
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
                        <telerik:radnumerictextbox incrementsettings-interceptarrowkeys="true" incrementsettings-interceptmousewheel="true" NumberFormat-GroupSeparator=","  NumberFormat-DecimalDigits="0" runat="server" id="tbExcheRate"  width="200px"/>
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
                        <telerik:RadComboBoxItem Value="Beneficiary" Text="B" />
                            <telerik:RadComboBoxItem Value="Correspondent Charges for the Beneficiary" Text="CB" />
                            <telerik:RadComboBoxItem Value="Correspondent Charges for the Openner" Text="CO" />
                            <telerik:RadComboBoxItem Value="Openner" Text="O" />
                            <telerik:RadComboBoxItem Value="Third Party" Text="T" />
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
                         <telerik:RadComboBoxItem Value="Beneficiary" Text="B" />
                            <telerik:RadComboBoxItem Value="Correspondent Charges for the Beneficiary" Text="CB" />
                            <telerik:RadComboBoxItem Value="Correspondent Charges for the Openner" Text="CO" />
                            <telerik:RadComboBoxItem Value="Openner" Text="O" />
                            <telerik:RadComboBoxItem Value="Third Party" Text="T" />
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
        </div>
    </div>
 </telerik:RadCodeBlock>
  </div>

  <%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbBeneficiaryCustNo">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblCustomer" />
                <telerik:AjaxUpdatedControl ControlID="tbAppAddr" />
                <telerik:AjaxUpdatedControl ControlID="tbApplicantAddr" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tbContingentExpiry">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="tbContingentExpiry" />
            </UpdatedControls>
        </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="rcbAvailWithNo">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblAvailWithNo" />
                <telerik:AjaxUpdatedControl ControlID="tbAvailWithNameAddr" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcCommodity">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblCommodity" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tbChargeAcct">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="tbChargeAcct" />
                <telerik:AjaxUpdatedControl ControlID="lblChargeAcct" />
            </UpdatedControls>
        </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="tbChargeAcct2">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="tbChargeAcct2" />
                <telerik:AjaxUpdatedControl ControlID="lblChargeAcct2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tbChargeAmt">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="tbChargeAmt" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxCcy" />
            </UpdatedControls>
        </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="tbChargeAmt2">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="tbChargeAmt2" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxAmt2" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxCode2" />
                <telerik:AjaxUpdatedControl ControlID="lblTaxCcy2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="btThem">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="divCharge2" />
                <telerik:AjaxUpdatedControl ControlID="divChargeInfo2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbLCType">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblLCType" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tbExpiryDate">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="tbContingentExpiry" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbAdviseBankNo">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblAdviseBankNo" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbChargeStatus">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblChargeStatus" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbPartyCharged">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblPartyCharged" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbChargeAcct">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblChargeAcct" />
            </UpdatedControls>
        </telerik:AjaxSetting>

		<telerik:AjaxSetting AjaxControlID="comboRevivingBank">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="tbRevivingBankName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="comboFormOfDocumentaryCredit">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="tbFormOfDocumentaryCreditName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="comboAvailableRule">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="tbAvailableRuleName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="combo42DDrawee">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="tb42DDraweeName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
<telerik:AjaxSetting AjaxControlID="comboAvailableWithBy">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="tbAvailableWithByName" />
            </UpdatedControls>
        </telerik:AjaxSetting>

<telerik:AjaxSetting AjaxControlID="comboReceivingBankMT740">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="tbReceivingBankMT740Name" />
            </UpdatedControls>
        </telerik:AjaxSetting>


    </AjaxSettings>
</telerik:RadAjaxManager>--%>
<telerik:RadInputManager ID="RadInputManager1" runat="server">
        <%-- <telerik:DateInputSetting BehaviorID="DateInputBehavior2" DateFormat="dd MMM yyyy">
            <TargetControls>
                <telerik:TargetInput ControlID="tbIssuingDate"></telerik:TargetInput>
                <telerik:TargetInput ControlID="tbExpiryDate"></telerik:TargetInput>
                <telerik:TargetInput ControlID="tbContingentExpiry"></telerik:TargetInput>
            </TargetControls>
        </telerik:DateInputSetting>--%>
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

  <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
  <script type="text/javascript">
      function SetValue() {
          var rcbBeneficiaryCustNo = document.getElementById("<%=rcbBeneficiaryCustNo.ClientID%>");
          $("#spCustomerID").html(rcbBeneficiaryCustNo.value);
      }
      function ChargecodeChange(loai) {

         <%-- if (loai == 1) {
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
      --%>

      }

      $(document).ready(
    function () {
        $('a.add').live('click',
            function () {
                $(this)
                    .html('<img src="Icons/Sigma/Delete_16X16_Standard.png" />')
                    .removeClass('add')
                    .addClass('remove')
                    ;
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
      $("#<%=tbEssurLCCode.ClientID %>").keyup(function (event) {
         
          if (event.keyCode == 13) {
              $("#<%=btSearch.ClientID %>").click();
            }
      });
  </script>
      
  </telerik:RadCodeBlock>

