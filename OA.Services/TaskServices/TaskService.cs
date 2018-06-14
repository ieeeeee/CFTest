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

namespace OA.Services.TaskServices
{
   public class TaskService:ITaskService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IMapper _mapper;
        public TaskService(IDbContextScopeFactory dbContextScopeFactory, IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
        }
        //增加
        public async Task<string> AddAsync(TaskDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = _mapper.Map<TaskDto, W_TaskListEntity>(dto);
                entity.Create();
                entity.LastTime = DateTime.Now;
                db.W_TaskLists.Add(entity);
                return await scope.SaveChangesAsync() > 0 ? entity.TaskID : string.Empty;
            }
        }

        //删
        public async Task<bool> DeleteAsync(IEnumerable<string> ids)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entities = await db.W_TaskLists.Where(x=>ids.Contains(x.TaskID)).ToListAsync();
                foreach(var entity in entities)
                {
                    entity.IsDeleted = 1;
                }
                await scope.SaveChangesAsync();
                return true;
            }
        }

        //查
        public async Task<TaskDto> FindAsync(string TaskID)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = await db.W_TaskLists.FindAsync(TaskID);
                var dto = _mapper.Map<W_TaskListEntity, TaskDto>(entity);
                if (dto == null)
                {
                    return new TaskDto();
                }
                return dto;
            }
        }

        //查列表
        public async Task<PagedResult<TaskDto>> SearchAsync(TaskFilter filter)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var query = db.W_TaskLists.Where(x => x.IsDeleted != 1)
                    .WhereIf(filter.keywords.IsNotBlank(), x => x.TaskTitle.Contains(filter.keywords) || x.TaskType.Contains(filter.keywords));
                return await query.OrderByCustom(filter.sidx, filter.sord)
                    .Select(item => new TaskDto
                    {
                        TaskID = item.TaskID,
                        TaskTitle = item.TaskTitle,
                        TaskBody = item.TaskBody,
                        TaskType = item.TaskType,
                        Remark = item.Remark,
                        IsDeleted = item.IsDeleted
                    }).PagingAsync(filter.page, filter.rows);
            }
        }

        //改
        public async Task<bool> UpdateAsync(TaskDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = await db.W_TaskLists.LoadAsync(dto.TaskID);
                entity.TaskTitle = dto.TaskTitle;
                entity.TaskBody = dto.TaskBody;
                entity.TaskStatus = dto.TaskStatus;
                entity.TaskType = dto.TaskType;
                entity.TaskAppr = dto.TaskAppr;
                entity.LastTime = DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}
