<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Index_ListReview.ascx.cs" Inherits="BankProject.Views.CustomerManagement.OpenInvidualCustomer.Index_ListReview" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div style="padding: 10px;">
    <telerik:radgrid runat="server" autogeneratecolumns="False"
         id="RadGridAccountReviewList" allowpaging="false" onneeddatasource="RadGridAccountReviewList_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Customer ID" DataField="CustomerID" />
                <telerik:GridBoundColumn HeaderText="GB Short Name" DataField="GBShortName" />
                <telerik:GridBoundColumn HeaderText="City/Province" DataField="TenTinhThanh" />
                <telerik:GridBoundColumn HeaderText="Nationality" DataField="NationalityName" />
                <telerik:GridBoundColumn HeaderText="Main Industry" DataField="IndustryName" />
                <telerik:GridBoundColumn HeaderText="Doc ID" DataField="DocID" />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# GeturlReview(Eval("CustomerID").ToString()) %>'>
                            <img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> 
                        </a> 
                    </itemtemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:radgrid>
</div>
