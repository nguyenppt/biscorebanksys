<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnquiryAccountTransaction.ascx.cs" Inherits="BankProject.Views.TellerApplication.EnquiryAccountTransaction" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadToolBar runat="server" ID="RadToolBar2" EnableRoundedCorners="true" EnableShadows="true" width="100%" OnButtonClick="radtoolbar2_onbuttonclick" >
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
                ToolTip="Commit Data" Value="btCommit" CommandName="commit">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
                ToolTip="Preview" Value="btReview" CommandName="review">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
                ToolTip="Authorize" Value="btAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
                ToolTip="Revert" Value="btRevert" CommandName="revert">
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
     <table width="100%" cellpadding="0" cellspacing="0">
         <tr>
    <td class="MyLable">Transaction Type</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbTransactionType" runat="server" AllowcustomText="false" MarkFirstMatch="true" AppendDataBoundItems="True" width="170" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" /> 
                    <Items>
                        <telerik:RadComboBoxItem Value="0" Text="Cash Deposit" />
                        <telerik:RadComboBoxItem Value="1" Text="Cash Withdrawal" />
                        <telerik:RadComboBoxItem Value="2" Text="Transfer Withdrawal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
      <td class="MyLable">Account Type</td>
            <td class="MyContent">
                
                <telerik:RadComboBox id="rcbAccountType" runat="server" AllowcustomText="false" MarkFirstMatch="true" AppendDataBoundItems="True" width="200" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" /> 
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                        <telerik:RadComboBoxItem Value="1" Text="Non term Saving Account" />
                        <telerik:RadComboBoxItem Value="2" Text="Saving Account - Arrear" />
                        <telerik:RadComboBoxItem Value="3" Text="Saving Account - Periodic" />
                        <telerik:RadComboBoxItem Value="4" Text="Saving Account - Discounted" />
                    </Items>
                </telerik:RadComboBox>
            </td>
             </tr>
         <tr>
            <td class="MyLable">Ref ID:</td>
            <td class="MyContent">
                <asp:TextBox ID="txtRefID" width="170" runat ="server" ValidationGroup="Group1" />
                </td>
            <td class="MyLable">Currency:</td>
            <td class="MyContent">
               <telerik:RadComboBox id="rcbCurrency" runat="server" width="80" AllowcustomText="false" MarkFirstMatch="true" AppendDataBoundItems="True"  >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" /> 
                     <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="USD" Text="USD" />
                            <telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                            <telerik:RadComboBoxItem Value="GBP" Text="GBP" />
                            <telerik:RadComboBoxItem Value="JPY" Text="JPY" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND" />
                        </Items>
                </telerik:RadComboBox>
           
           </td>
        </tr>
        <tr>
            <td class="MyLable" width="100"> Customer Type:</td>
            <td class="MyContent" >
                <telerik:RadComboBox id="rcbCustomerType" runat="server" AllowcustomText="false" MarkFirstMatch="true" AppendDataBoundItems="True" width="170" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" /> 
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                        <telerik:RadComboBoxItem Value="P" Text="P - Person" />
                        <telerik:RadComboBoxItem Value="C" Text="C - Corporate" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="MyLable">Customer ID:</td>
            <td class="MyContent">
                <asp:TextBox ID="tbCustomerID" runat ="server" ValidationGroup="Group1" width="300" />
            </td>
        </tr>
        <tr>
            <td class="MyLable">GB Full Name:</td>
            <td class="MyContent">
                <asp:TextBox ID="tbGBFullName" runat ="server" ValidationGroup="Group1" width="300" />
            </td>
            <td class="MyLable">Customer Account:</td>
            <td class="MyContent">
                <asp:TextBox ID="tbCustomerAccount" runat ="server" ValidationGroup="Group1" width="300" />
            </td>
        </tr>
        <tr>
            <td class="MyLable">Amount:</td>
            <td class="MyContent">
                <telerik:RadNumericTextbox id ="txtFrom" runat="server" width="135" >
                        <NumberFormat DecimalDigits="2" />
                </telerik:RadNumericTextbox>
                &nbsp;To&nbsp;
                <telerik:RadNumericTextbox id ="txtTo" width="135" runat="server" >
                        <NumberFormat DecimalDigits="2" />
                </telerik:RadNumericTextbox>
            </td>
            <td class="MyLable">Date:</td>
            <td class="MyContent">
                <telerik:RadDatePicker id="txtDate" runat="server"></telerik:RadDatePicker>
            </td>
        </tr>
    </table>
</div>

<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Account Type" HeaderStyle-Width = "20%" DataField="AccountTypeName" />
            <telerik:GridBoundColumn HeaderText="Ref ID" HeaderStyle-Width = "15%" DataField="Code" />
            <telerik:GridBoundColumn HeaderText="Account Code" HeaderStyle-Width = "15%" DataField="AccountCode" />
            <telerik:GridBoundColumn HeaderText="Customer Name" HeaderStyle-Width = "30%" DataField="CustomerName" />
            <telerik:GridBoundColumn HeaderText="Currency" HeaderStyle-Width = "5%" DataField="Currency" />
            <telerik:GridBoundColumn HeaderText="Transaction Amount" DataField="TransactionAmount" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width = "15%"  ItemStyle-HorizontalAlign="Right" DataType="System.Decimal"  DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Status" HeaderStyle-Width = "5%" DataField="Status" />

            <telerik:GridTemplateColumn>
                   <ItemStyle width="25" />
                   <ItemTemplate>
                       <a href='<%# getUrlPreview(Eval("Code").ToString(), Eval("get_for").ToString()) %>'><img src="Icons/bank/text_preview.png" width="20" /></a>
                   </ItemTemplate>
               </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>

   <telerik:RadAjaxManager id="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting  AjaxControlID="rcbcategory">
            <UpdatedControls>
                  <telerik:AjaxUpdatedControl ControlID="rcbProductLine" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>