using Model.In;
using Model.In.User;
using Model.Out;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    /// <summary>
    /// 用户
    /// </summary>
    public interface IUserService : IService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        Result Login(In<LoginIn> inData);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        Result AddUser(In<AddUserIn> inData);
    }
}
