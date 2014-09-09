<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PastDueLoanRepayment_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.PastDueLoanRepayment_PL" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
 <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="true" AutoGenerateColumns="false"  OnNeedDataSource="RadGrid_OnNeedDataSource">
        <MasterTableView>
            <Columns>
               <telerik:GridBoundColumn HeaderText="Customer ID" DataField="CustomerID" />
                <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" />
                <telerik:GridBoundColumn HeaderText="Loan Contract Reference" DataField="LoanContractReference" />
                <telerik:GridBoundColumn HeaderText="Purpose" DataField="Purpose" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" />
               <telerik:GridBoundColumn HeaderText="Repay Amount" HeaderStyle-Width="150" DataField="RepayAmount" ItemStyle-HorizontalAlign="right" HeaderStyle-HorizontalAlign="right" />
                <telerik:GridBoundColumn HeaderText="RepayDate" DataField="RepayDate" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center" />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <Itemtemplate>
                        <a href='<%# geturlReview(Eval("LoanContractReference").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /></a> 
                    </Itemtemplate>
                </telerik:GridTemplateColumn>
                </Columns>
        </MasterTableView>
    </telerik:RadGrid>