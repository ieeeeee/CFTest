using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Models
{
    public class DeptDto
    {
        public int DepartmentID { get; set; }

        [Display(Name ="部门编号")]
        [MinLength(2,ErrorMessage =Message.MinLength)]
        [MaxLength(6,ErrorMessage =Message.MaxLength)]
        public string DeptNo { get; set; }

        [Display(Name = "部门名称")]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(40, ErrorMessage = Message.MaxLength)]
        public string DeptName { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string Remark { get; set; }

        public int IsDeleted { get; set; }
    }
}
