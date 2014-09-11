<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NormalLCChargeIssue.ascx.cs" Inherits="BankProject.NormalLCChargeIssue" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="NormalLCCharge.ascx" TagName="NormalLCCharge" TagPrefix="uc1" %>



<asp:TextBox ID="TextBoxDebug" runat="server" Visible="false" />

<asp:SqlDataSource ID="SqlDataSourceChargeMaster" runat="server" ConnectionString="<%$ ConnectionStrings:VietVictoryCoreBanking %>" SelectCommand="SELECT dbo.V_LC_ChargeMaster.* FROM dbo.V_LC_ChargeMaster where NormalLCCode=@NormalLCCode" UpdateCommand="sp_LC_ChargeMaster_Update" UpdateCommandType="StoredProcedure">
    <SelectParameters>
        <asp:Parameter Name="NormalLCCode" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="NormalLCCode" Type="String" />
        <asp:Parameter Name="ChargeRemarks" Type="String" />
        <asp:Parameter Name="ChargeVATNo" Type="String" />
        <asp:Parameter Name="ChargeTaxCode" Type="String" />
        <asp:Parameter Name="ChargeTaxCcy" Type="String" />
        <asp:Parameter Name="ChargeTaxAmt" Type="String" />
        <asp:Parameter Name="ChargeTaxinLCCYAmt" Type="String" />
        <asp:Parameter Name="ChargeTaxDate" Type="String" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:FormView ID="FormViewChargeMaster" runat="server" DataSourceID="SqlDataSourceChargeMaster" Width="100%" DefaultMode="Insert">
    <EditItemTemplate>
    </EditItemTemplate>
    <InsertItemTemplate>
        <div>
            <asp:CheckBox ID="CheckBoxWaiveCHarges" runat="server" Text="Waive Charges" AutoPostBack="True" OnCheckedChanged="CheckBoxWaiveCHarges_CheckedChanged"
                Font-Bold="true" />
        </div>

        <asp:TabContainer ID="TabContainerCharges" runat="server" ActiveTabIndex="0">
            <asp:TabPanel runat="server" HeaderText="TabPanelOPEN" ID="TabPanelOPEN">
                <HeaderTemplate>
                    Open charge
                </HeaderTemplate>
                <ContentTemplate>
                    <uc1:NormalLCCharge ID="ChargeOpen2" runat="server" ChargeCode="OPEN" EditMode="false" />
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanelOPENAMORT" runat="server">
                <HeaderTemplate>
                    Open charge (Amort)
                </HeaderTemplate>
                <ContentTemplate>
                    <uc1:NormalLCCharge ID="NormalLCCharge2" runat="server" ChargeCode="OPENAMORT" EditMode="false"  />
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanelCABLE" runat="server">
                <HeaderTemplate>
                    Cable Charge
                </HeaderTemplate>
                <ContentTemplate>
                    <uc1:NormalLCCharge ID="NormalLCCharge5" runat="server" ChargeCode="CABLE" EditMode="false"  />
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>

        <table width="100%" cellpadding="0" cellspacing="0" style="border-bottom: 1px solid #CCC;">
            <tr>
                <td class="MyLable">Charge Remarks</td>
                <td class="MyContent">
                    <asp:TextBox ID="tbChargeRemarks" runat="server" Width="300" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">VAT No</td>
                <td class="MyContent">
                    <asp:TextBox ID="tbVatNo" runat="server" Enabled="false" Width="300" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Tax Code</td>
                <td class="MyContent">
                    <asp:Label ID="lblTaxCode" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Tax Ccy</td>
                <td class="MyContent">
                    <asp:Label ID="lblTaxCcy" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Tax Amt</td>
                <td class="MyContent">
                    <asp:Label ID="lblTaxAmt" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Tax in LCCY Amt</td>
                <td class="MyContent">
                    <asp:TextBox ID="TextBox1" runat="server" Enabled="false" Width="300" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Tax Date</td>
                <td class="MyContent">
                    <asp:TextBox ID="TextBox2" runat="server" Enabled="false" Width="300" />
                </td>
            </tr>
        </table>
    </InsertItemTemplate>
    <ItemTemplate>
    </ItemTemplate>
</asp:FormView>
