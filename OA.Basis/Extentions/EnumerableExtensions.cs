using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OA.Basis.Extentions
{
    //扩展集合功能
    public static class EnumerableExtensions
    {

        // 判断序列是否包含任何元素，如果序列为空，则返回False
        public static bool AnyOne<T>(this IEnumerable<T> source)
        {
            return source != null ? source.Any() : false;
        }

        public static async Task<PagedResult<T>> PagingAsync<T>(this IQueryable<T> source,int pageIndex,int pageSize )
        {
            if (pageIndex <= 0)
                throw new ArgumentException("Index of current page can not less than 0 !", "pageIndex");
            if (pageSize <= 1)
                throw new ArgumentException("Size of page can not less than 1 !", "pageSize");

            var pagedQuery = source
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
            return new PagedResult<T>
            {
                rows=await pagedQuery.ToListAsync(),//引用EntityFramework
                records=await source.CountAsync(),

                page=pageIndex,
                pagesize=pageSize
            };
        }

    }
}