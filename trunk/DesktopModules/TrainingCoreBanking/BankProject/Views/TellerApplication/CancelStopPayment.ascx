<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CancelStopPayment.ascx.cs" Inherits="BankProject.Views.TellerApplication.CancelStopPayment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>

<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" 
         OnButtonClick="OnRadToolBarClick"
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
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtId" runat="server" Width="200" Text="TT/09161/07941" />
            <i>
                <asp:Label ID="lblDepositCode" runat="server" /></i></td>
    </tr>
</table>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#Main">Cancle Payment Stop</a></li>
        <li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full Vi</a></li>
    </ul>
    <div id="Main" class="dnnClear">
    </div>
</div>
