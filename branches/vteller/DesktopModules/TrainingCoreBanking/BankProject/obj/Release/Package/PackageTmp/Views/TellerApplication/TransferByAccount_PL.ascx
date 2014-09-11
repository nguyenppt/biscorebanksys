<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransferByAccount_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.TransferByAccount_PL" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Reference No" DataField="ReferenceNo" HeaderStyle-Width="20%" />
            <telerik:GridBoundColumn HeaderText="Sending Name" DataField="SendingName" HeaderStyle-Width="20%" />
            <telerik:GridBoundColumn HeaderText="Receiving  Name" DataField="ReceivingName" HeaderStyle-Width="15%" />
            <telerik:GridBoundColumn HeaderText="Amount" DataField="Amt" ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="right"  HeaderStyle-Width="200"/>
            <telerik:GridBoundColumn HeaderText="Charge Amount" DataField="ChargeAmt" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width=""/>
           <%-- <telerik:GridBoundColumn HeaderText="Charge Vat Amt" DataField="ChargeVatAmt" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="10%"/>--%>
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status"/>
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("LCCode").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>