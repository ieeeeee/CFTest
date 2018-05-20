/// <summary>
/// 字典
/// </summary>
namespace OA.Data.Entity
{
    public class B_BaseInfoEntity:BaseEntity
    {
        public int BaseInfoID { get; set; }

        /// <summary>
        /// 字典字段名
        /// </summary>
        public string BaseName { get; set; }
       /// <summary>
       /// 字典字段值
       /// </summary>
        public string BaseValue { get; set; }

        //和B_BaseClass的关系
        public int BaseClassID { get; set; }
        public virtual B_BaseClassEntity B_BaseClass { get; set; }
    }
}
