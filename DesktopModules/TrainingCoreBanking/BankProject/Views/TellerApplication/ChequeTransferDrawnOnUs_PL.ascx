<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChequeTransferDrawnOnUs_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.WebUserControl1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" TagPrefix="dnn" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<div style="padding:10px">
    <telerik:RadGrid ID="RadGrid1" runat="server"  AutoGenerateColumns="false" AllowPaging="true" OnNeedDataSource="RadGrid1_OnNeedDataSource" >
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn  HeaderText="Reference ID"  DataField="ID" />
                <telerik:GridBoundColumn HeaderText="Customer Name" DataField="CustomerName" HeaderStyle-Width="130"  />
                 <telerik:GridBoundColumn HeaderText="Amount Debit" DataField="DebitAmount" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" 
                     HeaderStyle-Width="150"  />
                 <telerik:GridBoundColumn HeaderText="Debit Currency" DataField="DebitCurrency" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="100"  />
                 <telerik:GridBoundColumn HeaderText="Cheque No" DataField="ChequeNo"  HeaderStyle-Width="100"   />
                <telerik:GridBoundColumn HeaderText="Beneficiary Name" DataField="BeneficialName" HeaderStyle-Width="130"  />
                <telerik:GridBoundColumn HeaderText="Amount Transfer" DataField="AmtCreditForCust" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" 
                     HeaderStyle-Width="150"  />
                 <telerik:GridBoundColumn HeaderText="Credit Currency" DataField="CreditCurrency" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center"  HeaderStyle-Width="100"   />

                <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
                <telerik:GridBoundColumn HeaderText="Transfer Date" DataField="CreatedDate"  HeaderStyle-Width="100"   />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# getUrlPreview(Eval("ID").ToString()) %>'><img src="Icons/bank/text_preview.png" width="20" /></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>