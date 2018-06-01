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
    public class BaseClassController : BaseMsgController
    {
        private readonly IBaseClassService _baseClassService;
        public BaseClassController(IBaseClassService baseClassService)
        {
            _baseClassService = baseClassService;
        }
        #region View
        // GET: DeptInfo
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Add(int id)
        {
            var model = await _baseClassService.FindAsync(id);
            if (model == null)
                return View(new BaseClassDto());
            return View(model);
        }
        #endregion

        #region GetInfo

        public async Task<JsonResult> GetAllBaseClass(BaseClassFilter filter)
        {
            var result = await _baseClassService.SearchAsync(filter);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetDetailInfo(int id)
        {
            var result = await _baseClassService.FindAsync(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region OperateResult

        public async Task<JsonResult> Save(BaseClassDto dto)
        {
            var result = string.Empty;
            var success = false;
            if (ModelState.IsValid)
            {
                if (dto.BaseClassID == 0)
                {
                    result = await _baseClassService.AddASync(dto);
                    if (result.IsNotBlank())
                        return OkOperate(result);
                    return FailOperate("添加失败");
                }
                else
                {
                    success = await _baseClassService.UpdateAsync(dto);
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
                success.flag = await _baseClassService.DeleteAsync(ids);
            }
            return Json(success, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}