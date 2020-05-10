using System;
using System.Collections.Generic;
using System.Text;

namespace Model.In.Procurement
{
    /// <summary>
    /// 采购单
    /// </summary>
    public class ApplyOrderIn
    {
        /// <summary>
        /// 产品列表
        /// </summary>
        public List<ApplyOrderItemIn> product_list { get; set; }
    }
}
