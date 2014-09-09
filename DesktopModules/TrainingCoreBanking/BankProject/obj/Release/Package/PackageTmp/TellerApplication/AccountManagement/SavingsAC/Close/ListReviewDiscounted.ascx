<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListReviewDiscounted.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Close.ListReviewDiscounted" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div style="padding: 10px;">
    <telerik:radgrid runat="server" autogeneratecolumns="False"
         id="grdSavingAccReviewList" allowpaging="false" onneeddatasource="grdSavingAccReviewList_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Ref Id" DataField="RefId" />
                <telerik:GridBoundColumn HeaderText="Ld  Id " DataField="LDId " />
                <telerik:GridBoundColumn HeaderText="Working acc id " DataField="TDWorkingAccountId" />
                <telerik:GridBoundColumn HeaderText="Working acc name" DataField="TDWorkingAccountName" />
                <telerik:GridBoundColumn HeaderText="Currency" DataField="TDCurrency" />
                <telerik:GridBoundColumn HeaderText="Principal" DataField="TDAmmount" 
                    DataType="System.Decimal" DataFormatString="{0:N}" />             
                
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# GeturlReview(Eval("refId").ToString()) %>'>
                            <img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> 
                        </a> 
                    </itemtemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:radgrid>
</div>