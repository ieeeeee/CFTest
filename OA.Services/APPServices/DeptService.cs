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
using System.Threading.Tasks;

namespace OA.Services.AppServices
{
    public  class DeptService:IDeptService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IMapper _mapper;
        public DeptService(IDbContextScopeFactory dbContextScopeFactory,IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
        }
        //增加
        public async Task<string> AddAsync(DeptDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = _mapper.Map<DeptDto, B_DepartmentEntity>(dto);//查看OAModuleInitializer
                entity.Create();
                entity.CreateDateTime = DateTime.Now;
                db.B_Departments.Add(entity);
                return await scope.SaveChangesAsync() > 0 ? entity.DepartmentID.ToString() : string.Empty;
            }
        }

        //删
       public async  Task<bool> DeleteAsync(IEnumerable<int> ids)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entities = await db.B_Departments.Where(item => ids.Contains(item.DepartmentID)).ToListAsync();
                foreach(var entity in entities)
                {
                    entity.IsDeleted = 1;
                }
                await scope.SaveChangesAsync();
                return true;     
            }
        }

        //查
       public async  Task<DeptDto> FindAsync(int DeptID)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = await db.B_Departments.FindAsync(DeptID);
                var dto = _mapper.Map<B_DepartmentEntity, DeptDto>(entity);
                if (dto == null)
                {
                    return new DeptDto();
                }
                return dto;
            }
        }

        //查列表
       public async  Task<PagedResult<DeptDto>> SearchAsync(DeptFilter filter)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var query = db.B_Departments.Where(x => x.IsDeleted != 1)
                          .WhereIf(filter.keywords.IsNotBlank(), x => x.DeptNo.Contains(filter.keywords) || x.DeptName.Contains(filter.keywords));
                return await query.OrderByCustom(filter.sidx, filter.sord)
                    .Select(item => new DeptDto
                    {
                        DepartmentID = item.DepartmentID,
                        DeptNo = item.DeptNo,
                        DeptName = item.DeptName,
                        IsDeleted = item.IsDeleted,
                        Remark = item.Remark
                    }).PagingAsync(filter.page, filter.rows);
            }
        }

        //改
        public async Task<bool> UpdateAsync(DeptDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = await db.B_Departments.LoadAsync(dto.DepartmentID);
                entity.DeptName = dto.DeptName;
                entity.DeptNo = dto.DeptNo;
                entity.IsDeleted = dto.IsDeleted;
                entity.Remark = dto.Remark;
                await scope.SaveChangesAsync();
                return true;
            }
        }
    }
}