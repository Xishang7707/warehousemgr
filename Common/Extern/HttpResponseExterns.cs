using Microsoft.AspNetCore.Http;
using Model.Out;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extern
{
    public static class HttpResponseExterns
    {
        /// <summary>
        /// @xis 向客户端写数据 2020-2-21 12:43:19
        /// </summary>
        /// <param name="res"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Task WriteBodyAsync(this HttpResponse res, object obj)
        {
            string str = JsonConvert.SerializeObject(obj);
            var bt = Encoding.Default.GetBytes(str);
            res.ContentType = "application/json";
            return res.Body.WriteAsync(bt, 0, bt.Length);
        }

        /// <summary>
        /// @xis 向客户端写数据 2020-2-21 12:43:19
        /// </summary>
        /// <param name="res"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Task WriteBodyAsync(this HttpResponse res, Result obj)
        {
            res.HttpContext.Response.StatusCode = obj.status != 0 ? obj.status : obj.result ? 200 : 403;
            return res.WriteBodyAsync(obj as object);
        }
    }
}
