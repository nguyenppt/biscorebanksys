<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransferWithdrawalPreviewList.ascx.cs" Inherits="BankProject.Views.TellerApplication.TransferWithdrawalPreviewList" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Transfer Code" HeaderStyle-Width = "11%" DataField="Code" />
            <telerik:GridBoundColumn HeaderText="Account Type" HeaderStyle-Width = "18%" DataField="AccountTypeName" />
            <telerik:GridBoundColumn HeaderText="Debit Account Code" HeaderStyle-Width = "11%" DataField="DebitAccountCode" />
            <telerik:GridBoundColumn HeaderText="Debit Amount" DataField="DebitAmount" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width = "12%"  ItemStyle-HorizontalAlign="Right" DataType="System.Decimal"  DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Ballance" DataField="CustBallance" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width = "12%"  ItemStyle-HorizontalAlign="Right" DataType="System.Decimal" DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="New Ballance" DataField="NewCustBallance" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width = "12%"  ItemStyle-HorizontalAlign="Right" DataType="System.Decimal" DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Credit Account Code" HeaderStyle-Width = "11%" DataField="CreditAccountCode" />
            <telerik:GridBoundColumn HeaderText="Amount Credit" DataField="AmountCreditForCustomer" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width = "12%"  ItemStyle-HorizontalAlign="Right" DataType="System.Decimal"  DataFormatString="{0:N}" />

            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>  
                    <a href='Default.aspx?tabid=126&preview=1&codeid=<%# Eval("Id") %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>    
</telerik:RadGrid>