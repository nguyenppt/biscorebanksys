<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditAccountPeriod.ascx.cs" Inherits="BankProject.SessionManagment.EditAccountPeriod" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />
<telerik:RadToolBar runat="server" ID="radToolBar" EnableRoundedCorners="true" EnableShadows="true" Width="100%"
    OnButtonClick="radToolBar_OnButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btSave" CommandName="commit">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png" Enabled="False"
            ToolTip="Preview" Value="btReview" CommandName="review">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png" Enabled="False"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Back" Value="btRevert" CommandName="Back">
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
    <fieldset>
        <legend>
            <div style="font-weight: bold; text-transform: uppercase;">Basic</div>
        </legend>
        <table style="width: 80%">
            <asp:HiddenField runat="server" ID="hfAccountPeriodId" />
            <tr>
                <td width="120px">User <span class="Required">(*)</span></td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cboUsername" DataTextField="Username" DataValueField="UserID"></telerik:RadComboBox>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator4"
                        ControlToValidate="cboUsername"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="User is required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="120px">Title</td>
                <td>
                    <telerik:RadTextBox runat="server" ID="txtTitle"></telerik:RadTextBox>
                </td>
            </tr>


        </table>
    </fieldset>
    <fieldset>
        <legend>
            <div style="font-weight: bold; text-transform: uppercase;">Time</div>

        </legend>
        <table style="width: 80%">
            <tr>
                <td width="120px">Begin Period <span class="Required">(*)</span></td>
                <td>
                    <telerik:RadDatePicker runat="server" ID="rdpkBeginPeriod"></telerik:RadDatePicker>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="rdpkBeginPeriod"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Begin period is required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>End Period <span class="Required">(*)</span>
                </td>
                <td>
                    <telerik:RadDatePicker runat="server" ID="rdpkEndPeriod" />
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator2"
                        ControlToValidate="rdpkEndPeriod"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="End period is required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Working days
                </td>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td>Sunday
                            <asp:CheckBox runat="server" ID="chkSunday" />
                            </td>
                            <td>Monday
                            <asp:CheckBox runat="server" ID="chkMonday" />
                            </td>
                            <td>Tuesday
                            <asp:CheckBox runat="server" ID="chkTuesday" />
                            </td>
                            <td>Wednesday
                            <asp:CheckBox runat="server" ID="chkWednesday" />
                            </td>
                        </tr>
                        <tr>

                            <td>Thursday
                            <asp:CheckBox runat="server" ID="chkThursday" />
                            </td>
                            <td>Friday
                            <asp:CheckBox runat="server" ID="chkFriday" />
                            </td>
                            <td>Saturday
                            <asp:CheckBox runat="server" ID="chkSaturday" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>Shifts</td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cboShift1" DataTextField="Title" DataValueField="Id"></telerik:RadComboBox>
                    <telerik:RadComboBox runat="server" ID="cboShift2" DataTextField="Title" DataValueField="Id"></telerik:RadComboBox>
                    <telerik:RadComboBox runat="server" ID="cboShift3" DataTextField="Title" DataValueField="Id"></telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td>Max Session <span class="Required">(*)</span>
                </td>
                <td>
                    <telerik:RadNumericTextBox runat="server" ID="rnumAvailableSlot" Value="0">
                        <NumberFormat AllowRounding="false" DecimalDigits="0" DecimalSeparator="." GroupSeparator="," GroupSizes="3" />
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator3"
                        ControlToValidate="rnumAvailableSlot"
                        ValidationGroup="Commit"
                        ErrorMessage="Max session is required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>
            <div style="font-weight: bold; text-transform: uppercase;">Control</div>

        </legend>
        <table style="width: 80%">
            <tr>
                <td width="120px">Enabled
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkIsEnabled" />
                </td>
            </tr>
            <tr>
                <td>Block
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkIsBlocked" />
                </td>
            </tr>
        </table>
    </fieldset>
</div>
