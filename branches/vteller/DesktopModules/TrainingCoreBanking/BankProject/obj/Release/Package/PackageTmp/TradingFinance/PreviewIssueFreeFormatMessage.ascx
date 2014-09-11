<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreviewIssueFreeFormatMessage.ascx.cs" Inherits="BankProject.TradingFinance.PreviewIssueFreeFormatMessage" %>
<div style="padding:10px;">
    
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Transaction Reference Number" DataField="TFNo" />
            <telerik:GridBoundColumn HeaderText="Cable Type" DataField="CableType" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("Id").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>

</div>