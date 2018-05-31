using AutoMapper;
using Mehdime.Entity;
using OA.Basis;
using OA.Basis.Extentions;
using OA.Data;
using OA.Interfaces;
using OA.Models;
using OA.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.Data.Entity;
using OA.Basis.Utilities;
using System.Data.Entity;

namespace OA.Services.AppServices
{
    public class EntService:IEntService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IMapper _mapper;
        public EntService(IDbContextScopeFactory dbContextScopeFactory,IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
        }
        //添加企业
        public async Task<string> AddAsync(EntDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var entity = _mapper.Map<EntDto, B_EnterpriseEntity>(dto);
                entity.Create();
                entity.GroupID= BaseIdGenerator.Instance.GetNo();
                entity.CreateDateTime = DateTime.Now;
                var db = scope.DbContexts.Get<OAContext>();
                db.B_Enterprises.Add(entity);
                return await scope.SaveChangesAsync()>0 ? entity.EntID.ToString() : string.Empty;
            }
        }

        //根据主键获得DTO
        public async Task<EntDto> FindAsync(int EntID)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = await db.B_Enterprises.FindAsync(EntID);
                var dto = _mapper.Map<B_EnterpriseEntity,EntDto>(entity);
                if (dto == null)
                {
                    return new EntDto();
                }
                return dto;
            }
               
        }
        //查询企业信息
        public async Task<PagedResult<EntDto>> SearchAsync(EntFilter entFilter)
        {
            if (entFilter == null)
                return new PagedResult<EntDto>(1, 6);
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

        //删除企业
        public async Task<bool> DeleteAsync(IEnumerable<int> ids)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entities = await db.B_Enterprises.Where(item => ids.Contains(item.EntID)).ToListAsync();
                foreach(var entEntity in entities)
                {
                    entEntity.IsDeleted = 1;
                }
                await scope.SaveChangesAsync();
                return true;
            }
        }

       public async Task<bool> UpdateAsync(EntDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = await db.B_Enterprises.LoadAsync(dto.EntID);
                entity.EntName = dto.EntName;
                entity.Tel = dto.Tel;
                entity.Address = dto.Address;
                entity.Remark = dto.Remark;
                entity.CreateDateTime = DateTime.Now;
                await scope.SaveChangesAsync();
                return true;
            }
        }
    }
}
