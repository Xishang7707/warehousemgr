using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Out
{
    public class Result : IActionResult
    {
        /// <summary>
        /// 结果
        /// </summary>
        public bool result { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int status { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var resp = context.HttpContext.Response;
            resp.StatusCode = status != 0 ? status : result ? 200 : 403;
            resp.ContentType = "application/json";
            await resp.WriteAsync(JsonConvert.SerializeObject(this));
        }
    }

    public class Result<T> : Result
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T data { get; set; }
    }
}
