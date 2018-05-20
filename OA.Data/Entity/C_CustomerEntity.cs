using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Entity
{
    public class C_CustomerEntity:BaseEntity
    {
        public C_CustomerEntity()
        {
            C_Contacts = new HashSet<C_ContactsEntity>();
        }
        public int CustomerID { get; set; }

        public string CustomerNO { get; set; }

        public string CustomerName { get; set; }

        public string CustomerType { get; set; }

        public string Address { get; set; }
        public string Tel { get; set; }

        public virtual B_EnterpriseEntity B_Enterprise { get; set; }

        public virtual ICollection<C_ContactsEntity> C_Contacts { get; set; }
    }
}
