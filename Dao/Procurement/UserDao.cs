using Common;
using Model.Db;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dao.Procurement
{
    /// <summary>
    /// 用户dao
    /// </summary>
    public static class UserDao
    {
        /// <summary>
        /// 获取用户姓名
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<string> GetUserRealName(DBHelper db, int id)
        {
            string sql = @"SELECT real_name FROM t_user WHERE `id`=@id";
            return await db.QueryAsync<string>(sql, new { id = id });
        }
    }
}
