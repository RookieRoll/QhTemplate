$('#dataTable').dataTable({
    lengthChange: true,
    serverSide: true,
    ajax: '/User/GetData',
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
                html += "<li><a href=\"javaScript:void(0)\" onclick=\"openEditModel(" + id + ")\">编辑</a></li>";
                html += "<li><a href=\"javaScript:void(0)\" onclick=\"openAuthorizeModel(" + id + ")\">权限</a></li>";
                html += "<li><a href=\"javaScript:void(0)\" onclick=\"openRolesModel(" + id + ")\">角色</a></li>";
                html += "<li><a href=\"javaScript:void(0)\" onclick=\"openDeleteModel(" + id + ")\">删除</a></li>";
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
            title: '姓名',
        },
        {
            name: 'username',
            data: 'username',
            title: '用户名',
        },
        {
            name: 'emailaddress',
            data: 'emailAddress',
            title: '邮箱'
        },
        {
            name: 'creationtime',
            data: 'creationTime',
            title: '创建时间'
        }
    ],
    language: {
        "url": "/lib/datatables/styles/i18n/zh-CN.json"
    }
})


function openCreateModel() {
    $('#myCreateModal').modal('show');
}

function openEditModel(id) {
    $.ajax({
        type: 'Get',
        url: "/User/Update",
        data: { "id": id },
        success: function (data) {
            $("#model").html(data)
        }
    });
}

function openDeleteModel(id) {
    $.ajax({
        type: 'Get',
        url: "/User/Delete",
        data: { "id": id },
        success: function (data) {
            $("#model").html(data)
        }
    });
}

function openAuthorizeModel(id) {
    $.ajax({
        type: 'Get',
        url: "/User/Authorize",
        data: { "id": id },
        success: function (data) {
            $("#model").html(data)
        }
    });
}
function openRolesModel(id) {
    $.ajax({
        type: 'Get',
        url: "/User/Role",
        data: { "id": id },
        success: function (data) {
            $("#model").html(data)
        }
    });
}