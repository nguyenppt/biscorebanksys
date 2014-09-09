<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenLoanWorkingAccount.ascx.cs" Inherits="BankProject.Views.TellerApplication.OpenLoanWorkingAccount" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" />

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
   
    function OnValueChanged_GetAccountName(sender, args) {
        var CustomerName = $find("<%=rcbCustomerID.ClientID%>").get_selectedItem().get_text();
        var Currency = $find("<%=rcbCurrency.ClientID%>").get_selectedItem().get_value();
        var AccountName = $find("<%=txtAccountName.ClientID%>");
        var ShortTittle = $find("<%=tbShortTitle.ClientID%>");
        if (CustomerName && Currency) {
            AccountName.set_value("TKTV-" + CustomerName.substr(10, CustomerName.length - 1) + "-" + Currency);
            ShortTittle.set_value("TKTV-" + CustomerName.substr(10, CustomerName.length - 1) + "-" + Currency);
        }
    }
</script>
    </telerik:RadCodeBlock>
<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click1" Text="Search" />
</div>
<div>
   <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit">
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
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/edit.png" 
            ToolTip="Edit Data" Value="btEdit" CommandName="edit" />
    </Items>
</telerik:RadToolBar>
    </div>
<div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:150px ;padding:5px 0 5px 20px;">
                <asp:TextBox Width="150" ID="tbID" runat="server" text="06.000265047.1"/>
                <i>
                    <asp:Label ID="lblID" runat="server"></asp:Label>
                </i>

            </td>
        </tr>
    </table>
</div>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#blank1">Current Account</a></li>
    </ul>
    <div id="blank1" class="dnnClear"> 
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">CustomerID:<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator1"
                        ControlToValidate="rcbCustomerID" ValidationGroup="Commit" InitialValue="" ErrorMessage="CustomerID is required"
                        ForeColor="Red"  ></asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="350">
                                    <telerik:RadComboBox AppendDataBoundItems="True" OnClientSelectedIndexChanged="OnValueChanged_GetAccountName"                                
                                        ID="rcbCustomerID" runat="server" OnItemDataBound="rcbCustomerID_OnItemDataBound"
                                        MarkFirstMatch="True" Width="350" Height="150px"
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
                </td>            
            </tr>
            <tr>
                <td class="MyLable">Category:<span class="Required">(*)</span>
                     <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator2" 
                        ControlToValidate="rcbCategory" ValidationGroup="Commit" InitialValue="" ErrorMessage="Category is required"
                        ForeColor="Red"></asp:RequiredFieldValidator>                 
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbCategory" runat="server" MarkFirstMatch="true" Width="350" AllowCustomtext="false" Height="150px"
                        appendDataboundItems ="true"   >
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />                
                    <Items>
                        <telerik:RadComboBoxItem value="" text="" />
                    </Items>
                        </telerik:RadComboBox>
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
        
            <tr>
                <td class="MyLable">Currency:<span class="Required">(*)</span>
                     <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator3"
                        ControlToValidate="rcbCurrency" ValidationGroup="Commit" InitialValue="" ErrorMessage="Currency is required" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox id="rcbCurrency" width="150" runat="server"  appenddatabounditems="true" Height="150px"
                    MarkFirstMatch="true" AllowCustomText="false" OnClientSelectedIndexChanged="OnValueChanged_GetAccountName"
                      >
                <ExpandAnimation type="None" />
                <CollapseAnimation type="None" />
                <Items>
                    <telerik:RadComboBoxItem value="" text="" />
                </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Account Name:</td>
                <td class="MyContent">
                    <telerik:RadTextBox id="txtAccountName" width="350" runat="server" ValidationGroup="Group1" />
                </td>
                <td class="MyContent"><i><asp:Label ID="lblNote" runat="server" /> </i></td>
            
            </tr>
            <tr>
                <td class="MyLable">Short Title:</td>
                <td class="MyContent">
                    <telerik:RadTextBox id="tbShortTitle" width="350" runat="server" ValidationGroup="Group1" />
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
            </table>
         <hr />
        <table style="visibility:hidden;">
            <tr>
                <td class="MyLable">Product Line:<span class="Required">(*)  </span>
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox id="rcbProductLine" width="350" runat="server" appendDataboundItems="true" height="150px"
                    MarkFirstMatch="true" AllowCustomText="false" >
                <ExpandAnimation type="None" />
                <CollapseAnimation type="None" />
                <Items>
                    <telerik:RadComboBoxItem value="" text="" />
                </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Mnemonic:</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox id="tbMnemonic" width="350" runat="server" ValidationGroup="Group1" />
                </td>
            
            </tr>
            <tr>
                <td class="MyLable">Alternate Acct:</td>
                <td class="MyContent" width="350">
                    <telerik:RadTextBox ID="tbAlternateAcct" width="150" runat="server" ValidationGroup="Group1"></telerik:RadTextBox>
                </td>
            </tr>
        </table>
       
        <table width="100%" cellpadding="0" cellspacing="0" style="visibility:hidden;">
            <tr>
                <td class="MyLable">Override:</td>
                <td class="MyContent"></td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
            <tr>
                <td class="MyLable">Record Status:</td>
                <td class="MyContent"></td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
            <tr>
                <td class="MyLable">Current No:</td>
                <td class="MyContent"></td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
             <tr>
                <td class="MyLable">Inputter:</td>
                <td class="MyContent"></td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
             <tr>
                <td class="MyLable">Date.Time:</td>
                <td class="MyContent"></td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
             <tr>
                <td class="MyLable">Authoriser:</td>
                <td class="MyContent"></td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
             <tr>
                <td class="MyLable">Company:</td>
                <td class="MyContent"></td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
             <tr>
                <td class="MyLable">Department:</td>
                <td class="MyContent"></td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
             <tr>
                <td class="MyLable">Auditor Code:</td>
                <td class="MyContent"></td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
             <tr>
                <td class="MyLable">Audit Date & Time:</td>
                <td class="MyContent"></td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
            </table>
    </div>
</div>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"
    DefaultLoadingPanelID="AjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbCustomerID">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblCustomer" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
<script type="text/javascript">
    $("#<%=tbID.ClientID%>").keyup(function (event) {
        if (event.keyCode == 13) {
            $("#<%=btSearch.ClientID%>").click();
        }
    });
</script>
    </telerik:RadCodeBlock>



