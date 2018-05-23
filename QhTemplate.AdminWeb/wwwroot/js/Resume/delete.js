$(function () {
    $("#deleteModal").modal();
})
//删除文章
function deleteResume(id) {
    $.ajax({
        type: 'POST',
        url: "/Resume/DeleteComfirm",
        data: { "id": id },
        success: function (data) {
            $('#deleteModal').modal('hide');
            //$('#dataTable').dataTable().fnReloadAjax("/Role/GetData");
            location.reload();
        }
    });
}