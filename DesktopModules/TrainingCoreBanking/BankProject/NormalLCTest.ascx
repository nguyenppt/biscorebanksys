<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NormalLCTest.ascx.cs" Inherits="BankProject.NormalLCTest" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
</asp:ScriptManagerProxy>
<asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
    <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
        <HeaderTemplate>
            Tab 1
        </HeaderTemplate>
        <ContentTemplate>
            Tab 1
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
        <HeaderTemplate>
            Tab 2
        </HeaderTemplate>
        <ContentTemplate>
            Tab 2
        </ContentTemplate>
    </asp:TabPanel>
</asp:TabContainer>

