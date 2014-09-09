<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FTAccountClose.ascx.cs" Inherits="BankProject.Views.TellerApplication.FTAccountClose" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });

    function OnClientIndexChanged() {
        var creditCurrencyElement = $find("<%= cmbAccountPaid.ClientID %>");
        var creditCurrencyValue = creditCurrencyElement.get_value();
        var lblCreditCurrencyElement = $('#<%=lblAccountPaid.ClientID%>')

        //if (creditCurrencyValue) {
        //    lblCreditCurrencyElement.html("");
        //}
        //else {
        //    lblCreditCurrencyElement.html("");
        //}
    }
     
    //radconfirm("Unauthorised overdraft of VND 19570 on account 060000769870", confirmCallbackFunction3);
    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();        

        if (button.get_commandName() == "doclines" && !clickCalledAfterRadconfirm) {            
            args.set_cancel(true);            
            lastClickedItem = args.get_item();
            radconfirm("Unauthorised overdraft of VND 19570 on account 060000769870", confirmCallbackFunction3);
        }
    }

    var lastClickedItem = null;    
    var clickCalledAfterRadconfirm = false;
    
    function confirmCallbackFunction3(args) {
        clickCalledAfterRadconfirm = true;
        lastClickedItem.click();

        lastClickedItem = null;
    }
</script>

<telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>
 <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
        OnButtonClick="OnRadToolBarClick">
        <Items>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
                ToolTip="Commit Data" Value="btdoclines" CommandName="doclines">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
                ToolTip="Preview" Value="btdocnew" CommandName="docnew">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
                ToolTip="Authorize" Value="btdraghand" CommandName="draghand">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
                ToolTip="Reverse" Value="btsearch" CommandName="search">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
                ToolTip="Search" Value="searchNew" CommandName="searchNew">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
                ToolTip="Print Deal Slip" Value="print" CommandName="print">
            </telerik:RadToolBarButton>
        </Items>
    </telerik:RadToolBar>

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtId" runat="server" Width="200" Text="FT/14161/80155" />
        </td>
    </tr>
</table>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">FT Acct Close</a></li>
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
                    <asp:Label ID="lblCustomer" CssClass="cssDisableLable" />
                </td>
            </tr>

            <tr>
                <td class="MyLable">Currency:</td>
                <td class="MyContent" style="width: 80px">
                    <asp:Label ID="lblCurrency" CssClass="cssDisableLable" runat="server" Text="VND">
                    </asp:Label>
                </td>

                <td class="MyContent">
                    <asp:Label ID="lblCurrencyUnit" CssClass="cssLableLink" runat="server" Text="Dong">
                    </asp:Label>
                </td>
            </tr>

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
        </table>

        <br />
        

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>_________________________________Credit Information___________________________________________________________________
                </td>
            </tr>
        </table>

        <asp:UpdatePanel id="UpdatePanel1" runat="server">
            <ContentTemplate>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Credit Currency:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbCreditCurrency" runat="server" MarkFirstMatch="True" 
                        OnSelectedIndexChanged="cmbCreditCurrency_OnSelectedIndexChanged" AutoPostBack="true"
                        AllowCustomText="false">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="USD" Text="USD" />
                            <telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                            <telerik:RadComboBoxItem Value="GBP" Text="GBP" />
                            <telerik:RadComboBoxItem Value="JPY" Text="JPY" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
                   

                
       
                    
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Account Paid:</td>
                <td class="MyContent" width="400">
                    <telerik:RadComboBox ID="cmbAccountPaid" MarkFirstMatch="True" 
                        OnClientSelectedIndexChanged="OnClientIndexChanged"  width="400"
                        AllowCustomText="false" runat="server">
                        
                    </telerik:RadComboBox>
                </td>
                <td>
                    <asp:Label runat="server" ID="lblAccountPaid" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Credit Amount:</td>
                <td class="MyContent">
                    <asp:Label ID="lblCreditAmount" CssClass="cssDisableLable" />
                </td>
            </tr>
        </table>
                    </ContentTemplate>
            </asp:UpdatePanel>
                 <asp:UpdatePanel id="UpdatePanel4" runat="server">
            <ContentTemplate>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Narrative:</td>
                <td class="MyContent" width="160">
                    <telerik:RadTextBox ID="txtNarrative" runat="server" />
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
        </table>
                </ContentTemplate>
            </asp:UpdatePanel>

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
                <td class="MyContent">
                    <asp:Label CssClass="cssDisableLable" runat="server">                                                    
                    </asp:Label>
                </td>
            </tr>
            </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Record Status:</td>
                <td class="MyContent" width="80">
                    <asp:Label ID="Label1" CssClass="cssDisableLable" runat="server">   
                            IHLD                         
                    </asp:Label>
                </td>
                <td class="MyContent">INPUT Held
                </td>
            </tr>
            </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Current Number:</td>
                <td class="MyContent">
                    <asp:Label CssClass="cssDisableLable">
                            1
                    </asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Inputter:</td>
                <td class="MyContent">
                    <asp:Label CssClass="cssDisableLable">
                            112_ID2054_I_INAU
                    </asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Inputter:</td>
                <td class="MyContent">
                    <asp:Label CssClass="cssDisableLable">
                            112_SYSTEM
                    </asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Authorised:</td>
            </tr>

            <tr>
                <td class="MyLable">Date Time:</td>
                <td class="MyContent">
                    <asp:Label CssClass="cssDisableLable" ID="lbDateTime1" runat="server">
                            
                    </asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Date Time:</td>
                <td class="MyContent">
                    <asp:Label CssClass="cssDisableLable" ID="lbDateTime2" runat="server">
                    </asp:Label>
                </td>
            </tr>
            </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Company Code:</td>
                <td class="MyContent" width="100">
                    <asp:Label CssClass="cssDisableLable">
                            VN-001-1221
                    </asp:Label>
                </td>
                <td class="MyContent">
                    <asp:Label ID="Label3" CssClass="cssLableLink">
                        CHI NHANH CHO LON
                    </asp:Label>
                </td>
            </tr>
    </table>            

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Department Code:</td>
                <td class="MyContent">
                    <asp:Label CssClass="cssDisableLable">
                            1
                    </asp:Label>
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
