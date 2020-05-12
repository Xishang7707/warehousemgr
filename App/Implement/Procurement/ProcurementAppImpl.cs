using App.Interface;
using Model.In;
using Model.In.Procurement;
using Model.Out;
using Service;
using Service.Interface;
using System.Threading.Tasks;

namespace App.Implement.Procurement
{
    class ProcurementAppImpl : IProcurementApp
    {
        private static IProcurementService procurementService = ServiceFactory.Get<IProcurementService>();
        public async Task<Result> AddProcurement(In<ApplyOrderIn> inData)
        {
            return await procurementService.AddProcurement(inData);
        }

        public async Task<Result> AuditOrder(In<AuditOrder> inData)
        {
            return await procurementService.AuditOrder(inData);
        }

        public async Task<Result> GetOrderList(In inData)
        {
            return await procurementService.GetOrderList(inData);
        }
    }
}
