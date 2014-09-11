<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreclosureFinalInterestedAmount.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.SavingsAC.Close.Preclose.PreclosureFinalInterestedAmount" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl" %>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>
<telerik:RadToolBar runat="server" ID="RadToolBar1" 
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
        <li><a href="#Preclose">Pre_close</a></li>
    </ul>
    <div id="ChristopherColumbus" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />

        <%--<fieldset>
            <legend>
                <div style="font-weight: bold; text-transform: uppercase;">Customer Infomation</div>
            </legend>--%>

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
                    <td class="MyLable">Open Date 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblOpenDate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Maturity Date 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblMaturityDate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Principal 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblPrincipal"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Original Principal 
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblOrgPrincipal"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Interest Rate
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblInterestedRate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Value Date</td>
                    <td class="MyContent">
                        <telerik:RadDatePicker ID="dtpValueDate" Width="130" runat="server" MinDate="1/1/1900" MaxDate="1/12/9999" 
                            DateInput-DateFormat="dd/MM/yyyy" >
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Start Date
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblStartDate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">End Date
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblEndDate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Interest Rate
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblInterestRate"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Interest Amount
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblInterestAmount"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Start Date 2
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblStartDate2"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">End Date 2
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblEndDate2"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Interest Rate 2
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblInterestRate2"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Interest Amount 2
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblInterestAmount2"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Total in Amt
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblTotalInAmt"></asp:Label></td>
                </tr>
                <tr>
                    <td class="MyLable">Next Teller Tran
                    </td>
                    <td class="MyContent"><asp:Label runat="server" ID ="lblNextTeller"></asp:Label></td>
                </tr>
            </table>
        <%--</fieldset>--%>
        
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
        $(document).ready(function () {
            
            var PreclosureType = $('#<%= hdfPreclosureType.ClientID %>').val();
            var today = new Date();

            if (PreclosureType == "1") {
                var startDate1 = new Date(Date.parse(today, "dd/MM/yyyy"));
                startDate1.setMonth(today.getMonth() - 11);
                startDate1.setDate(startDate1.getDate() + 2);
                var endDate1 = new Date(Date.parse(startDate1, "dd/MM/yyyy"));
                endDate1.setMonth(startDate1.getMonth() + 10);

                var instRate1 = 17.16;
                var instAmt1 = Number(instRate1) * (5000000 / 12);

                $('#<%=lblStartDate.ClientID%>').text(startDate1.format("dd/MM/yyyy"));
                $('#<%=lblEndDate.ClientID%>').text(endDate1.format("dd/MM/yyyy"));

                $('#<%=lblInterestRate.ClientID%>').text(instRate1);
                $('#<%=lblInterestAmount.ClientID%>').text(instAmt1.format(2));

                $('#<%=lblStartDate2.ClientID%>').text(endDate1.format("dd/MM/yyyy"));

                $('#<%=lblEndDate2.ClientID%>').text(today.format("dd/MM/yyyy"));

                var instRate2 = 3;
                var numday = Math.ceil(Number(Math.abs(today - endDate1)) / 86400000);

                var instAmt2 = ((Number(instRate2) * (500000 / 12)) / 30) * numday;

                $('#<%=lblInterestRate2.ClientID%>').text("3.00");
                $('#<%=lblInterestAmount2.ClientID%>').text(instAmt2.format(2));

                //var totalInAmt = Math.round(instAmt1 + instAmt2);
                var totalInAmt = instAmt1 + instAmt2;
                $('#<%=lblTotalInAmt.ClientID%>').text(totalInAmt.format(2));
                //$('#<%=lblNextTeller.ClientID%>').text("");


            } else {
                var startDate1 = new Date(Date.parse(today, "dd/MM/yyyy"));
                startDate1.setMonth(today.getMonth() - 1);
                startDate1.setDate(startDate1.getDate() + 2);
                var endDate1 = new Date(Date.parse(today, "dd/MM/yyyy"));


                var instRate1 = 3;
                var numday = Math.ceil(Number(Math.abs(endDate1 - startDate1)) / 86400000);
                var instAmt1 = ((Number(instRate1) * (2500000 / 12)) / 30) * numday;

                $('#<%=lblStartDate.ClientID%>').text(startDate1.format("dd/MM/yyyy"));
                $('#<%=lblEndDate.ClientID%>').text(endDate1.format("dd/MM/yyyy"));

                $('#<%=lblInterestRate.ClientID%>').text(instRate1);
                $('#<%=lblInterestAmount.ClientID%>').text(instAmt1.format(2));

                $('#<%=lblStartDate2.ClientID%>').text("");
                $('#<%=lblEndDate2.ClientID%>').text("");
                $('#<%=lblInterestRate2.ClientID%>').text("");
                $('#<%=lblInterestAmount2.ClientID%>').text("");
                var totalInAmt = instAmt1;
                $('#<%=lblTotalInAmt.ClientID%>').text(totalInAmt.format(2));
                //$('#<%=lblNextTeller.ClientID%>').text("");
            }

            $('#<%= hdfTotalAmt.ClientID %>').val(totalInAmt);
        });

        $("#<%=tbDepositCode.ClientID %>").keyup(function (event) {
            if (event.keyCode == 13) {
                $("#<%=btSearch.ClientID %>").click();
            }
        });

        function addCommas(nStr) {
            nStr += '';
            x = nStr.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            }
            return x1 + x2;
        };

        /**
         * Number.prototype.format(n, x)
         * 
         * @param integer n: length of decimal
         * @param integer x: length of sections
         */
        Number.prototype.format = function (n, x) {
            var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
            return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&,');
        };
    </script>
</telerik:RadCodeBlock>
<div style="visibility: hidden;">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" />
</div>
<asp:HiddenField ID="hdfPreclosureType" runat="server" Value="0" />
<asp:HiddenField ID="hdfTotalAmt" runat="server" Value="0" />