<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentList.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.CurrentNonTermSavingAC.SalaryPayment.PaymentList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div style="padding: 10px;">
    <telerik:radgrid runat="server" autogeneratecolumns="False"
         id="radGridReview" allowpaging="false" onneeddatasource="radGridReview_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Ref Id" DataField="RefId" />
                <telerik:GridBoundColumn HeaderText="Fequency" DataField="Fequency" />
                <telerik:GridBoundColumn HeaderText="Term" DataField="Term" />
                <telerik:GridBoundColumn HeaderText="End Date" DataField="EndDate" />
                <telerik:GridBoundColumn HeaderText="Currency" DataField="Currency" />
                <telerik:GridBoundColumn HeaderText="Total Debit Amt" DataField="TotalDebitAmt" 
                    DataType="System.Decimal" DataFormatString="{0:N}" />
                
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='Default.aspx?tabid=<%=TabId %>&RefId=<%# Eval("RefId") %>&disable=true&mode=preview'>
                            <img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> 
                        </a> 
                    </itemtemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:radgrid>
</div>


   