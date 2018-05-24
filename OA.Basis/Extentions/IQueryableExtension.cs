using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace OA.Basis.Extentions
{
    public static class IQueryableExtension
    {
        //自定义排序
        public static IQueryable<T> OrderByCustom<T>(this IQueryable<T> query,string fieldName,string sord)
        {
            var fields = fieldName.WithDefaultValueIfEmpty("CreateDateTime");
            sord = sord.IsBlank() ? "DESC" : "ASC";
            var sorts = string.Format("{0} {1}", fields, sord);
            return query.OrderBy(sorts); //using System.Linq.Dynamic;
        }

        // WhereIf[在condition为true的情况下应用Where表达式]
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source,bool condition,Expression<Func<T,bool>> expression)
        {
            return condition ? source.Where(expression) : source;
        }
    }
}
