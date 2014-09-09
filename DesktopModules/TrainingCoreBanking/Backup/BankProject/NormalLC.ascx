<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NormalLC.ascx.cs" Inherits="BankProject.NormalLC" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
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
    <table width="100%" cellpadding="0" cellspacing="0">
<tr>
    <td style="width:200px; padding:5px 0 5px 20px;"><asp:TextBox ID="tbEssurLCCode" runat="server" Width="200" /></td>
</tr>
</table>
    <div class="dnnForm" id="tabs-demo">
        <ul class="dnnAdminTabNav">
            <li><a href="#Main">Main</a></li>
            <li><a href="#MT700">MT700</a></li>
            <li><a href="#MT740">MT740</a></li>
            <li><a href="#Charges">Charges</a></li>
            <li><a href="#DeliveryAudit">Delivery Audit</a></li>
        </ul>
        <div id="Main" class="dnnClear">
             <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                    <td class="MyLable">2 LC Type</td>
                    <td class="MyContent">
                        <telerik:RadComboBox 
                        ID="rcbLCType" Runat="server"  width="355"
                        MarkFirstMatch="True"  OnItemDataBound="rcbLCType_ItemDataBound"
                        AllowCustomText="false" >
                        <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <HeaderTemplate>
                            <table style="width:305px" cellpadding="0" cellspacing="0"> 
                              <tr> 
                                 <td style="width:60px;"> 
                                    LC Type 
                                 </td> 
                                 <td style="width:200px;"> 
                                    Description
                                 </td> 
                                 <td > 
                                    Category
                                 </td> 
                              </tr> 
                           </table> 
                       </HeaderTemplate>
                        <ItemTemplate>
                        <table style="width:305px"  cellpadding="0" cellspacing="0"> 
                          <tr> 
                             <td style="width:60px;"> 
                                <%# DataBinder.Eval(Container.DataItem, "LCTYPE")%> 
                             </td> 
                             <td style="width:200px;"> 
                                <%# DataBinder.Eval(Container.DataItem, "Description")%> 
                             </td> 
                             <td > 
                                <%# DataBinder.Eval(Container.DataItem, "Category")%> 
                             </td> 
                          </tr> 
                       </table> 
               </ItemTemplate>
                    </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">9 Applicant ID</td>
                    <td class="MyContent">
                    <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td  Width="150"><telerik:RadComboBox AppendDataBoundItems="True"   AutoPostBack="true" 
                    OnSelectedIndexChanged="rcbApplicantID_SelectIndexChange"
                    ID="rcbApplicantID" Runat="server"
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
                    <td class="MyLable">10.1 Applicant Addr</td>
                    <td class="MyContent"><asp:TextBox ID="TextBox4" runat="server" Width="300" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">10.2 Applicant Addr</td>
                    <td class="MyContent"><asp:TextBox ID="TextBox2" runat="server" Width="300" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">10.3 Applicant Addr</td>
                    <td class="MyContent"><asp:TextBox ID="TextBox3" runat="server" Width="300" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">11 Applicant Acct</td>
                    <td class="MyContent"><telerik:RadComboBox 
                    ID="rcbApplicant" Runat="server" 
                    MarkFirstMatch="True" Width="150" 
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">20 Ccy, Amount</td>
                    <td class="MyContent"><telerik:RadComboBox 
                    ID="rcbCurrentcy" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                        <telerik:RadComboBoxItem Value="USD" Text="USD" />
                        <telerik:RadComboBoxItem Value="EURO" Text="EURO" />
                    </Items>
                </telerik:RadComboBox>
                <telerik:radnumerictextbox incrementsettings-interceptarrowkeys="true" incrementsettings-interceptmousewheel="true"  runat="server" id="Numeric1" width="200px" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">52 Cr.Tolerance</td>
                    <td class="MyContent">
                    <telerik:radnumerictextbox incrementsettings-interceptarrowkeys="true" incrementsettings-interceptmousewheel="true"  runat="server" id="Numeric2" width="80px" />
                     51 Dr.Tolerance <telerik:radnumerictextbox incrementsettings-interceptarrowkeys="true" incrementsettings-interceptmousewheel="true"  runat="server" id="Numeric3" width="80px" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">27 Issuing Date</td>
                    <td class="MyContent">
                    <telerik:RadDatePicker runat="server" ID="StartDate">
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">160 Expiry Date</td>
                    <td class="MyContent">
                     <telerik:RadDatePicker runat="server" ID="StartDate1">
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">29 Expiry Place</td>
                    <td class="MyContent">
                    <telerik:RadComboBox 
                    ID="rcbExpiryPlace" Runat="server" 
                    MarkFirstMatch="True" Width="150"
                    AllowCustomText="false" >
                    <ExpandAnimation Type="None" />
                    <CollapseAnimation Type="None" />
                    <Items>
                    <telerik:RadComboBoxItem Value="" Text="" />
                        <telerik:RadComboBoxItem Value="USA" Text="US" />
                    </Items>
                </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">28 Contingent Expiry</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">63.1 Pay Type</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">64.1 Payment pCt</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">65.1 Payment Portion</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">66.1 Accpt Time Band</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">74.1 Limit Ref</td>
                    <td class="MyContent">
                    </td>
                </tr>
                 </table>
              <div style="font-weight:bold; padding:10px 150px; text-transform:uppercase;">Beneficiary Details</div>
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">12 Beneficiary No</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">13.1 Beneficiary Name-Addr</td>
                    <td class="MyContent">
                    </td>
                </tr>
                </table>
              <div style="font-weight:bold; padding:10px 150px; text-transform:uppercase;">Advising/Reimbursing Bank Details</div>
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">3 Advise Bank Ref</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">15 Advise Bank No</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">16.1 Advise Bank Addr</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">17 Advise Bank Acct</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">44 Reimb. Bank No</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">45.1 Reimb Bank Addr</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">46 Reimb. Bank Acct</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">47 Advise Thru No.</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">48.1 Advise Thru Addr</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">49 Advise Thru Acct</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">55 Avail With No.</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">56.1 Avail With Name.Addr</td>
                    <td class="MyContent">
                    </td>
                </tr>
                </table>
              <div style="font-weight:bold; padding:10px 150px; text-transform:uppercase;">Commodity/Prov Details</div>
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">176.12 Commodity</td>
                    <td class="MyContent">
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">176.22 Prov %</td>
                    <td class="MyContent">
                    </td>
                </tr>
            </table>
        </div>
        <div id="MT700" class="dnnClear">
        </div>
        <div id="MT740" class="dnnClear">
        </div>
        <div id="Charges" class="dnnClear">
        </div>
        <div id="DeliveryAudit" class="dnnClear">
        </div>
  </div>
  <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
    DefaultLoadingPanelID="AjaxLoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbApplicantID">
            <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="lblCustomer" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
    </AjaxSettings>
</telerik:RadAjaxManager>
  <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
  <script type="text/javascript">
      function SetValue() {
          var rcbApplicantID = document.getElementById("<%=rcbApplicantID.ClientID%>");
          $("#spCustomerID").html(rcbApplicantID.value);
      }
  </script>
  </telerik:RadCodeBlock>