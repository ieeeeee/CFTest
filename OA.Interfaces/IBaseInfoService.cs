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
    public interface IBaseInfoService
    {
        //增加
        Task<string> AddASync(BaseInfoDto dto);

        //删
        Task<bool> DeleteAsync(IEnumerable<int> ids);

        //查
        Task<BaseInfoDto> FindAsync(int ID);

        //查列表
        Task<PagedResult<BaseInfoDto>> SearchAsync(BaseInfoFilter filter);

        //改
        Task<bool> UpdateAsync(BaseInfoDto dto);
        Task<List<BaseInfoDto>> GetClassInfo(int baseClassID);
    }
}
