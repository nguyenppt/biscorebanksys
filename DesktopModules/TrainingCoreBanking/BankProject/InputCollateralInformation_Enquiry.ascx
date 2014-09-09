<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InputCollateralInformation_Enquiry.ascx.cs" Inherits="BankProject.InputCollateralInformation_Enquiry" %>
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
            <td class="MyLable">Right ID:</td>
            <td class="MyContent" width="300">
                <telerik:radtextbox  id="tbRightID" runat="server" validationGroup="Group1" width="300"></telerik:radtextbox>
            </td>
            <td class="MyLable" >Collateral Info ID:</td>
            <td class="MyContent" width="300">
                <telerik:radtextbox runat="server" id="tbCollInfoID" validationGroup="Group1" width="300"  ></telerik:radtextbox>
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
            <td class="MyLable">Currency:</td>
            <td class="MyContent" width="300">
                <telerik:RadComboBox id="rcbCurrency" runat="server" appendDataboundItems="true" AllowCustomText="false" MarkFirdMatch="true" 
                    width="300" height="150">
                    <Items>
                        <telerik:RadComboboxItem text="" value="" />
                    </Items>
                    </telerik:RadComboBox>
            </td>
            <td class="MyLable">Nominal Value:</td>
            <td class="MyContent" >From <telerik:radnumerictextbox id="tbFromNominalValue" runat="server" ValidationGroup="Group1" width="120"  />
                 To <telerik:radnumerictextbox id="tbToNominalValue" runat="server" ValidationGroup="Group1" width="129" />
            </td>
        </tr>
        <tr>
            <td class="MyLable">Contingent Account ID</td>
            <td class="MyContent" >
                <asp:TextBox ID="tbContingentAccID" runat="server" width="300"/>
            </td>
            <td class="MyLable"></td>
            <td class="MyContent"></td>
        </tr>
        </table>
</div>
<div style="padding:10px;">
    <telerik:RadGrid id="RadGrid" runat="server" AllowPaging="true" AutoGenerateColumns="false" OnNeedDataSource="RadGrid_OnNeedDataSource">
        <MasterTableView>
            <columns>
                <telerik:GridBoundColumn HeaderText="CollateralInfo ID" DataField="CollateralInfoID" />
                <telerik:GridBoundColumn HeaderText="Collateral Name" DataField="CollateralName" />
                <telerik:GridBoundColumn HeaderText="Contingent Acct" DataField="ContingentAcctID" ItemStyle-horizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="Status" DataField="CollateralStatusDesc" ItemStyle-horizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="Customer" DataField="CustomreIDName" ItemStyle-horizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="Currency" DataField="Currency" ItemStyle-horizontalAlign="center" />
                <telerik:GridBoundColumn HeaderText="Nominal Amount" DataField="NominalValue"  headerStyle-HorizontalAlign="center"
                    ItemStyle-HorizontalAlign="right" />
                <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Right" >
                    <ItemStyle Width="25" />
                    <Itemtemplate>
                        <a href='<%# geturlReview(Eval("CollateralInfoID").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
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