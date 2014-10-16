<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListCreditExportProcessing.ascx.cs" Inherits="BankProject.TradingFinance.Export.DocumentaryCredit.ListCreditExportProcessing" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" 
    OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Docs No." DataField="PaymentId" />
            <telerik:GridTemplateColumn HeaderText="Amount">
                <ItemStyle HorizontalAlign="Right" />
                <ItemTemplate><%# Eval("Amount")  + " " + Eval("Currency") %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("PaymentId").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>