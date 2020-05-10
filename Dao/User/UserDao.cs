using Common;
using Model.Db;
using Model.Db.Enum;
using Model.In.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dao.User
{
    /// <summary>
    /// 用户
    /// </summary>
    public static class UserDao
    {
        /// <summary>
        /// 插入用户
        /// </summary>
        /// <param name="db"></param>
        /// <param name="data"></param>
        /// <param name="department_id"></param>
        /// <returns></returns>
        public static async Task<int> AddUser(DBHelper db, AddUserIn data, int department_id)
        {
            t_user user = new t_user
            {
                department_id = department_id,
                position_id = int.Parse(data.position_id),
                real_name = data.name,
                user_name = data.user_name
            };

            string sql = @"INSERT t_user(department_id,position_id,real_name,user_name) VALUES(@department_id,@position_id,@real_name,@user_name); SELECT LAST_INSERT_ID();";
            return await db.ExecAsync<int>(sql, user);
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<bool> UpdatePassword(DBHelper db, int id, string password)
        {
            string salt = MakeCommon.MakeSalt(id.ToString());
            string p = MakeCommon.MakeSalt(salt + id + password + id + salt);
            string pwd = ConcealCommon.EncryptDES(salt + p + salt);

            string sql = @"UPDATE t_user set `password`=@password, `salt`=@salt WHERE `id`=@id";
            return await db.ExecAsync(sql, new { password = pwd, salt = salt, id = id }) > 0;
        }

        /// <summary>
        /// 用户是否存在 user_name
        /// </summary>
        /// <param name="db"></param>
        /// <param name="user_name"></param>
        /// <returns></returns>
        public static async Task<bool> IsExist(DBHelper db, string user_name)
        {
            string sql = @"SELECT id FROM t_user WHERE `user_name`=@user_name AND `state`=@state AND `status`=@status";
            return await db.QueryAsync<int>(sql, new { user_name = user_name, status = (int)EStatus.Normal, state = (int)EState.Normal }) > 0;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="user_name"></param>
        /// <returns></returns>
        public static async Task<t_user> GetUser(DBHelper db, string user_name)
        {
            string sql = @"SELECT * FROM t_user WHERE `user_name`=@user_name";
            return await db.QueryAsync<t_user>(sql, new { user_name = user_name });
        }
    }
}
