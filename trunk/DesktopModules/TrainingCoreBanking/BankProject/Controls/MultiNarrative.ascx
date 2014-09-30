<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiNarrative.ascx.cs" Inherits="BankProject.Controls.MultiNarrative" %>
<style>
    .addNarrativeRow, .removeNarrativeRow {
        cursor:hand; cursor:pointer;
    }
</style>
<asp:HiddenField ID="hiddenNarrative" runat="server" />
<table cellpadding="0" cellspacing="0">
    <asp:Literal ID="litNarrative" runat="server"><tr>
        <td class="MyLable">Narrative</td>
        <td class="MyContent" style="width:350px; "><input id="txtNarrative" name="txtNarrative" style="width:350px;" type="text" /></td>
        <td><a class="addNarrativeRow"><img src="Icons/Sigma/Add_16X16_Standard.png"></a></td>
    </tr></asp:Literal>
</table>
<script type="text/javascript">
    $(document).on("click", "a.addNarrativeRow", function () {
        $(this)
            .html('<img src="Icons/Sigma/Delete_16X16_Standard.png" />')
            .removeClass('addNarrativeRow')
            .addClass('removeNarrativeRow')
        ;
        var objTr = $(this).closest('tr').clone();
        objTr.find('input[type="text"]').each(
                function () {
                    this.value = '';
                });
        objTr.appendTo($(this).closest('table'));
        $(this)
                .html('<img src="Icons/Sigma/Add_16X16_Standard.png" />')
                .removeClass('removeNarrativeRow')
                .addClass('addNarrativeRow');
    });
    $(document).on("click", "a.removeNarrativeRow", function () {
        $(this)
            .closest('tr')
            .remove();
    });
    function NarrativeJoin() {
        var objNar = $('#<%=hiddenNarrative.ClientID%>');
        objNar.val('');
        objNar.parent().find('input[id="txtNarrative"]').each(
                        function () {
                            if ($(this).val() != '') {
                                if (objNar.val() == '')
                                    objNar.val($(this).val());
                                else
                                    objNar.val(objNar.val() + '\n' + $(this).val());
                            }
                        });
        alert(objNar.val());
    };
</script>