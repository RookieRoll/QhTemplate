$(function () {
    $("#deleteModal").modal();
})
//删除角色
function deleteRole(id) {
    $.ajax({
        type: 'POST',
        url: "/Role/ConfiredRemove",
        data: { "id": id },
        success: function (data) {
            $('#deleteModal').modal('hide')
            $('#dataTable').dataTable().fnReloadAjax("/Role/GetData");
        }
    });
}