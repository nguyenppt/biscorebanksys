<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AmendLoanContract_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.AmendLoanContract_PL" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
 <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="true" AutoGenerateColumns="false"  OnNeedDataSource="RadGrid_OnNeedDataSource">
        <MasterTableView>
            <Columns>
               <telerik:GridBoundColumn HeaderText="Customer ID" DataField="CustomerID" />
                <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" HeaderStyle-Width="130" />
                <telerik:GridBoundColumn HeaderText="Loan Contract Reference" DataField="LoanContractReference" HeaderStyle-Width="100"/>
                <telerik:GridBoundColumn HeaderText="Working Balance"   DataField="WorkingBalance" 
                                          ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="Right"/>
                <telerik:GridboundColumn HeaderText="Total Paid Amount" DataField="TTpaidAmount"  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderText="Purpose" DataField="Purpose" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" />
<%--               <telerik:GridBoundColumn HeaderText="Repay Amount" HeaderStyle-Width="150" DataField="RepayAmount" ItemStyle-HorizontalAlign="right" HeaderStyle-HorizontalAlign="right" />--%>
                <telerik:GridBoundColumn HeaderText="Kind Of Contract" DataField="KindOfContract" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" />
                <telerik:GridBoundColumn HeaderText="New Mat Date" DataField="RepayDate" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center" />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <Itemtemplate>
                        <a href='<%# geturlReview(Eval("LoanContractReference").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /></a> 
                    </Itemtemplate>
                </telerik:GridTemplateColumn>
                </Columns>
        </MasterTableView>
    </telerik:RadGrid>