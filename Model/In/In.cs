using Model.Out.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.In
{
    public class In
    {
        /// <summary>
        /// 登录用户
        /// </summary>
        public LoginResult user { get; set; }
    }

    public class In<T> : In
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T data { get; set; }
    }
}
