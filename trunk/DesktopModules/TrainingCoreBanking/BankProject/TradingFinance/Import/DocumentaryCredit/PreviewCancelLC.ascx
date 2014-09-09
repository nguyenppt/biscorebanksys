<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreviewCancelLC.ascx.cs" Inherits="BankProject.TradingFinance.Import.DocumentaryCredit.PreviewCancelLC" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="LC Code" DataField="NormalLCCode" />
            <telerik:GridBoundColumn HeaderText="LC Type" DataField="LCType" />
            <telerik:GridBoundColumn HeaderText="CCY" DataField="Currency" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Cancel_Status" >
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