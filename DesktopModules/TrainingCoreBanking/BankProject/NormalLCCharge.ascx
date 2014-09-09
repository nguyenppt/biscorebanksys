<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NormalLCCharge.ascx.cs" Inherits="BankProject.NormalLCCharge" %>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <contenttemplate>
<asp:FormView ID="FormViewChargeDetail" runat="server" DataKeyNames="NormalLCCode" DataSourceID="SqlDataSourceChargeDetail" DefaultMode="Edit" Width="100%">
    <EditItemTemplate>
        <table style="width: 100%">
    <tr>
        <td class="MyLable">Charge code</td>
        <td class="MyContent">
            <asp:TextBox ID="tbChargeCode" runat="server" ReadOnly="True" Enabled="False"></asp:TextBox>
        </td>
    </tr>
            <tr>
                <td class="MyLable">Charge Acct</td>
                <td class="MyContent">
                    <asp:DropDownList AutoPostBack="True"
                        OnSelectedIndexChanged="rcbChargeAcct_SelectIndexChange"
                        ID="rcbChargeAcct" runat="server" SelectedValue='<%# Bind("ChargeAcct") %>'>
                        <asp:ListItem Value="" Text="" />
                        <asp:ListItem Text="CTY TNHH SONG HONG" Value="03.000237869.4" />
                        <asp:ListItem Text="CTY TNHH PHAT TRIEN PHAN MEM ABC" Value="03.000237870.4" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblChargeAcct" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Charge Period</td>
                <td class="MyContent" colspan="2">
                    <asp:TextBox ID="tbChargePeriod" Text='<%# Bind("ChargePeriod") %>' runat="server" Width="100px" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Charge Ccy</td>
                <td class="MyContent" colspan="2">
                    <asp:DropDownList
                        ID="rcbChargeCcy" runat="server" SelectedValue='<%# Bind("ChargeCcy") %>'>
                        <asp:ListItem Value="" Text="" />
                        <asp:ListItem Value="USD" Text="USD" />
                        <asp:ListItem Value="EUR" Text="EUR" />
                        <asp:ListItem Value="GBP" Text="GBP" />
                        <asp:ListItem Value="JPY" Text="JPY" />
                        <asp:ListItem Value="VND" Text="VND" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Exch. Rate</td>
                <td class="MyContent" colspan="2">
                    <asp:TextBox runat="server" ID="tbExcheRate" Width="200px" Text='<%# Bind("ExchRate") %>' />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Charge Amt</td>
                <td class="MyContent" colspan="2">
                    <asp:TextBox runat="server" ID="tbChargeAmt" Width="200px" AutoPostBack="True"
                        OnTextChanged="tbChargeAmt_TextChanged" Text='<%# Bind("ChargeAmt") %>' />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Party Charged</td>
                <td class="MyContent" style="width: 150px;">
                    <asp:DropDownList AutoPostBack="True"
                        OnSelectedIndexChanged="rcbPartyCharged_SelectIndexChange"
                        ID="rcbPartyCharged" runat="server" SelectedValue='<%# Bind("PartyCharged") %>'>
                        <asp:ListItem Value="" Text="" />
                        <asp:ListItem Text="BEN" Value="B" />
                        <asp:ListItem Text="BEN Charges for the Beneficiary" Value="BB" />
                        <asp:ListItem Text="Correspondent Charges for the Applicant" Value="AA" />
                        <asp:ListItem Text="Applicant" Value="A" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblPartyCharged" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Amort Charges</td>
                <td class="MyContent" colspan="2">
                    <asp:DropDownList
                        ID="rcbOmortCharge" runat="server" SelectedValue='<%# Bind("OmortCharges") %>'>
                        <asp:ListItem Value="" Text="" />
                        <asp:ListItem Value="YES" Text="YES" />
                        <asp:ListItem Value="NO" Text="NO" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Amt. In Local CCY</td>
                <td class="MyContent" colspan="2"></td>
            </tr>
            <tr>
                <td class="MyLable">Amt DR from Acct</td>
                <td class="MyContent" colspan="2"></td>
            </tr>
            <tr>
                <td class="MyLable">Charge Status</td>
                <td class="MyContent" style="width: 150px;">
                    <asp:DropDownList AutoPostBack="True"
                        OnSelectedIndexChanged="rcbChargeStatus_SelectIndexChange"
                        ID="rcbChargeStatus" runat="server" SelectedValue='<%# Bind("ChargeStatus") %>'>
                        <asp:ListItem Value="" Text="" />
                        <asp:ListItem Text="CHARGE COLECTED" Value="2" />
                        <asp:ListItem Text="CHARGE UNCOLECTED" Value="3" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblChargeStatus" runat="server" />
                </td>
            </tr>

        </table>
    </EditItemTemplate>
    <InsertItemTemplate>
        <table style="width: 100%">
    <tr>
        <td class="MyLable">Charge code</td>
        <td class="MyContent">
            <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True" Enabled="False"></asp:TextBox>
        </td>
    </tr>
            <tr>
                <td class="MyLable">Charge Acct</td>
                <td class="MyContent">
                    <asp:DropDownList AutoPostBack="true"
                        OnSelectedIndexChanged="rcbChargeAcct_SelectIndexChange"
                        ID="rcbChargeAcct" runat="server">
                        <asp:ListItem Value="" Text="" />
                        <asp:ListItem Text="CTY TNHH SONG HONG" Value="03.000237869.4" />
                        <asp:ListItem Text="CTY TNHH PHAT TRIEN PHAN MEM ABC" Value="03.000237870.4" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblChargeAcct" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Charge Period</td>
                <td class="MyContent" colspan="2">
                    <asp:TextBox ID="tbChargePeriod" Text="1" runat="server" Width="100" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Charge Ccy</td>
                <td class="MyContent" colspan="2">
                    <asp:DropDownList
                        ID="rcbChargeCcy" runat="server">
                        <asp:ListItem Value="" Text="" />
                        <asp:ListItem Value="USD" Text="USD" />
                        <asp:ListItem Value="EUR" Text="EUR" />
                        <asp:ListItem Value="GBP" Text="GBP" />
                        <asp:ListItem Value="JPY" Text="JPY" />
                        <asp:ListItem Value="VND" Text="VND" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Exch. Rate</td>
                <td class="MyContent" colspan="2">
                    <asp:TextBox runat="server" ID="tbExcheRate" Width="200px" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Charge Amt</td>
                <td class="MyContent" colspan="2">
                    <asp:TextBox runat="server" ID="tbChargeAmt" Width="200px" AutoPostBack="true"
                        OnTextChanged="tbChargeAmt_TextChanged" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Party Charged</td>
                <td class="MyContent" style="width: 150px;">
                    <asp:DropDownList AutoPostBack="true"
                        OnSelectedIndexChanged="rcbPartyCharged_SelectIndexChange"
                        ID="rcbPartyCharged" runat="server">
                        <asp:ListItem Value="" Text="" />
                        <asp:ListItem Text="BEN" Value="B" />
                        <asp:ListItem Text="BEN Charges for the Beneficiary" Value="BB" />
                        <asp:ListItem Text="Correspondent Charges for the Applicant" Value="AA" />
                        <asp:ListItem Text="Applicant" Value="A" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblPartyCharged" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Amort Charges</td>
                <td class="MyContent" colspan="2">
                    <asp:DropDownList
                        ID="rcbOmortCharge" runat="server">
                        <asp:ListItem Value="" Text="" />
                        <asp:ListItem Value="YES" Text="YES" />
                        <asp:ListItem Value="NO" Text="NO" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Amt. In Local CCY</td>
                <td class="MyContent" colspan="2"></td>
            </tr>
            <tr>
                <td class="MyLable">Amt DR from Acct</td>
                <td class="MyContent" colspan="2"></td>
            </tr>
            <tr>
                <td class="MyLable">Charge Status</td>
                <td class="MyContent" style="width: 150px;">
                    <asp:DropDownList AutoPostBack="true"
                        OnSelectedIndexChanged="rcbChargeStatus_SelectIndexChange"
                        ID="rcbChargeStatus" runat="server">
                        <asp:ListItem Value="" Text="" />
                        <asp:ListItem Text="CHARGE COLECTED" Value="2" />
                        <asp:ListItem Text="CHARGE UNCOLECTED" Value="3" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblChargeStatus" runat="server" />
                </td>
            </tr>

        </table>
    </InsertItemTemplate>
    <ItemTemplate>
        <table style="width: 100%">
    <tr>
        <td class="MyLable">Charge code</td>
        <td class="MyContent">
            <asp:TextBox ID="TextBox2" runat="server" ReadOnly="True" Enabled="False"></asp:TextBox>
        </td>
    </tr>
            <tr>
                <td class="MyLable">Charge Acct</td>
                <td class="MyContent">
                    <asp:DropDownList AutoPostBack="true"
                        OnSelectedIndexChanged="rcbChargeAcct_SelectIndexChange"
                        ID="rcbChargeAcct" runat="server">
                        <asp:ListItem Value="" Text="" />
                        <asp:ListItem Text="CTY TNHH SONG HONG" Value="03.000237869.4" />
                        <asp:ListItem Text="CTY TNHH PHAT TRIEN PHAN MEM ABC" Value="03.000237870.4" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblChargeAcct" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Charge Period</td>
                <td class="MyContent" colspan="2">
                    <asp:TextBox ID="tbChargePeriod" Text="1" runat="server" Width="100" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Charge Ccy</td>
                <td class="MyContent" colspan="2">
                    <asp:DropDownList
                        ID="rcbChargeCcy" runat="server">
                        <asp:ListItem Value="" Text="" />
                        <asp:ListItem Value="USD" Text="USD" />
                        <asp:ListItem Value="EUR" Text="EUR" />
                        <asp:ListItem Value="GBP" Text="GBP" />
                        <asp:ListItem Value="JPY" Text="JPY" />
                        <asp:ListItem Value="VND" Text="VND" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Exch. Rate</td>
                <td class="MyContent" colspan="2">
                    <asp:TextBox runat="server" ID="tbExcheRate" Width="200px" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Charge Amt</td>
                <td class="MyContent" colspan="2">
                    <asp:TextBox runat="server" ID="tbChargeAmt" Width="200px" AutoPostBack="true"
                        OnTextChanged="tbChargeAmt_TextChanged" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Party Charged</td>
                <td class="MyContent" style="width: 150px;">
                    <asp:DropDownList AutoPostBack="true"
                        OnSelectedIndexChanged="rcbPartyCharged_SelectIndexChange"
                        ID="rcbPartyCharged" runat="server">
                        <asp:ListItem Value="" Text="" />
                        <asp:ListItem Text="BEN" Value="B" />
                        <asp:ListItem Text="BEN Charges for the Beneficiary" Value="BB" />
                        <asp:ListItem Text="Correspondent Charges for the Applicant" Value="AA" />
                        <asp:ListItem Text="Applicant" Value="A" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblPartyCharged" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="MyLable">Amort Charges</td>
                <td class="MyContent" colspan="2">
                    <asp:DropDownList
                        ID="rcbOmortCharge" runat="server">
                        <asp:ListItem Value="" Text="" />
                        <asp:ListItem Value="YES" Text="YES" />
                        <asp:ListItem Value="NO" Text="NO" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Amt. In Local CCY</td>
                <td class="MyContent" colspan="2"></td>
            </tr>
            <tr>
                <td class="MyLable">Amt DR from Acct</td>
                <td class="MyContent" colspan="2"></td>
            </tr>
            <tr>
                <td class="MyLable">Charge Status</td>
                <td class="MyContent" style="width: 150px;">
                    <asp:DropDownList AutoPostBack="true"
                        OnSelectedIndexChanged="rcbChargeStatus_SelectIndexChange"
                        ID="rcbChargeStatus" runat="server">
                        <asp:ListItem Value="" Text="" />
                        <asp:ListItem Text="CHARGE COLECTED" Value="2" />
                        <asp:ListItem Text="CHARGE UNCOLECTED" Value="3" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblChargeStatus" runat="server" />
                </td>
            </tr>

        </table>

    </ItemTemplate>
</asp:FormView>


    </contenttemplate>
</asp:UpdatePanel>

<asp:SqlDataSource ID="SqlDataSourceChargeDetail" runat="server" ConnectionString="<%$ ConnectionStrings:VietVictoryCoreBanking %>" InsertCommand="sp_LC_ChargeDetail_Insert" InsertCommandType="StoredProcedure" SelectCommand="SELECT * FROM dbo.V_LC_ChargeDetail where NormalLCCode=@NormalLCCode" UpdateCommand="sp_LC_ChargeDetail_Update" UpdateCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter Name="NormalLCCode" ControlID="HiddenFieldLCCode" PropertyName="Value" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="NormalLCCode" Type="String" />
<asp:Parameter Name="NormalLCCode" Type="String"></asp:Parameter>
<asp:Parameter Name="WaiveCharges" Type="String"></asp:Parameter>
<asp:Parameter Name="Chargecode" Type="String"></asp:Parameter>
<asp:Parameter Name="ChargePeriod" Type="String"></asp:Parameter>
<asp:Parameter Name="ChargeAcct" Type="String"></asp:Parameter>
<asp:Parameter Name="ChargeCcy" Type="String"></asp:Parameter>
<asp:Parameter Name="ExchRate" Type="String"></asp:Parameter>
<asp:Parameter Name="ChargeAmt" Type="String"></asp:Parameter>
<asp:Parameter Name="PartyCharged" Type="String"></asp:Parameter>
<asp:Parameter Name="OmortCharges" Type="String"></asp:Parameter>
<asp:Parameter Name="AmtInLocalCCY" Type="String"></asp:Parameter>
<asp:Parameter Name="AmtDRfromAcct" Type="String"></asp:Parameter>
<asp:Parameter Name="ChargeStatus" Type="String"></asp:Parameter>
        <asp:Parameter Name="NormalLCCode" Type="String" />
        <asp:Parameter Name="WaiveCharges" Type="String" />
        <asp:Parameter Name="Chargecode" Type="String" />
        <asp:Parameter Name="ChargePeriod" Type="String" />
        <asp:Parameter Name="ChargeAcct" Type="String" />
        <asp:Parameter Name="ChargeCcy" Type="String" />
        <asp:Parameter Name="ExchRate" Type="String" />
        <asp:Parameter Name="ChargeAmt" Type="String" />
        <asp:Parameter Name="PartyCharged" Type="String" />
        <asp:Parameter Name="OmortCharges" Type="String" />
        <asp:Parameter Name="AmtInLocalCCY" Type="String" />
        <asp:Parameter Name="AmtDRfromAcct" Type="String" />
        <asp:Parameter Name="ChargeStatus" Type="String" />
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="NormalLCCode" Type="String" />
        <asp:Parameter Name="WaiveCharges" Type="String" />
        <asp:Parameter Name="Chargecode" Type="String" />
        <asp:Parameter Name="ChargePeriod" Type="String" />
        <asp:Parameter Name="ChargeAcct" Type="String" />
        <asp:Parameter Name="ChargeCcy" Type="String" />
        <asp:Parameter Name="ExchRate" Type="String" />
        <asp:Parameter Name="ChargeAmt" Type="String" />
        <asp:Parameter Name="PartyCharged" Type="String" />
        <asp:Parameter Name="OmortCharges" Type="String" />
        <asp:Parameter Name="AmtInLocalCCY" Type="String" />
        <asp:Parameter Name="AmtDRfromAcct" Type="String" />
        <asp:Parameter Name="ChargeStatus" Type="String" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="NormalLCCode" Type="String" />
        <asp:Parameter Name="WaiveCharges" Type="String" />
        <asp:Parameter Name="Chargecode" Type="String" />
        <asp:Parameter Name="ChargePeriod" Type="String" />
        <asp:Parameter Name="ChargeAcct" Type="String" />
        <asp:Parameter Name="ChargeCcy" Type="String" />
        <asp:Parameter Name="ExchRate" Type="String" />
        <asp:Parameter Name="ChargeAmt" Type="String" />
        <asp:Parameter Name="PartyCharged" Type="String" />
        <asp:Parameter Name="OmortCharges" Type="String" />
        <asp:Parameter Name="AmtInLocalCCY" Type="String" />
        <asp:Parameter Name="AmtDRfromAcct" Type="String" />
        <asp:Parameter Name="ChargeStatus" Type="String" />
    </UpdateParameters>
</asp:SqlDataSource>

<asp:HiddenField ID="HiddenFieldLCCode" runat="server" />
