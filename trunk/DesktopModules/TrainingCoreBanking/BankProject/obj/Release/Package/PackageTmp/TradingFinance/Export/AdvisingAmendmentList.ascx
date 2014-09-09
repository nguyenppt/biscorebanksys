<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisingAmendmentList.ascx.cs" Inherits="BankProject.AdvisingAmendmentList" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<div style="padding:10px;">
   <table width="100%" cellpadding="0" cellspacing="0">
<tr>
    <td class="MyContent" style="width:20%;"><asp:TextBox ID="tbEssurLCCode" runat="server" Width="100%" /></td>
    <td class="MyLable" style="width:2%;"></td>
    <td class="MyLable" style="width:5%;">LC Type</td>
    <td class="MyContent" style="width:28%;">
                         <telerik:RadComboBox  AppendDataBoundItems="True" DropDownCssClass="KDDL"
                        ID="rcbLCType" Runat="server"  width="100%"
                        MarkFirstMatch="True"  OnItemDataBound="rcbLCType_ItemDataBound"
                        AllowCustomText="false" >
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <HeaderTemplate>
                            <table style="width:305px" cellpadding="0" cellspacing="0"> 
                              <tr> 
                                 <td style="width:60px;"> 
                                    LC Type 
                                 </td> 
                                 <td style="width:200px;"> 
                                    Description
                                 </td> 
                                 <td > 
                                    Category
                                 </td> 
                              </tr> 
                           </table> 
                       </HeaderTemplate>
                        <ItemTemplate>
                        <table style="width:305px"  cellpadding="0" cellspacing="0"> 
                          <tr> 
                             <td style="width:60px;"> 
                                <%# DataBinder.Eval(Container.DataItem, "LCTYPE")%> 
                             </td> 
                             <td style="width:200px;"> 
                                <%# DataBinder.Eval(Container.DataItem, "Description")%> 
                             </td> 
                             <td > 
                                <%# DataBinder.Eval(Container.DataItem, "Category")%> 
                             </td> 
                          </tr> 
                       </table> 
                    </ItemTemplate>
                    </telerik:RadComboBox>
                    </td>

     <td class="MyLable" style="width:2%;"></td>
    <td class="MyLable" style="width:13%;">Beneficiary Cust.No</td>
    <td class="MyContent" style="width:28%;">
                        <telerik:RadComboBox AppendDataBoundItems="True" 
                    ID="rcbBeneficiaryCustNo" Runat="server"
                    MarkFirstMatch="True" Width="100%" Height="150px" 
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox>
                    </td>
    <td style="width:2%;">
        <telerik:RadToolBar runat="server" ID="RadToolBar1" Style="z-index: -1;" 
    EnableRoundedCorners="false" AutoPostBack="true" EnableShadows="false" width="100%" Height="30" OnButtonClick="RadToolBar1_ButtonClick" >
        <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview1.png"
            ToolTip="Preview" Value="btdocnew" CommandName="docnew" >
        </telerik:RadToolBarButton>
        </Items>
    </telerik:RadToolBar>  
    </td>
</tr>
</table>

<telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn HeaderText="Reperence No" DataField="ReperenceNo" />
            <telerik:GridBoundColumn HeaderText="LC Type" DataField="LCType" />
            <telerik:GridBoundColumn HeaderText="LC Number" DataField="LCNumber" />
            <telerik:GridBoundColumn HeaderText="Beneficiary Cust.No" DataField="BeneficiaryCustName" />
            <telerik:GridBoundColumn HeaderText="Issuing Bank No" DataField="IssuingBankNo" />
            <telerik:GridBoundColumn HeaderText="Reimb. Bank No." DataField="ReimbBankNo" />
            <telerik:GridBoundColumn HeaderText="Amount" DataField="Amount" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("AdvisingLCCode").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
</div>