using Model.In;
using Model.In.Procurement;
using Model.Out;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Interface
{
    /// <summary>
    /// 采购
    /// </summary>
    public interface IProcurementApp : IApp
    {
        /// <summary>
        /// 添加采购
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        Task<Result> AddProcurement(In<ApplyOrderIn> inData);
    }
}
