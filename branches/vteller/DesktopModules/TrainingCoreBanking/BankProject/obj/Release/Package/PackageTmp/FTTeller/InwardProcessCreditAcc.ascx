<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InwardProcessCreditAcc.ascx.cs" Inherits="BankProject.FTTeller.InwardProcessCreditAcc" %>
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
       OnClientButtonClicking="OnClientButtonClicking" OnButtonClick="RadToolBar1_ButtonClick" >
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
        <li><a href="#ChristopherColumbus">Account Transfer</a></li>
        <li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Clearing ID <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator8"
                        ControlToValidate="rcbClearingID"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Clearing ID is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                    </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbClearingID"
                        MarkFirstMatch="True"
                        AllowCustomText="false" Width="220"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="rcbClearingID_OnSelectedIndexChanged"
                        runat="server" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="1" Text="OW0906130000158" />
                            <telerik:RadComboBoxItem Value="2" Text="OW0906130000159" />
                            <telerik:RadComboBoxItem Value="3" Text="OW0906130000160" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
               
            </tr>
        </table>


        <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Debit Currency
                    </td>
                <td class="MyContent">
                    <asp:Label ID="lbDebitCurrency" runat="server"></asp:Label>
                </td>
            </tr>
        </table>

         <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Debit Account
                    </td>
                <td class="MyContent">
                    <asp:Label ID="lbDebitAccount" runat="server"></asp:Label>
                </td>
            </tr>
        </table>

         <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Debit Amt LCY
                    </td>
                <td class="MyContent">
                    <asp:Label ID="lbDebitAmtLCY" runat="server"></asp:Label>
                </td>
                 <td class="MyLable">Debit Amt FCY
                    </td>
                <td class="MyContent">
                    <asp:Label ID="lbDebitAmtFCY" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
                        
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Deal rate</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtDealRate" runat="server" NumberFormat-DecimalDigits="0" ></telerik:RadNumericTextBox>
                </td>
            </tr>
        </table>
          <fieldset id="Fieldset3" runat="server">
         <legend>
              <div style="font-weight:bold; text-transform:uppercase;"><asp:Label ID="Label1" runat="server" Text="Credit Information"></asp:Label></div>
                                </legend>    
        <table width="100%" cellpadding="0" cellspacing="0">
             <tr>
                <td class="MyLable">Credit Currency
                    </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbCreditCurrency"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" >
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
                        AppendDataBoundItems="true"
                        runat="server" Width="350" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="USD" Text="USD10001.1221.1311" />
                            <telerik:RadComboBoxItem Value="EUR" Text="EUR10001.1221.1311" />
                            <telerik:RadComboBoxItem Value="GBP" Text="GBP10001.1221.1311" />
                            <telerik:RadComboBoxItem Value="JPY" Text="JPY10001.1221.1311" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND10001.1221.1311" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
            
        
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Cr Amt LCY</td>
                <td class="MyContent">
                    <asp:Label ID="lbCrAmtLCY" runat="server"></asp:Label>
                </td>

                <td class="MyLable">Cr Amt FCY</td>
                <td class="MyContent">
                    <asp:Label ID="lbCrAmtFCY" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
                       
         </fieldset>

        <fieldset id="Fieldset1" runat="server">
         <legend>
              <div style="font-weight:bold; text-transform:uppercase;"><asp:Label ID="Label2" runat="server" Text="Sending Information"></asp:Label></div>
                                </legend>    
           
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                 <td class="MyLable">BO Name</td>
                <td class="MyContent"><telerik:RadTextBox ID="txtBOName" runat="server" Width="350" ></telerik:RadTextBox></td>
            </tr>
        </table>
                                              
            </fieldset>
 
  <fieldset id="Fieldset2" runat="server">
         <legend>
              <div style="font-weight:bold; text-transform:uppercase;"><asp:Label ID="Label3" runat="server" Text="Beneficiary Information"></asp:Label></div>
                                </legend>   

                           <uc1:VVTextBox runat="server" id="txtFOName" Width="350" VVTLabel="FO Name" VVTDataKey='txtId'  />

    
      <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Legal ID</td>
                <td class="MyContent"><telerik:RadTextBox ID="txtIdentityCard" runat="server" Width="160"></telerik:RadTextBox></td>
                <td class="MyLable">Isssue Date</td>
                <td class="MyContent"><telerik:RadDatePicker ID="txtIsssueDate" runat="server" Width="160"></telerik:RadDatePicker></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Tel</td>
                <td class="MyContent"><telerik:RadTextBox ID="txtTel" runat="server" Width="160"></telerik:RadTextBox></td>
                <td class="MyLable">Isssue Place</td>
                <td class="MyContent"><telerik:RadTextBox ID="txtIsssuePlace" runat="server" Width="160"></telerik:RadTextBox></td>
            </tr>
        </table>
      </fieldset>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Narrative <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="txtNarrative"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Narrative is Required" ForeColor="Red"></asp:RequiredFieldValidator>

                </td>
                <td class="MyContent" style="width:350px; ">
                    <telerik:RadTextBox ID="txtNarrative" Width="350"
                        runat="server"  />
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
        </table>

                  </ContentTemplate>
        </asp:UpdatePanel>
</div>
</div>
<asp:HiddenField ID="hdfDisable" runat="server" Value="0" />
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

    function ValidatorUpdateIsValid() {
        //var i;
        //for (i = 0; i < Page_Validators.length; i++) {
        //    if (!Page_Validators[i].isvalid) {
        //        Page_IsValid = false;
        //        return;
        //    }
        //}
        var ClearingID = $('#<%= rcbClearingID.ClientID%>').val();
        var CreditAccount = $('#<%= rcbCreditAccount.ClientID%>').val();
        var Narrative = $('#<%= txtNarrative.ClientID%>').val();
        Page_IsValid = ClearingID != "" && CreditAccount != "" && Narrative != "";
    }

    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        if (button.get_commandName() == "Preview") {
            window.location = "Default.aspx?tabid=172&ctl=chitiet&mid=862";
            return;
        }

        ValidatorUpdateIsValid();
        if (Page_IsValid) {

            $('#<%= hdfDisable.ClientID%>').val(1);
            clickCalledAfterRadconfirm = true;

            if (button.get_commandName() == "commit" && !clickCalledAfterRadconfirm) {
                args.set_cancel(true);
                lastClickedItem = args.get_item();
                clickCalledAfterRadconfirm = false;
                var isbool = radconfirm("Ch/exess Tt Amount Vnd 10000000", confirmCallbackFunction2);
                if (isbool == false) { confirmcallfail(); }
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
        var AmountElement = $('<%= lbDebitAmtLCY.ClientID%>');
        var Amount = AmountElement.get_value();

        var CreditCurrency = $find("<%= rcbCreditCurrency.ClientID%>");
        var DealRate = $find("<%= txtDealRate.ClientID%>");

        var dealratevalue = 1;

        if (DealRate.get_value() > 0 && rcbCreditCurrency != "VND") {
            dealratevalue = DealRate.get_value();
        }

        if (Amount) {
            var CrAmtLCY = $('<%= lbCrAmtLCY.ClientID%>');
            var lcy = Amount * dealratevalue;
            CrAmtLCY.set_value(lcy.toLocaleString("en-US"));
        }
    }

        function ShowMessageCurrencyNotMath() {
            radconfirm("Currency and Ben Com is not matched", confirmCallbackFunction2);
        }

  </script>