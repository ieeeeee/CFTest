using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Entity
{
    public class P_FeeApplyEntity:BaseEntity
    {
        public P_FeeApplyEntity()
        {
            P_ApplyOperators = new HashSet<P_ApplyOperatorEntity>();
        }
        public int FeeApplyID { get; set; }

        public string FeeApplyTitle { get; set; }

        public string FeeApplyType { get; set; }

        /// <summary>
        /// 申请金额
        /// </summary>
        public Decimal FeeApplyAmount { get; set; }

        /// <summary>
        /// 批准金额
        /// </summary>
        public Decimal FeeApproveAmount { get; set; }

        public virtual ICollection<P_ApplyOperatorEntity> P_ApplyOperators { get; set; }
    }
}
