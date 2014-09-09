<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnquiryAccount.ascx.cs" Inherits="BankProject.Views.TellerApplication.EnquiryAccount" %>
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
            <td class="MyLable">Account code:</td>
            <td class="MyContent">
                <asp:TextBox ID="txtAccountCode" width="120" runat ="server" ValidationGroup="Group1" />
                &nbsp;
                Currency:
                <telerik:RadComboBox id="rcbCurrency" runat="server" width="110" AllowcustomText="false" MarkFirstMatch="true" AppendDataBoundItems="True"  >
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
               
            <td class="MyLable">Blocked</td>
            <td class="MyContent">
                <asp:CheckBox ID="ckLocked" runat="server" />
                &nbsp;
                Close:
                <asp:CheckBox ID="ckClose" runat="server" />
            </td>
           
        </tr>
        <tr>
            <td class="MyLable" width="100"> Customer Type:</td>
            <td class="MyContent" width="300">
                <telerik:RadComboBox id="rcbCustomerType" runat="server" AllowcustomText="false" MarkFirstMatch="true" AppendDataBoundItems="True" width="120" >
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
            <td class="MyLable">Doc ID:</td>
            <td class="MyContent">
                <asp:TextBox ID="tbDocID" runat ="server" ValidationGroup="Group1" width="300" />
            </td>
        </tr>
        <tr>
            <td class="MyLable">category:</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbcategory" runat="server" AllowcustomText="false" MarkFirstMatch="true" autopostback="true"
                    onselectedindexchanged="cmbCategory_onselectedindexchanged"
                        onitemdatabound="cmbCategory_onitemdatabound"
                     AppendDataBoundItems="True" width="300" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" /> 
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="MyLable">Product Line:</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbProductLine" runat="server" AllowcustomText="false" MarkFirstMatch="true" AppendDataBoundItems="True" width="300" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" /> 
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
            </td>
        </tr>
    </table>
</div>
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Account Code" HeaderStyle-Width = "5%" DataField="AccountCode" />
            <telerik:GridBoundColumn HeaderText="Customer Name" HeaderStyle-Width = "20%" DataField="CustomerName" />
            <telerik:GridBoundColumn HeaderText="Doc Id" HeaderStyle-Width = "5%" DataField="DocID" />
            <telerik:GridBoundColumn HeaderText="Category" HeaderStyle-Width = "10%" DataField="CategoryName" />
            <telerik:GridBoundColumn HeaderText="Product Line" HeaderStyle-Width = "15%" DataField="ProductLineName" />
            <telerik:GridBoundColumn HeaderText="Currency" HeaderStyle-Width = "2%" DataField="Currency" />
            <telerik:GridBoundColumn HeaderText="Actual Ballance" DataField="ActualBallance" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width = "10%"  ItemStyle-HorizontalAlign="Right" DataType="System.Decimal"  DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Cleared Ballance" DataField="ClearedBallance" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width = "11%"  ItemStyle-HorizontalAlign="Right" DataType="System.Decimal" DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Working Amount" DataField="WorkingAmount" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width = "11%"  ItemStyle-HorizontalAlign="Right" DataType="System.Decimal" DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Locked Amount" DataField="LockedAmount" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width = "11%"  ItemStyle-HorizontalAlign="Right" DataType="System.Decimal" DataFormatString="{0:N}" />
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