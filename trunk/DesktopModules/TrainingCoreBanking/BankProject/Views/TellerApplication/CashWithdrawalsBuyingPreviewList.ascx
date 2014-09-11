<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CashWithdrawalsBuyingPreviewList.ascx.cs" Inherits="BankProject.Views.TellerApplication.CashWithdrawalsBuyingPreviewList" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"  />
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btPreview" CommandName="preview" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="search" ValidationGroup="Commit">
        </telerik:RadToolBarButton>
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print" Enabled="false">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
</div>
<div style="padding:10px;">
    <div>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer Id </td>
                <td class="MyContent"><asp:TextBox ID="txtCustomerId" runat="server" Width="200" /></td>
                <td class="MyLable">Customer name </td>
                <td class="MyContent"><asp:TextBox ID="txtCustomerName" runat="server" Width="200" /></td>                
            </tr>          
        </table>
    </div>
    <div>
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Reperence No" DataField="ReperenceNo" />
            <telerik:GridBoundColumn HeaderText="Customer Account" DataField="CustomerAccount" />
            <telerik:GridBoundColumn HeaderText="Amt LCY" DataField="AmtLCY" ItemStyle-HorizontalAlign="Right" />
            <telerik:GridBoundColumn HeaderText="Amt FCY" DataField="AmtFCY" ItemStyle-HorizontalAlign="Right" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="150" />
                <ItemTemplate><%# BankProject.Controls.Commont.GenerateEnquiryButtons(Eval("ReperenceNo").ToString(), Eval("Status").ToString(), this.TabId, this.TabId, this.TabId, this.TabId) %> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>
    </div>