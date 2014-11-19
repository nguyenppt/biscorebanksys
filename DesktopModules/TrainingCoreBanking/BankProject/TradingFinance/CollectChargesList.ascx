<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollectChargesList.ascx.cs" Inherits="BankProject.TradingFinance.CollectChargesList" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Trans Code" DataField="TransCode" />            
            <telerik:GridTemplateColumn HeaderText="Amount">
                <ItemStyle HorizontalAlign="Right" />
                <ItemTemplate><%# Eval("DebitAmount")  + " " + Eval("DebitCurrency") %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn HeaderText="Order Customer Id" DataField="OrderCustomerId" />
            <telerik:GridBoundColumn HeaderText="Order Customer Name" DataField="OrderCustomerName" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="150" />
                <ItemTemplate>
                    <a href='Default.aspx?tabid=254&tid=<%# Eval("TransCode").ToString() %>&lst=<%=Request.QueryString["lst"] %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a>
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>