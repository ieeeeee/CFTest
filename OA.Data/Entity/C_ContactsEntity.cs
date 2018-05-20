using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Entity
{
    public class C_ContactsEntity:BaseEntity
    {
        public int ContactsID { get; set; }
        /// <summary>
        /// 联系人名称
        /// </summary>
        public string ContactsName { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }

        public string Tel3 { get; set; }

        //有一个客户
        public Nullable<int> CustomerID { get; set; }
        public virtual C_CustomerEntity C_Customer { get; set; }
    }
}
