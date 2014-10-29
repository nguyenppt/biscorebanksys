<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransferByAccount_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.TransferByAccount_PL" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Reference No" DataField="ID" HeaderStyle-Width="100" />
            <telerik:GridBoundColumn HeaderText="Product Name" DataField="ProductName" HeaderStyle-Width="150" />
            <telerik:GridBoundColumn HeaderText="Sending Name" DataField="SendingName" HeaderStyle-Width="150" />
            <telerik:GridBoundColumn HeaderText="Receiving  Name" DataField="ReceivingName" HeaderStyle-Width="150" />
            <telerik:GridBoundColumn HeaderText="Currency" DataField="Currency" HeaderStyle-Width="90" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center" />
            <telerik:GridBoundColumn HeaderText="Amount" DataField="DebitAmount" ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="right"  HeaderStyle-Width="120"
                            dataType="System.Decimal" DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Charge Amount" DataField="ChargeAmtLCY" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="100"
                           dataType="System.Decimal" DataFormatstring="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Charge Vat Amt" DataField="ChargeVATAmt" ItemStyle-HorizontalAlign="Right" 
                 HeaderStyle-Width="100" DataType="System.Decimal"  DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status"  ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center"/>
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("ID").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>