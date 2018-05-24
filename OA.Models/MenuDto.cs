using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Models
{
    public class MenuDto
    {

        public int MenuID { get; set; }

        [Display(Name = "菜单名称")]
        [Required(ErrorMessage = Message.Required)]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(40, ErrorMessage = Message.MaxLength)]
        public string MenuName { get; set; }

        // 分支菜单 功能菜单
        public int MenuType { get; set; }

        [Display(Name = "菜单网址")]
        [Required(ErrorMessage = Message.Required)]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string Url { get; set; }

        /// <summary>
        /// 上级菜单ID
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 排序ID
        /// </summary>
        public int OrderID { get; set; }

        [Display(Name = "Icon图标")]
        public string Icon { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string Remark { get; set; }

        public int IsDeleted { get; set; }
    }
}
