<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreclosureWithDraw.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Close.PreclosureWithDraw" %>
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
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/cursor_drag_hand_icon.png"
            ToolTip="Hold" Value="btHold" CommandName="hold">
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
        <li><a href="#WithdrawalTerm Savings">Withdrawal - Term Savings</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />
        <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Payment No</td>
                <td class="MyContent">
                    <asp:Label runat="server" ID="lblTTPaymentNo"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Currency</td>
                <td class="MyContent">
                    <asp:Label runat="server" ID="lblCurrency"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Customer</td>
                <td class="MyContent">
                    <asp:Label runat="server" ID="lblCustomer"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Dr Account
                </td>
                <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cmbDrAccount" runat="server" MarkFirstMatch="True" Width="150" Height="150px" AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="1" Text="07.000168836.8" />
                                <telerik:RadComboBoxItem Value="1" Text="07.000164412.7" />
                            </Items>
                        </telerik:RadComboBox>
                        <i><asp:Label runat="server" ID="lblDrAccName"></asp:Label></i>
                    </td>
            </tr>
            <tr>
                <td class="MyLable">Amount Lcy
                </td>
                <td class="MyContent">
                        <telerik:RadNumericTextBox runat="server" ID="numAcmountLcy" MinValue="0"    
                            MaxValue="999999999999" >
                            <NumberFormat GroupSeparator="," DecimalDigits="0" AllowRounding="true"   KeepNotRoundedValue="false"  />
                            <ClientEvents OnBlur="numAcmountLcy_OnBlur" />
                        </telerik:RadNumericTextBox>
                    </td>
            </tr>
            <tr>
                <td class="MyLable">Amount Fcy
                </td>
                <td class="MyContent">
                        <telerik:RadNumericTextBox runat="server" ID="numAcmountFcy" MinValue="0"    
                            MaxValue="999999999999" >
                            <NumberFormat GroupSeparator="," DecimalDigits="0" AllowRounding="true"   KeepNotRoundedValue="false"  />
                        </telerik:RadNumericTextBox>
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
            <tr>
                <td class="MyLable">New Customer Balance</td>
                <td class="MyContent">
                    <asp:Label runat="server" ID="lblNewCustBal"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Amount Paid to Customer</td>
                <td class="MyContent">
                    &nbsp;<asp:Label runat="server" ID="lblAmtPaidToCust"></asp:Label>
                </td>
            </tr>
        </table>
        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Deal Rate</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox runat="server" ID="tbDealRate" MinValue="0"   Width= "100"
                        MaxValue="999999999999" >
                        <NumberFormat GroupSeparator="," DecimalDigits="2" AllowRounding="true"   KeepNotRoundedValue="false"  />
                    </telerik:RadNumericTextBox>
                </td>
            </tr>
        </table>
        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">For Teller
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbTeller" runat="server" Text="" Width="50">
                    </telerik:RadTextBox>
                    <i><asp:Label runat="server" ID="lblTellerName"></asp:Label></i>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Credit CCY
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox onclientblur="cmbCreditCCY_Onclientkeypressing" 
                        ID="cmbCreditCCY" runat="server" 
                        MarkFirstMatch="True" Width="200" Height="150px"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="1" Text="EUR" />
                            <telerik:RadComboBoxItem Value="2" Text="GBP" />
                            <telerik:RadComboBoxItem Value="3" Text="JPY" />
                            <telerik:RadComboBoxItem Value="4" Text="USD" />
                            <telerik:RadComboBoxItem Value="5" Text="VND" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <%--<tr>
                <td class="MyLable">Smartbank Branch
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox
                            ID="cmbSmartBank" runat="server" 
                            MarkFirstMatch="True" Width="200" Height="150px"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="0" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                </td>
            </tr>--%>
            <tr>
                <td class="MyLable">Credit Account
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox
                            ID="cmbCreditAcc" runat="server" 
                            MarkFirstMatch="True" Width="200" Height="150px"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="1" Text="EUR-30001-2054-3210" />
                                <telerik:RadComboBoxItem Value="2" Text="GBP-40001-2054-4242" />
                                <telerik:RadComboBoxItem Value="3" Text="JPY-50001-2054-6428" />
                                <telerik:RadComboBoxItem Value="4" Text="USD-20001-2054-1221" />
                                <telerik:RadComboBoxItem Value="5" Text="VND-10001-0695-1221" />
                            </Items>
                        </telerik:RadComboBox>
                        <i><asp:Label runat="server" ID="lblCreditAccName"></asp:Label></i>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Print Ln No of
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbPrintLnNoOf" runat="server" Text="" Width="200">
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>
        <hr />
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
        $(document).ready(function () {
            var AccountLcy = $('#<%= numAcmountLcy.ClientID %>').val();

            $('#<%=lblNewCustBal.ClientID%>').text("-" + AccountLcy);
            $('#<%=lblAmtPaidToCust.ClientID%>').text(AccountLcy);

        });

        function numAcmountLcy_OnBlur(sender, eventArgs) {
            var AccountLcy = $('#<%= numAcmountLcy.ClientID %>').val();
            var Currency = $('#<%= lblCurrency.ClientID %>').val();
            var CreditCCY = $find("<%= cmbCreditCCY.ClientID %>");
            var CreditAcc = $find("<%= cmbCreditAcc.ClientID %>");

            $('#<%=lblNewCustBal.ClientID%>').text("-" + AccountLcy);
            $('#<%=lblAmtPaidToCust.ClientID%>').text(AccountLcy);
            CreditCCY.set_text(CreditCCY.get_items().getItem(5).select());
            CreditAcc.set_text(CreditAcc.get_items().getItem(4).select());
            $('#<%=lblCreditAccName.ClientID%>').text("RECORD.AUTOMATICALLY.OF");

            var PrintLnNoOf = $find("<%= tbPrintLnNoOf.ClientID %>");
            PrintLnNoOf.set_value("1");
        }

        

        function cmbCreditCCY_Onclientkeypressing(sender, eventArgs) {
            var PreclosureType = $('#<%= hdfPreclosureType.ClientID %>').val();
            if (PreclosureType == "1") {
                $('#<%=lblCreditAccName.ClientID%>').text("Cash on Hand At");
            }

            var CreditCCY = $find('<%=cmbCreditCCY.ClientID %>').get_selectedItem().get_value(); 
            var CreditAcc = $find("<%= cmbCreditAcc.ClientID %>");

            CreditAcc.set_text(CreditAcc.get_items().getItem(Number(CreditCCY) - 1).select());

        }

    </script>
</telerik:RadCodeBlock>

<asp:HiddenField ID="hdfPreclosureType" runat="server" Value="0" />
<asp:HiddenField ID="hdfTotalAmt" runat="server" Value="0" />

