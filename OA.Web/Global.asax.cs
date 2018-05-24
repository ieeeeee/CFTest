using OA.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using System.Configuration;
using OA.Interfaces;

namespace OA.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly bool MiniProfileEnabled = bool.Parse(ConfigurationManager.AppSettings["MiniProfileEnabled"]);
        protected void Application_Start()
        {
            //用MiniProfiler.EF来监控调试MVC和EF的性能，查看生成的sql语句、运行了哪些sql，以及所花的时间
            if (MiniProfileEnabled)
                MiniProfilerEF6.Initialize();//Nuget  MiniProfiler.EF6

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IocConfig.Register();
            ModelBinderConfig.RegisterModelBinders(ModelBinders.Binders);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ViewEngineConfig());

            //初始化数据库
            var dbInitService = DependencyResolver.Current.GetService<IDatabaseInitService>();
            dbInitService.Init();
        }

        protected void Application_BeginRequest()
        {
            if (MiniProfileEnabled)
                MiniProfiler.Start();
        }

        protected void Application_EndRequest()
        {
            if (MiniProfileEnabled)
                MiniProfiler.Stop();
        }
    }
}
