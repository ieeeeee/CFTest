using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 文档中心--可下载
/// </summary>
namespace OA.Data.Entity
{
    public class W_DocumentEntity:BaseEntity
    {
        public int DocID { get; set; }
        public string DocName { get; set; }
        public string DocUrlPath { get; set; }

        public string DocType { get; set; }
    }
}
