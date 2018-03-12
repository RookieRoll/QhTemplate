$(function () {
    $("#deleteModal").modal();
})
//É¾³ý½ÇÉ«
function deleteMajor(id) {
    $.ajax({
        type: 'POST',
        url: "/major/DeleteComfirm",
        data: { "id": id },
        success: function (data) {
            $('#deleteModal').modal('hide')
            //$('#dataTable').dataTable().fnReloadAjax("/Role/GetData");
            location.reload();
        }
    });
}