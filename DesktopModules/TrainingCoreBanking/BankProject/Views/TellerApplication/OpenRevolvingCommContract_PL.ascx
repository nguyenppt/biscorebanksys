<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenRevolvingCommContract_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.OpenRevolvingCommContract_PL" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<div style="padding:10px;">
    <telerik:RadGrid runat="server" AutoGenerateColumns="false" ID="RadGridPreview" AllowPaging="true" 
        OnNeedDataSource="RadGridPreview_OnNeedDataSource" >
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Revolving Commitment Contract No" DataField="RevComContNo" HeaderStyle-Width="200" />
                 <telerik:GridBoundColumn HeaderText="CustomerID" DataField="CustomerID" HeaderStyle-Width="240"
                     ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"            />
                 <telerik:GridBoundColumn HeaderText="Commitment Amount" DataField="CommtAmt"  
                      HeaderStyle-Width ="150px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderText="StartDate" DataField="StartDate" 
                     ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
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