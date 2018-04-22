$(document).ready(function () {
    $('#dataTable').dataTable({
        lengthChange: true,
        serverSide: true,
        ajax: '/Recruitment/GetData',
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
                name: 'title',
                data: 'title',
                title: '标题'
            },
            {
                name: 'createTime',
                data: 'createTime',
                title: '发布时间',
                sortable:false
            },
            {
                name: 'endTime',
                data: 'endTIme',
                title: '截至时间'
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
        url: "/Recruitment/Delete",
        data: { "id": id },
        success: function (data) {
            $("#model").html(data);
        }
    });
}

function openEdit(id) {
    location.href = "/Recruitment/Update/" + id; 
}
