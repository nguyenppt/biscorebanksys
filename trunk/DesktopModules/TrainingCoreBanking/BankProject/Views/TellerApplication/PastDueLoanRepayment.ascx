<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PastDueLoanRepayment.ascx.cs" Inherits="BankProject.Views.TellerApplication.PastDueLoanRepayment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit" />
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_OnButtonClick">
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

<div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:200px; padding:5px 0 5px 20px">
                <telerik:RadTextBox id="tbID" runat="server" width="200px"  validationGroup="Group1">
                </telerik:RadTextBox>
            </td>
        </tr>
    </table>
</div>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#blank1">Main Info</a></li>
    </ul>
    <div id="blank1" class="dnnClear">
        <fieldset>
            <legend style="text-transform:uppercase; font-weight:bold">Contract Details:</legend>
            <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td class="MyLable">Customer ID:</td>
                <td class="MyContent" width="150">
                    <asp:Label ID="lblCustomreID" runat="server" />
                </td>
                <td class="MyContent" width="350">
                    <asp:Label ID="lblCustomreName" runat="server" />
                </td>
                <td class="MyContent"></td>
            </tr>
                <tr>
                <td class="MyLable">Main Category:</td>
                <td class="MyContent">
                    <asp:Label ID="lblMainCategory_Amt" runat="server" Text="21060"  />
                </td>
                <td class="MyContent"><asp:Label ID="lblMainCategory_Amt_Caption" runat="server" Text="Consumer Loan" /></td>
            </tr>
                <tr>
                <td class="MyLable">Sub Category:</td>
                <td class="MyContent"> <asp:Label ID="lblSubCategory_Amt" runat="server" Text="60-004"  /> </td>
                <td class="MyContent"><asp:Label ID="lblSubCategory__Amt_Caption" runat="server" Text="Cvtd Khac" /></td>
            </tr>
                <tr>
                <td class="MyLable">Purpose Code:</td>
                <td class="MyContent"><asp:Label ID="lblPurposeCode" runat="server" Text="30-000" /></td>
                <td class="MyContent"><asp:Label ID="lblPurposeCode_Caption" runat="server" Text="TD Mua nha, dat" /></td>
            </tr>
                <tr>
                <td class="MyLable">Currency:<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator2"
                        ControlToValidate="rcbCurrency"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Currency is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox id="rcbCurrency" runat="server" AllowCustomerText="false" markFirstMatch="true" 
                        OnSelectedIndexChanged="rcbCurrency_OnSelectedIndexChanged" 
                       AutoPostBack="true">
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
            </tr>
                <tr>
                <td class="MyLable">Penalty Rate:</td>
                <td class="MyContent">
                     <asp:Label ID="lblPenaltyRate" runat="server" Text="12.75" />
                </td>
                <td class="MyLable">Penalty Spread:</td>
                <td class="MyContent"> <asp:Label ID="lblPenaltySpread" runat="server" Text="6.75" /> </td>
            </tr>
                <tr>
                <td class="MyLable">Repay Date:</td>
                <td class="MyContent">
                    <telerik:RadDatePicker id="rdpRepayDate" runat="server" />
                </td>
            </tr>
                <tr>
                <td class="MyLable">Repay Amount:<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator8"
                        ControlToValidate="tbRepayAmt"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Repay Amount is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="tbRepayAmt" runat="server" ValidationGroup="Group1" />
                </td>
            </tr>
                <tr>
                <td class="MyLable">Contract Status:</td>
                <td class="MyContent">
                    <asp:Label ID="lblContractStatus" runat="server" Text="NAB" />
                </td>
                <td class="MyLable">Over Due Days:</td>
                <td class="MyContent">
                    <asp:Label ID="lblOverDueDays" runat="server" Text="29" />
                </td>
            </tr>

        </table>
        </fieldset>
        <fieldset>
            <legend style="text-transform:uppercase; font-weight:bold">Past Due Amount:</legend>
            <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td class="MyLable">PD Type:</td>
                <td class="MyContent">
                    <asp:Label ID="lblPDType1_type" runat="server" text="PR"/>
                </td>
                <td class="MyContent">
                    <asp:Label ID="lblPDType1_Caption" runat="server" text="Principal Amount"/>
                </td>
                 <td class="MyContent">
                    <asp:Label ID="lblPDType1_Amt" runat="server" text="10000000" />
                </td>
                <td class="MyContent"></td>
            </tr>
                
                <tr>
                <td class="MyLable">PD Type:</td>
                <td class="MyContent">
                    <asp:Label ID="lblPDType2_type" runat="server" text="IN"/>
                </td>
                     <td class="MyContent">
                    <asp:Label ID="lblPDType2_Caption" runat="server" text="Interest Amount"/>
                </td>
                 <td class="MyContent">
                    <asp:Label ID="lblPDType2_Amt" runat="server" text="10000000" />
                </td>
                <td class="MyContent"></td>
            </tr>
                <tr>
                <td class="MyLable">PD Type:</td>
                <td class="MyContent">
                    <asp:Label ID="lblPDType3_type" runat="server" text="PE"/>
                </td>
                    <td class="MyContent">
                    <asp:Label ID="lblPDType3_Caption" runat="server" text="Penalty Interest"/>
                </td>
                 <td class="MyContent">
                    <asp:Label ID="lblPDType3_Amt" runat="server" text="10000000" />
                </td>
                <td class="MyContent"></td>
            </tr>
                <tr>
                <td class="MyLable">PD Type:</td>
                <td class="MyContent">
                    <asp:Label ID="lblPDType4_type" runat="server" text="PS"/>
                </td>
                    <td class="MyContent">
                    <asp:Label ID="lblPDType4_Caption" runat="server" text="Penalty Interest-Spread"/>
                </td>
                 <td class="MyContent">
                    <asp:Label ID="lblPDType4_Amt" runat="server" text="10000000" />
                </td>
                <td class="MyContent"></td>
            </tr>
                <tr>
                <td class="MyLable">Total PD Amount:</td>
                <td class="MyContent">
                    <asp:Label ID="lblTotalPDAmount" runat="server" text="20000000"/>
                </td>
        </table>
        </fieldset>
        <fieldset>
            <legend style="text-transform:uppercase; font-weight:bold">Past Due Details:</legend>
            <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td class="MyLable">Due Date:</td>
                <td class="MyContent" width="150">
                    <asp:label ID="lblDueDate" runat="server" text="20000000"/>
                </td>
                <td class="MyLable">
                    <asp:label ID="lblDueDate_Amt" runat="server"  />
                </td>
                <td class="MyContent"></td>
            </tr>
                <tr>
                <td class="MyLable">Due Item:</td>
                <td class="MyContent">
                    <asp:Label ID="lblDueItem1" runat="server" text="PR"/>
                </td>
                    <td class="MyContent">
                    <asp:Label ID="lblDueItem1_Caption" runat="server" text="Principal Amount"/>
                </td>
                    <td class="MyContent">
                    <asp:Label ID="lblDueItem1_Amt" runat="server" Text="2000000" />
                </td>
                <td class="MyLable">Paid:</td>
                <td class="MyContent">
                    <asp:Label ID="lblDueItem1_Amt_col2" runat="server" Text="0" />
                </td>
            </tr>
                <tr>
                <td class="MyLable">Due Item:</td>
                <td class="MyContent">
                    <asp:Label ID="lblDueItem2" runat="server" text="IN"/>
                </td>
                    <td class="MyContent">
                    <asp:Label ID="lblDueItem2_Caption" runat="server" text="Interest Amount"/>
                </td>
                    <td class="MyContent">
                    <asp:Label ID="lblDueItem2_Amt" runat="server" Text="2000000"  />
                </td>
                <td class="MyLable">Paid:</td>
                <td class="MyContent">
                     <asp:Label ID="lblDueItem2_Amt_col2" runat="server" Text="0" />
                </td>
            </tr>
                
                <tr>
                <td class="MyLable">Due Item:</td>
                <td class="MyContent">
                    <asp:Label ID="lblDueItem3" runat="server" text="PE"/>
                </td>
                     <td class="MyContent">
                    <asp:Label ID="lblDueItem3_Caption" runat="server" text="Penalty Interest"/>
                </td>
                    <td class="MyContent">
                    <asp:Label ID="lblDueItem3_Amt" runat="server" Text="2000000" />
                </td>
                <td class="MyLable">Paid:</td>
                <td class="MyContent">
                     <asp:Label ID="lblDueItem3_Amt_col2" runat="server" Text="0" />
                </td>
            </tr>
               
                <tr>
                <td class="MyLable">Due Item:</td>
                <td class="MyContent">
                    <asp:Label ID="lblDueItem4" runat="server" text="PS"/>
                </td>
                    <td class="MyContent">
                    <asp:Label ID="lblDueItem4_Caption" runat="server" text="Penalty Interest-Spread"/>
                </td>
                    <td class="MyContent">
                    <asp:Label ID="lblDueItem4_Amt" runat="server" Text="2000000"  />
                </td>
                <td class="MyLable">Paid:</td>
                <td class="MyContent">
                     <asp:Label ID="lblDueItem4_Amt_col2" runat="server" Text="0" />
                </td>
            </tr>
                <tr>
                <td class="MyLable">Repaid Status:</td>
                <td class="MyContent">
                    <asp:Label ID="lblRepaidStatus" runat="server" Text="NAB" />
                </td>
                
        </table>
        </fieldset>
        <fieldset>
            <legend style="text-transform:uppercase; font-weight:bold">Account and Charges:</legend>
            <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td class="MyLable">Account to Debit:<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="rcbAccountToDebit"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Account to Debit is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox id="rcbAccountToDebit" runat="server" AllowCustomtext="false" MarkFirstMatch="True" AppendDataBoundItem="True"
                        validationGroup="Group1" width="390">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
                <tr>
                <td class="MyLable">Charge Code:</td>
                <td class="MyContent">
                    <telerik:radnumerictextbox id="tbChargeCode" runat="server" ValidationGroup="Group1"></telerik:radnumerictextbox>
                    <a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a>
                </td>
            </tr>
                </table>
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                <td class="MyLable">Charge Amount:</td>
                <td class="MyContent">
                    <telerik:radnumerictextbox id="tbChargeAmount" runat="server" ValidationGroup="Group1"></telerik:radnumerictextbox>
                </td>
            </tr>
        </table>
        </fieldset>
        <fieldset>
            <legend style="text-transform:uppercase; font-weight:bold">Total Repay:</legend>
            <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td class="MyLable">Amount:</td>
                <td class="MyContent" width="150">
                     <telerik:radnumerictextbox id="tbAmount" runat="server" ValidationGroup="Group1"></telerik:radnumerictextbox>
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
                <tr>
                <td class="MyLable">Tax:</td>
                <td class="MyContent"></td>
                <td class="MyLable">Default:</td>
                <td class="MyContent"></td>
            </tr>
        </table>
        </fieldset>
    </div>
</div>
<telerik:RadCodeBlock id="RadCodeBlock" runat="server">
<script type="text/javascript">
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

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>

        <telerik:AjaxSetting AjaxControlID="rcbCurrency">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="rcbAccountToDebit" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>