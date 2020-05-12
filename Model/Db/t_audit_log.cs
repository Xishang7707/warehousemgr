using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Db
{
    /// <summary>
    /// 审批记录
    /// </summary>
    public class t_audit_log
    {
        public int id { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string order_sn { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int user_id { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public int audit_status { get; set; }

        /// <summary>
        /// 审批步骤
        /// </summary>
        public int audit_step { get; set; }

        /// <summary>
        /// 职位id
        /// </summary>
        public int position_id { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? add_time { get; set; }
    }
}
