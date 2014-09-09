<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisingReviewList.ascx.cs" Inherits="BankProject.AdvisingReviewList" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Reperence No" DataField="ReperenceNo" />
            <telerik:GridBoundColumn HeaderText="LC Type" DataField="LCType" />
            <telerik:GridBoundColumn HeaderText="LC Number" DataField="LCNumber" />
            <telerik:GridBoundColumn HeaderText="Beneficiary Cust.No" DataField="BeneficiaryCustName" />
            <telerik:GridBoundColumn HeaderText="Issuing Bank No" DataField="IssuingBankNo" />
            <telerik:GridBoundColumn HeaderText="Reimb. Bank No." DataField="ReimbBankNo" />
            <telerik:GridBoundColumn HeaderText="Amount" DataField="Amount" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("AdvisingLCCode").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>