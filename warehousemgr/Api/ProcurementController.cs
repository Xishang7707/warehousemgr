using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App;
using App.Interface;
using Microsoft.AspNetCore.Mvc;
using Model.In.Procurement;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace warehousemgr.Api
{
    [Route("api/[controller]")]
    public class ProcurementController : BaseController
    {
        private static IProcurementApp procurementApp = AppFactory.Get<IProcurementApp>();

        /// <summary>
        /// 采购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("procurementapply")]
        public async Task<IActionResult> ProcurementApply([FromBody]ApplyOrderIn model)
        {
            return await procurementApp.AddProcurement(await Package(model));
        }

        /// <summary>
        /// 采购单
        /// </summary>
        /// <returns></returns>
        [HttpGet("getorders")]
        public async Task<IActionResult> GetOrders()
        {
            return await procurementApp.GetOrderList(await Package());
        }

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("auditorder")]
        public async Task<IActionResult> AuditOrder([FromBody]AuditOrder model)
        {
            return await procurementApp.AuditOrder(await Package(model));
        }
    }
}
