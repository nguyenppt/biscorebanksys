<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForeignExchange.ascx.cs" Inherits="BankProject.Views.TellerApplication.ForeignExchange.ForeignExchange" %>
<%@ Register Src="~/DesktopModules/TrainingCoreBanking/BankProject/Controls/MultiTextBox.ascx" TagPrefix="uc1" TagName="MultiTextBox" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit"  />
<style>
    .labelDisabled {
        color: darkgray !important;
    }
</style>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
         OnClientButtonClicking="OnClientButtonClicking" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btPreview" CommandName="preview" PostBack="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="search" PostBack="false">
        </telerik:RadToolBarButton>
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print" Enabled="false">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
</div>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtId" runat="server" Width="200" /><asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="txtId"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="TTNo is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Foreign Exchange</a></li>
        <!--<li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>-->
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer Name <span class="Required">(*)</span><asp:HiddenField ID="tbCustomerNameOld" runat="server" /></td>
                <td class="MyContent"><telerik:RadTextBox ID="txtCustomerName" runat="server" Width="410"></telerik:RadTextBox></td>
                <td>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldtbCustomerName"
                        ControlToValidate="txtCustomerName"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Customer Name is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Address <span class="Required">(*)</span></td>
                <td class="MyContent"><telerik:RadTextBox ID="txtAddress" runat="server" Width="410"></telerik:RadTextBox></td>
                <td>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldtbAddress"
                        ControlToValidate="txtAddress"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Address is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="display:none;">
                <td class="MyLable">Passport No.</td>
                <td class="MyContent"><telerik:RadTextBox ID="txtPassportNo" runat="server" Width="160"></telerik:RadTextBox></td>
                <td>
                </td>
            </tr>
            <tr style="display:none;">
                <td class="MyLable">Date of isssue</td>
                <td class="MyContent" style="width:160px;"><telerik:RadDatePicker ID="txtDateOfIsssue" runat="server" Width="140"></telerik:RadDatePicker></td>
                <td><table><tr>
                    <td class="MyLable" style="width:80px;">Place of Iss</td>
                    <td class="MyContent"><telerik:RadTextBox ID="txtPlaceOfIss" runat="server" Width="160"></telerik:RadTextBox></td>
                </tr></table></td>
            </tr>
            <tr>
                <td class="MyLable">Phone No.</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtPhoneNo" runat="server"></telerik:RadTextBox>
                   <%-- <telerik:RadMaskedTextBox ID="txtPhoneNo" runat="server" Mask="(###)###-####" 
                    EmptyMessage="-- Enter Phone Number --" HideOnBlur="true" ZeroPadNumericRanges="true" 
                    DisplayMask="(###)###-####"></telerik:RadMaskedTextBox>--%>

                </td>
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
                <td></td>
            </tr>
             <tr>
                <td class="MyLable">Debit Currency
                <span class="Required">(*)</span>
                    </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cboDebitCurrency"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        AutoPostBack="false"
                        runat="server"
                        OnClientSelectedIndexChanged="cboDebitCurrency_OnClientSelectedIndexChanged">
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
                <td><asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldcboDebitCurrency"
                        ControlToValidate="cboDebitCurrency"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Debit Currency is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="MyLable">Debit Account <span class="Required">(*)</span></td>
                <td class="MyContent" >
                    <telerik:RadComboBox ID="cboDebitAccount"                        
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" >                       
                    </telerik:RadComboBox> <asp:Label ID="lblDebitAccount" runat="server"></asp:Label></td>             
                <td><asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator2"
                        ControlToValidate="cboDebitAccount"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Debit Account is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
            </tr>
            <tr class="labelDisabled">
                <td class="MyLable">Debit Amt LCY <span class="Required">(*)</span></td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtDebitAmtLCY" runat="server" ReadOnly="true" ToolTip="Enter recalculate data !" />
                </td>
                <td><asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator3"
                        ControlToValidate="txtDebitAmtLCY"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Debit Amt LCY is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="MyLable">Debit Amt FCY</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtDebitAmtFCY" runat="server" />
                </td>
                <td></td>
            </tr>
            <tr class="labelDisabled">
                <td class="MyLable">Debit Deal Rate</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtDebitDealRate" runat="server" ReadOnly="true" ><NumberFormat AllowRounding="false" DecimalDigits="10" /></telerik:RadNumericTextBox>
                </td>
                <td></td>
            </tr>
        </table>
         <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
               <td class="MyLable">Currency Paid <span class="Required">(*)</span></td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cboCurrencyPaid"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" Enabled="true" 
                        OnClientSelectedIndexChanged="cboCurrencyPaid_OnClientSelectedIndexChanged">
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
                <td>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldrcbCurrencyPaid"
                        ControlToValidate="cboCurrencyPaid"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Currency Paid is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Teller ID</td>
                <td class="MyContent"><telerik:RadTextBox ID="txtCrTellerId" runat="server"></telerik:RadTextBox></td>
                <td></td>
            </tr>
            <tr>
                <td class="MyLable">Credit Account <span class="Required">(*)</span></td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cboCreditAccount"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        Width="160"
                        runat="server" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="10001" Text="USD-10001-0184-184" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td><asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator4"
                        ControlToValidate="cboCreditAccount"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Credit Account is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
            </tr>            
            <tr>
                <td class="MyLable">Credit Deal Rate</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtCreditDealRate" runat="server" ><NumberFormat AllowRounding="false" DecimalDigits="10" /></telerik:RadNumericTextBox>
                </td>
                <td></td>
            </tr>
            <tr class="labelDisabled">
                <td class="MyLable">Amount Paid to Cust</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtAmountPaid" ReadOnly="true" runat="server" />
                </td>
                <td></td>
            </tr>
        </table>
        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr><td><uc1:MultiTextBox runat="server" id="txtNarrative" Label="Narrative" /></td></tr>
        </table>
    </div>
    <div id="dvAudit" runat="server">
         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Override</td>
                <td class="MyContent">CREDIT TILL CLOSING BALANCE</td>
            </tr>
                 <tr>
                <td class="MyLable">Override</td>
                <td class="MyContent">Unauthorised overdraft of USD 5028 on account 050001688331</td>
            </tr>
                <tr>
                <td class="MyLable">Record Status</td>
                <td class="MyContent">INAU INPUT Unauthorised</td>
            </tr>
                <tr>
                <td class="MyLable">Current Number</td>
                <td class="MyContent">1</td>
            </tr>
                <tr>
                <td class="MyLable">Inputter</td>
                <td class="MyContent">23_ID0296_I_INAU</td>
            </tr>
              <tr>
                <td class="MyLable">Date Time</td>
                <td class="MyContent">05 JUL 14 16:34</td>
            </tr>

            <tr>
                <td class="MyLable">Authoriser</td>
                <td class="MyContent"></td>
            </tr>
            <tr>
                <td class="MyLable">Company Code</td>
                <td class="MyContent">VN-001-2691</td>
            </tr>
             <tr>
                <td class="MyLable">Department Code</td>
                <td class="MyContent">1</td>
            </tr>
             <tr>
                <td class="MyLable">Auditor Code</td>
                <td class="MyContent"></td>
            </tr>
             <tr>
                <td class="MyLable">Audit Date Time</td>
                <td class="MyContent"></td>
            </tr>
        </table>
    </div>
</div>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">
    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        var ttNo = $('#<%= txtId.ClientID%>').val();
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Preview%>") {
            window.location = '<%=EditUrl("list")%>&lst=4appr';
        }
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Search%>") {
            window.location = '<%=EditUrl("list")%>';
        }
        if (button.get_commandName() == '<%=BankProject.Controls.Commands.Commit%>') {
            args.set_cancel(true);
            //
            var debtCur = $find("<%=cboDebitCurrency.ClientID%>").get_text();
            var creditCur = $find("<%=cboCurrencyPaid.ClientID%>").get_text();
            if (debtCur == creditCur) {
                alert('"Debit Currency" must different with "Currency Paid"');
                return;
            }
            args.set_cancel(false);
            try {
                <%=((BankProject.Controls.MultiTextBox)txtNarrative).getJSFunction() + "();"%>
            } catch (e) {
            }
        }
    }

    function cboDebitCurrency_OnClientSelectedIndexChanged(sender, eventArgs) {        
        var objdebtCur = $find("<%=cboDebitCurrency.ClientID%>");
        var objAmtLCY = $find("<%=txtDebitAmtLCY.ClientID%>");
        var objAmtFCY = $find("<%=txtDebitAmtFCY.ClientID%>");
        var objDebtRate = $find("<%=txtDebitDealRate.ClientID%>");
        var objCreditRate = $find("<%=txtCreditDealRate.ClientID%>");
        if (objdebtCur.get_text() == "VND") {
            setReadOnly(objAmtLCY, false);
            setReadOnly(objAmtFCY, true);
            objAmtFCY.set_value();
            setReadOnly(objDebtRate, false);
            setReadOnly(objCreditRate, true);
            objCreditRate.set_value();
        }
        else {
            setReadOnly(objAmtLCY, true);
            objAmtLCY.set_value();
            setReadOnly(objAmtFCY, false);
            setReadOnly(objDebtRate, true);
            objDebtRate.set_value();
            setReadOnly(objCreditRate, false);
        }
        calculateAmountPaid();
    }

    function setReadOnly(ctr, readOnly) {
        var cssLabelDisabled = 'labelDisabled';
        ctr._textBoxElement.readOnly = readOnly;
        var objParent = $("#" + ctr.get_id()).parent().parent().parent();
        objParent.removeClass(cssLabelDisabled);
        if (ctr._textBoxElement.readOnly)
            objParent.addClass(cssLabelDisabled);
    }

    $("#<%=txtDebitAmtLCY.ClientID%>")
        .keypress(function(event){
            if (event.keyCode == 13) {
                calculateAmountPaid();
            }
        })
        .focusout(function () {
            calculateAmountPaid();
        });
    $("#<%=txtDebitAmtFCY.ClientID%>")
        .keypress(function (event) {
            if (event.keyCode == 13) {
                calculateAmountPaid();
            }
        })
        .focusout(function () {
            calculateAmountPaid();
        });
    $("#<%=txtDebitDealRate.ClientID%>")
        .keypress(function (event) {
            if (event.keyCode == 13) {
                calculateAmountPaid();
            }
        })
        .focusout(function () {
            calculateAmountPaid();
        });
    $("#<%=txtCreditDealRate.ClientID%>")
        .keypress(function (event) {
            if (event.keyCode == 13) {
                calculateAmountPaid();
            }
        })
        .focusout(function () {
            calculateAmountPaid();
        });

    function cboCurrencyPaid_OnClientSelectedIndexChanged(sender, eventArgs) {
        //calculateAmountPaid();
    }

    function calculateAmountPaid() {
        var objdebtCur = $find("<%=cboDebitCurrency.ClientID%>");
        var objAmtLCY = $find("<%=txtDebitAmtLCY.ClientID%>");
        var objAmtFCY = $find("<%=txtDebitAmtFCY.ClientID%>");
        var objDebtRate = $find("<%=txtDebitDealRate.ClientID%>");
        var objCreditRate = $find("<%=txtCreditDealRate.ClientID%>");
        var objAmtPaid = $find("<%= txtAmountPaid.ClientID%>");
        if ($.trim(objdebtCur.get_text()) == "VND") {
            var AmtLCY = objAmtLCY.get_textBoxValue().replace(/,/g,'');
            if (AmtLCY != '') {
                DebtRate = objDebtRate.get_textBoxValue().replace(/,/g, '');
                if (DebtRate != '') {
                    objAmtFCY.set_value(Number(AmtLCY) / Number(DebtRate));
                    objAmtPaid.set_value(objAmtFCY.get_value());
                    return;
                }
            }
            objAmtFCY.set_value();
            objAmtPaid.set_value();
        }
        else {
            var AmtFCY = objAmtFCY.get_textBoxValue().replace(/,/g, '');
            if (AmtFCY != '') {
                CreditRate = objCreditRate.get_textBoxValue().replace(/,/g, '');
                if (CreditRate != '') {
                    objAmtLCY.set_value(Number(AmtFCY) * Number(CreditRate));
                    objAmtPaid.set_value(objAmtLCY.get_value());
                    return;
                }
            }
            objAmtLCY.set_value();
            objAmtPaid.set_value();
        }
    }

  </script>
</telerik:RadCodeBlock>