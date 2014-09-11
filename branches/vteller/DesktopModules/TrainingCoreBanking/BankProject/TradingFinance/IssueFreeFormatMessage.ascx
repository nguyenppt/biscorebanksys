<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IssueFreeFormatMessage.ascx.cs" Inherits="BankProject.TradingFinance.IssueFreeFormatMessage" %>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" ></telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />


<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    
    <script type="text/javascript">
        var cableType = '<%= CableType %>';
        
        jQuery(function ($) {
            $('#tabs-demo').dnnTabs();
        });

        function RadToolBar1_OnClientButtonClicking(sender, args) {
            var button = args.get_item();
            
            if (cableType !== '') {
                if (button.get_commandName() == "print") {
                    args.set_cancel(true);
                    radconfirm("Do you want to download file?", confirmCallbackFunction1, 340, 150, null, 'Download');
                }
            }
        }
        
        function confirmCallbackFunction1(result) {
            if (result) {
                $("#<%=btnIssueFreeFormatMessage.ClientID %>").click();
            }
        }
    </script>
</telerik:RadCodeBlock>

<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%"
    OnButtonClick="RadToolBar1_ButtonClick" OnClientButtonClicking="RadToolBar1_OnClientButtonClicking">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btSave" CommandName="save">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btReview" CommandName="review">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Revert" Value="btRevert" CommandName="revert">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="search">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>


<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#Main" id="tabMain">Main</a></li>
    </ul>
    
    <div id="Main" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 200px">Transaction Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                            AppendDataBoundItems="True"
                            Width="200"
                            ID="comboWaiveCharges" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <Items>
                               <telerik:RadComboBoxItem Value="OFT" Text="Overseas Funds Transfer" />
                                <telerik:RadComboBoxItem Value="DOC" Text="Documentary Collection" />
                                <telerik:RadComboBoxItem Value="LC" Text="Documentary Credit" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
        
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 200px">MT Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox Width="200"
                            AppendDataBoundItems="True"
                            ID="comboCableType" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="199" Text="199" />
                                <telerik:RadComboBoxItem Value="499" Text="499" />
                                <telerik:RadComboBoxItem Value="799" Text="799" />
                                <telerik:RadComboBoxItem Value="999" Text="999" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 200px">Receiver</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadTextBox Width="200"
                            ID="txtReviver" runat="server" 
                            AutoPostBack="True" OnTextChanged="txtReviver_OnTextChanged"/>
                    </td>
                    <td>
                        <asp:Label ID="lblReviverError" runat="server" Text="" ForeColor="red" />
                        <asp:Label ID="lblReviverCode" runat="server"/>
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 200px" class="MyLable">20. Transaction Reference Number</td>
                    <td class="MyContent" style="width: 150px">
                        <telerik:RadTextBox Width="200"
                            ID="txtTFNo" runat="server" 
                            AutoPostBack="True" OnTextChanged="txtTFNo_OnTextChanged"/>
                    </td>
                    <td><asp:Label ID="lblTFNoError" runat="server" Text="" ForeColor="red" /></td>
                </tr>
            </table>
        
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" style="width: 200px">21. Related Reference</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtRelatedReference" runat="server" Width="200"/>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" style="vertical-align: top">79. Narrative</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtNarrative" runat="server" Width="700" Height="400"
                            TextMode="MultiLine"/>
                    </td>
                </tr>
            </table>
    </div>
</div>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1">
    <AjaxSettings>
        
        <telerik:AjaxSetting AjaxControlID="comboWaiveCharges" >
            
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="txtTFNo">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTFNoError" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
        <telerik:AjaxSetting AjaxControlID="txtReviver">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblReviverError" />
                <telerik:AjaxUpdatedControl ControlID="lblReviverCode" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<asp:HiddenField ID="hiddenId" runat="server" />
<div style="visibility:hidden;"><asp:Button ID="btnIssueFreeFormatMessage" runat="server" OnClick="btnIssueFreeFormatMessage_Click" Text="Search" /></div>