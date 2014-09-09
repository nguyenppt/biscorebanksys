<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChequeIssue_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.ChequeIssue_PL" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div style="padding:10px;">
    <telerik:RadGrid ID="RadGridPreview" runat="server"  AutoGenerateColumns="false" AllowPaging="true"  
        OnNeedDataSource="RadGridPreview_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Cheque Reference" DataField="ChequeID"  HeaderStyle-Width="120"
                    ItemStyle-HorizontalAlign="left" />
                <telerik:GridBoundColumn HeaderText="Cheque Type" DataField="ChequeType" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="150" />
                <telerik:GridBoundColumn HeaderText="WorkingAccount" DataField="WorkingAcctID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="150" />
                <telerik:GridBoundColumn HeaderText="Quantity" DataField="Quantity"  ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150"
                    HeaderStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderText="Serial Number" DataField="ChequeNo"  HeaderStyle-Width="150"/>
                 <telerik:GridBoundColumn HeaderText="Issue Date" DataField="IssueDate" ItemStyle-HorizontalAlign="Right" 
                    HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right"/>
                <telerik:GridTemplateColumn>
                    <ItemStyle width="25" />
                    <ItemTemplate>
                        <a href='<%# getUrlPreview(Eval("ChequeID").ToString()) %>'> <img src="Icons/bank/text_preview.png" width="20"" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>
