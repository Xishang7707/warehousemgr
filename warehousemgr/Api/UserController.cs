using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App;
using App.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.In.User;
using Model.Out;
using Model.Out.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace warehousemgr.Api
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private static IUserApp userApp = AppFactory.Get<IUserApp>();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginIn model)
        {
            return await userApp.Login(await Package(model));
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("adduser")]
        public async Task<IActionResult> AddUser([FromBody]AddUserIn model)
        {
            return await userApp.AddUser(await Package(model));
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("getuserinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            return new Result<LoginResult> { data = await GetLoginUser(), result = true, msg = "OK" };
        }
    }
}
