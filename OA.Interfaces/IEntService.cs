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
    public interface IEntService
    {
        public Task<PagedResult<EntDto>> Search(EntFilter entFilter);
    }
}
