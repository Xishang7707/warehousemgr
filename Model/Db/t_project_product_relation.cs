using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Db
{
    /// <summary>
    /// 项目关联产品
    /// </summary>
    public class t_project_product_relation
    {
        public int id { get; set; }

        /// <summary>
        /// 项目id
        /// </summary>
        public int project_id { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string product_name { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal product_quantity { get; set; }
    }
}
