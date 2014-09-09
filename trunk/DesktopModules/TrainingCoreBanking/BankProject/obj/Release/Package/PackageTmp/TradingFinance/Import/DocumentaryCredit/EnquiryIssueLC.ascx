<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnquiryIssueLC.ascx.cs" Inherits="BankProject.TradingFinance.Import.DocumentaryCredit.EnquiryIssueLC" %>

<telerik:RadToolBar runat="server" ID="RadToolBar2" EnableRoundedCorners="true" EnableShadows="true" width="100%"
    OnButtonClick="RadToolBar1_ButtonClick">
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
            <td style="width: 60px">REF No.</td>
            <td ><asp:TextBox ID="txtCode" runat="server" Width="200"/></td>
        </tr>
        <tr>
            <td style="width: 120px">Applicant ID</td>
            <td width="355">
                <telerik:RadComboBox 
                    AppendDataBoundItems="True"  
                    OnItemDataBound="rcbApplicantID_ItemDataBound"
                    ID="rcbApplicantID" Runat="server"
                    MarkFirstMatch="True" Width="355"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                </telerik:RadComboBox>
            </td>
            <td style="width: 120px">&nbsp; Applicant Name</td>
            <td>
                <asp:TextBox ID="txtApplicantName" runat="server" Width="355"/>
            </td>
        </tr>
    </table>
    
    <telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True"
         OnNeedDataSource="radGridReview_OnNeedDataSource">
        <MasterTableView DataKeyNames="Id">
            <Columns>
                <telerik:GridBoundColumn HeaderText="LC Code" DataField="NormalLCCode"  />
                <telerik:GridBoundColumn HeaderText="Collection Type" DataField="LCType" />
                <telerik:GridBoundColumn HeaderText="CCY" DataField="Currency" >
                    <ItemStyle Width="100" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Amount" DataField="Amount" >
                    <ItemStyle Width="100" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Status" DataField="Status" >
                    <ItemStyle Width="100" HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# geturlReview(Eval("NormalLCCode").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                    </itemtemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        $("#<%=txtCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btSearch.ClientID %>").click();
            }
        });

        $("#<%=txtApplicantName.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btSearch.ClientID %>").click();
            }
        });
    </script>
</telerik:RadCodeBlock>
<div style="visibility:hidden;"><asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" /></div>