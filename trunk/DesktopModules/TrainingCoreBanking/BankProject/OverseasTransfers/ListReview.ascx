<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListReview.ascx.cs" Inherits="BankProject.OverseasTransfers.ListReview" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Overseas Transfer Code" DataField="OverseasTransferCode" />

            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='Default.aspx?tabid=251&disable=1&CodeID=<%# Eval("OverseasTransferCode") %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
   