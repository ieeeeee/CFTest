using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Models.Enum
{
    public enum LoginResult
    {
        [Description("账号不存在")] //using System.ComponentModel;
        AccountNotExists=1,
        [Description("登录密码错误")] 
        WrongPassword = 2,
        [Description("登陆成功")] 
        Success = 3,
        [Description("账号被禁用")]
        IsDisabled = 4,
    }
}
