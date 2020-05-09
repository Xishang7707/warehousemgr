using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Db
{
    /// <summary>
    /// 项目
    /// </summary>
    public class t_project
    {
        public int id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 系统状态
        /// </summary>
        public int state { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime add_time { get; set; }
    }
}
