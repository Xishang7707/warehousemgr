using Common;
using Dao.Procurement;
using Model.In;
using Model.In.Procurement;
using Model.Out;
using Service.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement.Procurement
{
    class ProcurementServiceImpl : IProcurementService
    {
        /// <summary>
        /// 验证采购单
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private Result VerifyAddProcurement(ApplyOrderIn data)
        {
            Result result = new Result();
            if (data == null)
            {
                result.msg = "参数错误";
                return result;
            }
            if (data.product_list == null || data.product_list.Count(c => c != null) == 0)
            {
                result.msg = "请填写需要采购的物品";
                return result;
            }

            data.product_list = data.product_list.Where(w => w != null).ToList();
            foreach (var item in data.product_list)
            {
                if (string.IsNullOrWhiteSpace(item.product_name?.Trim()))
                {
                    result.msg = "请填写产品名称";
                    return result;
                }
                item.product_name = item.product_name.Trim();
                if (string.IsNullOrWhiteSpace(item.util_name?.Trim()))
                {
                    result.msg = "请填写单位名称";
                    return result;
                }
                item.util_name = item.util_name.Trim();
                if (string.IsNullOrWhiteSpace(item.quantity?.Trim()) || !decimal.TryParse(item.quantity, out decimal _))
                {
                    result.msg = "请填写采购数量";
                    return result;
                }
                item.quantity = item.quantity.Trim();
                item.remark = item.remark.Trim();
            }

            result.result = true;
            return result;
        }
        public async Task<Result> AddProcurement(In<ApplyOrderIn> inData)
        {
            Result result = VerifyAddProcurement(inData.data);
            if (!result.result)
            {
                return result;
            }
            string order_sn;
            DBHelper db = new DBHelper();
            do
            {
                order_sn = MakeCommon.MakeOrder("IN");
            } while (!await ProcurementDao.IsExist(db, order_sn));

            try
            {
                db.BeginTransaction();
                //添加订单
                int order_id = await ProcurementDao.AddProcurement(db, order_sn, inData.user);
                if (order_id <= 0)
                {
                    db.Rollback();
                    result.msg = "操作失败[1]";
                    return result;
                }

                bool add_product_flag = await ProcurementProductPreDao.AddProcurementProduct(db, order_sn, inData.data.product_list);
                if (!add_product_flag)
                {
                    db.Rollback();
                    result.msg = "操作失败[2]";
                    return result;
                }

                db.Commit();
                result.result = true;
                result.msg = "操作成功";
            }
            catch (Exception e)
            {
                db.Rollback();
                result.msg = e.Message;
            }
            return result;
        }
    }
}
