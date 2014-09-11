<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InputCollateralInformation_PL.ascx.cs" Inherits="BankProject.InputCollateralInformation_PL" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik"%>

<div style="padding:10px;">
    <telerik:RadGrid ID="RadGridPreview" runat="server" AutoGenerateColumns="false"  AllowPaging="true" 
        OnNeedDataSource="RadGridPreview_OnNeedDataSource" >
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn  HeaderText="Collateral Information No" HeaderStyle-Width="150" DataField="CollInfoNo" />
                <telerik:GridBoundColumn HeaderText="Collateral Type" DataField="CollType" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="300" />
                 <telerik:GridBoundColumn HeaderText="Collateral Status" DataField="CollStatus" HeaderStyle-Width="230" />
                <telerik:GridBoundColumn HeaderText="Execution Value" DataField="ExecValue" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="100" />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# getUrlPreview(Eval("LCCode").ToString()) %>'><img src="Icons/bank/text_preview.png" width="20" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>