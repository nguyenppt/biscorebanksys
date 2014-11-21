<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CashDepositePreviewList.ascx.cs" Inherits="BankProject.Views.TellerApplication.CashDepositePreviewList" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Deposit Code" HeaderStyle-Width = "15%" DataField="Code" />
            <telerik:GridBoundColumn HeaderText="Account Type" HeaderStyle-Width = "20%" DataField="AccountTypeName" />
            <telerik:GridBoundColumn HeaderText="Account Code" HeaderStyle-Width = "15%" DataField="CustomerAccount" />
            <telerik:GridBoundColumn HeaderText="Deposit Amount" DataField="AmountDeposited" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width = "15%"  ItemStyle-HorizontalAlign="Right" DataType="System.Decimal"  DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Ballance" DataField="CustBallance" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width = "15%"  ItemStyle-HorizontalAlign="Right" DataType="System.Decimal" DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="New Ballance" DataField="NewCustBallance" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width = "15%"  ItemStyle-HorizontalAlign="Right" DataType="System.Decimal" DataFormatString="{0:N}" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>  
                    <a href='Default.aspx?tabid=124&preview=1&codeid=<%# Eval("Code") %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>    
</telerik:RadGrid>