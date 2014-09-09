<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewNormalLoanEnquiry.ascx.cs" Inherits="BankProject.Views.TellerApplication.NewNormalLoanEnquiry" %>
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
            <td class="MyLable">Ref code:</td>
            <td class="MyContent">
                <asp:TextBox ID="txtRefCode" width="120" runat ="server" ValidationGroup="Group1" />
               
            </td>
               
            <td class="MyLable">Currency</td>
            <td class="MyContent">
                 <telerik:RadComboBox id="rcbCurrency" runat="server" width="110" AllowcustomText="false" MarkFirstMatch="true" AppendDataBoundItems="True"  >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" /> 
                     <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="USD" Text="USD" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND" />
                        </Items>
                </telerik:RadComboBox>
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
            <td class="MyLable">Main Category:</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbcategory" runat="server" AllowcustomText="false" MarkFirstMatch="true" autopostback="true"
                    onselectedindexchanged="cmbCategory_onselectedindexchanged"
                     AppendDataBoundItems="True" width="300" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" /> 
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="MyLable">Sub Category:</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbSubCategory" runat="server" AllowcustomText="false" MarkFirstMatch="true" AppendDataBoundItems="True" width="300" >
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
            <telerik:GridBoundColumn HeaderText="Ref Code" HeaderStyle-Width = "10%" DataField="Code" />
            <telerik:GridBoundColumn HeaderText="Customer Name" HeaderStyle-Width = "20%" DataField="CustomerName" />
            <telerik:GridBoundColumn HeaderText="Doc Id" HeaderStyle-Width = "5%" DataField="DocID" />
            <telerik:GridBoundColumn HeaderText="Main Category" HeaderStyle-Width = "15%" DataField="MainCategoryName" />
            <telerik:GridBoundColumn HeaderText="Sub Category" HeaderStyle-Width = "15%" DataField="SubCategoryName" />
            <telerik:GridBoundColumn HeaderText="Currency" HeaderStyle-Width = "5%" DataField="Currency" />
            <telerik:GridBoundColumn HeaderText="Loan Amount" DataField="LoanAmount" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width = "15%"  ItemStyle-HorizontalAlign="Right" DataType="System.Decimal"  DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Approve Amount" DataField="ApproveAmount" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width = "15%"  ItemStyle-HorizontalAlign="Right" DataType="System.Decimal" DataFormatString="{0:N}" />
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