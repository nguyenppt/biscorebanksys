<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForeignExchangeTradePreviewList.ascx.cs" Inherits="BankProject.Views.TellerApplication.ForeignExchangeTradePreviewList" %>
<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
         OnClientButtonClicking="RadToolBar1_OnClientButtonClicking" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommit" CommandName="commit" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btPreview" CommandName="preview" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="search">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print" Value="btPrint" CommandName="print" Enabled="false">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
<div style="padding:10px;">
    <%if (string.IsNullOrEmpty(lstType) || !lstType.ToLower().Equals("4appr")){ %>
    <table cellpadding="0" cellspacing="0" style="margin-bottom:10px">
        <tr>
            <td style="padding-left:10px; padding-right:10px;">Ref No</td>
            <td ><asp:TextBox ID="txtRefNo" runat="server" Width="150"/></td>
            <td style="padding-left:10px; padding-right:10px;">TF No</td>
            <td ><asp:TextBox ID="txtTFNo" runat="server" Width="150"/></td>
        </tr>
    </table>
    <%} %>
    <telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Ref No." DataField="RefNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="FT No." DataField="TFNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="BuyAmount" DataField="BuyAmount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderText="BuyCurrency" DataField="BuyCurrency" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderText="SellAmount" DataField="SellAmount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderText="SellCurrency" DataField="SellCurrency" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="50" HorizontalAlign="Right" />
                    <ItemTemplate>
                        <a href='Default.aspx?tabid=267&tid=<%# Eval("RefNo").ToString() %>&lst=<%=Request.QueryString["lst"] %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a>
                    </itemtemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function RadToolBar1_OnClientButtonClicking(sender, args) {
        }
    </script>
</telerik:RadCodeBlock>