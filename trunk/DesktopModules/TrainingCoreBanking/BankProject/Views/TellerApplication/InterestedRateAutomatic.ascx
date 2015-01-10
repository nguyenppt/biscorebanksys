<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InterestedRateAutomatic.ascx.cs" Inherits="BankProject.Views.TellerApplication.InterestedRateAutomatic" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ register Assembly="DotNetNuke.web" Namespace="DotNetNuke.Web.UI.WebControls" TagPrefix="dnn"%>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>
<div class="dnnForm" id="tabs-demo">
    <div id="IntestedRate" class="dnnClear">      
        <p>&nbsp;</p>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ListView ID="lvLoanInterestedRate" runat="server" DataKeyNames="ID" InsertItemPosition="LastItem" 
                    OnItemInserting="lvLoanInterestedRate_ItemInserting" OnItemCanceling="lvLoanInterestedRate_ItemCanceling"
                    OnItemEditing="lvLoanInterestedRate_ItemEditing" OnItemUpdating="lvLoanInterestedRate_ItemUpdating"
                    OnItemDeleting="lvLoanInterestedRate_ItemDeleting"> 
                    <LayoutTemplate>
                        <table id="Table2" runat="server" >
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                        <tr id="Tr2" runat="server" style="border:double">
                                            <th id="Th1" runat="server" ></th>
                                            <th id="Th2" runat="server" style="width:50px;">Deposite Rate (M)</th>
                                            <th id="Th3" runat="server">Display</th>
                                            <th id="Th4" runat="server" style="width:100px;" >Interested Rate (VND)</th>
                                            <th id="Th5" runat="server" style="width:100px;">Interested Rate (USD)</th>
                                            <th id="Th6" runat="server"></th>
                                        </tr>
                                        
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                                <td id="Td2" runat="server" style=""></td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                     <EmptyDataTemplate>
                        <table id="Table1" runat="server" style="">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr style="text-align: center">
                            <td style="visibility: hidden">
                                <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lbMonthsRate" runat="server" Text='<%# Eval("MonthLoanRateNo") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lbDisplay" runat="server" style="text-align:left;" Text='<%# Eval("LoanInterest_Key") %>' />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="rnbVNDRate" runat="server" Width="80px" readonly="true" borderwidth="0"  DbValue='<%# Bind("VND_InterestRate") %>'>
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="rnbUSDRate" runat="server" Width="80px" readonly="true" borderwidth="0" DbValue='<%# Bind("USD_InterestRate") %>'>
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Edit_16X16_Standard.png" ID="Button6" runat="server" CommandName="Edit" Text="Edit" />
                                &nbsp;&nbsp;&nbsp
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Delete_16X16_Standard.png" ID="Button5" runat="server" CommandName="Delete" Text="Delete" />
                            </td>
                        </tr>
                    </ItemTemplate>

                    <AlternatingItemTemplate>
                        <tr style="text-align: center">
                            <td style="visibility: hidden">
                                <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lbMonthsRate" runat="server" Text='<%# Eval("MonthLoanRateNo") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lbDisplay" runat="server" Text='<%# Eval("LoanInterest_Key") %>' />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="rnbVNDRate" runat="server" Width="80px" readonly="true" borderwidth="0" DbValue='<%# Eval("VND_InterestRate") %>'>
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:radnumerictextbox>       
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="rnbUSDRate" runat="server" Width="80px" readonly="true" borderwidth="0" DbValue='<%# Bind("USD_InterestRate") %>'>
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Edit_16X16_Standard.png" ID="Button6" runat="server" CommandName="Edit" Text="Edit" />
                                &nbsp;&nbsp;&nbsp
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Delete_16X16_Standard.png" ID="Button5" runat="server" CommandName="Delete" Text="Delete" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>

                    <EditItemTemplate>
                        <tr style="text-align: center">
                            <td style="visibility: hidden">
                                <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' />
                            </td>

                            <td>
                                <telerik:radnumerictextbox id="radMonthRate" runat="server" width="50px" numberformat-decimaldigits="0" readonly="true" borderwidth="0" DbValue='<%# Bind("MonthLoanRateNo") %>'>
                                    <EnabledStyle HorizontalAlign="Center" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                                <asp:TextBox Width="150" ID="tbRateDisplay" runat="server" Text='<%# Bind("LoanInterest_Key") %>' />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="rnbVNDRate" runat="server" width="80px" DbValue='<%# Bind("VND_InterestRate") %>'>
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="rnbUSDRate" runat="server"  width="80px" DbValue='<%# Bind("USD_InterestRate") %>'>
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:radnumerictextbox>
                            </td>

                            <td>
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Save_16X16_Standard.png" ID="Button1" runat="server" CommandName="Update" Text="Update" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Cancel_16X16_Standard.png" ID="Button2" runat="server" CommandName="Cancel" Text="Clear" />
                            </td>
                        </tr>
                    </EditItemTemplate>
                   
                    <InsertItemTemplate>
                        <tr style="text-align: center">
                            <td style="visibility: hidden">
                                <asp:Label ID="lbID" runat="server" width="0px" Text='<%# Eval("ID") %>' />
                            </td>

                            <td>
                                <telerik:radnumerictextbox id="radMonthRate" width="50px" numberformat-decimaldigits="0" runat="server" DbValue='<%# Bind("MonthLoanRateNo") %>'>
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                                <span class="riSingle RadInput RadInput_Default">
                                <asp:TextBox Width="150" ID="tbRateDisplay" runat="server" CssClass="riTextBox riEnabled"  Text='<%# Bind("LoanInterest_Key") %>' />
                                    </span>
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="rnbVNDRate" runat="server" width="80px"   DbValue='<%# Bind("VND_InterestRate") %>'>
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="rnbUSDRate" runat="server" width="80px"   DbValue='<%# Bind("USD_InterestRate") %>'>
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:radnumerictextbox>
                            </td>

                            <td>
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Save_16X16_Standard.png" ID="Button1" runat="server" CommandName="Insert" Text="Insert" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Cancel_16X16_Standard.png" ID="Button2" runat="server" CommandName="Cancel" Text="Cancle" />
                            </td>
                        </tr>
                    </InsertItemTemplate>
                    
                    
                    <SelectedItemTemplate>
                        <tr style="text-align: center">
                            <td style="visibility: hidden">
                                <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lbMonthsRate" runat="server" Text='<%# Eval("MonthLoanRateNo") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lbDisplay" runat="server" Text='<%# Eval("LoanInterest_Key") %>' />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="rnbVNDRate" runat="server" Width="50px" readonly="true" borderwidth="0" DbValue='<%# Bind("VND_InterestRate") %>'>
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="rnbUSDRate" runat="server" Width="50px" readonly="true" borderwidth="0" DbValue='<%# Bind("USD_InterestRate") %>'>
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Delete_16X16_Standard.png" ID="Button5" runat="server" CommandName="Delete" Text="Delete" />&nbsp;&nbsp;&nbsp
                                <asp:ImageButton ImageUrl="~/Icons/Sigma/Edit_16X16_Standard.png" ID="Button6" runat="server" CommandName="Edit" Text="Edit" />
                            </td>
                        </tr>
                    </SelectedItemTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</div>

