<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnquiryDocumentaryCollection.ascx.cs" Inherits="BankProject.TradingFinance.Import.DocumentaryCollections.EnquiryDocumentaryCollection" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

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
            <td style="width: 60px">REF No.</td>
            <td ><asp:TextBox ID="txtCode" runat="server" Width="200"/></td>
        </tr>
        <tr>
            <td style="width: 60px">Drawee</td>
            <td width="355">
                <telerik:RadComboBox width="355" DropDownCssClass="KDDL"
                    AppendDataBoundItems="True"
                    ID="comboDraweeCusNo" Runat="server"
                    OnItemDataBound="comboDraweeCusNo_ItemDataBound" 
                    MarkFirstMatch="True" Height="150px" 
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <HeaderTemplate>
		                <table cellpadding="0" cellspacing="0"> 
		                    <tr> 
			                    <td style="width:100px;"> 
				                Customer Id
			                    </td> 
			                    <td> 
				                Customer Name
			                    </td>
		                    </tr> 
	                    </table> 
                    </HeaderTemplate>
	                <ItemTemplate>
			            <table  cellpadding="0" cellspacing="0"> 
			                <tr> 
				                <td style="width:100px;"> 
					            <%# DataBinder.Eval(Container.DataItem, "CustomerID")%> 
				                </td> 
				                <td> 
					            <%# DataBinder.Eval(Container.DataItem, "CustomerName2")%> 
				                </td>
			                </tr> 
		                </table> 
                    </ItemTemplate>
                </telerik:RadComboBox>
            </td>
            <td style="width: 90px">&nbsp; Drawee Addr.</td>
            <td>
                <asp:TextBox ID="txtDraweeAddr" runat="server" Width="355"/>
            </td>
        </tr>
        <tr>
            <td style="width: 60px; ">Drawer</td>
            <td style="width: 355px;">
                <telerik:RadTextBox width="355"
                    ID="comboDrawerCusNo" Runat="server"                     >
                </telerik:RadTextBox>
            </td>
            <td style="width: 90px">&nbsp; Drawer Addr.</td>
            <td><asp:TextBox ID="txtDrawerAddr" runat="server" Width="355"/></td>
        </tr>
    </table>
    
    <telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
        <MasterTableView DataKeyNames="Id">
            <Columns>
                <telerik:GridBoundColumn HeaderText="Documetary Collection Code" DataField="DocCollectCode"  />
                <telerik:GridBoundColumn HeaderText="Collection Type" DataField="CollectionType" />
                <telerik:GridBoundColumn HeaderText="CCY" DataField="Currency" >
                    <ItemStyle Width="100" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Amount" DataField="Amount" >
                    <ItemStyle Width="100" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Status" DataField="Status" >
                    <ItemStyle Width="100" HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="25" />
                    <ItemTemplate>
                        <a href='<%# geturlReview(Eval("DocCollectCode").ToString()) %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a> 
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

        $("#<%=comboDraweeCusNo.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btSearch.ClientID %>").click();
            }
        });

        $("#<%=txtDraweeAddr.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btSearch.ClientID %>").click();
            }
        });

        $("#<%=comboDrawerCusNo.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btSearch.ClientID %>").click();
            }
        });

        $("#<%=txtDrawerAddr.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btSearch.ClientID %>").click();
            }
        });
    </script>
</telerik:RadCodeBlock>
<div style="visibility:hidden;"><asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" /></div>
