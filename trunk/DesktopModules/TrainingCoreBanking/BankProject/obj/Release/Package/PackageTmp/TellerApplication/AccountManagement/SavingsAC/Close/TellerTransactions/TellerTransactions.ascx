<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TellerTransactions.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Close.TellerTransactions.TellerTransactions" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

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
                <telerik:GridBoundColumn HeaderText="TotalAmt " DataField="TotalAmt "      
                    DataType="System.Decimal" DataFormatString="{0:N}"/>
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

<%--<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource" BorderWidth="1">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Teller Trans ID" DataField="TellerTrans" >
                <ItemStyle Width="20%" HorizontalAlign="Left"/> 
            </telerik:GridBoundColumn> 

            <telerik:GridBoundColumn HeaderText="Amount" DataField="AmountLcy" HeaderStyle-HorizontalAlign="Right" >
                <ItemStyle Width="20%" HorizontalAlign="Right"/> 
            </telerik:GridBoundColumn> 

            <telerik:GridBoundColumn HeaderText="TypeCode" DataField="TypeCode" Display="false" >
                <ItemStyle Width="0%" HorizontalAlign="Left"/> 
            </telerik:GridBoundColumn> 

            <telerik:GridTemplateColumn>
                <ItemStyle Width="60%" HorizontalAlign="Right"/> 
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("TellerTrans").ToString(),Eval("TypeCode").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" /> </a>  
                </itemtemplate>
            </telerik:GridTemplateColumn>

        </Columns>
    </MasterTableView>
</telerik:RadGrid>--%>
   