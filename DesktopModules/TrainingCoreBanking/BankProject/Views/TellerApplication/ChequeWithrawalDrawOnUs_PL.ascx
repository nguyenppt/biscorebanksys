<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChequeWithrawalDrawOnUs_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.ChequeWithrawalDrawOnUs_PL" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div style="padding:10px;">
    <telerik:RadGrid ID="RadGridPreview" runat="server" AutoGenerateColumns="false"  AllowPaging="true" 
        OnNeedDataSource="RadGridPreview_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn  HeaderText="Reference ID"  DataField="ID" />
                <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" HeaderStyle-Width="100"  />
                 <telerik:GridBoundColumn HeaderText="Amount Paid" DataField="AmountPaid" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" 
                     HeaderStyle-Width="150"  />
                 <telerik:GridBoundColumn HeaderText="Currency" DataField="Currency" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center" />
                 <telerik:GridBoundColumn HeaderText="Cheque No" DataField="ChequeNo" />
                <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
                <telerik:GridBoundColumn HeaderText="WithDrawal Date" DataField="CreatedDate" />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# getUrlPreview(Eval("ID").ToString()) %>'><img src="Icons/bank/text_preview.png" width="20" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>