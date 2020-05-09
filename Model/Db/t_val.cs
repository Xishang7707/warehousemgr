using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Db
{
    /// <summary>
    /// 配置
    /// </summary>
    public class t_val
    {
        public int id { get; set; }

        /// <summary>
        /// 配置标识
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string val { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 系统状态
        /// </summary>
        public int state { get; set; }
    }
}
