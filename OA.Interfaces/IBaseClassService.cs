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
    public interface IBaseClassService
    {

        //增加
        Task<string> AddASync(BaseClassDto dto);

        //删
        Task<bool> DeleteAsync(IEnumerable<int> ids);

        //查
        Task<BaseClassDto> FindAsync(int ID);

        //查列表
        Task<PagedResult<BaseClassDto>> SearchAsync(BaseClassFilter filter);

        //改
        Task<bool> UpdateAsync(BaseClassDto dto);
    }
}
