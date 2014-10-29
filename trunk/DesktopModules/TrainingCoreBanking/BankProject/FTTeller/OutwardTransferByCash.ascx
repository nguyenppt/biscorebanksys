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
      OnButtonClick="RadToolBar1_ButtonClick">
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
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
         <fieldset>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Product ID<span class="Required">(*)</span>
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
                <td class="MyLable">Credit Account</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbCreditAccount"
                        MarkFirstMatch="True" appendDataBoundItems="true"
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
            <td></td>
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
       </fieldset>        
        <fieldset id="Fieldset1" runat="server">
         <legend>
              <div style="font-weight:bold; text-transform:uppercase;"><asp:Label ID="Label2" runat="server" Text="Sending Information"></asp:Label></div>
                                </legend>    
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                 <td class="MyLable">Name<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator3"
                        ControlToValidate="txtSendingName"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Sending Name is Required" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td class="MyContent"><telerik:RadTextBox ID="txtSendingName" runat="server" Width="400" ></telerik:RadTextBox></td>
            </tr>
            <tr>
                <td class="MyLable">Address</td>
                <td class="MyContent">
                    <telerik:RadTextBox id="tbAddress" runat="server" width="400"></telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable"></td>
                <td class="MyContent">
                    <telerik:RadTextBox id="tbAddress2" runat="server" width="400"></telerik:RadTextBox>
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Phone</td>
                <td class="MyContent"><telerik:RadMaskedTextBox ID="tbPhone" runat="server" Mask="###########"
                        EmptyMessage="-- Enter Phone Number --" HideOnBlur="true" ZeroPadNumericRanges="true" DisplayMask="###########">
                    </telerik:RadMaskedTextBox>
                   </td>
            </tr>
        </table>
            </fieldset>
 
  <fieldset id="Fieldset2" runat="server">
         <legend>
              <div style="font-weight:bold; text-transform:uppercase;"><asp:Label ID="Label3" runat="server" Text="Receiving Information"></asp:Label></div>
                                </legend>   

       <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Name</td>
                <td class="MyContent">
                    <telerik:RadTextBox id="tbReceivingName" runat="server" width="400"></telerik:RadTextBox>
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
        </table>
      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>

      <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Ben Account</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtBenAccount" runat="server" onTextChanged="txtBenAccount_TextChanged"  autoPostBack="true"></telerik:RadTextBox>
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
                    <telerik:RadComboBox ID="rcbBankCode"
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
                   </td>
                <td class="MyContent"><telerik:RadTextBox ID="txtIsssuePlace" runat="server" Width="160"></telerik:RadTextBox></td>
            </tr>
        </table>

     
      </fieldset>
        <fieldset>
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
                <td class="MyContent" style="width:400px; ">
                    <telerik:RadTextBox ID="txtNarrative" Width="400"
                        runat="server"  />
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
            <tr>
                <td class="MyLable"></td>
                <td class="MyContent" style="width:400px; ">
                    <telerik:RadTextBox ID="txtNarrative2" Width="400"
                        runat="server"  />
                </td>
            </tr>
            <tr>
                <td class="MyLable"></td>
                <td class="MyContent" style="width:400px; ">
                    <telerik:RadTextBox ID="txtNarrative3" Width="400"
                        runat="server"  />
                </td>
            </tr>
        </table> </fieldset>
        <fieldset>
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
            </fieldset>
</div>
</div>
<asp:HiddenField ID="hdfDisable" runat="server" Value="1" />
<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" />
</div>
<telerik:RadCodeBlock id="codeBlock" runat="server">
<script type="text/javascript">
    $('#<%=txtId.ClientID%>').keyup(function (event) {
        if (event.keyCode == 13) $("#<%=btSearch.ClientID%>").click();
    })
    //var lastClickedItem = null;
    //var clickCalledAfterRadprompt = false;
    //var clickCalledAfterRadconfirm = false;

    //function confirmCallbackFunction1(args) {
    //    radconfirm("Unauthorised overdraft of USD on account 050001688331", confirmCallbackFunction2); //" + amtFCYDeposited + "
    //}
   
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
  </script>
    </telerik:RadCodeBlock>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>

        <telerik:AjaxSetting AjaxControlID="txtBenAccount">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="tbReceivingName" />
                <telerik:AjaxUpdatedControl ControlID="txtIdentityCard" />
                <telerik:AjaxUpdatedControl ControlID="txtIsssuePlace" /> 
                <telerik:AjaxUpdatedControl ControlID="txtIsssueDate" />
            </UpdatedControls>
        </telerik:AjaxSetting> 
</AjaxSettings>
</telerik:RadAjaxManager>