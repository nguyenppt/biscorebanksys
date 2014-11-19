<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BTransList.ascx.cs" Inherits="BankProject.Views.TellerApplication.BTransList" %>
<%@ register Assembly="Telerik.Web.Ui" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadWindowManager id="RadWindowManager1" runat="server"  EnableShadow="true" />
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
            <td class="MyLable">Doc ID:</td>
            <td class="MyContent" >
                <telerik:radtextbox  id="tbLegalID" runat="server" validationGroup="Group1" ></telerik:radtextbox>
            </td>
        </tr>
       </table>
</div>
<div style="padding:10px;">
    <telerik:RadGrid id="RadGridView" runat="server" AllowPaging="true" AutoGenerateColumns="false" 
        OnNeedDataSource="RadGrid1_OnNeedDataSource" >
        <MasterTableView>
            <columns>
                <telerik:GridBoundColumn  HeaderText="Ref ID"  DataField="ID" HeaderStyle-Width="100"/>
                <telerik:GridBoundColumn HeaderText="Sender" DataField="SendingName" HeaderStyle-Width="190"  />
                 <telerik:GridBoundColumn HeaderText="Receiver" DataField="ReceivingName"  HeaderStyle-Width="190"  />
                 <telerik:GridBoundColumn HeaderText="Doc ID" DataField="IdentityCard"  />
                 <telerik:GridBoundColumn HeaderText="TXN Type" DataField="TXN_Type" />
                <telerik:GridBoundColumn HeaderText="Amount" DataField="Amount" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" 
                     HeaderStyle-Width="150" DataType="System.Decimal"  DataFormatString="{0:N}"   />
                <telerik:GridBoundColumn HeaderText="Currency" DataField="Currency" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
               
            </columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>