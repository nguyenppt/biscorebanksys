<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CancelStopCheque.ascx.cs" Inherits="BankProject.Views.TellerApplication.CancelStopCheque" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true"
        OnButtonClick="OnRadToolBarClick" ValidationGroup="Commit"
        EnableShadows="true" Width="100%">
        <Items>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/CommitData1.png"
                ToolTip="Commit Data" Value="btdoclines" CommandName="doclines">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview1.png"
                ToolTip="Preview" Value="btdocnew" CommandName="docnew">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/button_white_check.png"
                ToolTip="Authorize" Value="btdraghand" CommandName="draghand">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
                ToolTip="Reverse" Value="btsearch" CommandName="search">
            </telerik:RadToolBarButton>
        </Items>
    </telerik:RadToolBar>
</div>

<div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 200px; padding: 5px 0 5px 20px;">
                <asp:TextBox ID="txtId" runat="server" Width="200" />
                <i>
                    <asp:Label ID="lblDepositCode" runat="server" /></i></td>
        </tr>
    </table>
</div>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Cancel Payment Stop</a></li>
        <li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Serial No</td>
                <td class="MyContent" width="250">
                    <telerik:RadComboBox ID="cmbSerialNo"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        Width="250" runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="1" Text="121214" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>

            <tr>
                <td class="MyLable">Cheque Type
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtChequeType" Width="300" runat="server" ValidationGroup="Group1" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Doc Expiry D</td>
                <td class="MyContent">
                    <telerik:RadDatePicker ID="cldDocExpiry" runat="server" ValidationGroup="Group1"></telerik:RadDatePicker>
                </td>
            </tr>
        </table>


    </div>

    <div id="blank">
    </div>
</div>

<script type="text/javascript">
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
