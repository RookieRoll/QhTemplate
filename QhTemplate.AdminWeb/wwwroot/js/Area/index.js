$(document).ready($(function () {
    var id = null;
    var name = null;
    var myTree = $('#schoolArea');
    var dataTables = $("#school-table");
    var result = false;                             //设置搜索结果为失败
    myTree.jstree({
        'core': {
            'data': {
                'url': '/Area/GetAreas',
                'data': function (node) {
                    return { 'id': node.id };
                },
            },
            'strings': {
                'Loading ...': '请稍等...'
            },
            'check_callback': true,
            'animation': 200,                           //设置动画时间
            "themes": {
                'dots': false,                          //设置是否显示连接线
                'icons': true,                          //设置是否显示图标
                'responsive': false,                     //设置对小屏幕设备友好
            },
        },
        'dnd': {
            'open_timeout': 500,                        //设置拖动一个结点时，所到达结点打开的时间
        },
        'search': {
            'fuzzy': false,                             //是否开启模糊搜索
            'case_sensitive': false,                    //是否区分大小写
            'show_only_matches': true,                  //只显示匹配的，与模糊搜索不共存
            'close_opened_onclear': true,               //撤销搜索时搜索到的结果是否收回
            'search_leaves_only': false,                //是否只搜索叶子结点
        },
        "contextmenu": {
            'show_at_node': false,                      //设置菜单是否是在固定位置，false为鼠标坐标
            "items": {
                "create": null,
                "rename": null,
                "remove": null,
                "ccp": null,
                "新增区域": {
                    "label": "新增区域",
                    "action": function (data) {
                        var parentId = id;
                        $.ajax({
                            type: "get",
                            url: "/Area/CreateArea",
                            data: { "parentId": parentId },
                            success: function (msg) {
                                $("#modal").html(msg);
                                $("#submit").click(function () {
                                    var orgName = $("#text").val();
                                    var code = $("#code").val();
                                    $.ajax({
                                        type: "post",
                                        url: "/Area/CreateArea",
                                        data: { "orgName": orgName, "parentId": parentId, "code": code },
                                        success: function (msg) {
                                            alert(msg);
                                            $('#my-modal').modal('hide');
                                            myTree.jstree(true).refresh();
                                        }
                                    })
                                });
                            }
                        });
                    }
                },
                "删除区域": {
                    "label": "删除区域",
                    "action": function (obj) {
                        var orgId = id;
                        $.ajax({
                            type: "get",
                            url: "/Area/DeleteArea",
                            data: { "areaId": orgId },
                            success: function (msg) {
                                $("#modal").html(msg);
                                $("#submit").click(function () {
                                    $.ajax({
                                        type: "post",
                                        url: "/Area/RemoveAreaComfirm",
                                        data: { "areaId": orgId },
                                        success: function (msg) {
                                            alert(msg);
                                            $('#my-modal').modal('hide');
                                            myTree.jstree(true).refresh();
                                        },
                                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                                            alert("您要删除的部门下存在子部门，不可删除！");
                                            $('#my-modal').modal('hide');
                                        }
                                    })
                                });
                            }
                        });
                    }
                },
                "重命名": {
                    "label": "重命名",
                    "action": function (obj) {
                        var orgId = id;
                        $.ajax({
                            type: "get",
                            url: "/Area/UpdateArea",
                            data: { "id": orgId },
                            success: function (msg) {
                                $("#modal").html(msg);
                                $("#submit").click(function () {
                                    var orgName = $("#text").val();
                                    var code = $("#code").val();
                                    if (orgName != null) {
                                        $.ajax({
                                            type: "post",
                                            url: "/Area/UpdateArea",
                                            data: { "id": id, "name": orgName,"code":code },
                                            success: function (msg) {
                                                alert(msg);
                                                $('#my-modal').modal('hide');
                                                myTree.jstree(true).refresh();
                                            }
                                        })
                                    }
                                });
                            }
                        });
                    }
                }

            }
        },
        "plugins": ["themes", "json_data", "search", "wholerow", "contextmenu", 'sort', 'types'],
    });

    myTree.on("changed.jstree", function (e, data) {
        id = data.node === undefined ? undefined : data.node.id;
        name = data.node === undefined ? undefined : data.node.text;
        dataTables.dataTable().fnReloadAjax("/school/GetData?areaid=" + id);
        //location.reload();
    });



    $('#add-root').on('click', function (obj) {
        $.ajax({
            type: "get",
            url: "/Area/CreateArea",
            success: function (msg) {
                $("#modal").html(msg);
                $("#submit").click(function () {
                    var orgName = $("#text").val();
                    var code = $("#code").val();
                    $.ajax({
                        type: "post",
                        url: "/Area/CreateArea",
                        data: {
                            "orgName": orgName,
                            "code": code
                        },
                        success: function (msg) {
                            alert(msg);
                            $('#my-modal').modal('hide');
                            myTree.jstree(true).refresh();
                        }
                    })
                });
            }
        });
    });

    $('#search').keyup(function () {
        if (result) {
            clearTimeout(result);
        }
        result = setTimeout(function () {
            myTree.jstree(true).search($('#search').val());
        }, 250);
    });

    dataTables.dataTable({
        lengthChange: true,
        serverSide: true,
        ajax: '/school/GetData',
        data: { "areaid": id },
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
                visible: false,
            },
            {
                name: 'name',
                data: 'name',
                title: '名称',
            },
            {
                name: 'code',
                data: 'code',
                title: '编码'
            },
        ],
        language: {
            "url": "/lib/datatables/styles/i18n/zh-CN.json"
        },
    })

    $('#add-member').on('click', function (obj) {
        if (id == null) {
            alert("请选择一个部门！")
        }
        else {
            var orgId = id;
            $.ajax({
                type: "get",
                url: "/School/Create",
                data: { "areaId": orgId },
                success: function (msg) {
                    $("#modal").html(msg);
             
                }
            });

        }
    })
}));


function openEdit(id) {
    $.ajax({
        type: 'Get',
        url: "/school/Update",
        data: { "id": id },
        success: function (data) {
            $("#modal").html(data)
        }
    });
}

function openDelete(id) {
    $.ajax({
        type: 'Get',
        url: "/school/Delete",
        data: { "id": id },
        success: function (data) {
            $("#modal").html(data)
        }
    });
}

