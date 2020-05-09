using System;
using System.Collections.Generic;
using System.Text;

namespace Model.In.User
{
    /// <summary>
    /// 添加用户
    /// </summary>
    public class AddUserIn
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string position_id { get; set; }
    }
}
