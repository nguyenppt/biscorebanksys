<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RateExchange.ascx.cs" Inherits="BankProject.Views.TellerApplication.RateExchange" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ register Assembly="DotNetNuke.web" Namespace="DotNetNuke.Web.UI.WebControls" TagPrefix="dnn"%>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"> </telerik:RadWindowManager>
<div class="dnnForm" id="tabs-demo">
    <div id="IntestedRate" class="dnnClear">      
        <p>&nbsp;</p>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ListView ID="lvRateExchange" runat="server" DataKeyNames="ID" InsertItemPosition="LastItem" 
                    OnItemInserting="lvRateExchange_ItemInserting" OnItemCanceling="lvRateExchange_ItemCanceling"
                    OnItemEditing="lvRateExchange_ItemEditing" OnItemUpdating="lvRateExchange_ItemUpdating"
                    OnItemDeleting="lvRateExchange_ItemDeleting"> 
                    <LayoutTemplate>
                        <table id="Table2" runat="server" border="0" >
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="1" style="">
                                        <tr id="Tr2" runat="server" style="border:double">
                                            <th id="Th1" runat="server"  visible="false" ></th>
                                            <th id="Th3" runat="server" style="width:60px;">Currency</th>
                                            <th id="Th4" runat="server" style="width:100px;" >Rate (VND)</th>
                                            <th id="Th6" runat="server" ></th>
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
                            <td style="display: none">
                                <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' />
                            </td>                            
                            <td style="width:50px;">
                                <asp:Label ID="lbCurrency" runat="server"  Width="50"  Text='<%# Eval("Currency") %>' />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="rnbVNDRate" runat="server" Width="80px" readonly="true" borderwidth="0"  DbValue='<%# Bind("Rate") %>'>
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
                            <td style="display: none">
                                <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                            
                            <td style="width:50px;">
                                <asp:Label ID="lbCurrency" runat="server"  Width="50"   Text='<%# Eval("Currency") %>' />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="rnbVNDRate" runat="server" Width="80px" readonly="true" borderwidth="0" DbValue='<%# Eval("Rate") %>'>
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
                            <td style="display: none">
                                <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' />
                            </td>

                           
                            <td style="width:50px;">
                                <asp:TextBox Width="50" ID="tbCurrency"   runat="server" Text='<%# Bind("Currency") %>' />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="rnbVNDRate" runat="server" width="80px" DbValue='<%# Bind("Rate") %>'>
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
                            <td style="display: none">
                                <asp:Label ID="lbID" runat="server" width="0px" Text='<%# Eval("ID") %>' />
                            </td>

                           
                            <td style="width:50px;">
                                <span class="riSingle RadInput RadInput_Default" style="width:50px">
                                <asp:TextBox Width="50" ID="tbCurrency" runat="server" CssClass="riTextBox riEnabled"  Text='<%# Bind("Currency") %>' />
                                    </span>
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="rnbVNDRate" runat="server" width="80px"   DbValue='<%# Bind("Rate") %>'>
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
                            <td style="display: none">
                                <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                            
                            <td style="width:50px;">
                                <asp:Label ID="lbCurrency" runat="server" Text='<%# Eval("Currency") %>' />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="rnbVNDRate" runat="server" Width="50px" readonly="true" borderwidth="0" DbValue='<%# Bind("Rate") %>'>
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
