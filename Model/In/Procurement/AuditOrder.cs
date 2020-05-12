namespace Model.In.Procurement
{
    /// <summary>
    /// 审批
    /// </summary>
    public class AuditOrder
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_sn { get; set; }

        /// <summary>
        /// 审核标记
        /// </summary>
        public string flag { get; set; }

        /// <summary>
        /// 审批备注
        /// </summary>
        public string remark { get; set; }
    }
}
