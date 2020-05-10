using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Out.User
{
    public class LoginResult : Result
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string position_name { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string department_name { get; set; }
    }
}
