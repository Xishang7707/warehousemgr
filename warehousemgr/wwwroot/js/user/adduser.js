$(function () {
    var frame_flag = getQuery('frame');
    $('#btn-user-add').click(add_user);
    if (!frame_flag) {
        $('#btn-user-add').show();
    }
    layui.use(['form'], () => {
        var form = layui.form;
    });
});

function add_user(func) {
    var btn = $('#btn-user-add');
    if (btn.data('is_submit')) {
        return;
    }
    btn.data('is_submit', true);
    var load_index = layer.load(2, { shade: [0.4, '#0000'] });
    post({
        url: '../api/user/adduser',
        data: get_data(),
        done: o => {
            layer.close(load_index);
            if (func) {
                return func(o);
            }
            layer.msg(o.msg);
            setTimeout(() => {
                location.reload();
            }, 2000);
        },
        err: o => {
            layer.close(load_index);
            btn.data('is_submit', false);
            layer.msg(o.msg);
        }
    })
}

function get_data() {
    var form = $('#user-add-form');
    var data = {
        name: form.find('input[name=name]').val(),
        user_name: form.find('input[name=user_name]').val(),
        password: form.find('input[name=password]').val(),
        position_id: get_selected('#select-position'),
    };
    return data;
}