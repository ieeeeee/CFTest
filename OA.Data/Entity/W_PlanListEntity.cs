using OA.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 工作台--计划单
/// </summary>
namespace OA.Data.Entity
{
    public class W_PlanListEntity:BaseEntity
    {
        public string PlanID { get; set; }
        public string PlanTitle { get; set; }
        public string PlanBody { get; set; }

        public string PlanType { get; set; }
        public DateTime UpdateTime { get; set; }

        public byte ProcStatus { get; set; }

        /// <summary>
        /// 是否私密计划
        /// </summary>
        public int IsPrivate { get; set; }

        /// <summary>
        /// 计划优先级
        /// </summary>
        public int PlanPriority { get; set; }

        public string PlanStartTime { get; set; }

        public string PlanEndTime { get; set; }

        /// <summary>
        ///  计划日期
        /// </summary>
        public string PlanDate { get; set; }

        public virtual B_UserEntity B_User { get; set; }
    }
}
