<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TellerTransactionsDisplay.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Close.TellerTransactions.TellerTransactionsDisplay" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
          </telerik:RadWindowManager>
<telerik:RadToolBar runat="server" ID="RadToolBar1" OnClientButtonClicking="OnClientButtonClicking"
     EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
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
                <asp:TextBox ID="tbDepositCode" runat="server" Width="400" />
                <i>
                    <asp:Label ID="lblDepositCode" runat="server" /></i></td>
        </tr>
    </table>
</div>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#Preclose">Teller Transaction</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Customer Infomation</div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Customer 
                        ID</td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblCustomerID"></asp:Label></td>
                </tr>

                <tr>
                    <td class="MyLable">Customer Name</td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblCustomerName"></asp:Label></td>
                </tr>

                <tr>
                    <td class="MyLable">Address 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblAddress"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Passport 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblPassport"></asp:Label></td>
                </tr>
                 <tr>
                    <td class="MyLable">Place Of I
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblPlaceOfIssue"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Date of Issue</td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblDateofIssue"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Phone No</td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblPhoneNo"></asp:Label></td>
                </tr>
           </table>
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Debit Infomation</div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Teller ID</td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblTellerID"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Customer 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblCustomer"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Debit Currency
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblDebitCurrency"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Debit Account
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblDebitAccount"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Debit Amt LCY
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblDebitAmtLCY"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Debit Amt FCY
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblDebitAmtFCY"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Narrative
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblNarrative"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Value Date
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblValueDate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">New Customer Balance
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblNewCustBal"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Old Cust Acc 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblOldCustAcc"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Smartbank Branch 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="Label1"></asp:Label></td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Credit Infomation</div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Teller ID2</td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblTellerID2"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Customer 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblCustomer2"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Credit Currency
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblCrebitCurrency"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Credit Account
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblCreditAccount"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Credit Amount LCY
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblCreditAmountLCY"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Credit Amount FCY
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblCreditAmountFCY"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Amount Paid
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblAmountPaid"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Deal Rate
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblDealRate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Value Date
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblValueDate2"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Narrative
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblNarrative2"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Smartbank Branch 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblSmartbankBranch"></asp:Label></td>
                </tr>
            </table>
        </fieldset>
        
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Check Infomation</div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Check No</td>
                    <td class="MyContent"><asp:Label runat="server" ID ="Label2"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Check Type 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="Label3"></asp:Label></td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
        <legend>
            <div style="font-weight: bold; text-transform: uppercase;">VAT Infomation</div>
        </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">VAT Serial No</td>
                    <td class="MyContent"><asp:Label runat="server" ID ="Label4"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">VAT Amt LCY
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="Label5"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">VAT Amt FCY
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="Label6"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">VAT Date
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="Label7"></asp:Label></td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
        <legend>
            <div style="font-weight: bold; text-transform: uppercase;">Interest Infomation</div>
        </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Acct Paid Int</td>
                    <td class="MyContent"><asp:Label runat="server" ID ="Label8"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Cap Date
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="Label9"></asp:Label></td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
        <legend>
            <div style="font-weight: bold; text-transform: uppercase;">Others Infomation</div>
        </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Acct No</td>
                    <td class="MyContent"><asp:Label runat="server" ID ="Label10"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Waives Charges ?
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="Label11" Text="YES"></asp:Label></td>
                </tr>
            </table>
        </fieldset>
        
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">USD Denomination</div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">TC.Issuer</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="rcbPrecloseYN" runat="server" MarkFirstMatch="True" Width="50" Height="50px" AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="1" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Denomination(Y/N)
                    </td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbDenomination" runat="server" Text="" Width="50">
                    </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Denomination</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="RadComboBox1" runat="server" MarkFirstMatch="True" Width="150" Height="50px" AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="1" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Rate</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="RadTextBox1" runat="server" Text="" Width="50">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Unit</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="RadTextBox2" runat="server" Text="" Width="150">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Amt Lcy</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="RadTextBox3" runat="server" Text="" Width="150">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Serial No</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="RadTextBox4" runat="server" Text="" Width="150">
                        </telerik:RadTextBox>
                    </td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Audit Information</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Overrides</td>
                    <td class="MyContent"></td>
                </tr>
                <tr>
                    <td class="MyLable">Record Status</td>
                    <td class="MyContent">IHLD -  INPUT Held</td>
                </tr>
                <tr>
                    <td class="MyLable">Curr Number</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCurrNo" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Inputter</td>
                    <td class="MyContent">
                        <asp:Label ID="lblInputter" runat="server" />114_ID1981_I_IHLD</td>
                </tr>
                <tr>
                    <td class="MyLable">Authoriser</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAuthoriser" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Date.Time</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDateTime" runat="server" /> 30/07/2014 14:02</td>
                </tr>
                <tr>
                    <td class="MyLable">Date.Time</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDateTime2" runat="server" /></td>
                </tr>
                
                <tr>
                    <td class="MyLable">Co.Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCoCode" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Dept.Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDeptCode" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Auditor Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAuditorCode" runat="server" /></td>
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
        var lastClickedItem = null;
        var clickCalledAfterRadprompt = false;
        var clickCalledAfterRadconfirm = false;

        function OnClientButtonClicking(sender, args) {
            var button = args.get_item();
                if (button.get_commandName() == "commit") {
                    if (!clickCalledAfterRadconfirm) {
                        args.set_cancel(true);
                        lastClickedItem = args.get_item();
                        radconfirm("Credit Till Closing Balance?", confirmCallbackFunction1, 350, 150, null, 'Override');
                    }
                }
        }

        function confirmCallbackFunction1(result) {
            clickCalledAfterRadconfirm = false;
            var totalAmt = Number($('#<%= hdfTotalAmt.ClientID %>').val());
            if (result) {
                radconfirm("Unauthorised overdraft of VND " + totalAmt.format(2) + " on account 070001644127", confirmCallbackFunction2, 350, 150, null, 'Override');
            }
        }
        function confirmCallbackFunction2(result) {
            if (result) {
                clickCalledAfterRadconfirm = true;
                lastClickedItem.click();

                lastClickedItem = null;
            }
        }

        /**
         * Number.prototype.format(n, x)
         * 
         * @param integer n: length of decimal
         * @param integer x: length of sections
         */
        Number.prototype.format = function (n, x) {
            var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
            return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&,');
        };

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
<asp:HiddenField ID="hdfPreclosureType" runat="server" Value="0" />
<asp:HiddenField ID="hdfTotalAmt" runat="server" Value="0" />