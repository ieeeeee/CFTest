using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Entity
{
    public class B_UserEntity:BaseEntity
    {
        public B_UserEntity()
        {
            B_Roles = new HashSet<B_RoleEntity>();
            W_PlanLists = new HashSet<W_PlanListEntity>();
        }
        public int UserID { get; set; }

        public string UserNo { get; set; }

        //登录用
        public string UserName { get; set; }
        //显示用
        public string NickName { get; set; }
        //密码
        public string Password { get; set; }
        //明码
        public string PlainCode { get; set; }
        //Email 可用于找回密码
        public string Email { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string Position { get; set; }

        public string Tel { get; set; }

        public string Address { get; set; }

        //对应一个企业
        public Nullable<int> EntID { get; set; }
        public virtual B_EnterpriseEntity B_Enterprise { get; set; }

        //对应一个部门
        public Nullable<int> DepartmentID { get; set; }
        public virtual B_DepartmentEntity B_Department { get; set; }

        public virtual ICollection<B_RoleEntity> B_Roles { get; set; }

        public virtual ICollection<W_PlanListEntity> W_PlanLists { get; set; }
    }
}
