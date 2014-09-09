<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChequePaymentStop.ascx.cs" Inherits="BankProject.Views.TellerApplication.ChequePaymentStop" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<telerik:RadWindowManager id="RadWindowManager1"  runat="server" EnableShadow="true" />
   <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });

  
</script>

<div>   
     <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
        OnButtonClick="OnRadToolBarClick">
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
</div>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="tbID" runat="server" Width="200" /><span class="Required">(*)</span>
             <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator1"
                     ControlToValidate="tbID" ValidationGroup="Commit" InitialValue="" ErrorMessage="Working Account ID is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> 
            <i>
                <asp:Label ID="lblNote" runat="server" /></i></td>
    </tr>
</table>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#Main">Stop Cheque</a></li>
    </ul>
    <div id="Main" class="dnnClear">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Customer:</td>
                <td class="MyContent" width="100">
                    <telerik:RadTextBox id="tbCustomerID" runat="server" ValidationGroup="Group1" borderWidth="0" readonly="true" />
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox id="tbCustomerName" runat="server" ValidationGroup="Group1" borderWidth="0" readonly="true" />
                </td>
                <td class="MyContent"></td>
            </tr>
            
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Currency:
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox id="tbCurrency" runat="server" ValidationGroup="Group1" borderwidth="0" readOnly="true" />
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Reason for Stopping:</td>
                <td class="MyContent" width="250">
                    <telerik:RadComboBox ID="rcbReasonForStopping" TabIndex="1"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        Width="250" runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="1" Text="1 - CHEQUES LOST" />
                            <telerik:RadComboBoxItem Value="2" Text="2 - CHEQUES DESTROYED" />
                            <telerik:RadComboBoxItem Value="3" Text="3 - CHEQUES STOLEN" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td class="MyLable"><%--<a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a>--%></td>
                <td class="MyContent"></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">From Serial:<span class="Required">(*)</span>
             <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator2"
                     ControlToValidate="tbFromSerial" ValidationGroup="Commit" InitialValue="" ErrorMessage="From serial Field is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> </td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="tbFromSerial" runat="server" ValidationGroup="Group1" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="" TabIndex="2" Width="150" ClientEvents-OnValueChanged="SoSanh_Serial" />
                </td>
                <td class="MyLable">To Serial:<span class="Required">(*)</span>
             <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator3"
                     ControlToValidate="tbToSerial" ValidationGroup="Commit" InitialValue="" ErrorMessage="To serial Field is required"
                    ForeColor="Red"></asp:RequiredFieldValidator> </td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="tbToSerial" runat="server" ValidationGroup="Group1" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator=""
                     TabIndex="3" width="150" ClientEvents-OnValueChanged="SoSanh_Serial" />
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">No.of Leaves:</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox id="tbNoOfLeave" runat="server" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" readOnly="true"
                        BorderWidth="0" />
                </td>
            </tr>

            <tr>
                <td class="MyLable">Cheques Type:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbChequeType"  AppendDataBoundItems="true" 
                        MarkFirstMatch="True" 
                        AllowCustomText="false" Width="250"
                        runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">From Amount:</td>
                <td class="MyContent" >
                    <telerik:RadNumericTextBox ID ="tbFromAmount" runat="server" ValidationGroup="Group1"  width="150"/>
                </td>

                <td class="MyLable">To Amount:</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="tbToAmount" runat="server" ValidationGroup="Group1"  width="150" />
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Waive Charges:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbWaiveCharges"
                        MarkFirstMatch="True"
                        AllowCustomText="false"                        
                        runat="server"  width="150">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="YES" Text="YES" />
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Activity Date:</td>
                <td>
                    <telerik:RadDatePicker ID="rdpActivityDate" runat="server" width="150"/>
                </td>
            </tr>
                <tr style="visibility:hidden;">
                <td><telerik:RadNumericTextBox id="tbFromSeri_H" runat="server" /> </td>
                <td><telerik:RadNumericTextBox id="tbToSeri_H" runat="server" /> </td>
            </tr>
                </table>
        </table>
    </div>
</div>
<script type="text/javascript">
    $("#<%=tbID.ClientID%>").keyup(function (event) {

        if (event.keyCode == 13) {
            $("#<%=btSearch.ClientID%>").click();
        }
    });
    
   
    function SoSanh_Serial() {
        var FromSerialElement = $find("<%= tbFromSerial.ClientID %>");
        var FromSerialValue = FromSerialElement.get_value();
        var ToSerialElement = $find("<%= tbToSerial.ClientID %>");
        var ToSerialValue = ToSerialElement.get_value();
        var FromSeri_H = $find("<%=tbFromSeri_H.ClientID%>").get_value();
        var ToSeri_H = $find("<%=tbToSeri_H.ClientID%>").get_value();
        if ((FromSerialValue)&&(FromSerialValue < FromSeri_H || FromSerialValue > ToSeri_H))
        { radconfirm("Cheque No does not exists, please check again, ChequeNo must be within scale " + FromSeri_H + " - " + ToSeri_H + " !", confirmCallbackFunction3); return; }
       
        if ((ToSerialValue) && (ToSerialValue < FromSeri_H || ToSerialValue > ToSeri_H))
        { radconfirm("Cheque No does not exists, please check again, ChequeNo must be within scale " + FromSeri_H + " - " + ToSeri_H + " !", confirmCallbackFunction3); return; }
        if (FromSerialValue && ToSerialValue) {
            if (ToSerialValue >= FromSerialValue) {
                $find("<%=tbNoOfLeave.ClientID%>").set_value(ToSerialValue - FromSerialValue + 1);
            } else {
                $find("<%=tbNoOfLeave.ClientID%>").set_value("");
                showMessage();
            }
        } else { $find("<%=tbNoOfLeave.ClientID%>").set_value(""); }
    }
    function confirmCallbackFunction3(args) {
        var clickCalledAfterRadconfirm = true;
        if (lastClickedItem != null) {
            lastClickedItem.click();
            lastClickedItem = null;
        }
       
    }
    function showMessage() {
        radconfirm("To Serial Value must be greater than From Serial Value, check these values again !", confirmcallbackfunction2);
    }
    function confirmcallbackfunction2(args) {
        clickcalledAfterRadconfirm = true;
        lastclickItem.click();
        lastClickItem = null;
    }

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
<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click1" Text="Search" />
</div>