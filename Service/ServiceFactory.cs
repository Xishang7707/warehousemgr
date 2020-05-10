using Service.Implement.Procurement;
using Service.Implement.User;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    /// <summary>
    /// 服务
    /// </summary>
    public static class ServiceFactory
    {
        public static T Get<T>(params object[] o) where T : class, IService
        {
            if (typeof(T) == typeof(IUserService))
                return new UserServiceImpl() as T;
            if (typeof(T) == typeof(IProcurementService))
                return new ProcurementServiceImpl() as T;

            return null;
        }
    }
}
