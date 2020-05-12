using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Model.Db.Enum
{
    /// <summary>
    /// 审批标识
    /// </summary>
    public enum EAuditFlag
    {
        /// <summary>
        /// 同意
        /// </summary>
        [Description("同意")]
        Agree = 1,

        /// <summary>
        /// 拒绝
        /// </summary>
        [Description("拒绝")]
        Reject = 2
    }
}
