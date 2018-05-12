$(document).ready(function () {
    $('#dataTable').dataTable({
        lengthChange: true,
        serverSide: true,
        ajax: '/Resume/GetData',
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
                    html += "<li><a href=\"javaScript:void(0)\" onclick=\"openEdit(" + id + ")\">下载</a></li>";
                 
                    html += "</ul></form> </div>";
                    return html;
                }
            },
            {
                name: 'id',
                data: 'id',
                title: 'Id',
                visible: false,
            }, {
                name: "filename",
                data: "filename",
                title:"简历名称"
            },
            {
                name: 'username',
                data: 'username',
                title: '投递者'
            }, {
                name: 'useremail',
                data: 'useremail',
                title:'useremail'
            },
            {
                name: 'sendtime',
                data: 'sendtime',
                title: '投递时间',
            }
        
        ],
        language: {
            "url": "/lib/datatables/styles/i18n/zh-CN.json"
        }
    });
});