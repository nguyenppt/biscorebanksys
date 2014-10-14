<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShiftManage.ascx.cs" Inherits="BankProject.SessionManagment.ShiftManage" %>
<telerik:RadToolBar runat="server" ID="radToolBar" EnableRoundedCorners="true" EnableShadows="true" Width="100%"
    OnButtonClick="radToolBar_OnButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit data" Value="btnEditShift" CommandName="commit">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png" Enabled="False"
            ToolTip="Preview" Value="btReview" CommandName="review">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png" Enabled="False"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png" Enabled="False"
            ToolTip="Back" Value="btnBack" CommandName="back">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png" Enabled="False"
            ToolTip="Revert" Value="btSearch" CommandName="search">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png" Enabled="False"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
<div style="padding: 10px">
    <asp:HiddenField ID="hfShiftId" runat="server" />
    <table>
        <tr>
            <td>Title <span class="Required">(*)</span>
            </td>
            <td>
                <telerik:RadTextBox ID="txtTitle" runat="server" Width="355"></telerik:RadTextBox>
                <asp:RequiredFieldValidator
                    runat="server"
                    ID="RequiredFieldValidator2"
                    ControlToValidate="txtTitle"
                    ValidationGroup="Commit"
                    ErrorMessage="Title is required" ForeColor="Red">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Begin <span class="Required">(*)</span>
            </td>
            <td>
                <telerik:RadTimePicker ID="tprBeginShift" runat="server" Width="355"></telerik:RadTimePicker>
                <asp:RequiredFieldValidator
                    runat="server"
                    ID="RequiredFieldValidator3"
                    ControlToValidate="tprBeginShift"
                    ValidationGroup="Commit"
                    ErrorMessage="Begin shift is required" ForeColor="Red">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" 
                    runat="server"
                    ControlToValidate="tprBeginShift" 
                    OnServerValidate="tprBeginShift_OnValidate" 
                    ValidationGroup="Commit" 
                    ErrorMessage="Begin shift must lower than end shift" ForeColor="Red">
                </asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td>End <span class="Required">(*)</span>
            </td>
            <td>
                <telerik:RadTimePicker ID="tprEndShift" runat="server" Width="355"></telerik:RadTimePicker>
                <asp:RequiredFieldValidator
                    runat="server"
                    ID="RequiredFieldValidator1"
                    ControlToValidate="tprEndShift"
                    ValidationGroup="Commit"
                    ErrorMessage="End shift is required" ForeColor="Red">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <br/>
    <telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="rgrdMain" AllowPaging="True" OnNeedDataSource="rgrdMain_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Title" DataField="Title" />
                <telerik:GridBoundColumn HeaderText="BeginShift" DataField="BeginShift" />
                <telerik:GridBoundColumn HeaderText="EndShift" DataField="EndShift" />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="50" />
                    <ItemTemplate>
                        <a href='<%# GetEditShiftUrl(Eval("Id").ToString()) %>'>
                            <img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" />
                        </a>
                        <a href='<%# GetDeleteShiftUrl(Eval("Id").ToString()) %>'>
                            <img src="Icons/Sigma/Delete_16X16_Standard.png" alt="" title="" style="" width="20" />
                        </a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>
