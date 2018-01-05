$(document).ready(function () {
	var id = $('#jstree').attr('data-id');
	$('#jstree').jstree({
		'core': {
			'data': {
				'url': '/User/RoleEdit/' + id,
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
})

function saveUserRole() {
	var id = $('#jstree').attr('data-id');
	var tree = $('#jstree').jstree();
	var list = tree.get_checked().concat(getUncertainNodeIds("jstree"));
	$.ajax({
		type: 'POST',
		url: "/User/RoleEdit",
		data: {
			"id": id,
			"roles": list,
		},
		success: function (data) {
			window.location.href = "/User/Index"
		},
	});
}

function getUncertainNodeIds(id) {
	var parentids = new Array();
	$("#" + id).find(".jstree-undetermined ").each(function () {
		parentids.push($(this).parent().parent().attr("id"));
	})
	return parentids;
}