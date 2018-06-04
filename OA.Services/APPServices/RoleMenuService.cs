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
    public class RoleMenuService:IRoleMenuService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IMapper _mapper;
        public RoleMenuService(IDbContextScopeFactory dbContextScopeFactory, IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
        }
        //增加
        public async Task<string> AddASync(RoleDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = _mapper.Map<RoleDto, B_RoleEntity>(dto);
                entity.Create();
                db.B_Roles.Add(entity);
                return await scope.SaveChangesAsync() > 0 ? entity.RoleID.ToString() : string.Empty;
            }
        }

        //删
        public async Task<bool> DeleteAsync(IEnumerable<int> ids)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entities =await db.B_Roles.Where(x => ids.Contains(x.RoleID)).ToListAsync();
                foreach(var entity in entities)
                {
                    entity.IsDeleted = 1;
                }
                await scope.SaveChangesAsync();
                return true;
            }
        }

        //查
        public async Task<RoleDto> FindAsync(int RoleID)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = await db.B_Roles.FindAsync(RoleID);
                var dto = _mapper.Map<B_RoleEntity, RoleDto>(entity);
                if (dto == null)
                {
                    return new RoleDto();
                }
                return dto;
            }
        }

        //查列表
        public async Task<PagedResult<RoleDto>> SearchAsync(RoleFilter filter)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var query = db.B_Roles.Where(x => x.IsDeleted != 1)
                    .WhereIf(filter.keywords.IsNotBlank(), x => x.RoleName.Contains(filter.keywords));
                return await query.OrderByCustom(filter.sidx, filter.sord).Select(item => new RoleDto
                {
                    RoleID=item.RoleID,
                    RoleName=item.RoleName,
                    Description=item.Description,
                    Remark=item.Remark,
                    IsDeleted=item.IsDeleted
                }).PagingAsync(filter.page, filter.rows);
            }
        }

        //改
        public async Task<bool> UpdateAsync(RoleDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity =await db.B_Roles.LoadAsync(dto.RoleID);
                entity.RoleName = dto.RoleName;
                entity.Description = dto.Description;
                entity.Remark = dto.Remark;
                entity.IsDeleted = dto.IsDeleted;
                await scope.SaveChangesAsync();
                return true;
            }
        }
    }
}
