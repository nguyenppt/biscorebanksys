<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenNonRevolingCoomContr_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.OpenNonRevolingCoomContr_PL" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<div style="padding:10px;">
    <telerik:RadGrid ID="RadGridPreview" runat="server" AutoGenerateColumns="false"  AllowPaging="true" 
        OnNeedDataSource="RadGridPreview_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn  HeaderText="Non-Revolving Commitment Contract No"  DataField="NonRevComContNo" HeaderStyle-Width="200"/>
                <telerik:GridBoundColumn HeaderText="CustomerID" DataField="CustomerID" HeaderStyle-Width="240"
                     ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                 <telerik:GridBoundColumn HeaderStyle-Width ="150px" HeaderText="Commitment Amount" DataField="CommtAmt" 
                     ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"/>
                <telerik:GridDateTimeColumn HeaderText="StartDate" EditDataFormatString="dd/MM/yyyy" DataField="StartDate" 
                     DataType="System.DateTime"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# geturlPreview(Eval("LCCode").ToString()) %>'><img src="Icons/bank/text_preview.png" width="20" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>