<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InwardEnquiry.ascx.cs" Inherits="BankProject.FTTeller.InwardEnquiry" %>
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
            <td class="MyLable">Transaction Type:</td>
            <td class="MyContent">
                <telerik:RadComboBox ID="rcbTransactionType" runat="server" AllowCustomText="false" 
                             MarkFirstMatch="true" ValidationGroup="Group1"
                             AppendDataBoundItems="true" width="270" >
                    <Items>
                       <telerik:RadComboBoxItem Value="CashWithdraw" Text="Cash Withdraw" />
                       <telerik:RadComboBoxItem Value="CreditAccount" Text="Credit Account" />
                    </Items>
                        </telerik:RadComboBox>
            </td>
        </tr>
        </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="MyLable">BO Name:</td>
             <td class="MyContent" width="320">   <telerik:radtextbox runat="server" id="tbBOName" ValidationGroup="Group1"  width="270"/>
            </td>
            <td class="MyLable">FO Legal ID:</td>
            <td class="MyContent">
                 <telerik:radTextBox id="tbLegalID" runat="server"  width="340" />
            </td>
        </tr></table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="MyLable">FO Name:</td> 
            <td class="MyContent">
                <telerik:RadTextBox id="tbFOName" runat="server"/>
            </td>
            <td class="MyLable">Reference ID:</td>
            <td class="MyContent" >
                <telerik:radtextbox  id="tbID" runat="server" validationGroup="Group1" ></telerik:radtextbox>
            </td>
            
            <td class="MyLable">Credit Currency:</td>
            <td class="MyContent">
               <telerik:RadComboBox ID="rcbCurrency" runat="server" AllowCustomText="false" 
                             MarkFirstMatch="true" ValidationGroup="Group1"
                             AppendDataBoundItems="true" >
                        </telerik:RadComboBox>
            </td>
        </tr>
    </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
            <td class="MyLable">Credit Amount:</td>
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
<div style="padding:10px;">
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Reference No" DataField="ID" HeaderStyle-Width="90" />
            <telerik:GridBoundColumn HeaderText="BO Name" DataField="BOName" HeaderStyle-Width="200" />
            <telerik:GridBoundColumn HeaderText="FO Name" DataField="FOName" HeaderStyle-Width="200" />
            <telerik:GridBoundColumn HeaderText="Credit Currency" DataField="CreditCurrency" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center"/>
            <telerik:GridBoundColumn HeaderText="Credit Amount LCY" DataField="CreditAmtLCY" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="150"
                HeaderStyle-HorizontalAlign="Right" datatype="System.Decimal" DataFormatString="{0:N}"/>
            <telerik:GridBoundColumn HeaderText="Credit Amount FCY" DataField="CreditAmtFCY" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="150"
                HeaderStyle-HorizontalAlign="Right" datatype="System.Decimal" DataFormatString="{0:N}"/>
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" HeaderStyle-Width="90"
                ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("ID").ToString(), Eval("Status").ToString(), Eval("tu").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>