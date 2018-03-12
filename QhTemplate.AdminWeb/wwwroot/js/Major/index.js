$(document).ready(function () {
    $('#dataTable').dataTable({
        lengthChange: true,
        serverSide: true,
        ajax: '/Major/GetData',
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

                    html += "<li><a href=\"javaScript:void(0)\" onclick=\"openDelete(" + id + ")\">删除</a></li>";

                    html += "</ul></form> </div>";
                    return html;
                }
            },
            {
                name: 'id',
                data: 'id',
                title: 'Id',
                visible: false
            },
            {
                name: 'name',
                data: 'name',
                title: '姓名'
            },
            {
                name: 'code',
                data: 'code',
                title: '编码'
            }
        ],
        language: {
            "url": "/lib/datatables/styles/i18n/zh-CN.json"
        }
    });
});

function openEdit(id) {
    $.ajax({
        type: 'Get',
        url: "/major/Update",
        data: { "id": id },
        success: function (data) {
            $("#model").html(data)
        }
    });
}
function openDelete(id) {
    $.ajax({
        type: 'POST',
        url: "/major/Delete",
        data: { "id": id },
        success: function (data) {
            $("#model").html(data);
        }
    });
}

$("#openCreateModal").click(function () {
    $("#myCreateModal").modal("show");
});