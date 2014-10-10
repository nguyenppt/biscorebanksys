<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CloseAccount.ascx.cs" Inherits="BankProject.Views.TellerApplication.CloseAccount" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>
<%@ Register Src="../../Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });

    $('#<%=txtId.ClientID%>').keyup(function (event) {
        if (event.keyCode == 13) { $("#<%=btSearch.ClientID%>").click(); }
    });

    function pageLoad() {
    }

    function OnCloseOnlineIndexChanged() {
        var closeOnlineElement = $find("<%= cmbCloseOnline.ClientID %>");
        var closeOnlineElementValue = closeOnlineElement.get_value();
        var closeModeElement = $find("<%= cmbCloseMode.ClientID %>");
        var totalCreditInterestElement = $('#<%=lblTotalCreditInterest.ClientID%>');
        if (closeOnlineElementValue == "Y") {
            closeModeElement.set_visible(true);
            var hdfOnlineTotal = $('#<%=hdfOnlineTotal.ClientID%>');
            totalCreditInterestElement.html(hdfOnlineTotal.val());
        }
        else {
            closeModeElement.set_visible(false);
            var hdfOpenTotal = $('#<%=hdfOpenTotal.ClientID%>');
            totalCreditInterestElement.html(hdfOpenTotal.val());
        }
    }

    function OnClientButtonClicking(sender, args) {
        $('#<%= hdfDisable.ClientID%>').val(1);
        var button = args.get_item();
        return; //khong hien message
        if (button.get_commandName() == "btnCommit" && !clickCalledAfterRadconfirm) {
                args.set_cancel(true);
                lastClickedItem = args.get_item();
                var isbool = radconfirm("Calculated Interest May Be inaccurate Entries Posted Today", confirmCallbackFunction1);
                if (isbool == false) { confirmcallfail(); }
        }
    }
    var lastClickedItem = null;
    var clickCalledAfterRadprompt = false;
    var clickCalledAfterRadconfirm = false;

    function confirmCallbackFunction1(args) {
        //neu true thi continue
        if (args) {
            clickCalledAfterRadconfirm = false;
            var isbool = radconfirm("Cash Type Closure, No Settlement Account Specified", confirmCallbackFunction2);
            if (isbool == false) { confirmcallfail(); }
        }
    }

    function confirmCallbackFunction2(args) {
        //neu true thi continue
        if (args) {
            clickCalledAfterRadconfirm = false;
            var isbool = radconfirm("Record In Hold-Status", confirmCallbackFunction3);
            if (isbool == false) { confirmcallfail(); }
        }
    }

    function confirmCallbackFunction3(args) {
        if (args) {
            clickCalledAfterRadconfirm = true;
            lastClickedItem.click();
            lastClickedItem = null;
        }
    }

    function confirmcallfail() {
        $('#<%= hdfDisable.ClientID%>').val(0);//cancel thi gan disable = 0 de khoi commit
        confirmCallbackFunction3();
    }

</script>

<asp:HiddenField ID="hdfOnlineTotal" runat="server" />
<asp:HiddenField ID="hdfOpenTotal" runat="server" />
<asp:HiddenField ID="hdfDisable" runat="server" Value="0" />

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
          </telerik:RadWindowManager>
<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
        OnButtonClick="OnRadToolBarClick">
        <Items>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
                ToolTip="Commit Data" Value="btCommit" CommandName="Commit">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
                ToolTip="Preview" Value="btPreview" CommandName="Preview">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
                ToolTip="Authorize" Value="btAuthorize" CommandName="Authorize">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
                ToolTip="Reverse" Value="btReverse" CommandName="Reverse">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
                ToolTip="Search" Value="btSearch" CommandName="Search">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
                ToolTip="Print Deal Slip" Value="btPrint" CommandName="Print">
            </telerik:RadToolBarButton>
        </Items>
    </telerik:RadToolBar>

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtId" runat="server" Width="200"  />
        </td>
    </tr>
</table>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Close Account</a></li>
        <li><a href="#FTAccClose">FT Acc Close</a></li>
        <%--<li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>--%>
    </ul>
  
    <div id="ChristopherColumbus" class="dnnClear">
        <%--OnClientSelectedIndexChanged="OnCloseOnlineIndexChanged"--%>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Close Online:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbCloseOnline"
                        MarkFirstMatch="True" 
                        AllowCustomText="false"                                                 
                        OnClientSelectedIndexChanged="OnCloseOnlineIndexChanged"
                        runat="server" ValidationGroup="Group1">
                        <Items>                            
                            <telerik:RadComboBoxItem Value="Y" Text="Y" />
                            <telerik:RadComboBoxItem Value="N" Text="N" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Currency:</td>
                <td class="MyContent">
                    <asp:Label ID="lblCurrency" runat="server">VND</asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Working Ballance:</td>
                <td class="MyContent">
                    <Telerik:RadnumericTextbox ID="lblWorkingBallance"  NumberFormat-DecimalDigits="2" readonly="true" borderwidth="0" runat="server"></Telerik:RadnumericTextbox>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Standing  Orders:</td>
                <td class="MyContent">
                    <asp:Label ID="lblStandingOrders" runat="server">NO</asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">UnclearedEntries</td>
                <td class="MyContent">
                    <asp:Label ID="lblUnclearedEntries" runat="server">NO</asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Cheques OS</td>
                <td class="MyContent">
                    <asp:Label ID="lblChequesOS" runat="server">NO</asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Bank Cards</td>
                <td class="MyContent">
                    <asp:Label ID="lblBankCards" runat="server">NO</asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">CC Chgs OS</td>
                <td class="MyContent">
                    <asp:Label ID="lblCCChgsOS" runat="server">0</asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Total Credit Interest</td>
                <td class="MyContent">
                    <Telerik:RadnumericTextbox ID="lblTotalCreditInterest"  NumberFormat-DecimalDigits="2" readonly="true" borderwidth="0" runat="server"></Telerik:RadnumericTextbox>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Total Debit Interest</td>
                <td class="MyContent">
                    <asp:Label ID="lblTotalDebitInterest" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Total Charges</td>
                <td class="MyContent">
                    <asp:Label ID="lblTotalCharges" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Total VAT</td>
                <td class="MyContent">
                    <asp:Label ID="lblTotalVAT" runat="server">0</asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Int.CAP to AC</td>
                <td class="MyContent">
                    <asp:Label ID="lblIntCAPAC" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Trans Ref Next</td>
                <td class="MyContent">
                    <asp:Label ID="lblTransRefNext" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>

    <div id="FTAccClose" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>_________________________________Debit Information___________________________________________________________________
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer:</td>
                  <td class="MyContent" width="100">
                    <asp:Label ID="lblCustomerId" width="100" runat="server" />
                </td>
                <td class="MyContent">
                    <asp:Label ID="lblCustomer" runat="server" />
                </td>
            </tr>
            </table>
        <table width="100%" cellpadding="0" cellspacing="0">

            <tr>
                <td class="MyLable">Currency:</td>
                <td class="MyContent">
                    <asp:Label ID="lbCreditCurrency"  runat="server" Text="VND">
                    </asp:Label>
                </td>
            </tr>
            </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Close Account:</td>
                <td class="MyContent" >
                    <asp:Label ID="lblClosedAccount" CssClass="cssDisableLable" runat="server">
                    </asp:Label>
                </td>
                
            </tr>
            </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Debit Amount:</td>
                <td class="MyContent">
                    <telerik:radNumericTextbox ID="lblDebitAmount" readonly="true" NumberFormat-DecimalDigits="2"  borderwidth="0" runat="server">
                    </telerik:radNumericTextbox>
                </td>
            </tr>
            </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Debit Date:</td>
                <td class="MyContent">
                    <telerik:RadDatePicker ID="lblDebitDate" runat="server" Enabled="False" />
                </td>
            </tr>
        </table>

        <br />
        

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>_________________________________Credit Information___________________________________________________________________
                </td>
            </tr>
        </table>

        <asp:UpdatePanel id="UpdatePanel1" runat="server">
            <ContentTemplate>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Credit Currency:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbCreditCurrency" runat="server" MarkFirstMatch="True" 
                        OnSelectedIndexChanged="cmbCreditCurrency_OnSelectedIndexChanged" AutoPostBack="true"
                        AllowCustomText="false">
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
                <td class="MyLable">Account Paid:</td>
                <td class="MyContent" >
                    <telerik:RadComboBox ID="cmbAccountPaid" 
                        MarkFirstMatch="True" 
                        width="400"
                        AllowCustomText="false" 
                        runat="server">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Credit Amount:</td>
                <td class="MyContent">
                    <telerik:radNumericTextbox ID="lblCreditAmount" readonly="true" NumberFormat-DecimalDigits="2"  borderwidth="0" runat="server" ></telerik:radNumericTextbox>
                </td>
            </tr>
        </table>
                    </ContentTemplate>
            </asp:UpdatePanel>

         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Narrative</td>
                <td class="MyContent" >
                    <telerik:RadTextBox ID="txtNarrative" Width="300"
                        runat="server" ValidationGroup="Group1" />
                </td>
            </tr>
        </table>


        <br />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>_________________________________Audit Information___________________________________________________________________
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Override:</td>
                <td class="MyContent">
                    <asp:Label ID="Label2" CssClass="cssDisableLable" runat="server">                                                    
                    </asp:Label>
                </td>
            </tr>
            </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Record Status:</td>
                <td class="MyContent" width="80">
                    <asp:Label ID="Label3" CssClass="cssDisableLable" runat="server">   
                            IHLD                         
                    </asp:Label>
                </td>
                <td class="MyContent">INPUT Held
                </td>
            </tr>
            </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Current Number:</td>
                <td class="MyContent">
                    <asp:Label CssClass="cssDisableLable" runat="server">
                            1
                    </asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Inputter:</td>
                <td class="MyContent">
                    <asp:Label CssClass="cssDisableLable" runat="server">
                            112_ID2054_I_INAU
                    </asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Inputter:</td>
                <td class="MyContent">
                    <asp:Label CssClass="cssDisableLable" runat="server">
                            112_SYSTEM
                    </asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Authorised:</td>
            </tr>

            <tr>
                <td class="MyLable">Date Time:</td>
                <td class="MyContent">
                    <asp:Label CssClass="cssDisableLable" ID="lbDateTime1" runat="server">
                            
                    </asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Date Time:</td>
                <td class="MyContent">
                    <asp:Label CssClass="cssDisableLable" ID="lbDateTime2" runat="server">
                    </asp:Label>
                </td>
            </tr>
            </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Company Code:</td>
                <td class="MyContent" width="100">
                    <asp:Label CssClass="cssDisableLable" runat="server">
                            VN-001-1221
                    </asp:Label>
                </td>
                <td class="MyContent">
                    <asp:Label ID="Label4" runat="server" CssClass="cssLableLink">
                        CHI NHANH CHO LON
                    </asp:Label>
                </td>
            </tr>
    </table>            

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Department Code:</td>
                <td class="MyContent">
                    <asp:Label runat="server" CssClass="cssDisableLable">
                            1
                    </asp:Label>
                </td>
            </tr>
            
            <tr style="visibility:hidden;">
                <td class="MyLable">Close Mode:</td>
                <td class="MyContent">
                    <telerik:RadComboBox MarkFirstMatch="True"
                        AllowCustomText="false" 
                        ID="cmbCloseMode" runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="FT" Text="FT" />
                            <telerik:RadComboBoxItem Value="TELLER" Text="TELLER" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>

        </table>
    </div>
</div>


<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" Text="Search"  OnClick="btSearch_Click" />
</div>