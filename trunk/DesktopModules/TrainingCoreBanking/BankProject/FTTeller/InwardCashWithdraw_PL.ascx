<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InwardCashWithdraw_PL.ascx.cs" Inherits="BankProject.FTTeller.InwardCashWithdraw_PL" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Reperence No" DataField="ReperenceNo" HeaderStyle-Width="10%" />
            <telerik:GridBoundColumn HeaderText="BO Name" DataField="BOName" HeaderStyle-Width="25%" />
            <telerik:GridBoundColumn HeaderText="FO Name" DataField="FOName" HeaderStyle-Width="25%" />
            <telerik:GridBoundColumn HeaderText="Debit Amount LCY" DataField="DebitAmtLCY" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="15%"/>
            <telerik:GridBoundColumn HeaderText="Cr Amount LCY" DataField="CrbitAmtLCY" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="15%"/>
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" HeaderStyle-Width="10%" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("LCCode").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>