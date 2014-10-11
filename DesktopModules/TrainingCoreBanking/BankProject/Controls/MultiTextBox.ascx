<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiTextBox.ascx.cs" Inherits="BankProject.Controls.MultiTextBox" %>
<style>
    .MultiTextBoxAddRow, .MultiTextBoxRemoveRow {
        cursor:hand; cursor:pointer;
    }
</style>
<div id="divMultiTextBox" runat="server">
    <asp:HiddenField ID="txtMultiTextBoxString" runat="server" />
    <table cellpadding="0" cellspacing="0">
        <asp:Literal ID="litMultiTextBox" runat="server"></asp:Literal>
    </table>
</div>
<script type="text/javascript">
    var <%=divMultiTextBox.ClientID%>_MultiTextBoxRow = <%=MultiTextBoxRow%>;
    $(document).on("click", "#<%=divMultiTextBox.ClientID%> a.MultiTextBoxAddRow", function () {
        $(this)
            .html('<img src="Icons/Sigma/Delete_16X16_Standard.png" />')
            .removeClass('MultiTextBoxAddRow')
            .addClass('MultiTextBoxRemoveRow')
        ;
        //
        var objTr = $(this).closest('tr').clone();
        objTr.find('input[type="text"]').each(
                function () {
                    this.value = '';
                });
        //
        <%=divMultiTextBox.ClientID%>_MultiTextBoxRow += 1;
        var txt = objTr.find('.MyLable').text();
        var txtI = txt.lastIndexOf('.1');
        txt = txt.substring(0, txtI);
        objTr.find('.MyLable').text(txt + '.' + <%=divMultiTextBox.ClientID%>_MultiTextBoxRow);
        objTr.find('.MyLable').text('');
        //
        objTr.appendTo($(this).closest('table'));
        $(this)
                .html('<img src="Icons/Sigma/Add_16X16_Standard.png" />')
                .removeClass('MultiTextBoxRemoveRow')
                .addClass('MultiTextBoxAddRow');
    });
    $(document).on("click", "#<%=divMultiTextBox.ClientID%> a.MultiTextBoxRemoveRow", function () {
        $(this)
            .closest('tr')
            .remove();
    });
    function <%=getJSFunction()%>() {
        var objNar = $('#<%=txtMultiTextBoxString.ClientID%>');
        objNar.val('');
        $('#<%=divMultiTextBox.ClientID%> table').children().find("input:text").each(function () {
            //alert($(this).val() + '^' + $(this).text());
            var txt = $(this).val();
            if (txt != '') {
                if (objNar.val() == '')
                    objNar.val(txt);
                else
                    objNar.val(objNar.val() + '\n' + txt);
            }
        });
        //alert(objNar.val() + '^' + objNar.attr('id'));
    }
</script>