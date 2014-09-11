<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChequePayStop_Enquiry.ascx.cs" Inherits="BankProject.Views.TellerApplication.ChequePayStop_Enquiry" %>
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
                <telerik:radtextbox  id="tbDocID" runat="server" validationGroup="Group1" ></telerik:radtextbox>
            </td>
            <td class="MyLable" >Working Account:</td>
            <td class="MyContent" >
                <telerik:radtextbox runat="server" id="tbWorkingAcct" validationGroup="Group1"  ></telerik:radtextbox>
            </td>
            <td class="MyLable">Cheque No:</td>
            <td class="MyContent">
                <telerik:radnumerictextbox runat="server" id="tbChequeNo" ValidationGroup="Group1" Numberformat-decimalDigits="0" Numberformat-Groupseparator="" />
            </td>
        </tr>
        <tr>
            <td class="MyLable">CustomerID:</td>
            <td class="MyContent">
                <telerik:radtextbox runat="server" id="tbCustomerID" ValidationGroup="Group1" />
            </td>
            <td class="MyLable">Customer Name:</td>
             <td class="MyContent">   <telerik:radtextbox runat="server" id="tbCustomerName" ValidationGroup="Group1" />
            </td>
            <td class="MyLable">Active Date:</td>
            <td class="MyContent">
                <telerik:radDatePicker id="rdpActiveDate" runat="server" />
            </td>
        </tr></table>
        <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="MyLable">Cheque Type</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbChequeType" Allowcustomtext="false" MarkFirstMatch="true" AppendDataBoundItems="true" 
                     runat="server">
                   <Items>
                         <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                    </telerik:RadComboBox> 
            </td>
            <td class="MyLable">Stopping Reason:</td>
            <td class="MyContent">
                <telerik:RadComboBox id="rcbStopReason" runat="server" MarkFirstMatch="true" AllowCustomText="false" >
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="1" Text="1 - CHEQUES LOST" />
                            <telerik:RadComboBoxItem Value="2" Text="2 - CHEQUES DESTROYED" />
                            <telerik:RadComboBoxItem Value="3" Text="3 - CHEQUES STOLEN" />
                        </Items>
                </telerik:RadComboBox>
            </td>
            <td class="MyLable" style="visibility:hidden;">asdf asf asd</td>
            <td class="MyContent" style="visibility:hidden;">
                <telerik:radTextBox id="RadTextBox1" runat="server" />
            </td>
            </tr>
    </table>
       
</div>
<div style="padding:10px;">
    <telerik:RadGrid ID="RadGridDataPreview" runat="server"  AutoGenerateColumns="false"  AllowPaging="true" OnNeedDataSource="DataPreview_OnNeedDataSource" >
       <MasterTableView>
           <Columns>
               <telerik:GridBoundColumn HeaderText="Account ID"  DataField="AccountID" HeaderStyle-Width="300" />
               <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" HeaderStyle-Width="300" />
               <telerik:GridBoundColumn HeaderText="Reason" DataField="ReasonStopDesc" HeaderStyle-Width="300"/>
               <telerik:GridBoundColumn HeaderText="FromCheque" DataField="FromChequeSerial" HeaderStyle-Width="100"/>
               <telerik:GridBoundColumn HeaderText="ToCheque" DataField="ToChequeSerial" HeaderStyle-Width="100"/>
               <telerik:GridBoundColumn HeaderText="Quantity" DataField="NoOfLeave" HeaderStyle-Width="300"
                  headerStyle-horizontalAlign="center"  itemstyle-horizontalAlign="center"/>
               <telerik:GridBoundColumn HeaderText="Active Date" DataField="ActiveDate" />
               <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
               <telerik:GridTemplateColumn>
                   <ItemStyle width="25" />
                   <ItemTemplate>
                       <a href='<%# getUrlPreview(Eval("AccountID").ToString()) %>'><img src="Icons/bank/text_preview.png" width="20" /></a>
                   </ItemTemplate>
               </telerik:GridTemplateColumn>
           </Columns>
       </MasterTableView>
         </telerik:RadGrid>

</div>