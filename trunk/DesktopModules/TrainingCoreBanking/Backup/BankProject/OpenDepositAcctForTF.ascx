<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenDepositAcctForTF.ascx.cs" Inherits="BankProject.OpenDepositAcctForTF" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script> 
 <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" width="100%" OnButtonClick="RadToolBar1_ButtonClick" >
        <Items>
            <%--<telerik:RadToolBarButton Value="bplaybackprev" ImageUrl="~/Icons/bank/playback_prev_icon.png"
                 ToolTip="Back" CommandName="playbackprev">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/left_arrow.png"
                 ToolTip="Back" Value="btleftarrow"  CommandName="leftarrow">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/right_arrow.png"
                 ToolTip="Next Record" id="btrightarrow" CommandName="rightarrow">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/playback_next_icon.png"
                 ToolTip="Back" Value="btplaybacknext" CommandName="playbacknext">
            </telerik:RadToolBarButton>--%>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/doc_lines_icon.png"
                 ToolTip="Commit Data" Value="btdoclines" CommandName="doclines">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/doc_new_icon.png"
                 ToolTip="Back" Value="btdocnew" CommandName="docnew">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/cursor_drag_hand_icon.png"
                 ToolTip="Back" Value="btdraghand" CommandName="draghand">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
                 ToolTip="Back" Value="btsearch" CommandName="search">
            </telerik:RadToolBarButton>
        </Items>
    </telerik:RadToolBar>    
<div>
<table width="100%" cellpadding="0" cellspacing="0">
<tr>
    <td style="width:200px; padding:5px 0 5px 20px;"><asp:TextBox ID="tbDepositCode"  runat="server" Width="200" /> <i><asp:Label ID="lblDepositCode" runat="server" /></i></td>
</tr>
</table>
</div>
<div class="dnnForm" id="tabs-demo">
        <ul class="dnnAdminTabNav">
            <li><a href="#ChristopherColumbus">Open Account</a></li>
        </ul>
        <div id="ChristopherColumbus" class="dnnClear">
          <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                    <td class="MyLable">Customer ID</td>
                    <td class="MyContent">
                    <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td  Width="150"><telerik:RadComboBox AppendDataBoundItems="True"   AutoPostBack="true" 
                    OnSelectedIndexChanged="rcbCustomerID_SelectIndexChange"
                    ID="rcbCustomerID" Runat="server"
                    MarkFirstMatch="True" Width="150" Height="150px" 
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="" Text="" />
                    </Items>
                </telerik:RadComboBox><asp:Label ID="lblCustomerID" runat="server" /></td>
                        <td> <i><asp:Label ID="lblCustomer" runat="server" /></i></td>
                    </tr>
                    </table>
                     </td>
              </tr>
              <tr>
                    <td class="MyLable">Category Code</td>
                    <td class="MyContent"><telerik:RadComboBox 
                    ID="rcbCategoryCode" Runat="server"
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                        <telerik:RadComboBoxItem Value="Deposits for LC Openning" Text="1-121" />
                    </Items>
                </telerik:RadComboBox> <i>Deposits for LC Openning</i></td>
              </tr>
              <tr>
                    <td class="MyLable">Currentcy</td>
                    <td class="MyContent"><telerik:RadComboBox 
                    ID="rcbCurrentcy" Runat="server" OnClientSelectedIndexChanged="FillMnemonic" 
                    MarkFirstMatch="True" Width="150" Height="150px" 
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                        <telerik:RadComboBoxItem Value="USD" Text="USD" />
                        <telerik:RadComboBoxItem Value="EURO" Text="EURO" />
                    </Items>
                </telerik:RadComboBox></td>
              </tr>
              <tr>
                    <td class="MyLable">Account Name</td>
                    <td class="MyContent"><asp:TextBox ID="TextBox4" runat="server" Width="300" /></td>
              </tr>
              <tr>
                    <td class="MyLable">Short Name</td>
                    <td class="MyContent"><asp:TextBox ID="TextBox5" runat="server" Width="300" /></td>
              </tr>
              <tr>
                    <td class="MyLable">Account Mnemonic</td>
                    <td class="MyContent"><asp:TextBox ID="tbAccountMnemonic" runat="server" /></td>
              </tr>
              <tr>
                    <td class="MyLable">20.1 Product Line</td>
                    <td class="MyContent"> </td>
              </tr>
              </table>
              <div style="font-weight:bold; padding:10px 150px; text-transform:uppercase;">Join Account Infomation</div>
                <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                    <td class="MyLable">Joint Holder ID</td>
                    <td class="MyContent"><telerik:RadComboBox 
                    ID="rcbJointHolderID" Runat="server"
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                </telerik:RadComboBox></td>
              </tr>
              <tr>
                    <td class="MyLable">Relation Code</td>
                    <td class="MyContent"><telerik:RadComboBox 
                    ID="rcbRelationCode" Runat="server"
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                </telerik:RadComboBox></td>
              </tr>
              <tr>
                    <td class="MyLable">Notes</td>
                    <td class="MyContent"><asp:TextBox ID="TextBox10" runat="server"  Width="300"/></td>
              </tr>
              </table>
               <div style="font-weight:bold; padding:10px 150px; text-transform:uppercase; ">Audit Trail</div>
              <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                    <td class="MyLable">Override</td>
                    <td class="MyContent"></td>
              </tr>
              <tr>
                    <td class="MyLable">Record Status</td>
                    <td class="MyContent"></td>
              </tr>
              <tr>
                    <td class="MyLable">Curr No</td>
                    <td class="MyContent"><asp:Label ID="lblCurrNo" runat="server" /></td>
              </tr>
              <tr>
                    <td class="MyLable">Inputter</td>
                    <td class="MyContent"><asp:Label ID="lblInputter" runat="server" /></td>
              </tr>
              <tr>
                    <td class="MyLable">Date.Time</td>
                    <td class="MyContent"><asp:Label ID="lblDateTime" runat="server" /></td>
              </tr>
               <tr>
                    <td class="MyLable">Date.Time</td>
                    <td class="MyContent"><asp:Label ID="lblDateTime2" runat="server" /></td>
              </tr>
              <tr>
                    <td class="MyLable">Authoriser</td>
                    <td class="MyContent"><asp:Label ID="lblAuthoriser" runat="server" /></td>
              </tr>
              <tr>
                    <td class="MyLable">Co.Code</td>
                    <td class="MyContent"><asp:Label ID="lblCoCode" runat="server" /></td>
              </tr>
              <tr>
                    <td class="MyLable">Dept.Code</td>
                    <td class="MyContent"><asp:Label ID="lblDeptCode" runat="server" /></td>
              </tr>
              <tr>
                    <td class="MyLable">Auditor Code</td>
                    <td class="MyContent"><asp:Label ID="lblAuditorCode" runat="server" /></td>
              </tr>
          </table>
        </div>
        
  </div>
 
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
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
      function FillMnemonic() {
          var tbAccountMnemonic = document.getElementById("<%=tbAccountMnemonic.ClientID%>");
          var rcbCurrentcy = document.getElementById("<%=rcbCurrentcy.ClientID%>");
          var rcbCustomerID = document.getElementById("<%=rcbCustomerID.ClientID%>");
          tbAccountMnemonic.value = rcbCurrentcy.value + 'O' + rcbCustomerID.value;
      }
  </script>
  </telerik:RadCodeBlock>