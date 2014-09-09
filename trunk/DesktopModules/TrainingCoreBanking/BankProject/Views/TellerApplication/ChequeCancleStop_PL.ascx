<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChequeCancleStop_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.ChequeCancleStop_PL" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div style="padding:10px;">
    <telerik:RadGrid ID="RadGrid" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnNeedDataSource="RadGrid_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Account ID" DataField="AccountID" HeaderStyle-Width="110" />
                <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" HeaderStyle-Width="160" />
                <telerik:GridBoundColumn HeaderText="Doc ID" DataField="DocID" HeaderStyle-Width="160" />
                <telerik:GridBoundColumn HeaderText="Currency" DataField="Currency" HeaderStyle-Width="160" />
                <telerik:GridBoundColumn HeaderText="Serial No" DataField="SerialNo" HeaderStyle-Width="100" />
                <telerik:GridBoundColumn HeaderText="Cheque Type" DataField="ChequeType" HeaderStyle-Width="100" headerStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"/>
                <telerik:GridBoundColumn HeaderText="Status" DataField="Status" HeaderStyle-Width="110" />
                 <telerik:GridBoundColumn HeaderText="Active Date" DataField="ActiveDate"  />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25px" />
                    <ItemTemplate>
                        <a href='<%# getUrlPreview(Eval("AccountID").ToString(), Eval("SerialNo").ToString()) %>'><img src="Icons/bank/text_preview.png" width="20" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>

</div>