using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Entity
{
    public class W_CompanyNewsEntity:BaseEntity
    {
        public int NewsID { get; set; }

        public string NewsTitle { get; set; }

        public string NewsBody { get; set; }

        public string NewsType { get; set; }

        //对应一个企业
        public int EntID { get; set; }
        public virtual B_EnterpriseEntity B_Enterprise { get; set; }

        //对应一个作者
        public int UserID { get; set; }
        public virtual B_UserEntity Author { get; set; }

    }
}
