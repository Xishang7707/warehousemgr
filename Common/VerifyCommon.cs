using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    /// <summary>
    /// 验证
    /// </summary>
    public static class VerifyCommon
    {
        /// <summary>
        /// 验证姓名中文
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool Name(string name) => new Regex("^[\u4E00-\u9FA5]+$").IsMatch(name);

        /// <summary>
        /// 用户名 英文数字
        /// </summary>
        /// <param name="user_name"></param>
        /// <returns></returns>
        public static bool UserName(string user_name) => new Regex("^[\\w]+$").IsMatch(user_name);

        /// <summary>
        /// 密码 英文数字下划线
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Password(string password) => new Regex("^[\\w]+$").IsMatch(password);

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="salt"></param>
        /// <param name="pwd"></param>
        /// <param name="pwd2"></param>
        /// <returns></returns>
        public static bool VerifyPassword(int id, string salt, string pwd, string pwd2)
        {
            string p = MakeCommon.MakeMD5(salt + id + pwd2 + id + salt);
            string enc_pwd = ConcealCommon.EncryptDES(salt + p + salt);
            return enc_pwd == pwd;
        }
    }
}
