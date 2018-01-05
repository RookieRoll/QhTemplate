$(function () {
    $("#eidtrolemodel").modal();
    $(".show-set-role-attribute").addClass("roleactive");
})

//样式转换
$(".show-set-role-attribute").click(function () {
    $(".show-set-permission-tree-value").hide();
    $(".show-set-role-attribute-value").show();
    $(".show-set-role-attribute").addClass("roleactive");
    $(".show-set-permission-tree").removeClass("roleactive");
})
$(".show-set-permission-tree").click(function () {
    $(".show-set-permission-tree-value").show();
    $(".show-set-role-attribute-value").hide();
    $(".show-set-role-attribute").removeClass("roleactive");
    $(".show-set-permission-tree").addClass("roleactive");
})

function saveEditPermission(id) {
    var tree = $('#edit-permission-tree').jstree();
    var list = tree.get_checked().concat(getUncertainNodeIds("edit-permission-tree"));

    var isdefault = $("#editisdefault:checked").length > 0 ? true : false;
    $.ajax({
        type: 'POST',
        url: "/Role/UpdateRole",
        data: {
            "roleId": id,
            "roleName": $("#edit-role-name").val(),
            "isDefault": isdefault,
            "permissions": list,
        },
        success: function (data) {
            $('#eidtrolemodel').modal("hide");
            $('#dataTable').dataTable().fnReloadAjax("/Role/GetData");
        },
    });
}

//角色名称不能为空
$("#edit-role-name").blur(function () {
    var text = $("#edit-role-name").val();
    if (text === "") {
        $("#saveeditroleset").attr("disabled", true)
    } else {
        $("#saveeditroleset").attr("disabled", false)
    }
})
