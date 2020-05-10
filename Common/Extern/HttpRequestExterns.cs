using Microsoft.AspNetCore.Http;

namespace Common.Extern
{
    public static class HttpRequestExterns
    {
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="_req"></param>
        /// <returns></returns>
        public static string GetToken(this HttpRequest _req)
        {
            string token = _req.Headers["x-access-s"];
            if (string.IsNullOrWhiteSpace(token))
            {
                token = _req.Cookies["x-access-s"];
            }
            return token;
        }

        /// <summary>
        /// 获取lang
        /// </summary>
        /// <param name="_req"></param>
        /// <returns></returns>
        public static string GetLang(this HttpRequest _req)
        {
            return _req.Headers["lang"];
        }
    }
}
