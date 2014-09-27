<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollectionForCreditCard_Enquiry.ascx.cs" Inherits="BankProject.Views.TellerApplication.CollectionForCreditCard_Enquiry" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>
<%@ register Assembly="Telerik.Web.Ui" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadWindowManager id="RadWindowManager2" runat="server"  EnableShadow="true" />
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
            <td class="MyLable">Payment Type:</td>
            <td class="MyContent">
                <telerik:RadComboBox ID="rcbPaymentType" runat="server" AllowCustomText="false" 
                             MarkFirstMatch="true" ValidationGroup="Group1"
                             AppendDataBoundItems="true" width="270" >
                    <Items>
                        <telerik:RadComboBoxItem Value="Collection" Text="Collection for Credit Card Payment" />
                        <telerik:RadComboBoxItem Value="Transfer" Text="Transfer for Credit Card Payment" />
                    </Items>
                        </telerik:RadComboBox>
            </td>
        </tr>
        </table>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="MyLable">Reference ID:</td>
            <td class="MyContent" >
                <telerik:radtextbox  id="tbID" runat="server" validationGroup="Group1" ></telerik:radtextbox>
            </td>
            <td class="MyLable" >Debit Account ID:</td>
            <td class="MyContent" >
                <telerik:radtextbox runat="server" id="tbDebitAcct" validationGroup="Group1"  ></telerik:radtextbox>
            </td>
            <td class="MyLable">Debit Currency:</td>
            <td class="MyContent">
               <telerik:RadComboBox ID="rcbDebitCurrency" runat="server" AllowCustomText="false" 
                             MarkFirstMatch="true" ValidationGroup="Group1"
                             AppendDataBoundItems="true" >
                        </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="MyLable">Customer ID:</td>
            <td class="MyContent">
                <telerik:radtextbox runat="server" id="tbCustomerID" ValidationGroup="Group1" />
            </td>
            <td class="MyLable">Customer Name:</td>
             <td class="MyContent">   <telerik:radtextbox runat="server" id="tbCustomerName" ValidationGroup="Group1" />
            </td>
            <td class="MyLable">Legal ID:</td>
            <td class="MyContent">
                 <telerik:radTextBox id="tbLegalID" runat="server" />
            </td>
        </tr></table>
        
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
            <td class="MyLable">Debit Amount:</td>
             <td class="MyContent" style="visibility:hidden;">
                <telerik:radTextBox id="RadTextBox2" runat="server" />
            </td>
            <td class="MyLable">From:</td>
            <td class="MyContent">
                <telerik:radnumericTextBox id="tbFromAmt"  runat="server" />
            </td>
            <td class="MyLable">To:</td>
            <td class="MyContent">
                <telerik:radnumericTextBox id="tbToAmt"  runat="server"  />
            </td>
        </tr>
        </table>
</div>
<div>
    <telerik:RadGrid id="RadGridView" runat="server" AllowPaging="true" AutoGenerateColumns="false" 
        OnNeedDataSource="RadGrid1_OnNeedDataSource" >
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Reference No" DataField="ID" HeaderStyle-Width ="100" />
            <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" HeaderStyle-Width ="150"/>
            <telerik:GridBoundColumn HeaderText="Debit Account" DataField="DebitAccount" HeaderStyle-Width ="150"/>
            <telerik:GridBoundColumn HeaderText="Debit Amount" DataField="DebitAmt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width ="100" />
            <telerik:GridBoundColumn HeaderText="Debit Currency" DataField="DebitCurrency" HeaderStyle-Width ="100"
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>

            <telerik:GridBoundColumn HeaderText="Credit Amount" DataField="CreditAmt" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width ="100" />
            <telerik:GridBoundColumn HeaderText="Credit Currency" DataField="CreditCurrency" HeaderStyle-Width ="100"
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>

            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" HeaderStyle-Width ="50" />
                <telerik:GridTemplateColumn>
                    <ItemStyle width="25" />
                    <ItemTemplate>
                        <a href='<%# getUrlPreview(Eval("ID").ToString(), Eval("Type").ToString()) %>'> <img src="Icons/bank/text_preview.png" width="20" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>