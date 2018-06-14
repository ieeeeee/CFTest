using OA.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Models
{
    public class TaskDto
    {
        public string TaskID { get; set; }

        [Display(Name = "任务单标题")]
        [Required(ErrorMessage = Message.Required)]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(40, ErrorMessage = Message.MaxLength)]
        public string TaskTitle { get; set; }

        [Display(Name = "任务单内容")]
        [Required(ErrorMessage = Message.Required)]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        public string TaskBody { get; set; }

        [Display(Name = "任务单类型")]
        [MaxLength(40, ErrorMessage = Message.MaxLength)]
        public string TaskType { get; set; }

        [Display(Name = "更新时间")]
        public DateTime UpdateTime { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string Remark { get; set; }

        public ProcStatus ProcStatus { get; set; }

        public int IsDeleted { get; set; }

        /// <summary>
        /// 是否私密任务
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// 任务优先级
        /// </summary>
        public int TaskPriority { get; set; }

        public string TaskStartTime { get; set; }

        public string TaskEndTime { get; set; }

        /// <summary>
        ///  任务日期
        /// </summary>
        public string TaskDate { get; set; }

        /// <summary>
        ///  任务完成评价
        /// </summary>
        public string TaskAppr { get; set; }

        /// <summary>
        ///  任务状态
        /// </summary>
        public int TaskStatus { get; set; }
    }
}
