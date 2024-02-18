using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMonitor.Models
{
    /// <summary>
    /// 机台数据模型
    /// </summary>
    internal class MachineModel
    {
        /// <summary>
        /// 机台名称
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 计划任务数量
        /// </summary>
        public int PlanCount { get; set; }

        /// <summary>
        /// 已完成任务数量
        /// </summary>
        public int FinishedCount { get; set; }

        /// <summary>
        /// 工单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 完成百分比(只读)
        /// </summary>
        public double Percent
        {
            get
            {
                return FinishedCount * 100.0 / PlanCount;
            }
        }
    }
}
