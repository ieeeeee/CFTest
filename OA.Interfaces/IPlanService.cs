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
    public interface IPlanService
    {
        //增加
        Task<string> AddASync(PlanDto dto);

        //删
        Task<bool> DeleteAsync(IEnumerable<string> ids);

        //查
        Task<PlanDto> FindAsync(string planID);

        //查列表
        Task<IList<PlanTabDto>> SearchAsync(PlanFilter filter);

        //改
        Task<bool> UpdateAsync(PlanDto dto);

        //更改计划状态
        Task<bool> ChangeStatus(int status, IEnumerable<string> ids);
    }
}
