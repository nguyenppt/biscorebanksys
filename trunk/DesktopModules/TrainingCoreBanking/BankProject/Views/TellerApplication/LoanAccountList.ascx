<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoanAccountList.ascx.cs" Inherits="BankProject.Views.TellerApplication.LoanAccountList" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>

            <telerik:GridBoundColumn HeaderText="Code" DataField="Code" HeaderStyle-Width = "25%"  />
            <telerik:GridBoundColumn HeaderText="Customer ID" DataField="CustomerID" HeaderStyle-Width = "25%" />
            <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" HeaderStyle-Width = "40%" />
            <telerik:GridBoundColumn HeaderText="Loan Amount" DataField="LoanAmount" HeaderStyle-Width = "10%"  ItemStyle-HorizontalAlign="Right" DataType="System.Decimal"  DataFormatString="{0:N}" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='Default.aspx?tabid=196&codeid=<%# Eval("Code") %>'>
                        <img src="Icons/bank/text_preview.png" alt="" title="" style="" />
                    </a>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
   