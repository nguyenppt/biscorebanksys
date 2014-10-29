<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OutwardTransferByCash_Enquiry.ascx.cs" Inherits="BankProject.FTTeller.OutwardTransferByCash_Enquiry" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>
<%@ register Assembly="Telerik.Web.Ui" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadWindowManager id="RadWindowManager2" runat="server"  EnableShadow="true" />
<telerik:RadToolBar runat="server" ID="RadToolBar2" EnableRoundedCorners="true" EnableShadows="true" width="100%" OnbuttonClick="radtoolbar2_onbuttonclick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
                ToolTip="Commit Data" Value="btCommit" CommandName="commit">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
                ToolTip="Preview" Value="btPreview" CommandName="review">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
                ToolTip="Authorize" Value="btAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
                ToolTip="Revert" Value="btReverse" CommandName="revert">
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
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="MyLable">Product ID:</td>
            <td class="MyContent" width="320">
                <telerik:RadComboBox ID="rcbProductID" runat="server" AllowCustomText="false" 
                             MarkFirstMatch="true" ValidationGroup="Group1"
                    onSelectedIndexChanged="rcbPaymentType_onSelectedIndexChanged" AutoPostBack="True"
                             AppendDataBoundItems="true" width="270" >
                    <Items>
                       <telerik:RadComboBoxItem Value="" Text="" />
                       <telerik:RadComboBoxItem Value="1000" Text="1000 - Điện CMND" />
                       <telerik:RadComboBoxItem Value="3000" Text="3000 - Chuyển đi thanh toán CI-TAD" />
                    </Items>
                        </telerik:RadComboBox>
            </td>
            <td class="MyLable">Transaction Type</td>
            <td class="MyContent">
                <telerik:RadComboBox AppendDataBoundItems="True"                                     
                                    ID="rcbTransactionType" runat="server"
                                    MarkFirstMatch="True" 
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" /> 
                                    <Items>
                                        <telerik:RadComboBoxItem Value="Cash" Text="Transfer by Cash" />
                                        <telerik:RadComboBoxItem Value="Account" Text="Transfer by Account" />
                                    </Items>
                                </telerik:RadComboBox>
            </td>
        </tr>
        </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="MyLable">Sending Name:</td>
             <td class="MyContent" width="320">   <telerik:radtextbox runat="server" id="tbSendingName" ValidationGroup="Group1"  width="270"/>
            </td>
            <td class="MyLable">Receving Legal ID:</td>
            <td class="MyContent">
                 <telerik:radTextBox id="tbLegalID" runat="server"  width="340" />
            </td>
        </tr>
        <tr>
            <td class="MyLable">ReceivingName</td> 
            <td class="MyContent">
                <telerik:RadTextBox id="tbReceivingName" runat="server"  width="270"/>
            </td>
            <td class="MyLable">Ben Account</td>
            <td class="MyContent">
                 <telerik:RadTextBox id="tbBenAccount" runat="server"  width="340"/>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="MyLable">Reference ID:</td>
            <td class="MyContent" >
                <telerik:radtextbox  id="tbID" runat="server" validationGroup="Group1" ></telerik:radtextbox>
            </td>
            <td class="MyLable" >Ben Com:</td>
            <td class="MyContent" >
                 <telerik:RadComboBox ID="rcbBenCom" runat="server" AllowCustomText="false" 
                             MarkFirstMatch="true" ValidationGroup="Group1"
                             AppendDataBoundItems="true" >
                        </telerik:RadComboBox>
            </td>
            <td class="MyLable">Currency:</td>
            <td class="MyContent">
               <telerik:RadComboBox ID="rcbCurrency" runat="server" AllowCustomText="false" 
                             MarkFirstMatch="true" ValidationGroup="Group1"
                             AppendDataBoundItems="true" >
                        </telerik:RadComboBox>
            </td>
        </tr> </table>
    
        
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
            <td class="MyLable">Amount:</td>
             <td class="MyContent" style="visibility:hidden;">
                <telerik:radTextBox id="RadTextBox2" runat="server" />
            </td>
            <td class="MyLable">From:</td>
            <td class="MyContent">
                <telerik:radnumericTextBox id="tbFromAmt"  runat="server" />
            </td>
            <td class="MyLable">To:</td>
            <td class="MyContent">
                <telerik:radnumericTextBox id="tbToAmt"  runat="server"  />
            </td>
        </tr>
        </table>
</div>
<div>
    <telerik:RadGrid id="RadGridView" runat="server" AllowPaging="true" AutoGenerateColumns="false" 
        OnNeedDataSource="RadGrid1_OnNeedDataSource" >
        <MasterTableView>
            <Columns>
            <telerik:GridBoundColumn HeaderText="ID" DataField="ID" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="Center"  />
            <telerik:GridBoundColumn HeaderText="Product Name" DataField="ProductName" HeaderStyle-Width="100" />
            <telerik:GridBoundColumn HeaderText="Sending Name" DataField="SendingName" HeaderStyle-Width="110" />
            <telerik:GridBoundColumn HeaderText="Receiving  Name" DataField="ReceivingName" HeaderStyle-Width="110" />
            <telerik:GridBoundColumn HeaderText="Currency" DataField="Currency" HeaderStyle-Width="50" />

            <telerik:GridBoundColumn HeaderText="Amount" DataField="Amount" ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="right"   HeaderStyle-Width="100" DataType="System.Decimal"  DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Charge Amt" DataField="ChargeAmount" ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="right"  HeaderStyle-Width="100"  DataType="System.Decimal"  DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Charge Vat Amt" DataField="VATChargeAmount" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="right" HeaderStyle-Width="100" DataType="System.Decimal"  DataFormatString="{0:N}" />
            <telerik:GridBoundColumn HeaderText="Status" DataField="Status" HeaderStyle-Width="10%"  ItemStyle-HorizontalAlign="center"  HeaderStyle-HorizontalAlign="center"/>
            <telerik:GridTemplateColumn>
                <ItemStyle Width="25" />
                <ItemTemplate>
                    <a href='<%# geturlReview(Eval("ID").ToString(),Eval("TransactionType").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                </itemtemplate>
            </telerik:GridTemplateColumn>
        </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbProductID">   
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="tbReceivingName" />
                 <telerik:AjaxUpdatedControl ControlID="tbBenAccount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>