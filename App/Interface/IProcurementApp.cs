﻿using Model.In;
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

        /// <summary>
        /// 获取采购单 仅用户可操作或已操作的
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        Task<Result> GetOrderList(In inData);

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        Task<Result> AuditOrder(In<AuditOrder> inData);
    }
}
