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
        public virtual B_UserEntity B_User { get; set; }
    }
}
