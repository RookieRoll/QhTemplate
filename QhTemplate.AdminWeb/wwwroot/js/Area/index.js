$(document).ready($(function () {
    var id = null;
    var name = null;
    var myTree = $('#schoolArea');
    var dataTables = $("#schoolArea-table");
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
                                    $.ajax({
                                        type: "post",
                                        url: "/Area/CreateArea",
                                        data: { "orgName": orgName, "parentId": parentId },
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
                            url: "/Area/RemoveArea",
                            data: { "orgId": orgId },
                            success: function (msg) {
                                $("#modal").html(msg);
                                $("#submit").click(function () {
                                    $.ajax({
                                        type: "post",
                                        url: "/Area/RemoveAreaComfirm",
                                        data: { "orgId": orgId },
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
                            data: { "orgId": orgId },
                            success: function (msg) {
                                $("#modal").html(msg);
                                $("#submit").click(function () {
                                    var orgName = $("#text").val();
                                    if (orgName != null) {
                                        $.ajax({
                                            type: "post",
                                            url: "/Area/UpdateArea",
                                            data: { "orgId": orgId, "orgName": orgName },
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
                // "添加员工": {
                //     "label": "添加员工",
                //     "action": function (obj) {
                //         var orgId = id;
                //         $.ajax({
                //             type: "get",
                //             url: "/Organization/AddUsersToOrganization",
                //             data: { "orgId": orgId },
                //             success: function (msg) {
                //                 $("#modal").html(msg);
                //                 addUser(orgId);
                //                 var usersId = [];
                //                 $("#submit-user").click(function () {
                //                     $("input:checkbox:checked").each(function () {
                //                         usersId.push($(this).val());
                //                     });
                //                     addNewUser(orgId, usersId);
                //                 })
                //             }
                //         });
                //     }
                // },
            }
        },
        "plugins": ["themes", "json_data", "search", "wholerow", "contextmenu", 'dnd', 'sort', 'types'],
    });

    myTree.on("changed.jstree", function (e, data) {
        id = data.node === undefined ? undefined : data.node.id;
        name = data.node === undefined ? undefined : data.node.text;
        dataTables.dataTable().fnReloadAjax("/Organization/GetUsersFromOrganization?orgId=" + id);
        //location.reload();
    });

    myTree.on('move_node.jstree', function (e, data) {
        var orgId = data.node.id, parentId = data.node.parent;
        $.ajax({
            type: "get",
            url: "/Area/MigrateArea",
            data: { "orgId": orgId, "parentId": parentId },
            success: function (msg) {
                $("#modal").html(msg);
                $("#cancel").click(function () {
                    myTree.jstree(true).refresh();
                })
                $("#submit").click(function () {
                    $.ajax({
                        type: "post",
                        url: "/Area/MigrateArea",
                        data: { "orgId": orgId, "parentId": parentId },
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

    $('#add-root').on('click', function (obj) {
        $.ajax({
            type: "get",
            url: "/Organization/CreateOrganization",
            success: function (msg) {
                $("#modal").html(msg);
                $("#submit").click(function () {
                    var orgName = $("#text").val();
                    $.ajax({
                        type: "post",
                        url: "/Organization/CreateOrganization",
                        data: { "orgName": orgName },
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
        ajax: '/Organization/GetUsersFromOrganization',
        data: { "orgId": id },
        columns: [
            {
                name: 'operate',
                title: '操作',
                sortable: false,
                searchable: false,
                render: function (data, type, row) {
                    var html = "";
                    html = "<div class=\"operator\"><button class=\"btn btn-default\" type=\"button\" onclick = \"removeUser(" + id + "," + row.id + ")\">移除</button></div>";
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
                name: 'emailaddress',
                data: 'emailAddress',
                title: '邮箱'
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
                url: "/Organization/AddUsersToOrganization",
                data: { "orgId": orgId },
                success: function (msg) {
                    $("#modal").html(msg);
                    addUser(orgId);
                    var usersId = [];
                    $("#submit-user").click(function () {
                        $("input:checkbox:checked").each(function () {
                            usersId.push($(this).val());
                        });
                        addNewUser(orgId, usersId);
                    })
                }
            });

        }
    })
}));

function addUser(orgId) {
    var userData = $("#add-user-table");
    userData.dataTable({
        lengthChange: true,
        serverSide: true,
        ajax: '/Organization/GetUsers?orgId=' + orgId,
        columns: [
            {
                name: 'operate',
                title: '人员列表',
                render: function (data, type, row) {
                    var id = "'" + row.id + "'";
                    if (row.checked) {
                        var html = "<div class=\"operator\">" + row.name + "<\/div>";
                    }
                    else {
                        var html = "<div class=\"operator\"><input type=\"checkbox\" value=\"" + row.id + "\">" + row.name + "</div>";
                    }
                    return html;
                }
            },
            {
                name: 'id',
                data: 'id',
                title: 'Id',
                visible: false,
            },
        ],
        language: {
            "url": "/lib/datatables/styles/i18n/zh-CN.json"
        }

    })
}

function addNewUser(orgId, usersId) {
    $.ajax({
        type: "post",
        url: "/Organization/AddUsersToOrganization",
        data: { "orgId": orgId, "usersId": usersId },
        success: function (msg) {
            alert(msg);
            $('#my-modal').modal('hide');
            $('#organization').jstree(true).refresh();
        }
    })
}

function removeUser(orgId, userId) {
    $.ajax({
        type: "get",
        url: "/Organization/RemoveUserFromOrganization",
        data: { "orgId": orgId, "userId": userId },
        success: function (msg) {
            $("#modal").html(msg);
            $("#submit").click(function () {
                $.ajax({
                    type: "post",
                    url: "/Organization/RemoveUserFromOrganization",
                    data: { "orgId": orgId, "userId": userId },
                    success: function (msg) {
                        alert(msg);
                        $('#my-modal').modal('hide');
                        $('#organization').jstree(true).refresh();
                    }
                })
            })
        }
    })
}



