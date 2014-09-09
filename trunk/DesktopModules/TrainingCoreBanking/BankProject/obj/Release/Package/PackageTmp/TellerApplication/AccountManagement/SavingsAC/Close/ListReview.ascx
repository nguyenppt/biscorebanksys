<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListReview.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Close.ListReview" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div style="padding: 10px;">
    <telerik:radgrid runat="server" autogeneratecolumns="False"
         id="grdSavingAccReviewList" allowpaging="false" onneeddatasource="grdSavingAccReviewList_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Ref Id" DataField="RefId" />
                <telerik:GridBoundColumn HeaderText="Category " DataField="AccCategory" />
                <telerik:GridBoundColumn HeaderText="CCY" DataField="Currency" />
                <telerik:GridBoundColumn HeaderText="Product Line" DataField="ProductLineId" />
                <telerik:GridBoundColumn HeaderText="Principal" DataField="AZPrincipal" 
                    DataType="System.Decimal" DataFormatString="{0:N}" />
                <telerik:GridBoundColumn HeaderText="Working Acc " DataField="AZWorkingAccount " />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# GeturlReview(Eval("RefId").ToString(),Eval("FromTable").ToString()) %>'>
                            <img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> 
                        </a> 
                    </itemtemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:radgrid>
</div>