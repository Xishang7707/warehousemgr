using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App;
using App.Interface;
using Common.Extern;
using Microsoft.AspNetCore.Mvc;
using Model.In;
using Model.Out.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace warehousemgr.Api
{
    public class BaseController : Controller
    {
        protected async Task<LoginResult> GetLoginUser()
        {
            return await AppFactory.Get<IUserApp>().GetLoginUser(Request.GetToken());
        }
        protected async Task<In> Package()
        {
            return new In
            {
                user = await GetLoginUser()
            };
        }

        protected async Task<In<T>> Package<T>(T data)
        {
            return new In<T>
            {
                data = data,
                user = await GetLoginUser()
            };
        }
    }
}
