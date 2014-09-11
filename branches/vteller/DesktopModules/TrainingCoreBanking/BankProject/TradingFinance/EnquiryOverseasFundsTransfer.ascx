<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnquiryOverseasFundsTransfer.ascx.cs" Inherits="BankProject.TradingFinance.EnquiryOverseasFundsTransfer" %>
<telerik:RadToolBar runat="server" ID="RadToolBar2" EnableRoundedCorners="true" EnableShadows="true" width="100%"
    OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
                ToolTip="Commit Data" Value="btSave" CommandName="save">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
                ToolTip="Preview" Value="btReview" CommandName="review">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
                ToolTip="Authorize" Value="btAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
                ToolTip="Revert" Value="btRevert" CommandName="revert">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
                ToolTip="Revert" Value="btSearch" CommandName="search">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>

<div style="padding:10px;">
    <table width="100%" cellpadding="0" cellspacing="0" style="margin-bottom:10px">
        <tr>
            <td style="width: 60px">TF No.</td>
            <td ><asp:TextBox ID="txtCode" runat="server" Width="355"/></td>
            <td style="width: 100px">&nbsp;Country Code&nbsp;</td>
            <td>
                <telerik:RadComboBox Width="355" AppendDataBoundItems="True"
                    ID="comboCountryCode" Runat="server" 
                    AutoPostBack="False"
                    MarkFirstMatch="True"
                    AllowCustomText="false" >
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">Transaction Type</td>
            <td >
                <telerik:RadComboBox width="355" 
                    DropDownCssClass="KDDL"  AppendDataBoundItems="True" 
                    ID="comboTransactionType" Runat="server" 
                    MarkFirstMatch="True"
                    AutoPostBack="True"
                    OnItemDataBound="comboTransactionType_ItemDataBound"
                    OnSelectedIndexChanged="comboTransactionType_OnSelectedIndexChanged"
                    AllowCustomText="false" >
                    <HeaderTemplate>
		                <table cellpadding="0" cellspacing="0"> 
		                    <tr> 
			                    <td style="width:100px;"> 
				                Id
			                    </td> 
			                    <td> 
				                Description
			                    </td>
		                    </tr> 
	                    </table> 
                    </HeaderTemplate>
	                <ItemTemplate>
			                <table  cellpadding="0" cellspacing="0"> 
			                    <tr> 
				                    <td style="width:100px;"> 
					                <%# DataBinder.Eval(Container.DataItem, "Id")%> 
				                    </td> 
				                    <td> 
					                <%# DataBinder.Eval(Container.DataItem, "Description")%> 
				                    </td>
			                    </tr> 
		                    </table> 
                    </ItemTemplate>
                </telerik:RadComboBox>
            </td>
            <td style="width: 100px">&nbsp;Commodity/Services&nbsp;</td>
            <td>
                <telerik:RadComboBox Width="355" AppendDataBoundItems="True"
                    ID="comboCommoditySer" Runat="server"
                    MarkFirstMatch="True"
                    AllowCustomText="false" >
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px; ">Customer ID</td>
            <td style="width: 355px;">
                <asp:TextBox ID="txtCusId" runat="server" Width="355"/>
            </td>
            <td style="width: 100px">&nbsp;Customer Name</td>
            <td><asp:TextBox ID="txtCusName" runat="server" Width="355"/></td>
        </tr>
    </table>
    
    <telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
        <MasterTableView DataKeyNames="OverseasTransferCode">
            <Columns>
                <telerik:GridBoundColumn HeaderText="LC Code" DataField="OverseasTransferCode"  />
                <telerik:GridBoundColumn HeaderText="Transaction Type" DataField="TransactionType" />
                <telerik:GridBoundColumn HeaderText="Commodity/Services" DataField="CommoditySer" />
                <telerik:GridBoundColumn HeaderText="Currency" DataField="DebitCurrency" />
                <telerik:GridBoundColumn HeaderText="Amount " DataField="DebitAmount" >
                    <ItemStyle Width="100" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Status" DataField="Status" >
                    <ItemStyle Width="100" HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# geturlReview(Eval("OverseasTransferCode").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
                    </itemtemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        $("#<%=txtCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btSearch.ClientID %>").click();
            }
        });
       
        $("#<%=txtCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=txtCusId.ClientID %>").click();
            }
        });
        
        $("#<%=txtCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=txtCusName.ClientID %>").click();
            }
        });
    </script>
</telerik:RadCodeBlock>
<div style="visibility:hidden;"><asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" /></div>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="comboTransactionType">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="comboCommoditySer" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>