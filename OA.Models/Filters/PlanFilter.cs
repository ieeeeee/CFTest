using OA.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Models.Filters
{
    public class PlanFilter:BaseFilter
    {
        /// <summary>
        /// 当前计划进程状态
        /// </summary>
        public int CurrStatus { get; set; }

        /// <summary>
        /// 计划的类型
        /// </summary>
        public string PlanType { get; set; }

        public string Operator { get; set; }
    }
}
