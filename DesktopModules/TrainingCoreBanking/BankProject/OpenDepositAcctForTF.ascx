<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenDepositAcctForTF.ascx.cs" Inherits="BankProject.OpenDepositAcctForTF" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>
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
    </Items>
</telerik:RadToolBar>
<div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 200px; padding: 5px 0 5px 20px;">
                <asp:TextBox ID="tbDepositCode" runat="server" Width="200" />
                <i>
                    <asp:Label ID="lblDepositCode" runat="server" /></i></td>
        </tr>
    </table>
</div>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Open Account</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer ID 
                    <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator 
                runat="server" Display="None"
                ID="RequiredFieldValidator1" 
                ControlToValidate="rcbCustomerID" 
                        ValidationGroup="Commit"
                InitialValue="" 
                ErrorMessage="Customer ID is Required" ForeColor="Red">
            </asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="350">
                                <telerik:RadComboBox AppendDataBoundItems="True" 
                                    ID="rcbCustomerID" runat="server"
                                    MarkFirstMatch="True" Width="350" Height="150px"
                                    AllowCustomText="false">
                                    <ExpandAnimation Type="None" />
                                    <CollapseAnimation Type="None" />
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:Label ID="lblCustomerID" runat="server" /></td>
                            <td><i>
                                <asp:Label ID="lblCustomer" runat="server" /></i></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Category Code<span class="Required">(*)</span></td>
                <td class="MyContent">
                    <telerik:RadComboBox
                        ID="rcbCategoryCode" runat="server"
                        MarkFirstMatch="True" Width="150"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="Deposits for LC Openning" Text="1-121" />
                        </Items>
                    </telerik:RadComboBox>
                    <i>Deposits for LC Openning</i></td>
            </tr>
            <tr>
                <td class="MyLable">Currency <span class="Required">(*)</span><asp:RequiredFieldValidator 
                runat="server" Display="None"
                ID="RequiredFieldValidator2" 
                ControlToValidate="rcbCurrentcy" 
                        ValidationGroup="Commit"
                InitialValue="" 
                ErrorMessage="Currentcy is Required" ForeColor="Red">
            </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadComboBox
                        ID="rcbCurrentcy" runat="server" OnClientSelectedIndexChanged="FillMnemonic"
                        MarkFirstMatch="True" Width="150" Height="150px"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="USD" Text="USD" />
                            <telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                            <telerik:RadComboBoxItem Value="GBP" Text="GBP" />
                            <telerik:RadComboBoxItem Value="JPY" Text="JPY" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="MyLable">Account Name</td>
                <td class="MyContent">
                    <asp:TextBox ID="tbAccountName" runat="server" Width="300" /></td>
            </tr>
            <tr>
                <td class="MyLable">Short Name</td>
                <td class="MyContent">
                    <asp:TextBox ID="tbShortName" runat="server" Width="300" /></td>
            </tr>
            <tr>
                <td class="MyLable">Account Mnemonic</td>
                <td class="MyContent">
                    <asp:TextBox ID="tbAccountMnemonic" runat="server" MaxLength="9" /></td>
            </tr>
            <tr>
                <td class="MyLable">Product Line</td>
                <td class="MyContent">
                    <telerik:RadComboBox
                        ID="rcbProductLine" runat="server" OnClientSelectedIndexChanged="FillMnemonic"
                        MarkFirstMatch="True" Width="150"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Join Account Infomation</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Joint Holder ID</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="rcbJointHolderID" runat="server"
                            MarkFirstMatch="True" Width="150"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Relation Code</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="rcbRelationCode" runat="server"
                            MarkFirstMatch="True" Width="150"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Notes</td>
                    <td class="MyContent">
                        <asp:TextBox ID="tbNotes" runat="server" Width="300" /></td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Audit Trail</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Override</td>
                    <td class="MyContent"></td>
                </tr>
                <tr>
                    <td class="MyLable">Record Status</td>
                    <td class="MyContent"></td>
                </tr>
                <tr>
                    <td class="MyLable">Curr No</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCurrNo" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Inputter</td>
                    <td class="MyContent">
                        <asp:Label ID="lblInputter" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Date.Time</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDateTime" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Date.Time</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDateTime2" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Authoriser</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAuthoriser" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Co.Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCoCode" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Dept.Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDeptCode" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Auditor Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAuditorCode" runat="server" /></td>
                </tr>
            </table>
        </fieldset>
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
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function FillMnemonic() {
            var tbAccountMnemonic = document.getElementById("<%=tbAccountMnemonic.ClientID%>");
          var rcbCurrentcy = document.getElementById("<%=rcbCurrentcy.ClientID%>");
          var rcbCustomerID = document.getElementById("<%=rcbCustomerID.ClientID%>");
          tbAccountMnemonic.value = rcbCurrentcy.value + 'O' + rcbCustomerID.value;
      }
      $("#<%=tbDepositCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btSearch.ClientID %>").click();
          }
      });
    </script>
</telerik:RadCodeBlock>
<div style="visibility: hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" />
</div>
