<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArrearandPeriodic.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Enquiry.ArrearandPeriodic" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<style>
    .MyLable {
        width:75px
    }
    .MyContent {
        padding-right:20px
    }
    .MyContent input {
        width:200px
    }
    .cssProductLine:hover {
        background-color:#CCC;
    }
</style>
<telerik:radtoolbar runat="server" id="RadToolBar1"
    enableroundedcorners="true" enableshadows="true"  width="100%" onbuttonclick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit" Enabled ="false"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit">
        </telerik:RadToolBarButton>
        <%--<telerik:RadToolBarButton ImageUrl="~/Icons/bank/doc_new_icon.png"
            ToolTip="Clear" Value="btClear"  CommandName="clear">
        </telerik:RadToolBarButton>--%>
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png" Enabled ="false"
            ToolTip="Preview" Value="btPreview" CommandName="preview">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png" Enabled ="false"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png" Enabled ="false"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse">
        </telerik:RadToolBarButton>   
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="search">
        </telerik:RadToolBarButton>        
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png" Enabled ="false"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print">
        </telerik:RadToolBarButton>
    </Items>
</telerik:radtoolbar>
 
<div style="padding: 10px;">
    <fieldset>
    <table width="100%" cellpadding="0" cellspacing="0" id="data">
        <tr>
            <td class="MyLable">Type</td>
            <td class="MyContent" >
                <telerik:radcombobox appenddatabounditems="true" width="205px" 
                            id="rcbtype" runat="server"
                            markfirstmatch="True" height="50px"
                            allowcustomtext="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None"/>
                        <Items>
                            <telerik:RadComboBoxItem Value="Arrear" Text="Arrear" />
                            <telerik:RadComboBoxItem Value="Periodic" Text="Periodic" />
                        </Items>
                </telerik:radcombobox>
            </td>        
            <td class="MyLable">Status</td>
            <td class="MyContent" >
                <telerik:radcombobox appenddatabounditems="true" width="205px" 
                            id="rcbStatus" runat="server"
                            markfirstmatch="True" height="150px"
                            allowcustomtext="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None"/>
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="UNA" Text="UNA" />
                            <telerik:RadComboBoxItem Value="AUT" Text="AUT" />
                            <telerik:RadComboBoxItem Value="REV" Text="REV" />
                        </Items>
                </telerik:radcombobox>
            </td>
            <td class="MyLable">Principal </td>
            <td class="MyContent"> from   <telerik:radnumerictextbox width="175px" runat="server" id="tbPrincipalFrom" minvalue="0" maxvalue="999999999999">
                            <NumberFormat GroupSeparator="," DecimalDigits="0" AllowRounding="true"   KeepNotRoundedValue="false"  />
                        </telerik:radnumerictextbox></td>

        
        </tr>
        <tr>           

            <td class="MyLable">Ref id </td>
            <td class="MyContent"><asp:TextBox ID="tbRefId" runat="server" /></td>

            <td class="MyLable">Category</td>
            <td class="MyContent">
                <telerik:radcombobox appenddatabounditems="true" width="205px" 
                            id="rcbCategory" runat="server"
                            markfirstmatch="True" height="150px"
                            allowcustomtext="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None"/>
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                </telerik:radcombobox>
            </td>
            

            <td class="MyLable"> </td>
            <td class="MyContent">To &nbsp;&nbsp;&nbsp; <telerik:radnumerictextbox  width="175px" runat="server" id="tbPrincipalTo" minvalue="0" maxvalue="999999999999">
                            <NumberFormat GroupSeparator="," DecimalDigits="0" AllowRounding="true"   KeepNotRoundedValue="false"  />
                        </telerik:radnumerictextbox></td>

        </tr>

        <tr>
            <td class="MyLable">Customer ID </td>
            <td class="MyContent">
                <telerik:radcombobox appenddatabounditems="True"
                        id="rcbCustomerID" runat="server"
                        width="205px"
                        markfirstmatch="True" height="150px"                      
                        allowcustomtext="false">
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:radcombobox>

            </td>
            
            <td class="MyLable">Product Line </td>
            <td class="MyContent">
                <telerik:radcombobox appenddatabounditems="True" width="205px" dropdowncssclass="KDDL"
                        id="rcbProductLine" runat="server"
                        markfirstmatch="True" height="150px"
                        allowcustomtext="false">
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                    <HeaderTemplate>
                        <table style="width:205px" cellpadding="0" cellspacing="0"> 
                            <tr> 
                                <td style="width:60px;padding-bottom:3px;border-bottom:1px solid #CCC;"> 
                                Product 
                                </td> 
                                <td style="width:145px;padding-bottom:3px;border-bottom:1px solid #CCC"> 
                                Description
                                </td> 
                            </tr> 
                        </table> 
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="width: auto" cellpadding="0" cellspacing="0">
                            <tr class="cssProductLine">
                                <td style="width: 63px;height:30px;">
                                    <%# DataBinder.Eval(Container.DataItem, "ProductID")%> 
                                </td>
                                <td style="width: 142px;height:30px;padding-left:3px;line-height:18px">
                                    <%# DataBinder.Eval(Container.DataItem, "Description")%> 
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:radcombobox>
            </td>

            <td class="MyLable">Currency </td>
            <td class="MyContent">
                <telerik:radcombobox appenddatabounditems="true" width="205px" 
                            id="rcbCurrentcy" runat="server"
                            markfirstmatch="True" height="150px"
                            allowcustomtext="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None"/>
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                </telerik:radcombobox>
            </td>
           
        </tr>
    </table>
</fieldset>
    <telerik:radgrid runat="server" autogeneratecolumns="False"
         id="radGridReview" allowpaging="true" onneeddatasource="radGridReview_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Ref Id" DataField="RefId" />
                <telerik:GridBoundColumn HeaderText="Starts " DataField="Status " />
                <telerik:GridBoundColumn HeaderText="Customer ID " DataField="CustomerId" />
                <telerik:GridBoundColumn HeaderText="Category " DataField="AccCategory" />
                <telerik:GridBoundColumn HeaderText="CCY" DataField="Currency" />
                <telerik:GridBoundColumn HeaderText="Product Line" DataField="ProductLineId" />
                <telerik:GridBoundColumn HeaderText="Principal" DataField="AZPrincipal" 
                    DataType="System.Decimal" DataFormatString="{0:N}" />                
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# GeturlReview(Eval("RefId").ToString(),Eval("FromTable").ToString()) %>'>
                            <img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> 
                        </a> 
                    </itemtemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:radgrid>
</div>
