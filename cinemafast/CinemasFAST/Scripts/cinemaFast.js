$(function () {
    var mes = "@TempData["WarningMessage"]";
    if (mes != "") {
        $('#myModal').modal('show');
    }
})