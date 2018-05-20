using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Entity
{
    public class B_RoleEntity:BaseEntity
    {
        public B_RoleEntity()
        {
            B_Users = new HashSet<B_UserEntity>();
            B_Menus = new HashSet<B_MenuEntity>();
        }
        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public virtual ICollection<B_UserEntity> B_Users { get; set; }

        public virtual ICollection<B_MenuEntity> B_Menus { get; set; }
    }
}
