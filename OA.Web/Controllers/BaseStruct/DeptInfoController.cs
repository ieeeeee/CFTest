using OA.Models;
using OA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using OA.Models.Filters;
using OA.Basis.Extentions;
using OA.Web.Models;

namespace OA.Web.Controllers.BaseStruct
{
    public class DeptInfoController : BaseMsgController
    {
        private readonly IDeptService _deptService;
        public DeptInfoController(IDeptService deptService)
        {
            _deptService = deptService;
        }
        #region View
        // GET: DeptInfo
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Add(int id)
        {
            var model = await _deptService.FindAsync(id);
            if (model == null)
                return View(new DeptDto());
            return View(model);
        }
        #endregion

        #region GetInfo

        public async Task<JsonResult> GetAllDeptInfo(DeptFilter filter)
        {
            var result = await _deptService.SearchAsync(filter);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetDetailInfo(int id)
        {
            var result = await _deptService.FindAsync(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region OperateResult
         public async Task<JsonResult> Save(DeptDto dto)
        {
            var result = string.Empty;
            var success = false;
            if(ModelState.IsValid)
            {
                if (dto.DepartmentID == 0)
                {
                    result = await _deptService.AddAsync(dto);
                    if (result.IsNotBlank())
                    {
                        return OkOperate(result);
                    }
                    return FailOperate("添加失败");
                }
                else
                {
                    success = await _deptService.UpdateAsync(dto);
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
                success.flag = await _deptService.DeleteAsync(ids);
            }
            return Json(success, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}