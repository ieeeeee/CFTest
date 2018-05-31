using OA.Basis;
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
    public class EntInfoController : BaseMsgController
    {
        private readonly IEntService _entService;

        public EntInfoController(IEntService entService)
        {
            _entService = entService;
        }

        #region View
        // GET: EntInfo
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Add(int id)
        {
            var model = await _entService.FindAsync(id);
            if (model == null)
            {
                return View(new EntDto());
            }
            return View(model);
        }

        public async Task<ActionResult> Edit(int entID)
        {
            var model = await _entService.FindAsync(entID);
            return View(model);
        }
        #endregion

        #region GetInfo
        //获取企业信息
        public async Task<JsonResult> GetEntInfo(EntFilter entFilter)
        {
            var result = await _entService.SearchAsync(entFilter);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //根据主键或得企业信息
        public async Task<JsonResult> GetDetailInfo(int id)
        {
            var result = await _entService.FindAsync(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region OperateResult
        //添加企业信息
        [HttpPost]
        public async Task<ActionResult> Add(EntDto dto)
        {
            if(ModelState.IsValid)
            {
                var result = await _entService.AddAsync(dto);
                if(result.IsNotBlank())
                {
                    return RedirectToAction("Index");
                }
               
            }
            return View(dto);
        }
        //添加或修改保存
        public async Task<JsonResult> Save(EntDto dto)
        {
            var result = string.Empty;
            var success = false;
            if(ModelState.IsValid)
            {
                if(dto.EntID==0)
                {
                    result = await _entService.AddAsync(dto);
                    if (result.IsNotBlank())
                        return OkOperate(result);
                    else
                        return FailOperate("添加失败");
                }
                else
                {
                    success = await _entService.UpdateAsync(dto);
                    return Json(success);
                }
            }
            return FailOperate("验证失败");
        }
        //删除
        public async Task<JsonResult> Del(IList<int> ids)
        {
            var result = new JsonResultModel<bool>();
            if (ids.AnyOne())
            {
                result.flag = await _entService.DeleteAsync(ids);
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}