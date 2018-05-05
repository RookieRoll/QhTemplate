$(document).ready(function () {
    $('#dataTable').dataTable({
        lengthChange: true,
        serverSide: true,
        ajax: '/CareerTalk/GetData',
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

                    html += "<li><a href=\"javaScript:void(0)\" onclick=\"openDelete(" + id + ")\">删除</ a></li>";

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
                name: 'companyName',
                data: 'companyName',
                title: '公司名'
            },
            {
                name: 'held',
                data: 'held',
                title: '举办地点'
            },
            {
                name: 'startTime',
                data: 'startTime',
                title: '开始时间'
            },
            {
                name: 'publishTime',
                data: 'publishTime',
                title: '发布时间'
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
        url: "/CareerTalk/Delete",
        data: { "id": id },
        success: function (data) {
            $("#model").html(data);
        }
    });
}

function openEdit(id) {
    location.href = "/CareerTalk/Update/" + id;
}
