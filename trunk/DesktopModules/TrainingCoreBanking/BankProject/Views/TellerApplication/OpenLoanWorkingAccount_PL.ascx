<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenLoanWorkingAccount_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.OpenLoanWorkingAccount_PL" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div style="padding: 10px;">
    <telerik:RadGrid ID="RadGridPreview" runat="server" AutoGenerateColumns="false"  AllowPaging="true" 
        	 OnNeedDataSource="RadGridPreview_OnNeedDataSource">
        <MasterTableView>
           <Columns>
                <telerik:GridBoundColumn HeaderText="Account ID" DataField="ID" />
                <telerik:GridBoundColumn HeaderText="Customer ID" DataField="CustomerID" />
                <telerik:GridBoundColumn HeaderText="Account Name" DataField="AccountName" />
                <telerik:GridBoundColumn HeaderText="Category Name" DataField="Categoryname" />
                <telerik:GridBoundColumn HeaderText="ProductLine Description" DataField="ProductLineDescription" />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# GeturlReview(Eval("ID").ToString()) %>'>
                            <img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> 
                        </a> 
                    </itemtemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>
