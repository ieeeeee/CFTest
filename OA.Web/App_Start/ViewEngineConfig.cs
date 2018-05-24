using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.Web.App_Start
{
    public class ViewEngineConfig:RazorViewEngine
    {
        public ViewEngineConfig()
        {
            base.AreaViewLocationFormats = new string[]
            {
                 "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                //以下我们的规则
                "~/Views/System/{1}/{0}.cshtml",
                "~/Views/BaseStruct/{1}/{0}.cshtml",
                "~/Views/CustomerMgr/{1}/{0}.cshtml",
                "~/Views/SelfCenter/{1}/{0}.cshtml",
                "~/Views/WorkerCenter/{1}/{0}.cshtml"
            };
            base.AreaMasterLocationFormats = new string[]
           {
                 "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                //以下我们的规则
                "~/Views/System/{1}/{0}.cshtml",
                "~/Views/BaseStruct/{1}/{0}.cshtml",
                "~/Views/CustomerMgr/{1}/{0}.cshtml",
                "~/Views/SelfCenter/{1}/{0}.cshtml",
                "~/Views/WorkerCenter/{1}/{0}.cshtml"
           };
            base.MasterLocationFormats = new string[]
           {
                 "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                //以下我们的规则
                "~/Views/System/{1}/{0}.cshtml",
                "~/Views/BaseStruct/{1}/{0}.cshtml",
                "~/Views/CustomerMgr/{1}/{0}.cshtml",
                "~/Views/SelfCenter/{1}/{0}.cshtml",
                "~/Views/WorkerCenter/{1}/{0}.cshtml"
           };
            base.AreaPartialViewLocationFormats = new string[]
           {
                 "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                //以下我们的规则
                "~/Views/System/{1}/{0}.cshtml",
                "~/Views/BaseStruct/{1}/{0}.cshtml",
                "~/Views/CustomerMgr/{1}/{0}.cshtml",
                "~/Views/SelfCenter/{1}/{0}.cshtml",
                "~/Views/WorkerCenter/{1}/{0}.cshtml"
           };
            base.PartialViewLocationFormats = new string[]
           {
                 "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                //以下我们的规则
                "~/Views/System/{1}/{0}.cshtml",
                "~/Views/BaseStruct/{1}/{0}.cshtml",
                "~/Views/CustomerMgr/{1}/{0}.cshtml",
                "~/Views/SelfCenter/{1}/{0}.cshtml",
                "~/Views/WorkerCenter/{1}/{0}.cshtml"
           };
            base.ViewLocationFormats = new string[]
           {
                 "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                //以下我们的规则
                "~/Views/System/{1}/{0}.cshtml",
                "~/Views/BaseStruct/{1}/{0}.cshtml",
                "~/Views/CustomerMgr/{1}/{0}.cshtml",
                "~/Views/SelfCenter/{1}/{0}.cshtml",
                "~/Views/WorkerCenter/{1}/{0}.cshtml"
           };
            base.FileExtensions = new string[]
            {
                "cshtml",
            };
        }
    }
}