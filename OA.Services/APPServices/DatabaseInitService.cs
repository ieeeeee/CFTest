using OA.Interfaces;
using Mehdime.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.Data;

namespace OA.Services.AppServices
{
    //数据库初始化
    public class DatabaseInitService:IDatabaseInitService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public DatabaseInitService(IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        //初始化数据库
        public void Init()
        {
            InitData.Init();
        }
    }
}
