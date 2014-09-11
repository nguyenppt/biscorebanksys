<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OutwardTransferByCash.ascx.cs" Inherits="BankProject.FTTeller.OutwardTransferByCash" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Controls/VVComboBox.ascx" TagPrefix="uc1" TagName="VVComboBox" %>
<%@ Register Src="../Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>


<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit"  />
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });

</script>
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
      OnClientButtonClicking="OnClientButtonClicking"   OnButtonClick="RadToolBar1_ButtonClick">
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
            <asp:TextBox ID="txtId" runat="server" Width="200" />
        </td>
    </tr>
</table>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Cash Deposits Outside System</a></li>
        <li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Product ID <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator8"
                        ControlToValidate="rcbProductID"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Product ID is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                    </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbProductID"
                        MarkFirstMatch="True"
                        AllowCustomText="false" Width="220"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="rcbProductID_OnSelectedIndexChanged"
                        runat="server" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="1000" Text="1000 - Điện CMND" />
                            <telerik:RadComboBoxItem Value="3000" Text="3000 - Chuyển đi thanh toán CI-TAD" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
               
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Currency <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="rcbCurrency"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Currency is Required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbCurrency"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        OnClientSelectedIndexChanged="OnCurrencyMatch"
                        OnSelectedIndexChanged="rcbCurrency_OnSelectedIndexChanged"
                        AutoPostBack="True"
                        runat="server" >
                       
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
                <td class="MyLable">Ben Com <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator2"
                        ControlToValidate="rcbBenCom"
                        
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Ben Com is Required" ForeColor="Red"></asp:RequiredFieldValidator>
                      
                </td>
                <td class="MyContent" style="width:220px;" >
                    <telerik:RadComboBox ID="rcbBenCom"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="rcbCurrency_OnSelectedIndexChanged"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        AppendDataBoundItems="true"
                        runat="server" Width="220" >
                    </telerik:RadComboBox>
                </td>
            <td><asp:Label ID="lbBenCom" runat="server"></asp:Label></td>
            </tr>
        </table>


        <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
                <td class="MyLable">Credit Account <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator4"
                        ControlToValidate="rcbCreditAccount"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Credit Account is Required" ForeColor="Red"></asp:RequiredFieldValidator>

                </td>
                <td class="MyContent"  >
                    <telerik:RadComboBox ID="rcbCreditAccount"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        Enabled = "false"
                        runat="server" Width="160" >
                        
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
                <td class="MyLable">Cash Account</td>
                <td class="MyContent" style="width:160px;" >
                    <telerik:RadComboBox ID="rcbCashAccount"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        
                        runat="server" Width="160" >
                      
                    </telerik:RadComboBox>
                </td>
            <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            <td><asp:Label ID="Label1" runat="server"></asp:Label></td>
            </tr>
        </table>
                         </ContentTemplate>
        </asp:UpdatePanel>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Amount</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtAmountLCY" runat="server" NumberFormat-DecimalDigits="2" ></telerik:RadNumericTextBox>
                </td>
            </tr>
        </table>
        
        <fieldset id="Fieldset1" runat="server">
         <legend>
              <div style="font-weight:bold; text-transform:uppercase;"><asp:Label ID="Label2" runat="server" Text="Sending Information"></asp:Label></div>
                                </legend>    
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                 <td class="MyLable">Sending Name <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator3"
                        ControlToValidate="txtSendingName"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Sending Name is Required" ForeColor="Red"></asp:RequiredFieldValidator>

                </td>
                <td class="MyContent"><telerik:RadTextBox ID="txtSendingName" runat="server" Width="200" ></telerik:RadTextBox></td>
            </tr>
        </table>
                    <uc1:VVTextBox runat="server" id="txtSendingAddress" VVTLabel="Sending Address" VVTDataKey='txtId'  />
        <table width="100%" cellpadding="0" cellspacing="0">
                 <td class="MyLable">Sending Phone</td>
                <td class="MyContent"><telerik:RadTextBox ID="txtSendingPhone" runat="server" Width="200" ></telerik:RadTextBox></td>
        </table>
            </fieldset>
 
  <fieldset id="Fieldset2" runat="server">
         <legend>
              <div style="font-weight:bold; text-transform:uppercase;"><asp:Label ID="Label3" runat="server" Text="Receiving Information"></asp:Label></div>
                                </legend>   

       <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyContent">
                <uc1:VVTextBox runat="server" id="txtReceivingName" VVTLabel="Receiving Name" VVTDataKey='txtId'  />
                </td>
            </tr>
        </table>
      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>

      <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Ben Account</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtBenAccount" runat="server" ></telerik:RadTextBox>
                </td>
            </tr>
        </table>

         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Province</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbProvince"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        AutoPostBack="true"
                        AppendDataBoundItems="true"
                        OnSelectedIndexChanged="rcbProvince_OnSelectedIndexChanged"
                        runat="server"  >
                     
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Bank Code</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbBenCode"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" Width="400" >
                      
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

       </ContentTemplate>
      </asp:UpdatePanel>

      <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Identity Card </td>
                <td class="MyContent"><telerik:RadTextBox ID="txtIdentityCard" runat="server" Width="160"></telerik:RadTextBox></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Isssue Date 
                </td>
                <td class="MyContent"><telerik:RadDatePicker ID="txtIsssueDate" runat="server" Width="160"></telerik:RadDatePicker></td>
                <td class="MyLable">Isssue Place
                    </asp:RequiredFieldValidator>
                </td>
                <td class="MyContent"><telerik:RadTextBox ID="txtIsssuePlace" runat="server" Width="160"></telerik:RadTextBox></td>
            </tr>
        </table>

     
      </fieldset>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Teller</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtTeller" runat="server"></telerik:RadTextBox>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Narrative</td>
                <td class="MyContent" style="width:200px; ">
                    <telerik:RadTextBox ID="txtNarrative" Width="200"
                        runat="server"  />
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
        </table>
<hr />
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
          <ContentTemplate>
        <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Waive Charges?</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbWaiveCharges"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        OnSelectedIndexChanged="cmbWaiveCharges_SelectedIndexChanged"
                        autopostback="true"
                        width="50"
                        runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Value="YES" Text="YES" />
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Vat Serial</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtVatSerial" runat="server"></telerik:RadTextBox>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Charge Amt LCY</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtChargeAmtLCY" runat="server" ClientEvents-OnValueChanged="OnChargeAmountValueChanged" NumberFormat-DecimalDigits="0" ></telerik:RadNumericTextBox>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Charge Vat Amt</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtChargeVatAmt" runat="server" NumberFormat-DecimalDigits="0" ></telerik:RadNumericTextBox>
                </td>
            </tr>
        </table>

              </ContentTemplate>
            </asp:UpdatePanel>

</div>
</div>
<asp:HiddenField ID="hdfDisable" runat="server" Value="1" />

<script type="text/javascript">
    $(document).ready(
        function () {
            $('a.add').live('click',
                function () {
                    $(this)
                        .html('<img src="Icons/Sigma/Delete_16X16_Standard.png" />')
                        .removeClass('add')
                        .addClass('remove')
                    ;
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

    function BenCom_OnClientSelectedIndexChanged() {
        var CreditAccountElement = $find("<%= rcbBenCom.ClientID %>");
        var lbCrAccountElement = $('#<%= lbBenCom.ClientID%>');
        lbCrAccountElement.html("");
        if (CreditAccountElement.get_value() != "") {
            lbCrAccountElement.html("CHI NHANH DONG NAI");
        }
    }

    function ValidatorUpdateIsValid() {
        //var i;
        //for (i = 0; i < Page_Validators.length; i++) {
        //    if (!Page_Validators[i].isvalid) {
        //        Page_IsValid = false;
        //        return;
        //    }
        //}
        var ProductID = $('#<%= rcbProductID.ClientID%>').val();
        var Currency = $('#<%= rcbCurrency.ClientID%>').val();
        var BenCom = $('#<%= rcbBenCom.ClientID%>').val();
        var CreditAccount = $('#<%= rcbCreditAccount.ClientID%>').val();
        var SendingName = $('#<%= txtSendingName.ClientID%>').val();
        
        Page_IsValid = ProductID != "" && Currency != "" && BenCom != "" && CreditAccount != "" && SendingName != "";
    }

    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        if (button.get_commandName() == "Preview") {
            window.location = "Default.aspx?tabid=158&ctl=chitiet&mid=842";
            return;
        }

        return; //an khong cho hien cau thong bao
        ValidatorUpdateIsValid();
        if (Page_IsValid) {
            $('#<%= hdfDisable.ClientID%>').val(1);

            if (button.get_commandName() == "commit" && !clickCalledAfterRadconfirm) {
                args.set_cancel(true);
                lastClickedItem = args.get_item();
                var isbool = radconfirm("Ch/exess Tt Amount Vnd 10000000", confirmCallbackFunction2);
                if (isbool == false) { confirmcallfail(); }
                return;
            }

            if (button.get_commandName() == "authorize" && !clickCalledAfterRadconfirm) {
                radconfirm("Authorised Completed", confirmCallbackFunction2);
            }
        }
    }

    var lastClickedItem = null;
    var clickCalledAfterRadprompt = false;
    var clickCalledAfterRadconfirm = false;

    function confirmCallbackFunction1(args) {
        radconfirm("Unauthorised overdraft of USD on account 050001688331", confirmCallbackFunction2); //" + amtFCYDeposited + "
    }
   
    function confirmCallbackFunction2(args) {
        if (args) {
            clickCalledAfterRadconfirm = true;
            lastClickedItem.click();
            lastClickedItem = null;
        }
    }

    function confirmcallfail() {
        $('#<%= hdfDisable.ClientID%>').val(0);//cancel thi gan disable = 0 de khoi commit
        confirmCallbackFunction2();
    }

    function OnChargeAmountValueChanged() {
        var percent = 0.1;//10%
        var AmountElement = $find("<%= txtChargeAmtLCY.ClientID%>");
        var Amount = AmountElement.get_value();

        var AmountPaidElement = $find("<%= txtChargeVatAmt.ClientID%>");

        if (Amount) {
            var lcy = Amount * percent;
            AmountPaidElement.set_value(lcy);
        }
    }

    function ShowMessageCurrencyNotMath() {
        radconfirm("Currency and Ben Com is not matched", confirmCallbackFunction2);
    }

    function OnCurrencyMatch(e) {
        var currencyDepositedElement = $find("<%= rcbCurrency.ClientID %>");
        var currencyDepositedValue = currencyDepositedElement.get_value();
        var cashAccountElement = $find("<%= rcbBenCom.ClientID %>");
        var cashAccountValue = cashAccountElement.get_value();
        //if (currencyDepositedValue && cashAccountValue && (currencyDepositedValue != cashAccountValue)) {
        //    ShowMessageCurrencyNotMath();
        //    currencyDepositedElement.trackChanges();
        //    currencyDepositedElement.get_items().getItem(0).select();
        //    currencyDepositedElement.updateClientState();
        //    currencyDepositedElement.commitChanges();

        //    cashAccountElement.trackChanges();
        //    cashAccountElement.get_items().getItem(0).select();
        //    cashAccountElement.updateClientState();
        //    cashAccountElement.commitChanges();
        //    return false;
        //}

        return true;
    }
  </script>