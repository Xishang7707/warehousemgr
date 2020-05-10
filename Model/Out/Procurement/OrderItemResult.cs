using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Out.Procurement
{
    /// <summary>
    /// 采购订单
    /// </summary>
    public class OrderItemResult
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_sn { get; set; }

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

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public string add_time { get; set; }
    }
}
