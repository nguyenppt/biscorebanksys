<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AmendLoanContract.ascx.cs" Inherits="BankProject.Views.TellerApplication.AmendLoanContract" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadToolBar id="RadToolBar1" runat="server" EnableRoundedCorners="true" EnableShadows="true" width="100%" OnButtonClick="RadToolBar1_OnButtonClick">
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
    <telerik:RadGrid id="RadGrid" runat="server" allowpaging="true" AutoGenerateColumns="false" onneeddatasource="RadGrid_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Customer ID" DataField="CustomerID" />
                <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" />
                <telerik:GridBoundColumn HeaderText="Loan Contract Reference" DataField="LoanContractReference" />
                <telerik:GridBoundColumn HeaderText="Working Balance"   DataField="WorkingBalance" 
                                          ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="Right"/>
                <telerik:GridBoundColumn HeaderText="Purpose" DataField="Purpose" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" />
                <telerik:GridBoundColumn HeaderText="Kind Of Contract" DataField="KindOfContract" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left" />
                <telerik:GridTemplateColumn>
                    <Itemstyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# getUrlReview(Eval("CustomerID").ToString())%>'> <img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>