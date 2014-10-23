<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CashRepayment.ascx.cs" Inherits="BankProject.Views.TellerApplication.CashRepayment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<telerik:RadWindowManager id="RadWindowManager1" runat="server"  EnableShadow="true" />
<asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="Commit" runat="server" ShowMessageBox="true" ShowSummary="false" />
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
            <asp:TextBox ID="tbID" runat="server" Width="200" ForeColor="Black" /> </td>
        <td> <i> <asp:Label ID="lblCashDeposit" runat="server"  ForeColor="Black" /></i></td>
    </tr>
</table>
</div>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="# blank1">Cash Deposits</a></li>
    </ul>
    <div id="blank1" class="dnnClear">
        <fieldset>
            <%--<asp:UpdatePanel ID="UpdatePanel" runat="server">
                <ContentTemplate>--%>
            <table width="100%" cellpadding ="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Customer:</td>
                    <td class="MyContent" width="100">
                        <asp:Label ID="lblCustomerID" runat="server" width="100" ForeColor="Black" ></asp:Label>
                    </td>
                    <td class="MyContent"> 
                        <asp:Label ID="lblCustomerName" runat="server" ForeColor="Black" />
                    </td>
                </tr>
                </table>
            <table width="100%" cellpadding ="0" cellspacing="0">

                <tr>
                     <td class="MyLable">Currency:<span class="Required">(*)</span>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="None" 
                             ControlToValidate="rcbCurrency" ValidationGroup="Commit" InitialValue="" ErrorMessage="Currency is required"
                             ForeColor="Red" />
                     </td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="rcbCurrency" runat="server" AllowCustomText="false" 
                             MarkFirstMatch="true" ValidationGroup="Group1"
                             AppendDataBoundItems="true" 
                             OnClientSelectedIndexChanged="rcbCustAccount_rcbCurrency_OnClientSelectedIndexChanged"
                               ForeColor="Black"   >
                        </telerik:RadComboBox>
                    </td>
                   
                </tr>
                <tr>
                     <td class="MyLable">Customer Account:<span class="Required">(*)
                          <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator1" 
                            ControlToValidate="tbCusomerAcct" ValidationGroup="Commit" InitialValue="" ErrorMessage="Customer Account is required"
                            ForeColor="Red" /></span></td>
                    <td class="MyContent" >
                        <asp:TextBox runat="server" ID="tbCusomerAcct" ValidationGroup="Group1" ForeColor="Black" ></asp:TextBox>
                       
                    </td>
                </tr>
                 <tr>
                     <td class="MyLable">Balance Amount:</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox runat="server" id="tbBalanceAmt" readonly="true" Borderwidth="0"  ForeColor="Black" ></telerik:radnumerictextbox>
                    </td>
                </tr>
                 <tr>
                     <td class="MyLable">New Balance Amount:</td>
                    <td class="MyContent">
                         <telerik:radnumerictextbox runat="server" id="tbNewBalanceAmt" readonly="true" Borderwidth="0"  ForeColor="Black" ></telerik:radnumerictextbox>
                    </td>
                </tr>
                 <tr>
                     <td class="MyLable">Teller ID:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator2"
                     ControlToValidate="tbTellerID" ValidationGroup="Commit" InitialValue="" ErrorMessage="Teller ID is required"
                    ForeColor="Red" />
                     </td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbTellerID" runat="server" ValidationGroup="Group1" ForeColor="Black" ></telerik:RadTextBox>
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
                            runat="server"  ForeColor="Black" 
                            AllowCustomText="false" 
                            AppendDataBoundItems="true" 
                            MarkFirstMatch="true"
                            AutoPostBack="true" 
                            OnSelectedIndexChanged="rcbCurrencyDeposited_rcbCurrencyDeposited"
                            >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
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
                             />
                     </td>
                    <td class="MyContent" width="390">
                        <telerik:RadComboBox ID="rcbCashAccount" runat="server" 
                            MarkFirstMatch="true" AllowCustomText="false" 
                            ValidationGroup="Group1"   ForeColor="Black" 
                             AppendDataBoundItems="true"  
                            AutoPostBack="false" ></telerik:RadComboBox> 
                    </td>
                    <td class="MyLable"></td>
                </tr>
            </table>
                  <%-- </ContentTemplate>
           </asp:UpdatePanel>--%>
            <table width="100%" cellpadding="0" cellspacing="0" >
                <tr>
                     <td class="MyLable">Amt LCY Deposited:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" Display="None" 
                             ControlToValidate="tbAmtLCYDeposited" 
                             ValidationGroup="Commit" InitialValue="" ErrorMessage="Amount LCY Deposited is required"
                             ForeColor="Red" />

                     </td>
                    <td class="MyContent" width="350">
                        <telerik:RadNumericTextBox ID="tbAmtLCYDeposited" runat="server" ValidationGroup="Group1"  ForeColor="Black" 
                            ClientEvents-OnValueChanged="tbAmtLCYDeposited_thanhtien" ></telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                     <td class="MyLable">Next Tran Com:</td>
                </tr>
                <tr>
                     <td class="MyLable">Deal Rate:</td>
                    <td class="MyContent">
                          <telerik:RadNumericTextBox ID="tbDealRate" runat="server" ValidationGroup="Group1" NumberFormat-DecimalDigits="5" ForeColor="Black" 
                              ClientEvents-OnValueChanged="tbAmtLCYDeposited_thanhtien"></telerik:RadNumericTextBox>
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
                 <tr>
                     <td class="MyLable">Waive Charges:</td>
                    <td class="MyContent">
                         <telerik:RadComboBox ID="rcbWaiveCharge" 
                        AppendDataBoundItems="true"  ForeColor="Black" 
                        MarkFirstMatch="True" AllowCustomText="false" runat="server" ValidationGroup="Group1">
                        <Items>
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
                        <telerik:RadTextBox ID="tbNarrative" runat="server" width="390" ForeColor="Black" />
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                     <td class="MyLable"></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbNarrative2" runat="server" width="390" ForeColor="Black" />
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                    </td>
                </tr>
                </table>
            <table width="100%" cellpadding="0" cellspacing="0" >
                <tr>
                     <td class="MyLable">Print Ln No of PS:</td>
                    <td class="MyContent">
                         <telerik:RadNumericTextBox ID="tbPrint" runat="server" NumberFormat-DecimalDigits="0" ForeColor="Black"  />
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
        var CustAccount = $('#<%=tbCusomerAcct.ClientID%>').val();
        var CurrencyElement = $find("<%=rcbCurrency.ClientID%>");
        var Currency = CurrencyElement.get_value();
        var CustomerIDElement = $('#<%=lblCustomerID.ClientID%>');
        var CustomerNameElement = $('#<%=lblCustomerName.ClientID%>');
        var BalanceAmt = $find("<%=tbBalanceAmt.ClientID%>");
        var NewBalanceAmt = $find("<%=tbNewBalanceAmt.ClientID%>");
        
        if (Currency.length != 0) {
            if (CustAccount.length ==0 || !CustAccount.trim()) {
                CustomerIDElement.html("");
                CustomerNameElement.html("");
                BalanceAmt.set_value("");
                NewBalanceAmt.set_value("");
            }
            else {
                //CustomerIDElement.html(CustAccountElement.get_selectedItem().get_attributes().getAttribute("CustomerID"));
                //CustomerNameElement.html(CustAccountElement.get_selectedItem().get_attributes().getAttribute("CustomerName"));
                tbAmtLCYDeposited_thanhtien();
            }
        }
        else
        {
            CustomerIDElement.html("");
            CustomerNameElement.html("");
            BalanceAmt.set_value("");
            NewBalanceAmt.set_value("");
            $find("<%=tbAmtLCYDeposited.ClientID%>").set_value("");
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
        var CustomerIDElement = $('#<%=lblCustomerID.ClientID%>');
        var CustomerNameElement = $('#<%=lblCustomerName.ClientID%>');
        var BalanceAmt = $find("<%=tbBalanceAmt.ClientID%>");
        var NewBalanceAmt = $find("<%=tbNewBalanceAmt.ClientID%>");
        var CustAccount = $('#<%=tbCusomerAcct.ClientID%>').val();
        var CurrencyDepositElement = $find("<%=rcbCurrencyDeposited.ClientID%>");
        var CurrencyDeposit = CurrencyDepositElement.get_value();
        var CurrencyElement = $find("<%=rcbCurrency.ClientID%>");
        var Currency = CurrencyElement.get_value();
        
        //var CustAccount = CustAccountElement.get_value();
        var CashAccountElement = $find("<%=rcbCashAccount.ClientID%>");
        var CashAccount = CashAccountElement.get_value();
        if (CashAccount && CustAccount && CurrencyDeposit && Currency) // && AmtDeposit
        {
            var DealRate = get_dealrate();
            var AmtDepositElement = $find("<%=tbAmtLCYDeposited.ClientID%>");
            var AmtDeposit = AmtDepositElement.get_value();
            var temAmtPaid = BalanceAmt.get_value() - AmtDeposit;
            if (temAmtPaid >= 0) {
                NewBalanceAmt.set_value(temAmtPaid.toLocaleString("en-US"));
            } else {
                showMessage();
                AmtDepositElement.set_value("");
            }
        } else
        {
            CustomerIDElement.html("");
            CustomerNameElement.html("");
            BalanceAmt.set_value("");
            NewBalanceAmt.set_value("");
            $find("<%=tbAmtLCYDeposited.ClientID%>").set_value("");
        };
    }
    function showMessage()
    {
        var BalanceAmt = $find("<%=tbBalanceAmt.ClientID%>").get_value();
        var Currency = $find("<%=rcbCurrency.ClientID%>").get_value();
        radconfirm("Can not Over Draf . Maxium Balance Amount Value is " + BalanceAmt.toLocaleString("en-US") + " " + Currency, confirmCallbackFunction2)
    }
    function confirmCallbackFunction2(args) {
        clickCalledAfterRadconfirm = true;
        lastclickedItem.click();
        lastclickedItem = null;
        var AmtLCYDeposited = $find("<%= tbAmtLCYDeposited.ClientID%>");
        AmtLCYDeposited.focus();
        AmtLCYDeposited.set_value("");
    }
    $('#<%=tbCusomerAcct.ClientID%>').keyup(function (event) {

        if (event.keyCode == 13) {
            $("#<%=btAccountCust.ClientID%>").click();
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
</telerik:RadCodeBlock>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbCurrencyDeposited">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="rcbCashAccount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<div style="visibility:hidden;">
    <asp:Button ID="btAccountCust" runat="server" Text="AccountCust" OnClick="btAccountCust_Click1" />
</div>