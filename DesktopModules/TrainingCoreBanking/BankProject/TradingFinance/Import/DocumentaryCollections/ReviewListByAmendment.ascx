<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReviewListByAmendment.ascx.cs" Inherits="BankProject.TradingFinance.Import.DocumentaryCollections.ReviewListByAmendment" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Documetary Collection Code" DataField="DocCollectCode" />
            <telerik:GridBoundColumn HeaderText="Collection Type" DataField="CollectionType" />
            <telerik:GridBoundColumn HeaderText="CCY" DataField="Currency" />
            <telerik:GridBoundColumn HeaderText="Amount" DataField="Amount" HeaderStyle-Width="100" ItemStyle-Width="100" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Amend_Status" HeaderStyle-Width="100" ItemStyle-Width="100" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("DocCollectCode").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>