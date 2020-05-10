$(function () {
    var frame_flag = getQuery('frame');
    if (!frame_flag) {
        $('#btn-submit').show();
    }
    layui.use(['form'], () => {

    });
    get({
        url: '../api/user/getuserinfo',
        done: o => {
            $('#name').val(o['data']['name']);
            $('#department_name').val(o['data']['department_name']);
            $('#position_name').val(o['data']['position_name']);
        }
    });
    add_product();
    $('#btn-product-add').click(add_product);
    $('#btn-submit').click(() => { apply_order(); });
});

function add_product() {
    var wapper = $('#procurement-add-form');
    var t = +new Date();
    var item = `
        <div class="apply-product-item" data-item-key='${t}'>
                    <fieldset class="layui-elem-field">
                        <form class="layui-form" action="">
                            <div class="layui-field-box">
                                <div class="layui-form-item">
                                    <div class="layui-inline">
                                        <label class="layui-form-label">物品名称</label>
                                        <div class="layui-input-inline" style="width: 225px;">
                                            <input type="text" name="product_name" autocomplete="off" class="layui-input" maxlength="30">
                                        </div>
                                    </div>
                                    <div class="layui-inline">
                                        <label class="layui-form-label">单位名称</label>
                                        <div class="layui-input-inline" style="width: 225px;">
                                            <input type="text" name="util_name" autocomplete="off" class="layui-input" maxlength="30">
                                        </div>
                                    </div>
                                    <div class="layui-inline">
                                        <label class="layui-form-label">规格</label>
                                        <div class="layui-input-inline" style="width: 225px;">
                                            <input type="text" name="package_size" autocomplete="off" class="layui-input" maxlength="30">
                                        </div>
                                    </div>
                                    <div class="layui-inline">
                                        <label class="layui-form-label">数量</label>
                                        <div class="layui-input-inline" style="width: 225px;">
                                            <input type="text" name="quantity" autocomplete="off" class="layui-input" maxlength="30">
                                        </div>
                                    </div>
                                    <div class="layui-inline">
                                        <label class="layui-form-label">备注</label>
                                        <div class="layui-input-inline" style="width: 225px;">
                                            <input type="text" name="remark" autocomplete="off" class="layui-input" maxlength="30">
                                        </div>
                                    </div>
                                </div>
                                <div class="layui-block">
                                    <div class="layui-btn-container" style='text-align: center;'>
                                        <button type="button" class="layui-btn layui-bg-red btn-rm-product">移除</button>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </fieldset>
                </div>
`;

    wapper.append($(item));
    wapper.find('button.btn-rm-product').click(remove_product);
}

function remove_product(e) {

    $(e.target).parents('.apply-product-item').remove();
}

function get_data() {
    var wapper = $('#procurement-add-form');
    var product_list = new Array();
    var dom_list = wapper.find('.apply-product-item');
    for (var i = 0; i < dom_list.length; i++) {
        var t = dom_list.eq(i);
        var item = {
            product_name: t.find('input[name=product_name]').val(),
            util_name: t.find('input[name=util_name]').val(),
            package_size: t.find('input[name=package_size]').val(),
            quantity: t.find('input[name=quantity]').val(),
            remark: t.find('input[name=remark]').val(),
        };

        product_list.push(item);
    }
    return product_list;
}

function apply_order(func) {
    var btn = $('#btn-submit');
    if (btn.data('is_submit')) {
        return;
    }
    btn.data('is_submit', true);
    var load_index = layer.load(2, { shade: [0.4, '#000'] });
    post({
        url: '../api/procurement/procurementapply',
        data: { product_list: get_data() },
        done: o => {
            layer.close(load_index);
            if (func) {
                return func(o);
            }
            layer.msg(o.msg);
            setTimeout(() => {
                $('#procurement-add-form').html();
                add_product();
                btn.data('is_submit', false);
            }, 2000);
        },
        err: o => {
            layer.close(load_index);
            btn.data('is_submit', false);
            layer.msg(o.msg);
        }
    })
}