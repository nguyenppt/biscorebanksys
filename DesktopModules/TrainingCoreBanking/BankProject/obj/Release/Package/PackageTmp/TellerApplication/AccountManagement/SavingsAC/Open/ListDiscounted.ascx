<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListDiscounted.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Open.ListDiscounted" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div style="padding: 10px;">
    <telerik:radgrid runat="server" autogeneratecolumns="False"
         id="grdSavingAccReviewList" allowpaging="false" onneeddatasource="grdSavingAccReviewList_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Ref Id" DataField="refId" />                 
                <telerik:GridBoundColumn HeaderText="CCY" DataField="PaymentCCY" />
                  <telerik:GridBoundColumn HeaderText="Principal" DataField="TDAmmount" 
                    DataType="System.Decimal" DataFormatString="{0:N}" />
                <telerik:GridBoundColumn HeaderText="Product Line" DataField="TDProductLineId" />
              
                <telerik:GridBoundColumn HeaderText="Working account" DataField="TDWorkingAccountId" />
                
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