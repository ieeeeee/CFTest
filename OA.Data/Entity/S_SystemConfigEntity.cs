using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Entity
{
    public class S_SystemConfigEntity : BaseEntity
    {
        public string SystemConfigID{get;set;}
        //系统名称
        public string SystemName { get; set; }

        //数据初始化是否完成
        public bool IsDataInited { get; set; }

        //数据初始化时间
        public DateTime DataInitedDate { get; set; }
    }
}
