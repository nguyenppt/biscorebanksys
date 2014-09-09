<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UnBlockAccountListEmpty.ascx.cs" Inherits="BankProject.Views.TellerApplication.UnBlockAccountListEmpty" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>
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
                ToolTip="Commit Data" Value="btnCommit" CommandName="btnCommit">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview1.png"
                ToolTip="Preview" Value="btnPreview" CommandName="btnPreview">
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
