using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Models
{
   public class UserAddDto
    {
        public int UserID { get; set; }

        public string UserNo { get; set; }

        //登录用
        public string UserName { get; set; }
        //显示用
        public string NickName { get; set; }
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

        public string EntName { get; set; }

        //对应一个部门
        public Nullable<int> DeptID { get; set; }
        public string DeptName { get; set; }


        [Display(Name = "备注")]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string Remark { get; set; }

        public int IsDeleted { get; set; }

        //public int LockEntID { get; set; }
        //public int LockMenuID { get; set; }
        //public int LockSubMenuID { get; set; }
    }
}
