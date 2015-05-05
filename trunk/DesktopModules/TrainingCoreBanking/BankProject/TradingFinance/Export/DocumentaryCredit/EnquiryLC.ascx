﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnquiryLC.ascx.cs" Inherits="BankProject.TradingFinance.Export.DocumentaryCredit.EnquiryLC" %>
<telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png" ValidationGroup="Commit"
                ToolTip="Commit Data" Value="btCommitData" CommandName="commit" Enabled="false">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
                ToolTip="Preview" Value="btPreview" CommandName="preview" Enabled="false">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
                ToolTip="Authorize" Value="btAuthorize" CommandName="authorize" Enabled="false">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
                ToolTip="Reverse" Value="btReverse" CommandName="reverse" Enabled="false">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
                ToolTip="Search" Value="btSearch" CommandName="search">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
                ToolTip="Print Deal Slip" Value="btPrint" CommandName="print" Enabled="false">
            </telerik:RadToolBarButton>
        </Items>
</telerik:RadToolBar>
<%var display = "display:none;";
  if (string.IsNullOrEmpty(lstType) || !lstType.ToLower().Equals("4appr"))
  {display = "";}%>
<div style="padding:10px;<%=display%>">
    <table cellpadding="0" cellspacing="0" style="margin-bottom:10px">
        <tr>
            <td style="width: 170px">REF No.</td>
            <td><telerik:RadTextBox ID="txtRefNo" runat="server" Width="200" /></td>
            <td style="width: 120px; padding-left:5px;">Applicant Name</td>
            <td><telerik:RadTextBox ID="txtApplicantName" runat="server" Width="200" /></td>
        </tr>
        <tr>
            <td>Beneficiary ID</td>
            <td style="padding-top:5px;" colspan="3"><telerik:RadComboBox
                                AppendDataBoundItems="True"
                                ID="rcbBeneficiaryNumber" runat="server"
                                MarkFirstMatch="True"
                                Width="525"
                                Height="150"
                                AllowCustomText="false">
                                <ExpandAnimation Type="None" />
                                <CollapseAnimation Type="None" />
                            </telerik:RadComboBox></td>
        </tr>
        <tr>
            <td>Issue date</td>
            <td style="padding-top:5px;"><telerik:RadDatePicker ID="txtIssueDate" runat="server" Width="200" /></td>
            <td style="padding-left:5px;">Issuing Bank</td>
            <td style="padding-top:5px;"><telerik:RadTextBox ID="txtIssuingBank" runat="server" Width="200" /></td>
        </tr>
        <tr>
            <td>Documentary Credit Number</td>
            <td style="padding-top:5px;"><telerik:RadTextBox ID="txtDocumentaryCreditNumber" runat="server" Width="200" /></td>
            <td style="width: 120px; padding-left:5px;"></td>
            <td></td>
        </tr>
    </table>
    <telerik:RadGrid runat="server" AutoGenerateColumns="False" ID="radGridReview" AllowPaging="True" OnNeedDataSource="radGridReview_OnNeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="Ref No" DataField="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="LC No" DataField="ImportLCCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="Applicant Name" DataField="ApplicantName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="Amount" DataField="Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderText="Currency" DataField="Currency" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="Beneficiary ID" DataField="BeneficiaryNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="Beneficiary Name" DataField="BeneficiaryName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="Issue date" DataFormatString="{0:dd-MM-yyyy}" DataField="DateOfIssue" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderText="Issuing Bank" DataField="IssuingBankNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderText="Status" DataField="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn>
                    <ItemStyle Width="50" HorizontalAlign="Right" />
                    <ItemTemplate>
                        <a href='Default.aspx?tabid=<%=refId %>&code=<%# Eval("Code").ToString() %>&lst=<%=lstType %>'><img src="Icons/bank/text_preview.png" alt="" title="" style="" width="20" /> </a>
                    </itemtemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>