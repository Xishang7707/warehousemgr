using App.Implement.User;
using App.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    /// <summary>
    /// APP
    /// </summary>
    public static class AppFactory
    {
        public static T Get<T>(params object[] o) where T : class, IApp
        {
            if (typeof(T) == typeof(IUserApp))
                return new UserAppImpl() as T;

            return null;
        }
    }
}
