<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListExportPayment.ascx.cs" Inherits="BankProject.TradingFinance.Export.DocumentaryCredit.ListExportPayment" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="LC Code" DataField="LCCode" />            
            <telerik:GridTemplateColumn HeaderText="Amount">
                <ItemStyle HorizontalAlign="Right" />
                <ItemTemplate><%# Eval("Amount")  + " " + Eval("Currency") %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn HeaderText="Customer Id" DataField="ApplicantId" />
            <telerik:GridBoundColumn HeaderText="Customer Name" DataField="ApplicantName" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="150" />
                <ItemTemplate><%# GenerateEnquiryButtons(Eval("Id").ToString()) %> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>