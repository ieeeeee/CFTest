using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Entity
{
   public class P_LeaveApplyEntity:BaseEntity
    {
        public P_LeaveApplyEntity()
        {
            P_ApplyOperators = new HashSet<P_ApplyOperatorEntity>();
        }
        public int LeaveApplyID { get; set; }
        public string LeaveApplyTitle { get; set; }

        /// <summary>
        /// 病假 事假 年假 调休 请假
        /// </summary>
        public string LeaveApplyType { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public virtual ICollection<P_ApplyOperatorEntity> P_ApplyOperators { get; set; }
    }
}
