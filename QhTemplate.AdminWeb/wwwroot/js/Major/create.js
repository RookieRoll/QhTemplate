function save() {
    if ($("#name").val() === '' || $("#code").val() === '') {
        alert("不能为空");
        return;
    }

    $.ajax({
        url: "/major/Create",
        type: "post",
        data: {
            name: $("#name").val(),
            code: $("#code").val()
        },
        success: function (result) {
            alert(result);
            $('#myCreateModal').modal('hide');
            $("#name").val("");
            $("#code").val("") ;
            $('#dataTable').dataTable().fnReloadAjax("/major/GetData");

        }
    })
}