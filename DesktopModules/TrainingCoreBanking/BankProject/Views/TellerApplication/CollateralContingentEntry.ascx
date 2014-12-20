<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollateralContingentEntry.ascx.cs" Inherits="BankProject.Views.TellerApplication.CollateralContingentEntry" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>
<telerik:RadWindowManager id="RadWindowManager1" runat="server" EnableShadow="true" />
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    })
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
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/edit.png" 
            ToolTip="Edit Data" Value="btEdit" CommandName="edit" />
    </Items>
</telerik:RadToolBar>

<div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:260px; padding:5px 0 5px 20px">
                <telerik:RadTextBox ID="tbID" runat="server" ForeColor="Black"  ValidationGroup="Group1"  />
                <i><asp:Label ID="lblCheckCustomer" runat="server" Text="" ForeColor="Black" /></i>
            </td>
        </tr>
    </table>
</div>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#blank2">Contingent Entry Information</a></li>
    </ul>
    <%-- Contingent Entry information --%>
    <div id="blank2" class="dnnClear">
        <fieldset>
            <legend style="text-transform:uppercase ;font-weight:bold;">Customer Information</legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr >
                    <td class="MyLable">Contingent Entry ID: </td>
                    <td class="MyContent" > 
                        <asp:TextBox ID="tbContingentEntryID" runat="server" ValidationGroup="Group1" />
                    </td>
                </tr>
        </table>
             <table width="100%" cellpadding="0" cellspacing="0">
                 <tr>
                     <td class="MyLable">Customer ID:</td>
                     <td class="MyContent">
                         <table width="100%" cellpadding="0" cellspacing="0" >
                             <tr>
                                 <td width="350">
                                     <telerik:RadTextBox ID="tbCustomerIDName_Cont" runat="server" validationGroup="Group1" readOnly="true" width="350"/>
                                      </td>
                                 <td style="visibility:hidden">
                                     <telerik:RadTextBox id="tbCollateralType" runat="server" />
                                 </td>
                                 </tr>
                             </table>
                           </td>
                           </tr>
                 <tr>
                     <td class="MyLable">Address:</td>
                     <td class="MyContent">
                         <telerik:RadTextBox ID="tbAddress_cont" runat="server" ValidationGroup="Group1" Width="350" TextMode="MultiLine" readonly="true" />
                     </td>
                     <td ></td>
                     </tr> 
                           </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                 
                 <tr>
                     <td class="MyLable">ID / Tax Code:</td>
                     <td class="MyContent" width="350">
                         <telerik:RadTextBox ID="tbIDTaxCode" runat="server" ValidationGroup="Group1" Width="150" readonly="true" />
                     </td>
                     <td class="MyLable">Date Of Issue:</td>
                     <td class="MyContent">
                         <telerik:RadTextBox id="tbDateOfIssue" runat="server"  readonly="true" />
                     </td>
                 </tr>            
             </table>

        </fieldset>
        <fieldset>
            <legend style="text-transform:uppercase ;font-weight:bold;">CONTINGENT&nbsp; Information</legend>
             <table width="100%" cellpadding="0" cellspacing="0">
                 <tr>
                     <td class="MyLable">Transaction Code:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator5"
                     ControlToValidate="rcbTransactionCode" ValidationGroup="Commit" InitialValue="" ErrorMessage="Transaction Code is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> 
                     </td>
                     <td class="MyContent">
                          <telerik:RadComboBox ID="rcbTransactionCode" runat="server" MarkFirstMatch="true" AllowCustomText="false"
                            width="150" OnSelectedIndexChanged="rcbTransactionCode_OnSelectedIndexChanged" autoPostBack="true">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />   
                                <telerik:RadComboBoxItem Value="901" Text="901 - Nhap ngoai bang" />                               
                                <telerik:RadComboBoxItem Value="902" Text="902 - Xuat ngoai bang" />                               
                            </Items>
                        </telerik:RadComboBox>
                     </td>
                     
                 </tr>
                 <tr>
                     <td class="MyLable">Debit or Credit:<span class="Required">(*)</span>
                          <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator6"
                     ControlToValidate="rcbDebitOrCredit" ValidationGroup="Commit" InitialValue="" ErrorMessage="Debit or Credit is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> 
                     </td>
                     <td class="MyContent">
                         <telerik:RadComboBox ID="rcbDebitOrCredit" runat="server" MarkFirstMatch="true" AllowCustomText="false"
                            width="150" Enabled="false" >
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />  
                                <telerik:RadComboBoxItem Value="D" Text="D - Debit" /> 
                                <telerik:RadComboBoxItem Value="C" Text="C - Credit" />                               
                            </Items>
                        </telerik:RadComboBox>
                     </td>                     
                 </tr>
                 <tr>
                     <td class="MyLable">Currency:</td>
                     <td class="MyContent">
                         <telerik:RadComboBox ID="rcbCurrency" runat="server" MarkFirstMatch="true" AllowCustomText="false" 
                            width="150" AutoPostback="true" OnselectedIndexchanged="rcbCurrency_OnClientSelectedIndexChanged">
                            <CollapseAnimation Type="None" />
                            <ExpandAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />                                
                            </Items>
                        </telerik:RadComboBox>
                     </td>                     
                 </tr>
                 <tr>
                     <td class="MyLable">Account No:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator7"
                     ControlToValidate="rcbAccountNo" ValidationGroup="Commit" InitialValue="" ErrorMessage="CategoryID is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> 
                     </td>
                     <td class="MyContent">
                         <telerik:RadComboBox ID="rcbAccountNo" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                             width="350">
                             <CollapseAnimation Type="None" />
                             <ExpandAnimation Type="None" />
                             <Items>
                                 <telerik:RadComboBoxItem value="" text=""/>
                             </Items>
                         </telerik:RadComboBox>
                     </td>                    
                 </tr>
                 <tr>
                     <td class="MyLable">Amount:</td>
                     <td class="MyContent">
                         <telerik:RadNumericTextBox ID="tbAmount" runat="server" Width="150"  ValidationGroup="Group1"></telerik:RadNumericTextBox>
                         
                     </td>
                      <td class="MyLable">Deal Rate:</td>
                     <td class="MyContent">
                         <telerik:RadNumericTextBox ID="tbDealRate" runat="server" ValidationGroup="Group1" 
                             NumberFormat-DecimalDigits="2" ></telerik:RadNumericTextBox>
                         
                     </td>
                     
                 </tr>
                
                 <tr>
                     <td class="MyLable">Value Date:</td>
                     <td class="MyContent">
                         <telerik:RadDatePicker width="150" ID="rdpValuedate_cont" runat="server" ValidationGroup="Group1"></telerik:RadDatePicker>
                        
                     </td>
                 </tr>
                 <tr>
                     <td class="MyLable">Loan Contract No:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator8"
                     ControlToValidate="tbReferenceNo" ValidationGroup="Commit" InitialValue="" ErrorMessage="Loan Contract is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> 
                     </td>
                     <td class="MyContent">
                         <telerik:RadTextBox ID="tbReferenceNo" runat="server" ValidationGroup="Group1" width="150"  />
                     </td>
                     
                 </tr>
                 <tr>
                     <td class="MyLable">Narrative:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator9"
                     ControlToValidate="tbNarrative" ValidationGroup="Commit" InitialValue="" ErrorMessage="Narrative is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> 
                     </td>
                     <td class="MyContent" width="350">
                         <telerik:RadTextBox ID="tbNarrative" runat="server" ValidationGroup="Group1" Width="350" TextMode="MultiLine" />
                     </td>
                     
                     
                 </tr>
             </table>

        </fieldset>
    </div>
</div>

<telerik:RadCodeBlock id="RadCodeBlock2" runat="server"> 
<script type="text/javascript">
    $("#<%=tbID.ClientID%>").keyup(function (event) {

        if (event.keyCode == 13) {
            $("#<%=btSearch.ClientID%>").click();
        }
    });
    
    function GenerateZero(k) {
        n = 1;
        for (var i = 0; i < k.length; i++) {
            n *= 10;
        }
        return n;
    }
    function addCommas(str) {
        var parts = (str + "").split("."),
            main = parts[0],
            len = main.length,
            output = "",
            i = len - 1;

        while (i >= 0) {
            output = main.charAt(i) + output;
            if ((len - i) % 3 === 0 && i > 0) {
                output = "," + output;
            }
            --i;
        }
        return output + ".00";
    }
    function ClearCommas(sender, args) {
        var m = sender.get_value().replace(".00", "").replace(/,/g, '');
        console.log(m);
        sender.set_value(m);
    }
    function SetNumber(sender, args) {
        //sender.set_value(sender.get_value().toUpperCase());
        var number;
        var m = sender.get_value().substring(sender.get_value().length - 1);
        if (isNaN(m)) {
            var val = sender.get_value().substring(0, sender.get_value().length - 1).split(".");
            switch (m.toUpperCase()) {

                case "T":
                    var n1 = val[0] * 1000;
                    var n2 = 0;
                    if (val[1] != null)
                        n2 = (val[1] / GenerateZero(val[1])) * 1000;
                    number = n1 + n2;
                    sender.set_value(addCommas(number));
                    break;
                case "M":
                    var n1 = val[0] * 1000000;
                    var n2 = 0;
                    if (val[1] != null)
                        n2 = (val[1] / GenerateZero(val[1])) * 1000000;
                    number = n1 + n2;
                    sender.set_value(addCommas(number));
                    break;
                case "B":
                    var n1 = val[0] * 1000000000;
                    var n2 = 0;
                    if (val[1] != null)
                        n2 = (val[1] / GenerateZero(val[1])) * 1000000000;
                    number = n1 + n2;
                    sender.set_value(addCommas(number));
                    break;
                default:
                    alert("Character is not valid. Please use T, M and B character");
                    return false;
                    break;
            }
        } else {
            console.log("is number" + m);
            number = sender.get_value();
            sender.set_value(addCommas(number));

        }
        //var num = sender.get_value();
    }
  </script>
    </telerik:RadCodeBlock> 
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" >
    <AjaxSettings>
        
        <telerik:AjaxSetting AjaxControlID="rcbTransactionCode">
            <UpdatedControls>  
                 <telerik:AjaxUpdatedControl ControlID="rcbDebitOrCredit" />
            </UpdatedControls>
        </telerik:AjaxSetting> 

        <telerik:AjaxSetting AjaxControlID="rcbCurrency">
            <UpdatedControls>  
                 <telerik:AjaxUpdatedControl ControlID="rcbAccountNo" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager> 
<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" />
</div>