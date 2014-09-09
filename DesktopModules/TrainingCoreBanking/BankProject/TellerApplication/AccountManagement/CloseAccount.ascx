<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CloseAccount.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.CloseAccount" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>
<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%">
    <items>
        <%--<telerik:RadToolBarButton Value="bplaybackprev" ImageUrl="~/Icons/bank/playback_prev_icon.png"
                 ToolTip="Back" CommandName="playbackprev">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/left_arrow.png"
                 ToolTip="Back" Value="btleftarrow"  CommandName="leftarrow">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/right_arrow.png"
                 ToolTip="Next Record" id="btrightarrow" CommandName="rightarrow">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/playback_next_icon.png"
                 ToolTip="Back" Value="btplaybacknext" CommandName="playbacknext">
            </telerik:RadToolBarButton>--%>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/doc_lines_icon.png"
            ToolTip="Commit Data" Value="btdoclines" CommandName="doclines">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/doc_new_icon.png"
            ToolTip="Back" Value="btdocnew" CommandName="docnew">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/cursor_drag_hand_icon.png"
            ToolTip="Back" Value="btdraghand" CommandName="draghand">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Back" Value="btsearch" CommandName="search">
        </telerik:RadToolBarButton>
    </items>
</telerik:RadToolBar>
