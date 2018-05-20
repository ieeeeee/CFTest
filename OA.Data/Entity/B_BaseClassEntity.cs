using System.Collections.Generic;

namespace OA.Data.Entity
{
    public class B_BaseClassEntity:BaseEntity
    {
        public B_BaseClassEntity()
        {
            B_BaseInfos = new HashSet<B_BaseInfoEntity>();
        }
        public int BaseClassID { get; set; }

        public string BaseClassName { get; set; }

        public virtual ICollection<B_BaseInfoEntity> B_BaseInfos { get; set; }
    }
}