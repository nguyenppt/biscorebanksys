//Special character input//
function chkSpecialCharacter(val) {
    var pattern = /[!@#$%\^&*]/;
    if (pattern.test(val) == true) {
        return "User can’t input special characters (!@#$%^&*) in the Tab MTxxx. Please check and remove";
    }
    return "";
}
function MTIsValidInput(MTContainerId, ignoreIds) {
    var pattern = /[!@#$%\^&*]/;
    var isValid = true;
    $('#' + MTContainerId).children().find('input[type=text]').each(function () {
        if (!isValid) return;
        var itemId = $(this).attr('id');
        if (ignoreIds != null) {
            for (i = 0; i < ignoreIds.length; i++) {
                if (ignoreIds[i] == itemId) {
                    return;
                }
            }
        }
        if (pattern.test($(this).val())) {
            alert("User can’t input special characters (!@#$%^&*) in the Tab '" + $('#' + MTContainerId).attr('id') + "'. Please check and remove");
            $(this).focus();
            isValid = false;
        }
    });
    return isValid;
}