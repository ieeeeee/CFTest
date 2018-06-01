using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Models
{
    public class BaseClassDto
    {
        public int BaseClassID { get; set; }

        [Display(Name = "字典分类名称")]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string BaseClassName { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string Remark { get; set; }

        public int IsDeleted { get; set; }

    }
}
