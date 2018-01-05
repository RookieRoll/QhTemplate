$(document).ready(function () {
    var id = $('#jstree').attr('data-id');
   
})

function saveUserPermission(id) {
    $('#myModal').modal('hide')
    var tree = $('#jstree').jstree();
    var list = tree.get_checked().concat(getUncertainNodeIds("jstree"));
    $.ajax({
        type: 'POST',
        url: "/User/Permission",
        data: {
            "id": id, 
            "permissions": list,
        },
        success: function (data) {
            window.location.href="/User/Index"
        },
    });
}

function getUncertainNodeIds(id) {
    var parentids = new Array();
    $("#" + id).find(".jstree-undetermined ").each(function () {
        parentids.push($(this).parent().parent().attr("id"));
    })
    return parentids;
}