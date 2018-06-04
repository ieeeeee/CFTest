﻿using OA.Basis.Extentions;
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
    public class UserInfoController : BaseMsgController
    {
        private readonly IUserService _userService;
        public UserInfoController(IUserService userService)
        {
            _userService = userService;
        }
        #region View
        // GET: DeptInfo
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Add(int id)
        {
            var model = await _userService.FindAsync(id);
            if (model == null)
                return View(new UserDto());
            return View(model);
        }
        #endregion

        #region GetInfo

        public async Task<JsonResult> GetAllUserInfo(UserFilter filter)
        {
            var result = await _userService.SearchAsync(filter);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetDetailInfo(int id)
        {
            if (id <= 0)
            {
                id = int.Parse(User.Identity.GetLoginUserID());
            }
            var result = await _userService.FindAsync(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region OperateResult

        public async Task<JsonResult> Save(UserAddDto dto)
        {
            var result = string.Empty;
            var success = false;
            if(ModelState.IsValid)
            {
                if(dto.UserID==0)
                {
                    result = await _userService.AddASync(dto);
                    if(result.IsNotBlank())
                    {
                        return OkOperate(result);
                    }
                    return FailOperate("添加失败");
                }
                else
                {
                    success = await _userService.UpdateAsync(dto);
                    return Json(success);
                }
            }
            return FailOperate("验证失败");
        }

        public async Task<JsonResult> Del(IList<int> ids)
        {
            var success = new JsonResultModel<bool>();
            if(ids.AnyOne())
            {
                success.flag = await _userService.DeleteAsync(ids);
            }
            return Json(success, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}