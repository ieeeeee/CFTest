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
using AutoMapper;
using OA.Data.Entity;

namespace OA.Services.AppServices
{
    public class TableStructService:ITableStructService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IMapper _mapper;
        public TableStructService(IDbContextScopeFactory dbContextScopeFactory,IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
        }

        public async Task<List<TableStructDto>> GetAddTableStructAsync(int tableID)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var query = db.B_TableStructEntities.Where(item => (item.TableID == tableID || item.TableID == 99) && item.IsDeleted != 1&&item.IsQuery==1);
                return  await query.OrderBy(x => x.OrderID).Select(item => new TableStructDto
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
        //获取TableStruct 列表
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
                        OrderID=item.OrderID,
                        IsQuery=item.IsQuery,
                        IsHide = item.IsHide,
                        Icon = item.Icon
                    }).PagingAsync(filters.page, filters.rows);
            }
        }

        //根据主键ID获取
        public async Task<TableStructDto> FindAsync(int id)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity =await db.B_TableStructEntities.FindAsync(id);
                var dto = _mapper.Map<B_TableStructEntity,TableStructDto>(entity);
                if(dto==null)
                {
                    return new TableStructDto();
                }
                return dto;
            }
        }

        public async Task<string> AddAsync(TableStructDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = _mapper.Map<TableStructDto, B_TableStructEntity>(dto);
                entity.Create();
                db.B_TableStructEntities.Add(entity);
                return await scope.SaveChangesAsync() > 0 ? entity.TStructID.ToString() : string.Empty;
;               
            }
        }

        public async Task<bool> UpdateAsync(TableStructDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = await db.B_TableStructEntities.FindAsync(dto.TStructID);
                entity.TableID = dto.TableID;
                entity.TableName = dto.TableName;
                entity.Field = dto.Field;
                entity.FieldName = dto.FieldName;
                entity.VueTemplate = dto.VueTemplate;
                entity.ShowID = dto.ShowID;
                entity.OrderID = dto.OrderID;
                entity.IsDeleted = dto.IsDeleted;
                entity.IsQuery = dto.IsQuery;
                entity.IsHide = dto.IsHide;
                entity.Icon = dto.Icon;
                entity.Placeholder = dto.Placeholder;
                entity.HelpBlock = dto.HelpBlock;
                entity.Remark = dto.Remark;
                entity.CreateDateTime = DateTime.Now;
                await scope.SaveChangesAsync();
                return true;
            }
            
        }
    }
}
