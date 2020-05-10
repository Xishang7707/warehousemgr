using Common;
using Model.Db.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dao.User
{
    /// <summary>
    /// 职位
    /// </summary>
    public static class PositionDao
    {
        /// <summary>
        /// 职位是否存在
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id">职位id</param>
        /// <returns></returns>
        public static async Task<bool> IsExist(DBHelper db, int id)
        {
            string sql = @"SELECT id FROM t_position WHERE `id`=@id AND `status`=@status";
            return (await db.QueryAsync<int>(sql, new { id, status = (int)EStatus.Normal })) > 0;
        }

        /// <summary>
        /// 获取部门id
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id">职位id</param>
        /// <returns></returns>
        public static async Task<int> GetDepartmentId(DBHelper db, int id)
        {
            string sql = @"SELECT department_id FROM t_position WHERE `id`=@id";
            return await db.QueryAsync<int>(sql, new { id });
        }

        /// <summary>
        /// 获取职位名称
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<string> GetPositionName(DBHelper db, int id)
        {
            string sql = @"SELECT `name` FROM t_position WHERE `id`=@id";
            return await db.QueryAsync<string>(sql, new { id = id });
        }
    }
}
