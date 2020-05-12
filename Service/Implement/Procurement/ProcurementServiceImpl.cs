using Common;
using Dao.Procurement;
using Model.Db;
using Model.Db.Enum;
using Model.In;
using Model.In.Procurement;
using Model.Out;
using Model.Out.Procurement;
using Model.Progress;
using Model.Progress.Enum;
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
            } while (await ProcurementDao.IsExist(db, order_sn));

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

        public async Task<Result> GetOrderList(In inData)
        {
            DBHelper db = new DBHelper();
            List<int> position_list = await PositionDao.GetChildId(db, inData.user.position_id);
            position_list.Add(inData.user.position_id);
            position_list = position_list.Distinct().ToList();
            List<t_procurement> order_list = await ProcurementDao.GetList(db, position_list.ToArray());
            List<OrderItemResult> order_result_list = new List<OrderItemResult>();
            foreach (var item in order_list)
            {
                order_result_list.Add(new OrderItemResult
                {
                    add_time = item.add_time.ToString("yyyy-MM-dd HH:mm:ss"),
                    department_name = await DepartmentDao.GetDepartmentName(db, item.department_id),
                    position_name = await Dao.User.PositionDao.GetPositionName(db, item.position_id),
                    name = await UserDao.GetUserRealName(db, item.id),
                    order_sn = item.order_sn,
                    remark = item.remark,
                    status = item.status
                });
            }

            Result<List<OrderItemResult>> result = new Result<List<OrderItemResult>> { result = true, msg = "OK", data = order_result_list };
            return result;
        }

        /// <summary>
        /// 验证审核的订单
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<Result> VerifyAuditOrder(In<AuditOrder> inData)
        {
            var data = inData.data;
            Result result = new Result();
            if (data == null)
            {
                result.msg = "参数错误";
                return result;
            }
            if (string.IsNullOrWhiteSpace(data.order_sn?.Trim()))
            {
                result.msg = "订单不存在";
                return result;
            }
            data.order_sn = data.order_sn.Trim();
            DBHelper db = new DBHelper();
            t_procurement order = await ProcurementDao.GetOrder(db, data.order_sn);
            db.Close();
            if (order == null)
            {
                result.msg = "订单不存在";
                return result;
            }
            int pass_step = ProcurementConfig.PassStep(order.department_id, order.position_id);
            ProcurementItem step = ProcurementConfig.CurrentStep(order.department_id, pass_step, order.audit_step);
            if (step == null || step.act != EProcurementAction.Apply || inData.user.position_id != step.id)
            {
                result.msg = "无权审批改订单";
                return result;
            }
            if (string.IsNullOrWhiteSpace(data.flag?.Trim()) || !int.TryParse(data.flag, out int audit_flag) || !Enum.IsDefined(typeof(EAuditFlag), audit_flag))
            {
                result.msg = "审批状态错误";
                return result;
            }
            data.flag = data.flag.Trim();

            if (audit_flag == (int)EAuditFlag.Reject && string.IsNullOrWhiteSpace(data.remark?.Trim()))
            {
                result.msg = "请填写拒绝理由";
                return result;
            }
            data.remark = data.remark?.Trim();
            if (data.remark?.Length > 100)
            {
                result.msg = "拒绝理由最多100个字符";
                return result;
            }

            result.result = true;
            return result;
        }
        public async Task<Result> AuditOrder(In<AuditOrder> inData)
        {
            Result result = await VerifyAuditOrder(inData);
            if (!result.result)
            {
                return result;
            }

            DBHelper db = new DBHelper();
            try
            {
                db.BeginTransaction();
                //最后一步 [入库]

                //普通
                if (int.Parse(inData.data.flag) == (int)EAuditFlag.Agree)
                {
                    result = await AuditAgree(db, inData);
                }
                //拒绝
                else if (int.Parse(inData.data.flag) == (int)EAuditFlag.Reject)
                {
                    result = await AuditReject(db, inData);
                }

                if (!result.result)
                {
                    db.Rollback();
                    return result;
                }

                db.Commit();
                result.msg = "审批成功";
            }
            catch (Exception e)
            {
                db.Rollback();
                result.msg = "审批失败";
            }

            return result;
        }

        /// <summary>
        /// 同意审批
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        private async Task<Result> AuditAgree(DBHelper db, In<AuditOrder> inData)
        {
            Result result = new Result();
            t_procurement order = await ProcurementDao.GetOrder(db, inData.data.order_sn);
            bool update_order_flag = await ProcurementDao.AuditAgreeOrder(db, inData.data.order_sn, order.audit_step + 1);
            if (!update_order_flag)
            {
                result.msg = "审批失败[1]";
                return result;
            }
            bool add_audit_log_flag = await AuditLogDao.AddAuditLog(db, order.order_sn, inData.user.user_id, EAuditFlag.Agree, inData.user.position_id, order.audit_step, inData.data.remark);
            if (!add_audit_log_flag)
            {
                result.msg = "审批失败[2]";
                return result;
            }
            result.result = true;
            return result;
        }

        /// <summary>
        /// 拒绝审批
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        private async Task<Result> AuditReject(DBHelper db, In<AuditOrder> inData)
        {
            Result result = new Result();
            t_procurement order = await ProcurementDao.GetOrder(db, inData.data.order_sn);
            bool update_order_flag = await ProcurementDao.AuditRejectOrder(db, inData.data.order_sn, order.audit_step + 1);
            if (!update_order_flag)
            {
                result.msg = "审批失败[1]";
                return result;
            }
            bool add_audit_log_flag = await AuditLogDao.AddAuditLog(db, order.order_sn, inData.user.user_id, EAuditFlag.Reject, inData.user.position_id, order.audit_step, inData.data.remark);
            if (!add_audit_log_flag)
            {
                result.msg = "审批失败[2]";
                return result;
            }
            result.result = true;
            return result;
        }
    }
}
