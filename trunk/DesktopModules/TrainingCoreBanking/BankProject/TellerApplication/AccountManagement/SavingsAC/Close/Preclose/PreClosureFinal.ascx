<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreClosureFinal.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Close.PreClose.PreClosureFinal" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Working Account" DataField="Account" />
            <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" />
            <telerik:GridBoundColumn HeaderText="TypeCode" DataField="TypeCode" Display="false" />

            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("Account").ToString(),Eval("TypeCode").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" /> </a>  
                </itemtemplate>
            </telerik:GridTemplateColumn>

        </Columns>
    </MasterTableView>
</telerik:RadGrid>
   