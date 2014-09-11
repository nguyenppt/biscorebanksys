<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollateralContingentEntry.ascx.cs" Inherits="BankProject.Views.TellerApplication.CollateralContingentEntry" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    })
</script>

 <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
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

<div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:250px; padding: 5px 0 5px 20px;" >
                <asp:TextBox width="250" ID="tbCollateralContengentEntry" runat="server" ValidationGroup="Group1" />
                </td>
        </tr>
    </table>
</div>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li ><a href="#blank1">Contingent Info</a></li>
    </ul>
    <div id="blank1" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />
        <fieldset>
            <legend style="text-transform:uppercase ;font-weight:bold;">Customer Information</legend>
             <table width="100%" cellpadding="0" cellspacing="0">
                 <tr>
                     <td class="MyLable">Customer ID:</td>
                     <td class="MyContent">
                         <table width="100%" cellpadding="0" cellspacing="0">
                             <tr>
                                 <td width="350">
                                     <telerik:RadComboBox ID="rcbCustomerID" runat="server" AllowCustomText="false"
                                         MarkFirstMatch="true" width="350" AppendDataBoundItems="true" OnItemDataBound="rcbCustomerID_ItemDataBound"
                                         OnClientSelectedIndexChanged="rcbCustomerID_OnClientSelectedIndexChanged">                                          
                                         <ExpandAnimation Type="None" />
                                         <CollapseAnimation Type="None" />
                                         <Items>
                                             <telerik:RadComboBoxItem Value="" Text="" />
                                         </Items>
                                     </telerik:RadComboBox> 
                                      </td>
                                 </tr>
                             </table>
                           </td>
                           </tr>
                 <tr>
                     <td class="MyLable">Address:</td>
                     <td class="MyContent">
                         <telerik:RadTextBox ID="tbAddress" runat="server" ValidationGroup="Group1" Width="350" />
                         <a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a>
                     </td>
                     <td ></td>
                     </tr> 
                           </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                 
                 <tr>
                     <td class="MyLable">ID / Tax Code:</td>
                     <td class="MyContent" width="350">
                         <telerik:RadTextBox ID="tbID" runat="server" ValidationGroup="Group1" Width="150" />
                     </td>
                     <td class="MyLable">Date Of Issue:</td>
                     <td class="MyContent">
                         <telerik:RadDatePicker ID="rdpDateOfIssue" runat="server" />
                     </td>
                 </tr>            
             </table>

        </fieldset>
        <fieldset>
            <legend style="text-transform:uppercase ;font-weight:bold;">CONTINGENT&nbsp; Information</legend>
             <table width="100%" cellpadding="0" cellspacing="0">
                 <tr>
                     <td class="MyLable">Transaction Code:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator1"
                     ControlToValidate="rcbTransactionCode" ValidationGroup="Commit" InitialValue="" ErrorMessage="Transaction Code is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> 
                     </td>
                     <td class="MyContent">
                          <telerik:RadComboBox ID="rcbTransactionCode" runat="server" MarkFirstMatch="true" AllowCustomText="false"
                            width="150">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />   
                                <telerik:RadComboBoxItem Value="901" Text="901 - Contingent Debit" />                               
                            </Items>
                        </telerik:RadComboBox>
                     </td>
                     
                 </tr>
                 <tr>
                     <td class="MyLable">Debit or Credit:<span class="Required">(*)</span>
                          <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator2"
                     ControlToValidate="rcbDebitOrCredit" ValidationGroup="Commit" InitialValue="" ErrorMessage="Debit or Credit is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> 
                     </td>
                     <td class="MyContent">
                         <telerik:RadComboBox ID="rcbDebitOrCredit" runat="server" MarkFirstMatch="true" AllowCustomText="false"
                            width="150">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />  
                                <telerik:RadComboBoxItem Value="D" Text="D - Debit" /> 
                                <telerik:RadComboBoxItem Value="C" Text="C - Credit" />                               
                            </Items>
                        </telerik:RadComboBox>
                     </td>                     
                 </tr>
                 <tr>
                     <td class="MyLable">Currency:</td>
                     <td class="MyContent">
                         <telerik:RadComboBox ID="rcbFreignCcy" runat="server" MarkFirstMatch="true" AllowCustomText="false"
                            width="150">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />                                
                            </Items>
                        </telerik:RadComboBox>
                     </td>                     
                 </tr>
                 <tr>
                     <td class="MyLable">Account No:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator3"
                     ControlToValidate="rcbAccountNo" ValidationGroup="Commit" InitialValue="" ErrorMessage="CategoryID is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> 
                     </td>
                     <td class="MyContent">
                         <telerik:RadComboBox ID="rcbAccountNo" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                             width="350">
                             <CollapseAnimation Type="None" />
                             <ExpandAnimation Type="None" />
                             <Items>
                                 <telerik:RadComboBoxItem value="VND-19411-00011221" text="VND-19411-00011221 - Real-estate Collat.For Borrowing"/>
                             </Items>
                         </telerik:RadComboBox>
                     </td>                    
                 </tr>
                 <tr>
                     <td class="MyLable">Amount:</td>
                     <td class="MyContent">
                         <telerik:RadNumericTextBox ID="tbAmountLCY" runat="server" Width="150"  ValidationGroup="Group1"></telerik:RadNumericTextBox>
                         
                     </td>
                      <td class="MyLable">Deal Rate:</td>
                     <td class="MyContent">
                         <telerik:RadNumericTextBox ID="tbDealRate" runat="server" ValidationGroup="Group1" 
                             NumberFormat-DecimalDigits="5" ></telerik:RadNumericTextBox>
                         
                     </td>
                     
                 </tr>
                
                 <tr>
                     <td class="MyLable">Value Date:</td>
                     <td class="MyContent">
                         <telerik:RadDatePicker width="150" ID="rdpValueDate" runat="server" ValidationGroup="Group1"></telerik:RadDatePicker>
                        
                     </td>
                 </tr>
                 <tr>
                     <td class="MyLable">Reference No:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator4"
                     ControlToValidate="tbReferenceNo" ValidationGroup="Commit" InitialValue="" ErrorMessage="Reference No is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> 
                     </td>
                     <td class="MyContent">
                         <telerik:RadTextBox ID="tbReferenceNo" runat="server" ValidationGroup="Group1" width="150"/>
                     </td>
                     
                 </tr>
                 <tr>
                     <td class="MyLable">Narrative:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator5"
                     ControlToValidate="tbNarrative" ValidationGroup="Commit" InitialValue="" ErrorMessage="Narrative is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> 
                     </td>
                     <td class="MyContent" width="350">
                         <telerik:RadTextBox ID="tbNarrative" runat="server" ValidationGroup="Group1" Width="350" />
                     </td>
                     <td ><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a> </td>
                     
                 </tr>
             </table>

        </fieldset>
    </div>
</div>

<script type="text/javascript">
    function rcbCustomerID_OnClientSelectedIndexChanged()
    {
        var customerElement = $find("<%= rcbCustomerID.ClientID %>");

         var AddressElement = $find("<%= tbAddress.ClientID %>");
        AddressElement.set_value(customerElement.get_selectedItem().get_attributes().getAttribute("Address"));

        var IDElement = $find("<%= tbID.ClientID %>");
        IDElement.set_value(customerElement.get_selectedItem().get_attributes().getAttribute("IdentityNo"));
       
        var datesplit = customerElement.get_selectedItem().get_attributes().getAttribute("IssueDate").split('/');
        var IsssuedDateElement = $find("<%= rdpDateOfIssue.ClientID %>");
        IsssuedDateElement.set_selectedDate(new Date(datesplit[2].substring(0, 4), datesplit[0], datesplit[1]));
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
              $(this).attr('name', 'row' + thisRow + thisName);
              $(this).attr('id', 'row' + thisRow + thisName);
          });

  });

  </script>