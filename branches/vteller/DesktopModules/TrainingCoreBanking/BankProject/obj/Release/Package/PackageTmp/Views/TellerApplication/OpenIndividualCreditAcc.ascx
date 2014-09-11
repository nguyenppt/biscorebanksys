<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenIndividualCreditAcc.ascx.cs" Inherits="BankProject.Views.TellerApplication.OpenIndividualCreditAcc" %>
<%@ Register Assembly="Telerik.Web.UI " Namespace="Telerik.Web.UI" TagPrefix="telerik"%>
<%@ Register Assembly="BankProject" Namespace="BankProject.Controls" TagPrefix="customControl"%>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>


<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    });
</script>

<telerik:RadToolBar Runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" width="100%">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/doc_lines_icon.png" Tooltip="Commit Data" values="btdoclines"
            commandName="doclines"> 
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/doc_new_icon.png" ToolTip="back" values="btdocnew"
            commandName="docnew"></telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/cursor_drag_hand_icon.png" ToolTip="back" Values="btdraphand"
            Commandname="draphand"> </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png" ToolTip="Back" values="btsearch" CommandName="search">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>


<div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:200px; padding:5px 0 5px 20px ;">
                <asp:TextBox ID="txtID" runat="server" width="200" Text="1105369" />
                <i>
                    <asp:Label ID="lblCreditAccount" runat="server" />
                </i>
            </td>
        </tr>
    </table>
</div>

<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li ><a href="#BasicDetails">Basic Details</a> </li>
        <li><a href="#FurtherDetails">Further Details</a></li>
        <li><a href="#OrtherDetails">Orther Details</a></li>
        <li><a href="#Audit">Audit</a></li>
        <li><a href="#FullReview">Full Review</a></li>
    </ul>
    <div id="FurtherDetails"></div>
    <div id="OrtherDetails"> </div>
    <div id="Audit"></div>
    <div id="FullReview"></div>
    <div id="BasicDetails" class="dnnClear">
        <table>
        <tr>
            <td width="420px"> <hr style="size:10 ;color:red;" /> </td>
            <td width="90px" ><b>Basic Details</b></td>
            <td width="460px"> <hr style="size:10 ;color:red;" /> </td>
            
        </tr>
            <table width="100%" cellpading="0" cellspacing="0"> 
            <tr>
                <td class="MyLable">First Name:</td>
                <td class="MyContent"> 
                    <telerik:RadTextBox id="txtFirstName" width="200" RunAt="server" ValidationGroup="Group1" />                       
                </td>
            </tr>
                <tr>
                    <td class="MyLable">Last Name:</td>
                    <td class="MyContent">
                        <telerik:RadTextBox id="txtLastName" width="200px" Runat="server" ValidationGroup="Group1" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Middle Name:</td>
                    <td class="MyContent">
                        <telerik:RadTextBox id="txtMiddleName" width="200" Runat="server" ValidationGroup="Group1" />
                    </td>
                    </tr>
                <tr>
                    <td class="MyLable">GB Short Name:</td>
               <!--     <td class="MyContent"> <telerik:RadComboBox id="cbShortName" width="350" Runat="server"
                        MarkFirdtMatch="true" AllowCustomText="false"  validationGroup="Group1" /> 
                        <Items>
                            <telerik:RadComboBoxItem value="NGO THANH THAO" />
                            <telerik:RadComboBoxItem value="VO THU NGAN" />
                          </td>
                        </Items>   -->
                    <td class="MyContent" width="350"> <telerik:RadTextBox  id="txtShortName" width="350" runat="server" ValidationGroup="Group1" /> </td>
                    <td width="420"><a class="add"> <img src="Icons/Sigma/Add_16X16_Standard.png" /></a> </td>
                </tr>
                <tr>
                    <td class="MyLable">GB Full Name:</td>
                    <td class="MyContent" width="350">
                        <telerik:RadTextBox id="txtFullName" width="350" runat="server" ValidationGroup="Group1" />

                    </td>
                    <td width="420"><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a> </td>
                </tr>
                 
                <tr>
                    <td class="MyLable">Mnemonic:</td>
                    <td class="MyContent" width="320">
                        <telerik:RadTextBox id="txtMnemonic" width="150" runat="server" ValidationGroup="Group1" />

                    </td>
                    <td class="MyLable" >BirthDay:</td>
                    <td class="MyContent">
                        <customControl:CustomDataTimePicker id="TxtBirthDay"  Runat="server" ValidationGroup="Group1" />
                    </td>   
                   

                </tr>
                </table>
            
                <hr />
            <table width="100%" cellpading="0" cellspacing="0"> 
                <tr>
                    <td class="MyLable">GB Street:</td>
                    <td class="MyContent" width="420">
                        <telerik:RadTextBox id="txtGbStreet" width="350" runat="server" ValidationGroup="Group1" />
                    </td>
                    <td width="420"><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></td>
                </tr>
                <tr>
                    <td class="MyLable">GB Town/Dist:</td>
                    <td class="MyContent" width="420">
                        <telerik:RadTextBox id="txtGBTown" width="350" runat="server" validationGroup="Group1" />
                    </td>
                    <td width="420"><a class="add"><img src="Icons/Sigma/Add_16X16_Standard.png" /></a></td>

                </tr>
                <tr>
                    <td class="MyLable">Mobile Phone:</td>
                    <td class="MyContent">
                        <telerik:RadTextBox id="txtPhone" runat="server" ValidationGroup="Group1" width="150"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">City / Province:</td>
                    <td class="MyContent" width="150">
                   <!--     <telerik:RadComboBox id="cbCity" width="200" runat="server" ValidationGroup="Group1" MarkFirstMatch="true" AllowCustomText="false">
                        <Items>
                            <telerik:RadComboBoxItem value="5000" text="HO CHI MINH" />
                            <telerik:RadComboBoxItem value="5001" text="HA NOI" />
                            <telerik:RadComboBoxItem value="3200" text="CAN THO" />
                        </Items>
                            </telerik:RadComboBox>  -->

                        <telerik:RadComboBox ID="cmbCurrency"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        runat="server" ValidationGroup="Group1"> <ExpandAnimation Type="None" />
                        <CollapseAnimation Type="None" />
                        <Items>
                            <telerik:RadComboBoxItem Value="USD" Text="USD" />
                            <telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                            <telerik:RadComboBoxItem Value="GBP" Text="GBP" />
                            <telerik:RadComboBoxItem Value="JPY" Text="JPY" />
                            <telerik:RadComboBoxItem Value="VND" Text="VND" />
                        </Items>
                    </telerik:RadComboBox>

                    </td>
                </tr>
                <tr>
                    <td class="MyLable">GB Country:</td>
                    <td class="MyContent" width="450">
                        <telerik:RadTextBox id="txtCountry" width="150" runat="server" ValidationGroup="Group1" />
                    </td>
                    <td width="50"><a class="add"> <img src="Icons/Sigma/Add_16X16_Standard.png"</a></td>
                    <td class="MyLable">Nationality:</td>
                    <td class="MyContent">
                        <telerik:RadComboBox id="cbnationality" width="130" runat="server" validationGroup="Group1"
                            MarkFirstMatch="True"  AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="VIET NAM" Text="VN - VIET NAM" />
                                <telerik:RadComboBoxItem value="US" text="US - United Kingdom" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">
                        Residence:
                    </td>
                    <td class="MyContent">
                        <telerik:RadcomboBox id="RadComboBox1" width="150" runat="server" validationGroup="Group1"
                            MarkFirstMatch="True"  AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="VN" Text="VN - VIET NAM" />
                                <telerik:RadComboBoxItem value="US" text="US - United Kingdom" />
                            </Items>
                        </telerik:RadcomboBox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Doc Type:</td>
                    <td class="MyContent">
                        <telerik:RadComboBox id="cbDocType" runat="server" width="150" ValidationGroup="Group1"
                            MarkFirstMatch="True"  AllowCustomText="false">
                            <Items>
                                <telerik:RadComboBoxItem Value="DRIVING LICENSE" Text="DRIVING LICENSE" />
                                <telerik:RadComboBoxItem value="ESTAB.LIC.CODE" text="ESTAB.LIC.CODE" />
                                 <telerik:RadComboBoxItem value="NATIONAL.ID" text="NATIONAL.ID" />
                                <telerik:RadComboBoxItem value="OTHER.DOC" text="OTHER.DOC" />
                                <telerik:RadComboBoxItem value="PASSPORT" text="PASSPORT" />
                                <telerik:RadComboBoxItem value="TAX.CODE" text="TEXT.CODE" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td class="MyLable">Doc:</td>
                    <td class="MyContent">
                        <telerik:RadTextBox id="txtDoc" width="120" runat="server" ValidationGroup="Group1" />
                    </td>
                    <td width="50"><a class="add"> <img src="Icons/Sigma/Add_16X16_Standard.png"</a></td>
                </tr>
                <tr>
                    <td class="MyLable">Doc Issue Place:</td>
                    <td class="MyContent">
                        <teletik:RadTextBox id="DocIssPlace" width="350" runat="server" ValidationGroup="Group1" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Doc Issue Date</td>
                    <td class="MyContent">
                        <customControl:customDataTimePicker id="DocIssDate" runat="server" validationGroup="Group1"
                            width="" />
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
