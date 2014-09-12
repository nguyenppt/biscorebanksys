<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChequeIssue.ascx.cs" Inherits="BankProject.Views.TellerApplication.ChequeIssue" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" />

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>
<div>   
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%"
         OnButtonClick="RadToolBar1_ButtonClick">
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
            <td style="width: 300px; padding: 5px 0 5px 20px;">
                <asp:TextBox ID="tbChequeID" runat="server" Width="200" />
                <i><asp:Label ID="lblCustomerID" runat="server">   </asp:Label>  </i></td>
            <td><i><asp:Label ID="lblCustomeName" runat="server" /></i></td>
        </tr>
    </table>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server"  ShowMessageBox="true" ShowSummary="false" ValidationGroup="Commit" />
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Issue Cheque</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Cheque Status:<span class="Required">(*)</span> 
                    <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator3"
                     ControlToValidate="rcbChequeStatus" ValidationGroup="Commit" InitialValue="" 
                     ErrorMessage="Cheque Status  is required"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbChequeStatus" appendDataBoundItems="true"
                        MarkFirstMatch="True"
                        AllowCustomText="false" 
                        runat="server" ValidationGroup="Group1" Width="150">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Currency Account:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbCurrency" runat="server" MarkFirstMatch="true" AllowCustomText="false"
                        validationGroup="Group1"  AppendDataBoundItems="true" width="150" enabled="false">
                        <CollapseAnimation Type="None" />
                        <ExpandAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Issue Date:</td>
                <td class="MyContent">
                    <telerik:RadDatePicker ID="rdpIssueDate" runat="server" ValidationGroup="Group1" />
                </td>
            </tr>

            <tr>
                <td class="MyLable">Quantity of Issued
                        <span class="Required">:(*)</span>
                    <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator1"
                     ControlToValidate="tbQuantityOfIssued" ValidationGroup="Commit" InitialValue="" 
                     ErrorMessage="Quantity of Issued is required"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:radnumerictextbox ID="tbQuantityOfIssued" runat="server" 
                       NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" />
                </td>
            </tr>

            <tr>
                <td class="MyLable">Cheque No Start:
                        <span class="Required">(*)</span>
                      <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator2"
                     ControlToValidate="tbChequeNoStart" ValidationGroup="Commit" InitialValue="" 
                     ErrorMessage="Cheque No Start is required"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:radnumerictextbox runat="server" ID="tbChequeNoStart"  NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" ></telerik:radnumerictextbox>
                </td>
            </tr>
            <tr style="visibility:hidden;">
                <td class="MyLable">Next Trans Com:</td>
                <td class="MyContent">
                    <asp:Label ID="lblNextTransCom" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="blank"></div>
</div>

<script type="text/javascript">
    $("#<%=tbChequeID.ClientID%>").keyup(function (event) {
        
        if (event.keyCode == 13) {
            $("#<%=btSearch.ClientID%>").click();
        }
    });
  </script>
<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click1" Text="Search" />
</div>