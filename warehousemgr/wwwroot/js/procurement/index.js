$(function () {
    $('#open-procurement-add').click(open_add_procurement);
})


function open_add_procurement() {
    layer.open({
        type: 2,
        title: '申请采购',
        content: 'applyprocurement?frame=1',
        area: ['100%', '100%'],
        btn: ['提交'],
        yes: function (index, layero) {
            var body = layer.getChildFrame('body', index);
            var iframeWin = window[layero.find('iframe')[0]['name']]; //得到iframe页的窗口对象，执行iframe页的方法：iframeWin.method();
            iframeWin.apply_order(o => {
                var w = get_top_window();
                w.layer.msg(o.msg);
                layer.close(index);
                location.reload();
            });
        }
    });
}