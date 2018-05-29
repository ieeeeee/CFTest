using OA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OA.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly ITableStructService _tableStructService;

        public BaseController(IMenuService menuService,ITableStructService tableStructService)
        {
            _menuService = menuService;
            _tableStructService = tableStructService;
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
    }
}