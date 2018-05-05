$(function () {
    $("#deleteModal").modal();
})
//删除文章
function deleteArticle(id) {
    $.ajax({
        type: 'POST',
        url: "/CareerTalk/DeleteComfirm",
        data: { "id": id },
        success: function (data) {
            $('#deleteModal').modal('hide')
            //$('#dataTable').dataTable().fnReloadAjax("/Role/GetData");
            location.reload();
        }
    });
}