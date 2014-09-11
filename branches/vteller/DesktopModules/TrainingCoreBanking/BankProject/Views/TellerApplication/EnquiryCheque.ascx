<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnquiryCheque.ascx.cs" Inherits="BankProject.Views.TellerApplication.EnquiryCheque" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<telerik:RadToolBar runat="server" ID="RadToolBar2" EnableRoundedCorners="true" EnableShadows="true" width="100%"  OnButtonClick="RadToolBar2_OnButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
                ToolTip="Commit Data" Value="btSave" CommandName="save">
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
                ToolTip="Revert" Value="btSearch" CommandName="search">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>

<div style="padding:10px;">
    <table width="100%" cellpadding="0" cellspacing="0" style="margin-bottom:10px">
        <tr>
            <td class="MyLable">Cheques Reference: </td>
            <td class="MyContent">
                <asp:TextBox ID="tbChequeReference" runat="server" Width="300px" />
            </td>
            <td class="MyLable">Issue Date:</td>
            <td class="MyContent" >
                <telerik:RadDatePicker id="rdpIssueDate" runat="server"></telerik:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td class="MyLable">Customer ID:</td>
            <td>
                 <telerik:RadComboBox ID="TbCustomerID"
                        AppendDataBoundItems="true" 
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                    Width="300px"
                        runat="server" ValidationGroup="Group1"></telerik:RadComboBox>
            </td>
            <td class="MyLable">Customer Name:</td>
            <td class="MyContent">
                <asp:TextBox ID="tbCustomerName" runat="server" Width="300px"  />
            </td>
        </tr>
    </table>

    <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="true" AutoGenerateColumns="false"  OnNeedDataSource="RadGrid1_OnNeedDataSource">
        <MasterTableView>
            <Columns>
               <telerik:GridBoundColumn HeaderText="Customer ID" DataField="CustomerID" />
                <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" />
                <telerik:GridBoundColumn HeaderText="Cheque Reference" DataField="ChequeReference" />
                <telerik:GridBoundColumn HeaderText="ChequeType" DataField="ChequeType" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderText="Quantity" DataField="Quantity" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                <telerik:GridBoundColumn HeaderText="Cheque No" DataField="ChequeNo" />
                <telerik:GridBoundColumn HeaderText="IssueDate" DataField="IssueDate" />
                </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>


<script type="text/javascript">
    $('#<%=tbChequeReference.ClientID%>').keyup(function (event) {
        if (event.keyCode == 13) { $("#<%=btSearch.ClientID%>").click();  }
    });
    $('#<%=TbCustomerID.ClientID%>').keyup(function (event) {
        if (event.keyCode == 13) { $("<%=btSearch.ClientID%>").click(); }
    });
    $('#<%=tbCustomerName.ClientID%>').keyup(function (event) {
        if (event.keyCode == 13) { $("<%=btSearch.ClientID%>").click(); }
    });
    $find("<%=rdpIssueDate.ClientID%>").keyup(function (event) {
        if (event.keyCode == 13) { $("<%=btSearch.ClientID%>").click(); }
    });


</script>
<div style="visibility:hidden;"><asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" /></div>