using Model.Progress.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Progress
{
    /// <summary>
    /// 采购流程
    /// </summary>
    public class ProcurementItem
    {
        /// <summary>
        /// 职位id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public EProcurementAction act { get; set; }
    }
}
