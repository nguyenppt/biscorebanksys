<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenCorpCustomer.ascx.cs" Inherits="BankProject.OpenCorpCustomer" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>

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

<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
    ShowSummary="False" ValidationGroup="Commit" />
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtId" runat="server" Width="200" TabIndex="1" />
        </td>
    </tr>
</table>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#ChristopherColumbus">Customer Info</a></li>
        <li><a href="#FurtherDetails">Details</a></li>
        <li style="visibility: hidden;"><a href="#blank">Other Details</a></li>
        <li style="visibility: hidden;"><a href="#blank">Audit</a></li>
        <li style="visibility: hidden;"><a href="#blank">Full View</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">GB Short Name
                    <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1"
                        ControlToValidate="txtGBShortName"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="GB Short Name is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="MyContent" width="300">
                    <telerik:RadTextBox ID="txtGBShortName" Width="300" runat="server" TabIndex="1" ValidationGroup="Group1" />
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">GB Full Name
                     <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator2"
                        ControlToValidate="txtGBFullName"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="GB Full Name is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="MyContent" width="300">
                    <telerik:RadTextBox ID="txtGBFullName" Width="300" TabIndex="2" runat="server" ValidationGroup="Group1" />
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Incorp Date<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator4"
                        ControlToValidate="rdpIncorpDate"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Incorp Date is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadDatePicker runat="server" ID="rdpIncorpDate" MinDate="1900/1/1" TabIndex="4">
                    </telerik:RadDatePicker>
                </td>
            </tr>
        </table>

        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">GB Street
                    <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator5"
                        ControlToValidate="txtGBStreet"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="GB Street is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="MyContent" width="300">
                    <telerik:RadTextBox ID="txtGBStreet" runat="server" Width="300" ValidationGroup="Group1" TabIndex="5" />
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">GB Town/Dist. <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator6"
                        ControlToValidate="txtGBDist"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="GB Town/Dist is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent" width="300">
                    <telerik:RadTextBox ID="txtGBDist" Width="300" runat="server"
                        TabIndex="6"
                        ValidationGroup="Group1" />
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">City/Province<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator7"
                        ControlToValidate="cmbCity"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="City/Province is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbCity"
                        AppendDataBoundItems="true"
                         Width="250" runat="server"
                        MarkFirstMatch="True" TabIndex="7"
                        AllowCustomText="false"
                        ValidationGroup="Group1">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">GB Country</td>
                <td class="MyContent" width="250">
                    <telerik:RadComboBox ID="cmbCountry" Width="250" MarkFirstMatch="True"
                        AllowCustomText="false" TabIndex="8" AppendDataBoundItems="True"   
                        runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem value="" text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Nationality</td>
                <td class="MyContent">
                    <telerik:RadComboBox MarkFirstMatch="True" TabIndex="9" AppendDataBoundItems="True"   
                        AllowCustomText="false" ID="cmbNationality" Width="250" runat="server" ValidationGroup="Group1" >
                     <Items>
                            <telerik:RadComboBoxItem value="" text="" />
                     </Items>
                        </telerik:RadComboBox>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Residence</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbRecidence" MarkFirstMatch="True"
                        AllowCustomText="false" TabIndex="10" AppendDataBoundItems="True"   
                        Width="250" runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem value="" text="" />
                     </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Doc Type</td>
                <td class="MyContent" width="300">
                    <telerik:RadComboBox ID="cmbDocType"
                        MarkFirstMatch="True" TabIndex="11"
                        AllowCustomText="false"
                        HighlightTemplatedItems="true" Width="250" runat="server" ValidationGroup="Group1" />
                  <asp:ImageButton ID="btThem" runat="server" OnClick="btThem_Click" ImageUrl="~/Icons/Sigma/Add_16X16_Standard.png" />
                </td>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="MyLable" style="width: 80px;">Doc ID</td>
                            <td class="MyContent">
                                <telerik:RadTextBox ID="txtDocID" Width="300" runat="server" ValidationGroup="Group1" TabIndex="12" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Doc Issue Place</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtDocIssuePlace" TabIndex="13" Width="250"  runat="server" ValidationGroup="Group1" />
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Doc Issue Date:</td>
                <td class="MyContent" width="300">
                    <telerik:RadDatePicker ID="rdpDocIssueDate" runat="server" MinDate="1900/1/1" TabIndex="14"></telerik:RadDatePicker>
                </td>

                <td class="MyLable" style="width: 80px;">Doc Expiry Date</td>
                <td class="MyContent">
                    <telerik:RadDatePicker ID="rdpDocExpiryDate" runat="server" TabIndex="15" ValidationGroup="Group1"></telerik:RadDatePicker>
                </td>
            </tr>
        </table>

        <div id="divDocType" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Doc Type:</td>
                    <td class="MyContent" width="300">
                        <telerik:RadComboBox ID="cmbInVisibleDocType"
                            MarkFirstMatch="True"
                            AllowCustomText="false"
                            HighlightTemplatedItems="true" TabIndex="16" Width="250" runat="server" ValidationGroup="Group1" />
                    </td>

                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="MyLable" style="width: 80px;">Doc ID</td>
                                <td class="MyContent">
                                    <telerik:RadTextBox ID="RadTextBox1" Width="300" runat="server" ValidationGroup="Group1" TabIndex="17" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Doc Issue Place</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="RadTextBox2" Width="250" runat="server" TabIndex="18" ValidationGroup="Group1" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Doc Issue Date:</td>
                    <td class="MyContent" width="300">
                        <telerik:RadDatePicker ID="RadDatePicker1" runat="server" TabIndex="19"></telerik:RadDatePicker>
                    </td>

                    <td class="MyLable" style="width: 80px;">Doc Expiry Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="RadDatePicker2" runat="server" TabIndex="20" ValidationGroup="Group1"></telerik:RadDatePicker>
                    </td>
                </tr>
            </table>
        </div>

        <hr />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Contact Person</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtContactPerson" Width="300" TabIndex="21" runat="server" ValidationGroup="Group1" />
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Position:</td>
                <td class="MyContent" width="300">
                    <telerik:RadTextBox ID="txtPosition" runat="server" TabIndex="22" ValidationGroup="Group1" />
                </td>
                <td class="MyLable" style="width: 80px;">Telephone</td>
                <td class="MyContent">
                    <telerik:RadMaskedTextBox ID="txtTelephone" runat="server" Mask="###########" 
                        EmptyMessage="-- Enter Phone Number --" HideOnBlur="true" ZeroPadNumericRanges="true" DisplayMask="###########">
                    </telerik:RadMaskedTextBox>
                </td>
            </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Email Address
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtEmailAddress" runat="server" TabIndex="24" ValidationGroup="Group1" width="300"/>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Remarks
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtRemarks" Width="300" runat="server" TabIndex="25" ValidationGroup="Group1" />
                </td>
            </tr>
        </table>

        <hr />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>
        <table>
            <tr>
                <td class="MyLable">Main Sector</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbMainSector"
                        MarkFirstMatch="True" TabIndex="26"
                        AllowCustomText="false"
                        Width="300" runat="server"  AppendDataBoundItems="true"
                        OnSelectedIndexChanged="cmbMainSector_SelectedIndexChanged"
                        AutoPostBack="true"
                        ValidationGroup="Group1">
                         <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Sector<span class="Required">(*)</span>
                    <asp:RequiredFieldValidator
                        runat="server" Display="None"
                        ID="RequiredFieldValidator8"
                        ControlToValidate="cmbSector"
                        ValidationGroup="Commit"
                        InitialValue=""
                        ErrorMessage="Sector is Required" ForeColor="Red">
                    </asp:RequiredFieldValidator></td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbSector"
                        MarkFirstMatch="True" TabIndex="27"
                        AllowCustomText="false" AppendDataBoundItems="true"
                        Width="300" runat="server" ValidationGroup="Group1">
                    </telerik:RadComboBox>
                </td>
            </tr>
           
            <tr>
                <td class="MyLable">Main Industry</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbMainIndustry" MarkFirstMatch="True"
                        AllowCustomText="false" TabIndex="28"
                        OnSelectedIndexChanged="cmbMainIndustry_SelectedIndexChanged"
                        AutoPostBack="true"
                        Width="300" runat="server" ValidationGroup="Group1">
                    </telerik:RadComboBox>
                </td>
            </tr>

            <tr>
                <td class="MyLable">Industry</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbIndustry"
                        MarkFirstMatch="True" AppendDataBoundItems="true"
                        AllowCustomText="false"
                        HighlightTemplatedItems="true" Width="300" TabIndex="29" runat="server" ValidationGroup="Group1">
                    </telerik:RadComboBox>
                </td>
            </tr> </table>
          
 </ContentTemplate>
            </asp:UpdatePanel>
            <table>
            <tr>
                <td class="MyLable">Target</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbTarget" Width="300"
                        MarkFirstMatch="True" TabIndex="30"
                        AllowCustomText="false"
                        runat="server" ValidationGroup="Group1">
                    </telerik:RadComboBox>
                </td>
            </tr>

            <%--<tr>
                <td class="MyLable">Customer Status</td>
                <td>
                    <telerik:RadComboBox ID="cmbCustomerStatus" MarkFirstMatch="True"
                        AllowCustomText="false" TabIndex="31"
                        Width="300" runat="server" ValidationGroup="Group1">
                    </telerik:RadComboBox>
                </td>
            </tr>--%>

            <tr>
                <td class="MyLable">Account Officer</td>
                <td>
                    <telerik:RadComboBox ID="cmbAccountOfficer" MarkFirstMatch="True"
                        AllowCustomText="false" TabIndex="32" AppendDataBoundItems="true"
                        Width="300" runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem value="" text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>

            <tr hidden="hidden">
                <td class="MyLable">Cif Code</td>
                <td>
                    <telerik:RadTextBox ID="txtCifCode" TabIndex="33" Width="300" runat="server" ValidationGroup="Group1" />
                </td>
            </tr>

            <tr>
                <td class="MyLable">Company Book</td>
                   <td >CHI NHANH TAN BINH</td>
            </tr>
        </table>

    </div>
   <div id="FurtherDetails">
          <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Contact Date</td>
                <td class="MyContent">
                    <telerik:RadDatePicker ID="rdpContactDate" runat="server" mindate="1/1/1900"></telerik:RadDatePicker>
                </td>
                </tr>
           </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Relation Code:</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbRelationCode" Width="300"
                        MarkFirstMatch="True" AppendDataBoundItems="true"
                        AllowCustomText="false" Height="150"
                        runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="1" Text="1 - HEAD OFFICE" />
                            <telerik:RadComboBoxItem Value="2" Text="2 - MOTHER COMPANY" />
                            <telerik:RadComboBoxItem Value="3" Text="3 - SISTER COMPANY" />
                            <telerik:RadComboBoxItem Value="4" Text="4 - FUND MANAGER" />
                            <telerik:RadComboBoxItem Value="5" Text="5 - INTRODUCER" />
                            <telerik:RadComboBoxItem Value="6" Text="6 - STOCKHOLDER OF" />
                            <telerik:RadComboBoxItem Value="7" Text="7 - SAME OWNER" />
                            <telerik:RadComboBoxItem Value="8" Text="8 - UNIQUE CLIENT" />
                            <telerik:RadComboBoxItem Value="9" Text="9 - SAME OWNER" />
                            <telerik:RadComboBoxItem Value="10" Text="10 - BRANCH" />
                            <telerik:RadComboBoxItem Value="11" Text="11 - BRANCH" />
                            <telerik:RadComboBoxItem Value="12" Text="12 - SUBSIDIARY/AFFLIATE" />
                            <telerik:RadComboBoxItem Value="13" Text="13 - SISTER COMPANY" />
                            <telerik:RadComboBoxItem Value="14" Text="14 - MANAGED CLIENT" />
                            <telerik:RadComboBoxItem Value="15" Text="15 - INTRODUCED CLIENT" />
                            <telerik:RadComboBoxItem Value="16" Text="16 - STOCK FROM" />
                            <telerik:RadComboBoxItem Value="17" Text="17 - SAME OWNER" />
                            <telerik:RadComboBoxItem Value="18" Text="18 - UNIQUE CLIENT" />
                            <telerik:RadComboBoxItem Value="21" Text="21 - PARENT" />
                            <telerik:RadComboBoxItem Value="22" Text="22 - SPOUSE" />
                            <telerik:RadComboBoxItem Value="23" Text="23 - SIBLING" />
                            <telerik:RadComboBoxItem Value="24" Text="24 - RELATIVE" />
                            <telerik:RadComboBoxItem Value="25" Text="25 - FRIEND" />
                            <telerik:RadComboBoxItem Value="26" Text="26 - EMPLOYER" />
                            <telerik:RadComboBoxItem Value="31" Text="31 - CHILD" />
                            <telerik:RadComboBoxItem Value="32" Text="32 - SPOUSE" />
                            <telerik:RadComboBoxItem Value="33" Text="33 - SIBLING" />
                            <telerik:RadComboBoxItem Value="34" Text="34 - RELATIVE" />
                            <telerik:RadComboBoxItem Value="35" Text="35 - FRIEND" />
                            <telerik:RadComboBoxItem Value="36" Text="36 - EMPLOYEE" />
                        </Items>
                        
                    </telerik:RadComboBox>
                </td>
            </tr>
            </table>

        <hr />
          <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Office number</td>
                <td class="MyContent">
                    <telerik:RadMaskedTextBox ID="txtOfficeNumber" runat="server" Mask="###########" 
                        EmptyMessage="-- Enter Phone Number --" HideOnBlur="true" ZeroPadNumericRanges="true" DisplayMask="###########">
                    </telerik:RadMaskedTextBox>
                </td>
             </tr>
           </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Total Capital</td>
                <td class="MyContent"><telerik:radnumerictextbox id="txtTotalCapital" NumberFormat-DecimalDigits="0" runat="server"></telerik:radnumerictextbox>
                </td>

                <td class="MyLable">No. of Employees</td>
                <td class="MyContent"><telerik:radnumerictextbox id="txtNoOfEmployee" NumberFormat-DecimalDigits="0" runat="server"></telerik:radnumerictextbox>
                </td>
             </tr>
           </table>
          <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Total Assets</td>
                <td class="MyContent"><telerik:radnumerictextbox id="txtTotalAssets" NumberFormat-DecimalDigits="0" runat="server"></telerik:radnumerictextbox>
                </td>
                </tr>
           </table>
                 <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Total Revenue</td>
                <td class="MyContent"><telerik:radnumerictextbox id="txtTotalRevenue" NumberFormat-DecimalDigits="2" runat="server"></telerik:radnumerictextbox>
                </td>
                </tr>
           </table>
        <hr />
         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">
                    Customer Liability
                </td>
                <td class="MyContent">
                    <telerik:radtextbox id="txtCustomerLiability" width="200" runat="server"></telerik:radtextbox>
                </td>

                <td class="MyLable">
                    Legacy Ref
                </td>
                <td class="MyContent">
                    <telerik:radtextbox id="txtLegacyRef" width="200" runat="server"></telerik:radtextbox>
                </td>
             </tr>
        </table>
    </div>

</div>
<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click"  text="Search"/>
</div>
<script type="text/javascript">
    $('#<%=txtId.ClientID%>').keyup(function (event) {
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
