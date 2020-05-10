using Model.In;
using Model.In.Procurement;
using Model.Out;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    /// <summary>
    /// 采购
    /// </summary>
    public interface IProcurementService : IService
    {
        /// <summary>
        /// 添加采购
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        Task<Result> AddProcurement(In<ApplyOrderIn> inData);
    }
}
