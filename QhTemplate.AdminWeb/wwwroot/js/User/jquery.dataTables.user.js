$(document).ready(function(){
    $('#dataTable').dataTable({
        lengthChange: true,
        serverSide: true,
        ajax: '/User/DataTable',
        columns: [
            {
                name: 'operate',
                title: '操作',
                sortable: false,
                searchable: false,
                render: function (data, type, row) {
                    var id = "'" + row.id + "'";
                    var html = "<div class=\"dropdown operator\">" +
                            "<button class=\"btn btn-default\" type=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">" +
                            "<span class=\"glyphicon glyphicon-th-list\"></span> </button>" +
                            "<ul class=\"dropdown-menu\" role=\"menu\" aria-labelledby=\"dLabel\">";
                    html += "<li><a href=\"javaScript:void(0)\">编辑</a><form action=\"/User/Update/" + row.id + "\" data-ajax=\"true\" data-ajax-method=\"get\"" +
                            "data-ajax-mode=\"replace\" data-ajax-update=\"#modal\" method=\"post\"></form></li>";
                    html += "<li><a href=\"javaScript:void(0)\">权限</a><form action=\"/User/Authorize/" + row.id + "\" data-ajax=\"true\" data-ajax-method=\"get\"" +
                            "data-ajax-mode=\"replace\" data-ajax-update=\"#modal\" method=\"post\"></form></li>";
                    html += "<li><a href=\"javaScript:void(0)\">角色</a><form action=\"/User/Role/" + row.id + "\" data-ajax=\"true\" data-ajax-method=\"get\"" +
                            "data-ajax-mode=\"replace\" data-ajax-update=\"#modal\" method=\"post\"></form></li>";
                    html += "<li><a href=\"javaScript:void(0)\">删除</a><form action=\"/User/Delete/" + row.id + "\" data-ajax=\"true\" data-ajax-method=\"get\"" +
                            "data-ajax-mode=\"replace\" data-ajax-update=\"#modal\" method=\"post\"></form></li>";
                    html += "</ul> </div>";
                    return html;
                }
            },
            {
                name: 'id',
                data: 'id',
                title: 'Id',
                visible:false,
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
           "url": "/Assets/datatables/styles/i18n/zh-CN.json"
        }
    })

    $('#dataTable').on('draw.dt', function () {
        $('.operator>.dropdown-menu>li>a').click(function () {
            $(this).next().submit();
        })
    })
})