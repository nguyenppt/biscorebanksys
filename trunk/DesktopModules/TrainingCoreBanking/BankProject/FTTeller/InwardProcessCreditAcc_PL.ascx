<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InwardProcessCreditAcc_PL.ascx.cs" Inherits="BankProject.FTTeller.InwardProcessCreditAcc_PL" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Reference No" DataField="ID" HeaderStyle-Width="90" />
            <telerik:GridBoundColumn HeaderText="BO Name" DataField="BOName" HeaderStyle-Width="200" />
            <telerik:GridBoundColumn HeaderText="FO Name" DataField="FOName" HeaderStyle-Width="200" />
            <telerik:GridBoundColumn HeaderText="Credit Currency" DataField="CreditCurrency" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center" />
            <telerik:GridBoundColumn HeaderText="Credit Amount LCY" DataField="CreditAmtLCY" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="150"
                HeaderStyle-HorizontalAlign="Right" datatype="System.Decimal" DataFormatString="{0:N}"/>
            <telerik:GridBoundColumn HeaderText="Credit Amount FCY" DataField="CreditAmtfCY" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="150"
                HeaderStyle-HorizontalAlign="Right" datatype="System.Decimal" DataFormatString="{0:N}"/>
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" HeaderStyle-Width="90" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("ID").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>