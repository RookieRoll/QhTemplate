function save() {
    if ($("#name").val() === '' || $("#code").val() === '') {
        alert("不能为空");
        return;
    }
     
    $.ajax({
        url: "/major/Update",
        type: "post",
        data: {
            id: $("#id").val(),
            name: $("#name").val(),
            code:$("#code").val()
        },
        success: function (result) {
            alert(result);
            $('#myCreateModal').modal('hide');
            $('#dataTable').dataTable().fnReloadAjax("/major/GetData");

        }
    })
}