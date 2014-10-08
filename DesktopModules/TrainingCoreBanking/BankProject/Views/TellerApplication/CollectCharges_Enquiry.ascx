<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollectCharges_Enquiry.ascx.cs" Inherits="BankProject.Views.TellerApplication.CollectCharges_Enquiry" %>
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
     <table cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td class="MyLable">Collection Type:</td>
            <td class="MyContent">
                <telerik:RadComboBox ID="rcbCollectionType" runat="server" AllowCustomText="false" 
                             MarkFirstMatch="true" ValidationGroup="Group1"
                             AppendDataBoundItems="true" width="270" >
                    <Items>
                        <telerik:RadComboBoxItem Value="Account" Text="Collect Charges From Account" />
                        <telerik:RadComboBoxItem Value="Cash" Text="Collect Charges By Cash" />
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
            <td class="MyLable" >Account Type:</td>
            <td class="MyContent" >
               <telerik:RadComboBox ID="rcbAccountType" runat="server" AllowCustomText="false" 
                             MarkFirstMatch="true" ValidationGroup="Group1"
                             AppendDataBoundItems="true" width="270" >
                    <Items>
                       <telerik:RadComboBoxItem Value="1" Text="Non Term Saving Account" />
                        <telerik:RadComboBoxItem Value="2" Text="Saving Account - Arrear" />
                        <telerik:RadComboBoxItem Value="3" Text="Saving Account - Periodic" />
                        <telerik:RadComboBoxItem Value="4" Text="Saving Account - Discounted" />
                        <telerik:RadComboBoxItem Value="5" Text="Loan Working Account" />
                    </Items>
                        </telerik:RadComboBox>
            </td>
            <td class="MyLable">Account ID:</td>
            <td class="MyContent">
                     <telerik:radtextbox  id="tbAccountID" runat="server" validationGroup="Group1" ></telerik:radtextbox>
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
        </tr>
         <tr>
            <td class="MyLable">Charges Amount:</td>
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
                <telerik:GridBoundColumn HeaderText="Code" HeaderStyle-Width = "100" DataField="Code" />
            <telerik:GridBoundColumn HeaderText="Account Type" HeaderStyle-Width = "150" DataField="AccountTypeName" />
            <telerik:GridBoundColumn HeaderText="Account" HeaderStyle-Width = "100" DataField="CustomerAccount" />
            <telerik:GridBoundColumn HeaderText="Category" HeaderStyle-Width = "190" DataField="CategoryPLName" />
            <telerik:GridBoundColumn HeaderText="Charge Amount" DataField="ChargAmountLCY" HeaderStyle-HorizontalAlign="right"
                 HeaderStyle-Width = "20%"  ItemStyle-HorizontalAlign="Right" DataType="System.Decimal"  DataFormatString="{0:N}" />

            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" HeaderStyle-Width ="50"  HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
                <telerik:GridTemplateColumn>
                    <ItemStyle width="25" />
                    <ItemTemplate>
                        <a href='<%# getUrlPreview(Eval("Code").ToString()) %>'> <img src="Icons/bank/text_preview.png" width="20" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>