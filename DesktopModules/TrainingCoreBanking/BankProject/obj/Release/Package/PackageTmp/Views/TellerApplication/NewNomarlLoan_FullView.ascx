<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewNomarlLoan_FullView.ascx.cs" Inherits="BankProject.Views.TellerApplication.NewNomarlLoan_FullView" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ register Assembly="DotNetNuke.web" Namespace="DotNetNuke.Web.UI.WebControls" TagPrefix="dnn"%>
<%@ Register src="~/controls/LabelControl.ascx" TagPrefix="dnn" TagName="Label"%>

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
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:150px; padding: 5px 0 5px 20px;" >
                <asp:TextBox width="150" ID="tbNewNormalLoan" runat="server" ValidationGroup="Group1" />
               
                </td>
        </tr>
    </table>
</div>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#Full">Full View</a></li>
    </ul>

    <div id="Full" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="Commit" />
        <p></p>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MyLable">Forward/Backward Key <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator15"
                        ControlToValidate="tbForwardBackWard" ValidationGroup="Commit" InitialValue="" ErrorMessage="Forward/Backward Key is required"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="tbForwardBackWard" runat="server" ValidationGroup="Group1" />
                </td>
                <td class="MyLable"><i>FWD IN MONTH</i></td>
                <td class="MyLable">Base Date Key <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator14"
                        ControlToValidate="tbBaseDate" ValidationGroup="Commit" InitialValue="" ErrorMessage="Base Date Key is required"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="tbBaseDate" runat="server" ValidationGroup="Group1" />
                </td>
                <td class="MyLable"><i>Base Date</i> </td>
            </tr>
        </table>
        <hr />
        <table style="width: 100%; padding: 0px;">
            <tr>
                <th>Type <span class="Required">(*)</span>
                    <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator13"
                        ControlToValidate="rcbType" ValidationGroup="Commit" InitialValue="" ErrorMessage="Type is required"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </th>
                <th>Date</th>
                <th>Amount/Diary Action</th>
                <th>Rate</th>
                <th>Chrg</th>
                <th>No.</th>
                <th>Freq</th>
            </tr>
            <tr>
                <td class="MyContent">
                    <telerik:RadComboBox AppendDataBoundItems="True"
                        ID="rcbType" runat="server" Width="50px"
                        MarkFirstMatch="True"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                            <telerik:RadComboBoxItem Value="" Text="I" />
                            <telerik:RadComboBoxItem Value="" Text="P" />
                            <telerik:RadComboBoxItem Value="" Text="A" />
                            <telerik:RadComboBoxItem Value="" Text="B" />
                            <telerik:RadComboBoxItem Value="" Text="CF" />
                            <telerik:RadComboBoxItem Value="" Text="CFI" />
                            <telerik:RadComboBoxItem Value="" Text="CIF" />
                            <telerik:RadComboBoxItem Value="" Text="CP" />
                            <telerik:RadComboBoxItem Value="" Text="CPI" />
                            <telerik:RadComboBoxItem Value="" Text="D" />
                        </Items>
                    </telerik:RadComboBox>

                </td>
                <td class="MyContent">
                    <telerik:RadDatePicker ID="RadDatePicker1" Width="150px" runat="server" />
                </td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtAmountDiary" runat="server" ValidationGroup="Group1" />
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="txtRate" runat="server" ValidationGroup="Group1" />
                </td>
                <td class="MyContent">
                    <telerik:RadComboBox AppendDataBoundItems="True"
                        ID="rcbCharge" runat="server" Width="50px"
                        MarkFirstMatch="True"
                        AllowCustomText="false">
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="" Text="" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="50px"
                        ValidationGroup="Group1">
                        <NumberFormat DecimalDigits="0" />
                    </telerik:RadNumericTextBox>
                </td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="RadTextBox1" runat="server" ValidationGroup="Group1" />
                    <a class="add">
                        <img src="Icons/Sigma/Add_16X16_Standard.png" />
                    </a>
                </td>
            </tr>
        </table>
    </div>
</div>
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