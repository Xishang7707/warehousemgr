using System;
using System.Collections.Generic;
using System.Text;

namespace Model.In.Procurement
{
    /// <summary>
    /// 采购单物品
    /// </summary>
    public class ApplyOrderItemIn
    {
        /// <summary>
        /// 物品名称
        /// </summary>
        public string product_name { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string util_name { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string package_size { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string quantity { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
