<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChequeTransfer_Enquiry.ascx.cs" Inherits="BankProject.Views.TellerApplication.ChequeTransfer_Enquiry" %>
<%@ register Assembly="Telerik.Web.Ui" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadWindowManager id="RadWindowManager1" runat="server"  EnableShadow="true" />
<telerik:RadToolBar runat="server" ID="RadToolBar2" EnableRoundedCorners="true" EnableShadows="true" width="100%" OnbuttonClick="radtoolbar2_onbuttonclick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
                ToolTip="Commit Data" Value="btCommit" CommandName="commit">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
                ToolTip="Preview" Value="btPreview" CommandName="review">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
                ToolTip="Authorize" Value="btAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
                ToolTip="Revert" Value="btReverse" CommandName="revert">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
                ToolTip="Search" Value="btSearch" CommandName="search">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
<div style="padding:10px;">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="MyLable">Transfer ID:</td>
            <td class="MyContent" >
                <telerik:radtextbox  id="tbTransferID" runat="server" validationGroup="Group1" ></telerik:radtextbox>
            </td>
            <td class="MyLable" >Working Account:</td>
            <td class="MyContent" >
                <telerik:radtextbox runat="server" id="tbWorkingAcct" validationGroup="Group1"  ></telerik:radtextbox>
            </td>
            <td class="MyLable">Cheque No:</td>
            <td class="MyContent">
                <telerik:radnumerictextbox runat="server" id="tbChequeNo" ValidationGroup="Group1" Numberformat-decimalDigits="0" Numberformat-Groupseparator="" />
            </td>
        </tr>
        <tr>
            <td class="MyLable">CustomerID:</td>
            <td class="MyContent">
                <telerik:radtextbox runat="server" id="tbCustomerID" ValidationGroup="Group1" />
            </td>
            <td class="MyLable">Customer Name:</td>
             <td class="MyContent">   <telerik:radtextbox runat="server" id="tbCustomerName" ValidationGroup="Group1" />
            </td>
            <td class="MyLable">Transfer Date:</td>
            <td class="MyContent">
                <telerik:radDatePicker id="rdpTransferDate" runat="server" />
            </td>
        </tr></table>
        <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="MyLable">Cheque Type</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbChequeType" Allowcustomtext="false" MarkFirstMatch="true" AppendDataBoundItems="true" 
                     runat="server">
                   <Items>
                         <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                    </telerik:RadComboBox> 
            </td>
            <td class="MyLable">Legal ID:</td>
            <td class="MyContent">
                <telerik:radTextBox id="tbLegalID" runat="server" />
            </td>
            <td class="MyLable" style="visibility:hidden;">asdf asf asd</td>
            <td class="MyContent" style="visibility:hidden;">
                <telerik:radTextBox id="RadTextBox1" runat="server" />
            </td>
             
            
            </tr>
    </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
            <td class="MyLable">Transfer Amount:</td>
             <td class="MyContent" style="visibility:hidden;">
                <telerik:radTextBox id="RadTextBox2" runat="server" />
            </td>
            <td class="MyLable">From:</td>
            <td class="MyContent">
                <telerik:radnumericTextBox id="tbFromAmountLCY"  runat="server" />
            </td>
            <td class="MyLable">To:</td>
            <td class="MyContent">
                <telerik:radnumericTextBox id="tbToAmountLCY"  runat="server"  />
            </td>
        </tr>
        </table>
</div>
<div style="padding:10px;">
    <telerik:RadGrid id="RadGridView" runat="server" AllowPaging="true" AutoGenerateColumns="false" 
        OnNeedDataSource="RadGrid1_OnNeedDataSource" >
        <MasterTableView>
            <columns>
                <telerik:GridBoundColumn  HeaderText="Reference ID"  DataField="ID" />
                <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" HeaderStyle-Width="100"  />
                 <telerik:GridBoundColumn HeaderText="Amount Debit" DataField="DebitAmount" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" 
                     HeaderStyle-Width="150"  />
                 <telerik:GridBoundColumn HeaderText="Debit Currency" DataField="DebitCurrency" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center" />
                 <telerik:GridBoundColumn HeaderText="Cheque No" DataField="ChequeNo" HeaderStyle-HorizontalAlign="center" />
                <telerik:GridBoundColumn HeaderText="Beneficiary Name" DataField="BeneficialName" HeaderStyle-Width="120"  />
                <telerik:GridBoundColumn HeaderText="Amount Transfer" DataField="AmtCreditForCust" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" 
                     HeaderStyle-Width="150"  />
                 <telerik:GridBoundColumn HeaderText="Credit Currency" DataField="CreditCurrency" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center" />
                <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
                <telerik:GridBoundColumn HeaderText="Transfer Date" DataField="CreatedDate" HeaderStyle-HorizontalAlign="center"  />
                <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Right" >
                    <ItemStyle Width="25" />
                    <Itemtemplate>
                        <a href='<%# geturlReview(Eval("ID").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                    </Itemtemplate>
                </telerik:GridTemplateColumn>
            </columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>