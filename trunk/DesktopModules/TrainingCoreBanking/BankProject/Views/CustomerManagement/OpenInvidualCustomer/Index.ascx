
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Index.ascx.cs" Inherits="BankProject.TellerApplication.CustomerManagement.OpenInvidualCustomer.Index" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../../Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>


<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs({ selected: 0, cookieMilleseconds: 0 });
    });

    function OnMiddleNameChanged() {
        var firstNameElement = $find("<%= txtFirstName.ClientID %>");
        var firstName = firstNameElement.get_value();

        var firstMiddleElement = $find("<%= txtMiddleName.ClientID %>");
        var middleName = firstMiddleElement.get_value();

        var firstLastElement = $find("<%= txtLastName.ClientID %>");
     var lastName = firstLastElement.get_value();

     var gbShortNameLastElement = $find("<%= txtGBShortName.ClientID %>");
            var gbFullNameLastElement = $find("<%= txtGBFullName.ClientID %>");
        var fullName =  lastName + " " + middleName +" "+ firstName ;
        gbShortNameLastElement.set_value(fullName);
        gbFullNameLastElement.set_value(fullName);
        //gbShortNameLastElement.focus();
    }
</script>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
    ShowSummary="False" ValidationGroup="Commit" />

<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%"
        OnButtonClick="OnRadToolBarClick">
        <Items>
            <%--<telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
                ToolTip="Commit Data" Value="btdoclines" CommandName="doclines">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
                ToolTip="Preview" Value="btdocnew" CommandName="docnew">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
                ToolTip="Authorize" Value="btdraghand" CommandName="draghand">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
                ToolTip="Reverse" Value="btsearch" CommandName="search">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
                ToolTip="Search" Value="searchNew" CommandName="searchNew">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
                ToolTip="Print Deal Slip" Value="print" CommandName="print">
            </telerik:RadToolBarButton>--%>

            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit" Enabled ="false"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png" Enabled ="false"
            ToolTip="Preview" Value="btPreview" CommandName="preview">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png" Enabled ="false"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png" Enabled ="false"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse">
        </telerik:RadToolBarButton>
       <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png" Enabled ="false"
            ToolTip="Search" Value="btSearch" CommandName="search">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png" Enabled ="false"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print">
        </telerik:RadToolBarButton>
        </Items>
        
    </telerik:RadToolBar>   
</div>

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 200px; padding: 5px 0 5px 20px;">
            <asp:TextBox ID="txtId" runat="server" Width="200" TabIndex="1" />
            <i>
                <asp:Label ID="lblDepositCode" runat="server" /></i></td>
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
        <div class="dnnForm" id="Div1">
        </div>
        <hr />
        <div id="Div2" class="dnnClear">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">First Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtFirstName" runat="server" ValidationGroup="Group1" TabIndex="2" ClientEvents-OnValueChanged="OnMiddleNameChanged" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Last Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtLastName" runat="server" ValidationGroup="Group1" TabIndex="3" ClientEvents-OnValueChanged="OnMiddleNameChanged" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Middle Name</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtMiddleName" runat="server" ClientEvents-OnValueChanged="OnMiddleNameChanged" ValidationGroup="Group1" TabIndex="3" />
                    </td>
                </tr>
            </table>

            <table class="btc" width="100%" cellpadding="0" cellspacing="0">
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
                        <telerik:RadTextBox ID="txtGBShortName" Width="300" runat="server" ValidationGroup="Group1" TabIndex="4" />
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
                        <telerik:RadTextBox ID="txtGBFullName" Width="300" runat="server" ValidationGroup="Group1" TabIndex="5" />
                    </td>
                    <td><a class="add">
                        <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>

            <table class="btc" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Birthday<span class="Required">(*)</span>
                          <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator4"
                            ControlToValidate="rdpBirthDay"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Birthday is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="rdpBirthDay" runat="server" MinDate="1/1/1900" TabIndex="7"></telerik:RadDatePicker>
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
                            ControlToValidate="txtStreet"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="GB Street is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent" width="300">
                        <telerik:RadTextBox ID="txtStreet" Width="300" runat="server" ValidationGroup="Group1" TabIndex="8" />
                    </td>
                    <td><a class="add">
                        <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">GB Town/Dist.<span class="Required">(*)</span>
                          <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator6"
                            ControlToValidate="txtGBTownDist"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="GB Town/Dist. is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent" width="300">
                        <telerik:RadTextBox ID="txtGBTownDist" Width="300" runat="server" ValidationGroup="Group1" TabIndex="9" />
                    </td>
                    <td><a class="add">
                        <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Mobile Phone</td>
                    <td class="MyContent">
                        <telerik:RadMaskedTextBox ID="txtMobilePhone" runat="server" Mask="###########" 
                        EmptyMessage="-- Enter Phone Number --" HideOnBlur="true" ZeroPadNumericRanges="true" DisplayMask="###########">
                        </telerik:RadMaskedTextBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">City/Province
                        <span class="Required">(*)</span>
                           <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator8"
                            ControlToValidate="cmbCity"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="City/Province is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbCity" runat="server"
                            MarkFirstMatch="True"
                            AllowCustomText="false"
                            ValidationGroup="Group1"
                            AppendDataBoundItems="true"
                            OnItemDataBound="cmbCity_OnItemDataBound" TabIndex="11"
                            HighlightTemplatedItems="true" Width="250">
                        </telerik:RadComboBox>
                    </td>
                </tr>

            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">GB Country</td>
                    <td class="MyContent" width="250">
                        <telerik:RadComboBox ID="cmbCountry" Width="250"
                            MarkFirstMatch="True" TabIndex="12"
                            AllowCustomText="false" AppendDataBoundItems="true"
                            HighlightTemplatedItems="true" runat="server" ValidationGroup="Group1">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" /> 
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td><a class="add">
                        <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Nationality</td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbNationality"
                            MarkFirstMatch="True" AppendDataBoundItems="true"
                            AllowCustomText="false"
                            HighlightTemplatedItems="true" Width="250"
                             runat="server" ValidationGroup="Group1" TabIndex="13">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" /> 
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Residence</td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbRecidence"
                            MarkFirstMatch="True" AppendDataBoundItems="true"
                            AllowCustomText="false"
                            HighlightTemplatedItems="true" TabIndex="14" Width="250" runat="server" ValidationGroup="Group1">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" /> 
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
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
                            MarkFirstMatch="True" 
                            AllowCustomText="false"
                            HighlightTemplatedItems="true" TabIndex="15" Width="250" runat="server" ValidationGroup="Group1" />
                    </td>

                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="MyLable" style="width: 80px;">Doc ID</td>
                                <td class="MyContent">
                                    <telerik:RadTextBox ID="txtDocID" Width="300" runat="server" ValidationGroup="Group1" TabIndex="16" />
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
                        <telerik:RadTextBox ID="txtDocIssuePlace" Width="250" runat="server" ValidationGroup="Group1" TabIndex="17" />
                    </td>
                </tr>
            </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Doc Issue Date</td>
                    <td class="MyContent" width="300">
                        <telerik:RadDatePicker ID="rdpDocIssueDate" runat="server" MinDate="1/1/1900" TabIndex="18"></telerik:RadDatePicker>
                    </td>

                    <td class="MyLable" style="width: 80px;">Doc Expiry Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="rdpDocExpiry" runat="server" MinDate="1/1/1900" TabIndex="19" ValidationGroup="Group1"></telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Email Address</td>
                    <td class="MyContent">
                        <telerik:RadTextBox runat="server" id="tbEmailAddress" width="300" />
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td class="MyLable">Main Sector <span class="Required">(*)</span>
                           <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator102"
                            ControlToValidate="cmbMainSector"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Main Sector is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator> </td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbMainSector"
                            MarkFirstMatch="True"
                            AllowCustomText="false" enabled="false"
                            HighlightTemplatedItems="true" Width="300" runat="server" MinDate="1/1/1900" ValidationGroup="Group1" TabIndex="20">
                        </telerik:RadComboBox>
                    </td>
                </tr>
               

                <tr>
                    <td class="MyLable">SubSector<span class="Required">(*)</span>
                           <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator7"
                            ControlToValidate="cmbSubSector"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Sector is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbSubSector"
                            MarkFirstMatch="True"
                            AllowCustomText="false" enabled="false"
                            HighlightTemplatedItems="true" Width="300" TabIndex="21" runat="server" ValidationGroup="Group1">
                        </telerik:RadComboBox>
                    </td>
                </tr>
 </table>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
            <table>
                <tr>
                    <td class="MyLable">Main Industry <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator3"
                            ControlToValidate="cmbMainIndustry"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Sector is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbMainIndustry"
                            MarkFirstMatch="True"
                            AllowCustomText="false"
                            OnSelectedIndexChanged="cmbMainIndustry_SelectedIndexChanged"
                            
                            AutoPostBack="true"
                            HighlightTemplatedItems="true" TabIndex="22" Width="300" runat="server" ValidationGroup="Group1">
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Industry<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator
                            runat="server" Display="None"
                            ID="RequiredFieldValidator9"
                            ControlToValidate="cmbIndustry"
                            ValidationGroup="Commit"
                            InitialValue=""
                            ErrorMessage="Sector is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbIndustry"
                            MarkFirstMatch="True"
                            AllowCustomText="false"
                            HighlightTemplatedItems="true" Width="300" TabIndex="23" runat="server" ValidationGroup="Group1">
                        </telerik:RadComboBox>
                    </td>
                </tr>
 </table>
                </ContentTemplate>
</asp:UpdatePanel>
            <table>
                <tr>
                    <td class="MyLable">Target</td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbTarget" HighlightTemplatedItems="true"
                            MarkFirstMatch="True"
                            AllowCustomText="false"
                            TabIndex="24" Width="300" runat="server" ValidationGroup="Group1">
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Marital Status</td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbMaritalStatus" HighlightTemplatedItems="true"
                            MarkFirstMatch="True"
                            AllowCustomText="false"
                            TabIndex="25" Width="300" runat="server" ValidationGroup="Group1">
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Account Officer</td>
                    <td class="MyContent">
                        <telerik:RadComboBox ID="cmbAccountOfficer" AppendDataBoundItems="true" HighlightTemplatedItems="true"
                            MarkFirstMatch="True"
                            AllowCustomText="false"
                            TabIndex="26" Width="250" runat="server" ValidationGroup="Group1">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" /> 
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr hidden="hidden">
                    <td class="MyLable">Cif Code</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="txtCifCode" TabIndex="27" runat="server" ValidationGroup="Group1" />
                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Company Book</td>
                    <td >CHI NHANH TAN BINH</td>
                </tr>
            </table>
        </div>

    </div>
    <div id="FurtherDetails">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Gender</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbGender" runat="server"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        TabIndex="11"
                        HighlightTemplatedItems="true" Width="80">
                        <items>
                            <telerik:radcomboboxitem value="" text="" />
                            <telerik:radcomboboxitem value="Male" text="Male" />
                            <telerik:radcomboboxitem value="Female" text="Female" />
                        </items>
                    </telerik:RadComboBox>
                </td>
      </table>
        <table width="100%" cellpadding="0" cellspacing="0">
                <td class="MyLable">Title</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbTitle" runat="server"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        TabIndex="11"
                        HighlightTemplatedItems="true" Width="80">
                        <items>
                            <telerik:radcomboboxitem value="" text="" />
                            <telerik:radcomboboxitem value="Mr" text="Mr" />
                            <telerik:radcomboboxitem value="Ms" text="Ms" />
                            <telerik:radcomboboxitem value="Mrs" text="Mrs" />
                        </items>
                    </telerik:RadComboBox>
                </td>
            </tr>
      </table>

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
                <td class="MyLable">Relation Code</td>
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
                <td class="MyLable">Fax Number</td>
                <td class="MyContent">
                    <telerik:RadMaskedTextBox ID="txtFaxNumber" runat="server" Mask="###########"
                        EmptyMessage="-- Enter Phone Number --" HideOnBlur="true" ZeroPadNumericRanges="true" DisplayMask="###########">
                    </telerik:RadMaskedTextBox>
                </td>
             </tr>
           </table>

        <hr />
          <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">
                    No. of dependants
                </td>
                <td class="MyContent">
                    <telerik:radnumerictextbox id="txtNoOfDependants" NumberFormat-DecimalDigits="0" runat="server"></telerik:radnumerictextbox>
                </td>
             </tr>
           </table>

           <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">
                    No. Child (< 15 yrs old)
                </td>
                <td class="MyContent">
                    <telerik:radnumerictextbox id="txtNoChild15" NumberFormat-DecimalDigits="0" runat="server"></telerik:radnumerictextbox>
                </td>
             </tr>
           </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">
                    No. Child (15-25 yrs old)
                </td>
                <td class="MyContent">
                    <telerik:radnumerictextbox id="txtNoChild15_25" NumberFormat-DecimalDigits="0" runat="server"></telerik:radnumerictextbox>
                </td>
             </tr>
           </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">
                    No. Child (>25 yrs old)
                </td>
                <td class="MyContent">
                    <telerik:radnumerictextbox id="txtNoChild25" NumberFormat-DecimalDigits="0" runat="server"></telerik:radnumerictextbox>
                </td>
             </tr>
        </table>
         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Home Ownership</td>
                
                <td class="MyContent" style="width:80px;" >
                    <telerik:RadComboBox ID="rcbHomeOwnership"
                        MarkFirstMatch="True" 
                        AllowCustomText="false" width="80"
                        runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem Value="YES" Text="YES" />
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
        </table>

         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Residence Type</td>
                
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbResidenceType" runat="server" allowcustomtext="false" MarkFirstMatch="true" AppendDataBoundItems="true">
                        <Items>
                            <telerik:RadComboBoxItem value="" Text="" />
                            <telerik:RadComboBoxItem value="FARM.HOUSE" Text="FARM.HOUSE" />
                            <telerik:RadComboBoxItem value="INDEPENDENT.HOUSE" Text="INDEPENDENT.HOUSE" />
                            <telerik:RadComboBoxItem value="RESIDENTIAL.APT" Text="RESIDENTIAL.APT" />
                            <telerik:RadComboBoxItem value="SERVICED.APT" Text="SERVICED.APT" />
                            <telerik:RadComboBoxItem value="OTHER" Text="OTHER" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>


         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Employment Status</td>
                
                <td class="MyContent" style="width:80px;">
                    <telerik:RadComboBox ID="rcbEmployeementStatus"
                        MarkFirstMatch="True" 
                        AllowCustomText="false" width="80"
                        runat="server" ValidationGroup="Group1">
                        <Items>
                             <telerik:RadComboBoxItem Value="YES" Text="YES" />
                            <telerik:RadComboBoxItem Value="NO" Text="NO" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td><a class="add">
                    <img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
            </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Company's Name</td>
                <td class="MyContent"><telerik:radtextbox id="txtCompanyName" width="300" runat="server"></telerik:radtextbox>
                </td>
             </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Currency</td>
                
                <td class="MyContent">
                    <telerik:RadComboBox ID="cmbCurrency"
                        MarkFirstMatch="True" 
                        AllowCustomText="false"
                        runat="server" ValidationGroup="Group1">
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                            <telerik:RadComboBoxItem Value="USD" Text="USD" />
                            <telerik:RadComboBoxItem Value="GBP" Text="GBP" />
                            <telerik:RadComboBoxItem Value="JPY" Text="JPY" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>

         <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">
                    Monthly income
                </td>
                <td class="MyContent">
                    <telerik:radnumerictextbox id="txtMonthlyIncome" runat="server"></telerik:radnumerictextbox>
                </td>
             </tr>
        </table>

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">
                    Office Address
                </td>
                <td class="MyContent">
                    <telerik:radtextbox id="txtOfficeAddress" width="300" runat="server"></telerik:radtextbox>
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
                    <telerik:radtextbox id="txtCustomerLiability" width="300" runat="server"></telerik:radtextbox>
                </td>
             </tr>
        </table>
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
    $('#<%=txtId.ClientID%>').keyup(function (event) {
        if (event.keyCode == 13) { $("#<%=btSearch.ClientID%>").click(); }
    });
</script>
</telerik:RadCodeBlock>

<div style="visibility:hidden;">
    <asp:Button ID="btSearch" runat="server" Text="Search"  OnClick="btSearch_Click" />
</div>