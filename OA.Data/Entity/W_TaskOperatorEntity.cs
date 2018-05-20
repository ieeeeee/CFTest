using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Entity
{
    public class W_TaskOperatorEntity
    {
        public int TaskOperatID { get; set; }

        /// <summary>
        /// 创建 编辑  解决 激活 关闭
        /// </summary>
        public int TaskOperatType { get; set; }


        /// <summary>
        /// 任务每步处理的人
        /// </summary>
        public virtual B_UserEntity W_Operator { get; set; }
        public virtual W_TaskListEntity W_Task { get; set; }
    }
}
