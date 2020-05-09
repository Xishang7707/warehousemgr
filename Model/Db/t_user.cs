using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Db
{
    /// <summary>
    /// 用户
    /// </summary>
    public class t_user
    {
        public int id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }

        public string salt { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string real_name { get; set; }

        /// <summary>
        /// 职位id
        /// </summary>
        public int position_id { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>
        public int department_id { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime add_time { get; set; }
    }
}
