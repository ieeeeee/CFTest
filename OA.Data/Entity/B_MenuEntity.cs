using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Entity
{
   public  class B_MenuEntity:BaseEntity
    {
        public B_MenuEntity()
        {
            B_Roles = new HashSet<B_RoleEntity>();
        }
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        /// <summary>
        /// 分支菜单 功能菜单
        /// </summary>
        public int MenuType { get; set; }
        /// <summary>
        /// 菜单页面地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 上级菜单ID
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 排序ID
        /// </summary>
        public int OrderID { get; set; }

        //图标
        public string Icon { get; set; }
        public virtual ICollection<B_RoleEntity> B_Roles { get; set; }
    }
}
