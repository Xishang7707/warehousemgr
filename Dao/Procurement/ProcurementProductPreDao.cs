using Common;
using Model.Db;
using Model.In.Procurement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dao.Procurement
{
    /// <summary>
    /// 采购需求
    /// </summary>
    public static class ProcurementProductPreDao
    {
        /// <summary>
        /// 添加采购的物品
        /// </summary>
        /// <param name="db"></param>
        /// <param name="order_sn"></param>
        /// <param name="product_list"></param>
        /// <returns></returns>
        public static async Task<bool> AddProcurementProduct(DBHelper db, string order_sn, List<ApplyOrderItemIn> product_list)
        {
            string sql = @"INSERT t_procurement_product_pre(order_sn, quantity, product_name,unit_name,package_size,remark) VALUES(@order_sn, @quantity, @product_name, @unit_name, @package_size, @remark)";
            List<t_procurement_product_pre> list = new List<t_procurement_product_pre>();
            foreach (var item in product_list)
            {
                list.Add(new t_procurement_product_pre
                {
                    order_sn = order_sn,
                    package_size = item.package_size,
                    product_name = item.product_name,
                    quantity = decimal.Parse(item.quantity),
                    unit_name = item.util_name,
                    remark = item.remark,
                });
            }

            return await db.ExecAsync(sql, list) == product_list.Count;
        }
    }
}
