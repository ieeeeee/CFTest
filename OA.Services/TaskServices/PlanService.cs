﻿using AutoMapper;
using Mehdime.Entity;
using OA.Basis;
using OA.Basis.Extentions;
using OA.Basis.Utilities;
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
    public class PlanService:IPlanService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IMapper _mapper;
        public PlanService(IDbContextScopeFactory dbContextScopeFactory, IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
        }
        //增加
        public async Task<string> AddASync(PlanDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = _mapper.Map<PlanDto, W_PlanListEntity>(dto);
                entity.PlanID= BaseIdGenerator.Instance.GetId();
                entity.CreateDateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                db.W_PlanLists.Add(entity);
                return await scope.SaveChangesAsync() > 0 ? entity.PlanID : string.Empty;
            }
        }

        //删
        public async Task<bool> DeleteAsync(IEnumerable<string> ids)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entities =await  db.W_PlanLists.Where(x => ids.Contains(x.PlanID)).ToListAsync();
                foreach(var entity in entities)
                {
                    entity.IsDeleted = 1;
                }
                await scope.SaveChangesAsync();
                return true; 
            }
        }

        /// <summary>
        /// 更改计划表的状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> ChangeStatus(int status, IEnumerable<string> ids)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entities = await db.W_PlanLists.Where(x => ids.Contains(x.PlanID)).ToListAsync();
                foreach(var  entity in entities)
                {
                    entity.ProcStatus = (byte)status;
                }
                await scope.SaveChangesAsync();
                return true;
            }
        }

        //查
        public async Task<PlanDto> FindAsync(string planID)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = await db.W_PlanLists.FindAsync(planID);
                var dto = _mapper.Map<W_PlanListEntity, PlanDto>(entity);
                if(dto==null)
                {
                    return new PlanDto();
                }
                return dto;
            }
        }

        //查列表
        public async Task<PagedResult<PlanTabDto>> SearchAsync(PlanFilter filter)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var query = db.W_PlanLists.Where(x => x.IsDeleted != 1)
                    .WhereIf(filter.CurrStatus != -1, x => x.ProcStatus == filter.CurrStatus)
                    .WhereIf(int.Parse(filter.PlanType) != -1, x => x.PlanType == filter.PlanType)
                    .WhereIf(filter.Operator.IsNotBlank(), x => x.Operator == filter.Operator)
                    .WhereIf(filter.keywords.IsNotBlank(), x => x.PlanID.Contains(filter.keywords) || x.PlanTitle.Contains(filter.keywords));
                 var pagedQuery=query.Skip((filter.page - 1) * filter.rows)
                    .Take(filter.rows);
                
                var dateQuery =await pagedQuery.GroupBy(x => x.PlanDate).Select(p=> (new { PlanDate = p.Key })).ToListAsync();
                List<PlanTabDto> tab = new List<PlanTabDto>();
                foreach(var itemDate in dateQuery)
                {
                    var itemPlan = new PlanTabDto()
                    {
                        PlanDate = itemDate.PlanDate,
                        PlanData = _mapper.Map<List<W_PlanListEntity>,List<PlanDto>>(pagedQuery.Where(x=>x.PlanDate==itemDate.PlanDate).ToList())
                    };
                    tab.Add(itemPlan);
                }
                var result = new PagedResult<PlanTabDto>();
                result.rows = tab;
                result.records = query.Count();
                result.page = filter.page;
                result.pagesize = filter.rows;
                return  result;
               
                //return await dateQuery.OrderBy(x => x.Key).Select(p => new PlanTabDto
                //{
                //    PlanDate = p.Key,
                //    PlanData = p.Select(item => new PlanDto
                //    {
                //        PlanID = item.PlanID,
                //        PlanTitle = item.PlanTitle,
                //        PlanBody = item.PlanBody,
                //        PlanType = item.PlanType,
                //        Remark = item.Remark,
                //        IsDeleted = item.IsDeleted
                //    }).ToList()
                //}).ToListAsync();
                   
              

            }
        }

        //改
        public async Task<bool> UpdateAsync(PlanDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity =await db.W_PlanLists.LoadAsync(dto.PlanID);
                entity.PlanTitle = dto.PlanTitle;
                entity.PlanBody = dto.PlanBody;
                entity.PlanType = dto.PlanType;
                entity.ProcStatus =(byte)dto.ProcStatus;
                entity.UpdateTime = DateTime.Now;
                await scope.SaveChangesAsync();
                return true;
            }
        }
    }
}
