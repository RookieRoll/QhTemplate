$(function () {
    $("#myUPdateModal").modal("show");
})

function update() {
    if ($("#names").val() === '' || $("#codes").val() === '') {
        alert("不能为空");
        return;
    }

    $.ajax({
        url: "/major/Update",
        type: "post",
        data: {
            id: $("#id").val(),
            name: $("#names").val(),
            code: $("#codes").val()
        },
        success: function (result) {
            alert(result);
            $('#myUPdateModal').modal('hide');
            $('#dataTable').dataTable().fnReloadAjax("/major/GetData");

        }
    })
}