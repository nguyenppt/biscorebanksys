<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default.ascx.cs" Inherits="BankProject.SessionManagment.Default" %>
<telerik:RadToolBar runat="server" ID="radToolBar" EnableRoundedCorners="true" EnableShadows="true" Width="100%"
    OnButtonClick="radToolBar_OnButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Create New" Value="btnEditShift" CommandName="CreateNew">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png" Enabled="False"
            ToolTip="Manage Shift" Value="btReview" CommandName="ManageShift">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png" Enabled="False"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png" Enabled="False"
            ToolTip="Revert" Value="btRevert" CommandName="revert">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="Search">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png" Enabled="False"
            ToolTip="View History" Value="btnViewHistory" CommandName="ViewHistory">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>

<div style="padding: 10px">
    <table>
        <tr>
            <td>Username
            </td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtUsername"></telerik:RadTextBox>
            </td>
        </tr>
    </table>
    <br/>
    <telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridAccountPeriod" AllowPaging="True" OnNeedDataSource="radGridAccountPeriod_OnNeedDataSource">
        <MasterTableView DataKeyNames="Id">
            <Columns>
                <telerik:GridBoundColumn HeaderText="Username" DataField="Username" />
                <telerik:GridBoundColumn HeaderText="Title" DataField="Title" />
                <telerik:GridBoundColumn HeaderText="BeginPeriod" DataField="BeginPeriod"  DataFormatString="{0:dd/MM/yyyy}"/>
                <telerik:GridBoundColumn HeaderText="EndPeriod" DataField="EndPeriod"  DataFormatString="{0:dd/MM/yyyy}"/>
                <telerik:GridBoundColumn HeaderText="WorkingDayDisplay" DataField="WorkingDayDisplay" />
                <telerik:GridBoundColumn HeaderText="ShiftDisplay" DataField="ShiftDisplay" />
                <telerik:GridBoundColumn HeaderText="AvailableSlot" DataField="AvailableSlot" />
                <telerik:GridCheckBoxColumn HeaderText="IsEnabled" DataField="IsEnabled" />
                <telerik:GridCheckBoxColumn HeaderText="IsBlocked" DataField="IsBlocked" />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="50" />
                    <ItemTemplate>
                        <a href='<%# GetEditAccountPeriodUrl(Eval("Id").ToString()) %>'>
                            <img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" />
                        </a>
                        <a href='<%# GetDeleteAccountPeriodUrl(Eval("Id").ToString()) %>'>
                            <img src="Icons/Sigma/Delete_16X16_Standard.png" alt="" title="" style="" width="20" />
                        </a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>

