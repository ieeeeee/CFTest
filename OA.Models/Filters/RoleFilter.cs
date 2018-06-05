using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Models.Filters
{
    /// <summary>
    /// 角色搜索过滤器
    /// </summary>
   public  class RoleFilter:BaseFilter
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 是否排除当前UserID拥有的角色
        /// </summary>
        public bool ExcludeMyRoles { get; set; }
    }
}
