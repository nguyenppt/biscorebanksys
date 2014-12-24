<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenDepositAcctForTFList.ascx.cs" Inherits="BankProject.TradingFinance.OpenDepositAcctForTFList" %>
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
            <td style="padding-left:10px; padding-right:10px;">Deposite Account</td>
            <td ><asp:TextBox ID="txtDepositeAccount" runat="server" Width="150"/></td>
            <td style="padding-left:10px; padding-right:10px;"></td>
            <td ></td>
        </tr>
        <tr>
            <td style="padding-left:10px; padding-right:10px;">Customer ID</td>
            <td style="padding-top:5px;"><asp:TextBox ID="txtCustomerID" runat="server" Width="150"/></td>
            <td style="padding-left:10px; padding-right:10px;">Customer Name</td>
            <td ><asp:TextBox ID="txtCustomerName" runat="server" Width="150"/></td>
        </tr>
        <tr>
            <td style="padding-left:10px; padding-right:10px;">Account Name</td>
            <td style="padding-top:5px;"><asp:TextBox ID="txtAccountName" runat="server" Width="150"/></td>
            <td style="padding-left:10px; padding-right:10px;">Account Mnemonic</td>
            <td ><asp:TextBox ID="txtAccountMnemonic" runat="server" Width="150"/></td>
        </tr>
    </table>
    <%} %>
    <telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Deposit Code" DataField="DepositCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="Currentcy" DataField="Currentcy" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderText="Customer ID" DataField="CustomerID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="Account Name" DataField="AccountName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="Account Mnemonic" DataField="AccountMnemonic" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="Product Line" DataField="ProductLine" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="50" HorizontalAlign="Right" />
                    <ItemTemplate>
                        <a href='Default.aspx?tabid=89&tid=<%# Eval("DepositCode").ToString() %>&lst=<%=Request.QueryString["lst"] %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a>
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