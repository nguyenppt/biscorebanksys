<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArrearNewDeposit.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Open.ArrearNewDeposit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>
<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
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
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 200px; padding: 5px 0 5px 20px;">
                <asp:TextBox ID="tbDepositCode" runat="server" Width="200" />
                <i>
                    <asp:Label ID="lblDepositCode" runat="server" /></i></td>
        </tr>
    </table>
</div>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">New Deposit - Term Savings</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Acct No
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="radAcctNo" runat="server" Text="" Width ="300">
                        <ClientEvents OnKeyPress="radAcctNo_OnKeyPress" />
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Payment CCY
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox
                            ID="rcbPaymentCcy" runat="server" 
                            MarkFirstMatch="True" Width="50" Height="150px"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                                <telerik:RadComboBoxItem Value="GBP" Text="GBP" />
                                <telerik:RadComboBoxItem Value="JPY" Text="JPY" />
                                <telerik:RadComboBoxItem Value="USD" Text="USD" />
                                <telerik:RadComboBoxItem Value="VND" Text="VND" />
                            </Items>
                        </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">For Teller
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbTeller" runat="server" Text="" Width="50">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Debit Account
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox
                            ID="rcbDebitAmmount" runat="server" 
                            MarkFirstMatch="True" Width="200" Height="150px"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="0" Text="" />
                                <telerik:RadComboBoxItem Value="2" Text="EUR-10001-0296-2147" />
                                <telerik:RadComboBoxItem Value="3" Text="GBP-10001-0296-3258" />
                                <telerik:RadComboBoxItem Value="4" Text="JPY-10001-0296-1155" />
                                <telerik:RadComboBoxItem Value="5" Text="USD-10001-0296-3661" />
                                <telerik:RadComboBoxItem Value="1" Text="VND-10001-0296-2691" />
                            </Items>
                        </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Narative
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbNarative" runat="server" Text="" Width ="300">
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;"> </div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Account CCy</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblAccountCCy"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Customer ID</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblCustomerID"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Account No</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblAccountNo"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Account Lcy</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblAccountLcy"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Account Fcy</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblAccountFcy"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Narrative</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblAZDepodit" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Narrative</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblCurrency" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Narrative</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblAmmount" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Narrative</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblDate" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Deal Rate</td>
                    <td class="MyContent">
                        <telerik:RadNumericTextBox runat="server" ID="tbDealRate" MinValue="0"   Width= "100"
                            MaxValue="999999999999" >
                            <NumberFormat GroupSeparator="," DecimalDigits="2" AllowRounding="true"   KeepNotRoundedValue="false"  />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Account In LCY</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblAmountInLCY" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
        
    </div>

</div>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"
    DefaultLoadingPanelID="AjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbCustomerID">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblCustomer" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function radAcctNo_OnKeyPress(sender, eventArgs) {
            var Currency = $('#<%= hdfCurrency.ClientID %>').val();
            var CurrencyText = $('#<%= hdfCurrencyText.ClientID %>').val();
            var CustomerID = $('#<%= hdfCustomerID.ClientID %>').val();
            var Account = $('#<%= hdfAccount.ClientID %>').val();
            var AccountText = $('#<%= hdfAccountText.ClientID %>').val();
            var DateText = $('#<%= hdfDateText.ClientID %>').val();

            $find('<%=rcbPaymentCcy.ClientID %>').set_text(Currency);
            $find("<%= tbTeller.ClientID %>").set_value("0296");
            $find('<%=rcbDebitAmmount.ClientID %>').set_text("VND-10001-0296-2691");
            $('#<%=lblAccountLcy.ClientID%>').text(Currency); 
            $('#<%=lblCurrency.ClientID%>').text(CurrencyText); 
            $('#<%=lblCustomerID.ClientID%>').text(CustomerID); 
            $('#<%=lblAccountNo.ClientID%>').text("07.0001688380.");
            $('#<%=lblAccountLcy.ClientID%>').text(Account); 
            $('#<%=lblAmountInLCY.ClientID%>').text(Account); 
            $('#<%=lblAmmount.ClientID%>').text(AccountText); 
            $('#<%=lblDate.ClientID%>').text(DateText);

            $('#<%=lblAZDepodit.ClientID%>').text("AZ Deposit Credit");

            
        }
        
        $("#<%=tbDepositCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btSearch.ClientID %>").click();
            }
      });
    </script>
</telerik:RadCodeBlock>
<div style="visibility: hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" />
</div>

<asp:HiddenField ID="hdfCurrency" runat="server" Value="0" />
<asp:HiddenField ID="hdfCurrencyText" runat="server" Value="0" />
<asp:HiddenField ID="hdfCustomerID" runat="server" Value="0" />
<asp:HiddenField ID="hdfAccount" runat="server" Value="0" />
<asp:HiddenField ID="hdfAccountText" runat="server" Value="0" />
<asp:HiddenField ID="hdfDateText" runat="server" Value="0" />
