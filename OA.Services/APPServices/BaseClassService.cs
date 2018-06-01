using AutoMapper;
using Mehdime.Entity;
using OA.Basis;
using OA.Basis.Extentions;
using OA.Data;
using OA.Data.Entity;
using OA.Interfaces;
using OA.Models;
using OA.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Services.AppServices
{
    public class BaseClassService:IBaseClassService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IMapper _mapper;
        public BaseClassService(IDbContextScopeFactory dbContextScopeFactory, IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
        }
        //增加
        public async Task<string> AddASync(BaseClassDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = _mapper.Map<BaseClassDto, B_BaseClassEntity>(dto);
                db.B_BaseClasses.Add(entity);
                return await scope.SaveChangesAsync() > 0 ? entity.BaseClassID.ToString() : string.Empty;
            }
        }

        //删
        public async Task<bool> DeleteAsync(IEnumerable<int> ids)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entities = db.B_BaseClasses.Where(x => ids.Contains(x.BaseClassID) && x.IsDeleted != 1);
                foreach (var entity in entities)
                {
                    entity.IsDeleted = 1;
                }
                await scope.SaveChangesAsync();
                return true;
            }
        }

        //查
        public async Task<BaseClassDto> FindAsync(int ID)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = await db.B_BaseClasses.FindAsync(ID);
                var dto = _mapper.Map<B_BaseClassEntity, BaseClassDto>(entity);
                if (dto == null)
                {
                    return new BaseClassDto();
                }
                return dto;
            }
        }

        //查列表
        public async Task<PagedResult<BaseClassDto>> SearchAsync(BaseClassFilter filter)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var query = db.B_BaseClasses.Where(x => x.IsDeleted != 1)
                    .WhereIf(filter.keywords.IsNotBlank(), x => x.BaseClassName.Contains(filter.keywords));
                filter.sidx = "BaseClassID";
                filter.sord = "ASC";
                return await query.OrderByCustom(filter.sidx, filter.sord)
                    .Select(item => new BaseClassDto
                    {
                        BaseClassID = item.BaseClassID,
                        BaseClassName = item.BaseClassName,
                        Remark = item.Remark,
                        IsDeleted = item.IsDeleted
                    }).PagingAsync(filter.page, filter.rows);
            }
        }

        //改
        public async Task<bool> UpdateAsync(BaseClassDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = await db.B_BaseClasses.LoadAsync(dto.BaseClassID);
                entity.Create();
                entity.BaseClassID = dto.BaseClassID;
                entity.BaseClassName = dto.BaseClassName;
                await scope.SaveChangesAsync();
                return true;
            }
        }
    }
}
