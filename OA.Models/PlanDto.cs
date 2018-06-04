using OA.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Models
{
   public class PlanDto
    {
        public string PlanID { get; set; }

        [Display(Name = "计划单标题")]
        [Required(ErrorMessage = Message.Required)]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(40, ErrorMessage = Message.MaxLength)]
        public string PlanTitle { get; set; }

        [Display(Name = "计划单内容")]
        [Required(ErrorMessage = Message.Required)]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(40, ErrorMessage = Message.MaxLength)]
        public string PlanBody { get; set; }

        [Display(Name = "计划单类型")]
        [MaxLength(40, ErrorMessage = Message.MaxLength)]
        public string PlanType { get; set; }

        [Display(Name = "更新时间")]
        public DateTime UpdateTime { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string Remark { get; set; }

        public ProcStatus ProcStatus { get; set; }

        public string ProcStatusName { get; set; }
        public int IsDeleted { get; set; }
    }
}
