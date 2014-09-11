<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentFrequency.ascx.cs" Inherits="BankProject.TellerApplication.AccountManagement.CurrentNonTermSavingAC.SalaryPayment.PaymentFrequency" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../../../Controls/VVTextBox.ascx" TagPrefix="uc1" TagName="VVTextBox" %>
<%@ Register src="../../../../Controls/VVComboBox.ascx"  TagPrefix="uc2" TagName="VVComboBox" %>
<%@ register Assembly="DotNetNuke.web" Namespace="DotNetNuke.Web.UI.WebControls" TagPrefix="dnn"%>
<%@ Register src="~/controls/LabelControl.ascx" TagPrefix="dnn" TagName="Label"%>

<script type="text/javascript">
    jQuery(function ($) {
        $('#tabs-demo').dnnTabs({ selected: 0, cookieMilleseconds: 0 });
    })
  
</script>
<style type="text/css">
div.Upload .ruFakeInput
    {
        visibility: hidden;
        width:0px;
        padding:0px;
        
    }
  
 </style>
<telerik:RadWindowManager runat="server" id="RadWindowManager1"></telerik:RadWindowManager>
<telerik:RadToolBar runat="server" ID="RadToolBar1"  EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit" 
            ToolTip="Commit Data" Value="btnCommit" CommandName="commit" Enabled ="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btnPreview" CommandName="Preview" Enabled ="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btnAuthorize" CommandName="authorize" Enabled ="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btnReverse" CommandName="reverse" Enabled ="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btnSearch" CommandName="search" Enabled ="false">
        </telerik:RadToolBarButton>
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btnPrint" CommandName="print" Enabled ="false">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>

<div>
    <table width="100%" cellpadding="0" cellspacing="0" style="padding-left:10px;padding-bottom:20px">
        <tr>
            <td style="padding-top:10px;padding-left:10px" >  <asp:RequiredFieldValidator
                    runat="server" Display="None"
                    ID="RequiredFieldValidator1"
                    ControlToValidate="rcbAccountPayment"
                    ValidationGroup="Commit"
                    InitialValue=""
                    ErrorMessage="Account Payment is Required" ForeColor="Red">
                </asp:RequiredFieldValidator>
                <telerik:RadComboBox ID="rcbAccountPayment"
                        MarkFirstMatch="True"
                        AllowCustomText="false"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="rcbAccountPayment_SelectedIndexChanged"                        
                        Width="310" runat="server" >
                    </telerik:RadComboBox></td>
            
        </tr>
    </table>
</div>

<div  class="dnnForm" id="tabs-demo">
    <ul class="dnnAdminTabNav">
        <li><a href="#Main">Salary Frequency</a></li>       
    </ul>
    
    <div id="Main" class="dnnClear">
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Commit" />
        <table style="width: 100%; padding: 0px" id="textfile">
            
            <tr>
                <td class="MyLable">Currency</td>
                <td class="MyContent">
                    <asp:Label ID="lblCurrency" runat="server" Enabled="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width: 100%; padding: 0px;margin-left:0px;border-collapse: collapse;"   cellspacing="0"  cellpadding="0" >
                        <tr>
                            <td class="MyLable" style="width:150px">GB IMPORT FILE</td>
                            <td class="MyContent" style="width:200px">
                                <telerik:RadTextBox ID="tbImportFile" Width="316" runat="server" ReadOnly="true"></telerik:RadTextBox>
                            <td>
                            <td style="width:80px;">
                                <telerik:RadUpload OnClientFileSelected="GetPath" style="clear:both;margin-top:3px" ID="ImportFileUpload"  Width="50" CssClass="Upload"  ControlObjectsVisibility="None" runat="server"></telerik:RadUpload>
                            </td>
                            <td>
                                <asp:Button runat="server" ID="SubmitButton" Text="Upload file" OnClick="SubmitButton_Click" />
                            </td>                           
                            <td>
                                <asp:Label runat="server" ForeColor="Red" ID="labelNoResults" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>

            </tr>
            <tr>
                <td class="MyLable">Total Debit Amt</td>
                <td class="MyContent">
                    <telerik:RadNumericTextBox width="315" ID="tbTotalDebitAmtShow" runat="server" ValidationGroup="Group1">
                    </telerik:RadNumericTextBox>
                </td>             
            </tr>
         
            <tr runat="server" id="NonFequencyCheck">
                <td class="MyLable">Fequency</td>
                <td class="MyContent">
                     <telerik:RadDatePicker width="341" ID="rdpFrequency" runat="server"/>
                </td>
            </tr>

            <tr runat="server" id="TermCheck">
                <td class="MyLable">Term</td>
                <td class="MyContent">
                    <telerik:RadComboBox ID="rcbTerm"
                        MarkFirstMatch="True"                        
                        AllowCustomText="false"
                        Width="315" runat="server">
                    </telerik:RadComboBox>
                </td>
            </tr>
         
            <tr>
                <td class="MyLable">End Date</td>
                <td class="MyContent">
                    <telerik:RadDatePicker width="341" ID="rdpEndDate" runat="server"  />
                </td>

            </tr>

            <tr>
                <td class="MyLable">Ordering Cust</td>
                <td class="MyContent">
                    <telerik:RadTextBox ID="tbOrderingCust" runat="server" Width="315"></telerik:RadTextBox>
                </td>

            </tr>

        </table>

        <fieldset>
            <legend style="text-transform: uppercase; font-weight: bold;">Credit Information</legend>

            <table id="CreditInformation" style="width:100%">
             <%--   <thead>
                    <tr class="MyLable">
                        <td style="width:200px" >Payment Method</td>
                        <td style="width:200px">Credit Amount</td>
                        <td style="width:auto">Credit Account</td>
                        <td style="width:100px"></td>
                    </tr>                   
                </thead>--%>
                    <tr>
                        <td style="width:220px" >
                            <telerik:radcombobox appenddatabounditems="True"
                                    id="rcbPaymentMethod" runat="server"
                                    markfirstmatch="True" height="150px"
                                    width="180"                                                                      
                                    allowcustomtext="false">
                                <ExpandAnimation Type="None" />
                                <CollapseAnimation Type="None" />
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                </Items>
                            </telerik:radcombobox>
                        </td>
                        <td style="width:220px" >
                             <telerik:radnumerictextbox runat="server" Width="200"  id="tbCreditAmount" minvalue="0" maxvalue="999999999999">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" AllowRounding="true"   KeepNotRoundedValue="false"  />
                            </telerik:radnumerictextbox>
                        </td>
                       
                        <td style="width:auto" >
                             <telerik:radcombobox appenddatabounditems="True"
                                    id="rcbCreditAccount" runat="server"
                                    markfirstmatch="True" height="150px"
                                    width="90%"                                                                      
                                    allowcustomtext="false">
                                <ExpandAnimation Type="None" />
                                <CollapseAnimation Type="None" />
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                </Items>
                            </telerik:radcombobox>
                        </td>
                        <td style="width:90px" >
                            <asp:ImageButton  ImageUrl="~/Icons/Sigma/Save_16X16_Standard.png"  OnClick="UpdateRow_Click" ID="UpdateRowData" runat="server" Text="Insert" />&nbsp;&nbsp;&nbsp
                            <asp:ImageButton ImageUrl="~/Icons/Sigma/Refresh_16x16_Standard.png" ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                        </td>
                    </tr>
                <tr>

                    <td colspan="4">
                         <telerik:radgrid runat="server" autogeneratecolumns="False" OnItemCommand="grdReviewList_ItemCommand"
                             id="grdReviewList" allowpaging="false" onneeddatasource="grdReviewList_OnNeedDataSource">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Payment Method"  DataField="PaymentMethod" UniqueName="PaymentMethod">
                                         <ItemStyle width="200" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Credit Amount " DataField="CreditAmount"  UniqueName="CreditAmount" 
                                         DataType="System.Decimal" 
                                        DataFormatString="{0:N}">
                                          <ItemStyle Width="220" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Credit Account" DataField="CreditAccount" UniqueName="CreditAccount" />                                   
                                    <telerik:GridBoundColumn HeaderText="   " DataField="Exist" UniqueName="Exist" >   
                                          <ItemStyle Width="75" ForeColor="Red"  />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn>
                                        <ItemStyle Width="100" />
                                        <ItemTemplate>
                                              <asp:ImageButton ID="ImageButton1" ImageUrl="~/Icons/Sigma/Delete_16X16_Standard.png" runat="server" CommandName="DeleteRow" CommandArgument='<%# Eval("CreditAccount") %>' Text="Delete" />&nbsp;&nbsp;&nbsp
                                              <asp:ImageButton ImageUrl="~/Icons/Sigma/Edit_16X16_Standard.png" ID="EditButton" runat="server" CommandName="EditRow" CommandArgument='<%# Eval("CreditAccount") %>'  Text="Edit" />
                                        </itemtemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:radgrid>
                    </td>
                </tr>
          
            </table>
            
          
        </fieldset>

    </div>

</div>
  <div style="display:none">
    <asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" />
    <asp:TextBox ID="CreditAccountForUpdate" runat="server" />
     <asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator2"
            ControlToValidate="CheckExist"
            ValidationGroup="Commit"
            InitialValue=""
            ErrorMessage="have account not exist" ForeColor="Red">
        </asp:RequiredFieldValidator>
     <telerik:radtextbox ID="CheckExist" runat="server"  ></telerik:radtextbox>
   
     <asp:RequiredFieldValidator
            runat="server" Display="None"
            ID="RequiredFieldValidator3"
            ControlToValidate="NotTotalDebitAmt"
            ValidationGroup="Commit"
            InitialValue=""
            ErrorMessage="Total debit amt invalid value" ForeColor="Red">
        </asp:RequiredFieldValidator>
     <telerik:radtextbox ID="NotTotalDebitAmt" runat="server" Text="ok"  ></telerik:radtextbox>
      <asp:TextBox ID="tbDepositCode" runat="server" Width="200px" />
        <telerik:RadNumericTextBox width="315" ID="tbTotalDebitAmt" runat="server" ValidationGroup="Group1">
        </telerik:RadNumericTextBox>
</div>
<telerik:radcodeblock id="RadCodeBlock1" runat="server">
  
    <telerik:radajaxmanager id="RadAjaxManager1" runat="server" defaultloadingpanelid="AjaxLoadingPanel1">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rcbAccountPayment">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblCurrency" />
                <telerik:AjaxUpdatedControl ControlID="tbTotalDebitAmt" />            
                <telerik:AjaxUpdatedControl ControlID="rcbCreditAccount" />  
                <telerik:AjaxUpdatedControl ControlID="CheckExist" /> 
               <telerik:AjaxUpdatedControl ControlID="NotTotalDebitAmt" /> 
            </UpdatedControls>
        </telerik:AjaxSetting>   

      
    
    </AjaxSettings>
</telerik:radajaxmanager>
<script type="text/javascript">

    function GetPath() {
        var upload = $find("<%= ImportFileUpload.ClientID %>");
        var filePath = upload.getFileInputs()[0].value;       
        $("#<%= tbImportFile.ClientID %>").val(filePath);
    }

    $("#<%=tbDepositCode.ClientID %>").keyup(function (event) {
        if (event.keyCode == 13) {
            $("#<%=btSearch.ClientID %>").click();
            }

    });
    function alert(m) {
        m = m.replace(/-/g, '<br>-') + "<br> &nbsp;<br> &nbsp;";
        radalert(m, "Alert");
    }
</script>
</telerik:radcodeblock>


