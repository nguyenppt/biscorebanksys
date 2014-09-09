<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenRevoling_Enquiry.ascx.cs" Inherits="BankProject.Views.TellerApplication.OpenRevoling_Enquiry" %>
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
            <td class="MyLable">Account Reference:</td>
            <td class="MyContent" width="300">
                <telerik:radtextbox  id="tbAcctRef" runat="server" validationGroup="Group1" width="300"></telerik:radtextbox>
            </td>
            <td class="MyLable" >Customer ID:</td>
            <td class="MyContent" width="300">
                <telerik:radtextbox runat="server" id="tbCustomerID" validationGroup="Group1" width="300"  ></telerik:radtextbox>
            </td>
        </tr>
        <tr>
            <td class="MyLable">Full Name:</td>
            <td class="MyContent" width="300">
                <telerik:radtextbox id="tbFullName" runat="server" validationGroup="Group1" width="300"></telerik:radtextbox>
            </td>
            <td class="MyLable">Currency:</td>
            <td class="MyContent" width="300">
                <telerik:RadComboBox id="rcbCurrency" runat="server" appendDataboundItems="true" AllowCustomText="false" MarkFirdMatch="true" 
                    width="300" height="150">
                    <Items>
                        <telerik:RadComboboxItem text="" value="" />
                    </Items>
                    </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="MyLable">Category:</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbCategory" runat="server" appendDataboundItems="true" AllowCustomText="false" MarkFirdMatch="true"
                    width="300" height="150">
                    <Items>
                        <telerik:RadComboboxItem text="" value="" />
                    </Items>
                    </telerik:RadComboBox>
            </td>
            <td class="MyLable">Doc ID:</td>
            <td class="MyContent" >
                <telerik:radtextbox id="tbDocId" runat="server" ValidationGroup="Group1" width="300" />
            </td>
        </tr>
        <tr>
            <td class="MyLable">Interested Repay Account:</td>
            <td class="MyContent">
                 <telerik:radtextbox id="tbIntRepayAcct" runat="server" ValidationGroup="Group1" width="300" />
            </td>           
        </tr>
        </table>
</div>
<div style="padding:10px;">
    <telerik:RadGrid id="RadGrid" runat="server" AllowPaging="true" AutoGenerateColumns="false" 
        OnNeedDataSource="RadGrid1_OnNeedDataSource" >
        <MasterTableView>
            <columns>
                <telerik:GridBoundColumn HeaderText="Contract ID" DataField="ID" />
                <telerik:GridBoundColumn HeaderText="Category" DataField="CategoryName" />
                <telerik:GridBoundColumn HeaderText="Customer ID" DataField="CustomerID" />
                <telerik:GridBoundColumn HeaderText="Customer Name" DataField="GBFullName" />
                <telerik:GridBoundColumn HeaderText="Doc ID" DataField="DocID" />
                <telerik:GridBoundColumn HeaderText="Commitment Amount" DataField="CommitmentAmt" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />
                <telerik:GridBoundColumn HeaderText="Created Date" DataField="CreatedDate" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" />
                <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Right" >
                    <ItemStyle Width="25" />
                    <Itemtemplate>
                        <a href='<%# geturlReview(Eval("ID").ToString(),Eval("Status").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                    </Itemtemplate>
                </telerik:GridTemplateColumn>
            </columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>
