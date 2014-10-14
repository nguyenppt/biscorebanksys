<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoanPaymentManualCalc.ascx.cs" Inherits="BankProject.Views.TellerApplication.LoanPaymentManualCalculated" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="DotNetNuke.web" Namespace="DotNetNuke.Web.UI.WebControls" TagPrefix="dnn" %>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>

<div class="dnnForm" id="tabs-demo">
    <div id="ChristopherColumbus" class="dnnClear">
      <table width="100%" cellpadding="0" cellspacing="0">
         
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