<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollectionForCreditCardPayment_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.CollectionForCreditCardPayment_PL" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
             <telerik:GridBoundColumn HeaderText="Reference No" DataField="ID" HeaderStyle-Width ="100" />
            <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" HeaderStyle-Width ="150"/>
            <telerik:GridBoundColumn HeaderText="Debit Account" DataField="DebitAccount" HeaderStyle-Width ="150"/>
            <telerik:GridBoundColumn HeaderText="Debit Amount" DataField="DebitAmt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width ="100" />
            <telerik:GridBoundColumn HeaderText="Debit Currency" DataField="DebitCurrency" HeaderStyle-Width ="100"
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>

            <telerik:GridBoundColumn HeaderText="Credit Amount" DataField="CreditAmt" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width ="100" />
            <telerik:GridBoundColumn HeaderText="Credit Currency" DataField="CreditCurrency" HeaderStyle-Width ="100"
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>

            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" HeaderStyle-Width ="50" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("ID").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>