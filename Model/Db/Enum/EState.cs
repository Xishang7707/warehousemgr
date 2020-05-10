using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Model.Db.Enum
{
    /// <summary>
    /// 系统状态
    /// </summary>
    public enum EState
    {
        /// <summary>
        /// 被删除
        /// </summary>
        [Description("被删除")]
        Delete = 0,

        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 1
    }
}
