using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Entity
{
    public class W_TaskListEntity:BaseEntity
    {
        public int TaskID { get; set; }

        public string TaskTitle { get; set; }
        public string TaskBody { get; set; }

        public string TaskType { get; set; }

        public int TaskStatus { get; set; }

        /// <summary>
        /// 最新状态的时间
        /// </summary>
        public DateTime LastTime { get; set; }
        public virtual ICollection<W_TaskOperatorEntity> W_TaskOperators { get; set; }
    }
}
