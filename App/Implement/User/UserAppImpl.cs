using App.Interface;
using Model.In;
using Model.In.User;
using Model.Out;
using Model.Out.User;
using Service;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Implement.User
{
    class UserAppImpl : IUserApp
    {
        private static IUserService userApp = ServiceFactory.Get<IUserService>();
        public async Task<Result> AddUser(In<AddUserIn> inData)
        {
            return await userApp.AddUser(inData);
        }

        public async Task<LoginResult> GetLoginUser(string token)
        {
            return await userApp.GetLoginUser(token);
        }

        public async Task<Result> Login(In<LoginIn> inData)
        {
            return await userApp.Login(inData);
        }
    }
}
