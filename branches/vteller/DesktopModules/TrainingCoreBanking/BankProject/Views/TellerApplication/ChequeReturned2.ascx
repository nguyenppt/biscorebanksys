<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChequeReturned2.ascx.cs" Inherits="BankProject.Views.TellerApplication.ChequeReturned2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="ValidationSummary1" runat="server"  ShowMessageBox="true" ShowSummary="false" ValidationGroup="Commit" />
<telerik:RadWindowManager id="RadWindowManager1"  runat="server" EnableShadow="true" />

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableShadows="true" EnableRoundedCorners="true" Width="100%" OnButtonClick="RadToolBar1_OnButtonClick">
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


<div>
    <table width="100%" cellpadding="0" cellspacing="0" >
        <tr>
            <td style="width:200px; padding:5px 0 5px 20px">
                <telerik:RadTextBox ID="tbID" runat="server" Width="200px" ValidationGroup="Group1">
                </telerik:RadTextBox>
            </td>
        </tr>
    </table>
</div>
<div class="dnnForm" id="tabs-demo">
    <div>
        <ul class="dnnAdminTabNav">
            <li><a href="# blank1">Register</a></li>
        </ul>
    </div>
    <div id="blan1" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Total Issued:</td>
                <td class="MyContent">
                     <telerik:RadNumericTextBox id="tbTotalIssued" runat="server"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator=""
                         borderWidth="0" readOnly="true" />
                </td>
                 <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
            <tr>
                <td class="MyLable">Total Used:</td>
                <td class="MyContent">
                   <telerik:RadNumericTextBox id="tbTotalUsed" runat="server"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" 
                        borderWidth="0" readOnly="true" />
                </td>
                 <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
            <tr>
                 <td class="MyLable">Total Held:</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox id="tbTotalHeld" runat="server"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator=""
                         borderWidth="0" readOnly="true"  />
                </td>
            </tr>
            <tr>
                 <td class="MyLable">Cheque.Nos:</td>
                <td class="MyContent">
                  <telerik:RadTextBox id="tbChequeNos" runat="server"  borderWidth="0" readOnly="true"  />
                </td>
            </tr>
            <tr>
                 <td class="MyLable">Presented Cheques:</td>
                <td class="MyContent">
                    <asp:Label ID="Label3" runat="server" />
                </td>
            </tr>
             <tr>
                 <td class="MyLable">Stopped Cheques:</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox id="tbStoppedCheque" runat="server"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" 
                         borderWidth="0" readOnly="true" />
                </td>
            </tr>
             <tr>
                 <td class="MyLable">Returned Cheques: <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator71"
                     ControlToValidate="tbReturnedCheque" ValidationGroup="Commit" InitialValue="" 
                    ErrorMessage="Returned Cheque is required"  ForeColor="Red" /></td>
                <td class="MyContent" width="200">
                    <telerik:RadNumericTextBox ID="tbReturnedCheque"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator=""
                         runat="server" ValidationGroup="Group1" width="200" /> 
                </td>
                 <td ><%--<a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a>--%></td>           

             </tr>
        </table>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="MyLable">Narrative:</td>
                <td class="MyContent">
                    <asp:Label ID="lblNarrative" runat="server" />
                </td>
            </tr>
        </table>
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