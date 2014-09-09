<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollectionForCreditCardPayment_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.CollectionForCreditCardPayment_PL" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
             <telerik:GridBoundColumn HeaderText="Reperence No" DataField="ReperenceNo" HeaderStyle-Width ="25%" />
            <telerik:GridBoundColumn HeaderText="Customer Account" DataField="CustomerAccount" HeaderStyle-Width ="50%"/>
            <telerik:GridBoundColumn HeaderText="Amt LCY" DataField="AmtLCY" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width ="10%" />
            <telerik:GridBoundColumn HeaderText="Amt FCY" DataField="AmtFCY" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width ="10%" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" HeaderStyle-Width ="5%" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("LCCode").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>