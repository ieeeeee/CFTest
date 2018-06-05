using OA.Basis.Extentions;
using OA.Basis.Utilities;
using OA.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

//种子数据
namespace OA.Data
{
   internal sealed  class DbConfiguration:DbMigrationsConfiguration<OAContext>
    {
        private readonly DateTime _now = new DateTime(2018, 5, 9, 23, 59, 59);
        private readonly BaseIdGenerator _instance = BaseIdGenerator.Instance; //using OA.Basis.Utilities;
        public DbConfiguration()
        {
            AutomaticMigrationsEnabled = true;//启用自动迁移
            AutomaticMigrationDataLossAllowed = true;//是否允许接受数据丢失的情况，false 不允许,会抛异常，true 允许，数据有可能丢失
        }

        protected override void Seed(OAContext context)
        {
            if(!context.Set<S_SystemConfigEntity>().Any(item=>item.IsDataInited)) //any表示集合中的任一个元素满足表达式条件，即返回true。 all表示集合中的所有元素满足表达式条件，即返回true。
            {
                #region 菜单
                var system = new B_MenuEntity
                {
                    MenuName = "系统菜单",
                    Icon = "fa fa-gear",
                    Url = "#",
                    MenuType = 1,
                    OrderID = 1,
                    CreateDateTime = _now,
                    ParentID = -1
                };
                var baseStruct = new B_MenuEntity
                {
                    MenuName = "基础结构",
                    Icon="fa fa-gears",
                    Url = "#",
                    MenuType = 1,
                    ParentID = system.MenuID,
                    OrderID = 1,
                    CreateDateTime = _now
                };
                var entMgr = new B_MenuEntity
                {
                    MenuName = "企业管理",
                    Icon = "lnr lnr-apartment",
                    Url = "/BaseStruct/EntInfo",
                    MenuType = 2,
                    ParentID = baseStruct.MenuID,
                    OrderID = 2,
                    CreateDateTime = _now
                };
                var departmentMgr = new B_MenuEntity
                {
                    MenuName = "部门管理",
                    Icon = "fa fa-sitemap",
                    Url = "/BaseStruct/DepartMentInfo",
                    MenuType = 2,
                    ParentID = baseStruct.MenuID,
                    OrderID = 3,
                    CreateDateTime = _now
                };
                var userMgr = new B_MenuEntity
                {
                    MenuName = "用户管理",
                    Icon = "fa fa-users",
                    Url = "/BaseStruct/UserInfo",
                    MenuType = 2,
                    ParentID = baseStruct.MenuID,
                    OrderID = 4,
                    CreateDateTime = _now
                };
                var roleMenuMgr = new B_MenuEntity
                {
                    MenuName = "角色授权",
                    Icon = "fa fa-usersecret",
                    Url = "/RoleMenu/Index",
                    MenuType = 3,
                    ParentID = baseStruct.MenuID,
                    OrderID = 5,
                    CreateDateTime = _now
                };
                var userRoleMgr = new B_MenuEntity
                {
                    MenuName = "用户授权",
                    Icon = "fa fa-user-md",
                    Url = "/BaseStruct/UserRoleMgr",
                    MenuType = 3,
                    ParentID = baseStruct.MenuID,
                    OrderID = 6,
                    CreateDateTime = _now
                };
                var workCenter = new B_MenuEntity
                {
                    MenuName = "工作中心",
                    Icon = "fa fa-laptop",
                    Url = "#",
                    MenuType = 1,
                    ParentID = system.MenuID,
                    OrderID = 7,
                    CreateDateTime = _now
                };
                var planMgr = new B_MenuEntity
                {
                    MenuName = "计划单",
                    Icon = "fa fa-text-o",
                    Url = "/WorkerCenter/PlanList/Index",
                    MenuType = 2,
                    ParentID = workCenter.MenuID,
                    OrderID = 8,
                    CreateDateTime = _now
                };
                var taskMgr = new B_MenuEntity
                {
                    MenuName = "任务单",
                    Icon = "fa fa-word-o",
                    Url = "/WorkerCenter/TaskList/Index",
                    MenuType = 2,
                    ParentID = workCenter.MenuID,
                    OrderID = 9,
                    CreateDateTime = _now
                };
                var meetingMgr = new B_MenuEntity
                {
                    MenuName = "会议纪录",
                    Url = "/WorkerCenter/MeetingReport/Index",
                    MenuType = 2,
                    ParentID = workCenter.MenuID,
                    OrderID = 10,
                    CreateDateTime = _now
                };
                var newsMgr = new B_MenuEntity
                {
                    MenuName = "公司公告",
                    Icon = "fa fa-bullhorn",
                    Url = "/WorkerCenter/CompanyNews/Index",
                    MenuType = 2,
                    ParentID = workCenter.MenuID,
                    OrderID = 11,
                    CreateDateTime = _now
                };
                var docMgr = new B_MenuEntity
                {
                    MenuName = "文档中心",
                    Icon = "fa-folder",
                    Url = "/WorkerCenter/Documents/Index",
                    MenuType = 2,
                    ParentID = workCenter.MenuID,
                    OrderID = 12,
                    CreateDateTime = _now
                };
                var customerMgr = new B_MenuEntity
                {
                    MenuName = "客户管理",
                    Url = "#",
                    MenuType = 1,
                    ParentID = system.MenuID,
                    OrderID = 13,
                    CreateDateTime = _now
                };
                var customerInfo = new B_MenuEntity
                {
                    MenuName = "客户信息",
                    Url = "/CustomerMgr/Customer/Index",
                    MenuType = 2,
                    ParentID = customerMgr.MenuID,
                    OrderID = 14,
                    CreateDateTime = _now
                };
                var contactsMgr = new B_MenuEntity
                {
                    MenuName = "联系人",
                    Icon = "fa fa-users",
                    Url = "/CustomerMgr/Cantacts/Index",
                    MenuType = 2,
                    ParentID = customerMgr.MenuID,
                    OrderID = 15,
                    CreateDateTime = _now
                };
                var selfCenter = new B_MenuEntity
                {
                    MenuName = "个人中心",
                    Icon = "fa fa-heartbeat",
                    Url = "#",
                    MenuType = 1,
                    ParentID = system.MenuID,
                    OrderID = 16,
                    CreateDateTime = _now
                };
                var feeMgr = new B_MenuEntity
                {
                    MenuName = "费用申请",
                    Icon = "fa fa-money",
                    Url = "/selfCenter/FeeMgr/Index",
                    MenuType = 2,
                    ParentID = selfCenter.MenuID,
                    OrderID = 17,
                    CreateDateTime = _now
                };
                var leaveMgr = new B_MenuEntity
                {
                    MenuName = "请假申请",
                    Icon = "fa fa-send-o",
                    Url = "/selfCenter/LeaveMgr/Index",
                    MenuType = 2,
                    ParentID = selfCenter.MenuID,
                    OrderID = 18,
                    CreateDateTime = _now
                };
                //报表中心

                var menus = new List<B_MenuEntity>
                {
                    system,
                    baseStruct,
                    entMgr,
                    departmentMgr,
                    userMgr,
                    roleMenuMgr,
                    userRoleMgr,
                    workCenter,
                    planMgr,
                    taskMgr,
                    meetingMgr,
                    newsMgr,
                    docMgr,
                    customerMgr,
                    customerInfo,
                    contactsMgr,
                    selfCenter,
                    feeMgr,
                    leaveMgr
                };

                #endregion

                #region 角色
                var superAdminRole = new B_RoleEntity
                {
                    RoleName = "超级管理员",
                    Description = "超级管理员",
                    B_Menus = menus
                };
                var entAdminRole = new B_RoleEntity
                {
                    RoleName = "企业管理员",
                    Description = "企业管理员",
                    B_Menus = menus
                };
                var guestRole = new B_RoleEntity
                {
                    RoleName = "游客",
                    Description = "游客",
                    B_Menus = menus.Where(a => a.MenuType != 3).ToList()
                };

                var roles = new List<B_RoleEntity>
                {
                    superAdminRole,
                    entAdminRole,
                    guestRole
                };
                #endregion

                #region 用户
                var admin = new B_UserEntity
                {
                    UserName = "OAadmin",
                    NickName = "超级管理员",
                    Password = EnDecryption.MD5Encrypt("qazwsx"), //using OA.Basis.Extentions;
                    PlainCode="qazwsx",
                    Email = "",
                    CreateDateTime = _now,
                    B_Roles = new List<B_RoleEntity> { superAdminRole }
                };
                var guest = new B_UserEntity {
                    UserName = "guest",
                    NickName = "游客",
                    Password = EnDecryption.MD5Encrypt("123456"), //using OA.Basis.Extentions;
                    PlainCode="123456",
                    Email = "",
                    CreateDateTime = _now,
                    B_Roles = new List<B_RoleEntity> { guestRole }
                };
                var user = new List<B_UserEntity> {
                    admin,
                    guest
                };
                #endregion

                #region 系统配置
                var sysConfig = new List<S_SystemConfigEntity> {
                    new S_SystemConfigEntity
                    {
                        SystemConfigID=_instance.GetId(),
                        SystemName="CFTestOA",
                        IsDataInited=true,
                        DataInitedDate=_now,
                        CreateDateTime=_now,
                        IsDeleted=0
                    }
                };
                #endregion
                AddOrUpdate(context, m => m.UserName, user.ToArray());
                AddOrUpdate(context, m => new { m.ParentID, m.MenuName }, menus.ToArray());
                AddOrUpdate(context, m => m.RoleName, roles.ToArray());
                AddOrUpdate(context, m => m.SystemName, sysConfig.ToArray());
            }
            //base.Seed(context);
        }

        void AddOrUpdate<T>(DbContext context, Expression<Func<T, object>> exp, T[] param) where T : class
        {
            var set = context.Set<T>();
            set.AddOrUpdate(exp, param);
        }
    }
}
