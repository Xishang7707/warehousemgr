$(function () {
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
    $('#btn-user-add').click(add_product);
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