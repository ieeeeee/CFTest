using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Models
{
    public class EntDto
    {
        public int EntID { get; set; }

        [Display(Name = "分组编码")]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(4, ErrorMessage = Message.MaxLength)]
        public string GroupID { get; set; }

        [Display(Name = "企业名称")]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string EntName { get; set; }

        [Display(Name = "联系电话")]
        [MaxLength(20, ErrorMessage = Message.MaxLength)]
        public string Tel { get; set; }

        [Display(Name = "企业地址")]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string Address { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string Remark { get; set; }

        public int IsDeleted { get; set; }
    }
}
