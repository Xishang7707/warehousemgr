using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Db
{
    /// <summary>
    /// 权限
    /// </summary>
    public class t_privilege
    {
        public int id { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime add_time { get; set; }
    }
}
