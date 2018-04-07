$(function () {
    $("#deleteModal").modal();
})
//删除专业
function deleteSchool(id) {
    $.ajax({
        type: 'POST',
        url: "/school/DeleteComfirm",
        data: { "id": id },
        success: function (data) {
            $('#deleteModal').modal('hide')
            //$('#dataTable').dataTable().fnReloadAjax("/Role/GetData");
            location.reload();
        }
    });
}