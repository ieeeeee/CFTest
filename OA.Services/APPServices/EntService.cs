using Mehdime.Entity;
using OA.Basis;
using OA.Basis.Extentions;
using OA.Data;
using OA.Models;
using OA.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Services.AppServices
{
    public class EntService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        public EntService(IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        //查询企业信息
        public async Task<PagedResult<EntDto>> Search(EntFilter entFilter)
        {
            if (entFilter == null)
                return new PagedResult<EntDto>(1, 0);
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var query = db.B_Enterprises.Where(item => item.IsDeleted != 1)
                    .WhereIf(entFilter.keywords.IsNotBlank(), x => x.EntName.Contains(entFilter.keywords) || x.Tel.Contains(entFilter.keywords));

                return await query.OrderByCustom(entFilter.sidx, entFilter.sord)
                    .Select(x => new EntDto
                    {
                        EntID = x.EntID,
                        GroupID=x.GroupID,
                        EntName=x.EntName,
                        Tel=x.Tel,
                        Address=x.Address,
                        IsDeleted=x.IsDeleted

                    }).PagingAsync(entFilter.page,entFilter.rows);              
            }

        }
    }
}
