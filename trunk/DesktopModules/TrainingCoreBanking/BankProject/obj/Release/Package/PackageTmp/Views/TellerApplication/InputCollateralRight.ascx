<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InputCollateralRight.ascx.cs" Inherits="BankProject.Views.TellerApplication.InputCollateralRight" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
          </telerik:RadWindowManager>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    })
</script>


 <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
     OnButtonClick="RadToolBar1_ButtonClick">
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
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:200px; padding: 5px 0 5px 20px;" >
                <asp:TextBox ID="tbCollateralRightID" runat="server" ValidationGroup="Group1"/>
                <i><asp:Label ID="lblCheckCustomerName" runat="server" text=""/></i>
                </td>
        </tr>
    </table>
</div>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#blank1">Collateral Info</a></li>

    </ul>
    <div id="blank1" class="dnnClear">
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />
         <fieldset>
            <legend style="text-transform:uppercase; font-weight:bold">Allocated Details</legend>
            
             <table width="100%" cellpadding="0" cellspacing="0">
                 <tr>
                    <td class="MyLable">Customer Name:</td>
                    <td class="MyContent" width="350">
                        <asp:Label ID="lblCustomerName" runat="server" />
                    </td> 
                     <td class="MyLable"></td>                  
                     <td class="MyContent"></td>
                </tr>
                <tr>
                    <td class="MyLable">Limit ID/ Contract:</td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="rcbLimitID" width="350" runat="server" ValidationGroup="Group1" height="150"
                            appendDataboundItems="true" MarkFirstMatch="true" AllowCustomText="false" autoPostBack="true"
                            onSelectedIndexChanged="rcbLimitID_LoadFor_COllateral" OnItemDataBound="rcbLimitID_OnItemDataBound">
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
            <legend style="text-transform:uppercase; font-weight:bold">Collateral Info</legend>
            <table widt="100%" cellpadding="0" cellspacing="0">
                
                <tr>
                    <td class="MyLable">Validity Date:</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="RdpValidityDate" width="150" runat="server" ValidationGroup="Group1"></telerik:RadDatePicker>
                       
                    </td>
                    <td class="MyLable">Expiry Date:</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="RdpExpiryDate" runat="server" ValidationGroup="Group1"></telerik:RadDatePicker>
                       
                    </td>
                </tr>
                
                 <tr>
                    <td class="MyLable">Notes:</td>
                    <td class="MyContent" width="250">
                        <telerik:RadTextBox ID="tbNotes" runat="server" ValidationGrooup="Group1" Width="250" Text="NHAP MA LOAI HINH TAI SAN DAM BAO" />

                    </td>
                    <td> <%--<a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a> --%></td>                   
                </tr>
                <tr style="visibility:hidden;"> 
                    <td class="MyLable">Collateral Type:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator2"
                     ControlToValidate="rcbCollateralType" ValidationGroup="Commit" InitialValue="" ErrorMessage="Collateral Type is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> 
                    </td>
                    <td class="MyContent" width="350">
                        <telerik:RadComboBox ID="rcbCollateralType" runat="server" MarkFirstMatch="true" AllowCustomText="false"
                            width="350" appendDataBoundItems="true">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />  
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
                <tr style="visibility:hidden;">
                    <td class="MyLable">Collateral Code:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator1"
                     ControlToValidate="rcbCollateralCode" ValidationGroup="Commit" InitialValue="" ErrorMessage="Collateral Code is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> 
                    </td>
                    <td class="MyContent" width="350">
                        <telerik:RadComboBox ID="rcbCollateralCode" runat="server" MarkFirstMatch="true" AllowCustomText="false"
                            width="350" appendDataBoundItems="true">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
            </table>
        </fieldset>
       
    </div>

</div>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbLimitID">
            <UpdatedControls>  
                 <telerik:AjaxUpdatedControl ControlID="rcbCollateralType" />
                 <telerik:AjaxUpdatedControl ControlID="rcbCollateralCode" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock id="RadCodeBlock" runat="server">
<script type="text/javascript">
    $("#<%=tbCollateralRightID.ClientID%>").keyup(function (event) {
        if (event.keyCode == 13 || event.keyCode == 9) {
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

  </script>
</telerik:RadCodeBlock>
<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click1" Text="Search" />
</div>