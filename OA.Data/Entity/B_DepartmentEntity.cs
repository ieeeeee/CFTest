using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Entity
{
    public class B_DepartmentEntity:BaseEntity
    {
        public B_DepartmentEntity()
        {
            B_Users = new HashSet<B_UserEntity>();
        }
        public int DepartmentID { get; set; }

        public string DeptNo { get; set; }

        public string DeptName { get; set; }

        public int EntID { get; set; }
        public virtual B_EnterpriseEntity B_Enterprise { get; set; }
        public virtual ICollection<B_UserEntity> B_Users { get; set; }
        
    }
}
