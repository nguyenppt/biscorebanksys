<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollectCharges.ascx.cs" Inherits="BankProject.TradingFinance.CollectCharges" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit"  />
<style>
    .addChargeType, .removeChargeType {
        cursor:hand; cursor:pointer;
    }
</style>
<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
         OnClientButtonClicking="RadToolBar1_OnClientButtonClicking" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommit" CommandName="commit" Enabled="true">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btPreview" CommandName="preview" postback="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="search" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print" Value="btPrint" CommandName="print" postback="false" Enabled="false">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtCode" runat="server" Width="200" /> <span class="Required">*</span><asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator6"
            ControlToValidate="txtCode"
            ValidationGroup="Commit"
            InitialValue=""
            ErrorMessage="FT Number is required" ForeColor="Red">
        </asp:RequiredFieldValidator> &nbsp;<asp:Label ID="lblError" runat="server" ForeColor="red" />
        </td>        
    </tr>
</table>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-main').dnnTabs();
    });
</script>
<div class="dnnForm" id="tabs-main">    
    <ul class="dnnAdminTabNav">
        <li><a href="#">Collect Charges</a></li>
    </ul>
    <div id="divCollectCharges" class="dnnClear">
        <fieldset>
            <legend>
                <span style="font-weight: bold; text-transform: uppercase;"></span>
            </legend>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Charge Acct <span class="Required">*</span><asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator1"
            ControlToValidate="txtChargeAcct"
            ValidationGroup="Commit"
            InitialValue=""
            ErrorMessage="Charge Acct is required" ForeColor="Red">
        </asp:RequiredFieldValidator></td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtChargeAcct" Runat="server" AutoPostBack="True" OnTextChanged="txtChargeAcct_OnTextChanged" />
                    </td>
                    <td><asp:Label ID="lblChargeAcctName" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Charge Currency</td>
                    <td class="MyContent" colspan="2"><asp:Label ID="lblChargeCurrency" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Transaction Type</td>
                    <td class="MyContent" colspan="2">
                        <telerik:RadComboBox
                            ID="cboTransactionType_ChargeCommission" 
                            Runat="server" OnSelectedIndexChanged="cboTransactionType_ChargeCommission_OnSelectedIndexChanged"
                            MarkFirstMatch="True" AutoPostBack="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="TT" Text="TT" />
	                            <telerik:RadComboBoxItem Value="LC" Text="LC" />
	                            <telerik:RadComboBoxItem Value="DP/DA" Text="DP/DA" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" id="divChargeType" runat="server">
                <tr>    
                    <td class="MyLable">Charge Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboChargeType" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                    <td><div id="divCmdChargeType" runat="server"><a class="addChargeType" index="1"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></div></td>
                </tr>
                <tr>    
                    <td class="MyLable">Charge Amount</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadNumericTextBox ID="txtChargeAmount" Runat="server" AutoPostBack="True" OnTextChanged="txtChargeAmount_OnTextChanged" />
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" id="divChargeType1" runat="server" style="display:none;">
                <tr>    
                    <td class="MyLable">Charge Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboChargeType1" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                    <td><div id="divCmdChargeType1" runat="server"><a class="removeChargeType" index="2"><img src="Icons/Sigma/Delete_16X16_Standard.png" /></a><a class="addChargeType" index="2"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></div></td>
                </tr>
                <tr>    
                    <td class="MyLable">Charge Amount</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadNumericTextBox ID="txtChargeAmount1" Runat="server" AutoPostBack="True" OnTextChanged="txtChargeAmount1_OnTextChanged" />
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" id="divChargeType2" runat="server" style="display:none;">
                <tr>    
                    <td class="MyLable">Charge Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboChargeType2" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                    <td><div id="divCmdChargeType2" runat="server"><a class="removeChargeType" index="3"><img src="Icons/Sigma/Delete_16X16_Standard.png" /></a></div></td>
                </tr>
                <tr>    
                    <td class="MyLable">Charge Amount</td>
                    <td style="width: 150px" class="MyContent">
                        <telerik:RadNumericTextBox ID="txtChargeAmount2" Runat="server" AutoPostBack="True" OnTextChanged="txtChargeAmount2_OnTextChanged" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>    
                    <td class="MyLable">Charge For</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboChargeFor" Runat="server" OnSelectedIndexChanged="cboChargeFor_ChargeCommission_OnSelectedIndexChanged"
                            MarkFirstMatch="True" AutoPostBack="True"
                            AllowCustomText="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="A" Text="A" />
                                <telerik:RadComboBoxItem Value="AC" Text="AC" />
                                <telerik:RadComboBoxItem Value="B" Text="B" />
                                <telerik:RadComboBoxItem Value="BC" Text="BC" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>    
                    <td class="MyLable">VAT No</td>
                    <td class="MyContent"><telerik:RadTextBox ID="txtVATNo" runat="server" Enabled="false" /></td>
                </tr>
                <tr>    
                    <td class="MyLable">Add Remarks</td>
                    <td class="MyContent"><telerik:RadTextBox ID="txtAddRemarks_Charges1" runat="server" Width="300" /></td>
                </tr>                
                <tr>    
                    <td class="MyLable"></td>
                    <td class="MyContent"><telerik:RadTextBox ID="txtAddRemarks_Charges2" runat="server" Width="300" /></td>
                </tr>
                <tr>    
                    <td class="MyLable">Account Officer</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="cboAccountOfficer" Runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false" >
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>    
                    <td class="MyLable">Total Charge Amount</td>
                    <td class="MyContent"><asp:Label ID="lblTotalChargeAmount" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>    
                    <td class="MyLable">Total Tax Amount</td>
                    <td class="MyContent"><asp:Label ID="lblTotalTaxAmount" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </fieldset>
    </div>
</div>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function RadToolBar1_OnClientButtonClicking(sender, args) {
            var button = args.get_item();
            //
            if (button.get_commandName() == '<%=BankProject.Controls.Commands.Preview%>') {
                window.location = '<%=EditUrl("list")%>&lst=4appr';
            }
            if (button.get_commandName() == '<%=BankProject.Controls.Commands.Search%>') {
                window.location = '<%=EditUrl("list")%>';
            }
            if (button.get_commandName() == '<%=BankProject.Controls.Commands.Print%>') {
                args.set_cancel(false);
                radconfirm("Do you want to download VAT file ?", showReport1, 420, 150, null, 'Download');
            }
            if (button.get_commandName() == '<%=BankProject.Controls.Commands.Commit%>') {
                var ChargeAcct = $find('<%=txtChargeAcct.ClientID%>').get_value();
                if (ChargeAcct == null || ChargeAcct == '') {
                    return;
                }
                var s = $('#<%=lblChargeAcctName.ClientID%>').text();
                if (s == null || s == '' || s == '<%=ChargeAcctMessage%>') {
                    args.set_cancel(true);
                    alert('<%=RequiredFieldValidator1.ErrorMessage%>');
                    return;
                }                
            }
        }
        function showReport1(result) {
            if (result) {
                $("#<%=btnReportVAT.ClientID %>").click();
            }
        }
        //
        $("#<%=txtCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $('#<%=btnLoadCodeInfo.ClientID%>').click();
            }
        });
        //
        $('a.addChargeType').click(function () {
                var index = $(this).attr('index');
                if (index == "1") {
                    if ($('#<%=divChargeType2.ClientID%>').css('display') == 'none')
                        $('#<%=divChargeType1.ClientID%> .addChargeType').css('display', '');
                    else
                        $('#<%=divChargeType1.ClientID%> .addChargeType').css('display', 'none');
                    $find("<%=cboChargeType1.ClientID%>").set_value('');
                    $('#<%=divChargeType1.ClientID%>').css('display', '');
                }
                else if (index == "2") {
                    $('#<%=divChargeType2.ClientID%> .addChargeType').css('display', '');
                    $find("<%=cboChargeType2.ClientID%>").set_value('');
                    $('#<%=divChargeType2.ClientID%>').css('display', '');
                }
                $(this).css('display', 'none');
            });
        $('a.removeChargeType').click(function () {
                var index = $(this).attr('index');
                if (index == "2") {
                    $('#<%=divChargeType.ClientID%> .addChargeType').css('display', '');
                    $find("<%=cboChargeType1.ClientID%>").get_items().getItem(0).select();
                    $find("<%=txtChargeAmount1.ClientID%>").set_value(0);
                    $('#<%=divChargeType1.ClientID%>').css('display', 'none');
                }
                else if (index == "3") {
                    $('#<%=divChargeType1.ClientID%> .addChargeType').css('display', '');
                    $find("<%=cboChargeType2.ClientID%>").get_items().getItem(0).select();
                    $find("<%=txtChargeAmount1.ClientID%>").set_value(0);
                    $('#<%=divChargeType2.ClientID%>').css('display', 'none');
                }
            });
    </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"><img src="icons/bank/ajax-loader-16x16.gif" /></telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="txtChargeAcct">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblChargeCurrency" />
                <telerik:AjaxUpdatedControl ControlID="lblChargeAcctName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboTransactionType_ChargeCommission">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="cboChargeType" />
                <telerik:AjaxUpdatedControl ControlID="cboChargeType1" />
                <telerik:AjaxUpdatedControl ControlID="cboChargeType2" />                
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtChargeAmount">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTotalChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalTaxAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtChargeAmount1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTotalChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalTaxAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtChargeAmount2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTotalChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalTaxAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboChargeFor">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTotalChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalTaxAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboChargeType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTotalChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalTaxAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboChargeType1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTotalChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalTaxAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboChargeType2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTotalChargeAmount" />
                <telerik:AjaxUpdatedControl ControlID="lblTotalTaxAmount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<div style="visibility: hidden;"><asp:Button ID="btnLoadCodeInfo" runat="server" OnClick="btnLoadCodeInfo_Click" Text="" /></div>
<div style="visibility: hidden;"><asp:Button ID="btnReportVAT" runat="server" OnClick="btnReportVAT_Click" Text="" /></div>