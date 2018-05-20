using System.Collections.Generic;
using System.Linq;

namespace OA.Basis.Extentions
{
    //扩展集合功能
    public static class EnumerableExtensions
    {

        public static bool AnyOne<T>(this IEnumerable<T> source)
        {
            return source != null ? source.Any() : false;
        }
    }
}