<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreviewAmendLC.ascx.cs" Inherits="BankProject.TradingFinance.Import.DocumentaryCredit.PreviewAmendLC" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="LC Code" DataField="NormalLCCode" />
            <telerik:GridBoundColumn HeaderText="LC Type" DataField="LCType" />
            <telerik:GridBoundColumn HeaderText="CCY" DataField="Currency" />
            <telerik:GridBoundColumn HeaderText="Amount" DataField="Amount" >
                <ItemStyle Width="100" HorizontalAlign="Right" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Status" DataField="Amend_Status" >
                <ItemStyle Width="100" HorizontalAlign="Center" />
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("NormalLCCode").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>