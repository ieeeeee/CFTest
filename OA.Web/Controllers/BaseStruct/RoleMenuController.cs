using OA.Basis.Extentions;
using OA.Interfaces;
using OA.Models;
using OA.Models.Filters;
using OA.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OA.Web.Controllers.BaseStruct
{
    public class RoleMenuController : BaseMsgController
    {
        private readonly IRoleMenuService  _roleMenuService;
        private readonly IMenuService _menuService;
        public RoleMenuController(IRoleMenuService roleMenuService, IMenuService menuService)
        {
            _roleMenuService = roleMenuService;
            _menuService = menuService;
        }
        #region View
        // GET: DeptInfo
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Add(int id)
        {
            var model = await _roleMenuService.FindAsync(id);
            if (model == null)
                return View(new RoleDto());
            return View(model);
        }
        #endregion

        #region GetInfo

        public async Task<JsonResult> GetAllRoleInfo(RoleFilter filter)
        {
            var result = await _roleMenuService.SearchAsync(filter);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetDetailInfo(int id)
        {
            var result = await _roleMenuService.FindAsync(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //加载菜单
        public async Task<ActionResult> AuthenMenuData()
        {
            var result = await _menuService.GetMenuTreeAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //根据角色ID 获取对应的菜单
        public async Task<JsonResult> AuthenRoleMenus(int id)
        {
            var list = await _menuService.GetMenusByRoleIDAsync(id);
            var menuIDs = list?.Select(item => item.MenuID);
            return Json(menuIDs, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region OperateResult

        public async Task<JsonResult> Save(RoleDto dto)
        {
            var result = string.Empty;
            var success = false;
            if(ModelState.IsValid)
            {
                if(dto.RoleID==0)
                {
                    result = await _roleMenuService.AddASync(dto);
                    if (result.IsNotBlank())
                        return OkOperate(result);
                    return FailOperate("添加失败");
                }
                else
                {
                    success = await _roleMenuService.UpdateAsync(dto);
                    return Json(success);
                }
            }
            else
            {
                return FailOperate("验证失败");
            }
        }

        public async Task<JsonResult> Del(IList<int> ids)
        {
            var success = new JsonResultModel<bool>();
            if(ids.AnyOne())
            {
                success.flag = await _roleMenuService.DeleteAsync(ids);
                
            }
            return Json(success, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 角色菜单授权
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public async Task<JsonResult> SaveRoleMenus(List<RoleMenuDto> datas)
        {
            var success = new JsonResultModel<bool>();
            if(datas.AnyOne())
            {
                success.flag = await _roleMenuService.SaveRoleMenusAsync(datas);
            }
            else
            {
                success.msg = "请选择要授权的菜单";
            }
            return Json(success, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 清空该角色下的所有菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JsonResult> ClearRoleMenus(int id)
        {
            var success = new JsonResultModel<bool>
            {
                flag = await _roleMenuService.ClearRoleMenusAsync(id)
            };
            return Json(success, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}