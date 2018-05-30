using AutoMapper;
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
using OA.Basis.Extentions;
using OA.Data.Entity;
using OA.Basis;
using OA.Models.Filters;

namespace OA.Services.AppServices
{
    public class MenuService:IMenuService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IMapper _mapper;
        public MenuService(IDbContextScopeFactory dbContextScopeFactory,IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
        }
        //根据主键获得菜单信息
        public async Task<MenuDto> FindAsync(int menuID)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity = await db.B_Menus.FindAsync(menuID);
                var dto = _mapper.Map<B_MenuEntity, MenuDto>(entity);
                if (dto == null)
                {
                    return new MenuDto();
                }
                return dto;
            }
        }
        //获取所有菜单分页列表
        public async Task<PagedResult<MenuDto>> SearchAsync(MenuFilter menuFilter)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var query =db.B_Menus.Where(item => item.IsDeleted != 1)
                    .WhereIf(menuFilter.keywords.IsNotBlank(), x => x.MenuName.Contains(menuFilter.keywords))
                    .WhereIf(menuFilter.ExcludeType.HasValue, x => x.MenuType != (byte)menuFilter.ExcludeType.Value);
                return await query.OrderByCustom(menuFilter.sidx, menuFilter.sord)
                    .Select(item => new MenuDto
                    {
                        MenuID = item.MenuID,
                        ParentID = item.ParentID,
                        MenuName = item.MenuName,
                        Url = item.Url,
                        OrderID = item.OrderID,
                        Icon = item.Icon,
                        MenuType = item.MenuType
                    }).PagingAsync(menuFilter.page,menuFilter.rows);
            }
        }
        //获取主菜单列表
        public async Task<List<MenuDto>> GetMenuInfo()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var query = db.B_Menus.Where(x => x.ParentID == 1 && x.IsDeleted != 1);
                return await query.OrderBy(x => x.OrderID).Select(item => new MenuDto
                {
                    MenuID=item.MenuID,
                    ParentID=item.ParentID,
                    MenuName=item.MenuName,
                    Url=item.Url,
                    OrderID=item.OrderID,
                    Icon=item.Icon,
                    MenuType=item.MenuType
                }).ToListAsync(); //using System.Data.Entity;
            }
        }
        // 获取主菜单下的子菜单列表
        public async Task<List<MenuDto>> GetSubMenuInfo(int MenuID,int LoginUserID)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var user = db.B_Users.Find(LoginUserID);
                if(user.B_Roles.AnyOne())
                {
                    var roleIDs = user.B_Roles.Select(x => x.RoleID);
                    var query = db.B_Menus.Where(x => x.ParentID == MenuID && x.IsDeleted != 1&&
                    x.B_Roles.Any(r=>roleIDs.Contains(r.RoleID)));

                    return await query.OrderBy(x => x.OrderID).Select(item => new MenuDto
                    {
                        MenuID = item.MenuID,
                        ParentID = item.ParentID,
                        MenuName = item.MenuName,
                        Url = item.Url,
                        OrderID = item.OrderID,
                        Icon = item.Icon,
                        MenuType = item.MenuType
                    }).ToListAsync();
                }
                return null;
                
            }
        }

        //添加一个菜单
        public async Task<string> AddAsync(MenuDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var entity = _mapper.Map<MenuDto,B_MenuEntity> (dto);
                entity.Create();
                var db = scope.DbContexts.Get<OAContext>();
                db.B_Menus.Add(entity);
                return await scope.SaveChangesAsync() > 0 ? entity.MenuID.ToString() : string.Empty;
            }
        }

        public async  Task<bool> UpdateAsync(MenuDto dto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entity =  await db.B_Menus.LoadAsync(dto.MenuID);//using DbContextExtension
                entity.MenuName = dto.MenuName;
                entity.MenuType = dto.MenuType;
                entity.Url = dto.Url;
                entity.Icon = dto.Icon;
                entity.ParentID = dto.ParentID;
                entity.OrderID = dto.OrderID;
                entity.Remark = dto.Remark;
                entity.IsDeleted = dto.IsDeleted;
                entity.CreateDateTime = DateTime.Now;
                await  scope.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteAsync(IEnumerable<int> ids)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var db = scope.DbContexts.Get<OAContext>();
                var entities = await db.B_Menus.Where(item => ids.Contains(item.MenuID)).ToListAsync();
                foreach (var menuEntity in entities)
                {
                    menuEntity.IsDeleted = 1;
                }
                await scope.SaveChangesAsync();
                return true;
            }
        }
    }
}
