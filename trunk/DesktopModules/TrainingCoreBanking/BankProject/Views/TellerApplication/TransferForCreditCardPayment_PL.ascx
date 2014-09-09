<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransferForCreditCardPayment_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.TransferForCreditCardPayment_PL" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<div style="padding:10px;">
    <telerik:RadGrid ID="RadGridPreview" runat="server" AutoGenerateColumns="false"  AllowPaging="true" 
        OnNeedDataSource="RadGridPreview_OnNeedDataSource" HeaderStyle-HorizontalAlign="Justify">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn  HeaderText="CustomerID"  DataField="CustomerID" HeaderStyle-Width="150" />
                <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" HeaderStyle-Width="190"/>
                <telerik:GridBoundColumn HeaderText="Debit Amount LCY" HeaderStyle-Width="105" HeaderStyle-HorizontalAlign="Right" DataField="DebitAmountLCY" ItemStyle-HorizontalAlign="Right"/>
                <telerik:GridBoundColumn HeaderText="Value Date" DataField="ValueDate" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150"
                    HeaderStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# getUrlPreview(Eval("LCCode").ToString()) %>'><img src="Icons/bank/text_preview.png" width="20" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>