using App;
using App.Interface;
using Common;
using Common.Extern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Model.Out;
using Model.Out.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using warehousemgr.Attributes;

namespace warehousemgr.Middleware
{
    /// <summary>
    /// 授权验证接口
    /// </summary>
    /// <summary>
    /// 授权中间件
    /// </summary>
    public class AuthMiddleware
    {
        private RequestDelegate next;
        public AuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            bool is_api = context.Request.Path.ToString().ToLower().StartsWith("/api/");
            try
            {
                bool flag = false;
                if (is_api)
                {
                    flag = await VerifyApi(context);
                }
                else
                {
                    flag = await VerifyPage(context);
                }

                if (flag)
                {
                    await next.Invoke(context);
                }
            }
            catch (Exception ex)
            {
                if (is_api)
                {
                    await context.Response.WriteBodyAsync(new Result
                    {
                        msg = ex.Message,
                        status = 500
                    });
                }
                else
                {
                    context.Response.Redirect("/other/error_500", false);
                }
            }
        }

        /// <summary>
        /// 验证页面
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<bool> VerifyPage(HttpContext context)
        {
            Endpoint endpoint = context.GetEndpoint();

            if (endpoint == null)
            {
                context.Response.Redirect("/other/error_404", false);
                return false;
            }

            if (!endpoint.Metadata.Any(a => a is IAllowAnonymous))
            {
                //--更换为权限验证
                if (!(await VerifyUser(context)))
                {
                    context.Response.Redirect("/login", false);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 验证api
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<bool> VerifyApi(HttpContext context)
        {
            Endpoint endpoint = context.GetEndpoint();
            Result result = new Result();
            if (endpoint == null)
            {
                result.result = false;
                result.status = 404;
                result.msg = "未知请求";
                await context.Response.WriteBodyAsync(result);
                return false;
            }

            //身份验证
            LoginResult user = await AppFactory.Get<IUserApp>().GetLoginUser(context.Request.GetToken());
            if (!await IDentityVerify(context, user))
            {
                Result res = new Result
                {
                    msg = "无权访问",
                    status = 401
                };
                await context.Response.WriteBodyAsync(res);
                return false;
            }

            //不需要验证用户
            if (user == null || user.user_id == 1)
            {
                return true;
            }

            ////token 续期
            //IUserServer userServer = new UserServerImpl();
            //await userServer.TokenRenewalAsync(user.token, user);

            //验证权限
            //if (!await PrivilegeVerify(context, user))
            //{
            //    Result res = new Result
            //    {
            //        code = ErrorCodeConst.ERROR_1035,
            //        status = ErrorCodeConst.ERROR_400
            //    };
            //    await context.Response.WriteBodyAsync(res);
            //    return false;
            //}
            return true;
        }

        /// <summary>
        /// @xis 验证用户
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<bool> VerifyUser(HttpContext context)
        {
            try
            {
                //1.身份验证
                Result<LoginResult> res = await HttpUtils.HttpGet<Result<LoginResult>>($"{context.Request.Scheme}://{context.Request.Host}/api/user/getuserinfo", _token: context.Request.GetToken(), _lang: context.Request.GetLang());

                if (res == null || !res.result)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// @xis 验证权限
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<bool> VerifyPrivilege(HttpContext context)
        {
            //1.身份验证
            LoginResult user = await HttpUtils.HttpGet<LoginResult>("http://127.0.0.1:7001/api/user/getuserinfo", _token: context.Request.GetToken(), _lang: context.Request.GetLang());
            if (user == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// @xis 身份验证 2020-3-29 09:43:17
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<bool> IDentityVerify(HttpContext context, LoginResult user)
        {
            if (context.GetEndpoint().Metadata.Any(a => a is IAllowAnonymous))
            {
                return true;
            }

            if (user == null)
            {
                return false;
            }

            return await Task.FromResult(true);
        }

        /// <summary>
        /// @xis 权限验证 2020-3-29 09:43:30
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<bool> PrivilegeVerify(HttpContext context, LoginResult user)
        {
            if (context.GetEndpoint().Metadata.Any(a => a is PrivilegeAnyAttribute))
            {
                return true;
            }

            IEnumerable<PrivilegeAttribute> privilege_list = context.GetEndpoint().Metadata.Where(w => w is PrivilegeAttribute).Select(s => s as PrivilegeAttribute);
            if (privilege_list.Count() == 0)
            {
                return true;
            }
            //IPrivilegeServer privilegeServer = new PrivilegeServerImpl();
            //foreach (var item in privilege_list)
            //{
            //    if (await privilegeServer.HasPrivilege(user.user_id, item.privilege_key))
            //    {
            return true;
            //    }
            //}

            //return false;
        }
    }
}
