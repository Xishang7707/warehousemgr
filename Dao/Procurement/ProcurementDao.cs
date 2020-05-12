using Common;
using Model.Db;
using Model.Db.Enum;
using Model.In.Procurement;
using Model.Out.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dao.Procurement
{
    /// <summary>
    /// 采购单
    /// </summary>
    public static class ProcurementDao
    {
        /// <summary>
        /// 添加采购单
        /// </summary>
        /// <param name="order_sn">订单号</param>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public static async Task<int> AddProcurement(DBHelper db, string order_sn, LoginResult user)
        {
            t_procurement model = new t_procurement
            {
                department_id = user.department_id,
                position_id = user.position_id,
                user_id = user.user_id,
                order_sn = order_sn,
                audit_step = 0,
                audit_status = (int)EAuditStatus.Progress
            };

            string sql = @"INSERT t_procurement(`order_sn`,`user_id`,`position_id`,`audit_step`,`audit_status`,`department_id`) VALUES(@order_sn,@user_id,@position_id,@audit_step,@audit_status,@department_id); SELECT LAST_INSERT_ID();";
            return await db.ExecAsync<int>(sql, model);
        }

        /// <summary>
        /// 订单号是否存在
        /// </summary>
        /// <param name="order_sn"></param>
        /// <returns></returns>
        public static async Task<bool> IsExist(DBHelper db, string order_sn)
        {
            string sql = @"SELECT id FROM t_procurement WHERE order_sn=@order_sn;";
            return await db.QueryAsync<int>(sql, new { order_sn = order_sn }) > 0;
        }

        /// <summary>
        /// 获取采购单
        /// </summary>
        /// <param name="db"></param>
        /// <param name="position_arr"></param>
        /// <returns></returns>
        public static async Task<List<t_procurement>> GetList(DBHelper db, int[] position_arr)
        {
            string sql = @"SELECT * FROM t_procurement WHERE position_id in @position_arr";
            return await db.QueryListAsync<t_procurement>(sql, new { position_arr = position_arr });
        }

        /// <summary>
        /// 获取采购单
        /// </summary>
        /// <param name="db"></param>
        /// <param name="order_sn"></param>
        /// <returns></returns>
        public static async Task<t_procurement> GetOrder(DBHelper db, string order_sn)
        {
            string sql = @"SELECT * FROM t_procurement WHERE `order_sn`=@order_sn";
            return await db.QueryAsync<t_procurement>(sql, new { order_sn = order_sn });
        }

        /// <summary>
        /// 同意审批
        /// </summary>
        /// <param name="db"></param>
        /// <param name="order_sn">订单号</param>
        /// <param name="audit_step">审批步骤</param>
        /// <returns></returns>
        public static async Task<bool> AuditAgreeOrder(DBHelper db, string order_sn, int audit_step)
        {
            string sql = @"UPDATE t_procurement SET audit_step=@audit_step WHERE order_sn=@order_sn";
            return await db.ExecAsync(sql, new { audit_step = audit_step, order_sn = order_sn }) > 0;
        }

        /// <summary>
        /// 拒绝审批
        /// </summary>
        /// <param name="db"></param>
        /// <param name="order_sn">订单号</param>
        /// <param name="audit_step">审批步骤</param>
        /// <returns></returns>
        public static async Task<bool> AuditRejectOrder(DBHelper db, string order_sn, int audit_step)
        {
            string sql = @"UPDATE t_procurement SET audit_step=@audit_step, audit_status=@audit_status WHERE order_sn=@order_sn";
            return await db.ExecAsync(sql, new { audit_step = audit_step, audit_status = (int)EAuditStatus.Fail, order_sn = order_sn }) > 0;
        }
    }
}
