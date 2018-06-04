using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Models
{
    public  class RoleDto
    {
        public int RoleID { get; set; }

        [Display(Name = "角色名称")]
        [Required(ErrorMessage = Message.Required)]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(40, ErrorMessage = Message.MaxLength)]
        public string RoleName { get; set; }

        [Display(Name = "角色描述")]
        [MaxLength(40, ErrorMessage = Message.MaxLength)]
        public string Description { get; set; }


        [Display(Name = "备注")]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string Remark { get; set; }

        public int IsDeleted { get; set; }
    }
}
