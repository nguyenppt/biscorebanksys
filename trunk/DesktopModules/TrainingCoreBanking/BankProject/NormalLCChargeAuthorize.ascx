<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NormalLCChargeAuthorize.ascx.cs" Inherits="BankProject.NormalLCChargeAuthorize" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="NormalLCCharge.ascx" TagName="NormalLCCharge" TagPrefix="uc1" %>

<asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <contenttemplate>
        <div>
            <asp:CheckBox ID="CheckBoxWaiveCHarges" runat="server" Text="Waive Charges" AutoPostBack="True" OnCheckedChanged="CheckBoxWaiveCHarges_CheckedChanged" />
        </div>
        <div>
            <asp:TextBox ID="TextBoxDebug" runat="server" />
        </div>

        <asp:TabContainer ID="TabContainerCharges" runat="server" ActiveTabIndex="0">
            <asp:TabPanel runat="server" HeaderText="TabPanelOPEN" ID="TabPanelOPEN">
                <HeaderTemplate>
                    Open charge
                </HeaderTemplate>
                <ContentTemplate>
                    <uc1:NormalLCCharge ID="ChargeOpen" runat="server" ChargeCode="OPEN" />
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanelOPENAMORT" runat="server">
                <HeaderTemplate>
                    Open charge (Amort)
                </HeaderTemplate>
                <ContentTemplate>
                    <uc1:NormalLCCharge ID="NormalLCCharge1" runat="server" ChargeCode="OPENAMORT" />
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanelCABLE" runat="server">
                <HeaderTemplate>
                    Cable Charge
                </HeaderTemplate>
                <ContentTemplate>
                    <uc1:NormalLCCharge ID="NormalLCCharge5" runat="server" ChargeCode="CABLE" />
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </contenttemplate>
</asp:UpdatePanel>

