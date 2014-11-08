<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewNormalLoan_Repayment.ascx.cs" Inherits="BankProject.Views.TellerApplication.NewNormalLoan_Repayment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="DotNetNuke.web" Namespace="DotNetNuke.Web.UI.WebControls" TagPrefix="dnn" %>
<telerik:radcodeblock runat="server">
<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs();
    })
</script>
    </telerik:radcodeblock>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>
<telerik:radtoolbar runat="server" id="RadToolBar1" enableroundedcorners="true" enableshadows="true" width="100%" onbuttonclick="RadToolBar1_ButtonClick" onclientbuttonclicking="RadToolBar1_OnClientButtonClicking">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit" 
            ToolTip="Commit Data" Value="btnCommit" CommandName="commit">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="CommitFull"  Visible="false"
            ToolTip="Commit Data" Value="btnCommit2" CommandName="commit2">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btnPreview" CommandName="Preview">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btnAuthorize" CommandName="authorize">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btnReverse" CommandName="reverse">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btnSearch" CommandName="search">
        </telerik:RadToolBarButton>
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btnPrint" CommandName="print">
        </telerik:RadToolBarButton>
    </Items>
</telerik:radtoolbar>
<div>
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
</div>
<div>

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 150px; padding: 5px 0 5px 20px;">


                <asp:TextBox Width="150" ID="tbNewNormalLoan" runat="server" ValidationGroup="Group1" OnTextChanged="tbNewNormalLoan_TextChanged" AutoPostBack="true" />

            </td>
        </tr>
    </table>
</div>
<div class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#Main">Main Info</a></li>
        <li><a id="A2" href="#Full">Full View</a></li>
    </ul>

    <div id="Main" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="Commit" />
        <fieldset>
            <legend style="text-transform: uppercase; font-weight: bold">Contract Details</legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Main Category:<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator1"
                            ControlToValidate="radcbMainCategory" ValidationGroup="Commit" InitialValue="" ErrorMessage="Main Category ID is required"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">

                        <telerik:radcombobox id="radcbMainCategory" runat="server" width="330px"
                            autopostback="true" appenddatabounditems="True"   emptymessage="- Select a category -" onselectedindexchanged="Radcbmaincategory_Selectedindexchanged">
            </telerik:radcombobox>
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
                <tr>
                    <td class="MyLable">Sub Category:<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator2"
                            ControlToValidate="rcbSubCategory" ValidationGroup="Commit" InitialValue="" ErrorMessage="Sub Category ID is required"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:radcombobox id="rcbSubCategory" runat="server" allowcustomtext="false" 
                            appenddatabounditems="True" markfirstmatch="true" width="330" emptymessage="- Select a sub category -">
                     <ExpandAnimation Type="None" />
                     <CollapseAnimation Type="None" />
                                       
                                     </telerik:radcombobox>
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
                <tr>
                    <td class="MyLable">Purpose Code:<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator3"
                            ControlToValidate="rcbPurposeCode" ValidationGroup="Commit" InitialValue="" ErrorMessage="Purpose Code is required"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:radcombobox id="rcbPurposeCode" runat="server" allowcustomtext="false"
                            appenddatabounditems="True" markfirstmatch="true" width="330">
                     <ExpandAnimation Type="None" />
                     <CollapseAnimation Type="None" />
                        
                         </telerik:radcombobox>
                    </td>

                </tr>
                <tr>
                    <td class="MyLable">Customer ID:<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator4"
                            ControlToValidate="rcbCustomerID" ValidationGroup="Commit" InitialValue="" ErrorMessage="Customer ID is required"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:radcombobox id="rcbCustomerID" autopostback="true" onselectedindexchanged="rcbCustomerID_SelectedIndexChanged"
                            appenddatabounditems="True" runat="server" width="330" allowcustomtext="false" markfirstmatch="true">
                                     <ExpandAnimation Type="None" />
                                     <CollapseAnimation Type="None" />
                                       <ItemTemplate>
                                           <%# DataBinder.Eval(Container.DataItem,"CustomerID") %> - 
                                           <%# DataBinder.Eval(Container.DataItem,"GBFullName") %>
                                           </ItemTemplate>                                    
                                 </telerik:radcombobox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Loan Group:</td>
                    <td class="MyContent">
                        <telerik:radcombobox id="rcbLoadGroup" runat="server" width="330"
                            appenddatabounditems="True" allowcustomtext="false" markfirstmatch="true">
                     <ExpandAnimation Type="None" />
                     <CollapseAnimation Type="None" />
                         <Items>
                             <telerik:RadComboBoxItem Value="" Text=""  />
                             
                         </Items>
                         </telerik:radcombobox>
                    </td>

                </tr>
                <%--         <table width="100%" cellpadding="0" cellspacing="0">--%>
                <tr>
                    <td class="MyLable">Currency:</td>
                    <td class="MyContent">
                        <telerik:radcombobox id="rcbCurrency" autopostback="True" width="330" onselectedindexchanged="rcbCurrency_SelectedIndexChanged" runat="server" allowcustomtext="false" markfirstmatch="true">
                                 <ExpandAnimation Type="None" />
                                 <CollapseAnimation Type="None" />
                                 <Items>
                                     <telerik:RadComboBoxItem Value="" Text="" />
                                     <telerik:RadComboBoxItem Value="EUR" Text="EUR" />
                                     <telerik:RadComboBoxItem Value="GBP" Text="GBP" />
                                     <telerik:RadComboBoxItem Value="JPY" Text="JPY" />
                                     <telerik:RadComboBoxItem Value="VND" Text="VND" />
                                     <telerik:RadComboBoxItem Value="USD" Text="USD" />
                                  
                                 </Items>
                             </telerik:radcombobox>
                    </td>

                    <td class="MyLable">Business Day:</td>
                    <td class="MyContent">
                        <telerik:radcombobox id="rcbBusDay" autopostback="True" width="159px"
                            appenddatabounditems="True"  runat="server" allowcustomtext="false" markfirstmatch="true">
                                 <ExpandAnimation Type="None" />
                                 <CollapseAnimation Type="None" />
                                 <ItemTemplate>
                                           <%# DataBinder.Eval(Container.DataItem,"MaQuocGia") %> - 
                                           <%# DataBinder.Eval(Container.DataItem,"TenTA") %>
                                           </ItemTemplate> 
                             </telerik:radcombobox>

                    </td>
                </tr>

                <tr>
                    <td class="MyLable">Loan Amount:<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator5"
                            ControlToValidate="tbLoanAmount" ValidationGroup="Commit" InitialValue="" ErrorMessage="Loan Amount is required"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:radtextbox id="tbLoanAmount" runat="server" validationgroup="Group1">
                                 <ClientEvents OnBlur="SetNumber" OnFocus="ClearCommas" />
                             </telerik:radtextbox>
                    </td>
                    <td class="MyLable">Approved Amt:</td>
                    <td class="MyContent">
                        <telerik:radnumerictextbox id="tbApprovedAmt" runat="server" validationgroup="Group1"></telerik:radnumerictextbox>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Open Date:<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator6"
                            ControlToValidate="rdpOpenDate" ValidationGroup="Commit" InitialValue="" ErrorMessage="Open Date is required"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:raddatepicker id="rdpOpenDate" runat="server" />
                    </td>
                    <td class="MyLable">Drawdown Date:</td>
                    <td class="MyContent">
                        <telerik:raddatepicker id="rdpDrawdown" runat="server" >
                            <ClientEvents OnDateSelected="DateSelected" />
                            </telerik:raddatepicker>
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Value Date:</td>
                    <td class="MyContent">
                        <telerik:raddatepicker id="rdpValueDate" runat="server" />
                    </td>
                    <td class="MyLable">Maturity Date:<span class="Required">(*)</span>
                         <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator9"
                            ControlToValidate="rdpMaturityDate" ValidationGroup="Commit" InitialValue="" ErrorMessage="Maturity Date is required"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:raddatepicker id="rdpMaturityDate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="MyLable">Credit to Account:</td>
                    <td class="MyContent">

                        <telerik:radcombobox width="330"
                            appenddatabounditems="True" autopostback="False"
                            id="rcbCreditToAccount" runat="server"
                            markfirstmatch="True" height="150px"
                            allowcustomtext="false">
                                     <ExpandAnimation Type="None" />
                                     <CollapseAnimation Type="None" />
                                 </telerik:radcombobox>
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>

                <tr>
                    <td class="MyLable">Limit Reference:<span class="Required">(*)</span>
                        <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator8"
                            ControlToValidate="rcbLimitReference" ValidationGroup="Commit" InitialValue="" ErrorMessage="Limit Reference:is required"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td class="MyContent">
                        <telerik:radcombobox id="rcbLimitReference" runat="server" width="330" allowcustomtext="false"
                            markfirstmatch="true" appenddatabounditems="true">
                                         <ExpandAnimation Type="None" />
                                         <CollapseAnimation Type="None" />
                                         <Items>
                                             <telerik:RadComboBoxItem Value="" Text="" />
                                             <telerik:RadComboBoxItem Value="1234.0010000.01" Text="1234.0010000.01" />
                                         </Items>
                                     </telerik:radcombobox>
                    </td>
                </tr>
            </table>

        </fieldset>
        <fieldset>
            <legend style="text-transform: uppercase; font-weight: bold">INTEREST Details</legend>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="MyLable">Rate Type:</td>
                    <td class="MyContent">
                        <telerik:radcombobox id="rcbRateType" runat="server" allowcustomtext="false" markfirstmatch="true"> 
                         <CollapseAnimation Type="None" />
                         <CollapseAnimation Type="None" />
                         <Items>
                             <telerik:RadComboBoxItem Value="1" Text="1 - Fixed for Balance" /> <%--Du no giam dan--%>
                             <telerik:RadComboBoxItem Value="2" Text="2 - Fixed for Initial" /> <%--Du no ban dau--%>
                             <telerik:RadComboBoxItem Value="3" Text="3 - Periodic Automatic" /> <%--Du no giam dan, truot lai xuat + Ins speed--%>
                         </Items>
                     </telerik:radcombobox>
                    </td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
                <tr>
                    <td class="MyLable">Interest Basis:</td>
                    <td class="MyContent"><i>366/360</i></td>
                    <td class="MyLable"></td>
                    <td class="MyContent"></td>
                </tr>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="MyLable">Int Pay Method:</td>
                        <td class="MyContent">
                            <asp:Label ID="lblIntPayMethod" Text="INTEREST BEARING" Width="150" runat="server" /></td>

                        <td class="MyLable">Interest Rate:<span class="Required">(*)</span>
                            <asp:RequiredFieldValidator runat="server" Display="None" ID="RequiredFieldValidator7"
                                ControlToValidate="tbInterestRate" ValidationGroup="Commit" InitialValue="" ErrorMessage="Interest Rate is required"
                                ForeColor="Red" /></td>

                        <td class="MyContent">
                            <telerik:radnumerictextbox id="tbInterestRate" runat="server" validationgroup="Group1" />
                        </td>
                    </tr>

                    <tr>
                        <td class="MyLable">Deposit Rate:</td>
                        <td class="MyContent">
                            <telerik:radcombobox id="rcbDepositeRate" runat="server" allowcustomtext="false" markfirstmatch="true" validationgroup="Group1">
                         <CollapseAnimation Type="None" />
                         <ExpandAnimation Type="None" />
                         <Items>
                             <telerik:RadComboBoxItem Value="" Text="" />
                         </Items>
                     </telerik:radcombobox>
                        </td>
                        <td class="MyLable">Int Spread:</td>
                        <td class="MyContent">
                            <telerik:radnumerictextbox id="tbInSpread" runat="server" validationgroup="Group1"></telerik:radnumerictextbox>
                        </td>
                    </tr>
                    <tr>

                        <td class="MyLable">Define Schedule (Y/N):</td>
                        <td class="MyContent">
                            <telerik:radcombobox id="rcbDefineSch" runat="server" allowcustomtext="false" markfirstmatch="true" validationgroup="Group1">
                             <CollapseAnimation Type="None" />
                             <ExpandAnimation Type="None" />
                             <Items>
                                 <telerik:RadComboBoxItem Value="" Text="" />
                                 <telerik:RadComboBoxItem Value="Y" Text="Yes" />
                                 <telerik:RadComboBoxItem Value="N" Text="No" />
                             </Items>
                         </telerik:radcombobox>
                        </td>
                        <td class="MyLable">Loan Status:<asp:Label ID="lbLoanStatus" runat="server" Text=""></asp:Label></td>
                        <td class="MyContent"></td>
                    </tr>
                    <tr>
                        <td class="MyLable">Total Interest Amt:</td>
                        <td class="MyContent">
                            <asp:Label ID="lbTotalInterestAmt" runat="server" Text=""></asp:Label></td>
                        <td class="MyLable">Past Due Amount:<asp:Label ID="lbPDStatus" runat="server" Text=""></asp:Label></td>
                        <td class="MyContent"></td>
                    </tr>

                </table>
        </fieldset>

    </div>
    
    <div id="Full" class="dnnClear">
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="CommitFull" />
        <p>&nbsp;</p>
        <table width="100%" cellpadding="0" cellspacing="0" >
            <tr>   
                <td style="width: 50px;" class="MyLable">
                    Outstandting Amount: <telerik:radnumerictextbox value="3" id="Radnumerictextbox1" runat="server"  width="150" />
                </td>            
                
                
            </tr>
        </table>
        <hr />
        
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <asp:ListView ID="lvLoanControl" runat="server" DataKeyNames="ID" InsertItemPosition="LastItem"
                    OnItemCanceling="lvLoanControl_ItemCanceling" OnItemDeleting="lvLoanControl_ItemDeleting" OnItemInserting="lvLoanControl_ItemInserting"
                    OnItemEditing="lvLoanControl_ItemEditing" OnItemUpdating="lvLoanControl_ItemUpdating">
                    <AlternatingItemTemplate>
                        <tr style="text-align: center">
                            <td style="visibility: hidden">
                                <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                            <td>
                                <asp:Label ID="TypeLabel" runat="server" Text='<%# Eval("Type") %>' />
                            </td>
                            <td>
                                <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="AmountActionLabel" runat="server" readonly="true" borderwidth="0" value='<%# Bind("AmountAction") %>'>
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td style="display:none" >
                                <asp:Label ID="RateLabel" runat="server" Text='<%# Eval("Rate") %>' />
                            </td>
                            <td style="display:none" >
                                <asp:Label ID="ChrgLabel" runat="server" Text='<%# Eval("Chrg") %>' />
                            </td>
                            <td>
                                <asp:Label ID="NoLabel" runat="server" Text='<%# Eval("No") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FreqLabel" runat="server" Text='<%# Eval("Freq_display") %>' />
                            </td>
                            <td>
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Delete_16X16_Standard.png" ID="Button3" runat="server" CommandName="Delete" Text="Delete" />&nbsp;&nbsp;&nbsp
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Edit_16X16_Standard.png" ID="Button4" runat="server" CommandName="Edit" Text="Edit" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EditItemTemplate>
                        <tr style="text-align: center">
                            <td style="visibility: hidden">
                                <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                            <td>
                                <asp:DropDownList ID="TypeTextBox" runat="server" SelectedValue='<%# Bind("Type") %>'>
                                    <asp:ListItem Selected="True"></asp:ListItem>
                                    <asp:ListItem>I</asp:ListItem>
                                    <asp:ListItem>P</asp:ListItem>
                                    <asp:ListItem>I+P</asp:ListItem>
                                    <asp:ListItem>AC</asp:ListItem>
                                    <asp:ListItem>EI</asp:ListItem>
                                    <asp:ListItem>EP</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                    ErrorMessage="Please choose Type"
                                    ControlToValidate="TypeTextBox" ForeColor="Red" Display="None"
                                    ValidationGroup="myVGInsert" />
                            </td>
                            <td>

                                <telerik:raddatepicker id="DateTextBox" runat="server" selecteddate='<%# Bind("Date") %>'>
                                </telerik:raddatepicker>
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="AmountActionTextBox" runat="server" value='<%# Bind("AmountAction") %>'>
                                </telerik:radnumerictextbox>
                            </td>
                            <td style="display:none" >
                                <telerik:radnumerictextbox id="RateTextBox" runat="server" value='<%# Bind("Rate") %>'>
                                </telerik:radnumerictextbox>

                            </td>
                            <td style="display:none" >
                                <asp:DropDownList ID="ChrgTextBox" runat="server" SelectedValue='<%# Bind("Chrg") %>'>
                                    <asp:ListItem Selected="True"></asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td>
                                <telerik:radnumerictextbox id="NoTextBox" runat="server" value='<%# Bind("No") %>'>
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                                <asp:DropDownList ID="FreqTextBox" runat="server" SelectedValue='<%# Bind("Freq") %>'>
                                    <asp:ListItem Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1">M</asp:ListItem>
                                    <asp:ListItem Value="2">2M</asp:ListItem>
                                    <asp:ListItem Value="3">3M</asp:ListItem>
                                    <asp:ListItem Value="4">4M</asp:ListItem>
                                    <asp:ListItem Value="5">5M</asp:ListItem>
                                    <asp:ListItem Value="6">6M</asp:ListItem>
                                    <asp:ListItem Value="7">7M</asp:ListItem>
                                    <asp:ListItem Value="8">8M</asp:ListItem>
                                    <asp:ListItem Value="9">9M</asp:ListItem>
                                    <asp:ListItem Value="10">10M</asp:ListItem>
                                    <asp:ListItem Value="11">11M</asp:ListItem>
                                    <asp:ListItem Value="12">12M</asp:ListItem>
                                    <asp:ListItem Value="E">E</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Save_16X16_Standard.png" ID="Button1" runat="server" CommandName="Update" Text="Update" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Cancel_16X16_Standard.png" ID="Button2" runat="server" CommandName="Cancel" Text="Clear" />
                            </td>
                        </tr>
                    </EditItemTemplate>
                    <EmptyDataTemplate>
                        <table runat="server" style="">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <InsertItemTemplate>
                        <tr style="text-align: center">
                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="myVGInsert" />
                            <td style="visibility: hidden">
                                <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                            <td>
                                <asp:DropDownList ID="TypeTextBox" runat="server" SelectedValue='<%# Bind("Type") %>'>
                                    <asp:ListItem Selected="True"></asp:ListItem>
                                    <asp:ListItem>I</asp:ListItem>
                                    <asp:ListItem>P</asp:ListItem>
                                    <asp:ListItem>I+P</asp:ListItem>
                                    <asp:ListItem>AC</asp:ListItem>
                                    <asp:ListItem>EI</asp:ListItem>
                                    <asp:ListItem>EP</asp:ListItem>
                                    <%--cho phép user định nghĩa số tiền cần phải trả trong Kỳ Cuối--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                    ErrorMessage="Please choose Type"
                                    ControlToValidate="TypeTextBox" ForeColor="Red" Display="None"
                                    ValidationGroup="myVGInsert" />
                            </td>
                            <td>

                                <telerik:raddatepicker id="DateTextBox" runat="server" selecteddate='<%# Bind("Date") %>'>
                                </telerik:raddatepicker>
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="AmountActionTextBox" runat="server" value='<%# Bind("AmountAction") %>'>
                                </telerik:radnumerictextbox>
                            </td>
                            <td style="display:none" >
                                <telerik:radnumerictextbox id="RateTextBox" runat="server" value='<%# Bind("Rate") %>'>
                                </telerik:radnumerictextbox>

                            </td>
                            <td style="display:none" >
                                <asp:DropDownList ID="ChrgTextBox" runat="server" SelectedValue='<%# Bind("Chrg") %>'>
                                    <asp:ListItem Selected="True"></asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td>
                                <telerik:radnumerictextbox id="NoTextBox" runat="server" value='<%# Bind("No") %>'>
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                                <asp:DropDownList ID="FreqTextBox" runat="server" SelectedValue='<%# Bind("Freq") %>'>
                                    <asp:ListItem Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1">M</asp:ListItem>
                                    <asp:ListItem Value="2">2M</asp:ListItem>
                                    <asp:ListItem Value="3">3M</asp:ListItem>
                                    <asp:ListItem Value="4">4M</asp:ListItem>
                                    <asp:ListItem Value="5">5M</asp:ListItem>
                                    <asp:ListItem Value="6">6M</asp:ListItem>
                                    <asp:ListItem Value="7">7M</asp:ListItem>
                                    <asp:ListItem Value="8">8M</asp:ListItem>
                                    <asp:ListItem Value="9">9M</asp:ListItem>
                                    <asp:ListItem Value="10">10M</asp:ListItem>
                                    <asp:ListItem Value="11">11M</asp:ListItem>
                                    <asp:ListItem Value="12">12M</asp:ListItem>
                                    <%--12M: Định kỳ trả 12 tháng--%>
                                    <asp:ListItem Value="E">E</asp:ListItem>
                                    <%--E:   Thanh toán Cuối kỳ--%>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Save_16X16_Standard.png" ID="InsertButton" ValidationGroup="myVGInsert" runat="server" CommandName="Insert" Text="Insert" />&nbsp;&nbsp;&nbsp
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Refresh_16x16_Standard.png" ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                            </td>
                        </tr>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <tr style="text-align: center">
                            <td style="visibility: hidden">
                                <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                            <td>
                                <asp:Label ID="TypeLabel" runat="server" Text='<%# Eval("Type") %>' />
                            </td>
                            <td>
                                <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />
                            </td>
                            <td>

                                <telerik:radnumerictextbox id="AmountActionLabel" runat="server" readonly="true" borderwidth="0" value='<%# Bind("AmountAction") %>'>
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td style="display:none" >
                                <asp:Label ID="RateLabel" runat="server" Text='<%# Eval("Rate") %>' />
                            </td>
                            <td style="display:none" >
                                <asp:Label ID="ChrgLabel" runat="server" Text='<%# Eval("Chrg") %>' />
                            </td>
                            <td>
                                <asp:Label ID="NoLabel" runat="server" Text='<%# Eval("No") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FreqLabel" runat="server" Text='<%# Eval("Freq_display") %>' />
                            </td>
                            <td>
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Delete_16X16_Standard.png" ID="Button5" runat="server" CommandName="Delete" Text="Delete" />&nbsp;&nbsp;&nbsp
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Edit_16X16_Standard.png" ID="Button6" runat="server" CommandName="Edit" Text="Edit" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                        <tr runat="server" style="">
                                            <th runat="server"></th>
                                            <th runat="server">Type <span class="Required">(*)</span></th>
                                            <th runat="server">Date</th>
                                            <th runat="server">Amount - Diary Action</th>
                                            <th runat="server" style="display:none" >Rate</th>
                                            <th runat="server" style="display:none" >Chrg</th>
                                            <th runat="server">No</th>
                                            <th runat="server">Frequency</th>
                                            <th runat="server"></th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style=""></td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <SelectedItemTemplate>
                        <tr style="text-align: center">
                            <td style="visibility: hidden">
                                <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                            <td>
                                <asp:Label ID="TypeLabel" runat="server" Text='<%# Eval("Type") %>' />
                            </td>
                            <td>
                                <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />
                            </td>
                            <td>
                                <%--<asp:Label ID="AmountActionLabel" runat="server" Text='<%# Eval("AmountAction") %>' />--%>
                                <telerik:radnumerictextbox id="AmountActionLabel" runat="server" readonly="true" borderwidth="0" value='<%# Bind("AmountAction") %>'>
                    <EnabledStyle HorizontalAlign="Right" />
                    </telerik:radnumerictextbox>
                            </td>
                            <td>
                                <asp:Label ID="RateLabel" runat="server" Text='<%# Eval("Rate") %>' />
                            </td>
                            <td>
                                <asp:Label ID="ChrgLabel" runat="server" Text='<%# Eval("Chrg") %>' />
                            </td>
                            <td>
                                <asp:Label ID="NoLabel" runat="server" Text='<%# Eval("No") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FreqLabel" runat="server" Text='<%# Eval("Freq_display") %>' />
                            </td>
                            <td>
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Delete_16X16_Standard.png" ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Edit_16X16_Standard.png" ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                            </td>
                        </tr>
                    </SelectedItemTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</div>

<telerik:radajaxmanager id="RadAjaxManager1" runat="server"
    defaultloadingpanelid="AjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="radcbMainCategory">
            <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="rcbSubCategory" />
            </UpdatedControls>
        </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="rcbCurrency">
            <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="rcbCreditToAccount" />
            <telerik:AjaxUpdatedControl ControlID="rcbPrinRepAccount" />
            <telerik:AjaxUpdatedControl ControlID="rcbIntRepAccount" />
            <telerik:AjaxUpdatedControl ControlID="rcbChargRepAccount" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tbExpectedLoss">
            <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="tbLossGiven" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ListView1">
            <UpdatedControls>
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:radajaxmanager>
<telerik:radcodeblock runat="server">
<script type="text/javascript" >

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
    var clickCalledAfterRadconfirm = false;
    function GenerateZero(k) {
        n = 1;
        for (var i = 0; i < k.length; i++) {
            n *= 10;
        }
        return n;
    }
    function addCommas(str) {
        var parts = (str + "").split("."),
            main = parts[0],
            len = main.length,
            output = "",
            i = len - 1;

        while (i >= 0) {
            output = main.charAt(i) + output;
            if ((len - i) % 3 === 0 && i > 0) {
                output = "," + output;
            }
            --i;
        }
        // put decimal part back
        //if (parts.length > 1) {
        //    console.log(parts[1]);
        //    output += "." + parts[1];
        //}

        return output + ".00";
    }
    function ClearCommas(sender, args) {
        var m = sender.get_value().replace(".00", "").replace(/,/g, '');
        console.log(m);
        sender.set_value(m);
    }
    function SetNumber(sender, args) {
        //sender.set_value(sender.get_value().toUpperCase());
        var number;
        var m = sender.get_value().substring(sender.get_value().length - 1);
        if (isNaN(m)) {
            var val = sender.get_value().substring(0, sender.get_value().length - 1).split(".");
            switch (m.toUpperCase()) {

                case "T":
                    var n1 = val[0] * 1000;
                    var n2 = 0;
                    if (val[1] != null)
                        n2 = (val[1] / GenerateZero(val[1])) * 1000;
                    number = n1 + n2;
                    sender.set_value(addCommas(number));
                    break;
                case "M":
                    var n1 = val[0] * 1000000;
                    var n2 = 0;
                    if (val[1] != null)
                        n2 = (val[1] / GenerateZero(val[1])) * 1000000;
                    number = n1 + n2;
                    sender.set_value(addCommas(number));
                    break;
                case "B":
                    var n1 = val[0] * 1000000000;
                    var n2 = 0;
                    if (val[1] != null)
                        n2 = (val[1] / GenerateZero(val[1])) * 1000000000;
                    number = n1 + n2;
                    sender.set_value(addCommas(number));
                    break;
                default:
                    alert("Character is not valid. Please use T, M and B character");
                    $find('<%=tbLoanAmount.ClientID %>').focus();
                    return false;
                    break;
            }
        } else {
            console.log("is number" + m);
            number = sender.get_value();
            sender.set_value(addCommas(number));

        }

    }
    function clickFullTab() {
        document.getElementById("linkFull").style.display = "";
        $('#tabs-demo').tabs({ selected: 2 });
    }
    function clickMainTab() {
        $('#tabs-demo').tabs({ selected: 0 });
    }
    function OnClientSelectedIndexChanged(sender, eventArgs) {
        var item = eventArgs.get_item();
        if (item.get_value() == 'N') {
            document.getElementById("collateralID").style.display = "none"
            document.getElementById("amountAllocID").style.display = "none"
        } else {
            document.getElementById("collateralID").style.display = ""
            document.getElementById("amountAllocID").style.display = ""
        }
    }
    function OnClientPrint() {


        //var r1 = confirm("Do you want to print Principle Payment schedule?");
        //if (r1 == true) {
        //    PrintVon();
        //}

        var r2 = confirm("Do you want to print Interesting Payment schedule?");
        if (r2 == true) {
            PrintLai();
        }





    }

    

    function GetRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement && window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    }
    function callRadAlertOnParent() {
        var oWnd = GetRadWindow();
        if (oWnd != null) {
            setTimeout(function () {
                GetRadWindow().BrowserWindow.radalert("my error message");
            }, 0);
        }
        else {
            radalert("my error message");
        }
    }
    function RadToolBar1_OnClientButtonClicking(sender, args) {

        //var button = args.get_item();
        //if (button.get_commandName() == "print" && !clickCalledAfterRadconfirm) {
        //    clickCalledAfterRadconfirm = true;
        //    args.set_cancel(true);
        //    //radconfirm("Do you want to print Principle Payment schedule?", PrintVon, 340, 150, null, 'Download');


        //}
    }

    function DateSelected(sender, eventArgs) {       
        //if (eventArgs.get_newValue() == "") {
        //    Disbursal.style.display = 'block';
        //    disA.style.display = 'block';
        //} else {
        //    Disbursal.style.display = 'none';
        //    disA.style.display = 'none';
        //}
    }
    function pageLoad() {
        LoadDrawdown();
    }
    function LoadDrawdown() {
        //var date = $find("<%= rdpDrawdown.ClientID %>");
        //if (date.get_selectedDate() == null) {
        //    Disbursal.style.display = 'block';
        //    disA.style.display = 'block';
        //} else {
        //    Disbursal.style.display = 'none';
        //    disA.style.display = 'none';
        // }
    }

  </script>

</telerik:radcodeblock>
