<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreclosureFinalAzAccount.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Close.PreClose.PreclosureFinalAzAccount" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>
<telerik:RadToolBar runat="server" ID="RadToolBar1" OnClientButtonClicking="OnClientButtonClicking"
     EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
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
                <asp:TextBox ID="tbDepositCode" runat="server" Width="400" />
                <i>
                    <asp:Label ID="lblDepositCode" runat="server" /></i></td>
        </tr>
    </table>
</div>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#Preclose">Preclose</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Customer Infomation</div>
            </legend>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Customer 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblCustomer"></asp:Label></td>
                </tr>

                <tr>
                    <td class="MyLable">Category 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblCategory"></asp:Label></td>
                </tr>

                <tr>
                    <td class="MyLable">Currency 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblCurrency"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Product Code 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblProductCode"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Principal 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="Label2" text="0"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable"> 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblPrincipal" ForeColor="DarkGreen"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Value Date 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblValueDate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Maturity Date 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblMaturityDate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Interested Rate
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblInterestedRate"></asp:Label></td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Pre_Close</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Preclose (Y/N) 
                    </td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="rcbPrecloseYN" runat="server" MarkFirstMatch="True" Width="50" Height="50px" AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="1" Text="Y" />
                            </Items>
                        </telerik:RadComboBox>
                        <i><asp:Label runat="server" ID="Label1"></asp:Label></i>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Nominated Account 
                    </td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="rcbWorkingAcc" runat="server" MarkFirstMatch="True" Width="150" Height="150px" AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="1" Text="07.000168836.8" />
                                <telerik:RadComboBoxItem Value="2" Text="07.000164412.7" />
                            </Items>
                        </telerik:RadComboBox>
                        <i><asp:Label runat="server" ID="lblWokingAccName"></asp:Label></i>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Waive Charges</td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="rcbWaiveCharges" runat="server" 
                            MarkFirstMatch="True" Width="50" Height="150px" 
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="1" Text="YES" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Audit Information</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Overrides</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblOverrides" Text =""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblOverrides2" Text ="" ForeColor="DarkGreen"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Record Status</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblRecordStatus"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblRecordStatus2" Text ="" ForeColor="DarkGreen"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Curr Number</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCurrNumber" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblCurrNumber2" Text ="" ForeColor="DarkGreen"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Inputter</td>
                    <td class="MyContent">
                        <asp:Label ID="lblInputter" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblInputter2" Text ="" ForeColor="DarkGreen"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Authoriser</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAuthoriser" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable"></td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblAuthoriser2" Text ="" ForeColor="DarkGreen"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Company Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCoCode" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Dept Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblDeptCode" runat="server" /></td>
                </tr>
                <%--<tr>
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
                    <td class="MyLable">Auditor Code</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAuditorCode" runat="server" /></td>
                </tr>--%>
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
        function OnClientButtonClicking(sender, args) {

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
<asp:HiddenField ID="hdfPreclosureType" runat="server" Value="0" />
