<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentList.ascx.cs" Inherits="BankProject.TradingFinance.Import.DocumentaryCredit.PaymentList" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="LC Code" DataField="LCCode" />
            <telerik:GridBoundColumn HeaderText="Customer Account" DataField="CustomerAccount" />
            <telerik:GridBoundColumn HeaderText="Amt LCY" DataField="AmtLCY" ItemStyle-HorizontalAlign="Right" />
            <telerik:GridBoundColumn HeaderText="Amt FCY" DataField="AmtFCY" ItemStyle-HorizontalAlign="Right" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="150" />
                <ItemTemplate><%# GenerateEnquiryButtons(Eval("ReperenceNo").ToString()) %> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>