<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollateralContingentEntry_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.CollateralContingentEntry_PL" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
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
            <td class="MyLable">Contingent ID:</td>
            <td class="MyContent" width="300">
                <telerik:radtextbox  id="tbContingentID" runat="server" validationGroup="Group1" width="300"></telerik:radtextbox>
            </td>
            <td class="MyLable" >Reference ID:</td>
            <td class="MyContent" width="300">
                <telerik:radtextbox runat="server" id="tbRefID" validationGroup="Group1" width="300"  ></telerik:radtextbox>
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
            <td class="MyLable">Nominal Value:</td>
            <td class="MyContent" >From <telerik:radnumerictextbox id="tbFromNominalValue" runat="server" ValidationGroup="Group1" width="120"  />
                 To <telerik:radnumerictextbox id="tbToNominalValue" runat="server" ValidationGroup="Group1" width="129" />
            </td>
        </tr>
        
        <tr>
            <td class="MyLable">Legal ID:</td>
            <td class="MyContent" width="300">
                <telerik:radtextbox id="tbLegalID" runat="server" validationGroup="Group1" width="300"></telerik:radtextbox>
            </td>
            </tr>
        
        </table>
</div>
<div style="padding:10px;">
    <telerik:RadGrid ID="RadGridPreview" runat="server" AutoGenerateColumns="false"  AllowPaging="true" 
        OnNeedDataSource="RadGridPreview_OnNeedDataSource" >
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn  HeaderText="Contingent ID"  DataField="CollateralInfoID" HeaderStyle-Width="120"
                    ItemStyle-HorizontalAlign="left" />
                <telerik:GridBoundColumn HeaderText="Reference ID" DataField="ContingentEntryID" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                    HeaderStyle-Width="150" />
                <telerik:GridBoundColumn HeaderText="Customer Name" DataField="GBFULLNAME" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                    HeaderStyle-Width="150" />
                <telerik:GridBoundColumn HeaderText="Debit or Credit" DataField="DCTypeName" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150"
                    HeaderStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderText="Currency" DataField="Currency" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="150" />
                 <telerik:GridBoundColumn HeaderText="Amount" DataField="Amount" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right"
                     DataType="System.Decimal"  DataFormatString="{0:N}" />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25px" />
                    <ItemTemplate>
                        <a href='<%# getUrlPreview(Eval("CollateralInfoID").ToString()) %>'><img src="Icons/bank/text_preview.png" width="20" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>
