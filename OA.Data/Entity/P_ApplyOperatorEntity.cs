using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Entity
{
    public class P_ApplyOperatorEntity:BaseEntity
    {
        public int ApplyOperatID { get; set; }

        /// <summary>
        /// 申请 审核 执行 删除
        /// </summary>
        public string ApplyOperatType { get; set; }

        public virtual B_UserEntity P_Operator { get; set; }

        //可能是费用申请
        public Nullable<int> FeeApplyID { get; set; }
        public virtual P_FeeApplyEntity P_FeeApply { get; set; }



        //可能是请假申请
        public Nullable<int> LeaveApplyID { get; set; }
        public virtual P_LeaveApplyEntity P_LeaveApply { get; set; }
    }
}
