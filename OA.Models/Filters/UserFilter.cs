using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Models.Filters
{
   public class UserFilter:BaseFilter
    {
        /// <summary>
        /// 企业ID
        /// </summary>
        public int EntID { get; set; }
    }
}
