<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenNonRevolingCoomContr.ascx.cs" Inherits="BankProject.Views.TellerApplication.OpenNonRevolingCoomContr" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>

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
    </Items>
</telerik:RadToolBar>

<div>
    <table width="100%" cellpading="0" cellspacing="0">
        <tr>
            <td style="width:200px ;padding:5px 0 5px 20px;">
                <asp:TextBox Width="200" ID="tbID" runat="server" validationGroup="Group1" />
                <i>
                    <asp:Label ID="lblID" runat="server"></asp:Label>
                </i>

            </td>
        </tr>
    </table>
</div>

<div class="dnnForm" id="tabs-demo">
        <div>
    <ul class="dnnAdminTabNav">
        <li><a href="#blank1">Main Info</a></li>
    </ul>
        </div>
    <div id="blank1" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />
    <fieldset>   
        <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="MyLable" >CategoryID:<span class="Required">(*)</span>
                <asp:RequiredFieldValidator Runat="server" Display="None" ID="RequiredFieldValidator1"
                     ControlToValidate="rcbCategoryID" ValidationGroup="Commit" InitialValue="" ErrorMessage="CategoryID is required"
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td class="MyContent" width="150">
                <telerik:RadComboBox ID="rcbCategoryID" text="21-097" runat="server" AllowCustomText="false" MarkFirstMatch="true" Width="150">
                <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem value="21-097" Text="21-097" />
                    </Items>
                    </telerik:RadComboBox>
                <i>NON COMM REV</i>
            </td>
            <td class="MyLable"></td>
            <td class="MyContent"></td>
        </tr>
        <tr>
            <td class="MyLable">CustomerID:<span class="Required">(*)</span>                 
                <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator2" ControlToValidate="rcbCustomerID"
                    ValidationGroup="Commit" InitialValue="" ErrorMessage="Customer ID is Required!" ForeColor="Red" />
            </td>
            <td class="MyContent">
                <table width="100%" cellpading="0" cellspacing="0">
                    <tr>
                        <td style="width:150">
                            <telerik:RadComboBox ID="rcbCustomerID"  runat="server" width="350" MarkFirstMatch="true"
                                AllowCustomtext="false" AppendDataBoundItems="true" >
                                <CollapseAnimation Type="None" />
                                <ExpandAnimation Type="None" />
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                </Items>
                                </telerik:RadComboBox>
                            </td>                        
                    </tr>
                </table>
            </td>
            <td class="MyLable"></td>
            <td class="MyContent"></td>
        </tr>
            <table width="100%" cellpading="0" cellspacing="0">
          <tr>
            <td class="MyLable">Bus Day Define:</td>
            <td class="MyContent" width="150">
                <telerik:RadTextBox ID="tbBusDayDefine" runat="server" ValidationGroup="Group1" Text="VN" />
            </td>
              <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a> </td>
            
        </tr>
            </table>
            <Table width="100%" cellpading="0" cellspacing="0">
          <tr>
            <td class="MyLable" width="150">Currency & Comm Amt:</td>
            <td class="MyContent" width="150">
                  <telerik:RadComboBox
                        ID="rcbCurrency" runat="server" 
                        MarkFirstMatch="True" 
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                            <telerik:RadComboBoxItem Value="GBP" Text="GBP" />
                            <telerik:RadComboBoxItem Value="JPY" Text="JPY" />
                             <telerik:RadComboBoxItem Value="USD" Text="USD" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND" />
                        </Items>
                    </telerik:RadComboBox>
            </td>
              <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a> 
                  <telerik:RadNumericTextBox ID="tbCommAmt" NumberFormat-DecimalDigits="0" runat="server"  ValidationGroup="Group1"/>
              </td>
            
            <td class="MyContent" width="">
                
            </td>
              
              
        </tr>
             </Table>
            <table width="100%" cellpading="0" cellspacing="0">
         <tr>
            <td class="MyLable">Start Date:</td>
            <td class="MyContent">                
                <telerik:RadDatePicker ID="dtpStartDate" runat="server" />
            </td>
            <td class="MyLable" width="150">End Date:<span class="Required">(*)</span>
                  <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator3" ControlToValidate="dtpEndDate"
                  ValidationGroup="Commit" InitialValue="" ErrorMessage="End Date is Required!" ForeColor="Red"/>

            </td>
            <td class="MyContent" >  
                <telerik:RadDatePicker ID="dtpEndDate"  runat="server" ClientEvents-OnDateSelected="dtpEndDate_ClientEvents_OnDateSelected"/>

                    </td>
        </tr>
                </table>
         </fieldset>    
       <fieldset>
           <legend>
               <div style="font-weight:bold;text-transform:uppercase;">Basic Details</div>
           </legend>
                   <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable" width="150" >Comm Fee Start:</td>
                    <td class="MyContent" width="">
                        <telerik:RadNumericTextBox  width="" ID="tbFeeStart" NumberFormat-DecimalDigits="0" runat="server" validationGroup="Group1" />
                    </td>
                    <td class="MyLable">Comm Fee End:</td>
                    <td class="MyContent" width="">
                       <telerik:RadNumericTextBox ID="tbFeeEnd" runat="server" NumberFormat-DecimalDigits="0" validationGroup="Group1" />

                    </td>
                </tr>
                       <tr>
            <td class="MyLable">Available Amount:</td>            
        </tr>
        <table Width="100%" CellPadding="0" CellSpacing="0">
              <tr>
                <td class="MyLable">Tranche Amount:</td>
                <td style="width: 150px;" class="MyContent">
                    <telerik:RadNumericTextBox ID="tbTrancheAmount" runat="server" validationGroup="Group1" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                    <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"></a> <td>
                </td>
           
            </tr>
        </table>
        <Table Width="100%" CellPadding="0" CellSpacing="0">
          <tr>
            <td class="MyLable">DD Start Date:</td>
            <td class="MyContent" width="">
                <telerik:RadDatePicker ID="dtpDDStartDate" runat="server" />
            </td>
            <td class="MyLable">DD End Date:</td>
            <td class="MyContent" width="">
                <telerik:RadDatePicker ID="dtpDDEndDate" runat="server" />
            </td>
        </tr>
         
             </table>
             <table width="100%" cellpading="0" cellspacing="0">
                 <tr>
            <td class="MyLable">Interested Repay Account:</td>
            <td class="MyContent" width="">
                <telerik:RadComboBox
                        ID="rcbIntRepayAcct" runat="server" 
                        MarkFirstMatch="True" Width="390" 
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                           <telerik:RadComboBoxItem Value="10002650471" Text="10002650471 - TKTV-VND-Phan Van Han" />
                            <telerik:RadComboBoxItem Value="10002650472" Text="10002650472 - TKTV-VND-Dinh Tien Hoang" />
                            <telerik:RadComboBoxItem Value="10002650473" Text="10002650473 - TKTV-VND-Pham Ngoc Thach" />
                            <telerik:RadComboBoxItem Value="10002650474" Text="10002650474 - TKTV-VND-Vo Thi Sau"  />
                            <telerik:RadComboBoxItem Value="10002650475" Text="10002650475 - TKTV-VND-Truong Cong Dinh"  />
                            <telerik:RadComboBoxItem Value="20002665321" Text="20002665321 - TKTV-USD-CTY TNHH SONG HONG"  />
                            <telerik:RadComboBoxItem Value="20002665322" Text="20002665322 – TKTV-USD-CTY TNHH PHAT TRIEN PHAN MEM ABC" />
                            <telerik:RadComboBoxItem Value="20002665323" Text="20002665323 – TKTV-USD-CTY Travelocity Corp" />
                            <telerik:RadComboBoxItem Value="20002665324" Text="20002665324 – TKTV-VND-CTY Wall Street Corp" />
                            <telerik:RadComboBoxItem Value="20002665325" Text="20002665325 – TKTV-VND-CTY Viet Victory Corp" />
                        </Items>
                    </telerik:RadComboBox>
            </td>           
        </tr>
                </table>
         
             </fieldset>
            <fieldset>
                <legend>
                    <div style="font-weight:bold;text-transform:uppercase;" >Charges Details</div>
                </legend>     
        
         <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="MyLable" >Charge Code:</td>
            <td class="MyContent" width="150">
                 <telerik:RadComboBox
                        ID="RcbChargeCode" runat="server" 
                        MarkFirstMatch="True" Width="150" 
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />                         
                        
                        </Items>
                    </telerik:RadComboBox>
            </td>
            <td >
                <a class="add"> <img src="Icons/Sigma/Add_16X16_Standard.png" /></a>
            </td>
            
           
        </tr>
             </table>
              <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="MyLable">Charge Amount:</td>
            <td class="MyContent" width="150">
                <telerik:RadNumericTextBox ID="tbChargeAmount" runat="server" NumberFormat-DecimalDigits="0" ValidationGroup="Group1" />
            </td>
            
        </tr>
          <tr>
            <td class="MyLable">Charge Date:</td>
            <td class="MyContent" width="150">
                <telerik:RadDatePicker ID="dtpChargeDate" validationGroup="Group1" runat="server" />

            </td>
            <td class="MyLable"></td>
            <td class="MyContent"></td>
        </tr>
          <tr>
            <td class="MyLable">Charge Account:</td>
            <td class="MyContent" width ="150">
                <telerik:RadComboBox
                        ID="rcbChargeAccount" runat="server" 
                        MarkFirstMatch="True" Width="" 
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />                         
                        
                        </Items>
                    </telerik:RadComboBox>
            </td>
           
        </tr>
             </table>
                </fieldset>
            <fieldset>
                <legend>
                    <div style="text-transform:uppercase;font-weight:bold">Orther Details</div>
                </legend>            
         
         <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="MyLable">Secured (Y/N):</td>
            <td class="MyContent" width=""> 
                <telerik:RadComboBox
                        ID="rcbSecured" runat="server" 
                        MarkFirstMatch="True"  
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />  
                             <telerik:RadComboBoxItem Value="Y" Text="Yes" /> 
                             <telerik:RadComboBoxItem Value="N" Text="No" />  
                        </Items>
                    </telerik:RadComboBox>
             </td>
            
        </tr>
             </table>
              <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="MyLable">Cust Remarks:</td>
            <td class="MyContent" width="150">
                <telerik:RadTextBox ID="tbCustRemarks" Width="350" runat="server" ValidationGroup="Group1"></telerik:RadTextBox>
            </td>            
            <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png"</a></td>
            </tr>
            </table>
               <table width="100%" cellpadding="0" cellspacing="0">        
          <tr>
            <td class="MyLable" width="150">Account Officer:</td>
            <td class="MyContent">
                 <telerik:RadComboBox
                        ID="rcbAcctOfficer" runat="server" 
                        MarkFirstMatch="True" Width="350" 
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <%--<Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="6547" Text="6547 - HOANG NGOC MINH" />
                            <telerik:RadComboBoxItem Value="6231" Text="6231 - TRUONG VO KY"  />                        
                        </Items>--%>
                    </telerik:RadComboBox>
            </td>
           
        </tr>
                   </table>
            <%-- <table width="100%" cellpadding="0" cellspacing="0">
          <tr>
            <td class="MyLable">Legacy Ref:</td>
            <td class="MyContent" width="150">
                <telerik:RadTextBox ID="tbLegacyRef" runat="server" ValidationGroup="Group1"></telerik:RadTextBox>
            </td>
            <td><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></td>
            <td class="MyContent"></td>
        </tr>        
            </table>  --%>
                </fieldset>       
    </div>
</div>


<script type="text/javascript">
    function dtpEndDate_ClientEvents_OnDateSelected(sender, eventArgs) {
        var datePicker = $find("<%= dtpDDEndDate.ClientID %>");
        var ExpiryDate = $find("<%= dtpEndDate.ClientID %>");
        var date = ExpiryDate.get_selectedDate();

        datePicker.set_selectedDate(date);
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

<%--//doan code de load du lieu tu DB len Form cho Customer ID--%>
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

