using System.Data.Entity;

//初始化数据
namespace OA.Data
{
    public static class InitData
    {
        public static void Init()
        {
            //启用EF自动迁移
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<OAContext,DbConfiguration>());//注意DbConfiguration中继承OAContext
            //关闭自动迁移，从不创建数据库(不建议使用此方法)
            //Database.SetInitializer<OAContext>(null);
        }
    }
}