﻿using OA.Basis;
using OA.Models;
using OA.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Interfaces
{
   public  interface IEntService
    {
         Task<string> AddAsync(EntDto dto);

         Task<bool> DeleteAsync(IEnumerable<int> ids);

         Task<EntDto> FindAsync(int entID);

         Task<PagedResult<EntDto>> SearchAsync(EntFilter entFilter);

         Task<bool> UpdateAsync(EntDto dto);
         Task<List<EntDto>> GetEntMenuList(string loginUserID);

         Task<bool> WriteLockEnt(int loginUserID, int lockEntID);
    }
}
