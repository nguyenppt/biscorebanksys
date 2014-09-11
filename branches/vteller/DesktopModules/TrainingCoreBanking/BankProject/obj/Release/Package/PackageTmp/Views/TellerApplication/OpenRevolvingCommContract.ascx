<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenRevolvingCommContract.ascx.cs" Inherits="BankProject.Views.TellerApplication.OpenRevolvingCommContract" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ register Assembly="DotNetNuke.web" Namespace="DotNetNuke.Web.UI.WebControls" TagPrefix="dnn"%>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" />

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
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/edit.png" 
            ToolTip="Edit Data" Value="btEdit" CommandName="edit" />
    </Items>
</telerik:RadToolBar>

<div>
    <table style="width:100%; padding:0px; border-spacing:0px;">
        <tr>
            <td style="width:200px ;padding:5px 0 5px 20px;">
                <asp:TextBox Width="200" ID="tbID" runat="server" validationGroup="Group1" />
                <i>
                    <asp:Label ID="lblID" runat="server"></asp:Label>
                </i>

            </td>
        </tr>
    </table>
</div>

<div  class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#Main">Main Info</a></li>
    </ul>
    <div id="Main" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />
    <fieldset>  
    <table style="width:100%; padding:0px; border-spacing:0px;">
        <tr>
            <td class="MyLable" >CategoryID:<span class="Required">(*)</span>
                <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator1"
                     ControlToValidate="rcbCategory" ValidationGroup="Commit" InitialValue="" ErrorMessage="CategoryID is required"
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td class="MyContent" width="300">
                <telerik:RadComboBox ID="rcbCategory" runat="server" AllowCustomText="false" MarkFirstMatch="true" Width="300"
                    appendDataboundItems ="true" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                    </telerik:RadComboBox>
            </td>
            <td class="MyLable"></td>
            <td class="MyContent"></td>
        </tr>
        <tr>
            <td class="MyLable">CustomerID:<span class="Required">(*)</span>                 
                <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator2" ControlToValidate="rcbCustomerID"
                    ValidationGroup="Commit" InitialValue="" ErrorMessage="Customer ID is Required!" ForeColor="Red" />
            </td>
            <td class="MyContent">
                <table width="100%" cellpading="0" cellspacing="0">
                    <tr>
                        <td> 
                            <telerik:RadComboBox ID="rcbCustomerID"  runat="server" width="300" MarkFirstMatch="true"
                                AllowCustomtext="false" AppendDataBoundItems="true" height="200" AutoPostBack="true"
                                OnItemDataBOund="rcbCustomerID_OnItemDataBound" OnSelectedIndexChanged="rcbCustomerID_OnSelectedIndexChanged_forRepayAcct" >
                                <CollapseAnimation Type="None" />
                                <ExpandAnimation Type="None" />
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                </Items>
                                </telerik:RadComboBox>
                            </td>                        
                    </tr>
                </table>
            </td>
            <td class="MyLable"></td>
            <td class="MyContent"></td>
        </tr>

    </table>
    <Table style="width:100%; padding:0px; border-spacing:0px;">
          <tr>
            <td class="MyLable">Currency:</td>
            <td class="MyContent" width="150">
                  <telerik:RadComboBox
                        ID="rcbCurrency" runat="server"  OnSelectedIndexChanged="rcbCustomerID_OnSelectedIndexChanged_forRepayAcct"
                        MarkFirstMatch="True" appendDataboundItems="true" AutoPostBack="true"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                       <Items>
                           <telerik:RadComboBoxItem text="" value="" />
                       </Items>
                    </telerik:RadComboBox>
            </td>
              <td><%--<a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a> --%>
              </td>
        </tr>
        </table>
        <Table style="width:100%; padding:0px; border-spacing:0px;">
        <tr>
            <td class="MyLable">Commitment Amount:</td>
            <td class="MyContent" width="150">
                <telerik:radnumerictextbox ID="tbCommitmentAmt" runat="server" ValidationGroup="Group1" width="150"
                    ClientEvents-OnValueChanged="CommtAmt_OnvalueChanged" ></telerik:radnumerictextbox>
            </td>
            <td></td>
            <td></td>
        </tr>
             </Table>
    <table style="width:100%; padding:0px; border-spacing:0px;">
         <tr>
            <td class="MyLable">Start Date:</td>
            <td class="MyContent">                
                <telerik:RadDatePicker ID="rdpStartDate" runat="server" />
            </td>
            <td class="MyLable">End Date:<span class="Required">(*)</span>
                  <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator33" ControlToValidate="rdpEndDate"
                  ValidationGroup="Commit" InitialValue="" ErrorMessage="End Date is Required!" ForeColor="Red"/>

            </td>
                 <td class="MyContent" >  
                <telerik:RadDatePicker ID="rdpEndDate"  runat="server" />
                    </td>
             </tr>
                </table>
         </fieldset>    
        <fieldset>
           <legend style="font-weight:bold;text-transform:uppercase;">
             Basic Details
           </legend>
             <table style="width:100%; padding:0px; border-spacing:0px;">
                <tr>
                    <td class="MyLable" width="150" >Commiment Fee Start:</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox  width="" NumberFormat-DecimalDigits="0" ID="tbFeeStart" runat="server" validationGroup="Group1" />
                    </td>
                    <td class="MyLable">Commitment Fee End:</td>
                    <td class="MyContent">
                       <telerik:RadNumericTextBox ID="tbFeeEnd" runat="server" NumberFormat-DecimalDigits="0" validationGroup="Group1" />
                    </td>
                </tr>
                       <tr>
            <td class="MyLable">Available Amount:</td>
                           <td class="MyContent"><asp:Label ID="lblAvailableAmt" runat="server"></asp:Label></td>            
        </tr>
                       </table>
             <table style="width:100%; padding:0px; border-spacing:0px;">
              <tr>
                <td class="MyLable">Tranche Amount:</td>
                <td style="width: 150px;" class="MyContent">
                    <telerik:RadNumericTextBox ID="tbTrancheAmount" runat="server" validationGroup="Group1"  />
                       </td>
                    <td><%--<a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a> --%></td>
            </tr>
        </table>
             <Table style="width:100%; padding:0px; border-spacing:0px;">
          <tr>
            <td class="MyLable">DD Start Date:</td>
            <td class="MyContent" width="">
                <telerik:RadDatePicker ID="rdpDDStartDate" runat="server" />
            </td>
            <td class="MyLable">DD End Date:</td>
            <td class="MyContent" width="">
                <telerik:RadDatePicker ID="rdpDDEndDate" runat="server" />
            </td>
        </tr>
             </table>
            
            <table style="width:100%; padding:0px; border-spacing:0px;">
                 <tr>
            <td class="MyLable">Interested Repay Account:</td>
            <td class="MyContent">
                <telerik:RadComboBox height="200" appendDataboundItems="true"
                        ID="rcbIntRepayAcct" runat="server" 
                        MarkFirstMatch="True" Width="390" 
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
                    <legend style="text-transform:uppercase;font-weight:bold">Orther Details</legend>
        <table style="width:100%; padding:0px; border-spacing:0px;">
        <tr>
            <td class="MyLable">Secured (Y/N):</td>
            <td class="MyContent"> 
                <telerik:RadComboBox
                        ID="rcbSecured" runat="server" 
                        MarkFirstMatch="True"  
                        AllowCustomText="false">
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
             </table>
            <table style="width:100%; padding:0px; border-spacing:0px;">
        <tr>
            <td class="MyLable">Customer Remarks:<span class="Required">(*)</span>
                   <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator4" ControlToValidate="tbCustRemarks"
                  ValidationGroup="Commit" InitialValue="" ErrorMessage="Customer remark is Required!" ForeColor="Red"/>
            </td>
            <td class="MyContent" width="350">
                <telerik:RadTextBox ID="tbCustRemarks" Width="350" runat="server" ValidationGroup="Group1"></telerik:RadTextBox>
            </td>            
            <td><%--<a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a>--%></td>
            </tr>
            </table>
            <table style="width:100%; padding:0px; border-spacing:0px;">        
          <tr>
            <td class="MyLable">Account Officer:</td>
            <td class="MyContent">
                 <telerik:RadComboBox appendDataboundItems="true"
                        ID="rcbAccountOfficer" runat="server" 
                        MarkFirstMatch="True" Width="350" 
                        AllowCustomText="false">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
            </td>
        </tr>
                   </table>    
        </fieldset>
    </div>
</div>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbCustomerID">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="rcbIntRepayAcct" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rcbCurrency">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="rcbIntRepayAcct" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock id="RadCodeBlock1" runat="server" >
<script type="text/javascript">
    $("#<%=tbID.ClientID%>").keyup(function (event) {
        if (event.keyCode == 13) {
            $("#<%=btSearch.ClientID%>").click();
        }
    });
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
    function CommtAmt_OnvalueChanged(sender, args)
    {
        var CommitAmt = $find("<%=tbCommitmentAmt.ClientID%>").get_value();
        var TrancheAmt = $find("<%=tbTrancheAmount.ClientID%>");
        TrancheAmt.set_value(CommitAmt);
    }
  </script>
    </telerik:RadCodeBlock>
<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click1" Text="Search" />
</div>
