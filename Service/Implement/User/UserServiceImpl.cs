using Common;
using Dao.User;
using Model.Db;
using Model.In;
using Model.In.User;
using Model.Out;
using Model.Out.User;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement.User
{
    /// <summary>
    /// 用户
    /// </summary>
    class UserServiceImpl : IUserService
    {
        /// <summary>
        /// 验证添加用户数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<Result> VerifyAddUser(AddUserIn data)
        {
            Result result = new Result();
            if (data == null)
            {
                result.msg = "参数错误";
                return result;
            }
            if (string.IsNullOrWhiteSpace(data.name?.Trim()))
            {
                result.msg = "姓名不能为空";
                return result;
            }
            data.name = data.name?.Trim();
            if (!VerifyCommon.Name(data.name))
            {
                result.msg = "姓名只能为中文";
                return result;
            }
            if (data.name.Length < 2 || data.name.Length > 4)
            {
                result.msg = "姓名只能为2-4个字符";
                return result;
            }
            if (string.IsNullOrWhiteSpace(data.user_name?.Trim()))
            {
                result.msg = "用户名不能为空";
                return result;
            }
            data.user_name = data.user_name?.Trim();
            if (!VerifyCommon.UserName(data.user_name))
            {
                result.msg = "用户名只能由英文、数字、下划线组成";
                return result;
            }
            if (data.user_name.Length < 5 || data.user_name.Length > 12)
            {
                result.msg = "用户名只能为5-12个字符";
                return result;
            }
            if (string.IsNullOrWhiteSpace(data.password?.Trim()))
            {
                result.msg = "密码不能为空";
                return result;
            }
            data.password = data.password?.Trim();
            if (!VerifyCommon.Password(data.password))
            {
                result.msg = "密码只能由英文、数字、下划线组成";
                return result;
            }
            if (data.password.Length < 6 || data.password.Length > 18)
            {
                result.msg = "密码只能为6-18个字符";
                return result;
            }
            if (string.IsNullOrWhiteSpace(data.position_id?.Trim()))
            {
                result.msg = "请填写职位";
                return result;
            }
            if (!int.TryParse(data.position_id, out int position_id) || position_id <= 0)
            {
                result.msg = "职位信息错误";
                return result;
            }
            DBHelper db = new DBHelper();
            bool position_exist_flag = await PositionDao.IsExist(db, position_id);
            db.Close();
            if (!position_exist_flag)
            {
                result.msg = "职位不存在";
                return result;
            }

            result.result = true;
            return result;
        }
        public async Task<Result> AddUser(In<AddUserIn> inData)
        {
            Result result = await VerifyAddUser(inData.data);
            if (!result.result)
            {
                return result;
            }
            DBHelper db = new DBHelper();
            try
            {
                db.BeginTransaction();
                int position_id = int.Parse(inData.data.position_id);
                int department_id = await PositionDao.GetDepartmentId(db, position_id);

                int user_id = await UserDao.AddUser(db, inData.data, department_id);
                if (user_id <= 0)
                {
                    db.Rollback();
                    result.msg = "用户添加失败[1]";
                    return result;
                }
                bool update_user_pwd_flag = await UserDao.UpdatePassword(db, user_id, inData.data.password);
                if (!update_user_pwd_flag)
                {
                    db.Rollback();
                    result.msg = "用户添加失败[2]";
                    return result;
                }

                db.Commit();
                result.msg = "添加成功";
                result.result = true;
            }
            catch (Exception e)
            {
                db.Rollback();
                result.msg = "用户添加失败[3]";
            }
            return result;
        }

        /// <summary>
        /// 验证登录信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<Result> VerifyLogin(LoginIn data)
        {
            Result result = new Result();
            if (data == null)
            {
                result.msg = "参数错误";
                return result;
            }
            if (string.IsNullOrWhiteSpace(data.user_name?.Trim()))
            {
                result.msg = "用户名不能为空";
                return result;
            }
            data.user_name = data.user_name?.Trim();
            if (string.IsNullOrWhiteSpace(data.password?.Trim()))
            {
                result.msg = "密码不能为空";
                return result;
            }
            data.password = data.password?.Trim();

            if (!VerifyCommon.UserName(data.user_name))
            {
                result.msg = "用户名或密码错误";
                return result;
            }
            if (data.user_name.Length < 5 || data.user_name.Length > 12)
            {
                result.msg = "用户名或密码错误";
                return result;
            }

            if (!VerifyCommon.Password(data.password))
            {
                result.msg = "用户名或密码错误";
                return result;
            }
            if (data.password.Length < 6 || data.password.Length > 18)
            {
                result.msg = "用户名或密码错误";
                return result;
            }

            DBHelper db = new DBHelper();
            bool user_exist_flag = await UserDao.IsExist(db, data.user_name);
            db.Close();
            if (!user_exist_flag)
            {
                result.msg = "用户名或密码错误";
                return result;
            }
            result.result = true;
            return result;
        }
        public async Task<Result> Login(In<LoginIn> inData)
        {
            Result result = await VerifyLogin(inData.data);
            if (!result.result)
            {
                return result;
            }
            DBHelper db = new DBHelper();
            t_user user = await UserDao.GetUser(db, inData.data.user_name);
            bool password_flag = VerifyCommon.VerifyPassword(user.id, user.salt, user.password, inData.data.password);
            if (!password_flag)
            {
                db.Close();
                result.msg = "用户名或密码错误";
                return result;
            }

            LoginResult loginResult = new LoginResult
            {
                user_id = user.id,
                department_name = await DepartmentDao.GetDepartmentName(db, user.department_id),
                position_name = await PositionDao.GetPositionName(db, user.position_id),
                department_id = user.department_id,
                position_id = user.position_id,
                name = user.real_name,
                token = ConcealCommon.EncryptDES(user.id + DateTime.Now.ToString("yyy-MM-dd HH:mm:ss:ms")),
                user_name = user.user_name,

            };
            db.Close();

            await RedisHelper.Instance.SetStringKeyAsync($"user-multi-token:{loginResult.token}", loginResult, TimeSpan.FromHours(4));

            Result<LoginResult> result1 = new Result<LoginResult>
            {
                data = loginResult,
                result = true,
                msg = "登录成功"
            };
            return result1;
        }

        public async Task<LoginResult> GetLoginUser(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }
            return await RedisHelper.Instance.GetStringKeyAsync<LoginResult>($"user-multi-token:{token}");
        }
    }
}
