<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProvisionTransfer_DC_PL.ascx.cs" Inherits="BankProject.TradingFinance.Import.DocumentaryCredit.ProvisionTransfer_DC_PL" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
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

<div style="padding:10px;">
    <table width="100%" cellpadding="0" cellspacing="0" style="margin-bottom:10px">
        <tr>
            <td style="width: 60px">REF No.</td>
            <td ><asp:TextBox ID="txtRefNo" runat="server" Width="200"/></td>
        </tr>
        <tr>
            <td style="width: 60px">LC No.</td>
            <td><asp:TextBox ID="txtLCNo" runat="server" Width="200"/></td>
        </tr>
    </table>

<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Referrence No" DataField="ReperenceNo" />
            <telerik:GridBoundColumn HeaderText="LC No" DataField="LCNo" HeaderStyle-Width="200"  />
            <telerik:GridBoundColumn HeaderText="Type" DataField="TypeDescription" HeaderStyle-Width="160"  />
            <telerik:GridBoundColumn HeaderText="Currency" DataField="CreditCurrency" HeaderStyle-Width="120"  />
            <telerik:GridNumericColumn HeaderText="Credit Amount" DataField="CreditAmount" HeaderStyle-Width="120"  ItemStyle-HorizontalAlign="Right" NumericType="Number" DataFormatString="{0:#,##0.00}"  DecimalDigits="2">
                </telerik:GridNumericColumn>
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" HeaderStyle-Width="70"  />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("ProvisionNo").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>


<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        $("#<%=txtRefNo.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btSearch.ClientID %>").click();
            }
        });
        
        $("#<%=txtLCNo.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btSearch.ClientID %>").click();
            }
        });
    </script>
</telerik:RadCodeBlock>
<div style="visibility:hidden;"><asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" /></div>