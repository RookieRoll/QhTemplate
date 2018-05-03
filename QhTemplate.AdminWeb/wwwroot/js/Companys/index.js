$(document).ready(function () {
    $('#dataTable').dataTable({
        lengthChange: true,
        serverSide: true,
        ajax: '/Company/GetData',
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
                title: '公司名'
            },
            {
                name: 'legalPerson',
                data: 'legalPerson',
                title: '法人'
            },
            {
                name: 'tellPhone',
                data: 'tellPhone',
                title: '联系电话'
            },
            {
                name: 'email',
                data: 'email',
                title: '邮箱'
            },
            {
                name: 'createTime',
                data: 'createTime',
                title: '创建时间'
            }
        ],
        language: {
            "url": "/lib/datatables/styles/i18n/zh-CN.json"
        }
    });
});

function openDelete(id) {
    $.ajax({
        type: 'POST',
        url: "/Company/Delete",
        data: { "id": id },
        success: function (data) {
            $("#model").html(data);
        }
    });
}
