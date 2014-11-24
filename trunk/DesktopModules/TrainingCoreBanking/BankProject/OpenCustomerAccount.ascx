<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenCustomerAccount.ascx.cs" Inherits="BankProject.OpenCustomerAccount" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>

<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%"
      OnButtonClick="OnRadToolBarClick">
        <Items>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
                ToolTip="Commit Data" Value="btCommit" CommandName="Commit">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
                ToolTip="Preview" Value="btPreview" CommandName="Preview">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
                ToolTip="Authorize" Value="btAuthorize" CommandName="Authorize">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
                ToolTip="Reverse" Value="btReverse" CommandName="Reverse">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
                ToolTip="Search" Value="btSearchNew" CommandName="searchNew">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
                ToolTip="Print Deal Slip" Value="btPrint" CommandName="print">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/edit.png" Enabled ="false"
            ToolTip="Edit Data" Value="btEdit" CommandName="edit">
        </telerik:RadToolBarButton>
        </Items>
    </telerik:RadToolBar> 

<div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 200px; padding: 5px 0 5px 20px;">
                <asp:TextBox ID="txtId" runat="server" Width="200" />
                <i>
                    <asp:Label ID="lblDepositCode" runat="server" /></i></td>
        </tr>
    </table>
</div>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Open Account</a></li>
        <%--<li><a href="#blank">Audit</a></li>
        <li><a href="#blank">Full View</a></li>--%>
    </ul>
<%--    <div id="blank">
    </div>--%>

    <div id="ChristopherColumbus" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer ID <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="cmbCustomerId"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Customer ID is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent" Width="300">
                    <telerik:RadComboBox ID="cmbCustomerId" Width="300"
                        autopostback="true" 
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                         OnSelectedIndexChanged="cmbCustomerId_OnSelectedIndexChanged"
                        runat="server" ValidationGroup="Group1">
                    </telerik:RadComboBox>
                </td>
                <td>
                    <asp:Label ID="lbCustomerName" runat="server" Visible="false" ></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbCustomerType" Visible="false" runat="server" ></asp:Label>
                </td>
            </tr>

         </table>

        <table width="100%" cellpadding="0" cellspacing="0">

            <tr>
                <td class="MyLable">Category<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator2"
                        ControlToValidate="cmbCategory"
                        
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Category is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbCategory"
                        onitemdatabound="cmbCategory_onitemdatabound"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="cmbCategory_onselectedindexchanged"
                        Width="300" runat="server" ValidationGroup="Group1">
                    </telerik:RadComboBox>
                    <asp:Label ID="lbCategoryType" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Product Line:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbProductLine"
                        MarkFirstMatch="True" Height="300"
                        AllowCustomText="false"
                        Width="300" runat="server" ValidationGroup="Group1">
                    </telerik:RadComboBox>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Currency<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator3"
                        ControlToValidate="cmbCurrency"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Currency is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbCurrency"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        width="60"
                        runat="server" ValidationGroup="Group1">
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
                <td class="MyLable">Account Title:</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtAccountTitle" Width="300" runat="server" ClientEvents-OnValueChanged="OnAccountTitleChanged"
                        ValidationGroup="Group1" />
                </td>
            </tr>

            <tr>
                <td class="MyLable">Short Title:</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtShortTitle" Width="300" runat="server" ClientEvents-OnValueChanged="OnAccountTitleChanged" 
                        ValidationGroup="Group1" />
                </td>
            </tr>

           

            

            <tr>
                <td class="MyLable">Account Officer:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbAccountOfficer"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        Width="300" runat="server" ValidationGroup="Group1">
                    </telerik:RadComboBox>
                </td>
            </tr>



            <tr>
                <td class="MyLable">Charge.Code:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbChargeCode"
                        MarkFirstMatch="True" Height="150"
                        AllowCustomText="false"
                        Width="300" runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="1002" Text="1001 - PHI QUAN LY TAI KHOAN" />
                            <telerik:RadComboBoxItem Value="1003" Text="1002 - PHI SU DUNG MOBILE BANKING" />
                            <telerik:RadComboBoxItem Value="1004" Text="1003 - PHI CAP SO PHU QUA EMAIL" />
                            <telerik:RadComboBoxItem Value="1005" Text="1004 - PHI QLTK + SMS" />
                            <telerik:RadComboBoxItem Value="1006" Text="1005 - PHI QLTK + SMA" />
                            <telerik:RadComboBoxItem Value="1007" Text="1006 - PHI QLTK + SMS + MB" />
                            <telerik:RadComboBoxItem Value="1008" Text="1007 - PHI SMS + SMA" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>

            
        </table>

        <fieldset>
            <legend>
              <div style="font-weight:bold; text-transform:uppercase;">Join Holder</div>
            </legend>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">ID Join Holder:</td>
                <td class="MyContent" Width="285">
                    <telerik:RadComboBox ID="cmbIDJoinHolder"
                         runat="server"  
                        autopostback="true" Width="285"
                         MarkFirstMatch="True"
                        AllowCustomText="false"
                        OnSelectedIndexChanged="cmbIDJoinHolder_OnSelectedIndexChanged"
                        >
                       
                    </telerik:RadComboBox>
                </td>
                <td>
                    <asp:Label ID="lbJoinHolderName" runat="server" Visible="false" ></asp:Label>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Relation Code:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbRelationCode" Width="285"
                        MarkFirstMatch="True" AppendDataBoundItems="true"
                        AllowCustomText="false" Height="150"
                        runat="server" ValidationGroup="Group1">
                    </telerik:RadComboBox>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Join Notes:</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtJoinNotes" Width="285" runat="server" ValidationGroup="Group1" />
                </td>
            </tr>
            
        </table>

            </fieldset>
        <table width="100%" cellpadding="0" cellspacing="0">
        <tr style="visibility:hidden;">
                <td class="MyLable" >Restrict Txn:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbRestrictTxn"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        Width="300" runat="server" ValidationGroup="Group1" />
                </td>
            </tr>
            <tr style="visibility:hidden;">
                <td class="MyLable">Int. CAP to AC:</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbIntCaptoAC"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        Width="300" runat="server" ValidationGroup="Group1" />
                </td>
            </tr>
            </table>
    </div>
</div>

 <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="cmbCategory">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="cmbProductLine" />
                 <telerik:AjaxUpdatedControl ControlID="lbCategoryType" />
                
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="cmbCustomerId">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lbCustomerName" />
                 <telerik:AjaxUpdatedControl ControlID="txtDocID" />
                 <telerik:AjaxUpdatedControl ControlID="lbCustomerType" />
                
            </UpdatedControls>
        </telerik:AjaxSetting>

           <telerik:AjaxSetting AjaxControlID="cmbIDJoinHolder">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lbJoinHolderName" />
                
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });

    function OnAccountTitleChanged(sender, eventArgs) {
        var e = eventArgs.get_keyCode();
        if (e == 13) {
            var accountTitleElement = $find("<%= txtAccountTitle.ClientID %>");
            var accountTitleValue = accountTitleElement.get_value();

            var shortTitleElement = $find("<%= txtShortTitle.ClientID %>");
            var shortTitleValue = shortTitleElement.get_value();
            if (accountTitleValue.length == 0 || !accountTitleValue.trim()) {
                accountTitleElement.set_value("TKTT NGUYEN THUY TUYET TRINH");
            }

            if (shortTitleValue.length == 0 || !shortTitleValue.trim()) {
                shortTitleElement.set_value("TKTT-TUYET TRINH");
            }
        }
    }

    $('#<%= txtId.ClientID %>').keyup(function (event) {
        if (event.keyCode == 13) { $("#<%=btSearch.ClientID%>").click(); }
    });

    $(document).ready(
   function () {
       $('a.add').live('click',
           function () {
               $(this)
                   .html('<img src="Icons/Sigma/Delete_16X16_Standard.png" />')
                   .removeClass('add')
                   .addClass('remove');
               $(this)
                   .closest('tr')
                   .clone()
                   .appendTo($(this).closest('table'));
               $(this)
                   .html('<img src="Icons/Sigma/Add_16X16_Standard.png" />')
                   .removeClass('remove')
                   .addClass('add');
           });
       $('a.remove').live('click',
           function () {
               $(this)
                   .closest('tr')
                   .remove();
           });
       $('input:text').each(
           function () {
               var thisName = $(this).attr('name'),
                   thisRrow = $(this)
                               .closest('tr')
                               .index();
               $(this).attr('name', 'row' + thisRow + thisName);
               $(this).attr('id', 'row' + thisRow + thisName);
           });

   });

</script>
</telerik:RadCodeBlock>

<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" Text="Search"  OnClick="btSearch_Click" />
    <telerik:RadTextBox ID="txtDocID"  runat="server" ></telerik:RadTextBox>
</div>