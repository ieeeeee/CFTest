using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Models
{
   public  class TableStructDto
    {
        //主键 自增长
        public int TStructID { get; set; }

        //自定义表ID
        public int TableID { get; set; }

        [Display(Name = "表名称")]
        [Required(ErrorMessage = Message.Required)]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(40, ErrorMessage = Message.MaxLength)]
        public string TableName { get; set; }


        //表含义描述
        [Display(Name = "表含义描述")]
        [MaxLength(40, ErrorMessage = Message.MaxLength)]
        public string TableDescription { get; set; }

        //字段
        [Display(Name = "字段")]
        [Required(ErrorMessage = Message.Required)]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(40, ErrorMessage = Message.MaxLength)]
        public string Field { get; set; }

        //字段呈现在页面上的名字 比如表列头
        [Display(Name = "字段呈现在页面上的名字 比如表列头")]
        [Required(ErrorMessage = Message.Required)]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(40, ErrorMessage = Message.MaxLength)]
        public string FieldName { get; set; }

        //字段含义描述
        [Display(Name = "字段含义描述")]
        [MaxLength(40, ErrorMessage = Message.MaxLength)]
        public string FieldDescription { get; set; }

        //字段页面上的选择器绑定的ID
        [Display(Name = "字段页面上的选择器绑定的ID")]
        [MaxLength(40, ErrorMessage = Message.MaxLength)]
        public string ShowID { get; set; }

        //字段在页面上用什么标签展示 （编写的Vue组件）
        [Display(Name = "字段在页面上用什么标签展示")]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string VueTemplate { get; set; }

        //输入提示
        [Display(Name = "输入提示")]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string Placeholder { get; set; }

        //帮助提示
        [Display(Name = "帮助提示")]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string HelpBlock { get; set; }


        [Display(Name = "备注")]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string Remark { get; set; }



        public int IsDeleted { get; set; }

        //字段的图标符号
        [Display(Name = "字段的图标符号")]
        [MaxLength(40, ErrorMessage = Message.MaxLength)]
        public string Icon { get; set; }

        //字段排序
        public int OrderID { get; set; }

        //字段允许查询出来 1是允许查询
        public int IsQuery { get; set; }

        //字段在界面上是否隐藏 true是隐藏
        public bool IsHide { get; set; }

    }
}
