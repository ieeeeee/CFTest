using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Entity
{
    public class B_TableStructEntity:BaseEntity
    {
        //主键 自增长
        public int TStructID { get; set; }

        //自定义表ID
        public int TableID { get; set; }

        //表名 
        public string TableName { get; set; }

        //表含义描述
        public string TableDescription { get; set; }

        //字段
        public string Field { get; set; }

        //字段呈现在页面上的名字 比如表列头
        public string FieldName { get; set; }

        //字段含义描述
        public string FieldDescription { get; set; }

        //字段页面上的选择器绑定的ID
        public string ShowID { get; set; }

        //字段在页面上用什么标签展示 （编写的Vue组件）
        public string VueTemplate { get; set; }

        //输入提示
        public string Placeholder { get; set; }

        //帮助提示
        public string HelpBlock { get; set; }

        //其他语言字段 如表英文列头
        public string FieldLangName { get; set; }

        //字段的图标符号
        public string Icon { get; set; }
        //字段排序
        public int OrderID { get; set; }

        //字段允许查询出来 1是允许查询
        public int IsQuery { get; set; }

        //字段在界面上是否隐藏 true是隐藏
        public bool IsHide { get; set; }
    }
}
