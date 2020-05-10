using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao.Procurement
{
    public static class PositionDao
    {
        /// <summary>
        /// 获取下级所有职位id
        /// </summary>
        /// <param name="db"></param>
        /// <param name="position_id"></param>
        /// <returns></returns>
        public static async Task<List<int>> GetChildId(DBHelper db, int position_id)
        {
            if (position_id == 0)
            {
                return null;
            }
            string sql = @"select id from t_position where parent_id=@position_id";
            List<int> list = await db.QueryListAsync<int>(sql, new { position_id = position_id });
            if (list.Count == 0)
            {
                return list;
            }
            List<int> list_2 = new List<int>();
            foreach (var item in list)
            {
                List<int> list_3 = await GetChildId(db, item);
                list_2.AddRange(list_3);
            }
            list.AddRange(list_2);
            return list;
        }
    }
}
