using OA.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//登录返回的数据
namespace OA.Models
{
   public class UserLoginDto
    {
        public bool LoginSuccess { get; set; }
        public string Message { get; set; }

        public LoginResult Result { get; set; } //using OA.Models.Enum;

        public UserDto User { get; set; }
    }
}
