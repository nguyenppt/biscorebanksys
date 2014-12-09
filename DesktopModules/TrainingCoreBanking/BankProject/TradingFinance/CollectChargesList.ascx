<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollectChargesList.ascx.cs" Inherits="BankProject.TradingFinance.CollectChargesList" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Trans Code" DataField="TransCode" HeaderStyle-Width="150" ItemStyle-Width="150" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />            
            <telerik:GridBoundColumn HeaderText="Amount" DataField="TotalChargeAmount" HeaderStyle-Width="100" ItemStyle-Width="100" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
            <telerik:GridBoundColumn HeaderText="CCY" DataField="ChargeCurrency" HeaderStyle-Width="50" ItemStyle-Width="50" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="150" HorizontalAlign="Right" />
                <ItemTemplate>
                    <a href='Default.aspx?tabid=254&tid=<%# Eval("TransCode").ToString() %>&lst=<%=Request.QueryString["lst"] %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a>
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>