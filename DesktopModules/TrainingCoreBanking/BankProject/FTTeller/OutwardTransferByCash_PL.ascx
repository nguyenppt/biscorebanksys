<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OutwardTransferByCash_PL.ascx.cs" Inherits="BankProject.FTTeller.OutwardTransferByCash_PL" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Reperence No" DataField="ReperenceNo" HeaderStyle-Width="10%" />
            <telerik:GridBoundColumn HeaderText="Sending Name" DataField="SendingName" HeaderStyle-Width="25%" />
            <telerik:GridBoundColumn HeaderText="Receiving  Name" DataField="ReceivingName" HeaderStyle-Width="25%" />
            <telerik:GridBoundColumn HeaderText="Amount LCY" DataField="AmtLCY" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="10%"/>
            <telerik:GridBoundColumn HeaderText="Charge Amt LCY" DataField="ChargeAmtLCY" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="10%"/>
            <telerik:GridBoundColumn HeaderText="Charge Vat Amt" DataField="ChargeVatAmt" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="10%"/>
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