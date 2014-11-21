﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Preview_DocumentProcessing.ascx.cs" Inherits="BankProject.TradingFinance.Import.DocumentaryCredit.Preview_DocumentProcessing" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" 
    OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Docs No." DataField="PaymentId" HeaderStyle-Width="150" ItemStyle-Width="150" />
            <telerik:GridTemplateColumn HeaderText="Amount" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="100" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate><%# Eval("Amount")  + " " + Eval("Currency") %></ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" HeaderStyle-Width="50" ItemStyle-Width="50"/>
            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Right">
                <ItemTemplate><a href='<%# geturlReview(Eval("PaymentId").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a></itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>