<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Enquiry.ascx.cs" Inherits="BankProject.TellerApplication.ForeignExchange.Enquiry" %>
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="TT No" DataField="TTNo" />
            <telerik:GridBoundColumn HeaderText="Account" DataField="Account" />
            <telerik:GridTemplateColumn HeaderText="Amount">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate><%# Eval("Amount")  + " " + Eval("Currency") %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
            <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" />
            <telerik:GridBoundColumn HeaderText="Customer PassportNo" DataField="CustomerPassportNo" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="150" />
                <ItemTemplate><%# GenerateEnquiryButtons(Eval("TTNo").ToString()) %> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>