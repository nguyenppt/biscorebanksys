<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChequeIssue_Enquiry.ascx.cs" Inherits="BankProject.Views.TellerApplication.ChequeIssue_Enquiry" %>
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
            <td class="MyLable">Cheque Reference:</td>
            <td class="MyContent" width="300">
                <telerik:radtextbox  id="tbChequeRef" runat="server" validationGroup="Group1" width="300"></telerik:radtextbox>
            </td>
            <td class="MyLable" >Working Account:</td>
            <td class="MyContent" width="300">
                <telerik:radtextbox runat="server" id="tbWorkingAcct" validationGroup="Group1" width="300"  ></telerik:radtextbox>
            </td>
        </tr>
        <tr>
            <td class="MyLable">Cheque Type</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbChequeType" runat="server" appendDataboundItems="true" MarkFirstMatch="true" 
                     width="300" AllowCustomText="false">
                    <Items>
                         <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="MyLable">Issued Date:</td>
            <td class="MyContent">
                <telerik:radDatePicker id="rdpIssuedDate" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="MyLable">Cheque No</td>
            <td class="MyContent">
                <telerik:RadNumericTextBox id="tbChequeNo" runat="server"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator=""  width="300" >
                </telerik:RadNumericTextBox>
            </td>
        </tr>
    </table>
</div>
<div style="padding:10px;">
    <telerik:RadGrid id="RadGridView" runat="server" AllowPaging="true" AutoGenerateColumns="false" 
        OnNeedDataSource="RadGrid1_OnNeedDataSource" >
        <MasterTableView>
            <columns>
                <telerik:GridBoundColumn HeaderText="Cheque Reference" DataField="ChequeID"  HeaderStyle-Width="120"
                    ItemStyle-HorizontalAlign="left" />
                <telerik:GridBoundColumn HeaderText="Cheque Type" DataField="ChequeType" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="150" />
                <telerik:GridBoundColumn HeaderText="WorkingAccount" DataField="WorkingAcctID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="150" />
                <telerik:GridBoundColumn HeaderText="Quantity" DataField="Quantity"  ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150"
                    HeaderStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderText="Serial Number" DataField="ChequeNo"  HeaderStyle-Width="150"/>
                 <telerik:GridBoundColumn HeaderText="Issue Date" DataField="IssueDate" ItemStyle-HorizontalAlign="Right" 
                    HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right"/>
                 <telerik:GridBoundColumn HeaderText="Status" DataField="Status" ItemStyle-HorizontalAlign="Right" 
                    HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right"/>
                <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Right" >
                    <ItemStyle Width="25" />
                    <Itemtemplate>
                        <a href='<%# geturlReview(Eval("ChequeID").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                    </Itemtemplate>
                </telerik:GridTemplateColumn>
            </columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>
