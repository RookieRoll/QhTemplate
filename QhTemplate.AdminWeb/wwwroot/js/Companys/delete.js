$(function () {
    $("#deleteModal").modal();
})
//删除专业
function deleteMajor(id) {
    $.ajax({
        type: 'POST',
        url: "/Company/DeleteComfirm",
        data: { "id": id },
        success: function (data) {
            $('#deleteModal').modal('hide')
            //$('#dataTable').dataTable().fnReloadAjax("/Role/GetData");
            location.reload();
        }
    });
}