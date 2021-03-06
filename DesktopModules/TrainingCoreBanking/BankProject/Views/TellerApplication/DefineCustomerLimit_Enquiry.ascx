﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefineCustomerLimit_Enquiry.ascx.cs" Inherits="BankProject.Views.TellerApplication.DefineCustomerLimit_Enquiry" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadWindowManager id="RadWindowManager1" runat="server"  EnableShadow="true" />
<telerik:RadToolBar runat="server" ID="RadToolBar2" EnableRoundedCorners="true" EnableShadows="true" width="100%" onButtonClick="RadToolBar2_OnButtonClick">
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
            <td class="MyLable">Limit Type:</td>
            <td class="MyContent">
                <telerik:RadCombobox id="rcbLimitTYpe" runat="server" MarkFirstMatch="true" AlllowCustomtext="false" AppendDataboundItems="true" ForeColor="Black" 
                   >
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                                <Items>                     
                                        <telerik:RadComboBoxItem Value="Global" Text="Global Limit" />
                                        <telerik:RadComboBoxItem Value="Product" Text="Product Limit" />
                                </Items>
                </telerik:RadCombobox>
            </td>
            <td class="MyLable"></td>
            <td class="MyContent"></td>
        </tr>
        <tr>
            <td class="MyLable">Global Limit ID:</td>
            <td class="MyContent" width="300">
                <telerik:RadCombobox id="rcbGlobalLimit" runat="server" MarkFirstMatch="true" AlllowCustomtext="false" AppendDataboundItems="true" ForeColor="Black" 
                   >
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                                <Items>                     
                                        <telerik:RadComboBoxItem Value="" Text="" />
                                        <telerik:RadComboBoxItem Value="7000" Text="7000 - Global Revoling Limit" />
                                        <telerik:RadComboBoxItem Value="8000" Text="8000 - Global Non-Revoling Limit" />
                                </Items>
                </telerik:RadCombobox>
            </td>
            <td class="MyLable" >Product Limit ID:</td>
            <td class="MyContent" width="300">
                <telerik:RadCombobox id="rcbProductLimitID" runat="server" MarkFirstMatch="true" AlllowCustomtext="false" AppendDataboundItems="true" ForeColor="Black" 
                   >
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                                <Items>                     
                                        <telerik:RadComboBoxItem Value="" Text="" />
                                        <telerik:RadComboBoxItem Value="7700" Text="7700 - Revoling Limit" />
                                        <telerik:RadComboBoxItem Value="8700" Text="8700 - Non-Revoling Limit" />
                                </Items>
                </telerik:RadCombobox>
            </td>
        </tr>
        <tr>
            <td class="MyLable">Customer Name:</td>
            <td class="MyContent" width="300">
                <telerik:radtextbox id="tbFullName" runat="server" validationGroup="Group1" width="300"></telerik:radtextbox>
            </td>
            <td class="MyLable">Customer ID:</td>
            <td class="MyContent" width="300">
                <telerik:radtextbox id="tbCustomerID" runat="server" validationGroup="Group1" width="300"></telerik:radtextbox>
            </td>
            </tr>
        
        

            <tr>
            <td class="MyLable">Currency:</td>
            <td class="MyContent" width="300">
                <telerik:RadComboBox id="rcbCurrency" runat="server" appendDataboundItems="true" AllowCustomText="false" MarkFirdMatch="true" 
                    width="300" height="150">
                    <Items>
                        <telerik:RadComboboxItem text="" value="" />
                    </Items>
                    </telerik:RadComboBox>
            </td>
            <td class="MyLable">Global Limit Amount:</td>
            <td class="MyContent" >From <telerik:radnumerictextbox id="tbFromIntLimitAmt" runat="server" ValidationGroup="Group1" width="120"  />
                 To <telerik:radnumerictextbox id="tbToIntLimitAmt" runat="server" ValidationGroup="Group1" width="129" />
            </td>
        </tr>
        <tr style="visibility:hidden;">
            <td class="MyLable">Collateral Type:</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbCollateralType" runat="server" appendDataboundItems="true" AllowCustomText="false" MarkFirdMatch="true"
                    width="300" height="150" autoPostBack="true" ONSelectedIndexChanged="rcbCollateralType_ONSelectedIndexChanged">
                    <Items>
                        <telerik:RadComboboxItem text="" value="" />
                    </Items>
                    </telerik:RadComboBox>
            </td>
            <td class="MyLable">Collateral Code:</td>
            <td class="MyContent" >
                <telerik:RadComboBox id="rcbCollateral" runat="server" appendDataboundItems="true" AllowCustomText="false" MarkFirdMatch="true"
                    width="300" height="150" >
                    <Items>
                        <telerik:RadComboboxItem text="" value="" />
                    </Items>
                    </telerik:RadComboBox>
            </td>
        </tr>
        </table>
</div>
<div style="padding:10px;">
    <telerik:RadGrid id="RadGrid" runat="server" AllowPaging="true" AutoGenerateColumns="false" OnNeedDataSource="RadGrid_OnNeedDataSource">
        <MasterTableView>
            <columns>
                <telerik:GridBoundColumn HeaderText="Global Limit" DataField="MainLimitID" />
                <telerik:GridBoundColumn HeaderText="Product Limit" DataField="SubLimitID" />
                <telerik:GridBoundColumn HeaderText="Product" DataField="Product" />
                <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" />
                <telerik:GridBoundColumn HeaderText="Currency" DataField="Currency" ItemStyle-horizontalAlign="center"  HeaderStyle-horizontalAlign="center" />
                <telerik:GridBoundColumn HeaderText="Global Limit Amt" DataField="InternalLimitAmt"  headerStyle-HorizontalAlign="right"
                    ItemStyle-HorizontalAlign="right" />
                 <telerik:GridBoundColumn HeaderText="Product Limit Amt" DataField="MaxTotal"  headerStyle-HorizontalAlign="right"
                    ItemStyle-HorizontalAlign="right" />
                <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Right" >
                    <ItemStyle Width="25" />
                    <Itemtemplate>
                        <a href='<%# geturlReview(Eval("MainLimitID").ToString(),Eval("SubLimitID").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                    </Itemtemplate>
                </telerik:GridTemplateColumn>
            </columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbCollateralType">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="rcbCollateral" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>