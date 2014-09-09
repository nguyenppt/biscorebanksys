<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PastDueLoanRepayment_Search.ascx.cs" Inherits="BankProject.Views.TellerApplication.PastDueLoanRepayment_Search" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" width="100%"  OnButtonClick="RadToolBar1_OnButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
                ToolTip="Commit Data" Value="btCommitData" CommandName="save">
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
                ToolTip="Revert" Value="btSearch" CommandName="search">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>

<div style="padding:10px;">
    <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="true" AutoGenerateColumns="false"  OnNeedDataSource="RadGrid_OnNeedDataSource">
        <MasterTableView>
            <Columns>
               <telerik:GridBoundColumn HeaderText="Customer ID" DataField="CustomerID" />
                <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" />
                <telerik:GridBoundColumn HeaderText="Loan Contract Reference" DataField="LoanContractReference" />
                <telerik:GridBoundColumn HeaderText="Purpose" DataField="Purpose" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" />
               <%-- <telerik:GridBoundColumn HeaderText="Repay Amount" DataField="RepayAmount" ItemStyle-HorizontalAlign="right" HeaderStyle-HorizontalAlign="right" />
                <telerik:GridBoundColumn HeaderText="RepayDate" DataField="RepayDate" />--%>
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <Itemtemplate>
                        <a href='<%# geturlReview(Eval("CustomerID").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /></a> 
                    </Itemtemplate>
                </telerik:GridTemplateColumn>
                </Columns>
        </MasterTableView>
    </telerik:RadGrid>
     <table width="100%" cellpadding="0" cellspacing="0" style="margin-bottom:10px" style="visibility:hidden;">
        <tr>
            <td class="MyLable" style="visibility:hidden;">Loan Contract Reference: </td>
            <td class="MyContent" style="visibility:hidden;">
                <asp:TextBox ID="tbLoanContractReference" runat="server" Width="300px" />
            </td>
            <td class="MyLable" style="visibility:hidden;">Repay Date:</td>
            <td class="MyContent" style="visibility:hidden;">
                <telerik:RadDatePicker id="rdpRepayDate" runat="server"></telerik:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td class="MyLable" style="visibility:hidden;">Customer ID:</td>
            <td>
                <asp:TextBox ID="TbCustomerID" runat="server" Width="300px" style="visibility:hidden;" />
            </td>
            <td class="MyLable" style="visibility:hidden;">Customer Name:</td>
            <td class="MyContent">
                <asp:TextBox ID="tbCustomerName" runat="server" Width="300px" style="visibility:hidden;" />
            </td>
        </tr>
    </table>
</div>
<telerik:RadCodeBlock runat="server" >
    <script type="text/javascript">
        $('#<%=tbLoanContractReference.ClientID%>').keyup(function (event) {
            if (event.keyCode == 13) { $("#<%=btSearch.ClientID%>").click(); }
        });
        $('#<%=rdpRepayDate.ClientID%>').keyup(function (event) {
            if (event.keyCode == 13) { $("<%=btSearch.ClientID%>").click();}
        });
        $('#<%=TbCustomerID.ClientID%>').keyup(function (event) {
            if (event.keyCode == 13) { $("<%=btSearch.ClientID%>").click(); }
        });
        $('#<%=tbCustomerName.ClientID%>').keyup(function (event) {
            if (event.keyCode == 13) { $("<%=btSearch.ClientID%>").click(); }
        });
    </script>
</telerik:RadCodeBlock>
<div style="visibility:hidden">
    <asp:Button ID="btSearch" runat="server"  OnClick="btSearch_OnClick" />
</div>