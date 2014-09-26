<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExchangeBanknotesManyDeno.ascx.cs" Inherits="BankProject.Views.TellerApplication.ForeignExchange.ExchangeBanknotesManyDeno" %>
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
        <li><a href="#ChristopherColumbus">Exchange BankNotes Many Deno</a></li>
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
                        ErrorMessage="TC Currency is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="MyLable">Debit Account</td>
                <td class="MyContent" >
                    <telerik:RadComboBox ID="cboDebitAccount"                        
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" >                       
                    </telerik:RadComboBox> <asp:Label ID="lblDebitAccount" runat="server"></asp:Label></td>             
                <td></td>
            </tr>
            <tr>
                <td class="MyLable">Debit Amount</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtDebitAmount" runat="server" ClientEvents-OnValueChanged ="txtDebitAmount_OnValueChanged" />
                </td>
                <td></td>
            </tr>
             <tr>
                <td class="MyLable">Narrative</td>
                <td class="MyContent" style="width:350px; ">
                    <telerik:RadTextBox ID="txtNarrative" Width="350"
                        runat="server"  />
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
             <tr>
                 <td class="MyLable">Value Date</td>
                <td class="MyContent"><telerik:RadDatePicker ID="txtValueDate" runat="server"></telerik:RadDatePicker></td>
                 <td></td>
             </tr>
        </table>
         <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Teller ID</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtCrTellerId"
                        runat="server">
                    </telerik:RadTextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="MyLable">Credit Account</td>
                <td class="MyContent" style="width:160px">
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
                <td><asp:Label ID="lbCrAccount" runat="server"></asp:Label></td>
            </tr>            
            <tr>
                <td class="MyLable">Exchange Rate</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtExchangeRate" runat="server" ClientEvents-OnValueChanged ="txtExchangeRate_OnValueChanged" />
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
        <fieldset id="fDenominations" runat="server">
         <legend>
              <div style="font-weight:bold; text-transform:uppercase;"><asp:Label ID="titleAmend_Confirm" runat="server" Text="Denominations"></asp:Label></div>
                                </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width:50px; font-weight:bold;"></td>
                    <td style="width:100px; font-weight:bold; padding-bottom:5px;">Denomination</td>
                    <td style="width:100px; font-weight:bold; padding-bottom:5px;">Unit</td>
                    <td style="width:100px; font-weight:bold; padding-bottom:5px;">Rate</td>
                    <td style="width:100px; font-weight:bold; padding-bottom:5px;">Amt LCY</td>
                    <td></td>
                </tr>
                <asp:Literal ID="litDenominations" runat="server"></asp:Literal>
                <tr id="trDenominations" runat="server">
                    <td class="labelDisabled" style="font-weight:bold; text-align:right; padding-right:5px; padding-bottom:5px;"><span class="spanDebitCurrency"></span></td>
                    <td style="padding-bottom:5px;"><asp:TextBox ID="txtDenomination_Num" runat="server" Width="80" /></td>
                    <td style="padding-bottom:5px;"><asp:TextBox ID="txtDenomination_Unit" runat="server" Width="80" /></td>
                    <td style="padding-bottom:5px;"><asp:TextBox ID="txtDenomination_ExchangeRate" runat="server" Width="80" /></td>
                    <td style="padding-bottom:5px;"><asp:Label ID="lblDenomination_AmtLCY" runat="server" Width="80" /></td>
                    <td style="padding-bottom:5px;"><a class="add" style="cursor:hand; cursor:pointer;"><img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>
        </fieldset>
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
    $(document).ready(function () {
            $('a.add').live('click',
                function () {
                    $(this)
                        .html('<img src="Icons/Sigma/Delete_16X16_Standard.png" />')
                        .removeClass('add')
                        .addClass('remove')
                    ;
                    var objTr = $(this).closest('tr').clone();
                    objTr.find('input[type="text"]').each(
                            function () {
                                this.value = '0';
                            });
                    objTr.find('#<%=lblDenomination_AmtLCY.ClientID%>').text('');
                    objTr.appendTo($(this).closest('table'));
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
            /*$('input:text').each(
                function () {
                    var thisName = $(this).attr('name'),
                        thisRrow = $(this)
                                    .closest('tr')
                                    .index();
                    $(this).attr('name', 'row' + thisRow + thisName);
                    $(this).attr('id', 'row' + thisRow + thisName);
                });*/            
    });
    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        //
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Commit%>") {
            args.set_cancel(true);//cancel commit
            var isOK = true;
            var Denominations = new Array();
            var DenominationsIndex = 0;
            var objTable = $('#<%=trDenominations.ClientID%>').parent();
            //
            objTable.find('input[id="<%=txtDenomination_Num.ClientID%>"]').map(function () {
                var num = this.value;
                if (!($.isNumeric(num) && Math.floor(num) == num)) {
                    alert('Please, enter integer number !');
                    this.focus();
                    isOK = false;
                    return;
                }
                else {
                    Denominations[DenominationsIndex] = new Array(3);
                    Denominations[DenominationsIndex][0] = num;
                    DenominationsIndex += 1;
                }
            });
            if (!isOK) return;
            //            
            DenominationsIndex = 0;
            objTable.find('input[id="<%=txtDenomination_Unit.ClientID%>"]').map(function () {
                var num = this.value;
                if (!($.isNumeric(num) && Math.floor(num) == num)) {
                    alert('Please, enter integer number !');
                    this.focus();
                    isOK = false;
                    return;
                }
                else {
                    Denominations[DenominationsIndex][1] = num;
                    DenominationsIndex += 1;
                }
            });
            if (!isOK) return;
            //
            DenominationsIndex = 0;
            objTable.find('input[id="<%=txtDenomination_ExchangeRate.ClientID%>"]').map(function () {
                var num = this.value;
                if (!($.isNumeric(num))) {
                    alert('Please, enter number !');
                    this.focus();
                    isOK = false;
                    return;
                }
                else {
                    Denominations[DenominationsIndex][2] = num;
                    DenominationsIndex += 1;
                }
            });
            if (!isOK) return;
            //
            var DebitAmount = Number($find("<%= txtDebitAmount.ClientID%>").get_value());
            for (DenominationsIndex = 0; DenominationsIndex < Denominations.length; DenominationsIndex++) {
                DebitAmount -= Denominations[DenominationsIndex][0] * Denominations[DenominationsIndex][1];
            }
            if (DebitAmount != 0) {
                alert('Debit Amount does not equal total Denomination !');
                return;
            }
            //
            //alert('OK');
            args.set_cancel(false);
        }
        //
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Preview%>") {
            window.location = '<%=EditUrl("list")%>&lst=4appr';
        }
        //
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Search%>") {
            window.location = '<%=EditUrl("list")%>';
        }

    }

    function cboDebitCurrency_OnClientSelectedIndexChanged(sender, eventArgs) {
        calculateAmountPaid();
    }

    function txtDebitAmount_OnValueChanged() {
        calculateAmountPaid();
    }

    function txtExchangeRate_OnValueChanged() {
        calculateAmountPaid();
    }

    function calculateAmountPaid() {
        var objAmtPaid = $find("<%= txtAmountPaid.ClientID%>");
        var objCurDebit = $find("<%= cboDebitCurrency.ClientID%>");
        $('.spanDebitCurrency').text(objCurDebit.get_text());
        if (objCurDebit.get_text() == '') {
            objAmtPaid.set_value();
            return;
        }
        var CurDebitRate = Number(objCurDebit.get_value().split('#')[1]);
        var DebitAmount = Number($find("<%= txtDebitAmount.ClientID%>").get_value());
        //
        var DealRate = $find("<%= txtExchangeRate.ClientID%>").get_value();
        objAmtPaid.set_value(DebitAmount * DealRate);
    }
    //
    function calculateDenominationAmtLCY(ctr) {
        var parentTR = $(ctr).parent().parent();
        var Num = Number(parentTR.find('#<%=txtDenomination_Num.ClientID%>').val());
        var Unit = Number(parentTR.find('#<%=txtDenomination_Unit.ClientID%>').val());
        var Rate = Number(parentTR.find('#<%=txtDenomination_ExchangeRate.ClientID%>').val());
        parentTR.find('#<%=lblDenomination_AmtLCY.ClientID%>').text(Num * Unit * Rate);
        //
        var totalAmtLCY = 0;
        $(parentTR).parent().find('#<%=lblDenomination_AmtLCY.ClientID%>').each(
                            function () {
                                if ($(this).text() != '') {
                                    totalAmtLCY += Number($(this).text());
                                }
                            });
        var avgRate = 0;
        if (totalAmtLCY > 0) {
            avgRate = totalAmtLCY / Number($('#<%=txtDebitAmount.ClientID%>').val());
        }
        $find('<%=txtExchangeRate.ClientID%>').set_value(avgRate);
    }
    $(document).on("keypress", "#<%=txtDenomination_Num.ClientID%>", function (event) {
        if (event.keyCode < 48 || event.keyCode > 57) {
            if (event.keyCode != 13) {
                return false;
            }
            calculateDenominationAmtLCY(this);
        }
    });
    $(document).on("keypress", "#<%=txtDenomination_Unit.ClientID%>", function (event) {
        if (event.keyCode < 48 || event.keyCode > 57) {
            if (event.keyCode != 13) {
                return false;
            }
            calculateDenominationAmtLCY(this);
        }
    });
    $(document).on("keypress", "#<%=txtDenomination_ExchangeRate.ClientID%>", function (event) {
        if (event.keyCode < 48 || event.keyCode > 57) {
            if (event.keyCode != 13) {
                return false;
            }
            calculateDenominationAmtLCY(this);
        }
    });
  </script>
</telerik:RadCodeBlock>