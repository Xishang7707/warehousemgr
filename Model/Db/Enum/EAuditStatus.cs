using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Model.Db.Enum
{
    /// <summary>
    /// 审批状态
    /// </summary>
    public enum EAuditStatus
    {
        /// <summary>
        /// 进行中
        /// </summary>
        [Description("进行中")]
        Progress = 0,

        /// <summary>
        /// 通过
        /// </summary>
        [Description("通过")]
        Pass = 1,

        /// <summary>
        /// 未通过
        /// </summary>
        [Description("未通过")]
        Fail = 2,
    }
}
