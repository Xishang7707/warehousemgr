using Model.In;
using Model.In.User;
using Model.Out;
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
    }
}
