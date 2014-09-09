<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FTAccountClose2.ascx.cs" Inherits="BankProject.Views.TellerApplication.FTAccountClose2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
                ToolTip="Commit Data" Value="btnCommit" CommandName="btnCommit">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
                ToolTip="Preview" Value="btnPreview" CommandName="btnPreview">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
                ToolTip="Authorize" Value="btAuthorize" CommandName="btAuthorize">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
                ToolTip="Reverse" Value="btRevert" CommandName="btRevert">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
                ToolTip="Search" Value="searchNew" CommandName="searchNew">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
                ToolTip="Print Deal Slip" Value="print" CommandName="print">
            </telerik:RadToolBarButton>
        </Items>
    </telerik:RadToolBar>

</div>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtId" runat="server" Width="200" Text="FT/09161/80155" />
        </td>
    </tr>
</table>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">FT Acct Close</a></li>
        <li><a href="#blank">Full View</a></li>
    </ul>

    <div id="ChristopherColumbus" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>_________________________________Debit Information___________________________________________________________________
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer:</td>
                <td class="MyContent">
                    <asp:Label ID="lblCustomer" CssClass="cssDisableLable" runat="server" />
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Currency:</td>
                <td width="60">VND
                </td>
                <td>Dong
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Close Account:</td>
                <td class="MyContent" style="width: 120px">
                    <asp:Label ID="lblClosedAccount" CssClass="cssDisableLable" runat="server">
                        060000769870                   
                    </asp:Label>
                </td>


                <td class="MyContent">
                    <asp:Label ID="lblCustomerName" CssClass="cssLableLink" runat="server">
                        TRAN KIM LONG
                    </asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Debit Amount:</td>
                <td class="MyContent">
                    <asp:Label ID="lblDebitAmount" CssClass="cssDisableLable" runat="server">
                        233,588,129
                    </asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Debit Date:</td>
                <td class="MyContent">
                    <asp:Label runat="server" ID="lblDebitDate" />
                </td>
            </tr>

            <tr>
                <td class="MyLable">Status:</td>
                <td class="MyContent">
                    <asp:Label runat="server" ID="Label2" Text="CLOSED" />
                </td>
            </tr>
        </table>

        <br />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>_________________________________Credit Information___________________________________________________________________
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Credit Currency:</td>
                <td width="60">VND
                </td>
                <td>Dong
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Smartbank BR:</td>
                <td class="MyContent" width="250"></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Account Paid:</td>
                <td class="MyContent" width="150">
                    <asp:Label ID="lblAccountPaid" runat="server" Text="VND1000120541221" />
                </td>
                <td class="MyContent">
                    <asp:Label ID="Label1" runat="server" Text="RECORD.AUTOMATICALLY.OPENED" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Credit Amount:</td>
                <td class="MyContent">
                    <asp:Label ID="lblCreditAmount" CssClass="cssDisableLable" runat="server" />
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Narrative:</td>
                <td class="MyContent">CHI TAT TOAN TK 060000769870-KIM LONG
                </td>
            </tr>
        </table>

        <br />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>_________________________________Audit Information___________________________________________________________________
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Override:</td>
                <td class="MyContent">Unauthorised overdraft of VND 19570 on account 06000076987                        
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Record Status:</td>
                <td class="MyContent" width="70">INAU
                        
                </td>
                <td class="MyContent">INPUT Unauthorised                        
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Current Number:</td>
                <td class="MyContent">1                       
                </td>
            </tr>

            <tr>
                <td class="MyLable">Inputter:</td>
                <td class="MyContent">112_ID2054_I_INAU                        
                </td>
            </tr>

            <tr>
                <td class="MyLable">Inputter:</td>
                <td class="MyContent">112_SYSTEM                        
                </td>
            </tr>

            <tr>
                <td class="MyLable">Authorised:</td>
            </tr>

            <tr>
                <td class="MyLable">Date Time:</td>
                <td class="MyContent">
                    <asp:Label runat="server" ID="lblDateTime1"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Date Time:</td>
                <td class="MyContent">
                    <asp:Label runat="server" ID="lblDateTime2"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Company Code:</td>
                <td class="MyContent" width="150">VN-001-1221                        
                </td>
                <td class="MyContent">CHI NHANH CHO LON                        
                </td>
            </tr>

            <tr>
                <td class="MyLable">Department Code:</td>
                <td class="MyContent">1                        
                </td>
            </tr>
        </table>
    </div>

    <div id="blank">
    </div>
</div>
