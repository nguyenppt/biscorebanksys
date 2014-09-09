<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UnBlockAccountList.ascx.cs" Inherits="BankProject.Views.TellerApplication.UnBlockAccountList" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Account Code" DataField="Code" />

            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>  
                    <a href='Default.aspx?tabid=263&ctl=UnBlockAccount&mid=795&unblockaccount=1&codeid=<%# Eval("Id") %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>    
</telerik:RadGrid>