<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenCustomerAccountList.ascx.cs" Inherits="BankProject.Views.TellerApplication.OpenCustomerAccountList" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Account Code" DataField="AccountCode" />
            <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" />
            <telerik:GridBoundColumn HeaderText="Currency" DataField="Currency" />

            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>  
                    <a href='Default.aspx?tabid=119&codeid=<%# Eval("Id") %>'>
                        <img src="Icons/bank/text_preview.png" alt="" title="" style="" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
   