function renumerateTableRows() {
    $("tbody tr").each(function (i, v) {
        $(v).find(".tableIndex").text(i+1 + ".");
    });
}
