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

namespace OA.Web.Controllers.WorkerCenter
{
    public class PlanListController : BaseMsgController
    {
        private readonly IPlanService _planService;
        public PlanListController(IPlanService planService)
        {
            _planService = planService;
        }
        #region View
        // GET: DeptInfo
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Add(string id)
        {
            var model = await _planService.FindAsync(id);
            if (model == null)
                return View(new PlanDto());
            return View(model);
        }
        #endregion

        #region GetInfo

        public async Task<JsonResult> GetAllPlanInfo(PlanFilter filter)
        {
            var result = await _planService.SearchAsync(filter);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetDetailInfo(string id)
        {
            var result = await _planService.FindAsync(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region OperateResult

        public async Task<JsonResult> Save(PlanDto dto)
        {
            var result = string.Empty;
            var success = false;
            if(ModelState.IsValid)
            {
                if(dto.PlanID=="")
                {
                    result =await  _planService.AddASync(dto);
                    if (result.IsNotBlank())
                        return OkOperate(result);
                    return FailOperate("添加失败");
                }
                else
                {
                    success = await _planService.UpdateAsync(dto);
                    return Json(success);
                }
            }
            else
            {
                return FailOperate("验证失败");
            }
        }

        public async Task<JsonResult> Del(IList<string> ids)
        {
            var success = new JsonResultModel<bool>();
            if(ids.AnyOne())
            {
                success.flag = await _planService.DeleteAsync(ids);
            }
            return Json(success, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}