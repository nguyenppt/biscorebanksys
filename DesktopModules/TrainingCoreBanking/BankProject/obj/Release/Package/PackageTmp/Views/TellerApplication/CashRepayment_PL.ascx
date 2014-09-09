<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CashRepayment_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.CashRepayment_PL" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div>
    <telerik:RadGrid ID="RadGrid" runat="server" AllowPaging="true" AutoGenerateColumns="false" OnNeedDataSource="RadGrid_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn  HeaderText="Customer Accoount" DataField="CustomerAccount"  HeaderStyle-Width="300px"/>
                <telerik:GridBoundColumn  HeaderText="Cash Account" DataField="CashAccount" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Center"
                                           ItemStyle-HorizontalAlign="center" />
                <telerik:GridBoundColumn  HeaderText="Amount Deposited" DataField="AmtDeposited"  ItemStyle-HorizontalAlign="Right" 
                                           HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="200px"/>
                <telerik:GridTemplateColumn>
                    <ItemStyle width="25" />
                    <ItemTemplate>
                        <a href='<%# getUrlPreview(Eval("PLCode").ToString()) %>'> <img src="Icons/bank/text_preview.png" width="20"" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>