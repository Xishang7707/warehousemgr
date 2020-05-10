using Common;
using Model.Db;
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
                order_sn = order_sn
            };

            string sql = @"INSERT t_procurement(`order_sn`,`user_id`,`position_id`,`department_id`) VALUES(@order_sn,@user_id,@position_id,@department_id); SELECT LAST_INSERT_ID();";
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
    }
}
