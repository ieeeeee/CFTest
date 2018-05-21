using OA.Basis.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Basis
{
   public class PagedResult<T>
    {
        public PagedResult()
        {
            rows = new List<T>();
        }

        public PagedResult(int pageIndex,int pageSize)
        {
            page = pageIndex;
            pagesize = pageSize;
        }

        //总记录数
        public int records { set; get; }

        //当前页的所有项
        public IList<T> rows { get; set; }

        //当前页
        public int page { get; set; }

        //页大小
        public int pagesize { get; set; }

        //页总数
        public int total { get { return records.CeilingDivide(pagesize); } }
    }
}
