using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Basis.Extentions
{
    public static class DbContextExtension
    {

        //根据主键异步加载数据，如果没有则抛出异常
        public static async Task<T> LoadAsync<T>(this DbSet<T> dbset,string id) where T : class
        {
            var entity = await dbset.FindAsync(id);
            if(entity==null)
            {
                throw new Exception("记录未找到：id=" + id);
            }
            return entity;
        }
    }
}
