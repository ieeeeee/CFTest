using OA.Basis;
using OA.Models;
using OA.Models.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OA.Interfaces
{
    public interface IDeptService
    {
        //增加
        Task<string> AddAsync(DeptDto dto);

        //删
        Task<bool> DeleteAsync(IEnumerable<int> ids);

        //查
        Task<DeptDto> FindAsync(int DeptID);

        //查列表
        Task<PagedResult<DeptDto>> SearchAsync(DeptFilter filter);

        //改
        Task<bool> UpdateAsync(DeptDto dto);
        Task<List<DeptDto>> GetEntDeptInfo(int loginUserID);
    }
}