<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnquiryCustomer.ascx.cs" Inherits="BankProject.Views.CustomerManagement.EnquiryCustomer" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadWindowManager id="RadWindowManager1" runat="server"  EnableShadow="true" />
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
            <td class="MyLable" width="100"> Customer Type:</td>
            <td class="MyContent" width="300">
                <telerik:RadComboBox id="rcbCustomerType" runat="server" AllowcustomText="false" MarkFirstMatch="true" AppendDataBoundItems="True" width="150" 
                    OnSelectedIndexChanged="rcbCustomerType_OnSelectedIndexChanged" autoPostBack="true" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" /> 
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                        <telerik:RadComboBoxItem Value="P" Text="P - Person" />
                        <telerik:RadComboBoxItem Value="C" Text="C - Corporate" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="MyLable"  width="100"></td>
            <td class="MyContent"></td>
        </tr>
        <tr>
            <td class="MyLable">Customer ID:</td>
            <td class="MyContent">
                <asp:TextBox ID="tbCustomerID" runat ="server" ValidationGroup="Group1" width="300" />
            </td>
            <td class="MyLable">Cell Phone/Office Num:</td>
            <td class="MyContent">
                <telerik:RadMaskedTextBox ID="tbCellPhone" runat="server" Mask="###########" 
                    EmptyMessage="-- Enter Phone Number --" HideOnBlur="true" ZeroPadNumericRanges="true" DisplayMask="###########" />
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
            <td class="MyLable">Main Sector:</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbMainSector" runat="server" AllowcustomText="false" MarkFirstMatch="true" AppendDataBoundItems="True" 
                    OnSelectedIndexChanged="OnIndexSelectedIndexChanged_rcbMainSector_rcbSubSector" autoPostBack="true" width="300" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" /> 
                   
                </telerik:RadComboBox>
            </td>
            <td class="MyLable">SubSector:</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbSubSector" runat="server" AllowcustomText="false" MarkFirstMatch="true" AppendDataBoundItems="True" width="300" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" /> 
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="MyLable">Main Industry:</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbMainIndustry" runat="server" AllowcustomText="false" MarkFirstMatch="true" AppendDataBoundItems="True" 
                    OnSelectedIndexChanged="OnIndexSelectedIndexChanged_rcbMainIndustry_rcbSubIndustry" autoPostBack="true" width="300" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" /> 
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="MyLable">Sub Industry:</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbSubIndustry" runat="server" AllowcustomText="false" MarkFirstMatch="true" AppendDataBoundItems="True" width="300" >
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
<div style="padding:10px;">
    <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="true" AutoGenerateColumns="false"  OnNeedDataSource="RadGrid1_OnNeedDataSource">
        <MasterTableView>
            <Columns>
               <telerik:GridBoundColumn HeaderText="Customer ID" DataField="CustomerID" headerStyle-width="10%"/>
                <telerik:GridBoundColumn HeaderText="Customer Type" DataField="CustomerType" headerStyle-width="15%"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                <telerik:GridBoundColumn HeaderText="GB Full Name" DataField="GBFullName" headerStyle-width="25%"/>
                <telerik:GridBoundColumn HeaderText="Doc ID" DataField="DocID" headerStyle-width="20%" />
                <telerik:GridBoundColumn HeaderText="Cell Phone/Office Num" DataField="MobilePhone" headerStyle-width="15%"/>
                <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Right" >
                    <ItemStyle Width="25" />
                    <Itemtemplate>
                        <a href='<%#geturlReview(Eval("CustomerID").ToString(), Eval("Status").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                    </Itemtemplate>
                </telerik:GridTemplateColumn>
                </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>
<telerik:RadAjaxManager id="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting  AjaxControlID="rcbMainIndustry">
            <UpdatedControls>
                  <telerik:AjaxUpdatedControl ControlID="rcbSubIndustry" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting  AjaxControlID="rcbMainSector">
            <UpdatedControls>
                  <telerik:AjaxUpdatedControl ControlID="rcbSubSector" />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID ="rcbCustomerType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rcbMainSector" />
                <telerik:AjaxUpdatedControl ControlID="rcbSubSector" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>