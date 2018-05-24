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
                var query = db.B_TableStructEntities.Where(item => (item.TableID == tableID || item.TableID == 99) && item.IsDeleted != 1);
                return  await query.OrderBy(x => x.TStructID).Select(item => new TableStructDto
                {
                    TStructID = item.TStructID,
                    Field = item.Field,
                    FieldName = item.FieldName,
                    Placeholder = item.Placeholder,
                    HelpBlock = item.HelpBlock,
                    IsDeleted = item.IsDeleted
                }).ToListAsync();//using System.Data.Entity;


            }
        }
    }
}
