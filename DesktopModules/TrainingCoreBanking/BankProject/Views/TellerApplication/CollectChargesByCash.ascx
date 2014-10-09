<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollectChargesByCash.ascx.cs" Inherits="BankProject.Views.TellerApplication.CollectChargesByCash" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>
<%@ Register Src="../../Controls/VVComboBox.ascx" TagPrefix="uc1" TagName="VVComboBox" %>
<%@ Register Src="../../Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit"  />
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });

</script>
<asp:HiddenField ID="hdfDisable" runat="server" Value="0" />
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
        OnClientButtonClicking="OnClientButtonClicking"  OnButtonClick="RadToolBar1_ButtonClick">
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
        <li><a href="#ChristopherColumbus">Collect Charges</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
          <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer ID</td>
                <td class="MyContent" width="350">
                    <telerik:RadTextBox id="tbCustomerID" runat="server" width="350" 
                         AutoPostBack="true" OnTextChanged="tbCustomerID_TextChanged"></telerik:RadTextBox>
                </td>
                <td class="MyLable">
                <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                </td>
                <td class="MyContent"></td>
            </tr>
              <tr>
                  <td class="MyLable">Customer Name</td>
                  <td class="MyContent">
                      <telerik:RadTextBox id="tbCustomerName" runat="server" width="350"></telerik:RadTextBox>
                  </td>
              </tr>
        </table>

         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Address</td>
                <td class="MyContent" ><telerik:RadTextBox ID="tbAddress" runat="server" Width="350"></telerik:RadTextBox></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Legal ID</td>
                <td class="MyContent"><telerik:RadTextBox ID="tbLegalID" runat="server" Width="160"></telerik:RadTextBox></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Isssued Date</td>
                <td class="MyContent"><telerik:RadDatePicker ID="tbIsssuedDate" runat="server" Width="160"></telerik:RadDatePicker></td>
                <td class="MyLable">Place of Issue</td>
                <td class="MyContent"><telerik:RadTextBox ID="tbPlaceOfIss" runat="server" Width="160"></telerik:RadTextBox></td>
            </tr>
        </table>
       <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Teller ID <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldtxtTellerId"
                        ControlToValidate="txtTellerId"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Teller ID is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtTellerId"
                        runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>
        <hr />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
            <td class="MyLable">Currency <span class="Required">(*)</span>
                <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldCurrency"
                        ControlToValidate="rcbCurrency"
                        ValidationGroup="Commit"
                    
                        InitialValue=""
                        ErrorMessage="Currency is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
            </td>

        

            <td class="MyContent">              
                  <telerik:RadcomboBox
                        ID="rcbCurrency" runat="server" 
                        MarkFirstMatch="True" Width="150" Height="150"
                      OnClientSelectedIndexChanged="OnCurrencyMatch"
                      OnSelectedIndexChanged="rcbCurrency_SelectedIndexChanged"
                      AutoPostBack="true"
                        AllowCustomText="false" >

                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadcomboBox>   

            </td>
       </tr>
         </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
 <td class="MyLable">Account Type</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbAccountType" runat="server" 
                        OnSelectedIndexChanged="rcbAccountType_OnSelectedIndexChanged" autopostback="true"
                    AllowcustomText="false" MarkFirstMatch="true" AppendDataBoundItems="True" width="200" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" /> 
                    <Items>
                        <telerik:RadComboBoxItem Value="1" Text="Non Term Saving Account" />
                        <telerik:RadComboBoxItem Value="2" Text="Saving Account - Arrear" />
                        <telerik:RadComboBoxItem Value="3" Text="Saving Account - Periodic" />
                        <telerik:RadComboBoxItem Value="4" Text="Saving Account - Discounted" />
                    </Items>
                </telerik:RadComboBox>
            </td>
                </tr>
         </table>
        <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">Account</td>
                    <td class="MyContent">
                    
                       <telerik:RadcomboBox
                        ID="rcbCashAccount" runat="server" 
                        MarkFirstMatch="True" Width="350" Height="150"
                           AppendDataBoundItems="true"
                      OnClientSelectedIndexChanged="OnCurrencyMatch"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                       
                    </telerik:RadcomboBox> 
                    </td>
                </tr>          
            </table>

         <table width="100%" cellpadding="0" cellspacing="0">
         <tr>
            <td class="MyLable">Chrg Amt LCY</td>
            <td class="MyContent"> 
                <telerik:RadNumericTextBox ID="tbChargeAmountLCY" AutoPostBack="true" OnTextChanged="tbChargeAmountLCY_TextChanged" runat="server" NumberFormat-DecimalSeparator="." NumberFormat-DecimalDigits="0" />
            </td>
        </tr>
          <tr>
            <td class="MyLable">Chrg Amt FCY</td>
            <td class="MyContent">
                <telerik:RadNumericTextBox ID="tbChargeAmountFCY" runat="server" AutoPostBack="true" NumberFormat-DecimalSeparator="." NumberFormat-DecimalDigits="2"  OnTextChanged="tbChargeAmountFCY_TextChanged" />
            </td>
        </tr>
             <tr>
                 <td class="MyLable">Value Date</td>
                 <td class="MyContent">
                     <telerik:RadDatePicker ID="tbValueDate" runat="server"></telerik:RadDatePicker>
                 </td>
               
             </tr>
         <tr>
                 <td class="MyLable">Category PL <span class="Required">(*)</span>
                <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldCategoryPL"
                        ControlToValidate="rcbCategoryPL"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Category TP is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>

                 </td>
                 <td class="MyContent">
                    <telerik:RadComboBox width="350"  
                          ID="rcbCategoryPL" RunAt="server" MarkFirstMatch="true" 
                         AllowCustomText="false"> 
                        
                     <Items>
                         <telerik:RadComboBoxItem value=""  text="" />
                     </Items>
                         </telerik:RadComboBox>  
                 </td>
             </tr>
        <tr>
                  <td class="MyLable">Deal Rate</td>
                 <td class="MyContent">
                     <telerik:RadNumericTextBox ID="tbDealRate" runat="server" NumberFormat-DecimalSeparator="," AutoPostBack="true" NumberFormat-DecimalDigits="0" OnTextChanged="tbDealRate_TextChanged" />
                 </td>
             </tr>
             <tr>
                 <td class="MyLable">Vat Amount LCY</td>
                 <td class="MyContent">
                     <telerik:RadNumericTextBox id="tbVatAmountLCY" runat="server" ReadOnly="true" NumberFormat-DecimalSeparator="," AutoPostBack="true" OnTextChanged="tbVatAmountLCY_TextChanged" NumberFormat-DecimalDigits ="0"/>
                 </td>
             </tr>

                <tr>
                 <td class="MyLable">Vat Amount FCY</td>
                 <td class="MyContent">
                     <telerik:RadNumericTextBox id="tbVatAmountFCY" runat="server" ReadOnly="true" AutoPostBack="true" OnTextChanged="tbVatAmountFCY_TextChanged"  NumberFormat-DecimalSeparator="," NumberFormat-DecimalDigits="2"/>
                 </td>
             </tr>

              <tr>
                 <td class="MyLable">Total Amount LCY</td>
                 <td class="MyContent">
                     <telerik:RadNumericTextBox id="tbTotalAmountLCY" runat="server" ReadOnly="true" AutoPostBack="true" NumberFormat-DecimalSeparator="," NumberFormat-DecimalDigits="0"/>
                 </td>
             </tr>

                <tr>
                 <td class="MyLable">Total Amount FCY</td>
                 <td class="MyContent">
                     <telerik:RadNumericTextBox id="tbTotalAmountFCY" runat="server" ReadOnly="true" AutoPostBack="true" NumberFormat-DecimalSeparator="," NumberFormat-DecimalDigits="2"/>
                 </td>
             </tr>
             <tr>
                 <td class="MyLable">VAT Serial No <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldtbVATSerialNo"
                        ControlToValidate="tbVATSerialNo"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="VAT Serial No is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                 <td class="MyContent">
                     <telerik:RadTextBox id="tbVATSerialNo" runat="server"  />
                 </td>
             </tr>
    </table>
            </ContentTemplate>
             </asp:UpdatePanel>

        <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">Narrative</td>
                      <td class="MyContent" width="550">
                     <telerik:RadTextBox id="tbNarrative" runat="server" width="550"  />
                 </td>
                    <td class="MyContent" style="visibility:hidden;" >
                    <telerik:RadComboBox AppendDataBoundItems="True"  
                    ID="rcbCustomerID" Runat="server"
                    MarkFirstMatch="True" Height="150px" 
                         OnItemDataBound="rcbCustomerID_ItemDataBound"
                        OnClientSelectedIndexChanged="Customer_OnClientSelectedIndexChanged"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
                </td>
                </tr>
            </table>  
    </div>
    
</div>
<telerik:RadCodeBlock id="code" runat="server">
<script type="text/javascript">
    $('#<%=txtId.ClientID%>').keyup(function (event) {
        if (event.keyCode == 13) { $("#<%=btSearch.ClientID%>").click(); }
     });
    function ValidatorUpdateIsValid() {
        //var i;
        //for (i = 0; i < Page_Validators.length; i++) {
        //    if (!Page_Validators[i].isvalid) {
        //        Page_IsValid = false;
        //        return;
        //    }
        //}
        var teller = $('#<%= txtTellerId.ClientID%>').val();
        var VATSerialNo = $('#<%= tbVATSerialNo.ClientID%>').val();
        var CategoryPL = $('#<%= rcbCategoryPL.ClientID%>').val();
        var Currency = $('#<%= rcbCurrency.ClientID%>').val();

        Page_IsValid = teller != "" && CategoryPL != "" && VATSerialNo != "" && Currency != "";
    }

    function OnClientButtonClicking(sender, args) {
        ValidatorUpdateIsValid();
        var button = args.get_item();
        if (Page_IsValid) {

            $('#<%= hdfDisable.ClientID%>').val(1);

            //if (button.get_commandName() == "commit" && !clickCalledAfterRadconfirm) {
            //    args.set_cancel(true);
            //    lastClickedItem = args.get_item();
            //    var isbool = radconfirm("Credit Till Closing Balance", confirmCallbackFunction2);
            //    if (isbool == false) { confirmcallfail(); }
            //}

            //if (button.get_commandName() == "authorize" && !clickCalledAfterRadconfirm) {
            //    radconfirm("Authorised Completed", confirmCallbackFunction2);
            //}
        }

        if (button.get_commandName() == "Preview") {
            window.location = "Default.aspx?tabid=141&ctl=chitiet&mid=805";
        }
    }

    var lastClickedItem = null;
    var clickCalledAfterRadprompt = false;
    var clickCalledAfterRadconfirm = false;

    function confirmCallbackFunction1(args) {
        //neu true thi continue
        if (args) {
            clickCalledAfterRadconfirm = false;
            var isbool = radconfirm("Unauthorised overdraft of USD on account 050001688331", confirmCallbackFunction2); //" + amtFCYDeposited + "
            if (isbool == false) { confirmcallfail(); }
        }
    }

    function confirmCallbackFunction2(args) {
        //neu true thi continue
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

    function showmessageTrungCurrency() {
        radconfirm("Currency and Cash Account is not matched", confirmCallbackFunction2);
    }

    function OnCurrencyMatch(e) {
        var currencyDepositedElement = $find("<%= rcbCurrency.ClientID %>");
        var currencyDepositedValue = currencyDepositedElement.get_value();
        var cashAccountElement = $find("<%= rcbCashAccount.ClientID %>");
        var cashAccountValue = cashAccountElement.get_value();
        //if (currencyDepositedValue && cashAccountValue && (currencyDepositedValue != cashAccountValue)) {
        //    showmessageTrungCurrency();
        //    //alert("Currency and Cash Account is not matched");
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

    function Customer_OnClientSelectedIndexChanged() {
        var customerElement = $find("<%= rcbCustomerID.ClientID %>");

        var AddressElement = $find("<%= tbAddress.ClientID %>");
        AddressElement.set_value(customerElement.get_selectedItem().get_attributes().getAttribute("Address"));

        var legalIdElement = $find("<%= tbLegalID.ClientID %>");
        legalIdElement.set_value(customerElement.get_selectedItem().get_attributes().getAttribute("IdentityNo"));

        var PlaceOfIssElement = $find("<%= tbPlaceOfIss.ClientID %>");
        PlaceOfIssElement.set_value(customerElement.get_selectedItem().get_attributes().getAttribute("IssuePlace"));
        var IsssuedDateElement = $find("<%= tbIsssuedDate.ClientID %>");
        var date = customerElement.get_selectedItem().get_attributes().getAttribute("IssueDate");
        if (date != "") {
            var datesplit = customerElement.get_selectedItem().get_attributes().getAttribute("IssueDate").split('/');
            IsssuedDateElement.set_selectedDate(new Date(datesplit[2].substring(0, 4), datesplit[0], datesplit[1]));
        } else { IsssuedDateElement.set_selectedDate(null); }
    }
  </script>
</telerik:RadCodeBlock>
<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" Text="Search" onclick="btSearch_Click1" />
    <asp:hiddenfield ID="hdfCheckCustomer" runat="server" value="1"></asp:hiddenfield>
</div>