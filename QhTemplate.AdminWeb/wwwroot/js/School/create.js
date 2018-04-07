$(function () {
    $("#myCreateModal").modal("show");
})

function save() {
    var areaId = $("#areaId").val();
    var name = $("#name").val();
    var code = $("#code").val();
    var address = $("#address").val();
    if (!name && name === "") {
        alert("学校名称不能为空");
        return;
    }
    if (!code && code === "") {
        alert("学校编码不能为空");
        return;
    }
    if (!address && address === "") {
        alert("学校地址不能为空");
        return;
    }
    $.ajax({
        url: "/school/Create",
        type: "post",
        data: {
            "areaid": areaId,
            "name": name,
            "code": code,
            "address": address
        },
        success: function (data) {
            alert(data);
            location.reload();
        }
    })

}