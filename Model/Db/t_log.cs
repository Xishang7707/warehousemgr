using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Model.Db
{
    /// <summary>
    /// 日志
    /// </summary>
    public class t_log
    {
        public int id { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public string model { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public string data { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime make_time { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime add_time { get; set; }
    }
}
