using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Model.Db.Enum
{
    /// <summary>
    /// 可用状态
    /// </summary>
    public enum EStatus
    {
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disable = 0,

        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Normal = 1

    }
}
