using OA.Basis;
using OA.Models;
using OA.Models.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

//用户契约
namespace OA.Interfaces
{
    public interface IUserService
    {
        //登录
        Task<UserLoginDto> Login(LoginDto dto);

        //增加
        Task<string> AddASync(UserAddDto dto);

        //删
        Task<bool> DeleteAsync(IEnumerable<int> ids);

        //查
        Task<UserDto> FindAsync(int ID);

        //查列表
        Task<PagedResult<UserDto>> SearchAsync(UserFilter filter);

        //改
        Task<bool> UpdateAsync(UserAddDto dto);
        
        /// <summary>
        /// 用户角色授权
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        Task<bool> AddRightsAsync(int userID, int roleID);

        /// <summary>
        /// 用户角色取消授权
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        Task<bool> CancelRightsAsync(int userID, int roleID);
    }
}