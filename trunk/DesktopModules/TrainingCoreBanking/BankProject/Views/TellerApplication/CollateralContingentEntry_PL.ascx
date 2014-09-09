<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollateralContingentEntry_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.CollateralContingentEntry_PL" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<div style="padding:10px;">
    <telerik:RadGrid ID="RadGridPreview" runat="server" AutoGenerateColumns="false"  AllowPaging="true" 
        OnNeedDataSource="RadGridPreview_OnNeedDataSource" >
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn  HeaderText="Collateral Contingent No"  DataField="CollContEntry" HeaderStyle-Width="120"
                    ItemStyle-HorizontalAlign="left" />
                <telerik:GridBoundColumn HeaderText="Transaction Code" DataField="TransCode" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="150" />
                 <telerik:GridBoundColumn HeaderText="Amount LCY" DataField="AmtLCY" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderText="Debit or Credit" DataField="DC" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150"
                    HeaderStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25px" />
                    <ItemTemplate>
                        <a href='<%# getUrlPreview(Eval("LCCode").ToString()) %>'><img src="Icons/bank/text_preview.png" width="20" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>
