<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListReview.ascx.cs" Inherits="BankProject.TradingFinance.OverseasFundsTransfer.ListReview" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Transfer Code" DataField="OverseasTransferCode" />
            <telerik:GridBoundColumn HeaderText="Transaction Type" DataField="TransactionType" />
            <telerik:GridBoundColumn HeaderText="Amount" DataField="Amount" />
            <telerik:GridBoundColumn HeaderText="Currency" DataField="Currency" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
            <telerik:GridBoundColumn HeaderText="CreateDate" DataField="CreateDate" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='Default.aspx?tabid=251&disable=1&CodeID=<%# Eval("OverseasTransferCode") %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="width:20px;" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
   