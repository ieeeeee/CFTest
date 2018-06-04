using OA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OA.Web.Controllers
{
    public class BaseController : BaseMsgController
    {
        private readonly IMenuService _menuService;
        private readonly ITableStructService _tableStructService;
        private readonly IEntService _entService;

        public BaseController(IMenuService menuService,ITableStructService tableStructService,IEntService entService)
        {
            _menuService = menuService;
            _tableStructService = tableStructService;
            _entService = entService;
        }
        // GET: Base
        public ActionResult Index()
        {
            return View();
        }

        //获取一级主菜单
        public async Task<JsonResult> GetMenuInfo()
        {
            var menuList = await _menuService.GetMenuInfo();
            return Json(menuList, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetSubMenuInfo(int MenuID)
        {
            var menuList = await _menuService.GetSubMenuInfo(MenuID,Int32.Parse(User.Identity.GetLoginUserID()));
            return Json(menuList, JsonRequestBehavior.AllowGet);
        }

        //获取表结构
        public async Task<JsonResult> GetAddTableStruct(int TableID)
        {
            var result = await _tableStructService.GetAddTableStructAsync(TableID);
            for (var i = 0; i < result.Count(); i++)
            {

            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        //获取菜单上的下拉列表
        public async Task<JsonResult> GetEntMenuList()
        {
            var result = await _entService.GetEntMenuList(User.Identity.GetLoginUserID());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> WriteLockEnt(int lockEntID)
        {
            var success = await _entService.WriteLockEnt(int.Parse(User.Identity.GetLoginUserID()),lockEntID);
            return Json(success);
        }

        public async Task<JsonResult> WriteLockSubMenu(int subMenuID)
        {
            var success = await _menuService.WriteLockSubMenu(int.Parse(User.Identity.GetLoginUserID()),subMenuID);
            return Json(success);
        }
    }
}