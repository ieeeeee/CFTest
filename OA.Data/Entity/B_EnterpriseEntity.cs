﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 用户所在企业的信息
/// </summary>
namespace OA.Data.Entity
{
   public class B_EnterpriseEntity:BaseEntity
    {
        public B_EnterpriseEntity()
        {
            B_Users = new HashSet<B_UserEntity>();
        }
        public int EntID { get; set; }
        public string EntName { get; set; }
        public string Tel { get; set; }

        public string Address { get; set; }

        public virtual ICollection<B_UserEntity> B_Users { get; set; }
    }
}