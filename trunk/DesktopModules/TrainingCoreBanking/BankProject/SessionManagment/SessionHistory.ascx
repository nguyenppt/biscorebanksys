<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SessionHistory.ascx.cs" Inherits="BankProject.SessionManagment.SessionHistory" %>
<telerik:RadToolBar runat="server" ID="radToolBar" EnableRoundedCorners="true" EnableShadows="true" Width="100%"
    OnButtonClick="radToolBar_OnButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit" Enabled="False"
            ToolTip="Create New" Value="btnEditShift" CommandName="CreateNew">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png" Enabled="False"
            ToolTip="Manage Shift" Value="btReview" CommandName="ManageShift">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Purge" Value="btAuthorize" CommandName="purge">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png" Enabled="False"
            ToolTip="Revert" Value="btRevert" CommandName="revert">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="Search">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png" Enabled="False"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
<div style="padding: 10px">
    <table style="width: 80%">
        <tr>
            <td width="40px">User
            </td>
            <td colspan="3">
                <telerik:RadComboBox runat="server" ID="cboUsername" DataTextField="Username" DataValueField="UserID"></telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td width="40px">
                From:  
            </td>
            <td width="180px">
                <telerik:RadDatePicker runat="server" ID="rdpkFromDate"></telerik:RadDatePicker>
            </td>
            <td width="20px">
                To: 
            </td>
            <td>
                <telerik:RadDatePicker runat="server" ID="rdpkToDate"></telerik:RadDatePicker>
            </td>
        </tr>
    </table>
    <br/>
    <telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGrid" AllowPaging="True" OnNeedDataSource="radGrid_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="User" DataField="Username" />
                <telerik:GridBoundColumn HeaderText="Title" DataField="Title" />
                <telerik:GridBoundColumn HeaderText="Date" DataField="CreatedDate" DataFormatString="{0:dd/MM/yyyy}"/>
                <telerik:GridBoundColumn HeaderText="Begin Shift" DataField="BeginShift" />
                <telerik:GridBoundColumn HeaderText="End Shift" DataField="EndShift" />
                <telerik:GridBoundColumn HeaderText="Total User" DataField="TotalUser" />
                <telerik:GridBoundColumn HeaderText="Max Session" DataField="MaxSession" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>
