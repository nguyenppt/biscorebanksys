<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChequeCancleStop.ascx.cs" Inherits="BankProject.Views.TellerApplication.ChequeCancleStop" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<telerik:RadWindowManager id="RadWindowManager1"  runat="server" EnableShadow="true" />
<asp:ValidationSummary ID="ValidationSummary1" runat="server"  ShowMessageBox="true" ShowSummary="false" ValidationGroup="Commit" />

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>

<div>
    <telerik:RadToolBar ID="RadToolBar" runat="server" EnableRoundedCorners="true" EnableShadows="true" width="100%"  OnButtonClick="RadToolBar_OnButtonClick">
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
        </Items>
    </telerik:RadToolBar>
</div>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="tbID" runat="server" Width="200" />
            <i> <asp:Label ID="lblNote" runat="server" /></i></td>
    </tr>
</table>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#blank1">Cancle Payment Stop</a></li>
    </ul>
    <div id="blank1" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Serial No:
                    <span class="Required">:(*)</span>
                    <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator1"
                     ControlToValidate="tbSerialNo" ValidationGroup="Commit" InitialValue="" 
                     ErrorMessage="Serial No is required"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td class="MyContent" width="160">
                   <telerik:RadNumericTextBox id="tbSerialNo" runat="server" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" />
                </td>
                <td class="MyLable">
                  <%--  <a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a>--%>
                </td>
                <td class="MyContent"></td>
            </tr>
        </table>
         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Cheque Type:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbChequeType" runat="server" width="250" ValidationGroup="Group1" MarkFirstMatch="true" AllowCustomText="false" AppendDataBoundItems="true">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
             <tr>
                  <td class="MyLable">Activity Date:</td>
                 <td class="MyContent">
                     <telerik:RadDatePicker ID="rdpActivityDate" runat="server" ValidationGroup="Group1" />
                 </td>
             </tr>
        </table>

    </div>

</div>

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

</script>
<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click1" Text="Search" />
</div>