using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OA.Basis;
using OA.Basis.Extentions;
using OA.Interfaces;
using OA.Models;
using OA.Models.Filters;

namespace OA.Web.Controllers.BaseStruct
{
    public class MenuInfoController : BaseMsgController
    {
        private readonly IMenuService _menuService;
        public MenuInfoController(IMenuService menuservice)
        {
            _menuService = menuservice;
        }
        
        // GET: MenuInfo
        public ActionResult Index()
        {
            return View();
        }

        //Add
        public async Task<ActionResult> Add(int menuID)
        {
            var model = await _menuService.FindAsync(menuID);
            return View(model);
        }
        //Edit
        public async Task<ActionResult> Edit(int menuID)
        {
            var model = await _menuService.FindAsync(menuID);
            return View(model);
        }
        
        //index加载获取 分页menuList
        public async Task<JsonResult> GetAllMenuInfo(MenuFilter menuFilter)
        {
            var result = await _menuService.SearchAsync(menuFilter);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // Add页面提交 新增一个菜单
        [HttpPost]
        public async Task<ActionResult> Add(MenuDto dto)
        {
            if(ModelState.IsValid)
            {
                var result = await _menuService.AddAsync(dto);
                if(result.IsNotBlank())
                {
                    return RedirectToAction("Index");
                }
            }
            return View(dto);
        }

        //Add页面保存
        public async Task<JsonResult> Save(MenuDto dto)
        {
            var result = string.Empty;
            var success = false;
            if(ModelState.IsValid)
            {
                if(dto.MenuID==0)
                {
                    result = await _menuService.AddAsync(dto);
                    if (result.IsNotBlank())
                    {
                        return OkOperate(result);
                    }
                    else
                        return FailOperate("添加失败");
                }
                else
                {
                    success = await _menuService.UpdateAsync(dto);
                    return Json(success);
                }
            }
            return FailOperate("验证失败");
        }

        //Edit 根据菜单ID获取菜单
        //public async Task<JsonResult> GetMenuInfo(int menuID)
        //{
        //    var 
        //}
    }
}