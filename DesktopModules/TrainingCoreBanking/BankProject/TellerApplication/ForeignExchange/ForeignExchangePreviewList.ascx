<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForeignExchangePreviewList.ascx.cs" Inherits="BankProject.Views.TellerApplication.ForeignExchange.ForeignExchangePreviewList" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Reperence No" DataField="ReperenceNo" />
            <telerik:GridBoundColumn HeaderText="Customer Account" DataField="CustomerAccount" />
            <telerik:GridBoundColumn HeaderText="Amt LCY" DataField="AmtLCY" />
            <telerik:GridBoundColumn HeaderText="Amt FCY" DataField="AmtFCY" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("LCCode").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>