<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForeignExchangeTradePreviewList.ascx.cs" Inherits="BankProject.Views.TellerApplication.ForeignExchangeTradePreviewList" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
             <telerik:GridBoundColumn HeaderText="Foreign Exchange Code" DataField="Code" />
            <telerik:GridBoundColumn HeaderText="FT No." DataField="FTNo" />
            
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("Code").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>