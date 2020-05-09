using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Db
{
    /// <summary>
    /// 职位
    /// </summary>
    public class t_position
    {
        public int id { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 上级id
        /// </summary>
        public int? parent_id { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>
        public int department_id { get; set; }

        /// <summary>
        /// 系统状态
        /// </summary>
        public int state { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
    }
}
