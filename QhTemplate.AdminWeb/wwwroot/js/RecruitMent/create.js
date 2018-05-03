var width = document.getElementById("contentbox").offsetWidth;
var ue = UE.getEditor('contentbox', {
    toolbars: [
        [
            'anchor', //锚点
            'undo', //撤销
            'redo', //重做
            'bold', //加粗
            'indent', //首行缩进
            'snapscreen', //截图
            'italic', //斜体
            'underline', //下划线
            'strikethrough', //删除线
            'subscript', //下标
            'fontborder', //字符边框
            'superscript', //上标
            'formatmatch', //格式刷
            //'source', //源代码
            'blockquote', //引用
            'pasteplain', //纯文本粘贴模式
            'selectall', //全选
            // 'print', //打印
            // 'preview', //预览
            'horizontal', //分隔线
            'removeformat', //清除格式
            'time', //时间
            'date', //日期
            'unlink', //取消链接
            'insertrow', //前插入行
            'insertcol', //前插入列
            'mergeright', //右合并单元格
            'mergedown', //下合并单元格
            'deleterow', //删除行
            'deletecol', //删除列
            'splittorows', //拆分成行
            'splittocols', //拆分成列
            'splittocells', //完全拆分单元格
            'deletecaption', //删除表格标题
            'inserttitle', //插入标题
            'mergecells', //合并多个单元格
            'deletetable', //删除表格
            'cleardoc', //清空文档
            'insertparagraphbeforetable', //"表格前插入行"
            'insertcode', //代码语言
            'fontfamily', //字体
            'fontsize', //字号
            'paragraph', //段落格式
            'simpleupload', //单图上传
            'insertimage', //多图上传
            'edittable', //表格属性
            'edittd', //单元格属性
            'link', //超链接
            'emotion', //表情
            'spechars', //特殊字符
            'searchreplace', //查询替换
            'map', //Baidu地图
            //'gmap', //Google地图
            //'insertvideo', //视频
            //'help', //帮助
            'justifyleft', //居左对齐
            'justifyright', //居右对齐
            'justifycenter', //居中对齐
            'justifyjustify', //两端对齐
            'forecolor', //字体颜色
            'backcolor', //背景色
            'insertorderedlist', //有序列表
            'insertunorderedlist', //无序列表
            'fullscreen', //全屏
            'directionalityltr', //从左向右输入
            'directionalityrtl', //从右向左输入
            'rowspacingtop', //段前距
            'rowspacingbottom', //段后距
            'pagebreak', //分页
            //'insertframe', //插入Iframe
            'imagenone', //默认
            'imageleft', //左浮动
            'imageright', //右浮动
            'attachment', //附件
            'imagecenter', //居中
            'wordimage', //图片转存
            'lineheight', //行间距
            'edittip ', //编辑提示
            'customstyle', //自定义标题
            'autotypeset', //自动排版
            //'webapp', //百度应用
            'touppercase', //字母大写
            'tolowercase', //字母小写
            'background', //背景
            'template', //模板
            'scrawl', //涂鸦
            'music', //音乐
            'inserttable', //插入表格
            // 'drafts', // 从草稿箱加载
            'charts', // 图表
        ]
    ],
    initialFrameWidth: width,
    initialFrameHeight: 350,
    elementPathEnabled: false,
    autoHeightEnabled: false,
    imagePopup: false,
    zindex:99999


});

initFunction();
function initFunction(){
    init();
    GetArea();
}

function init(){
    $.ajax({
        url: "/Major/GetMajors",
        type: "Get",
        success: function(data) {
            var html = "";
            for (var i = 0; i < data.length; i++) {
                html += " <label style='margin-left: 20px;'><input type='checkbox' value='"+data[i].id+"' class='majorlist' />"+data[i].name+"</label>";
            }
            $("#major").html(html);
        }
    });
}


function GetArea() {
    $('#jstree').jstree({
        'core': {
            'data': {
                'url': '/Recruitment/GetAreas',
                'dataType': 'json'
            },
            "check_callback": true
        },
        'checkbox': {
            // 禁用级联选中
            'three_state': false,
            'cascade': 'undetermined' //有三个选项，up, down, undetermined; 使用前需要先禁用three_state
        },
        "plugins": ["themes", "json_data", "search", "checkbox"],

    });
}

function getUncertainNodeIds(id) {
    var parentids = new Array();
    $("#" + id).find(".jstree-undetermined ").each(function () {
        var id = $(this).parent().parent().attr("id");
        parentids.push(id);
    })
    return parentids;
}
function createArticle() {
    var content = ue.getContent();
    var subcontent = ue.getContentTxt();
    var title = $("#title").val();
    var majorlist=get_selected_major();
    var endtime = $("#endtime").val();

    var list = [];
    if ($('#jstree').children().length > 0) {
        var tree = $('#jstree').jstree();
        list = tree.get_checked().concat(getUncertainNodeIds("jstree"));
    }
    console.log(list)
    if (!title) {
        alert("标题不能为空");
        return;
    }
    if (!content) {
        alert("内容不能为空");
        return;
    }
    if (majorlist===null&&majorlist.length===0){
        alert("专业选项不能为空");
        return;
    }
    
    $.ajax({
        url: "/recruitment/Create",
        type:"post",
        data: {
            title: title,
            content: content,
            EndTime:endtime,
            MajorIds: majorlist,
            AreaId: list
        },
        success: function () {
            history.back(-1);
        }
    })
}

function get_selected_major(){
    var ele= $(".majorlist");
    var check_val = [];
    for(k in ele){
        if(ele[k].checked)
            check_val.push(ele[k].value);
    }
    return check_val;
}