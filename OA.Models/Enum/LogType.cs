using System.ComponentModel;

namespace OA.Models.Enum
{
    public enum LogType
    {
        [Description("登录日志")]
        LoginLog=1,
        [Description("操作日志")]
        OperateLog =2,

    }
}