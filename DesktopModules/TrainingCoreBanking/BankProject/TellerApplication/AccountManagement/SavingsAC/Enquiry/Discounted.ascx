<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Discounted.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Enquiry.Discounted" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<style>
    .MyLable {
        width:95px
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
                <td class="MyLable"></td>
            <td class="MyContent" ></td>

                <td class="MyLable">Principal </td>
            <td class="MyContent"> from   <telerik:radnumerictextbox width="175px" runat="server" id="tbPrincipalFrom" minvalue="0" maxvalue="999999999999">
                            <NumberFormat GroupSeparator="," DecimalDigits="0" AllowRounding="true"   KeepNotRoundedValue="false"  />
                        </telerik:radnumerictextbox></td>
        </tr>
        <tr>           

            <td class="MyLable">Ref id </td>
            <td class="MyContent"><asp:TextBox ID="tbRefId" runat="server" /></td>

            <td class="MyLable">Working acc id</td>
            <td class="MyContent">
                <asp:TextBox ID="tbWrokingAccid" runat="server" />
            </td>            
            

            <td class="MyLable"> </td>
            <td class="MyContent">To &nbsp;&nbsp;&nbsp; <telerik:radnumerictextbox  width="175px" runat="server" id="tbPrincipalTo" minvalue="0" maxvalue="999999999999">
                            <NumberFormat GroupSeparator="," DecimalDigits="0" AllowRounding="true"   KeepNotRoundedValue="false"  />
                        </telerik:radnumerictextbox></td>

        </tr>

        <tr>
            <td class="MyLable"> LD id </td>
            <td class="MyContent">
                <asp:TextBox ID="LDid" runat="server" /></td>
            
            
            <td class="MyLable">Working acc name</td>
            <td class="MyContent">
                 <asp:TextBox ID="tbWorkingAccName" runat="server" />
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
                <telerik:GridBoundColumn HeaderText="Ld  Id " DataField="LDId " />
                <telerik:GridBoundColumn HeaderText="Starts " DataField="Status " />
                <telerik:GridBoundColumn HeaderText="Working acc id " DataField="TDWorkingAccountId" />
                <telerik:GridBoundColumn HeaderText="Working acc name" DataField="TDWorkingAccountName" />
                <telerik:GridBoundColumn HeaderText="Currency" DataField="TDCurrency" />
                <telerik:GridBoundColumn HeaderText="Principal" DataField="TDAmmount" 
                    DataType="System.Decimal" DataFormatString="{0:N}" />                
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# geturlReview(Eval("RefId").ToString()) %>'>
                            <img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> 
                        </a> 
                    </itemtemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:radgrid>
</div>
   