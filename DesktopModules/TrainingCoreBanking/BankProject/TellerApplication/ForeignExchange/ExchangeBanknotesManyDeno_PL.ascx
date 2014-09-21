<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExchangeBanknotesManyDeno_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.ForeignExchange.ExchangeBanknotesManyDeno_PL" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Reperence No" DataField="ReperenceNo" HeaderStyle-Width="20%" />
            <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerAccount" HeaderStyle-Width="40%" />
            <telerik:GridBoundColumn HeaderText="Amount" DataField="AmtFCY" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="15%"/>
            <telerik:GridBoundColumn HeaderText="Amt Pay Cust" DataField="AmtLCY" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="15%" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" HeaderStyle-Width="10%" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("LCCode").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>