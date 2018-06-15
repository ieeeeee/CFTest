using OA.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Models
{
    public class PlanTabDto
    {
        /// <summary>
        /// 计划类型
        /// </summary>
        //public string[] PlanType { get; set; }

        /// <summary>
        /// 计划日期
        /// </summary>
        public string PlanDate { get; set; }

        /// <summary>
        /// 计划表格内容
        /// </summary>
        public Task<PagedResult<PlanDto>> PlanData { get; set; }
    }
}
