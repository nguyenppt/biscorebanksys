<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlockAccount.ascx.cs" Inherits="BankProject.Views.TellerApplication.BlockAccount" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>

<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%"
        OnButtonClick="OnRadToolBarClick">
        <Items>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
                ToolTip="Commit Data" Value="btCommit" CommandName="Commit">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
                ToolTip="Preview" Value="btPreview" CommandName="Preview">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
                ToolTip="Authorize" Value="btAuthorize" CommandName="Authorize">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
                ToolTip="Reverse" Value="btReverse" CommandName="Reverse">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
                ToolTip="Search" Value="btSearch" CommandName="Search">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
                ToolTip="Print Deal Slip" Value="btPrint" CommandName="Print">
            </telerik:RadToolBarButton>
        </Items>
    </telerik:RadToolBar>      
</div>


<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
    ShowSummary="False" ValidationGroup="Commit" />
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
        <li><a href="#ChristopherColumbus">Block Account</a></li>
        <%--<li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>--%>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer ID</td>
                <td class="MyContent" width="120">
                    <asp:label id="lbCustomerID" width="120" runat="server" ></asp:label>
                </td>
                <td class="MyContent">
                    <asp:label id="lbCustomerName" runat="server" ></asp:label>
                </td>
            </tr>
            </table>
        <table width="100%" cellpadding="0" cellspacing="0">

            <tr>
                <td class="MyLable">Account</td>
                <td class="MyContent">
                    <asp:label id="lbAccount" runat="server" ></asp:label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Amount</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtAmount" runat="server"                         
                        ValidationGroup="Group1">
                        <NumberFormat DecimalDigits="0" />
                    </telerik:RadNumericTextBox>                    
                </td>
            </tr>
            <tr>
            <td class="MyLable">Block Account</td>
            <td class="MyContent">
                <asp:CheckBox ID="chbBlockAccount" runat="server" />
            </td>
        </tr>
            <tr>
                <td class="MyLable">From Date:</td>
                <td class="MyContent">
                    <customControl:CustomDataTimePicker ID="dtpFromDate" runat="server" ValidationGroup="Group1" />
                </td>
            </tr>

            <tr>
                <td class="MyLable">To Date:</td>
                <td class="MyContent">
                    <customControl:CustomDataTimePicker ID="dptToDate" runat="server" ValidationGroup="Group1" />
                </td>
            </tr>

            <tr>
                <td class="MyLable">Description</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtDescription" Width="300" runat="server" ValidationGroup="Group1" />
                </td>
            </tr>
        </table>
    </div>
</div>
<script type="text/javascript">
    $('#<%=txtId.ClientID%>').keyup(function (event) {
        if (event.keyCode == 13) { $("#<%=btSearch.ClientID%>").click(); }
    });
</script>
<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" Text="Search"  OnClick="btSearch_Click" />
</div>
