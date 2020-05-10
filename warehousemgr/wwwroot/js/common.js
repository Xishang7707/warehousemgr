var api_host = 'http://8.129.167.212:7001/api/';//'http://127.0.0.1:7001/api/';

/**
 * GET请求
 * @param {any} param0
 */
function get({ url, data, async = true, done, err }) {
    $.ajax({
        url: url,
        data: data,
        headers: {
            'token': token(),
            'lang': lang()
        },
        async: async,
        type: 'get',
        success: o => {
            done && done(o);
        },
        error: o => {
            console.log(o);
            if (o.responseJSON)
                err && err(o.responseJSON);
            else err && err({ status: o.status, msg: '服务器请求异常' });
        }
    })
}

/**
 * POST请求
 * @param {any} param0
 */
function post({ url, data, async = true, done, err }) {
    $.ajax({
        url: url,
        headers: {
            'token': token(),
            'lang': lang()
        },
        contentType: 'application/json',
        data: JSON.stringify(data),
        type: 'post',
        processData: false,
        async: async,
        success: o => {
            done && done(o);
        },
        error: o => {
            console.log(o);
            if (o.responseJSON)
                err && err(o.responseJSON);
            else err && err({ status: o.status, msg: '服务器请求异常' });
        }
    })
}

/**
 * token
 * @param {any} v
 */
function token(v) {
    if (v || v == '') {
        Cookies.set('x-access-s', v);
    }
    return Cookies.get('x-access-s');
}

/**
 * lang
 * @param {any} v
 */
function lang(v) {
    if (v) {
        Cookies.set('lang', v);
    }
    return Cookies.get('lang');
}

/**
 * 获取顶层window
 */
function get_top_window() {
    var p = window.parent;
    while (p != p.window.parent) {
        p = p.window.parent;
    } return p;
}

var _cache_data = {};

/**
 * 登出
 * */
function login_out_tologin() {
    post({
        url: api_host + 'user/loginout',
        success: o => {
            to_login();
        },
        e: o => {
            to_login();
        }
    });
    token('');
}

function getQuery(variable) {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == variable) { return pair[1]; }
    }
    return '';
}

function get_selected(sor) {
    return $(sor).next('.layui-form-select').find('dl dd.layui-this').attr('lay-value');
}

function to_login() {
    get_top_window().location.href = 'login';
}