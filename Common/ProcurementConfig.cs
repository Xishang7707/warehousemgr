using Model.Progress;
using Model.Progress.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common
{
    /// <summary>
    /// 采购流程配置
    /// </summary>
    public static class ProcurementConfig
    {
        private static Dictionary<string, List<ProcurementItem>> _progress_conf = new Dictionary<string, List<ProcurementItem>>();

        static ProcurementConfig()
        {
            string path = Directory.GetCurrentDirectory() + "/Configs/progress.json";
            using StreamReader sr = new StreamReader(path);
            string text = sr.ReadToEnd();

            JObject json = (JObject)JObject.Parse(text)["procurement"];
            foreach (var item in json.Properties())
            {
                JArray jarr = JArray.FromObject(item.Values());
                _progress_conf.Add(item.Name, new List<ProcurementItem>());
                foreach (var it in jarr)
                {
                    ProcurementItem pi = new ProcurementItem
                    {
                        id = (int)it["id"]
                    };
                    switch (it["act"].ToString())
                    {
                        case "apply":
                            pi.act = EProcurementAction.Apply;
                            break;
                        case "procurement":
                            pi.act = EProcurementAction.Procurement;
                            break;
                    }
                    _progress_conf[item.Name].Add(pi);
                }
            }
        }

        /// <summary>
        /// 获取当前审批的步骤
        /// </summary>
        /// <param name="depart_id">订单部门id</param>
        /// <param name="pass_step">免审核步骤数</param>
        /// <param name="audit_step">已审批的步骤数</param>
        /// <returns></returns>
        public static ProcurementItem CurrentStep(int depart_id, int pass_step, int audit_step)
        {
            var list = _progress_conf["depart-" + depart_id];
            if (list.Count - 1 <= audit_step)
            {
                return null;
            }

            return list[pass_step + audit_step];
        }

        /// <summary>
        /// 免审核步骤数
        /// </summary>
        /// <param name="depart_id">部门id</param>
        /// <param name="posit_id">职位id</param>
        /// <returns></returns>
        public static int PassStep(int depart_id, int posit_id)
        {
            var list = _progress_conf["depart-" + depart_id];

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].id == posit_id)
                {
                    return i + 1;
                }
            }
            return 0;
        }
    }
}
