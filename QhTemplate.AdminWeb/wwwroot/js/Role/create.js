$(function () {
    $(".set-role-attribute").addClass("roleactive");

})

//样式转换
$(".set-role-attribute").click(function () {
    $(".set-permission-tree").hide();
    $(".set-role-attribute-value").show();
    $(".set-role-attribute").addClass("roleactive");
    $(".set-permission").removeClass("roleactive");
})
$(".set-permission").click(function () {
    $(".set-permission-tree").show();
    $(".set-role-attribute-value").hide();
    $(".set-role-attribute").removeClass("roleactive");
    $(".set-permission").addClass("roleactive");
    setPermissionsTree();
})

function setPermissionsTree() {
    $('#jstree').jstree({
        'core': {
            'data': {
                'url': '/Role/GetPermissions',
                'dataType': 'json'
            },
            "check_callback": true
        },
        //'checkbox': {
        //    // 禁用级联选中
        //    //'three_state': false,
        //    //'cascade': 'undetermined' //有三个选项，up, down, undetermined; 使用前需要先禁用three_state
        //},
        "plugins": ["themes", "json_data", "search", "checkbox"],

    });
}

function getUncertainNodeIds(id) {
    var parentids = new Array();
    $("#" + id).find(".jstree-undetermined ").each(function () {
        var id = $(this).parent().parent().attr("id");
        parentids.push(id);
    })
    return parentids;
}

$("#save-role-set").click(function () {
    var list = [];
    if ($('#jstree').children().length > 0) {
        var tree = $('#jstree').jstree();
        list = tree.get_checked().concat(getUncertainNodeIds("jstree"));
    }
    var isdefault = document.getElementById("isdefault").checked;
    $.ajax({
        type: 'POST',
        url: "/Role/CreateRole",
        data: {
            "roleName": $("#rolename").val(),
            "isDefault": isdefault,
            "permissionNames": list,
        },
        success: function (data) {
            alert(data);
            $('#createrolemodel').modal('hide')
           // location.href = "Role/index";.dataTable().fnReloadAjax
           
            $('#dataTable').dataTable().fnReloadAjax("/Role/GetData");
            $("#rolename").val(""),
                $("#isdefault").attr("checked", false),
                $('#jstree').data('jstree', false);
        },
    });
})


//角色名称不能为空
$("#rolename").keyup(function () {
    var text = $(this).val();
    if (text == "" || text == null) {
        $("#save-role-set").attr("disabled", true)
    } else {
        $("#save-role-set").attr("disabled", false)
    }
})