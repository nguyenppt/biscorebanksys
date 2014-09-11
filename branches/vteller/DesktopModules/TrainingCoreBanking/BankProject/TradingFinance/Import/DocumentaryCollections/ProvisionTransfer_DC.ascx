<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProvisionTransfer_DC.ascx.cs" Inherits="BankProject.TradingFinance.Import.DocumentaryCredit.ProvisionTransfer_DC" %>

<telerik:RadWindowManager ID="RadWindowManager2" runat="server" EnableShadow="true" />

<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });

    function checkDebitCredit() {
        var rcbDebitAccount = $find('<%= rcbDebitAccount.ClientID %>');
        var hdfCheckDebitAcc = $find('<%= hdfCheckDebitAcc.ClientID %>');
        var mySplitResult = hdfCheckDebitAcc.get_value().split(rcbDebitAccount.get_value());
        
        if (mySplitResult == null || (mySplitResult != null && mySplitResult.length == 1)) {
            radconfirm("Debit Account is not existed. Please check again!!!", confirmCallbackFunction1);
            return false;
        }

        var hdfCheckCreditAcc = $find('<%= hdfCheckCreditAcc.ClientID %>');
        var rcbCreditAccount = $find('<%= rcbCreditAccount.ClientID %>');

        if (rcbCreditAccount.get_value() != hdfCheckCreditAcc.get_value()) {
            radconfirm("Credit Account is not existed. Please check again!!!", confirmCallbackFunction1);
            return false;
        }
        return true;
    }

    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        if (button.get_commandName() == "print") {

            var tbLCNo = $("#<%= tbLCNo.ClientID %>").val();
            if (tbLCNo) {
                args.set_cancel(true);
                radconfirm("Do you want to download PHIEU CHUYEN KHOAN file?", confirmCallbackFunction_PCK, 410, 150, null, 'Download');
            }
        }

        if (button.get_commandName() == "commit") {
            if (checkDebitCredit()) {
                $('#<%= hdfDisable.ClientID %>').val(1);
            }
        }
    }

    var lastClickedItem = null;
    var clickCalledAfterRadprompt = false;
    var clickCalledAfterRadconfirm = false;
  
    function confirmCallbackFunction1(args) {
        clickCalledAfterRadconfirm = false;
        radconfirm("Print Transfer Slip?", confirmCallbackFunction2);
    }
    function confirmCallbackFunction2(args) {
        clickCalledAfterRadconfirm = false;
        radconfirm("Print Transfer Slip For Buying FCY?", confirmCallbackFunction3);
    }
    function confirmCallbackFunction3(args) {
        clickCalledAfterRadconfirm = true;
        lastClickedItem.click();

        lastClickedItem = null;
    }
    
    function confirmCallbackFunction_PCK(result) {
        if (result) {
            $("#<%=btnPCK_Report.ClientID %>").click();
        }
    }
</script>
</telerik:RadCodeBlock>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
          </telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />
<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_ButtonClick"
     OnClientButtonClicking="OnClientButtonClicking">
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
<div>
    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
        <ContentTemplate>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 200px; padding: 5px 0 5px 20px;">
                <asp:TextBox ID="tbDepositCode" runat="server" Width="200" AutoPostBack="true"  />
                <i>
                    <asp:Label ID="lblDepositCode" runat="server" /></i></td>
        </tr>
    </table>
            </ContentTemplate>
        </asp:UpdatePanel>
</div>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Provision Transfer</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
       <table width="100%" cellpadding="0" cellspacing="0">
           <tr>
               <td class="MyLable">Transactions Type</td>
               <td class="MyContent">ACPV Prov Transfer(FT)</td>
               <td></td>
           </tr>
            <tr>
                   <td class="MyLable">Type</td>
                   <td class="MyContent">
                       <telerik:RadComboBox width="150"
                            ID="rcbType" Runat="server"
                           OnSelectedIndexChanged="rcbType_SelectedIndexChanged"
                           AutoPostBack="true"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="LC" Text="Letter Of Credit" />
                                <telerik:RadComboBoxItem Value="DOC" Text="Documentary Collection" />
                            </Items>
                        </telerik:RadComboBox>
                       </td>
                   <td></td>
               </tr>
          
           <tr>
               <td class="MyLable">LC/Coll No.<span class="Required">(*)</span><asp:RequiredFieldValidator 
                runat="server" Display="None"
                ID="RequiredFieldValidator1" 
                ControlToValidate="tbLCNo" 
                        ValidationGroup="Commit"
                InitialValue="" 
                ErrorMessage="LC No. is Required" ForeColor="Red">
            </asp:RequiredFieldValidator></td>
               <td class="MyContent"><asp:TextBox ID="tbLCNo" AutoPostBack="true" runat="server" OnTextChanged="tbLCNo_TextChanged"/>
               </td>
               <td></td>
           </tr>
    
       </table>
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Debit Infomation</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
               <tr>
                   <td class="MyLable">Ordered by</td>
                   <td class="MyContent" style="width:350px;">
                       <telerik:RadComboBox   
                            AutoPostBack="False"
                            ID="rcbOrderedby" Runat="server"
                            MarkFirstMatch="True"
                            Width="350" 
                           OnItemDataBound="rcbOrderedby_ItemDataBound"
                            AllowCustomText="false" >
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                        </telerik:RadComboBox>
                   </td>
                   <td><asp:Label ID="lblOrderedby" runat="server" /></td>
               </tr>
               
                </table>
                 <table width="100%" cellpadding="0" cellspacing="0">
               <tr>
                   <td class="MyLable">Debit Currency</td>
                   <td class="MyContent" >
                       <telerik:RadComboBox width="150"
                            ID="rcbDebitCurrency" Runat="server" AutoPostBack="true"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
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
                   <td></td>
               </tr>
                <tr>
                   <td class="MyLable">Debit Account</td>
                   <td class="MyContent" style="width: 150px">
                       <telerik:RadTextBox 
                            ID="rcbDebitAccount"
                            Runat="server" 
                           AutoPostBack="True"
                           OnTextChanged="rcbDebitAccount_OnTextChanged"
                            Width="350" ></telerik:RadTextBox>
                   </td>
                    <td>
                        <asp:Label ID="lblDebitAccountName" runat="server"></asp:Label>
                    </td>
               </tr>
                
                <tr>
                   <td class="MyLable">Debit Amount</td>
                   <td class="MyContent"><telerik:RadNumericTextBox ID="tbDebitAmout" runat="server" ClientEvents-OnValueChanged="DebitAmount_OnValueChanged" NumberFormat-DecimalDigits="3" AutoPostBack="true" /></td>
               </tr>
                <tr>
                   <td class="MyLable">Debit Date</td>
                   <td class="MyContent"><telerik:RadDatePicker ID="rdpDebitDDate" AutoPostBack="true" runat="server"></telerik:RadDatePicker></td>
               </tr>
                <tr>
                   <td class="MyLable">Amount Debited</td>
                   <td class="MyContent"><telerik:RadNumericTextBox ID="txtAmountDebited" readonly="true" BorderWidth="0" runat="server"  NumberFormat-DecimalDigits="3" /></td>
               </tr>
           </table>

        </fieldset>
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Credit Infomation</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Credit Account</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadTextBox 
                            ID="rcbCreditAccount" Runat="server" 
                             Width="350" ></telerik:RadTextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblCreditAccountName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Credit Currency</td>
                    <td class="MyContent"><asp:Label ID="lblCreditCurrency" AutoPostBack="true" runat="server" /></td>
                </tr>
                </table>

               
                <table width="100%" cellpadding="0" cellspacing="0">
              
                <tr>
                    <td class="MyLable">Credit Amount</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox NumberFormat-DecimalDigits="3" incrementsettings-interceptarrowkeys="true" incrementsettings-interceptmousewheel="true"  
                            runat="server" id="tbCreditAmount"/></td>
                </tr>
                <tr>
                    <td class="MyLable">Credit Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="rdpCreditDate" runat="server"></telerik:RadDatePicker></td>
                </tr>
                <tr>
                    <td class="MyLable">Amount Credited</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox ID="txtAmountCredited" readonly="true" BorderWidth="0" runat="server"  NumberFormat-DecimalDigits="3" /></td>
                </tr>
                
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Add Remarks</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAddRemarks1" runat="server" Width="350" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtAddRemarks2" runat="server" Width="350"/>
                    </td>
                </tr>
            </table>

        </fieldset>
          </ContentTemplate>
           </asp:UpdatePanel>
    </div>

</div>
<asp:HiddenField ID="hdfDisable" runat="server" Value="0" />


<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
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
                //$(this).attr('name', 'row' + thisRow + thisName);
                //$(this).attr('id', 'row' + thisRow + thisName);
            });

    });

        <%-- $("#<%=tbDepositCode.ClientID %>").keydown(function (event) {

            if (event.keyCode == 13) {
                window.location.href = "Default.aspx?tabid=279&CodeID=" + $("#<%=tbDepositCode.ClientID %>").val();
            }
        }); --%>

        function rcbOrderedby_OnClientSelectedIndexChanged(sender, eventArgs) {
            var combo = $find('<%=rcbOrderedby.ClientID %>');
            //sender.set_text("You selected " + item.get_text());
            $('#<%=lblOrderedby.ClientID%>').html(combo.get_selectedItem().get_value());
        }

        function DebitAmount_OnValueChanged() {
            var DebitAmout = $find('<%= tbDebitAmout.ClientID %>');

            var AmountDebited = $find('<%= txtAmountDebited.ClientID %>');
            var AmountCredited = $find('<%= txtAmountCredited.ClientID %>');

            $find('<%= tbCreditAmount.ClientID %>').set_value(DebitAmout.get_value());
            AmountDebited.set_value(AmountDebited.get_value() + DebitAmout.get_value());
            AmountCredited.set_value(AmountCredited.get_value() + DebitAmout.get_value());
        }

    </script>
</telerik:RadCodeBlock>
<div style="visibility: hidden;">
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
       <ContentTemplate>
            <telerik:RadTextBox ID="hdfCheckCreditAcc" AutoPostBack="true" runat="server" ></telerik:RadTextBox>
            <telerik:RadTextBox ID="hdfCheckDebitAcc" AutoPostBack="true" runat="server" ></telerik:RadTextBox>
       </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" />
</div>
<div style="visibility:hidden;"><asp:Button ID="btnPCK_Report" runat="server" OnClick="btnPCK_Report_Click" Text="Search" /></div>

<%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbDebitAccount">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblDebitAccountName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>--%>