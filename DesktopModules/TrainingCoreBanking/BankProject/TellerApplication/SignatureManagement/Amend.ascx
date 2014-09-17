<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Amend.ascx.cs" Inherits="BankProject.TellerApplication.SignatureManagement.Amend" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"/>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Commit"  />
<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
<style>
    #<%=cmdSelectSignatureImage.ClientID%> {
        height: 25px;
        margin-right:10px;
    }
    #<%=imgSignaturePreview.ClientID%> {
        margin-top:5px;
    }
</style>
</telerik:RadCodeBlock>
<div>
    <telerik:RadToolBar runat="server" ID="RadToolBar1" EnableRoundedCorners="true" EnableShadows="true" Width="100%" 
         OnClientButtonClicking="OnClientButtonClicking" OnButtonClick="RadToolBar1_ButtonClick">
    <Items>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/commit.png"
            ToolTip="Commit Data" Value="btCommitData" CommandName="commit" ValidationGroup="Commit" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/preview.png"
            ToolTip="Preview" Value="btPreview" CommandName="preview" PostBack="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/authorize.png"
            ToolTip="Authorize" Value="btAuthorize" CommandName="authorize" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/reverse.png"
            ToolTip="Reverse" Value="btReverse" CommandName="reverse" Enabled="false">
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ImageUrl="~/Icons/bank/search.png"
            ToolTip="Search" Value="btSearch" CommandName="search" PostBack="false">
        </telerik:RadToolBarButton>
         <telerik:RadToolBarButton ImageUrl="~/Icons/bank/print.png"
            ToolTip="Print Deal Slip" Value="btPrint" CommandName="print" Enabled="false">
        </telerik:RadToolBarButton>
    </Items>
</telerik:RadToolBar>
</div>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td class="MyLable">Customer Id <span class="Required">(*)</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Customer Id require !" Display="None" ControlToValidate="txtCustomerId" ValidationGroup="Commit"></asp:RequiredFieldValidator></td>
        <td class="MyContent"><asp:HiddenField ID="txtCustomerIdOld" runat="server" /><asp:TextBox ID="txtCustomerId" runat="server" Width="200" />
        <span style="color: darkgrey;"><asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label></span></td>
    </tr>
    <tr>
        <td class="MyLable" style="vertical-align:top;">New signature <span class="Required">(*)</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Signature require !" Display="None" ControlToValidate="txtSignature" ValidationGroup="Commit"></asp:RequiredFieldValidator></td>
        <td class="MyContent"><asp:FileUpload ID="txtSignature" runat="server" CssClass="NoDisplay" /><asp:Button ID="cmdSelectSignatureImage" runat="server" Text="Select signature image" OnClientClick="return false;" /><asp:Label ID="lblSignatureImage" runat="server" Text=""></asp:Label>
            <br /><asp:Image ID="imgSignaturePreview" runat="server" CssClass="imgSignaturePreview NoDisplay" /></td>
    </tr>
    <tr>
        <td class="MyLable" style="vertical-align:top;">Old signature</td>
        <td class="MyContent"><asp:HyperLink ID="lnkSignatureOld" runat="server" Target="_blank"><asp:Image ID="imgSignatureOld" runat="server" CssClass="imgSignaturePreview" /></asp:HyperLink></td>
    </tr>
</table>
<script type="text/javascript">
    function loadInfo() {
        var objCust = $("#<%=txtCustomerId.ClientID%>");
        var objCustOld = $("#<%=txtCustomerIdOld.ClientID%>");
        if (objCust.val() == null || objCust.val() == '') {
            alert('CustomerId require !');
            return;
        }
        if (objCust.val() != objCustOld.val()) {
            objCustOld.val(objCust.val());
            $("#<%=btSearch.ClientID%>").click();
        }
    }
    function OnClientButtonClicking(sender, args) {
        var button = args.get_item();
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Commit%>") {
            //do nothing
        }
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Preview%>") {
            window.location = "Default.aspx?tabid=286&lst=4appr";
        }
        if (button.get_commandName() == "<%=BankProject.Controls.Commands.Search%>") {
            window.location = "Default.aspx?tabid=286";
        }
    }
    $("#<%=txtCustomerId.ClientID%>")
        .keypress(function (event) {
            if (event.which == 13) {
                loadInfo();
            }
        });
    $('#<%=cmdSelectSignatureImage.ClientID%>')
            .click(function () {
                $("#<%=txtSignature.ClientID%>").trigger("click");
            });
    $(function () {
        $("#<%=txtSignature.ClientID%>").on("change", function () {
            var lblSignatureImage = $('#<%=lblSignatureImage.ClientID%>');
                var imgSignaturePreview = $("#<%=imgSignaturePreview.ClientID%>");
                var fileTypes = '.jpg|.png';
                var files = !!this.files ? this.files : [];
                //
                lblSignatureImage.text('');
                imgSignaturePreview.removeClass('NoDisplay');
                imgSignaturePreview.addClass('NoDisplay');
                //alert(files.length);
                if (!files.length) {
                    //user not choose file
                    return;
                }
                if (!window.FileReader) {
                    // no file selected, or no FileReader support
                    alert('Browser not support preview image !');
                    return;
                }
                if (/^image/.test(files[0].type)) { // only image file
                    //
                    var fileName = files[0].name;
                    if (!isValidFileExt(fileName, fileTypes, '|')) {
                        this.val('');
                        alert('File image extension require : ' + fileTypes);
                        return;
                    }
                    var reader = new FileReader(); // instance of the FileReader
                    reader.readAsDataURL(files[0]); // read the local file
                    reader.onloadend = function () { // set image data as background of div
                        imgSignaturePreview.removeClass('NoDisplay');
                        imgSignaturePreview.attr("src", this.result);
                        lblSignatureImage.text(fileName);
                    }
                }
                else {
                    this.val('');
                    alert('File image extension require : ' + fileTypes);
                }
            });
        });
        function isValidFileExt(fileName, fileTypes, fileTypesSplitChar) {
            fileName = fileName.trim().toLowerCase();
            if (fileName == '') return false;
            fileTypes = fileTypes.trim().replace(' ', '');
            if (fileTypes == '') return false;
            i = fileName.lastIndexOf('.');
            if (i <= 0) return false;
            fileExt = fileTypesSplitChar + fileName.substring(i, fileName.length) + fileTypesSplitChar;
            fileTypes = fileTypesSplitChar + fileTypes + fileTypesSplitChar;
            return (fileTypes.indexOf(fileExt) >= 0);
        }
</script>
<div style="visibility: hidden;"><asp:Button ID="btSearch" runat="server" OnClick="btSearch_Click" Text="Search" /></div>