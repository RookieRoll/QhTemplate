$(function () {
    //console.log(location);
    var pathName = location.pathname;
    var nowMenu = $('.sidebar-menu .treeview').find('a[href="' + pathName + '"]');

    //console.log(nowMenu);
    if (nowMenu.length > 0) {
        var parents = nowMenu.parents('ul[class$="treeview-menu"]');
        if (parents.length > 0) {
            var muneClick = $(parents[0]).prev();
            if (muneClick.length > 0) {
                muneClick.trigger('click');
            }
        }
        nowMenu.parent().addClass('selected');
    }
    //var msg = $('#errorMessage').html() || '';
    //if (msg.length > 0) {
    //    alert(msg);
    //}
})