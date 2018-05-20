﻿using System.Collections.Generic;

namespace OA.Models
{
    //用户DTO
    public class UserDto
    {
        public string UserID { get; set; }



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
        public int EntID { get; set; }

        public string EnterpriseName { get; set; }

        //对应一个部门
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        public virtual IList<UserRoleDto> UserRoles { get; set; }

        public virtual IList<UserPlanDto> UserPlans { get; set; }
    }
}