using System;
using System.Collections.Generic;
using System.Text;

namespace Model.In.User
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public class LoginIn
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
    }
}
