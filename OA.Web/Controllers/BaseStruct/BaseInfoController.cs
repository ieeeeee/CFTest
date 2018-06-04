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
    public class BaseInfoController : BaseMsgController
    {
        private readonly IBaseInfoService _baseInfoService;
        public BaseInfoController(IBaseInfoService baseInfoService)
        {
            _baseInfoService = baseInfoService;
        }
        #region View
        // GET: DeptInfo
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Add(int id)
        {
            var model = await _baseInfoService.FindAsync(id);
            if (model == null)
                return View(new BaseInfoDto());
            return View(model);
        }
        #endregion

        #region GetInfo

        public async Task<JsonResult> GetAllBaseInfo(BaseInfoFilter filter)
        {
            var result = await _baseInfoService.SearchAsync(filter);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetDetailInfo(int id)
        {
            var result = await _baseInfoService.FindAsync(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //根据分类ID 获取这个分类的所有字典项

        public async Task<JsonResult> GetClassInfo(int baseClassID)
        {
            var result = await _baseInfoService.GetClassInfo(baseClassID);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region OperateResult

        public async Task<JsonResult> Save(BaseInfoDto dto)
        {
            var result = string.Empty;
            var success = false;
            if (ModelState.IsValid)
            {
                if (dto.BaseInfoID == 0)
                {
                    result = await _baseInfoService.AddASync(dto);
                    if (result.IsNotBlank())
                        return OkOperate(result);
                    return FailOperate("添加失败");
                }
                else
                {
                    success = await _baseInfoService.UpdateAsync(dto);
                    return Json(success);
                }

            }
            else
                return FailOperate("验证失败");
        }

        public async Task<JsonResult> Del(IList<int> ids)
        {
            var success = new JsonResultModel<bool>();
            if(ids.AnyOne())
            {
                success.flag = await _baseInfoService.DeleteAsync(ids);
            }

            return Json(success, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}