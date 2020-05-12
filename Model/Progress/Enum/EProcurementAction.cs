using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Model.Progress.Enum
{
    /// <summary>
    /// 采购流程操作
    /// </summary>
    public enum EProcurementAction
    {
        /// <summary>
        /// 审批
        /// </summary>
        [Description("审批")]
        Apply = 1,

        /// <summary>
        /// 审批
        /// </summary>
        [Description("采购")]
        Procurement = 1,
    }
}
