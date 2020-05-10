using Model.In;
using Model.In.User;
using Model.Out;
using Model.Out.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Interface
{
    public interface IUserApp : IApp
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        Task<Result> Login(In<LoginIn> inData);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        Task<Result> AddUser(In<AddUserIn> inData);

        /// <summary>
        /// 获取登录用户
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        Task<LoginResult> GetLoginUser(string token);
    }
}
