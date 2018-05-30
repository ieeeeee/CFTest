using Mehdime.Entity;
using OA.Data;
using OA.Interfaces;
using OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using OA.Basis;
using OA.Models.Filters;
using OA.Basis.Extentions;

namespace OA.Services.AppServices
{
    public class TableStructService:ITableStructService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        public TableStructService(IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public async Task<List<TableStructDto>> GetAddTableStructAsync(int tableID)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var query = db.B_TableStructEntities.Where(item => (item.TableID == tableID || item.TableID == 99) && item.IsDeleted != 1&&item.IsQuery==1);
                return  await query.OrderBy(x => x.TStructID).Select(item => new TableStructDto
                {
                    TStructID = item.TStructID,
                    Field = item.Field,
                    FieldName = item.FieldName,
                    Placeholder = item.Placeholder,
                    HelpBlock = item.HelpBlock,
                    IsDeleted = item.IsDeleted,
                    VueTemplate=item.VueTemplate,
                    IsHide=item.IsHide,
                    Icon=item.Icon
                }).ToListAsync();//using System.Data.Entity;


            }
        }

        public async Task<PagedResult<TableStructDto>> SearchAsync(TableStructFilter filters)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var query = db.B_TableStructEntities.Where(item => item.IsDeleted != 1)
                    .WhereIf(filters.keywords.IsNotBlank(), x => (x.TableName.Contains(filters.keywords) || x.Field.Contains(filters.keywords)));
                filters.sidx = "TableID,OrderID";
                return await query.OrderByCustom(filters.sidx, filters.sord)
                    .Select(item => new TableStructDto
                    {
                        TStructID = item.TStructID,
                        TableID=item.TableID,
                        TableName=item.TableName,
                        Field = item.Field,
                        FieldName = item.FieldName,
                        Placeholder = item.Placeholder,
                        HelpBlock = item.HelpBlock,
                        IsDeleted = item.IsDeleted,
                        VueTemplate = item.VueTemplate,
                        IsQuery=item.IsQuery,
                        IsHide = item.IsHide,
                        Icon = item.Icon
                    }).PagingAsync(filters.page, filters.rows);
            }
        }
    }
}
