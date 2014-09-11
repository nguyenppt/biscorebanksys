<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalaryPaymentEnquiry.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.CurrentNonTermSavingAC.SalaryPayment.SalaryPaymentEnquiry" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style>
    .MyLable {
        width:80px
    }
    .MyContent {
        padding-right:20px
    }
    .MyContent input {
        width:180px
    }
    .cssProductLine:hover {
        background-color:#CCC;
    }
</style>

 <telerik:RadToolBar runat="server" ID="RadToolBar1"  EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit" 
            ToolTip="Commit Data" Value="btnCommit" CommandName="commit" Enabled ="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btnPreview" CommandName="Preview" Enabled ="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btnAuthorize" CommandName="authorize" Enabled ="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btnReverse" CommandName="reverse" Enabled ="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btnSearch" CommandName="search" >
        </telerik:RadToolBarButton>
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btnPrint" CommandName="print" Enabled ="false">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
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
                            <telerik:RadComboBoxItem Value="Frequency" Text="Frequency" />
                            <telerik:RadComboBoxItem Value="Non-Frequency" Text="Non-Frequency" />
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
           
              <td class="MyLable">Total Debit Amt</td>
              <td class="MyContent"> from   <telerik:radnumerictextbox width="175px" runat="server" id="tbFrom" minvalue="0" maxvalue="999999999999">
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
           
           <td class="MyLable"> </td>
            <td class="MyContent">To &nbsp;&nbsp;&nbsp; <telerik:radnumerictextbox  width="175px" runat="server" id="tbTo" minvalue="0" maxvalue="999999999999">
                            <NumberFormat GroupSeparator="," DecimalDigits="0" AllowRounding="true"   KeepNotRoundedValue="false"  />
                        </telerik:radnumerictextbox></td>
        </tr>
    </table>
</fieldset>
<div style="padding: 10px;">
    <telerik:radgrid runat="server" autogeneratecolumns="False"
         id="radGridReview" allowpaging="false" onneeddatasource="radGridReview_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Ref Id" DataField="RefId" />
                <telerik:GridBoundColumn HeaderText="Fequency" DataField="Fequency" />
                <telerik:GridBoundColumn HeaderText="Term" DataField="Term" />
                <telerik:GridBoundColumn HeaderText="End Date" DataField="EndDate" />
                <telerik:GridBoundColumn HeaderText="Currency" DataField="Currency" />
                <telerik:GridBoundColumn HeaderText="Total Debit Amt" DataField="TotalDebitAmt" 
                    DataType="System.Decimal" DataFormatString="{0:N}" />
                
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# GeturlReview(Eval("RefId").ToString()) %>'>
                            <img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> 
                        </a> 
                    </itemtemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:radgrid>
</div>