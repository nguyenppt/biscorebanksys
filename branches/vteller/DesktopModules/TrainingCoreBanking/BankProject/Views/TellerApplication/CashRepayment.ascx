<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CashRepayment.ascx.cs" Inherits="BankProject.Views.TellerApplication.CashRepayment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<telerik:RadWindowManager id="RadWindowManager1" runat="server"  EnableShadow="true" />
<asp:ValidationSummary ID="ValidationSummary" ValidationGroup="Commit" runat="server" ShowMessageBox="true" ShowSummary="false" />

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>
<div>
    <telerik:RadToolBar ID="RadToolBar" runat="server" ValidationGroup="Group1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
       OnButtonClick="OnRadToolBarClick" >
        <Items>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit" />
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
<div >
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="tbID" runat="server" Width="200"/> </td>
        <td> <i> <asp:Label ID="lblCashDeposit" runat="server" /></i></td>
    </tr>
</table>
</div>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="# blank1">Cash Deposits</a></li>
        <li><a href="#blank2">Audit</a></li>
        <li><a href="#blank3">Full Preview</a></li>
    </ul>
    <div id="blank1" class="dnnClear">
        <fieldset>
            <%--<asp:UpdatePanel ID="UpdatePanel" runat="server">
                <ContentTemplate>--%>
            <table width="100%" cellpadding ="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Customer:</td>
                    <td class="MyContent" width="100">
                        <asp:Label ID="lblCustomerID" runat="server" width="100"></asp:Label>
                    </td>
                    <td class="MyContent"  width="390"> 
                        <asp:Label ID="lblCustomerName" runat="server"/>
                    </td>
                </tr>
                <tr>
                     <td class="MyLable">Currency:<span class="Required">(*)</span>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="None" 
                             ControlToValidate="rcbCurrency" ValidationGroup="Commit" InitialValue="" ErrorMessage="Currency is required"
                             ForeColor="Red" />
                     </td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="rcbCurrency" runat="server" AllowCustomText="false" 
                             MarkFirstMatch="true" ValidationGroup="Group1"
                             AppendDataBoundItems="true" AutoPostBack="true" 
                             OnSelectedIndexChanged="rcbCurrency_OnSelectedIndexChanged" 
                             OnClientSelectedIndexChanged="rcbCustAccount_rcbCurrency_OnClientSelectedIndexChanged"
                                >
                         <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <%--<telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                            <telerik:RadComboBoxItem Value="GBP" Text="GBP" />
                            <telerik:RadComboBoxItem Value="JPY" Text="JPY" />--%>
                            <telerik:RadComboBoxItem Value="USD" Text="USD" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND" />
                        </Items>
                        </telerik:RadComboBox>
                    </td>
                   
                </tr>
                <tr>
                     <td class="MyLable">Customer Account:<span class="Required">(*)
                          <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator1" 
                            ControlToValidate="rcbCustAccount" ValidationGroup="Commit" InitialValue="" ErrorMessage="Customer Account is required"
                            ForeColor="Red" /></span></td>
                    <td class="MyContent" >
                        <telerik:RadComboBox ID="rcbCustAccount" runat="server" 
                            MarkFirstMatch="true" 
                            AllowCustomText="false" 
                            ValidationGroup="Group1"
                            AutoPostBack="false" width="390"  
                            OnItemDataBound="rcbCustAccount_OnItemDataBound"
                            OnSelectedIndexChanged="rcbCustAccount_OnSelectedIndexChanged"
                            OnClientSelectedIndexChanged="rcbCustAccount_rcbCurrency_OnClientSelectedIndexChanged" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
                 <tr>
                     <td class="MyLable">Old Cust Account:</td>
                    <td class="MyContent">
                        <asp:Label ID="lblOldCustAccount" runat="server" />
                    </td>
                </tr>
                 
                 <tr>
                     <td class="MyLable">Amt Paid to Cust:</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAmtpaidToCust" runat="server" />
                    </td>
                </tr>
                 <tr>
                     <td class="MyLable">New Cust Balance:</td>
                    <td class="MyContent">
                        <asp:Label ID="lblNewCustBalance" runat="server" />
                    </td>
                </tr>
                 <tr>
                     <td class="MyLable">Teller ID:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator2"
                     ControlToValidate="tbTellerID" ValidationGroup="Commit" InitialValue="" ErrorMessage="Teller ID is required"
                    ForeColor="Red" />
                     </td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbTellerID" runat="server" ValidationGroup="Group1"></telerik:RadTextBox>
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
            </table>
                    <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </fieldset>
        <fieldset>
           <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
               <ContentTemplate>--%>
            <table width="100%" cellpadding="0" cellspacing="0" >
                <tr>
                     <td class="MyLable">Currency Deposited:<span class="Required">(*)
                          <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator3"
                     ControlToValidate="rcbCurrencyDeposited" ValidationGroup="Commit" InitialValue="" ErrorMessage="Currency Deposited is required"
                    ForeColor="Red" /></span>
                     </td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                            ID="rcbCurrencyDeposited" 
                            runat="server" 
                            AllowCustomText="false" 
                            AppendDataBoundItems="true" 
                            MarkFirstMatch="true"
                            AutoPostBack="true" 
                            OnSelectedIndexChanged="rcbCurrencyDeposited_rcbCurrencyDeposited"
                            OnClientSelectedIndexChanged="rcbCustAccount_rcbCurrency_OnClientSelectedIndexChanged" >
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
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
                <tr>
                     <td class="MyLable">Cash Account:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None" 
                             ControlToValidate="rcbCashAccount" 
                             ValidationGroup="Commit" InitialValue="" ErrorMessage="Currency is required"
                             ForeColor="Red" 
                             OnClientSelectedIndexChanged="rcbCustAccount_rcbCurrency_OnClientSelectedIndexChanged" />
                     </td>
                    <td class="MyContent" width="390">
                        <telerik:RadComboBox ID="rcbCashAccount" runat="server" 
                            MarkFirstMatch="true" AllowCustomText="false" 
                            ValidationGroup="Group1"
                            width="390" AppendDataBoundItems="true"  
                            AutoPostBack="false" ></telerik:RadComboBox> 
                    </td>
                    <td class="MyLable"><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a> </td>
                </tr>
            </table>
                  <%-- </ContentTemplate>
           </asp:UpdatePanel>--%>
            <table width="100%" cellpadding="0" cellspacing="0" >
                <tr>
                     <td class="MyLable">Amt LCY Deposited:</td>
                    <td class="MyContent" width="350">
                        <telerik:RadNumericTextBox ID="tbAmtLCYDeposited" runat="server" ValidationGroup="Group1" 
                            ClientEvents-OnValueChanged="tbAmtLCYDeposited_thanhtien" ></telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                     <td class="MyLable">Next Tran Com:</td>
                </tr>
                <tr>
                     <td class="MyLable">Deal Rate:</td>
                    <td class="MyContent">
                          <telerik:RadNumericTextBox ID="tbDealRate" runat="server" ValidationGroup="Group1" NumberFormat-DecimalDigits="5"
                              ClientEvents-OnValueChanged="tbAmtLCYDeposited_thanhtien"></telerik:RadNumericTextBox>
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
                 <tr>
                     <td class="MyLable">Waive Charges:</td>
                    <td class="MyContent">
                         <telerik:RadComboBox ID="rcbWaiveCharge" 
                        AppendDataBoundItems="true" 
                        MarkFirstMatch="True" AllowCustomText="false" runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="YES" Text="YES" />
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                        </Items>
                    </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0" >
                <tr>
                     <td class="MyLable">Narrative:</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbNarrative" runat="server" width="390"/><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a>
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyLable"></td>
                    
                </tr>
                </table>
            <table width="100%" cellpadding="0" cellspacing="0" >
                <tr>
                     <td class="MyLable">Print Ln No of PS:</td>
                    <td class="MyContent">
                         <telerik:RadNumericTextBox ID="tbPrint" runat="server" NumberFormat-DecimalDigits="0" /><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a>
                    </td>
                    <td class="MyLable" width="150">
                        
                    </td>
                     <td class="MyLable"></td>
                </tr>
                </table>
        </fieldset>
    </div>
</div>
<telerik:RadCodeBlock runat="server">
<script type="text/javascript">
    function rcbCustAccount_rcbCurrency_OnClientSelectedIndexChanged()
    {
                                
        var CustAccountElement = $find("<%=rcbCustAccount.ClientID%>");
        var CustAccount = CustAccountElement.get_value();
        var CurrencyElement = $find("<%=rcbCurrency.ClientID%>");
        var Currency = CurrencyElement.get_value();
        var CustomerIDElement = $('#<%=lblCustomerID.ClientID%>');
        var CustomerNameElement = $('#<%=lblCustomerName.ClientID%>');
        var AmtPaidtoCustElement = $('#<%=lblAmtpaidToCust.ClientID%>');
        var NewCustBalElement = $('#<%=lblNewCustBalance.ClientID%>');
        if (Currency.length != 0) {
            if (CustAccount.length == 0 || !CustAccount.trim()) {
                CustomerIDElement.html("");
                CustomerNameElement.html("");
                AmtPaidtoCustElement.html("");
                NewCustBalElement.html("");
            }
            else {
                CustomerIDElement.html(CustAccountElement.get_selectedItem().get_attributes().getAttribute("CustomerID"));
                CustomerNameElement.html(CustAccountElement.get_selectedItem().get_attributes().getAttribute("CustomerName"));
                tbAmtLCYDeposited_thanhtien();
            }
        }
        else
        {
            CustomerIDElement.html("");
            CustomerNameElement.html("");
            AmtPaidtoCustElement.html("");
            NewCustBalElement.html("");
        }
    }
    /// tinh toan deal rate
    function get_dealrate()
    {
        var CurrencyDepositElement = $find("<%=rcbCurrencyDeposited.ClientID%>");
        var CurrencyDeposit = CurrencyDepositElement.get_value();
        var CurrencyElement = $find("<%=rcbCurrency.ClientID%>");
        var Currency = CurrencyElement.get_value();
        var DealRateElement = $find("<%=tbDealRate.ClientID%>");
        var DealRate = DealRateElement.get_value();
        if (!Currency || !CurrencyDeposit || (Currency && CurrencyDeposit && !DealRate)) { DealRate = 0; }
        if (Currency && CurrencyDeposit && (CurrencyDeposit == Currency)) { DealRate = 1; }
        if (Currency && CurrencyDeposit && (CurrencyDeposit != Currency)) { DealRate = DealRateElement.get_value();}
        return DealRate;
    }
    ////// tinh toan tien cho khach hang
    function tbAmtLCYDeposited_thanhtien()
    {

        var AmtPaidtoCustElement = $('#<%=lblAmtpaidToCust.ClientID%>');
        var NewCustBalElement = $('#<%=lblNewCustBalance.ClientID%>');
        
        var CurrencyDepositElement = $find("<%=rcbCurrencyDeposited.ClientID%>");
        var CurrencyDeposit = CurrencyDepositElement.get_value();
        var CurrencyElement = $find("<%=rcbCurrency.ClientID%>");
        var Currency = CurrencyElement.get_value();
        var CustAccountElement = $find("<%=rcbCustAccount.ClientID%>");
        var CustAccount = CustAccountElement.get_value();
        var CashAccountElement = $find("<%=rcbCashAccount.ClientID%>");
        var CashAccount = CashAccountElement.get_value();
        if (CashAccount && CustAccount && CurrencyDeposit && Currency) // && AmtDeposit
        {
            var DealRate = get_dealrate();
            var AmtDepositElement = $find("<%=tbAmtLCYDeposited.ClientID%>");
            var AmtDeposit = AmtDepositElement.get_value();
            var temAmtPaid = DealRate * AmtDeposit;

            AmtPaidtoCustElement.html(temAmtPaid.toLocaleString("en-US"));
            NewCustBalElement.html(temAmtPaid.toLocaleString("en-US"));
        } else return false;
    }

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
</telerik:RadCodeBlock>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>

        <telerik:AjaxSetting AjaxControlID="rcbCurrency">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="rcbCustAccount" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="rcbCurrencyDeposited">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="rcbCashAccount" />
            </UpdatedControls>
        </telerik:AjaxSetting>

    </AjaxSettings>
</telerik:RadAjaxManager>
