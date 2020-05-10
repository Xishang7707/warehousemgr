using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dao.Procurement
{
    /// <summary>
    /// 部门
    /// </summary>
    public static class DepartmentDao
    {
        /// <summary>
        /// 获取部门名称
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<string> GetDepartmentName(DBHelper db, int id)
        {
            string sql = @"SELECT `name` FROM t_department WHERE `id`=@id";
            return await db.QueryAsync<string>(sql, new { id = id });
        }
    }
}
