using OA.Data.Config;
using OA.Data.Entity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace OA.Data
{
    public class OAContext : DbContext //using System.Data.Entity;
    {
        //DbSet表示上下文中所有实体的集合，或者可以从数据库中查询给定类型的数据库。 DbSet对象是从中创建的使用DbContext.Set方法的DbContext。
        #region DbSet
        public DbSet<B_BaseClassEntity> B_BaseClasses { get; set; }
        public DbSet<B_BaseInfoEntity> B_BaseInfos { get; set; }
        public DbSet<B_DepartmentEntity> B_Departments { get; set; }
        public DbSet<B_EnterpriseEntity> B_Enterprises { get; set; }
        public DbSet<B_LogEntity> B_Logs{ get; set; }
        public DbSet<B_MenuEntity> B_Menus { get; set; }
        public DbSet<B_RoleEntity> B_Roles { get; set; }

        public DbSet<B_TableStructEntity> B_TableStructEntities { get; set; }


        public DbSet<B_UserEntity> B_Users { get; set; }
        public DbSet<C_ContactsEntity> C_Contacts { get; set; }
        public DbSet<C_CustomerEntity> C_Customers { get; set; }
        public DbSet<P_ApplyOperatorEntity> P_ApplyOperators { get; set; }
        public DbSet<P_FeeApplyEntity> P_FeeApplies { get; set; }
        public DbSet<P_LeaveApplyEntity> P_LeaveApplies { get; set; }

        public DbSet<W_CompanyNewsEntity> W_CompanyNews { get; set; }
        public DbSet<W_DocumentEntity> W_Documents { get; set; }
        public DbSet<W_MeetingReportEntity> W_Meetings { get; set; }
        public DbSet<W_PlanListEntity> W_PlanLists { get; set; }
        public DbSet<W_TaskListEntity> W_TaskLists { get; set; }
        public DbSet<W_TaskOperatorEntity> W_TaskOperators { get; set; }

        public DbSet<S_SystemConfigEntity> S_SystemConfigs { get; set; }


        #endregion


        public OAContext():base("CFTestOA")
        {
#if DEBUG
            Database.Log += WriteSql;
#endif
        }

        public OAContext(string connectionString) : base(connectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除一对多的级联删除关系
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //移除表名复数形式
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            //配置实体和数据表的关系
            modelBuilder.Configurations.AddFromAssembly(typeof(B_UserConfig).Assembly);

        }


        private void WriteSql(string result)
        {
            Debug.WriteLine(result); //using System.Diagnostics;
        } 
    }
}
