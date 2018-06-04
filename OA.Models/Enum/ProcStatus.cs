using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Models.Enum
{
    public enum ProcStatus:byte
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        Pending = 1,

        /// <summary>
        /// 待修改
        /// </summary>
        [Description("待修改")]
        ToBeModified= 2,

        /// <summary>
        /// 已发布
        /// </summary>
        [Description("已发布")]
        published = 3
    }
}
