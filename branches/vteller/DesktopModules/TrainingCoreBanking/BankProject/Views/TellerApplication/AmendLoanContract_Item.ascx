<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AmendLoanContract_Item.ascx.cs" Inherits="BankProject.Views.TellerApplication.AmendLoanContract_Item" %>
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
<div id="tabs-demo" class="dnnForm">
    <ul class="dnnAdminTabNav">
        <li><a href="#blank1">Extended Info</a></li>
    </ul>
    <div id="blank1" class="dnnClear">
        <fieldset>
            <legend style="text-transform:uppercase; font-weight:bold;">Main Contract Details</legend>
            <table width="100%" cellspacing="0" cellpadding="0">
             <tr>
                <td class="MyLable">Main Category:</td>
                <td class="MyContent" width="350">
                    <asp:Label ID="lblMainCategory_ID" runat="server" Text="21-060"  />
                </td>
                 <td></td>
                <td class="MyContent"><asp:Label ID="lblMainCategory_ID_Caption" runat="server" Text="Consumer Loan" /></td>
            </tr>
            <tr>
                <td class="MyLable">Sub Category:</td>
                <td class="MyContent"> <asp:Label ID="lblSubCategory_ID" runat="server" Text="60-004"  /> </td>
                <td class="MyContent"></td>
                <td class="MyContent"><asp:Label ID="lblSubCategory__ID_Caption" runat="server" Text="Cvtd mua xe hoi" /></td>
            </tr>

            <tr>
                <td class="MyLable">Customer ID:</td>
                <td class="MyContent">
                    <asp:Label ID="lblCustomreID" runat="server" />
                </td>
                <td class="MyContent"></td>
                <td class="MyContent" width="270">
                    <asp:Label ID="lblCustomreName" runat="server" />
                </td>
                
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
                    <telerik:RadComboBox id="rcbCurrency" runat="server" AllowCustomerText="false" markFirstMatch="true">
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
                <td class="MyLable">
                    Drawdown Amount:
                </td>
                <td class="MyContent">
                    <asp:label id="lblDrawdownAmt" runat="server" validationGroup="Group1" text="290,000,000"/>
                </td>
            </tr>
             <tr>
                <td class="MyLable">Loan Amount:</td>
                <td class="MyContent"><asp:Label ID="lblLoanAmt" runat="server" Text="77,000,000" /></td>
                 <td class="MyLable">Approve Amount:</td>
                <td class="MyContent"><asp:Label ID="lblApproveAmt" runat="server" Text="290,000,000" /></td>
            </tr>
             
            <tr>
                <td class="MyLable">Value Date:</td>
                <td class="MyContent"><telerik:radDatePicker id="rdpValueDate" runat="server"></telerik:radDatePicker></td>
                <td class="MyLable">Orig Mat Date:</td>
                <td class="MyContent"><telerik:radDatePicker ID="rdpOrigMatDate" runat="server" /></td>
            </tr>
                
                <tr>
                <td class="MyLable">Interested Rate:</td>
                <td class="MyContent">
                     <asp:Label ID="lblInterestedRate" runat="server" Text="14.40" />
                </td>
                <td class="MyLable">Loan Status:</td>
                <td class="MyContent"> <asp:Label ID="lblLoanStatus" runat="server" Text="CUR" /> </td>
            </tr>
                <tr>
                <td class="MyLable">Interested Spread:</td>
                <td class="MyContent">
                     <asp:Label ID="lblInterestedSpread" runat="server" Text="" />
                </td>
                <td class="MyLable">PD Status:</td>
                <td class="MyContent"> <asp:Label ID="lblPDStatus" runat="server" Text="" /> </td>
            </tr>
                <tr>
                    <td class="MyLable">Total Interest Amount:</td>
                    <td class="MyContent"><asp:Label ID="lblTotalIntAmt" runat="server" /></td>
                </tr>
                <tr>
                <td class="MyLable">Total Amount:</td>
                <td class="MyContent">
                     <asp:Label ID="lblTotalAmt" runat="server" Text="1,091,200" />
                </td>
                <td class="MyLable">No Over Days:</td>
                <td class="MyContent"> <asp:Label ID="lblNoOverDays" runat="server" Text="0" /> </td>
            </tr>
                
        </table>
        </fieldset>
        <fieldset>
            <legend style="text-transform:uppercase; font-weight:bold">Extended Details</legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">
                        New Mat Date:<span class="Required">(*)
                            <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator8"
                        ControlToValidate="rdpNewMatDate"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="New Mat Date is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                                     </span>
                    </td>
                    <td class="MyContent">
                        <telerik:RadDatePicker id="rdpNewMatDate" runat="server" validationGroup="Group1" />
                    </td>
                    <td class="MyLable">
                    </td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">
                        Loan Class:
                    </td>
                    <td class="MyContent">
                        <telerik:radComboBox id="rcbLoanClass" runat="server" validationGroup="Group1" appenddataboundItem="true" 
                            AllowCustomerText="false" markFirstMatch="true" >
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="1" Text="1" />
                            </Items>
                            </telerik:radComboBox>
                    </td>
                    <td class="MyLable"> Loan Class date:</td>
                    <td class="MyContent"> <telerik:radDatePicker id="rdpLoanClassDate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Cust Remarks:<span class="Required">(*)
                         <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="tbCustRemark"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Custome remarks is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></span>
                    </td>
                    <td class="MyContent" width="350">
                        <telerik:RadTextBox id="tbCustRemark" runat="server" validationGroup="Group1" width="350"></telerik:RadTextBox>
                    </td>
                    <td class="MyLable"><a class="add"><a class="add"> <img src="Icons/Sigma/Add_16X16_Standard.png"/></a></td>
                </tr>
                </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Auto Sch(Y/N):</td>
                    <td class="MyContent" width="350">
                        <telerik:RadComboBox ID="rcbAutoSch" runat="server"  
                         MarkFirstMatch="True"
                         AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" /> 
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                            <telerik:RadComboBoxItem Value="YES" Text="YES" />
                        </Items>
                     </telerik:RadComboBox>
                    </td>
                    <td class="MyLable">Def. Sch(Y/N):</td>
                    <td class="MyContent"> 
                        <telerik:RadComboBox ID="rcbDefSch" runat="server"  
                         MarkFirstMatch="True"
                         AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" /> 
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                            <telerik:RadComboBoxItem Value="YES" Text="YES" />
                        </Items>
                     </telerik:RadComboBox> </td>
                </tr>
            <tr>
                <td class="MyLable">Charge Code:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbChargeCode" runat="server"  
                         MarkFirstMatch="True"
                         AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" /> 
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="1" Text="1" />
                        </Items>
                     </telerik:RadComboBox><a class="add"> <img src="Icons/Sigma/Add_16X16_Standard.png"/></a>
                </td>
                <td class="MyLable"></td>
                <td class="MyContent"></td>
            </tr>
                </table>
            <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Charge Amount:</td>
                <td class="MyContent" width="350">
                    <telerik:RadNumericTextBox ID="tbChargeAmount" runat="server"/>
                </td>
                <td class="MyLable">Charge Date:</td>
                <td class="MyContent"> <telerik:RadDatePicker ID="rdpChargeDate"  runat="server" /> </td>
            </tr>
            <tr>
                <td class="MyLable">Repay Sch Type:</td>
                <td class="MyContent">
                         <telerik:RadComboBox ID="rcbRepaySchType" runat="server"  
                         MarkFirstMatch="True"
                         AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" /> 
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="N" Text="N - NON-REDEMPTION" />
                        </Items>
                     </telerik:RadComboBox>
                </td>
            </tr>
            </table>
        </fieldset>
    </div>
</div>

<telerik:RadCodeBlock>
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