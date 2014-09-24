<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CashRepayment_Enquiry.ascx.cs" Inherits="BankProject.Views.TellerApplication.CashRepayment_Enquiry" %>
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
            <td class="MyLable">Cash Repayment ID:</td>
            <td class="MyContent" >
                <telerik:radtextbox  id="tbID" runat="server" validationGroup="Group1" ></telerik:radtextbox>
            </td>
            <td class="MyLable" >Customer Account ID:</td>
            <td class="MyContent" >
                <telerik:radtextbox runat="server" id="tbCustomerAcct" validationGroup="Group1"  ></telerik:radtextbox>
            </td>
            <td class="MyLable">Currency:</td>
            <td class="MyContent">
               <telerik:RadComboBox ID="rcbCurrency" runat="server" AllowCustomText="false" 
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
            <td class="MyLable">Deposited Amount:</td>
             <td class="MyContent" style="visibility:hidden;">
                <telerik:radTextBox id="RadTextBox2" runat="server" />
            </td>
            <td class="MyLable">From:</td>
            <td class="MyContent">
                <telerik:radnumericTextBox id="tbFromDepositedAmt"  runat="server" />
            </td>
            <td class="MyLable">To:</td>
            <td class="MyContent">
                <telerik:radnumericTextBox id="tbTODepositedAmt"  runat="server"  />
            </td>
        </tr>
        </table>
</div>
<div>
    <telerik:RadGrid id="RadGridView" runat="server" AllowPaging="true" AutoGenerateColumns="false" 
        OnNeedDataSource="RadGrid1_OnNeedDataSource" >
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn  HeaderText="ID" DataField="ID"  HeaderStyle-Width="70px"    HeaderStyle-HorizontalAlign="center"/>
                <telerik:GridBoundColumn  HeaderText="Customer Name" DataField="CustomerName" HeaderStyle-Width="190px"  />
                <telerik:GridBoundColumn  HeaderText="Customer Account ID" DataField="CustomerAccountID"   HeaderStyle-Width="150px"/>
                <telerik:GridBoundColumn  HeaderText="Cash Account ID" DataField="CashAccountID"  ItemStyle-HorizontalAlign="Right" 
                                           HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="190px"/>
                <telerik:GridBoundColumn  HeaderText="Balance Amount" DataField="BalanceAmount"  ItemStyle-HorizontalAlign="Right" 
                                           HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="150px"/>
                
                <telerik:GridBoundColumn  HeaderText="Currency" DataField="Currency"   HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="center" 
                                           HeaderStyle-HorizontalAlign="center"/>
                <telerik:GridBoundColumn  HeaderText="Amount Deposited" DataField="AmountDeposited"  ItemStyle-HorizontalAlign="Right" 
                                           HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="170px"/>
                <telerik:GridBoundColumn  HeaderText="Status" DataField="Status"  HeaderStyle-Width="50px"    HeaderStyle-HorizontalAlign="center"
                     ItemStyle-HorizontalAlign="center"/>
                <telerik:GridTemplateColumn>
                    <ItemStyle width="25" />
                    <ItemTemplate>
                        <a href='<%# getUrlPreview(Eval("ID").ToString()) %>'> <img src="Icons/bank/text_preview.png" width="20" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>