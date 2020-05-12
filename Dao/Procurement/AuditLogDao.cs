using Common;
using Model.Db;
using Model.Db.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dao.Procurement
{
    /// <summary>
    /// 审批记录
    /// </summary>
    public static class AuditLogDao
    {
        /// <summary>
        /// 添加审批记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="order_sn">订单号</param>
        /// <param name="user_id">用户id</param>
        /// <param name="flag">审批标记</param>
        /// <param name="position_id">职位id</param>
        /// <param name="audit_step">审批步骤</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public static async Task<bool> AddAuditLog(DBHelper db, string order_sn, int user_id, EAuditFlag flag, int position_id, int audit_step, string remark)
        {
            string sql = @"INSERT t_audit_log(`order_sn`,`user_id`,`audit_status`,`audit_step`,`position_id`,`remark`)
                                    VALUES(@order_sn,@user_id,@audit_status,@audit_step,@position_id,@remark)";

            t_audit_log model = new t_audit_log
            {
                order_sn = order_sn,
                audit_step = audit_step,
                audit_status = (int)flag,
                position_id = position_id,
                remark = remark,
                user_id = user_id
            };

            return await db.ExecAsync(sql, model) > 0;
        }
    }
}
