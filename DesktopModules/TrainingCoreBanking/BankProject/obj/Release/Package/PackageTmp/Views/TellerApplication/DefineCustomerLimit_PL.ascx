<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefineCustomerLimit_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.DefineCustomerLimit_PL" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div style="padding:10px;">
    <telerik:RadGrid ID="RadGridPreview" runat="server"  AutoGenerateColumns="false" AllowPaging="true" 
         onNeedDataSource="RadGridPreview_onNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Reference No" DataField="ReferenceNo" HeaderStyle-Width="150"/>
                <telerik:GridBoundColumn HeaderText="Approved Date" DataField="ApprovedDate" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderText="Limit Amount" DataField="LimitAmt" HeaderStyle-Width="150" />
                <telerik:GridBoundColumn Headertext="Fixed/Variable" HeaderStyle-HorizontalAlign="Center" DataField="FV" ItemStyle-HorizontalAlign="Center"/>
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="30px" />
                    <ItemTemplate>
                        <a href='<%# geturlPreview(Eval("LCCode").ToString()) %>'><img src="Icons/bank/text_preview.png" width="20" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>

<%--     <telerik:RadGrid ID="RadGridPreview" runat="server" AutoGenerateColumns="false"  AllowPaging="true" 
        OnNeedDataSource="RadGridPreview_onNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn  HeaderText="Reference No"  DataField="ReferenceNo" />
                <telerik:GridBoundColumn HeaderText="Approved Date" DataField="ApprovedDate" />
                <telerik:GridBoundColumn HeaderText="Limit Amount" DataField="LimitAmt" />
                <telerik:GridBoundColumn HeaderText="Fixed/Variable" DataField="FV" />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# geturlPreview(Eval("LCCode").ToString()) %>'><img src="Icons/bank/text_preview.png" width="20" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>--%>
</div>