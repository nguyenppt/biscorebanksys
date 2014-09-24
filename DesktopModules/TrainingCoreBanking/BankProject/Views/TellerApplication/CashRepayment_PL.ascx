<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CashRepayment_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.CashRepayment_PL" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div>
    <telerik:RadGrid ID="RadGrid" runat="server" AllowPaging="true" AutoGenerateColumns="false" OnNeedDataSource="RadGrid_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn  HeaderText="ID" DataField="ID"  HeaderStyle-Width="70px"    HeaderStyle-HorizontalAlign="center"/>
                                         
                <telerik:GridBoundColumn  HeaderText="Customer Name" DataField="CustomerName" HeaderStyle-Width="190px"  />
                <telerik:GridBoundColumn  HeaderText="Customer Account ID" DataField="CustomerAccountID"   HeaderStyle-Width="150px"/>
                <telerik:GridBoundColumn  HeaderText="Cash Account ID" DataField="CashAccountID"  ItemStyle-HorizontalAlign="Right" 
                                           HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="190px"/>
                <telerik:GridBoundColumn  HeaderText="Balance Amount" DataField="BalanceAmount"  ItemStyle-HorizontalAlign="Right" 
                                           HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="150px"/>
                
                <telerik:GridBoundColumn  HeaderText="Currency" DataField="Currency"   HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="center" 
                                           HeaderStyle-HorizontalAlign="center"/>
                <telerik:GridBoundColumn  HeaderText="Amount Deposited" DataField="AmountDeposited"  ItemStyle-HorizontalAlign="Right" 
                                           HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="200px"/>
                <telerik:GridTemplateColumn>
                    <ItemStyle width="25" />
                    <ItemTemplate>
                        <a href='<%# getUrlPreview(Eval("ID").ToString()) %>'> <img src="Icons/bank/text_preview.png" width="20" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>