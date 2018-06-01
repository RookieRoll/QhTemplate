﻿$(document).ready(function () {
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
                    html += "<li><a href=\"javaScript:void(0)\" onclick=\"openEdit(" + row.id + ")\">下载</a></li>";
                    html += "<li><a href=\"javaScript:void(0)\" onclick=\"openDelete(" + row.id + ")\">删除</a></li>";

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
                name: "fileName",
                data: "fileName",
                title: "简历名称"
            },
            {
                name: 'userName',
                data: 'userName',
                title: '投递者'
            }, {
                name: 'userEmail',
                data: 'userEmail',
                title: '邮箱'
            },
            {
                name: 'sendTime',
                data: 'sendTime',
                title: '投递时间',
            },
            {
                name: 'status',
                data: 'status',
                title: '状态'
            }

        ],
        language: {
            "url": "/lib/datatables/styles/i18n/zh-CN.json"
        }
    });
});


function openEdit(id) {
    //location.href = "http://localhost:54791/FileDownload/DownLoad?file=" + filename;
    //var filename = companyId + "@" + userId + "@" + filename;
    var url = "http://localhost:54791/FileDownload/CompanyDownloadFile/" + id;
    console.log(url)
    window.open(url, "_blank");
}

function openDelete(id) {
    $.ajax({
        type: 'POST',
        url: "/Resume/Delete",
        data: { "id": id },
        success: function (data) {
            $("#model").html(data);
        }
    });
}
