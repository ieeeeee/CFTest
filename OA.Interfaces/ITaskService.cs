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
    public interface ITaskService
    {
        //增加
        Task<string> AddAsync(TaskDto dto);

        //删
        Task<bool> DeleteAsync(IEnumerable<string> ids);

        //查
        Task<TaskDto> FindAsync(string planID);

        //查列表
        Task<PagedResult<TaskDto>> SearchAsync(TaskFilter filter);

        //改
        Task<bool> UpdateAsync(TaskDto dto);
    }
}
