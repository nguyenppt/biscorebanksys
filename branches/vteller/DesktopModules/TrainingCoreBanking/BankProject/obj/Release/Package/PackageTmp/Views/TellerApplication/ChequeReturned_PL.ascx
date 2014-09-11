<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChequeReturned_PL.ascx.cs" Inherits="BankProject.Views.TellerApplication.ChequeReturned_PL" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" TagPrefix="dnn" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<div style="padding:10px">
    <telerik:RadGrid ID="RadGrid" runat="server" AllowPaging="true" AutoGenerateColumns="false"  OnNeedDataSource="RadGrid_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Reference Cheque" DataField="RefCheque" HeaderStyle-Width="100" />
                 <telerik:GridBoundColumn HeaderText="Cheque Nos" DataField="ChequeNos" HeaderStyle-Width="100" 
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                 <telerik:GridBoundColumn HeaderText="Total Issued" DataField="TotalIssued" HeaderStyle-Width="70"
                      HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  />
                 <telerik:GridBoundColumn HeaderText="Total Used" DataField="TotalUsed" HeaderStyle-Width="70" 
                      HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                 <telerik:GridBoundColumn HeaderText="Total Held" DataField="TotalHeld" HeaderStyle-Width="70"
                      HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  />
                 <telerik:GridBoundColumn HeaderText="Total Stopped" DataField="TotalStopped" HeaderStyle-Width="70" 
                      HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                 <telerik:GridBoundColumn HeaderText="Returned Cheques" DataField="ReturnedCheque" HeaderStyle-Width="100" 
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                 <telerik:GridBoundColumn HeaderText="Created Date" DataField="CreatedDate" HeaderStyle-Width="100" />
                 <telerik:GridBoundColumn HeaderText="Status" DataField="Status" HeaderStyle-Width="50" />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25px" />
                    <ItemTemplate>
                        <a href='<%# getUrlPreview(Eval("RefCheque").ToString(), Eval("ReturnedCheque").ToString()) %>'><img src="Icons/bank/text_preview.png" width="20" /></a>
                        
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>
