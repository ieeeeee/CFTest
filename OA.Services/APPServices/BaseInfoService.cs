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
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Services.AppServices
{
   public class BaseInfoService:IBaseInfoService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IMapper _mapper;
        public BaseInfoService(IDbContextScopeFactory dbContextScopeFactory, IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
        }
        //增加
        public async Task<string> AddASync(BaseInfoDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = _mapper.Map<BaseInfoDto, B_BaseInfoEntity>(dto);
                db.B_BaseInfos.Add(entity);
                return await scope.SaveChangesAsync() > 0 ? entity.BaseInfoID.ToString() : string.Empty;
            }
        }

        //删
        public async Task<bool> DeleteAsync(IEnumerable<int> ids)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entities = db.B_BaseInfos.Where(x => ids.Contains(x.BaseInfoID) && x.IsDeleted != 1);
                foreach(var entity in entities)
                {
                    entity.IsDeleted = 1;
                }
                await scope.SaveChangesAsync();
                return true;
            }
        }

        //查
        public async Task<BaseInfoDto> FindAsync(int ID)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = await db.B_BaseInfos.FindAsync(ID);
                var dto = _mapper.Map<B_BaseInfoEntity, BaseInfoDto>(entity);
                if(dto==null)
                {
                    return new BaseInfoDto();
                }
                return dto;

            }
        }

        //查列表
        public async Task<PagedResult<BaseInfoDto>> SearchAsync(BaseInfoFilter filter)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var query = db.B_BaseInfos.Where(x => x.IsDeleted != 1)
                    .WhereIf(filter.keywords.IsNotBlank(), x => x.BaseName.Contains(filter.keywords));
                filter.sidx = "BaseClassID,BaseInfoID";
                filter.sord = "ASC";
                return await query.OrderByCustom(filter.sidx, filter.sord)
                    .Select(item => new BaseInfoDto
                    {
                        BaseClassID = item.BaseClassID,
                        BaseClassName=item.B_BaseClass.BaseClassName,
                        BaseInfoID=item.BaseInfoID,
                        BaseName=item.BaseName,
                        BaseValue=item.BaseValue,
                        Remark=item.Remark,
                        IsDeleted=item.IsDeleted

                    }).PagingAsync(filter.page, filter.rows);
            }
        }

        //改
        public async Task<bool> UpdateAsync(BaseInfoDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity =await db.B_BaseInfos.LoadAsync(dto.BaseInfoID);
                entity.Create();
                entity.BaseClassID = dto.BaseClassID;
                entity.BaseName = dto.BaseName;
                entity.BaseValue = dto.BaseValue;
                await scope.SaveChangesAsync();
                return true;
            }
        }

        //
       public async  Task<List<BaseInfoDto>> GetClassInfo(int baseClassID)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var query = db.B_BaseInfos.Where(x => x.IsDeleted != 1 && x.BaseClassID == baseClassID);
                return await query.OrderBy(x=>x.BaseInfoID).Select(item => new BaseInfoDto
                    {
                        BaseClassID = item.BaseClassID,
                        BaseInfoID = item.BaseInfoID,
                        BaseName = item.BaseName,
                        BaseValue = item.BaseValue,
                        Remark = item.Remark,
                        IsDeleted = item.IsDeleted
                    }).ToListAsync();
            }
        }
    }
}
