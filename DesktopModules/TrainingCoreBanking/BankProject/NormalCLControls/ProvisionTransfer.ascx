<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProvisionTransfer.ascx.cs" Inherits="BankProject.NormalCLControls.ProvisionTransfer" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true"  OnButtonClick="RadToolBar1_ButtonClick" EnableShadows="true" Width="100%" >
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png"  
            ToolTip="Commit Data" Value="btCommitData">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btPreview" CommandName="Preview">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="search">
        </telerik:RadToolBarButton>
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
<div style="padding:10px;">
     <table width="100%" cellpadding="0" cellspacing="0">
           <tr>
               <td class="MyLable">LC No.</td>
               <td class="MyContent">
                   <asp:TextBox ID="tbLCNo" runat="server" />
               </td>
               <td class="MyLable">Customer</td>
               <td class="MyContent">
                   <telerik:RadComboBox AppendDataBoundItems="True"  
                    ID="rcbApplicantID" Runat="server"
                    MarkFirstMatch="True" Width="355" Height="150px" 
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
               </td>
           </tr>
         </table>
<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Reperence No" DataField="ReperenceNo" />
            <telerik:GridBoundColumn HeaderText="LC Type" DataField="LCType" />
            <telerik:GridBoundColumn HeaderText="CCY" DataField="CcyAmount" />
            <telerik:GridBoundColumn HeaderText="Amount" DataField="SoTien" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("NormalLCCode").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid></div>