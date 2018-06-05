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
    public interface IRoleMenuService
    {
        //增加
        Task<string> AddASync(RoleDto dto);

        //删
        Task<bool> DeleteAsync(IEnumerable<int> ids);

        //查
        Task<RoleDto> FindAsync(int RoleID);

        //查列表
        Task<PagedResult<RoleDto>> SearchAsync(RoleFilter filter);

        //改
        Task<bool> UpdateAsync(RoleDto dto);
        
        /// <summary>
        /// 角色授权菜单
        /// </summary>
        Task<bool> SaveRoleMenusAsync(List<RoleMenuDto> datas);

        /// <summary>
        /// 清空该角色下的所有菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ClearRoleMenusAsync(int roleID);
    }
}
