using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMonitor.Models
{
    /// <summary>
    /// 环境信息
    /// </summary>
    internal class EnviromentModel
    {
        /// <summary>
        /// 环境项名称
        /// </summary>
        public string EnItemName { get; set; }

        /// <summary>
        /// 环境项的值
        /// </summary>
        public int EnItemValue { get; set; }
    }
}
