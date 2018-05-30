using OA.Basis;
using OA.Models;
using OA.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Interfaces
{
    public interface IMenuService
    {
        //根据主键查找信息
        Task<MenuDto> FindAsync(int menuID);
        //获取所有菜单分页
        Task<PagedResult<MenuDto>> SearchAsync(MenuFilter menuFilter);
        //获取主菜单
        Task<List<MenuDto>> GetMenuInfo();

        //获取主菜单下的子菜单
        Task<List<MenuDto>> GetSubMenuInfo(int MenuID,int LoginUserID);

        //添加一个菜单
        Task<string> AddAsync(MenuDto dto);

        //编辑一个菜单
        Task<bool> UpdateAsync(MenuDto dto);

        //批量删除
        Task<bool> DeleteAsync(IEnumerable<int> ids);
    }
}
