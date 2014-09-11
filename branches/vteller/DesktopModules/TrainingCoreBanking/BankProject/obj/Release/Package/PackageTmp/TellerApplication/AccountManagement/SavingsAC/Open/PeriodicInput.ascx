<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PeriodicInput.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Open.PeriodicInput" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>
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
        <li><a href="#All in one Account">All in one Account</a></li>
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

            </table>
        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Product Infomation</div>
            </legend>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Product 
                        <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator 
                        runat="server" Display="None"
                        ID="RequiredFieldValidator2" 
                        ControlToValidate="rcbProduct" 
                                ValidationGroup="Commit"
                        InitialValue="" 
                        ErrorMessage="Product is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                   
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="rcbProduct" runat="server" 
                            MarkFirstMatch="True" Width="300" Height="150px"
                            onselectedindexchanged="rcbProduct_SelectedIndexChanged"
                            OnClientSelectedIndexChanged="Product_OnClientSelectedIndexChanged"
                            AutoPostBack="true"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="" />
                                <telerik:RadComboBoxItem Value="M01" Text="200 : Tradi FD - Periodic - Monthly" />
                                <telerik:RadComboBoxItem Value="M03" Text="300 : Tradi - FD-Periodic Quaterly" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
               
                <tr>
                    <td class="MyLable">Principal 
                        <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator 
                            runat="server" Display="None"
                            ID="RequiredFieldValidator3" 
                            ControlToValidate="radNumPrincipal" 
                                    ValidationGroup="Commit"
                            InitialValue="" 
                            ErrorMessage="Principal is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <%--<asp:TextBox ID="tbPrincipal" runat="server" Width="150" />--%>
                        <telerik:RadNumericTextBox runat="server" ID="radNumPrincipal" MinValue="0"    
                            MaxValue="999999999999" >
                            <NumberFormat GroupSeparator="," DecimalDigits="0" AllowRounding="true"   KeepNotRoundedValue="false"  />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Value Date</td>
                    <td class="MyContent">
                        <%--<customControl:CustomDataTimePicker ID="dtpValueDate1" Width="130" runat="server" MinDate="1900/1/1">
                            <ClientEvents OnDateSelected="dtpValueDate_DateSelected" />
                        </customControl:CustomDataTimePicker>--%>
                        <telerik:RadDatePicker ID="dtpValueDate" Width="130" runat="server" MinDate="1/1/1900" MaxDate="1/12/9999" DateInput-DateFormat="dd/MM/yyyy">
                            <ClientEvents OnDateSelected="dtpValueDate_DateSelected" />
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Term</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                            AutoPostBack="true"
                            onclientselectedindexchanged="rcbTerm_OnClientSelectedIndexChanged" 
                            ID="rcbTerm" runat="server" 
                            MarkFirstMatch="True" Width="70" Height="150px" 
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            
                        </telerik:RadComboBox>
                    </td>
                </tr>
                  </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Maturity Date</td>
                    <td class="MyContent">
                        <%--<customControl:CustomDataTimePicker ID="dtpMaturityDate1" Width="130" runat="server" MinDate="1900/1/1">
                            <ClientEvents OnDateSelected="dtpValueDate_DateSelected" />
                        </customControl:CustomDataTimePicker>--%>
                        <telerik:RadDatePicker ID="dtpMaturityDate" Width="130" runat="server" MinDate="1/1/1900" MaxDate="1/12/9999" DateInput-DateFormat="dd/MM/yyyy">
                            <ClientEvents OnDateSelected="dtpMaturityDate_DateSelected" />
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Interest Rate</td>
                    <td class="MyContent">
                        <asp:TextBox ID="tbInterestRate" runat="server" Width="50" Text="7.872"/></td>
                </tr>

            </table>
        </fieldset>

        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Payment Infomation</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Working Account 
                        <span class="Required">(*)</span>
                        <asp:RequiredFieldValidator 
                        runat="server" Display="None"
                        ID="RequiredFieldValidator1" 
                        ControlToValidate="rcbWorkingAcc" 
                                ValidationGroup="Commit"
                        InitialValue="" 
                        ErrorMessage="Working Account is Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:RadComboBox
                            ID="rcbWorkingAcc" runat="server" 
                            MarkFirstMatch="True" Width="150" Height="150px"
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="1" Text="07.000168836.4" />
                            </Items>
                        </telerik:RadComboBox>
                        <i><asp:Label runat="server" ID="lblWokingAccName"></asp:Label></i></td>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable" aria-readonly="True">Maturity Instr</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblMaturityInstr" text="AUTOMATIC ROLLOVER"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Schedules</div>
            </legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Schedules(Y/N)</td>
                    <td class="MyContent">
                        <telerik:RadComboBox onclientkeypressing="rcbSchedules_Onclientkeypressing" onclientblur="rcbSchedules_Onclientkeypressing"
                            ID="rcbSchedules" runat="server" 
                            MarkFirstMatch="True" Width="50" Height="150px" 
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="1" Text="Y" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Sch Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                            ID="rcbSchType" runat="server" 
                            MarkFirstMatch="True" Width="50" Height="150px" 
                            AllowCustomText="false">
                            <ExpandAnimation Type="None" />
                            <CollapseAnimation Type="None" />
                            <Items>
                                <telerik:RadComboBoxItem Value="0" Text="I" />
                                <telerik:RadComboBoxItem Value="1" Text="IN" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Frequency</td>
                    <td class="MyContent">
                        <telerik:RadTextBox ID="tbFrequency" runat="server" Width="200" Text=""/>
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
                        <asp:Label runat="server" ID="lblOverrides"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Record Status</td>
                    <td class="MyContent">
                        <asp:Label runat="server" ID="lblRecordStatus"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Curr Number</td>
                    <td class="MyContent">
                        <asp:Label ID="lblCurrNumber" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Inputter</td>
                    <td class="MyContent">
                        <asp:Label ID="lblInputter" runat="server" /></td>
                </tr>
                <tr>
                    <td class="MyLable">Authoriser</td>
                    <td class="MyContent">
                        <asp:Label ID="lblAuthoriser" runat="server" /></td>
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
        function rcbTerm_OnClientSelectedIndexChanged(sender, eventArgs) {
            var combo = $find('<%=rcbTerm.ClientID %>');
            var ValueDate = $find("<%= dtpValueDate.ClientID %>");
            var MaturityDate = $find("<%= dtpMaturityDate.ClientID %>");
            var frequency = $find("<%= tbFrequency.ClientID %>");

            var date = ValueDate.get_selectedDate();
            var numMonth = combo.get_selectedItem().get_value();
            var newDate = new Date(Date.parse(date, "yyyy/MM/dd"));
            //var newDate = new Date(Date.parse(date, "yyyy/MM/dd"));
            date.setMonth(newDate.getMonth() + Number(numMonth));
            //frequency.set_value(newDate.format("yyyyMMdd") + "M0310");
            MaturityDate.set_selectedDate(date);
            //changeFrequency();
        }
        function dtpValueDate_DateSelected(sender, eventArgs) {
            var combo = $find('<%=rcbTerm.ClientID %>');
            var ValueDate = $find("<%= dtpValueDate.ClientID %>");
            var MaturityDate = $find("<%= dtpMaturityDate.ClientID %>");

            var date = ValueDate.get_selectedDate();
            var numMonth = combo.get_selectedItem().get_value();
            if (Number(numMonth) > 0) {
                var numMonth = combo.get_selectedItem().get_value();
                var newDate = new Date(Date.parse(date, "yyyy/MM/dd"));
                newDate.setMonth(newDate.getMonth() + Number(numMonth));
                MaturityDate.set_selectedDate(newDate);
            }
            //changeFrequency();
        }
        function dtpMaturityDate_DateSelected(sender, eventArgs) {
            changeFrequency();
        }

        function changeFrequency() {
            var Frequency = $find('<%=tbFrequency.ClientID %>');
            var rcbProduct = $find('<%=rcbProduct.ClientID %>');

            var mmvaluedate = "";
            var dd = "";
            var mm = ""; //January is 0!
            var yyyy = "";
            var date = new Date(Date.parse($find("<%= dtpValueDate.ClientID %>").get_selectedDate(), "yyyy/MM/dd"));

            switch (rcbProduct.get_value())
            {
                case "M01":
                    date.setMonth(date.getMonth() + 1);
                    break;

                case "M03":
                    date.setMonth(date.getMonth() + 3);
                    break;
            }

            if (date != null) {
                var today = date;
                var dd = today.getDate();
                var mm = today.getMonth() + 1; //January is 0!
                var yyyy = today.getFullYear();
                if (dd < 10) {
                    dd = '0' + dd;
                }

                if (mm < 10) {
                    mm = '0' + mm;
                }

                var mmvaluedate = date.getDate();
                if (mmvaluedate < 10) {
                    mmvaluedate = '0' + mmvaluedate;
                }
            }

            Frequency.set_value(yyyy.toString() + mm.toString() + dd.toString() + rcbProduct.get_value() + mmvaluedate);
        }
        function Product_OnClientSelectedIndexChanged() {
            changeFrequency();
        }
        function rcbSchedules_Onclientkeypressing(sender, eventArgs) {
            var combo = $find('<%=rcbTerm.ClientID %>');
            var ValueDate = $find("<%= dtpValueDate.ClientID %>");
            var MaturityDate = $find("<%= dtpMaturityDate.ClientID %>");
            var frequency = $find("<%= tbFrequency.ClientID %>");

            var date = ValueDate.get_selectedDate();
            var numMonth = combo.get_selectedItem().get_value();
            var newDate = new Date(Date.parse(date, "yyyy/MM/dd"));
            newDate.setMonth(newDate.getMonth() + Number(numMonth));
            newDate.setDate(newDate.getDate() -1);
            frequency.set_value(newDate.format("yyyyMMdd") + "M0310");
            var cmbSchType = $find('<%=rcbSchType.ClientID %>');
            //alert(cmbSchType.get_selectedItem().get_value());
            cmbSchType.set_text("IN");
        }

        $("#<%=tbDepositCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btSearch.ClientID %>").click();
            }
       <%-- $("#<%=rcbSchedules.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=rcbSchType.ClientID %>").set_selectedItem(2);
        }--%>
      });
    </script>
</telerik:RadCodeBlock>
<div style="visibility: hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" />
</div>
