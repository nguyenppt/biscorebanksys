<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReviewExport_DocumentCollection.ascx.cs" Inherits="BankProject.TradingFinance.Export.DocumentaryCollections.ReviewExport_DocumentCollection" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Documetary Collection Code" DataField="DocCollectCode" />
            <telerik:GridBoundColumn HeaderText="Collection Type" DataField="CollectionType" />
            <telerik:GridBoundColumn HeaderText="CCY" DataField="Currency" />
            <telerik:GridBoundColumn HeaderText="Amount" DataField="Amount" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("DocCollectCode").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>