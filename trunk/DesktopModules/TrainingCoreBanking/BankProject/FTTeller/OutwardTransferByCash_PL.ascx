<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OutwardTransferByCash_PL.ascx.cs" Inherits="BankProject.FTTeller.OutwardTransferByCash_PL" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="ID" DataField="ID" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center"  />
            <telerik:GridBoundColumn HeaderText="Product Name" DataField="ProductName" HeaderStyle-Width="100" />
            <telerik:GridBoundColumn HeaderText="Sending Name" DataField="SendingName" HeaderStyle-Width="110" />
            <telerik:GridBoundColumn HeaderText="Receiving  Name" DataField="ReceivingName" HeaderStyle-Width="110" />
            <telerik:GridBoundColumn HeaderText="Currency" DataField="Currency" HeaderStyle-Width="50" />

            <telerik:GridBoundColumn HeaderText="Amount" DataField="Amount" ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="right"   HeaderStyle-Width="100" DataType="System.Decimal"  DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Charge Amt" DataField="ChargeAmount" ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="right"  HeaderStyle-Width="100"  DataType="System.Decimal"  DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Charge Vat Amt" DataField="VATChargeAmount" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="100" DataType="System.Decimal"  DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" HeaderStyle-Width="10%"  ItemStyle-HorizontalAlign="center"  HeaderStyle-HorizontalAlign="center"/>
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("ID").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>