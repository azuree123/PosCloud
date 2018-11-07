function MultiSelect($el, $btn) {
    if (multiselect_selected($el)) {
        multiselect_deselectAll($el);
        $btn.text("Select All");
    }
    else {
        multiselect_selectAll($el);
        $btn.text("Deselect All");
    }
}
function multiselect_selected($el) {
    var ret = true;
    $('option', $el).each(function (element) {
        if (!!!$(this).prop('selected')) {
            ret = false;
        }
    });
    return ret;
}

function multiselect_selectAll($el) {
    $('option', $el).each(function (element) {
        $el.multiselect('select', $(this).val());
    });
}

function multiselect_deselectAll($el) {
    $('option', $el).each(function (element) {
        $el.multiselect('deselect', $(this).val());
    });
}