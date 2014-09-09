<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalculatedInterestedAmountManual.ascx.cs" Inherits="BankProject.Views.TellerApplication.CalculatedInterestedAmountManual" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>

<div class="dnnForm" id="tabs-demo">
    <div id="ChristopherColumbus" class="dnnClear">
      <table width="100%" cellpadding="0" cellspacing="0">
         <td class="MyLable" style="width: 80px;">Account Type</td>
         <td class="MyContent" style="width: 200px;">
                <telerik:RadComboBox id="rcbAccountType" runat="server" AllowcustomText="false" 
                    MarkFirstMatch="true" AppendDataBoundItems="True" width="200" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" /> 
                    <Items>
                        <telerik:RadComboBoxItem Value="1" Text="Non term Saving Account" />
                        <telerik:RadComboBoxItem Value="2" Text="Saving Account" />
                    </Items>
                </telerik:RadComboBox>
            </td>
          <td style="width: 50px;"></td>
            <td class="MyLable" style="width:50px;">Date:</td>
                <td class="MyContent" style="width:120px;">
                    <Telerik:RadDatePicker width="120" ID="dtpDate" runat="server" ValidationGroup="Group1" />
                </td>
          <td class="MyContent">
              <asp:button ID="btCalcu" runat="server" Text="Calculator" ImageUrl="~/Icons/bank/search.png" OnClick="btCalcu_Click" />
          </td>
     </table>
    </div>
</div>