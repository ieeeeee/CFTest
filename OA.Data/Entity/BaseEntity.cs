using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 每一个实体都有的键
/// </summary>
namespace OA.Data.Entity
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            IsDeleted = 0;
            CreateDataTime = DateTime.Now;
        }

        //public string  Id { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDataTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 是否删除  1 表示删除 2 表示修改 3 表示禁用 （其他表示创建或在用）
        /// </summary>
        public int IsDeleted { get; set; }

    }
}
