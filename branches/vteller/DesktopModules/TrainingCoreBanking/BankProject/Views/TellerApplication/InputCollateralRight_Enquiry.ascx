<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InputCollateralRight_Enquiry.ascx.cs" Inherits="BankProject.Views.TellerApplication.InputCollateralRight_Enquiry" %>
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
            <td class="MyLable">Parents Limit ID:</td>
            <td class="MyContent" width="300">
                <telerik:radtextbox  id="tbMaHanMucCha" runat="server" validationGroup="Group1" width="300"></telerik:radtextbox>
            </td>
            <td class="MyLable" >Child Limit ID:</td>
            <td class="MyContent" width="300">
                <telerik:radtextbox runat="server" id="tbMaHanMucCon" validationGroup="Group1" width="300"  ></telerik:radtextbox>
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
        <tr>
            <td class="MyLable">Right ID:</td>

            <td class="MyContent"><asp:TextBox ID="tbRightID" runat="server" width="300"></asp:TextBox></td>
        </tr>
        </table>
</div>
<div style="padding:10px;">
    <telerik:RadGrid id="RadGrid" runat="server" AllowPaging="true" AutoGenerateColumns="false" OnneedDatasource="RadGrid_OnneedDatasource">
        <MasterTableView>
            <columns>
                <telerik:GridBoundColumn HeaderText="Right ID"  DataField="RightID" />
                <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" />
                <telerik:GridBoundColumn HeaderText="SubLimit ID" DataField="SubLimitID" />
                <telerik:GridBoundColumn HeaderText="CollateralType Name" DataField="CollateralTypeName" />
                <telerik:GridBoundColumn HeaderText="Collateral Name" DataField="CollateralName" ItemStyle-horizontalAlign="left" />
                <telerik:GridBoundColumn HeaderText="Created Date" DataField="CreatedDate"  headerStyle-HorizontalAlign="center"
                    ItemStyle-HorizontalAlign="right" />
                <telerik:GridBoundColumn HeaderText="Collateral Name" DataField="CollateralName"  />
                <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Right" >
                    <ItemStyle Width="25" />
                    <Itemtemplate>
                       <a href='<%# GetUrlRightID(Eval("RightID").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
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