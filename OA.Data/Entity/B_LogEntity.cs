using OA.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Entity
{
    public class B_LogEntity:BaseEntity
    {
        public int LogID { get; set; }

        public string UserID { get; set; }
        public string UserName { get; set; }

        public string MenuID { get; set; }
        public string MenuName { get; set; }
        public string IP { get; set; }

        public string Mac { get; set; }

        public int LogType { get; set; }
        //public virtual B_MenuEntity B_Menu { get; set; }
        //public virtual B_UserEntity B_User { get; set; }
    }
}
