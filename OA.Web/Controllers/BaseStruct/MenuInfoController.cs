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
    public class MenuInfoController : Controller
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
        public ActionResult Add()
        {
            return View();
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
    }
}