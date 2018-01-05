$(document).ready(function () {
    $('#dataTable').dataTable({
        lengthChange: true,
        serverSide: true,
        ajax: '/Role/GetData',
        columns: [
            {
                name: 'operate',
                title: '操作',
                sortable: false,
                searchable: false,
                render: function (data, type, row) {
                    var id = "'" + row.id + "'";
                    var html = "<div class=\"dropdown operator\"><form>" +
                        "<button class=\"btn btn-default\" type=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">" +
                        "<span class=\"glyphicon glyphicon-th-list\"></span> </button>" +
                        "<ul class=\"dropdown-menu\" role=\"menu\" aria-labelledby=\"dLabel\">";
                    html += "<li><a href=\"javaScript:void(0)\" onclick=\"openEdit(" + id + ")\">编辑</a></li>";
                    if (!row.isStatic) {
                        html += "<li><a href=\"javaScript:void(0)\" onclick=\"openDelete(" + id + ")\">删除</a></li>";
                    }
                    html += "</ul></form> </div>";
                    return html;
                }
            },
            {
                name: 'id',
                data: 'id',
                title: 'Id',
                visible: false,
            },
            {
                name: 'name',
                data: 'name',
                title: '姓名'
            },
            {
                name: 'creationTime',
                data: 'creationTime',
                title: '创建时间',
            },
            {
                name: 'isDefaule',
                title: '是否为默认',
                sortable: false,
                searchable: false,
                render: function (data, type, row) {
                    var html = "";
                    if (row.isDefault)
                        html += "<span class='btn btn-default disabled'>默认角色</span>";
                    return html;
                }
            },
        ],
        language: {
            "url": "/lib/datatables/styles/i18n/zh-CN.json"
        }
    });
});

function openEdit(id) {
    $.ajax({
        type: 'Get',
        url: "/Role/UpdateRole",
        data: { "roleId": id },
        success: function (data) {
            $("#model").html(data)
        }
    });
}
function openDelete(id) {
    $.ajax({
        type: 'POST',
        url: "/Role/RemoveRole",
        data: { "id": id },
        success: function (data) {
            $("#model").html(data);
        }
    });
}

$("#openCreateModal").click(function () {
    $("#createrolemodel").modal("show");
    $(".set-role-attribute").click();
});