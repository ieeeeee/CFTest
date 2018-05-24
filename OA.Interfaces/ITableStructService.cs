using OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Interfaces
{
    public interface ITableStructService
    {

        Task<List<TableStructDto>> GetAddTableStructAsync(int TableID);
    }
}
